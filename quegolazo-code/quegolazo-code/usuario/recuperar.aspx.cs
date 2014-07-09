using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;
using Logica;

namespace quegolazo_code
{
    public partial class olvide_contrasenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                ocultarPaneles();

                GestorUsuario gestorUsuario=new GestorUsuario();

                string codigo= gestorUsuario.generarCodigoRecuperacion(email.Value);

                //parámetros para mandar mail
                string RecuperacionUrl = string.Empty;
                string mail = email.Value;
                string cuerpo = string.Empty;
                RecuperacionUrl = Server.HtmlEncode("http://localhost:12434/usuario/recuperar-contrasenia.aspx?Code=" + codigo);

                GestorMails gestorMail = new GestorMails();
                gestorMail.mandarMailRecuperacion(mail, "Recuperación de Contraseña",RecuperacionUrl);
                panExito.Visible = true;
                LitExito.Text = "<strong>Se ha enviado exitosamente el mail con los pasos para reestablecer su clave</strong><br />Revise su casilla de correo.";

            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                LitError.Text = ex.Message;
            }
        }



        private void ocultarPaneles()
        {
           
            panExito.Visible = false;
            panFracaso.Visible = false;
        }

    }
}