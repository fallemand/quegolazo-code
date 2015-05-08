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
        private GestorTorneo gestorTorneo;
        private GestorEdicion gestorEdicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gestorTorneo = new GestorTorneo();
                string nickTorneo = Request["nickTorneo"];
                if (nickTorneo == null)
                    Response.Redirect(GestorUrl.t404);
                torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
                if (torneo == null)
                    Response.Redirect(GestorUrl.t404);
                gestorEdicion = new GestorEdicion();
                GestorControles.cargarRepeaterList(rptEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(torneo.idTorneo));
            }
        }
    }
}