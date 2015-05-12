using System;
using Logica;
using Utils;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected int idEdicion;
        protected string nickTorneo;
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorEstadisticas gestorEstadisticas;
        protected GestorPartido gestorPartido;
        protected int idFecha, idFase;
        protected Partido proximoPartido;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack) 
                {
                    
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;
                    nickTorneo = torneo.nick;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    gestorEdicion.getFaseActual();
                    gestorEdicion.faseActual.getFechaActual();
                    gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                    idEdicion = edicion.idEdicion;
                    idFase = gestorEdicion.faseActual.idFase;
                    idFecha = gestorEdicion.faseActual.fechaActual.idFecha;

                    gestorEstadisticas = new GestorEstadisticas(gestorEdicion.edicion);
                    gestorPartido = new GestorPartido();

                    // Carga Otros Partidos de la Fecha
                    otrosPartidosDeLaFecha(); 
                    cargarNoticias();
                    cargarEstadisticas();
                    cargarProximoPartido();                    
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void otrosPartidosDeLaFecha()
        {
            GestorControles.cargarRepeaterList(rptFechaActual, (new GestorPartido()).otrosPartidosDeLaFecha(gestorEdicion.edicion.idEdicion, gestorEdicion.faseActual.idFase, gestorEdicion.faseActual.fechaActual.idFecha, 0));
        }

         private void cargarNoticias()
         {
            GestorControles.cargarRepeaterList(rptEventos, (new GestorNoticia()).obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaEVENTOS));
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

        public string nombreMes(int numeroMes)
        {
            return GestorExtra.nombreMes(numeroMes);
        }

        public string nombreDia(DateTime date)
        {
            return GestorExtra.nombreDia(date);
        }
        
        public void cargarEstadisticas()
        {
            //Carga Repeater ultimos goles y ultimas tarjetas
            sinUltimosGoles.Visible = !GestorControles.cargarRepeaterTable(rptUltimosGoles, gestorEstadisticas.ultimosGolesDeEdicion());
            sinUltimasTarjetas.Visible = !GestorControles.cargarRepeaterTable(rptUltimasTarjetas, gestorEstadisticas.ultimosTarjetasDeEdicion());
            //Carga estadisticas principales
            DataTable datosEstadisticasEdicion = gestorEstadisticas.estadisticasDeEdicion();
            ltPJ.Text = (datosEstadisticasEdicion.Rows.Count > 0) ? datosEstadisticasEdicion.Rows[0]["PJ"].ToString() : "0";
            ltGolesConvertidos.Text = (datosEstadisticasEdicion.Rows.Count > 0) ? datosEstadisticasEdicion.Rows[0]["Goles Convertidos"].ToString() : "0";
            ltTR.Text = (datosEstadisticasEdicion.Rows.Count > 0) ? datosEstadisticasEdicion.Rows[0]["TR"].ToString() : "0";
            ltTA.Text = (datosEstadisticasEdicion.Rows.Count > 0) ? datosEstadisticasEdicion.Rows[0]["TA"].ToString() : "0";
        }

        public void cargarProximoPartido()
        {
            proximoPartido = gestorPartido.proximoPartidoDeEdicion(gestorEdicion.edicion.idEdicion, gestorEdicion.faseActual.idFase , gestorEdicion.faseActual.fechaActual.idFecha);
            if(proximoPartido != null)
            {
                ltEquipoLocal.Text = proximoPartido.local.nombre;
                ltEquipoVisitante.Text = proximoPartido.visitante.nombre;
                tieneFotoLocal.Visible = proximoPartido.local.tieneImagen() ? true : false;
                noTieneFotoLocal.Visible = proximoPartido.local.tieneImagen() ? false : true;
                tieneFotoVisitante.Visible = proximoPartido.visitante.tieneImagen() ? true : false;
                noTieneFotoVisitante.Visible = proximoPartido.visitante.tieneImagen() ? false : true;
                if (proximoPartido.fecha != null)
                {
                    DateTime fecha = DateTime.Parse(((DateTime?)proximoPartido.fecha).ToString());
                    ltDiaDePartido.Text = GestorExtra.nombreDia(fecha);
                    ltFechaPartido.Text = fecha.ToString("dd/MM/yyyy");
                    ltHoraPartido.Text = fecha.Hour.ToString();
                    string fechaDePartido = fecha.ToString("yyyy/MM/dd");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fechaPartido", "cargarCounter(" + new JavaScriptSerializer().Serialize(fechaDePartido) + ");", true);
                }
            }
        }
    }
}