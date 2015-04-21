using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class partido : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorPartido gestorPartido;
        protected GestorEquipo gestorEquipo;
        protected bool hayPartidosPrevios;
        GestorEstadisticas gestorEstadistica;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            //TODO falta agregarle el try/catch y que redirija a una pagina de error...
            int idEdicion = int.Parse(Request["edicion"]);            
            string nickTorneo = Request["nickTorneo"];
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(idEdicion);
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
            serializador = new JavaScriptSerializer();
            string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(gestorTorneo.torneo.idTorneo));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "var configuracion = " + estilos + ";", true);
            gestorEstadistica = new GestorEstadisticas(); 
            gestorPartido = Sesion.getGestorPartido();
            gestorEquipo = Sesion.getGestorEquipo();
            if (!Page.IsPostBack)
            {
                gestorPartido.obtenerPartidoporId(Request["partido"]);
                otrosPartidosDeLaFecha(); // Carga Otros Partidos de la Fecha
                cargarDatosDePartido(); // Carga Resumen y Estadísticas del Partido
                if (!cargarUltimosPartidos())// Carga los Widgets Partidos Anteriores Equipo Local y Visitante
                    cargarProximosPartidos();// Carga Próximos partidos en caso que no haya partidos previos
                cargarComparativo(); //Carga widget Comparativo                
            }
        }

        [System.Web.Services.WebMethod(enableSession: true)]
        public static string guardarConfiguracion(object configuracion)
        {
            try
            {
                new GestorTorneo().registrarConfiguracionVisual(configuracion);
                return "CAMBIOS GUARDADOS!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Carga todos los datos del Partido: RESUMEN Y ESTADÍSTICAS
        //autor: Pau Pedrosa
        public void cargarDatosDePartido()
        {
            cargarResumenDePartido();
            cargarEstadisticasDePartido();
        }

        //Métodos de Carga de Resumen de Partido
        //autor: Pau Pedrosa
        private void cargarResumenDePartido()
        {   //Carga Repeater de Goles
            sinGolesLocal.Visible = !GestorControles.cargarRepeaterList(rptGolesLocal, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.local.idEquipo, true));
            sinGolesVisitante.Visible = !GestorControles.cargarRepeaterList(rptGolesVisitante, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.visitante.idEquipo, false));
            //Carga Repeater de Tarjetas
            sinTarjetasRojasLocal.Visible = !GestorControles.cargarRepeaterList(rptTarjetasRojasLocal, gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.local.idEquipo));
            sinTarjetasRojasVisitante.Visible = !GestorControles.cargarRepeaterList(rptTarjetasRojasVisitante, gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.visitante.idEquipo));
            sinTarjetasAmarillasLocal.Visible = !GestorControles.cargarRepeaterList(rptTarjetasAmarillasLocal, gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.local.idEquipo));
            sinTarjetasAmarillasVisitante.Visible = !GestorControles.cargarRepeaterList(rptTarjetasAmarillasVisitante, gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.visitante.idEquipo));
            //Carga Repeater de Cambios
            sinCambiosLocal.Visible = !GestorControles.cargarRepeaterList(rptCambiosLocal, gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.local.idEquipo));
            sinCambiosVisitante.Visible = !GestorControles.cargarRepeaterList(rptCambiosVisitante, gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.visitante.idEquipo));
        }
        
        //Métodos de Carga de Repeater de Tab Titulares - Goles - Cambios - Tarjetas y Sanciones
        //autor: Pau Pedrosa
        private void cargarEstadisticasDePartido()
        {   //Repeater Titulares - Local y Visitante
            sinTitularesLocal.Visible = !GestorControles.cargarRepeaterList(rptTitularesLocal, gestorPartido.partido.titularesLocal);
            sinTitularesVisitante.Visible = !GestorControles.cargarRepeaterList(rptTitularesVisitante, gestorPartido.partido.titularesVisitante);
            //Repeater Goles  - Local y Visitante
            sinGolesTabLocal.Visible = !GestorControles.cargarRepeaterList(rptTabGolesLocal, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.local.idEquipo));
            sinGolesTabVisitante.Visible = !GestorControles.cargarRepeaterList(rptTabGolesVisitante, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.visitante.idEquipo));
            //Repeater Cambios - Local y Visitante
            sinCambiosTabLocal.Visible = !GestorControles.cargarRepeaterList(rptTabCambiosLocal, gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.local.idEquipo));
            sinCambiosTabVisitante.Visible = !GestorControles.cargarRepeaterList(rptTabCambiosVisitante, gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.visitante.idEquipo));
            //Repeater Tarjetas - Local y Visitante
            sinTarjetasTabLocal.Visible = !(GestorControles.cargarRepeaterList(rptTabTarjetasAmarillasLocal, gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.local.idEquipo)) && GestorControles.cargarRepeaterList(rptTabTarjetasRojasLocal, gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.local.idEquipo)));
            sinTarjetasTabVisitante.Visible = !(GestorControles.cargarRepeaterList(rptTabTarjetasAmarillasVisitante, gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.visitante.idEquipo)) && GestorControles.cargarRepeaterList(rptTabTarjetasRojasVisitante, gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.visitante.idEquipo)));
            //Repeater Sanciones - Local y Visitante
            sinSancionesLocal.Visible = !GestorControles.cargarRepeaterList(rptSancionesLocal, gestorPartido.obtenerSancionesPorEquipo(gestorPartido.partido.local.idEquipo));
            sinSancionesVisitante.Visible = !GestorControles.cargarRepeaterList(rptSancionesVisitante, gestorPartido.obtenerSancionesPorEquipo(gestorPartido.partido.visitante.idEquipo));
        }

        //Carga Repeater de Otros Partidos de la Fecha
        //autor: Pau Pedrosa
        private void otrosPartidosDeLaFecha()
        {
            GestorControles.cargarRepeaterList(rptOtrosPartidosDeLaFecha, gestorPartido.otrosPartidosDeLaFecha(gestorEdicion.edicion.idEdicion, gestorPartido.partido.faseAsociada.idFase, gestorPartido.partido.idFecha, gestorPartido.partido.idPartido));
        }
        
        //Carga los repeater de últimos partidos del equipo local y visitante
        //Devuelve true si hay partidos previos y false si no hay partidos previos
        //autor: Pau Pedrosa
        protected bool cargarUltimosPartidos()
        {
            hayPartidosPrevios = true;
            if (GestorControles.cargarRepeaterList(rptUltimosPartidosEquipoLocal, gestorPartido.ultimosPartidosPrevioDeUnEquipo(gestorPartido.partido.local.idEquipo, gestorEdicion.edicion.idEdicion, gestorPartido.partido.idPartido)) == false ||
                GestorControles.cargarRepeaterList(rptUltimosPartidosEquipoVisitante, gestorPartido.ultimosPartidosPrevioDeUnEquipo(gestorPartido.partido.visitante.idEquipo, gestorEdicion.edicion.idEdicion, gestorPartido.partido.idPartido)) == false)
                hayPartidosPrevios = false;            
            return hayPartidosPrevios;
        }

        //Carga próximos partidos
        //autor: Pau Pedrosa
        private void cargarProximosPartidos()
        {
            GestorControles.cargarRepeaterList(rptProximosPartidosEquipoLocal, gestorPartido.proximosPartidosDeUnEquipo(gestorPartido.partido.local.idEquipo, gestorEdicion.edicion.idEdicion, gestorPartido.partido.idPartido));
            GestorControles.cargarRepeaterList(rptProximosPartidosEquipoVisitante, gestorPartido.proximosPartidosDeUnEquipo(gestorPartido.partido.visitante.idEquipo, gestorEdicion.edicion.idEdicion, gestorPartido.partido.idPartido)); 
        }

        //Carga Widget Comparativo
        //autor: Pau Pedrosa
        private void cargarComparativo()
        {
            var comparativoLocal = gestorEstadistica.obtenerEstadisticasEquipo(gestorPartido.partido.local.idEquipo);
            var comparativoVisitante = gestorEstadistica.obtenerEstadisticasEquipo(gestorPartido.partido.visitante.idEquipo);
            //Equipo Local
            ltPuntosEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["Puntos"].ToString() : "-";
            ltPartGanadosEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["PG"].ToString() : "-";
            ltPartEmpatadosEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["PE"].ToString() : "-";
            ltPartPerdidosEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["PP"].ToString() : "-";
            ltComparativoGolesEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["GF"].ToString() : "-";
            ltComparativoTarjRojasEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["TR"].ToString() : "-";
            ltComparativoTarjAmarillasEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["TA"].ToString() : "-";
            //Equipo Visitante
            ltPuntosEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["Puntos"].ToString() : "-";
            ltPartGanadosEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["PG"].ToString() : "-";
            ltPartEmpatadosEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["PE"].ToString() : "-";
            ltPartPerdidosEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["PP"].ToString() : "-";
            ltComparativoGolesEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["GF"].ToString() : "-";
            ltComparativoTarjRojasEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["TR"].ToString() : "-";
            ltComparativoTarjAmarillasEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["TA"].ToString() : "-";
        }        

        public string nombreMes(int numeroMes)
        {
            return GestorExtra.nombreMes(numeroMes);
        }
        public string nombreDia(DateTime date)
        {
            return GestorExtra.nombreDia(date);
        }
    }
}