using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// metodo de registro de usuarios
        /// autor=Flor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try{
                ocultarPaneles();
            //Registro de usuario en bd
            GestorUsuario gestorUsuario = new GestorUsuario();
            string codigo= gestorUsuario.registrarUsuario(txtApellido.Value ,txtNombre.Value,txtEmail.Value,txtClave.Value);

            //parámetros para mandar mail
            string ActivationUrl = string.Empty;
            string mail=txtEmail.Value;
            string cuerpo=string.Empty;
            ActivationUrl = Server.HtmlEncode("http://localhost:12434/admin/activar.usuario.aspx?UserCode=" + codigo);

            GestorMails gestorMail = new GestorMails();
            gestorMail.mandarMailActivacion(mail, "Activación de Cuenta", ActivationUrl);

            btnRegistrar.Enabled =false;
            panExito.Visible = true;
            litMensaje.Text = "<strong>Se registró exitosamente su usuario.</strong><br />Revise su casilla de correo para activar su cuenta";
            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                litError.Text = ex.Message;
            }

        }

        /// <summary>
        /// ocultar Paneles de mensajes de exito o fracaso
        /// </summary>
        private void ocultarPaneles()
        {
            panExito.Visible = false;
            panFracaso.Visible = false;
        }
    }
}