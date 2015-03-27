using Entidades;
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
    public partial class configurarSitio : System.Web.UI.Page
    {
        GestorTorneo gestorTorneo;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = Sesion.getGestorTorneo();
            //TODO esto está harcodeado para que funque!
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorId(88);            
            serializador = new JavaScriptSerializer();
            string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(gestorTorneo.torneo.idTorneo));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "var configuracion = " + estilos + ";", true);
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