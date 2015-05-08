using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class ediciones : System.Web.UI.Page
    {
        protected Torneo torneo;
        private GestorEdicion gestorEdicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                torneo = GestorUrl.validarTorneo();
                gestorEdicion = new GestorEdicion();
                GestorControles.cargarRepeaterList(rptEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(torneo.idTorneo));
            }
        }
    }
}