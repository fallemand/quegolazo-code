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

namespace quegolazo_code.torneo
{
    public partial class Formulario_web12 : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorEquipo gestorEquipo;
        protected GestorJugador gestorJugador;
        protected GestorEstadisticas gestorEstadisticas;
        protected int idJugador, idEdicion, idEquipo;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                gestorEquipo = Sesion.getGestorEquipo();
                gestorJugador = Sesion.getGestorJugador();

                idJugador = int.Parse(Request["idJugador"]);
                nickTorneo = Request["nickTorneo"].ToString();
                idEdicion = int.Parse(Request["idEdicion"]);
                idEquipo = (int.Parse(Request["idEquipo"]) != 0) ? int.Parse(Request["idEquipo"]) : gestorJugador.obtenerIdEquipo(idJugador);
                gestorTorneo = Sesion.getGestorTorneo();
                gestorEdicion = Sesion.getGestorEdicion();

                if (!Page.IsPostBack)
                {
                    gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(idEdicion);//2010
                    gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo); //jockeyClub
                    Sesion.setTorneo(gestorTorneo.torneo);
                    gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(idEquipo);//1
                    GestorControles.cargarRepeaterList(rptOtroseJugadores, gestorEquipo.equipo.jugadores);
                    gestorJugador.jugador = gestorJugador.obtenerJugadorPorId(idJugador);//2068
                    gestorEstadisticas = Sesion.getGestorEstadisticas();
                    cargarDatosJugador();
                    cargarPartidosJugador();
                    cargarGolesJugador();
                    cargarGraficoGoles();
                }
                
            }
            catch (Exception)
            {
                //TODO redireccionar a pagina de error
                throw;
            }
            
           
        }

        private void cargarGraficoGoles()
        {
            DataTable datos = gestorEstadisticas.cantidadDeGolesPorTipoGol(idJugador);
            if (datos.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tiposDeGoles", "var tiposDeGoles = " + gestorEstadisticas.generarDatosParaGraficoDeTorta(datos) + ";", true);
            }
            else {
                noGraphics.Visible = true;
                graphics.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tiposDeGoles", "var tiposDeGoles = null;", true);
            }
           
        }

        private void cargarDatosJugador()
        {
            var datosPrincipalesJugador = gestorEstadisticas.estadisticasDeUnJugador(idJugador);

            litPartidoJugados.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["PARTIDOS JUGADOS"].ToString() : "-"; 
            litGolesConvertidos.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Goles Convertidos"].ToString() : "-";
            litAmarillas.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Amarillas"].ToString() : "-";
            litRojas.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Rojas"].ToString() : "-";

            //Resumen Jugador
            litResumenApellido.Text= gestorJugador.jugador.nombre;
            litResumenEdad.Text = (DateTime.Now.Year - ((DateTime)gestorJugador.jugador.fechaNacimiento).Year).ToString();
            litResumenGC.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Goles Convertidos"].ToString() : "-"; ;
            litResumenNroCamiseta.Text = gestorJugador.jugador.numeroCamiseta.ToString();
            litResumenPJ.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["PARTIDOS JUGADOS"].ToString() : "-";
            litResumenTA.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Amarillas"].ToString() : "-";
            litResumenTR.Text = (datosPrincipalesJugador.Rows.Count > 0) ? datosPrincipalesJugador.Rows[0]["Rojas"].ToString() : "-";
        }


        private void cargarPartidosJugador()
        {
          sinHistorialDePartido.Visible= !GestorControles.cargarRepeaterTable(rptHistorialPartidos,gestorEstadisticas.obtenerUltimosPartidosJugador(gestorJugador.jugador.idJugador));
        }

        private void cargarGolesJugador()
        {
          sinGolesJugador.Visible =  !GestorControles.cargarRepeaterTable(rptGolesJugador, gestorEstadisticas.ultimosGolesDeUnJugador(gestorJugador.jugador.idJugador));
        }
    }
}