﻿using System;
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
        /// <summary>
        /// Enviar mail de confirmación
        /// </summary>
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
                LitExito.Text = "<strong>Revise su casilla de correo</strong> Se ha enviado un mail con los detalles para restablecer su contraseña.";
            }
            catch (Exception ex)
            {
                panFracaso.Visible = true;
                LitError.Text = ex.Message;
            }
        }

        ///
        /// ---Métodos Extras---
        ///
        /// <summary>
        /// Ocultar los paneles de exito y fracaso
        /// </summary>
        private void ocultarPaneles()
        {
            panExito.Visible = false;
            panFracaso.Visible = false;
        }
    }
}