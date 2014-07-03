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
                    cargarCombos();
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

        public void cargarCombos()
        {
            DAOCancha gestorCancha = new DAOCancha();
            DAOTipoSuperficie gestorTipoSuperficie = new DAOTipoSuperficie();

            ddlTamañoCancha.DataSource = gestorCancha.obtenerTodos();
            ddlTamañoCancha.DataValueField = "idTamanioCancha";
            ddlTamañoCancha.DataTextField = "nombre";
            ddlTamañoCancha.DataBind();
      


            ddlTipoSuperficie.DataSource = gestorTipoSuperficie.obtenerTodos();
            ddlTipoSuperficie.DataValueField = "idTipoSuperficie";
            ddlTipoSuperficie.DataTextField = "nombre";
            ddlTipoSuperficie.DataBind();
         
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
                    int idTorneo = ((Torneo)e.Item.DataItem).idTorneo;
                    rptEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorIdTorneo(idTorneo);
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
                Response.Redirect("/admin/mis-torneos.aspx");
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


          private Edicion obtenerEdicionDelFormulario()
          {
              DAOCancha gestorCancha = new DAOCancha();
              DAOTipoSuperficie gestorTipoSuperficie = new DAOTipoSuperficie();
              DAOEdicion gestorEdicion = new DAOEdicion();
              DAOTorneo gestorTorneo = new DAOTorneo();
              DAOEstado gestorEstado = new DAOEstado();
              string idTorneo = btnRegistrarEdicion.CommandArgument.ToString();
              
              TamanioCancha tc = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(ddlTamañoCancha.SelectedValue));
              TipoSuperficie ts = gestorTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(ddlTipoSuperficie.SelectedValue));
              int ganado = Int32.Parse(txtPuntosPorGanar.Value);
              int empatado = Int32.Parse(txtPuntosPorEmpatar.Value);
              int perdido = Int32.Parse(txtPuntosPorPerdes.Value);
              FormaPuntuacion fp = gestorEdicion.obtenerFormaPuntuacionPorGanadoEmpatadoPerdido(ganado, perdido, empatado);
              Estado e = gestorEstado.obtenerUnEstadoPorNombre("REGISTRADA", "EDICION");
              
              
             
              //agrega siempre la edicion en ese torneo  
              Torneo t = gestorTorneo.obtenerTorneoPorNombreYUsuario("Los pibes", ((Usuario)Session["usuario"]).idUsuario);

              return new Edicion() { nombre = txtNombreEdicion.Value, tamanioCancha = tc , tipoSuperficie = ts, formaPuntuacion = fp, estado = e , torneo = t};
          }

          protected void btnRegistrarEdicion_Click(object sender, EventArgs e)
          {
              try
              {
                  
                  DAOEdicion daoEdicion= new DAOEdicion();
                  DAOTorneo daoTorneo = new DAOTorneo();
                  Edicion edicionNueva = obtenerEdicionDelFormulario();

                  daoEdicion.registrarEdicion(edicionNueva);
                  
                  panFracaso.Visible = false;
                  Response.Redirect("/admin/mis-torneos.aspx");

              }
              catch (Exception ex)
              {
                  lblError.InnerText = ex.Message;
                  panFracaso.Visible = true;
                  panExito.Visible = false;
              }

          }

          protected void btnNuevaEdicion_Click(object sender, EventArgs e)
          {
              obtenerEdicionDelFormulario();
          }

       
          
    }
}