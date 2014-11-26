using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin
{
    public partial class recuperar_contrasenia : System.Web.UI.Page
    {
        GestorUsuario gestorUsuario = new GestorUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Code"] == null)
                    Response.Redirect(GestorUrl.uLOGIN);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Metodo para guardar clave nueva
        /// autor=Flor
        /// </summary>
        protected void btnGuardarClave_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = Request.QueryString["Code"];
                if (codigo == null)
                    throw new Exception("El código para recuperar la contraseña no es correcto");
                int idUsuario = gestorUsuario.reestablecerContrasenia(codigo, txtClave.Value);
                panExito.Visible = true;
                LitExito.Text = "Se ha registrado exitosamente su nueva clave de acceso. <strong><a href='"+GestorUrl.uLOGIN +"'>Ingresa Aquí</a></strong>";
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message); }
        }

        ///
        /// ---Métodos Extras---
        ///

        /// <summary>
        /// Mostrar Panel Fracaso 
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            LitError.Text = mensaje;
            panFracaso.Visible = true;
        }
    }
}