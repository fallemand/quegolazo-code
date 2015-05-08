using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;
using Logica;
using Entidades;

namespace quegolazo_code.torneo
{
    public partial class sanciones : System.Web.UI.Page
    {
       protected GestorEdicion gestorEdicion;
       protected GestorTorneo gestorTorneo;
       protected GestorEquipo gestorEquipo = new GestorEquipo();

        protected int idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;
                    nickTorneo = torneo.nick;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    idEdicion = edicion.idEdicion;

                    cargarRepeaterSanciones(idEdicion);
                    cargarRepeaterTarjetas();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void cargarRepeaterSanciones(int idEdicion)
        {
            GestorSancion gestorSancion = new GestorSancion();
            sinSanciones.Visible = !GestorControles.cargarRepeaterTable(rptSanciones, gestorSancion.obtenerSancionesDeUnaEdicion(idEdicion.ToString()));
        }

        private void cargarRepeaterTarjetas()
        {
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas(gestorEdicion.edicion);
            sinTarjetas.Visible = !GestorControles.cargarRepeaterTable(rptTarjetas, gestorEstadisticas.obtenerTablaTarjetas());
        }
    }
}