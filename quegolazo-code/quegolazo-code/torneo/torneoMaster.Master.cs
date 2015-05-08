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
                    gestorTorneo = new GestorTorneo();
                    torneo = new GestorTorneo().obtenerTorneoPorNick(Request["nickTorneo"]);
                    serializador = new JavaScriptSerializer();
                    string estilos = serializador.Serialize(gestorTorneo.obtenerConfiguracionVisual(torneo.idTorneo));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "variable", "var configuracion = " + estilos + ";", true);
                    edicion = new GestorEdicion().obtenerEdicionPorId(int.Parse(Request["idEdicion"]));
                    Utils.GestorControles.cargarRepeaterList(rptEdicionesMaster, new GestorEdicion().obtenerEdicionesPorTorneo(torneo.idTorneo));
                }

                
            }
            catch (ArgumentNullException ex)
            {
                //TODO redireccionar a pagina de error                
                throw;
            }
            catch (FormatException ex)
            {
                //TODO redireccionar a pagina de error                
                throw;
            }
            
        }
    }
}