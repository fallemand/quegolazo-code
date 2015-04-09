using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.torneo
{
    public partial class partido : System.Web.UI.Page
    {
        GestorTorneo gestorTorneo;
        GestorEdicion gestorEdicion;
        protected GestorPartido gestorPartido;
        protected GestorEquipo gestorEquipo;
        GestorEstadisticas gestorEstadistica;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {

            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            //TODO esto está harcodeado para que funque!
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(1006);
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
            cargarRepeaterGoles();
            cargarRepeaterTarjetas();
            cargarRepeaterCambios();
            cargarRepeaterTabGoles();
            cargarRepeaterTabCambios();
            cargarRepeaterTabTarjetas();
            cargarRepeaterTitulares();
            cargarRepeaterTadSanciones();
        }

        private void cargarRepeaterGoles()
        {
            rptGolesLocal.DataSource = gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.local.idEquipo);
            rptGolesLocal.DataBind();
            rptGolesVisitante.DataSource = gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptGolesVisitante.DataBind();
        }

        private void cargarRepeaterTarjetas()
        {
            rptTarjetasRojasLocal.DataSource = gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTarjetasRojasLocal.DataBind();
            rptTarjetasRojasVisitante.DataSource = gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTarjetasRojasVisitante.DataBind();
            rptTarjetasAmarillasLocal.DataSource = gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTarjetasAmarillasLocal.DataBind();
            rptTarjetasAmarillasVisitante.DataSource = gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTarjetasAmarillasVisitante.DataBind();
        }

        private void cargarRepeaterCambios()
        {
            rptCambiosLocal.DataSource = gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.local.idEquipo);
            rptCambiosLocal.DataBind();
            rptCambiosVisitante.DataSource = gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptCambiosVisitante.DataBind();
        }

        private void cargarRepeaterTabGoles()
        {
            rptTabGolesLocal.DataSource = gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTabGolesLocal.DataBind();
            rptTabGolesVisitante.DataSource = gestorPartido.obtenerGolesPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTabGolesVisitante.DataBind();
        }

        private void cargarRepeaterTabCambios()
        {
            rptTabCambiosLocal.DataSource = gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTabCambiosLocal.DataBind();
            rptTabCambiosVisitante.DataSource = gestorPartido.obtenerCambiosPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTabCambiosVisitante.DataBind();
        }

        private void cargarRepeaterTabTarjetas()
        {
            rptTabTarjetasAmarillasLocal.DataSource = gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTabTarjetasAmarillasLocal.DataBind();
            rptTabTarjetasRojasLocal.DataSource = gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.local.idEquipo);
            rptTabTarjetasRojasLocal.DataBind();
            rptTabTarjetasAmarillasVisitante.DataSource = gestorPartido.obtenerTarjetasAmarillasPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTabTarjetasAmarillasVisitante.DataBind();
            rptTabTarjetasRojasVisitante.DataSource = gestorPartido.obtenerTarjetasRojasPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptTabTarjetasRojasVisitante.DataBind();
        }

        private void cargarRepeaterTitulares()
        {
            rptTitularesLocal.DataSource = gestorPartido.partido.titularesLocal;
            rptTitularesLocal.DataBind();
            rptTitularesVisitante.DataSource = gestorPartido.partido.titularesVisitante;
            rptTitularesVisitante.DataBind();
        }

        private void cargarRepeaterTadSanciones()
        {
            rptSancionesLocal.DataSource = gestorPartido.obtenerSancionesPorEquipo(gestorPartido.partido.local.idEquipo);
            rptSancionesLocal.DataBind();
            rptSancionesVisitante.DataSource = gestorPartido.obtenerSancionesPorEquipo(gestorPartido.partido.visitante.idEquipo);
            rptSancionesVisitante.DataBind();
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
            return idEquipos;
        }

        private void cargarComparativo()
        {
            var comparativoLocal = gestorEstadistica.obtenerEstadisticasEquipo(gestorPartido.partido.local.idEquipo);
            var comparativoVisitante = gestorEstadistica.obtenerEstadisticasEquipo(gestorPartido.partido.local.idEquipo);
            ltPuntosEL.Text = (comparativoLocal.Rows.Count > 0) ? comparativoLocal.Rows[0]["Puntos"].ToString() : "";
            ltPuntosEV.Text = (comparativoVisitante.Rows.Count > 0) ? comparativoVisitante.Rows[0]["Puntos"].ToString() : "";
        }
    }
}