using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;

namespace quegolazo_code.usuario
{
    public partial class modificar_usuario : System.Web.UI.Page
    {
        GestorUsuario gestorUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                if (!Page.IsPostBack)
                {
                    gestorUsuario = Sesion.getGestorUsuario();
                    gestorUsuario.usuario = Sesion.getUsuario();
                    if (gestorUsuario.usuario != null)
                    {
                        gestorUsuario.mailUsuario = gestorUsuario.usuario.email;
                        txtApellido.Value = gestorUsuario.usuario.apellido;
                        txtNombre.Value = gestorUsuario.usuario.nombre;
                        txtEmailModif.Value = gestorUsuario.usuario.email;
                    }
                }
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Modificar los datos
        /// </summary>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ocultarPaneles();
                //Registro de usuario en bd
                GestorUsuario gestorUsuario = new GestorUsuario();
                string codigo = gestorUsuario.modificarUsuario(txtApellido.Value, txtNombre.Value, txtEmailModif.Value, txtClaveValidadora.Value,nuevaClave.Value);
                if (codigo != null)// si no el nulo, es porque cambió el mail
                {
                    //parámetros para mandar mail
                    string ActivationUrl = string.Empty;
                    string mail = txtEmailModif.Value;
                    string cuerpo = string.Empty;
                    ActivationUrl = Server.HtmlEncode("http://localhost:12434/usuario/activar.aspx?UserCode=" + codigo);

                    GestorMails gestorMail = new GestorMails();
                    gestorMail.mandarMailActivacion(mail, "Activación de Cuenta", ActivationUrl);
                    panExito.Visible = true;
                    litMensaje.Text = "<strong>Se modificaron exitosamente sus datos.</strong><br />Revise su casilla de correo para activar su cuenta";
                }
                else//no modificó el mail
                {
                    panExito.Visible = true;
                    litMensaje.Text = "<strong>Se modificaron exitosamente sus datos.</strong>";
                    obtenerNuevosDatos();
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Volver al index
        /// </summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.aINDEX);
        }

        /// <summary>
        /// muestra el panel para cambiar contraseña 
        /// </summary>
        protected void Modificar_contraseña_Click(object sender, EventArgs e)
        {
            div_modifCave.Visible = (div_modifCave.Visible) ? false : true;
        }

        /// <summary>
        /// ocultar Paneles de mensajes de exito o fracaso
        /// </summary>
        private void ocultarPaneles()
        {
            panExito.Visible = false;
            panFracaso.Visible = false;
        }

        /// <summary>
        /// Limpiar campos
        /// </summary>
        private void limpiarCampos()
        {
            GestorControles.cleanControls(new List<Object> { txtNombre ,txtApellido,txtEmailModif,txtClaveValidadora});
        }

        /// <summary>
        /// Obtener el usuario de sesión
        /// </summary>
        public void obtenerNuevosDatos()
        {
            gestorUsuario = Sesion.getGestorUsuario();
            gestorUsuario.usuario = Sesion.getUsuario();
            gestorUsuario.usuario = gestorUsuario.obtenerUsuarioPorId(gestorUsuario.usuario.idUsuario); 
        }

        /// <summary>
        /// Limpiar los paneles de exito y fracaso
        /// </summary>
        public void limpiarPaneles()
        {
            panFracaso.Visible = false;
            litError.Text = "";
            panExito.Visible = false;
            litMensaje.Text = "";
        }
        /// <summary>
        /// Panel Fracaso de lista Noticia
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litError.Text = mensaje;
            panFracaso.Visible = true;
        }
    }
}