using System;
using Logica;
using Utils;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected int idEdicion;
        protected string nickTorneo;
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["idEdicion"] == null)
                    Response.Redirect(GestorUrl.tEDICIONES);
                idEdicion = int.Parse(Request["idEdicion"]);
                nickTorneo = Request["nickTorneo"];

                if (!IsPostBack) {

                    gestorTorneo = new GestorTorneo();
                    gestorEdicion = new GestorEdicion();
                    gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
                    gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(idEdicion);
                    gestorEdicion.edicion.fases= gestorEdicion.obtenerFases();
                    gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                    gestorEdicion.edicion.equipos= gestorEdicion.obtenerEquipos();
                    gestorEdicion.getFaseActual();
                    gestorEdicion.faseActual.getFechaActual();
                    otrosPartidosDeLaFecha(); // Carga Otros Partidos de la Fecha
                    cargarNoticias();
                }

            }
            catch (ArgumentNullException)
            {
                //TODO redireccionar a pagina de error
                throw;
            }
            catch(Exception){
                throw;
            }
        }

        private void otrosPartidosDeLaFecha()
        {
            GestorControles.cargarRepeaterList(rptFechaActual, (new GestorPartido()).otrosPartidosDeLaFecha(gestorEdicion.edicion.idEdicion, gestorEdicion.faseActual.idFase, gestorEdicion.faseActual.fechaActual.idFecha, 0));
        }

         private void cargarNoticias()
         {
            GestorControles.cargarRepeaterTable(rptEventos, (new GestorNoticia()).obtenerNoticiasXCategoria(gestorEdicion.edicion.idEdicion, CategoriaNoticia.noticiaEVENTOS));
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


    }
}