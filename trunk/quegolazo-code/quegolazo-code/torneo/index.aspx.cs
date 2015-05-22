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
        protected List<Equipo> podio;

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
                    idEdicion = edicion.idEdicion;
                    gestorPartido = new GestorPartido();
                    gestorEstadisticas = new GestorEstadisticas(gestorEdicion.edicion);
                    if (gestorEdicion.edicion.estado.idEstado != Estado.edicionREGISTRADA)
                    {
                        gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                        gestorEdicion.getFaseActual();
                        gestorEdicion.faseActual.getFechaActual();
                        gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                        idFase = gestorEdicion.faseActual.idFase;
                        idFecha = gestorEdicion.faseActual.fechaActual.idFecha;
                        otrosPartidosDeLaFecha();
                        cargarProximoPartido();
                        cargarEquiposParticipantes();
                        cargarTablaPosiciones();
                        cargarUltimosPartidos();
                        cargarPodio();
                    }
                    habilitarPanelesSegunEstadoEdicion();                    
                    cargarNoticias();
                    cargarEstadisticas();  
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        public void habilitarPanelesSegunEstadoEdicion()
        {
            switch (gestorEdicion.edicion.estado.idEstado)
            { //Edición Registrada
                case Estado.edicionREGISTRADA:
                    divUtimosPartidos.Visible = false;
                    divTablaPosiciones.Visible = false;
                    divEquiposParticipantes.Visible = false;
                    divOtrosPartidosDeFecha.Visible = false;
                    break;                    
                case Estado.edicionCONFIGURADA: // Edición Configurada
                    divProximoPartido.Visible = true;
                    divUtimosPartidos.Visible = false;
                    break;                    
                case Estado.edicionINICIADA: //Edición Iniciada
                    if(proximoPartido != null)
                        divProximoPartido.Visible = true;
                    break;                    
                case Estado.edicionFINALIZADA: // Edición Finalizada
                    pnlPodio.Visible = true;
                    break;
                case Estado.edicionCANCELADA: // Edición Cancelada
                    break;                    
                default:
                    break;
            } 
        }

        private void otrosPartidosDeLaFecha()
        {
            GestorControles.cargarRepeaterList(rptFechaActual, (new GestorPartido()).otrosPartidosDeLaFecha(gestorEdicion.edicion.idEdicion, gestorEdicion.faseActual.idFase, gestorEdicion.faseActual.fechaActual.idFecha, 0));
        }
         private void cargarNoticias()
         {
             GestorNoticia gestorNoticia = new GestorNoticia();
             sinNoticias.Visible = !GestorControles.cargarRepeaterList(rptUltimasNoticias, (gestorNoticia.obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaBOLETIN).Count > 2) ? gestorNoticia.obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaBOLETIN).AsEnumerable().Take(3).ToList() : gestorNoticia.obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaBOLETIN));
             sinEventos.Visible = !GestorControles.cargarRepeaterList(rptEventos, gestorNoticia.obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaEVENTOS));
        }

        [System.Web.Services.WebMethod(enableSession: true)]
        public static string guardarConfiguracion(object configuracion)
        {
            try
            {
                new GestorTorneo().registrarConfiguracionVisual(configuracion, ((Torneo)HttpContext.Current.Session["torneoConfigurado"]).idTorneo);
                return "CAMBIOS GUARDADOS!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void cargarEstadisticas()
        {
            //Carga Repeater ultimos goles y ultimas tarjetas
            sinUltimosGoles.Visible = !GestorControles.cargarRepeaterTable(rptUltimosGoles, (gestorEstadisticas.ultimosGolesDeEdicion().Rows.Count > 3) ? (gestorEstadisticas.ultimosGolesDeEdicion()).AsEnumerable().Take(4).CopyToDataTable() : gestorEstadisticas.ultimosGolesDeEdicion());
            sinUltimasTarjetas.Visible = !GestorControles.cargarRepeaterTable(rptUltimasTarjetas, (gestorEstadisticas.ultimosTarjetasDeEdicion().Rows.Count > 3) ? gestorEstadisticas.ultimosTarjetasDeEdicion().AsEnumerable().Take(4).CopyToDataTable() : gestorEstadisticas.ultimosTarjetasDeEdicion());
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
                if (proximoPartido.fecha != null)
                {
                    DateTime fecha = DateTime.Parse(((DateTime?)proximoPartido.fecha).ToString());
                    ltDiaDePartido.Text = GestorExtra.nombreDia(fecha);
                    ltFechaPartido.Text = fecha.ToString("dd/MM/yyyy");
                    ltHoraPartido.Text = fecha.Hour.ToString();
                    string fechaDePartido = fecha.ToString("yyyy/MM/dd");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fechaPartido", "cargarCounter(" + new JavaScriptSerializer().Serialize(fechaDePartido) + ");", true);
                    pnlConProgramacion.Visible = true;
                }
            }
        }
        public void cargarEquiposParticipantes()
        {
            GestorControles.cargarRepeaterList(rptEquiposParticipantes, gestorEdicion.obtenerEquipos());
            ltCantidadEquipo.Text = rptEquiposParticipantes.Items.Count.ToString();        
        }
        public void cargarTablaPosiciones()
        {            
            GestorControles.cargarRepeaterTable(rptTablaPosiciones, gestorEstadisticas.obtenerTablaPosicionesAcotada(gestorEdicion.faseActual.idFase, 5));
        }
        public void cargarUltimosPartidos()
        {
            GestorControles.cargarRepeaterList(rptUltimosPartidos, gestorPartido.ultimosPartidosDeUnaEdicion(gestorEdicion.edicion.idEdicion));
        }
        public void cargarPodio()
        {
            podio = new List<Equipo>();
            GestorEquipo gestorEquipo = new GestorEquipo();           
            for (int i = 0; i < gestorEstadisticas.obtenerTablaPosicionesFinal().Rows.Count; i++)
            {
                Equipo equipo1 = new Equipo();
                equipo1 = gestorEquipo.obtenerEquipoPorId(int.Parse(gestorEstadisticas.obtenerTablaPosicionesFinal().Rows[i]["idEquipo"].ToString()));
                podio.Add(equipo1);
            }
            if(gestorEdicion.edicion.estado.idEstado != Estado.edicionFINALIZADA)
            {
                for (int i = 0; i < 3; i++)
			    { 
                    Equipo equipo1 = new Equipo();
                    equipo1.idEquipo = 0;
                    podio.Add(equipo1);		 
			    }
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
    }
}