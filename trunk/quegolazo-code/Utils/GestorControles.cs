using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Utils
{
    public class GestorControles
    {
        //Devuelve true si tiene elementos
        public static bool cargarRepeaterList<T>(Repeater repeater, List<T> dataSource)
        {
            repeater.DataSource = dataSource;
            repeater.DataBind();
            if (repeater.Items.Count > 0)
                return true;
            return false;
        }

        public static bool cargarRepeaterTable(Repeater repeater, DataTable dataSource)
        {
            repeater.DataSource = dataSource;
            repeater.DataBind();
            if (repeater.Items.Count > 0)
                return true;
            return false;
        }

        public static void cargarComboList<T>(DropDownList combo, List<T> dataSource, String valueField, String textField) 
        {
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
        }

        public static bool cargarCheckBoxList<T>(CheckBoxList combo, List<T> dataSource, String valueField, String textField)
        {
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
            if (combo.Items.Count > 0)
                return true;
            return false;
        }

        public static bool cargarComboList<T>(DropDownList combo, List<T> dataSource, String valueField, String textField, String defaultItemText, bool puedeSeleccionarse)
        {
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
            ListItem defaultItem = new ListItem(defaultItemText, "", true);
            if (!puedeSeleccionarse)
                defaultItem.Attributes.Add("disabled", "disabled");
            combo.Items.Insert(0, defaultItem);
            if (combo.Items.Count > 1)
                return true;
            return false;                           
        }

        public static void cargarComboTabla(DropDownList combo, DataTable dataSource, String valueField, String textField)
        {
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
        }

        public static bool cargarComboTabla(DropDownList combo, DataTable dataSource, String valueField, String textField, String defaultItemText, bool puedeSeleccionarse)
        {
            combo.DataSource = dataSource;
            combo.DataValueField = valueField;
            combo.DataTextField = textField;
            combo.DataBind();
            ListItem defaultItem = new ListItem(defaultItemText, "", true);
            if (!puedeSeleccionarse)
                defaultItem.Attributes.Add("disabled", "disabled");
            combo.Items.Insert(0, defaultItem);
            if (combo.Items.Count > 1)
                return true;
            return false;    
        }

        public static void enableControls(List<Object> controles)
        {
            foreach (Object control in controles)
            {
                if (control is WebControl)
                    ((WebControl)control).Enabled = true;
                else if (control is HtmlControl)
                    ((HtmlControl)control).Disabled = false;
            }
        }

        public static void disableControls(List<Object> controles)
        {
            foreach (Object control in controles)
            {
                if (control is WebControl)
                    ((WebControl)control).Enabled = false;
                else if(control is HtmlControl)
                    ((HtmlControl)control).Disabled= true;
            }
        }

        public static void showControls(List<Object> controles)
        {
            foreach (System.Web.UI.Control control in controles)
                control.Visible = true;
        }

        public static void hideControls(List<Object> controles)
        {
            foreach (System.Web.UI.Control control in controles)
                control.Visible = false;
        }

        public static void cleanControls<T>(List<T> controles)
        {
            foreach (Object control in controles) {
                switch (control.GetType().Name)
                {
                    case "HtmlInputText" : ((HtmlInputText)control).Value = ""; break;
                    case "TextBox" : ((TextBox)control).Text = ""; break;
                    case "HtmlTextArea": ((HtmlTextArea)control).Value = ""; break;
                    case "DropDownList": ((DropDownList)control).ClearSelection(); break;
                }
            }
        }
    }
}
