using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.torneo
{
    public partial class fechas : System.Web.UI.Page
    {
       protected  GestorPartido gestorPartido;
       public static GestorEdicion gestorEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPartido = Sesion.getGestorPartido();
        }
    }
}