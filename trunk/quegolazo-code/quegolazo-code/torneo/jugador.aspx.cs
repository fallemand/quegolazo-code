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
    public partial class Formulario_web12 : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorEquipo gestorEquipo;
        protected GestorJugador gestorJugador;
        protected GestorEstadisticas gestorEstadisticas;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(2010);
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorId(87);
            gestorEquipo = Sesion.getGestorEquipo();
            gestorJugador = Sesion.getGestorJugador();
            gestorEstadisticas = Sesion.getGestorEstadisticas();

            if (!Page.IsPostBack)
            {
                //gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(int.Parse(Request["idEquipo"]));
                gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(1);
                GestorControles.cargarRepeaterList(rptOtroseJugadores, gestorEquipo.equipo.jugadores);
                gestorJugador.jugador = gestorJugador.obtenerJugadorPorId(2068);
                cargarDatosJugador();
                cargarPartidosJugador();
                cargarGolesJugador();
            }
        }



        private void cargarDatosJugador()
        {
            var datosPrincipalesJugador= gestorEstadisticas.estadisticasDeUnJugador(gestorJugador.jugador.idJugador);

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
           GestorControles.cargarRepeaterTable(rptHistorialPartidos,gestorEstadisticas.obtenerUltimosPartidosJugador(gestorJugador.jugador.idJugador));
        }

        private void cargarGolesJugador()
        {

            GestorControles.cargarRepeaterTable(rptGolesJugador, gestorEstadisticas.ultimosGolesDeUnJugador(gestorJugador.jugador.idJugador));
        }

        protected void rptOtroseJugadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

          if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Literal litIniciales = (Literal)e.Item.FindControl("litIniciales");
                     string [] split = ((Jugador)e.Item.DataItem).nombre.Split(new Char [] {' '});

                foreach (string s in split) 
                {
                        if (s.Trim() != "")
                            litIniciales.Text+=s.Substring(0,1);
                 }
                   }
        
           
        }


     
    }
}