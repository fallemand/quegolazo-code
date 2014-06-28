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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try{

            //Registro de usuario en bd
            GestorUsuario gestorUsuario = new GestorUsuario();
            string codigo= gestorUsuario.registrarUsuario(txtApellido.Value ,txtNombre.Value,txtEmail.Value,txtClave.Value);
            


            //parámetros para mandar mail
            string ActivationUrl = string.Empty;
            string mail=txtEmail.Value;
            string cuerpo=string.Empty;
            //ActivationUrl = Server.HtmlEncode("http://www.gridovm.com/pedido-activarCuenta.aspx?UserCodigo=" + codigo);
            ActivationUrl="www.google.com";
            cuerpo = "Gracias por registrarte en nuestro sistema de gestión de campeonatos <br />" +
                                  " Por favor, <a href='" + ActivationUrl + "'>haz click aquí</a> para activar tu cuenta y comenzar a disfrutar de nuestro servicio. <br />Que Golazo!";

            GestorMails gestorMail = new GestorMails();
            gestorMail.mandarMail(mail, "Activación de Cuenta", cuerpo);

            panExito.Visible = true;
            litMensaje.Text = "Se resgistró exitosamente su usuario";
            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                litError.Text = ex.Message;

            }

        }
    }
}