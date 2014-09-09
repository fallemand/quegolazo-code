using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin.edicion
{
    public partial class edicion : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GestorEdicion gestorEdicion = Sesion.getGestorEdicion();
                if (gestorEdicion.edicion == null)
                    Response.Redirect(GestorUrl.aTORNEOS);
                litNombreEdicion.Text = gestorEdicion.edicion.nombre;
                string thisURL = Request.Url.Segments[Request.Url.Segments.Count() - 1];
                switch (thisURL)
                {
                    case "configurar.aspx":
                        barraProgreso.Attributes.Add("style", "width: 25%");
                        litProgreso.Text = "25% Completado";
                        break;
                    case "equipos.aspx":
                        barraProgreso.Attributes.Add("style", "width: 50%");
                        litProgreso.Text = "50% Completado";
                        break;
                    case "fases.aspx":
                        barraProgreso.Attributes.Add("style", "width: 75%");
                        litProgreso.Text = "75% Completado";
                        break;
                    case "confirmar.aspx":
                        barraProgreso.Attributes.Add("style", "width: 100%");
                        litProgreso.Text = "100% Completado";
                        break;
                }
            }
        }
    }
}