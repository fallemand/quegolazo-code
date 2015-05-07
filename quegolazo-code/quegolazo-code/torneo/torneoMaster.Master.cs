using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace quegolazo_code.torneo
{
    public partial class torneoMaster : System.Web.UI.MasterPage
    {
        protected Torneo torneo;
        protected Edicion edicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                
                if (!IsPostBack) {
                    torneo = new GestorTorneo().obtenerTorneoPorNick(Request["nickTorneo"]);
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