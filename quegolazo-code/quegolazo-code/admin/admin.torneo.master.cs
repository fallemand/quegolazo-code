using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;

namespace quegolazo_code.admin
{
    public partial class admin_torneo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Torneo"]==null)
            {
                GestorTorneo gestorTorneo = new GestorTorneo();
                Torneo torneo = gestorTorneo.obtenerUltimoTorneoDelUsuario();
                if (torneo != null)
                    Sesion.setTorneo(gestorTorneo.obtenerUltimoTorneoDelUsuario());
                else
                    Response.Redirect(GestorUrl.aTORNEOS);
            }
        }
    }
}