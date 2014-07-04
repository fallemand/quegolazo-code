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
        private string idTorneo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    cargarCombos();
                    cargarRepeaterTorneos();           
                }
                catch (Exception ex)
                {
                    lblMensajeTorneos.Text = ex.Message;
                }                             
            }
        }

        private void cargarRepeaterTorneos()
        {
            gestorTorneo = new DAOTorneo();
            usuarioLogueado = (Usuario)Session["usuario"];
            rptTorneos.DataSource = gestorTorneo.obtenerTorneosDeUnUsuario(usuarioLogueado.idUsuario);
            rptTorneos.DataBind();
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
                gestorEdicion = new DAOEdicion();
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
                    limpiarPaneles();
                    GestorDeArchivos gestor = new GestorDeArchivos();
                    DAOTorneo daoTorneo = new DAOTorneo();
                    Torneo torneoNuevo = obtenerTorneoDelFormulario();
                    torneoNuevo.idTorneo = daoTorneo.registrarTorneo(torneoNuevo, ((Usuario)Session["usuario"]));
                    if (imagenUpload.PostedFile.ContentLength > 0 && gestor.validarImagen(imagenUpload.PostedFile))
                        gestor.guardarImagen(imagenUpload.PostedFile, Server.MapPath("/imagenes/torneos/"), torneoNuevo.idTorneo.ToString() + ".png");
                    limpiarModalTorneo();
                    cargarRepeaterTorneos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalTorneo();", true);
                
	           }
	         catch (Exception ex)
	           {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTorneo();", true);
                   litFracasoTorneo.Text = ex.Message;
                   panFracasoTorneo.Visible = true;
	           }

            }
          protected void limpiarPaneles()
          {
              panFracasoTorneo.Visible = false;
              panFracasoEdicion.Visible = false;
          }
          protected void limpiarModalTorneo()
          {
              txtUrlTorneo.Value = "";
              txtNombreTorneo.Value = "";
              txtDescripcion.Value = "";
          }

          protected void limpiarModalEdicion()
          {
              txtTorneoAsociado.Value = "Nombre del Torneo";
              txtIdTorneo.Value = "";
              txtNombreEdicion.Value = "";
              ddlTamañoCancha.ClearSelection();
              ddlTipoSuperficie.ClearSelection();
              txtPuntosPorGanar.Value = "";
              txtPuntosPorEmpatar.Value = "";
              txtPuntosPorPerder.Value = "";
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
              string idTorneo = txtIdTorneo.Value;
              
              TamanioCancha tc = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(ddlTamañoCancha.SelectedValue));
              TipoSuperficie ts = gestorTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(ddlTipoSuperficie.SelectedValue));
              int ganado = Int32.Parse(txtPuntosPorGanar.Value);
              int empatado = Int32.Parse(txtPuntosPorEmpatar.Value);
              int perdido = Int32.Parse(txtPuntosPorPerder.Value);
              FormaPuntuacion fp = gestorEdicion.obtenerFormaPuntuacionPorGanadoEmpatadoPerdido(ganado, perdido, empatado);
              Estado e = gestorEstado.obtenerUnEstadoPorNombre("REGISTRADA", "EDICION");
              
              Torneo t = gestorTorneo.obtenerTorneoPorIdYUsuario(Int32.Parse(idTorneo), ((Usuario)Session["usuario"]).idUsuario);

              return new Edicion() { nombre = txtNombreEdicion.Value, tamanioCancha = tc , tipoSuperficie = ts, formaPuntuacion = fp, estado = e , torneo = t};
          }

          protected void btnRegistrarEdicion_Click(object sender, EventArgs e)
          {
              try
              {
                  DAOEdicion daoEdicion= new DAOEdicion();
                  Edicion edicionNueva = obtenerEdicionDelFormulario();
                  daoEdicion.registrarEdicion(edicionNueva);
                  limpiarModalEdicion();
                  cargarRepeaterTorneos();
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalEdicion();", true);

              }
              catch (Exception ex)
              {
                  litFracasoEdicion.Text = ex.Message;
                  panFracasoEdicion.Visible = true;
              }

          }

          protected void btnNuevaEdicion_Click(object sender, EventArgs e)
          {
              obtenerEdicionDelFormulario();
          }

          protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
          {
              if (e.CommandName == "agregarEdicion")
              {
                  DAOTorneo gestorTorneo = new DAOTorneo();
                  idTorneo = e.CommandArgument.ToString();
                  Torneo t = gestorTorneo.obtenerTorneoPorId(Int32.Parse(idTorneo));
                  txtTorneoAsociado.Value = t.nombre;
                  txtIdTorneo.Value = t.idTorneo.ToString();
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdicion();", true);
              }
          }
    }
}