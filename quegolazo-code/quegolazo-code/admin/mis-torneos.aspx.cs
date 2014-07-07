﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;
using Utils;
using Logica;

namespace quegolazo_code.admin
{
    public partial class mis_torneos : System.Web.UI.Page
    {
        
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


        /// <summary>
        /// Carga el Repeater de los torneos de un usuario con una lista de Torneos
        /// autor: Paula Pedrosa
        /// </summary>
        private void cargarRepeaterTorneos()
        {
            usuarioLogueado = (Usuario)Session["usuario"];
            GestorTorneo gestorTorneo = new GestorTorneo();
            
            rptTorneos.DataSource = gestorTorneo.obtenerTorneosDeUnUsuario(usuarioLogueado.idUsuario);
            rptTorneos.DataBind();
        }

        /// <summary>
        /// Carga los combos de los tamaños de cancha y de los tipos de superficie
        /// autor: Paula Pedrosa
        /// </summary>
        public void cargarCombos()
        {
            GestorCancha gestorCancha = new GestorCancha();
            GestorTipoSuperficie gestorTipoSuperficie = new GestorTipoSuperficie();

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
        /// autor: Paula Pedrosa
        /// </summary>
          protected void rptTorneosItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                GestorEdicion gestorEdicion = new GestorEdicion();
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

        /// <summary>
        /// Registra un nuevo torneo en la Base de datos
        /// autor: Antonio Herrera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          protected void btnResgitrar_Click(object sender, EventArgs e)
          {             
            try 
	           {
                    limpiarPaneles();
                    GestorDeArchivos gestor = new GestorDeArchivos();
                    GestorTorneo gestorTorneo = new GestorTorneo();
                    Torneo torneoNuevo = obtenerTorneoDelFormulario();
                    torneoNuevo.idTorneo = gestorTorneo.registrarTorneo(torneoNuevo, ((Usuario)Session["usuario"]));
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

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto edición.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <returns>Objeto Edicion</returns>
          private Edicion obtenerEdicionDelFormulario()
          {
              GestorCancha gestorCancha = new GestorCancha();
              GestorTipoSuperficie gestorTipoSuperficie = new GestorTipoSuperficie();
              GestorEdicion gestorEdicion = new GestorEdicion();
              GestorTorneo gestorTorneo = new GestorTorneo();
              GestorEstado gestorEstado = new GestorEstado();
              string idTorneo = txtIdTorneo.Value;
              
              TamanioCancha tamanioCancha = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(ddlTamañoCancha.SelectedValue));
              TipoSuperficie tipoSuperficie = gestorTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(ddlTipoSuperficie.SelectedValue));

              int ganado = Int32.Parse(txtPuntosPorGanar.Value);
              int empatado = Int32.Parse(txtPuntosPorEmpatar.Value);
              int perdido = Int32.Parse(txtPuntosPorPerder.Value);

              FormaPuntuacion formaPuntuacion = gestorEdicion.obtenerFormaPuntuacionPorGanadoEmpatadoPerdido(ganado, perdido, empatado);
              Estado estado = gestorEstado.obtenerUnEstadoPorNombre("REGISTRADA", "EDICION");
              Torneo torneo = gestorTorneo.obtenerTorneoPorIdYUsuario(Int32.Parse(idTorneo), ((Usuario)Session["usuario"]).idUsuario);

              return new Edicion() { nombre = txtNombreEdicion.Value, tamanioCancha = tamanioCancha , tipoSuperficie = tipoSuperficie, formaPuntuacion = formaPuntuacion, estado = estado , torneo = torneo};
          }

        /// <summary>
        /// Registra una nueva Edicion
         /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          protected void btnRegistrarEdicion_Click(object sender, EventArgs e)
          {
              try
              {
                  GestorEdicion gestorEdicion= new GestorEdicion();
                  Edicion edicionNueva = obtenerEdicionDelFormulario();
                  gestorEdicion.registrarEdicion(edicionNueva);
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

        /// <summary>
        /// Obtiene el torneo donde se va agregar la edición
        /// autor: Paula Pedrosa
        /// </summary>
          protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
          {
              if (e.CommandName == "agregarEdicion")
              {
                  GestorTorneo gestorTorneo = new GestorTorneo();
                  idTorneo = e.CommandArgument.ToString();
                  Torneo t = gestorTorneo.obtenerTorneoPorId(Int32.Parse(idTorneo));
                  txtTorneoAsociado.Value = t.nombre;
                  txtIdTorneo.Value = t.idTorneo.ToString();
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdicion();", true);
              }
          }
    }
}