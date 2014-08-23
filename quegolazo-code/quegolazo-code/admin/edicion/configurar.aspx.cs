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
            gestorEdicion = Sesion.getGestorEdicion();           
        }
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                //Preferencias Jugadores
                gestorEdicion.edicion.preferencias.jugadores = rdJugadoresRegistroSi.Checked;
                gestorEdicion.edicion.preferencias.golesJugadores = rdJugadoresGolesSi.Checked;
                gestorEdicion.edicion.preferencias.tarjetasJugadores = rdJugadoresTarjetasSi.Checked;
                gestorEdicion.edicion.preferencias.cambiosJugadores = rdJugadoresCambiosSi.Checked;
                //Preferencias Árbitros
                gestorEdicion.edicion.preferencias.arbitros = rdArbitrosSi.Checked;
                gestorEdicion.edicion.preferencias.asignaArbitros = rdArbitrosPorPartidoSi.Checked;
                gestorEdicion.edicion.preferencias.desempenioArbitros = rdArbitroDesempenioSi.Checked;
                //Preferencias Sanciones
                gestorEdicion.edicion.preferencias.sanciones = rdSancionesEquiposSi.Checked;
                gestorEdicion.edicion.preferencias.sancionesJugadores = rdSancionesJugadoresSi.Checked;
                //Preferencia Canchas
                gestorEdicion.edicion.preferencias.canchaUnica = rdCanchasComplejos.Checked;
                Response.Redirect(GestorUrl.eEQUIPOS); 
            }
            catch (Exception ex)
            {
                litFracaso.Text = ex.Message;
                panelFracaso.Visible = true;                
            }
        }
    }
}