using System;
using Entidades;
using Logica;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Torneo t = new GestorTorneo().obtenerTorneoPorNick(Request["nick"]);
            String nombre = t.nombre;
        }
    }
}