using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;
using Logica;
using Utils;

namespace quegolazo_code.admin.interfacesFeas
{
    public partial class Equipos : System.Web.UI.Page
    {
        private GestorEquipo gestorEquipo;
        private GestorJugador gestorJugador;
        private GestorEstadisticas gestorEstadisticas;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo = Sesion.getGestorEquipo();
                gestorJugador = Sesion.getGestorJugador();
                gestorEstadisticas = Sesion.getGestorEstadisticas();
                if (!Page.IsPostBack)
                {
                    imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
                    cargarRepeaterEquipos();
                    cargarRepeaterJugadores();
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        private void cargarRepeaterEquipos()
        {
            sinequipos.Visible = !GestorControles.cargarRepeaterList(rptEquipos1, gestorEquipo.obtenerEquiposDeUnTorneo());
        }

        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }

        protected void rptEquipos1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "elegirEquipo")
                {   //por CommandArgument recibe el ID del equipo a mostrar             
                    gestorEquipo.obtenerEquipoAModificar(Int32.Parse(e.CommandArgument.ToString()));
                    lblNombreEquipo.Text = gestorEquipo.equipo.nombre;
                    lblDirectorTecnico.Text = gestorEquipo.equipo.directorTecnico;
                    List<Delegado> delegados = gestorEquipo.obtenerDelegados();
                    lblDelegado1.Text = (delegados[0] != null) ? delegados[0].nombre : "";
                    lblDelegado2.Text = (delegados.Count>1) ? delegados[1].nombre : "";
                    imagenpreview.Src = gestorEquipo.equipo.obtenerImagenMediana();
                    cargarRepeaterJugadores();
                    cargarDatos(Int32.Parse(e.CommandArgument.ToString()));
                    cargarGoleadores(Int32.Parse(e.CommandArgument.ToString()));
                    cargarUltimosPartidos();
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }  
        }

        protected void rptJugadores_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        private void cargarRepeaterJugadores()
        {
            sinJugadores.Visible = !(GestorControles.cargarRepeaterList(rptJugadores, gestorJugador.obtenerJugadoresDeUnEquipo()));
        }

        private void cargarDatos(int idEquipo)
        {
            var estadisticasEquipo = gestorEstadisticas.obtenerEstadisticasEquipo(idEquipo);
            lblPuntos.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["Puntos"].ToString() : "";
            lblPartidosJugados.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PJ"].ToString() : ""; ;//Pedir a Pau
            lblGanados.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PG"].ToString() : ""; ;//Pedir a Pau
            lblPerdidos.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PP"].ToString() : ""; ;//Pedir a Pau
            lblEmpates.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PE"].ToString() : ""; ;//Pedir a Pau
            lblGolesFavor.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["GF"].ToString() : ""; ;//Pedir a Pau
            lblGolesContra.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["GC"].ToString() : ""; ;//Pedir a Pau
            lblAmarillas.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["AMARILLAS"].ToString() : ""; ;//Pedir a Pau
            lblRojas.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["ROJAS"].ToString() : ""; ;//Pedir a Pau
        }

        private void cargarGoleadores(int idEquipo)
        {
            GestorControles.cargarRepeaterTable(rptGoleadoresEquipo, gestorEstadisticas.obtenerGoleadoresDeUnEquipo(idEquipo)); 
        }

        private void cargarUltimosPartidos()
        {
            GestorControles.cargarRepeaterTable(rptUltimosPartidos, gestorEstadisticas.obtenerUltimosPartidosEquipo(gestorEquipo.equipo.idEquipo)); //;Pedir a Pau list de partidos
        }

      
        
    }
}