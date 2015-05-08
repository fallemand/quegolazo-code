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
    public partial class posiciones : System.Web.UI.Page
    {
        protected GestorEdicion gestorEdicion;
        protected GestorTorneo gestorTorneo;
        protected GestorEquipo gestorEquipo = new GestorEquipo();
        protected int idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                idEdicion = int.Parse(Request["idEdicion"]);
                nickTorneo = Request["nickTorneo"];
                gestorEdicion = Sesion.getGestorEdicion();
                gestorTorneo = Sesion.getGestorTorneo();
                gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo); 
                obtenerEdiciónSeleccionada();
                cargarEquipos();
            }
        }

        private void obtenerEdiciónSeleccionada()
        {
            gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
            gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
            gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
        }

        public void cargarEquipos()
        {
            gestorEdicion.getFaseActual();
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas(gestorEdicion.edicion);
            GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[gestorEdicion.faseActual.idFase - 1].grupos);
            GestorControles.cargarRepeaterTable(rptEquipos, gestorEstadisticas.obtenerTablaPosiciones(gestorEdicion.faseActual.idFase));
            GestorControles.cargarRepeaterList(rptListaEquipos, gestorEdicion.edicion.equipos);
        }
    }
}