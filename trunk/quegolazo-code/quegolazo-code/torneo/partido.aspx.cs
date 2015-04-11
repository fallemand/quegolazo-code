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
        GestorEstadisticas gestorEstadistica;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {

            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            //TODO esto está harcodeado para que funque!
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(2006);
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorId(87);
            serializador = new JavaScriptSerializer();
            string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(gestorTorneo.torneo.idTorneo));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "var configuracion = " + estilos + ";", true);
            gestorEstadistica = new GestorEstadisticas(); 
            gestorPartido = Sesion.getGestorPartido();
            gestorEquipo = Sesion.getGestorEquipo();
            if (!Page.IsPostBack)
            {
                gestorPartido.obtenerPartidoporId(Request["partido"]);
                otrosPartidosDeLaFecha();
                cargarDatosDePartido();
                cargarUltimoPartidoEL();
                cargarUltimoPartidoEV();
                cargarComparativo();                 
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

        public void cargarDatosDePartido()
        {
            cargarResumenDePartido();
            cargarEstadisticasDePartido();
        }

        //Métodos de Carga de Resumen de Partido
        private void cargarResumenDePartido()
        {   //Carga Repeater de Goles
            sinGolesLocal.Visible = !GestorControles.cargarRepeaterList(rptGolesLocal, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.local.idEquipo));
            sinGolesVisitante.Visible = !GestorControles.cargarRepeaterList(rptGolesVisitante, gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.visitante.idEquipo));
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

        private void otrosPartidosDeLaFecha()
        {
            GestorControles.cargarRepeaterList(rptOtrosPartidosDeLaFecha, gestorPartido.otrosPartidosDeLaFecha(gestorEdicion.edicion.idEdicion, gestorPartido.partido.faseAsociada.idFase, gestorPartido.partido.idFecha, gestorPartido.partido.idPartido));
        }
        
        protected List<int> cargarUltimoPartidoEL()
        {
            List<int> idEquipos = new List<int>();
            var ultimoPartidoLocal = gestorEstadistica.ultimoPartidoPrevioDeUnEquipo(gestorPartido.partido.local.idEquipo, gestorPartido.partido.idPartido);
            if (ultimoPartidoLocal.Rows.Count > 0)
            {
                //ULTIMO PARTIDO EQUIPO LOCAL
                ltUltimoPartidoEqLocal.Text = ultimoPartidoLocal.Rows[0]["Equipo Local"].ToString();
                ltUltimoPartidoEqVisitante.Text = ultimoPartidoLocal.Rows[0]["Equipo Visitante"].ToString();
                ltUltimoPartidoGolesLocalEL.Text = ultimoPartidoLocal.Rows[0]["Goles Local"].ToString();
                ltUltimoPartidoGolesVisitanteEL.Text = ultimoPartidoLocal.Rows[0]["Goles Visitante"].ToString();
                litUltimoPartidoFechaEL.Text = ultimoPartidoLocal.Rows[0]["Fecha"].ToString();
                idEquipos.Add(int.Parse(ultimoPartidoLocal.Rows[0]["Id Equipo Local"].ToString()));
                idEquipos.Add(int.Parse(ultimoPartidoLocal.Rows[0]["Id Equipo Visitante"].ToString()));
            }
            else
                sinPartidosPreviosLocal.Visible = true;
            return idEquipos;           
        }

        protected List<int> cargarUltimoPartidoEV()
        {
            List<int> idEquipos = new List<int>();
            var ultimoPartidoLocal = gestorEstadistica.ultimoPartidoPrevioDeUnEquipo(gestorPartido.partido.visitante.idEquipo, gestorPartido.partido.idPartido);
            if (ultimoPartidoLocal.Rows.Count > 0)
            {
                //ULTIMO PARTIDO EQUIPO VISITANTE
                ltPartidoPrevioEquipoLocalEV.Text = ultimoPartidoLocal.Rows[0]["Equipo Local"].ToString();
                ltPartidoPrevioEquipoVisitanteEV.Text = ultimoPartidoLocal.Rows[0]["Equipo Visitante"].ToString();
                ltPartidoPrevioGolesLocalEV.Text = ultimoPartidoLocal.Rows[0]["Goles Local"].ToString();
                ltPartidoPrevioGolesVisitanteEV.Text = ultimoPartidoLocal.Rows[0]["Goles Visitante"].ToString();
                litPartidoPrevioFechaEV.Text = ultimoPartidoLocal.Rows[0]["Fecha"].ToString();
                idEquipos.Add(int.Parse(ultimoPartidoLocal.Rows[0]["Id Equipo Local"].ToString()));
                idEquipos.Add(int.Parse(ultimoPartidoLocal.Rows[0]["Id Equipo Visitante"].ToString()));
            }
            else
                sinPartidosPreviosVisitante.Visible = true;
            return idEquipos;
        }

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

        public string MonthName(int month)
        {
            System.Globalization.CultureInfo ci = null;
            System.Globalization.DateTimeFormatInfo dtfi = null;
            ci = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            dtfi = ci.DateTimeFormat;
            return dtfi.GetMonthName(month);
        }

        public string DayMonthName(DateTime date)
        {
            System.Globalization.CultureInfo ci = null;
            ci = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            return date.ToString("dddd", ci).ToUpper();
        }
    }
}