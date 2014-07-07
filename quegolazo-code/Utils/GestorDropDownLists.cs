using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.UI.WebControls;



namespace Utils
{

    /// <summary>
    /// Clase utilitaria para gestionar todo lo que tenga que ver con controles DropDownList
    /// </summary>
    public static class GestorDropDownLists
    {
        public static void cargarDropDownListDesdeTabla(DataTable origenDeDatos, DropDownList ddlParaCargar, string valueField, string textField ) {
            ddlParaCargar.DataSource = origenDeDatos;
            ddlParaCargar.DataValueField = valueField;
            ddlParaCargar.DataTextField = textField;
            ddlParaCargar.DataBind();
        }

     
        
    }
}
