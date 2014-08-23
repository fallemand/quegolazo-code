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
        GestorUsuario gestorUsuario = new GestorUsuario();
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
                        gestor.activarUsuario(codigo);                     
                        panExito.Visible = true;
                        litMensaje.Text = "Ha sido activada. <strong><a href='"+GestorUrl.uLOGIN+"'>Ingresa Aquí</a></strong>";
                    }
                    catch (Exception ex)
                    {

                        panFracaso.Visible = true;
                        litError.Text = ex.Message;

                    }

                }
                else
                {
                    try
                    {
                        panel_no_activacion.Visible = true;
                        ocultarPaneles();

                        if (Request.QueryString["idUsuario"] != null)
                        {
                            //Busco el usuario
                            int idUsuario = Int32.Parse(Request.QueryString["idUsuario"]);
                            Usuario u = gestorUsuario.obtenerUsuario(idUsuario);
                            email.Value = u.email;

                        }
                    }
                    catch (Exception ex)
                    {
                        panFracaso.Visible = true;
                        litError.Text = ex.Message;
                    }
                
                }

            }
        }


        /// <summary>
        /// metodo para renviar código de activación
        /// autor: Flor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                ocultarPaneles();

               Usuario  usuario= gestorUsuario.obtenerUsuario(email.Value);
                
                    //parámetros para mandar mail
                    string ActivationUrl = string.Empty;
                    string mail = email.Value;
                    string cuerpo = string.Empty;
                    ActivationUrl = Server.HtmlEncode("http://localhost:12434/admin/activar.usuario.aspx?UserCode=" + usuario.codigo);

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