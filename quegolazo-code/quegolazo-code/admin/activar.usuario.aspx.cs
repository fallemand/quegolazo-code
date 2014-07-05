using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;

namespace quegolazo_code.admin
{
    public partial class activar_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string codigo = Request.QueryString["UserCode"];
            if (!Page.IsPostBack)
            {
                if (codigo != null)
                {
                    try
                    {
                        panel_activacion.Visible = true;

                        GestorUsuario gestor = new GestorUsuario();
                        int idUsuario = gestor.activarUsuario(codigo);
                        if (idUsuario == 0)
                        {
                            throw new Exception("Su cuenta ya se encuentra activada.");
                        }

                        LitEmail.Text = gestor.obtenerUsuario(idUsuario).email;
                        panExito.Visible = true;
                        litMensaje.Text = "Ha sido activada. <strong><a href='login.aspx'>Ingresa Aquí</a></strong>";



                    }
                    catch (Exception ex)
                    {

                        panFracaso.Visible = true;
                        litError.Text = ex.Message;

                    }



                }
                else
                {
                    panel_no_activacion.Visible = true;
                    ocultarPaneles();


   
                }

            }
        }


        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                ocultarPaneles();
                int idUsuario = Int32.Parse(Request.QueryString["idUsuario"]);
                   
                //Busco el usuario
                    GestorUsuario gestorUsuario = new GestorUsuario();
                    Usuario u = gestorUsuario.obtenerUsuario(idUsuario);

                    //parámetros para mandar mail
                    string ActivationUrl = string.Empty;
                    string mail = email.Value;
                    string cuerpo = string.Empty;
                    ActivationUrl = Server.HtmlEncode("http://localhost:12434/admin/activar.usuario.aspx?UserCode=" + u.codigo);

                    GestorMails gestorMail = new GestorMails();
                    gestorMail.mandarMailActivacion(mail, "Activación de Cuenta", ActivationUrl);
                    panExito1.Visible = true;
                    LitExito1.Text = "<strong>Se ha enviado exitosamente el mail de activación</strong><br />Revise su casilla de correo para activar su cuenta";

            }
            catch (Exception ex)
            {
                panFracaso1.Visible = true;
                LitError1.Text = ex.Message;
            }
        }

        private void ocultarPaneles()
        {
            panExito.Visible = false;
            panFracaso.Visible = false;
            panExito1.Visible = false;
            panFracaso1.Visible = false;
        }
    }
}