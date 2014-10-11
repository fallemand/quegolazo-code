using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using System.Web.Security;

namespace quegolazo_code.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Bandera que sirve para indicar que nos encontramos en el login
            Session["login"] = true;
        }

        protected void btnLoguearse_Click(object sender, EventArgs e)
        {
            try
            {
                ocultarPaneles();
                Session.Clear();
                GestorUsuario gestorUsuario = new GestorUsuario();
                gestorUsuario.usuario = gestorUsuario.validarUsuario(txtEmail.Value, txtContrasenia.Value);
                Sesion.setUsuario(gestorUsuario.usuario);
                FormsAuthentication.RedirectFromLoginPage(txtEmail.Value, noCerrarSesion.Checked);
                Session["login"] = null;
                GestorTorneo gestorTorneo = new GestorTorneo();
                gestorTorneo.torneo = gestorTorneo.obtenerUltimoTorneoDelUsuario();
                if (gestorTorneo.torneo != null)
                    Sesion.setTorneo(gestorTorneo.torneo);
                else
                    Response.Redirect(GestorUrl.aTORNEOS);
            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                litError.Text = ex.Message;
            }
        }

        private void ocultarPaneles()
        {
            panExito.Visible = false;
            panFracaso.Visible = false;
        }
    }
}