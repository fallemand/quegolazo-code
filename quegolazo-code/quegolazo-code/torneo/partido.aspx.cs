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
        protected GestorPartido gestorPartido;
        GestorEstadisticas gestorEstadistica;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {

            gestorTorneo = Sesion.getGestorTorneo();
            //TODO esto está harcodeado para que funque!
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorId(88);
            serializador = new JavaScriptSerializer();
            string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(gestorTorneo.torneo.idTorneo));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "var configuracion = " + estilos + ";", true);

            gestorPartido = Sesion.getGestorPartido();
            if (!Page.IsPostBack)
            {
                gestorPartido.obtenerPartidoporId(Request["partido"]);
                cargarDatosDePartido();
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
    }
}