using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;
using System.Web.Script.Serialization;

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
                litFavicon.Text = "<link rel='shortcut icon' href='" + torneo.obtenerImagenChicha() + "'>";
                JavaScriptSerializer serializador = new JavaScriptSerializer();
                GestorTorneo gestorTorneo = new GestorTorneo();
                string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(torneo.idTorneo));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "cargarEstilosVisuales(" + estilos + ");", true);
            }
        }
    }
}