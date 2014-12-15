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
            String funcionJS = "showPanelMessage('" + idPanelExito + "', '" + idMensajeExito + "', '" + mensaje + "')";

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;

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
            String funcionJS = "showPanelMessage('" + idPanelError + "', '" + idMensajeError + "', '" + mensaje + "')";

            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;

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
    }
}
