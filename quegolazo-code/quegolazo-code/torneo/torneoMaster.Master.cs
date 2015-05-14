using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.torneo
{
    public partial class torneoMaster : System.Web.UI.MasterPage
    {
        protected Torneo torneo;
        protected Edicion edicion;
        protected GestorTorneo gestorTorneo;
        JavaScriptSerializer serializador;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                if (!IsPostBack) {
                    torneo = GestorUrl.validarTorneo();
                    edicion = GestorUrl.validarEdicion(torneo.nick);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;

                    litFavicon.Text = "<link rel='shortcut icon' href='" + torneo.obtenerImagenChicha() +"'>";

                    serializador = new JavaScriptSerializer();
                    string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(torneo.idTorneo));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "cargarEstilosVisuales(" + estilos + ");", true);

                    Utils.GestorControles.cargarRepeaterList(rptEdicionesMaster, new GestorEdicion().obtenerEdicionesPorTorneo(torneo.idTorneo));

                    validarAdministradorLogueado();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void validarAdministradorLogueado()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Torneo t = GestorUrl.validarTorneo();
                if (gestorTorneo.verificarTorneoDeUsuario(t.idTorneo))
                {
                    panelConfiguracion.Visible = true;
                    Session["torneoConfigurado"] = t;
                }
            }
        }
    }
}