using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;
using System.Data;
using System.Web.Script.Serialization;

namespace quegolazo_code.torneo
{
    public partial class equipo : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorEquipo gestorEquipo;
        protected GestorPartido gestorPartido;
        private List<Jugador> goleadoresDelEquipo;
        private DataTable datosPrincipalesEquipo;
        GestorEstadisticas gestorEstadistica;
        protected int idEquipo, idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);
                    Equipo equipo = GestorUrl.validarEquipo(torneo.nick, edicion.idEdicion);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;
                    nickTorneo = torneo.nick;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    idEdicion = edicion.idEdicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();

                    gestorEquipo = new GestorEquipo();
                    gestorEquipo.equipo = equipo;
                    idEquipo = equipo.idEquipo;

                    gestorPartido = new GestorPartido();
                    gestorEstadistica = new GestorEstadisticas(edicion);

                    cargarDatosPrincipalesEquipo();
                    cargarHistorialDePartidos();
                    cargarGoleadores();
                    cargarRepeaterOtrosEquiposDeEdicion();
                    cargarRepeaterJugadores();
                    cargarGraficos();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void cargarGraficos()
        {
            if (datosPrincipalesEquipo.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "partidos", "var datosPartidos=" + gestorEstadistica.generarDatosParaGraficoPArtidos(datosPrincipalesEquipo) + ";", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goles", "var datosGoles=" + gestorEstadistica.generarDatosParaGraficoGoles(datosPrincipalesEquipo) + ";", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "puntos", "var datosEvolucionPuntos=" + gestorEstadistica.generarDatosParaGraficoEvolucionDePuntos(gestorEquipo.equipo.idEquipo, gestorEdicion.edicion.fases) + ";", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goleadores", "var datosGoleadores=" + gestorEstadistica.generarDatosGoleadores(goleadoresDelEquipo) + ";", true);
            }
            else
            {
                sinGraficoPartidos.Visible = true;
                sinGraficoGoles.Visible = true;
                sinGraficoPuntos.Visible = true;
                sinGraficoGoleadores.Visible = true;
            }
        
        }

        public void cargarDatosPrincipalesEquipo()
        {
            datosPrincipalesEquipo = gestorEstadistica.obtenerEstadisticasEquipo(gestorEquipo.equipo.idEquipo);
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
        {//Carga el historial de Partido
           sinHistorialDePartido.Visible = !GestorControles.cargarRepeaterList(rptHistorialPartidos, gestorPartido.ultimosPartidosDeUnEquipo(gestorEquipo.equipo.idEquipo, gestorEdicion.edicion.idEdicion));
           //divGraficoPuntos.Visible = !sinHistorialDePartido.Visible;
           sinGraficoPuntos.Visible = sinHistorialDePartido.Visible;
        }
        public void cargarGoleadores()
        {//Carga los goleadores de la edición
            goleadoresDelEquipo = gestorEquipo.goleadoresDeUnEquipo(gestorEquipo.equipo.idEquipo, gestorEdicion.edicion.idEdicion);
            sinGoleadores.Visible = !GestorControles.cargarRepeaterList(rptGoleadores, goleadoresDelEquipo);
            //divGraficoGoleadores.Visible = !sinGoleadores.Visible;//no visualizar grafico cuando no hay goleadores
            sinGraficoGoleadores.Visible = sinGoleadores.Visible;//visualizar cartel cuando no hay goleadores
            
        }
        public void cargarRepeaterOtrosEquiposDeEdicion()
        {//Carga el repeater de los otros equipos de la edición
            GestorControles.cargarRepeaterList(rptOtroseEquiposDeEdicion, gestorEdicion.obtenerEquipos());
        }                
        public void cargarRepeaterJugadores()
        {//Carga el repeater con todos los datos del Jugador
            sinJugadores.Visible = !GestorControles.cargarRepeaterList(rptJugadoresDelEquipo, gestorEquipo.jugadoresDeUnEquipo(gestorEdicion.edicion.idEdicion, gestorEquipo.equipo.idEquipo));
        }
        protected void rptHistorialPartidos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Contempla el caso que el partido se haya definido por penales
                Literal ltPenalesLocal = (Literal)e.Item.FindControl("ltPenalesLocal");
                Literal ltPenalesVisitante = (Literal)e.Item.FindControl("ltPenalesVisitante");
                if(((Partido)e.Item.DataItem).huboPenales != null && ((Partido)e.Item.DataItem).huboPenales == true)
                {
                    ltPenalesLocal.Visible = true;
                    ltPenalesLocal.Text = "(" + ((Partido)e.Item.DataItem).penalesLocal.ToString() + ")";
                    ltPenalesVisitante.Visible = true;
                    ltPenalesVisitante.Text = "(" + ((Partido)e.Item.DataItem).penalesVisitante.ToString() + ")";
                }
            }
        }
    }
}