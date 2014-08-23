using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.usuario
{
    public partial class modificar_usuario : System.Web.UI.Page
    {
        GestorUsuario gestorUsuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorUsuario = Sesion.getGestorUsuario();
            gestorUsuario.usuario = Sesion.getUsuario();
            gestorUsuario.mailUsuario = gestorUsuario.usuario.email;

            if (gestorUsuario.usuario != null)
            {
                txtApellido.Value = gestorUsuario.usuario.apellido;
                txtNombre.Value = gestorUsuario.usuario.nombre;
                txtEmailModif.Value = gestorUsuario.usuario.email;
               
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            
        }

        protected void Modificar_contraseña_Click(object sender, EventArgs e)
        {
            div_modifCave.Visible = true;
        }
    }
}