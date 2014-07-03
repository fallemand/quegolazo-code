using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;
using Utils;

namespace quegolazo_code.admin
{
    public partial class mis_torneos : System.Web.UI.Page
    {
        private DAOTorneo gestorTorneo;
        private DAOEdicion gestorEdicion;
        Usuario usuarioLogueado = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                try
                {
                    
                    panFracaso.Visible = false;
                    panExito.Visible = false;
                    if ((Usuario)Session["usuario"] != null)
                    {
                        usuarioLogueado = (Usuario)Session["usuario"];
                        gestorTorneo = new DAOTorneo();
                        gestorEdicion = new DAOEdicion();

                        rptTorneos.DataSource = gestorTorneo.obtenerTorneosDeUnUsuario(usuarioLogueado.idUsuario);
                        rptTorneos.DataBind();
                    }
                    else
                    {
                        Response.Redirect("/admin/login.aspx");
                    }
                                        
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

          protected void btnResgitrar_Click(object sender, EventArgs e)
          {             
         try 
	       {	
            GestorDeArchivos gestor = new GestorDeArchivos();
            
                DAOTorneo daoTorneo = new DAOTorneo();
                Torneo torneoNuevo = obtenerTorneoDelFormulario();
                torneoNuevo.idTorneo = daoTorneo.registrarTorneo(torneoNuevo, ((Usuario)Session["usuario"]));
                if (imagenUpload.PostedFile.ContentLength > 0 && gestor.validarImagen(imagenUpload.PostedFile))
                {  
                gestor.guardarImagen(imagenUpload.PostedFile, Server.MapPath("/imagenes/torneos/"), torneoNuevo.idTorneo.ToString() +".png" );
                }
                panFracaso.Visible = false;
                panExito.Visible = true;
                txtUrlTorneo.Value = "";
                txtNombreTorneo.Value = "";
                txtDescripcion.Value = "";
                rptTorneos.DataSource = daoTorneo.obtenerTorneosDeUnUsuario(((Usuario)Session["usuario"]).idUsuario);
                rptTorneos.DataBind();
	       }
	     catch (Exception ex)
	       {
               lblError.InnerText = ex.Message;
               panFracaso.Visible = true;
               panExito.Visible = false;
	       }

            }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto torneo.
        /// </summary>
        /// <returns></returns>
          private Torneo obtenerTorneoDelFormulario()
          {
              return new Torneo() {nombre= txtNombreTorneo.Value, descripcion = txtDescripcion.Value, nick= txtUrlTorneo.Value.Replace(" ","-") };
          }
          
    }
}