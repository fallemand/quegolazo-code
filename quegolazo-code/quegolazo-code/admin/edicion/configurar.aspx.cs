using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin.edicion
{
    public partial class configurar : System.Web.UI.Page
    {
        GestorEdicion gestorEdicion = new GestorEdicion();   
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorEdicion = Sesion.getGestorEdicion();
                if (!Page.IsPostBack)
                    cargarPreferencias();
            }
            catch (Exception ex)
            {
                litFracaso.Text = ex.Message;
                panelFracaso.Visible = true;   
            }

        }
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                //Preferencias Jugadores
                gestorEdicion.edicion.preferencias.jugadores = rdJugadoresSi.Checked;
                gestorEdicion.edicion.preferencias.jugadoresXPartido = rdJugadoresRegistroSi.Checked;
                gestorEdicion.edicion.preferencias.golesJugadores = rdJugadoresGolesSi.Checked;
                gestorEdicion.edicion.preferencias.tarjetasJugadores = rdJugadoresTarjetasSi.Checked;
                gestorEdicion.edicion.preferencias.cambiosJugadores = rdJugadoresCambiosSi.Checked;
                //Preferencias Árbitros
                gestorEdicion.edicion.preferencias.arbitros = rdArbitrosSi.Checked;
                gestorEdicion.edicion.preferencias.asignaArbitros = rdArbitrosPorPartidoSi.Checked;
                gestorEdicion.edicion.preferencias.desempenioArbitros = rdArbitroDesempenioSi.Checked;
                //Preferencias Sanciones
                gestorEdicion.edicion.preferencias.sanciones = rdSancionesSi.Checked;
                gestorEdicion.edicion.preferencias.sancionesEquipos = rdSancionesEquiposSi.Checked;
                gestorEdicion.edicion.preferencias.sancionesJugadores = rdSancionesJugadoresSi.Checked;
                //Preferencia Canchas
                gestorEdicion.edicion.preferencias.canchas = rdCanchasSi.Checked;
                gestorEdicion.edicion.preferencias.canchaUnica = rdCanchasComplejos.Checked;
                Response.Redirect(GestorUrl.eEQUIPOS); 
            }
            catch (Exception ex)
            {
                litFracaso.Text = ex.Message;
                panelFracaso.Visible = true;                
            }
        }

        private void cargarPreferencias()
        {
            //Preferencias Jugadores
            if (gestorEdicion.edicion.preferencias.jugadores)
            {
                rdJugadoresSi.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelJugadores", "showPanel('panelJugadores');", true);
                rdJugadoresRegistroSi.Checked = gestorEdicion.edicion.preferencias.jugadoresXPartido;
                rdJugadoresRegistroNo.Checked = !gestorEdicion.edicion.preferencias.jugadoresXPartido;
                rdJugadoresGolesSi.Checked = gestorEdicion.edicion.preferencias.golesJugadores;
                rdJugadoresGolesNo.Checked = !gestorEdicion.edicion.preferencias.golesJugadores;
                rdJugadoresTarjetasSi.Checked = gestorEdicion.edicion.preferencias.tarjetasJugadores;
                rdJugadoresTarjetasNo.Checked = !gestorEdicion.edicion.preferencias.tarjetasJugadores;
                rdJugadoresCambiosSi.Checked = gestorEdicion.edicion.preferencias.cambiosJugadores;
                rdJugadoresCambiosNo.Checked = !gestorEdicion.edicion.preferencias.cambiosJugadores;
            }
            //Preferencias Árbitros
            if (gestorEdicion.edicion.preferencias.arbitros)
            {
                rdArbitrosSi.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelSanciones", "showPanel('panelArbitros');", true);
                rdArbitrosPorPartidoSi.Checked = gestorEdicion.edicion.preferencias.asignaArbitros;
                rdArbitrosPorPartidoNo.Checked = !gestorEdicion.edicion.preferencias.asignaArbitros;
                rdArbitroDesempenioSi.Checked = gestorEdicion.edicion.preferencias.desempenioArbitros;
                rdArbitroDesempenioNo.Checked = !gestorEdicion.edicion.preferencias.desempenioArbitros;
            }
            //Preferencias Sanciones
            if (gestorEdicion.edicion.preferencias.sanciones)
            {
                rdSancionesSi.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelArbitros", "showPanel('panelSanciones');", true);
                rdSancionesEquiposSi.Checked = gestorEdicion.edicion.preferencias.sancionesEquipos;
                rdSancionesEquiposNo.Checked = !gestorEdicion.edicion.preferencias.sancionesEquipos;
                rdSancionesJugadoresSi.Checked = gestorEdicion.edicion.preferencias.sancionesJugadores;
                rdSancionesJugadoresNo.Checked = !gestorEdicion.edicion.preferencias.sancionesJugadores;
            }
            //Preferencia Canchas
            if (gestorEdicion.edicion.preferencias.canchas)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelCanchas", "showPanel('panelCanchas');", true);
                rdCanchasSi.Checked = true;
                rdCanchasComplejos.Checked = gestorEdicion.edicion.preferencias.canchaUnica;
                rdCanchasEquipos.Checked = !gestorEdicion.edicion.preferencias.canchaUnica;
            }
        }
    }
}