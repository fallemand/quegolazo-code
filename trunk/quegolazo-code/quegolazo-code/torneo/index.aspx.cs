using System;
using Entidades;
using Logica;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected Torneo torneo;
        protected Edicion edicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                torneo = new GestorTorneo().obtenerTorneoPorNick(Request["nickTorneo"]);
                edicion = new GestorEdicion().obtenerEdicionPorId(int.Parse(Request["idEdicion"]));
                if (!IsPostBack) { 
                    //aca mandamos como parametro una efcha con id negativo para que traiga todos los partidos de una fecha...
                 //var partidos = new GestorPartido().otrosPartidosDeLaFecha(edicion.idEdicion, 
                 //Utils.GestorControles.cargarRepeaterTable
                
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
    }
}