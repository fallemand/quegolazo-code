using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;

namespace quegolazo_code.admin
{
    public partial class mis_torneos : System.Web.UI.Page
    {
        private DAOTorneo gestorTorneo;
        private DAOEdicion gestorEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                try
                {
                    Usuario usuarioLogueado = null;

                    //if ((Usuario)Session["usuario"] != null)
                    // usuarioLogueado = (Usuario)Session["usuario"];


                    gestorTorneo = new DAOTorneo();
                    gestorEdicion = new DAOEdicion();

                    //rptTorneos.DataSource = gestorTorneo.obtenerTorneosDeUnUsuario(usuarioLogueado.idUsuario);
                    rptTorneos.DataSource = gestorTorneo.obtenerTorneosDeUnUsuario(1003);
                    rptTorneos.DataBind();
                }
                catch (Exception ex)
                {
                        lblMensajeTorneos.Text = ex.Message;
                }
                
                                                
            }

        }


        /// <summary>
        /// Carga las ediciones de un torneo en particular
        /// </summary>
          protected void rptTorneosItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater rptEdiciones = (Repeater)e.Item.FindControl("rptEdiciones");
                    rptEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorIdTorneo(((Torneo)e.Item.DataItem).idTorneo);
                    rptEdiciones.DataBind();

                }
            }
            catch (Exception ex)
            {
                Label lblMensajeEdiciones = (Label)e.Item.FindControl("lblMensajeEdiciones");
                lblMensajeEdiciones.Text = ex.Message;
            }
              
            
        }
    }
}