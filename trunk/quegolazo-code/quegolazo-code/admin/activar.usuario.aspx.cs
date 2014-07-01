using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin
{
    public partial class activar_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string codigo = Request.QueryString["UserCode"];
                    GestorUsuario gestor = new GestorUsuario();
                    int idUsuario= gestor.activarUsuario(codigo);

                    LitEmail.Text = gestor.obtenerUsuario(idUsuario).email;
                    panExito.Visible = true;
                    litMensaje.Text = "Tu cuenta ha sido activada.";


                }
                catch (Exception ex)
                {

                    panFracaso.Visible = true;
                    litError.Text = ex.Message;

                }


            }
        }
    }
}