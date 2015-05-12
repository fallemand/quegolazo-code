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

                    // Carga Otros Partidos de la Fecha
                    otrosPartidosDeLaFecha(); 
                    cargarNoticias();
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
    }
}