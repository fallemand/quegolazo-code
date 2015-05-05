using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;
using Logica;

namespace quegolazo_code.torneo
{
    public partial class sanciones : System.Web.UI.Page
    {
       protected GestorEdicion gestorEdicion;
       protected GestorTorneo gestorTorneo;

        protected int idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            idEdicion = int.Parse(Request["idEdicion"]);
            nickTorneo = Request["nickTorneo"];
            gestorEdicion = Sesion.getGestorEdicion();
            gestorTorneo = Sesion.getGestorTorneo();
            

            if (!Page.IsPostBack)
            {
                gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
                obtenerEdicionSeleccionada();
                cargarRepeaterSanciones(idEdicion);
                cargarRepeaterTarjetas();
            }
    
        }

        private void cargarRepeaterSanciones(int idEdicion)
        {
            GestorSancion gestorSancion = new GestorSancion();
            sinSanciones.Visible = !GestorControles.cargarRepeaterTable(rptSanciones, gestorSancion.obtenerSancionesDeUnaEdicion(idEdicion.ToString()));
        }

        private void cargarRepeaterTarjetas()
        {
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
            sinTarjetas.Visible = !GestorControles.cargarRepeaterTable(rptTarjetas, gestorEstadisticas.obtenerTablaTarjetas());
        }

         protected  void obtenerEdicionSeleccionada()
        {
            gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
            gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
            gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
        }


    }
}