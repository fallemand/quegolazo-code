using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using System.Web.Security;

namespace quegolazo_code.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoguearse_Click(object sender, EventArgs e)
        {
            try
            {
                GestorUsuario gestorUsuario = new GestorUsuario();
                Usuario u = gestorUsuario.validarUsuario(txtEmail.Value, txtContrasenia.Value);
                Session["usuario"] = u;
                FormsAuthentication.RedirectFromLoginPage(txtEmail.Value, noCerrarSesion.Checked);
                panExito.Visible = true;
                litMensaje.Text = "Usuario logueado con éxito";
            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                litError.Text = ex.Message;
            }
        }
    }
}