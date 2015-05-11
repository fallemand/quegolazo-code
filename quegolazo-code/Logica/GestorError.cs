using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Logica
{
    public class GestorError
    {
        private const string idPanelError = "panelFracaso";
        private const string idPanelExito = "panelExito";
        private const string idMensajeError = "mensajeFracaso";
        private const string idMensajeExito = "mensajeExito";

        public static void mostrarPanelExito(string mensaje)
        {
            mensaje = mensaje.Replace("'", "\"");
            mensaje = mensaje.Replace(System.Environment.NewLine, " - ");
            String funcionJS = "$(document).ready(function ($) { showPanelMessage('" + idPanelExito + "', '" + idMensajeExito + "', '" + mensaje + "');}); ";

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;
                hidePanels();
                if (ScriptManager.GetCurrent(page) != null)
                {
                    ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "showExito", funcionJS, true);
                }
                else
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(Page), "showExito", funcionJS, true);
                }
            }
        }

        public static void mostrarPanelFracaso(string mensaje)
        {
            mensaje = mensaje.Replace("'", "\"");
            mensaje = mensaje.Replace(System.Environment.NewLine, " - ");
            String funcionJS = "$(document).ready(function ($) { showPanelMessage('" + idPanelError + "', '" + idMensajeError + "', '" + mensaje + "');});";

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;
                hidePanels();
                if (ScriptManager.GetCurrent(page) != null)
                {
                    ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "showError", funcionJS, true);
                }
                else
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(Page), "showError", funcionJS, true);
                }
            }
        }

        public static void hidePanels()
        {
            String funcionJS_hideExito = "$(document).ready(function ($) { hidePanelMessage('" + idPanelExito + "');});";
            String funcionJS_hideError = "$(document).ready(function ($) { hidePanelMessage('" + idPanelError + "');});";

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;

                if (ScriptManager.GetCurrent(page) != null)
                {
                    ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "hideExito", funcionJS_hideExito, true);
                    ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "hideError", funcionJS_hideError, true);
                }
                else
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(Page), "hideExito", funcionJS_hideExito, true);
                    page.ClientScript.RegisterClientScriptBlock(typeof(Page), "hideError", funcionJS_hideError, true);
                }
            }
        }
    }
}
