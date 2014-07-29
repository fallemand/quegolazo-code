using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

namespace quegolazo_code.admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    //Usuario usuario = (Usuario)Session["usuario"];
                    //Literal litNombre = (Literal)lvNavBar.FindControl("LitNombre");
                    //litNombre.Text = usuario.nombre;
                }
            }
        }

        protected void hlCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx");
        }
    }
}