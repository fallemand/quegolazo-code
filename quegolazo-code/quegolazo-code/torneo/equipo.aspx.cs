using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;

namespace quegolazo_code.torneo
{
    public partial class equipo : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorEquipo gestorEquipo;
        protected GestorPartido gestorPartido;
        GestorEstadisticas gestorEstadistica;
        protected bool tieneLogo;
        protected int idEquipo;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            //TODO esto está harcodeado para que funque! 2008
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(4007);
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorId(87);
            gestorEquipo = Sesion.getGestorEquipo();
            gestorPartido = Sesion.getGestorPartido();
            gestorEstadistica = new GestorEstadisticas(); 
            if (!Page.IsPostBack)
            {
                gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(int.Parse(Request["idEquipo"]));
                cargarDatosPrincipalesEquipo();
                cargarHistorialDePartidos();
                cargarGoleadores();
                cargarRepeaterOtrosEquiposDeEdicion();
                cargarRepeaterJugadores();
            }
        }

        public void cargarDatosPrincipalesEquipo()
        {
            var datosPrincipalesEquipo = gestorEstadistica.obtenerEstadisticasEquipo(gestorEquipo.equipo.idEquipo);
            //Datos Equipo
            ltPuntos.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["Puntos"].ToString() : "-";
            ltGolesAFavor.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["GF"].ToString() : "-";
            ltGolesEnContra.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["GC"].ToString() : "-";
            ltPartidosJugados.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["PJ"].ToString() : "-";
            //Datos Resumen
            ltResumenPartidosJugados.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["PJ"].ToString() : "-";
            ltResumenPartidosGanados.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["PG"].ToString() : "-";
            ltResumenPartidosEmpatados.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["PE"].ToString() : "-";
            ltResumenPartidosPerdidos.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["PP"].ToString() : "-";
            ltResumenGoles.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? (int.Parse(datosPrincipalesEquipo.Rows[0]["GF"].ToString()) - int.Parse(datosPrincipalesEquipo.Rows[0]["GC"].ToString())).ToString() : "-";
            ltResumenGolesConvertidos.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["GF"].ToString() : "-";
            ltResumenGolesEnContra.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["GC"].ToString() : "-";
            ltResumenTarjetas.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? (int.Parse(datosPrincipalesEquipo.Rows[0]["TA"].ToString()) + int.Parse(datosPrincipalesEquipo.Rows[0]["TR"].ToString())).ToString() : "-";
            ltResumenTarjetasAmarillas.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["TA"].ToString() : "-";
            ltResumenTarjetasRojas.Text = (datosPrincipalesEquipo.Rows.Count > 0) ? datosPrincipalesEquipo.Rows[0]["TR"].ToString() : "-";
        }

        public void cargarHistorialDePartidos()
        {
            GestorControles.cargarRepeaterList(rptHistorialPartidos, gestorPartido.ultimosPartidosDeUnEquipo(gestorEquipo.equipo.idEquipo, gestorEdicion.edicion.idEdicion));
            //GestorControles.cargarRepeaterTable(rptHistorialPartidos, gestorEstadistica.obtenerUltimosPartidosEquipo(gestorEquipo.equipo.idEquipo));
        }

        public void cargarGoleadores()
        {
            GestorControles.cargarRepeaterList(rptGoleadores, gestorEquipo.goleadoresDeUnEquipo(gestorEquipo.equipo.idEquipo, gestorEdicion.edicion.idEdicion));
        }


        public void cargarRepeaterOtrosEquiposDeEdicion()
        {
            GestorControles.cargarRepeaterList(rptOtroseEquiposDeEdicion, gestorEdicion.obtenerEquipos());
        }

        public void cargarRepeaterJugadores()
        {
            List<Jugador> jugadores = gestorEquipo.jugadoresDeUnEquipo(gestorEdicion.edicion.idEdicion, gestorEquipo.equipo.idEquipo);
            GestorControles.cargarRepeaterList(rptJugadoresDelEquipo, gestorEquipo.jugadoresDeUnEquipo(gestorEdicion.edicion.idEdicion, gestorEquipo.equipo.idEquipo));
        }
    }
}