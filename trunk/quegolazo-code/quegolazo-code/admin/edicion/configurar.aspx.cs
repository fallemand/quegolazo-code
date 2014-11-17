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
                if (rdJugadoresSi.Checked)
                {                    
                    gestorEdicion.edicion.preferencias.jugadoresXPartido = rdJugadoresRegistroSi.Checked;
                    gestorEdicion.edicion.preferencias.jugadoresGoles = rdJugadoresGolesSi.Checked;
                    gestorEdicion.edicion.preferencias.jugadoresTarjetas = rdJugadoresTarjetasSi.Checked;
                    gestorEdicion.edicion.preferencias.jugadoresCambios = rdJugadoresCambiosSi.Checked;
                }
                //Preferencias Árbitros
                gestorEdicion.edicion.preferencias.arbitros = rdArbitrosSi.Checked;
                if (rdArbitrosSi.Checked)
                {                    
                    gestorEdicion.edicion.preferencias.arbitrosAsignaXPartido = rdArbitrosPorPartidoSi.Checked;
                    gestorEdicion.edicion.preferencias.arbitrosRegistraDesempenio = rdArbitroDesempenioSi.Checked;
                }
                //Preferencias Sanciones
                gestorEdicion.edicion.preferencias.sanciones = rdSancionesSi.Checked;
                if (rdSancionesSi.Checked)
                {                    
                    gestorEdicion.edicion.preferencias.sancionesEquipos = rdSancionesEquiposSi.Checked;
                    gestorEdicion.edicion.preferencias.sancionesJugadores = rdSancionesJugadoresSi.Checked; 
                }
                //Preferencia Canchas
                gestorEdicion.edicion.preferencias.canchas = rdCanchasSi.Checked;
                if (rdCanchasSi.Checked)
                    gestorEdicion.edicion.preferencias.canchaJueganEnComplejo = rdCanchasComplejos.Checked;                 
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
                rdJugadoresGolesSi.Checked = gestorEdicion.edicion.preferencias.jugadoresGoles;
                rdJugadoresGolesNo.Checked = !gestorEdicion.edicion.preferencias.jugadoresGoles;
                rdJugadoresTarjetasSi.Checked = gestorEdicion.edicion.preferencias.jugadoresTarjetas;
                rdJugadoresTarjetasNo.Checked = !gestorEdicion.edicion.preferencias.jugadoresTarjetas;
                rdJugadoresCambiosSi.Checked = gestorEdicion.edicion.preferencias.jugadoresCambios;
                rdJugadoresCambiosNo.Checked = !gestorEdicion.edicion.preferencias.jugadoresCambios;
            }
            //Preferencias Árbitros
            if (gestorEdicion.edicion.preferencias.arbitros)
            {
                rdArbitrosSi.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelSanciones", "showPanel('panelArbitros');", true);
                rdArbitrosPorPartidoSi.Checked = gestorEdicion.edicion.preferencias.arbitrosAsignaXPartido;
                rdArbitrosPorPartidoNo.Checked = !gestorEdicion.edicion.preferencias.arbitrosAsignaXPartido;
                rdArbitroDesempenioSi.Checked = gestorEdicion.edicion.preferencias.arbitrosRegistraDesempenio;
                rdArbitroDesempenioNo.Checked = !gestorEdicion.edicion.preferencias.arbitrosRegistraDesempenio;
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
                if (gestorEdicion.edicion.preferencias.canchaJueganEnComplejo)
                    rdCanchasComplejos.Checked = true;
                else
                    rdCanchasEquipos.Checked = true;                 
                rdCanchasEquipos.Checked = !gestorEdicion.edicion.preferencias.canchaJueganEnComplejo;
            }
        }
    }
}