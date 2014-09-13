using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;


namespace quegolazo_code.admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Sesion.getUsuario() != null)
                {
                    try
                    {
                        Literal litNombre = (Literal)lvNavBar.FindControl("LitNombre");
                        litNombre.Text = Sesion.getUsuario().nombre;
                    }
                    catch (Exception)
                    {
                        
                    }
                   
                }
            }
        }

        protected void hlCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect(GestorUrl.uLOGIN);
        }
    }
}