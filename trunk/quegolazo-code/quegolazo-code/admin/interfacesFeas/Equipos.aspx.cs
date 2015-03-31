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
                imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
                cargarRepeaterEquipos();
                cargarRepeaterJugadores();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        private void cargarRepeaterEquipos()
        {
            sinequipos.Visible = !GestorControles.cargarRepeaterList(rptEquipos, gestorEquipo.obtenerEquiposDeUnTorneo());
        }

        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }

        protected void rptEquipos_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                    lblDelegado2.Text = (delegados[1] != null) ? delegados[1].nombre : "";
                    imagenpreview.Src = gestorEquipo.equipo.obtenerImagenMediana();
                    cargarRepeaterJugadores();
                    cargarDatos(Int32.Parse(e.CommandArgument.ToString()));
                    cargarGoleador(Int32.Parse(e.CommandArgument.ToString()));
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
            lblGanados.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PG"].ToString() : ""; ;//Pedir a Pau
            lblPerdidos.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PP"].ToString() : ""; ;//Pedir a Pau
            lblEmpates.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["PE"].ToString() : ""; ;//Pedir a Pau
            lblGolesFavor.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["GF"].ToString() : ""; ;//Pedir a Pau
            lblGolesContra.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["GC"].ToString() : ""; ;//Pedir a Pau
            lblAmarillas.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["AMARILLAS"].ToString() : ""; ;//Pedir a Pau
            lblRojas.Text = (estadisticasEquipo.Rows.Count > 0) ? estadisticasEquipo.Rows[0]["ROJAS"].ToString() : ""; ;//Pedir a Pau
        }

        private void cargarGoleador(int idEquipo)
        {
            var goleadores = gestorEstadisticas.obtenerGoleadoresDeUnEquipo(idEquipo);
            imgGoleador.Src= (new Jugador(){idJugador=(goleadores.Rows.Count > 0) ? int.Parse(goleadores.Rows[0]["IdJugador"].ToString()):0}).obtenerImagenMediana();
            lblNombreGoleador.Text = (goleadores.Rows.Count > 0) ? goleadores.Rows[0]["Jugador"].ToString() : "";//Pedir a Pau
            lblGolesGoleador.Text = (goleadores.Rows.Count > 0) ? goleadores.Rows[0]["Goles"].ToString() : "";
        }

        private void cargarUltimosPartidos()
        {
            GestorControles.cargarRepeaterTable(rptPartidos, gestorEstadisticas.obtenerUltimosPartidosEquipo(gestorEquipo.equipo.idEquipo)); //;Pedir a Pau list de partidos
        }
    }
}