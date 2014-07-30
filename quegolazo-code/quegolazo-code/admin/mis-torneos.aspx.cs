using System;
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
            Session["usuario"] = new Usuario() { idUsuario = 6, nombre = "Usuario del orto me harte de cargarte" };
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
            List<Torneo> torneosDelUsuario = gestorTorneo.obtenerTorneosDeUnUsuario(usuarioLogueado.idUsuario);
            gestorTorneo.asignarRutaDeImagenATorneos(ref torneosDelUsuario, GestorImagen.enumDimensionImagen.MEDIANA);
            Session["torneos"] = torneosDelUsuario;
            rptTorneos.DataSource = torneosDelUsuario;
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
                GestorTorneo gestorTorneo = new GestorTorneo();
                usuarioLogueado = (Usuario)Session["usuario"];    
                int idTorneo = gestorTorneo.registrarTorneo(txtNombreTorneo.Value, txtDescripcion.Value, txtUrlTorneo.Value.Replace(" ", "-"), usuarioLogueado.idUsuario.ToString());
                //si la imagen esta ok, la guarda en el servidor. 
                if ( imagenUpload.PostedFile != null && imagenUpload.PostedFile.ContentLength > 0)
                    GestorImagen.guardarImagenTorneo(imagenUpload.PostedFile, idTorneo, GestorImagen.TORNEO);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalTorneo');", true);
                cargarRepeaterTorneos();            
                limpiarModalTorneo();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeClass", "removeClass('modalTorneo','fade');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
                litFracasoTorneo.Text = ex.Message;
                panFracasoTorneo.Visible = true;
            }

            }

          protected void limpiarPaneles()
          {
              panFracasoTorneo.Visible = false;
              panFracasoEdicion.Visible = false;
          }
          
        /// <summary>
        /// setea vacias las cadenas del modal torneo.
        /// </summary>
          protected void limpiarModalTorneo()
          {
              txtUrlTorneo.Value = "";
              txtNombreTorneo.Value = "";
              txtDescripcion.Value = "";
          }
        /// <summary>
        /// setea vacias las cadenas de los inputs del modal de la edicion
        /// </summary>
        protected void limpiarModalEdicion()
          {
              txtTorneoAsociado.Value = "Nombre del Torneo";
              txtNombreEdicion.Value = "";
              ddlTamañoCancha.ClearSelection();
              ddlTipoSuperficie.ClearSelection();
              txtPuntosPorGanar.Value = "3";
              txtPuntosPorEmpatar.Value = "1";
              txtPuntosPorPerder.Value = "0";
              panFracasoEdicion.Visible = false;
          }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto torneo.
        /// </summary>
        /// <returns></returns>
          private Torneo obtenerTorneoDelFormulario()
          {
              return new Torneo() { };
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
                      panFracasoEdicion.Visible = false;
                      GestorEdicion gestorEdicion = new GestorEdicion();
                      Session["idEdicionNueva"]= gestorEdicion.registrarEdicion(txtNombreEdicion.Value, Session["idTorneo"].ToString(), ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue,  txtPuntosPorGanar.Value,txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value );
                      btnRegistrarOpciones.Visible = true;
                      btnRegistrarEdicion.Visible = false;
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "activarTab", "activaTab('tabsModalEdicion','tabPersonalizacionEdicion');", true);                               
              }

              catch (Exception ex)
              {
                  litFracasoEdicion.Text = ex.Message;
                  panFracasoEdicion.Visible = true;
              }

          }
              
         
          
        /// <summary>
        /// Obtiene el torneo donde se va agregar la edición
        /// autor: Paula Pedrosa
        /// </summary>
          protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
          {
              if (e.CommandName == "agregarEdicion")
              {
                  Session["idTorneo"] = e.CommandArgument;
                  limpiarModalEdicion();
                  GestorTorneo gestorTorneo = new GestorTorneo();
                  idTorneo = e.CommandArgument.ToString();
                  Torneo t = gestorTorneo.obtenerTorneoPorId(Int32.Parse(idTorneo));
                  txtTorneoAsociado.Value = t.nombre;
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalEdicion');", true);
              }

              ////Flor
              if (e.CommandName == "editarTorneo")
              {
                  Session["idTorneo"] = e.CommandArgument;
                  Torneo t = buscarTorneo(int.Parse(e.CommandArgument.ToString()));
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
                  txtUrlTorneo.Disabled = true;
                  btnResgitrarTorneo.Visible = false;
                  btnModificarTorneo.Visible = true;
                  txtUrlTorneo.Value = t.nick;
                  txtNombreTorneo.Value = t.nombre;
                  txtDescripcion.Value = t.descripcion;
                  if(t.rutaImagen!=null)
                  imagenpreview.Src = t.rutaImagen;
              } 

          }
        
       
        /// <summary>
        /// Busca el torneo en la lista de torneos en sesión
        /// autor=Flor
        /// </summary>
        /// <param name="idtorneo"></param>
        /// <returns></returns>
          private Torneo buscarTorneo(int idtorneo)
          {
              Torneo to = new Torneo();

               foreach(Torneo t in (List<Torneo>)Session["torneos"])
                  {
                      if (t.idTorneo == idtorneo)
                      {
                          to = t;
                          break;
                      }
                  }
               return to;
          }  


        /// <summary>
        /// modifica los datos de un campeonato (nombre, descripción o imagen)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          protected void btnModificarTorneo_Click(object sender, EventArgs e)
          {
              try
              {
                  limpiarPaneles();
                  GestorTorneo gestorTorneo = new GestorTorneo();
                  gestorTorneo.modificarTorneo(txtNombreTorneo.Value, txtDescripcion.Value, Validador.castInt(Session["idTorneo"].ToString()));
                  //si la imagen esta ok, la guarda en el servidor. 
                  if (imagenUpload.PostedFile != null && imagenUpload.PostedFile.ContentLength > 0)
                      GestorImagen.guardarImagenTorneo(imagenUpload.PostedFile, Validador.castInt(Session["idTorneo"].ToString()), GestorImagen.TORNEO);
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalTorneo');", true);
                  cargarRepeaterTorneos();
                  limpiarModalTorneo();
              }
              catch (Exception ex)
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
                  litFracasoTorneo.Text = ex.Message;
                  panFracasoTorneo.Visible = true;
              }
          }


        /// <summary>
        /// Metodo para grabar las preferencias de una edición
        /// autor=Flor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
          protected void btnRegistrarOpcioens_Click(object sender, EventArgs e)
          {
              try
              {
                  //manejador de la lógica
                  GestorEdicion gestorEdicion = new GestorEdicion();

                  //Tomo el valor del id de la edición recientemente generada
                  gestorEdicion.edicion.idEdicion = int.Parse(Session["idEdicionNueva"].ToString());


                  //Preferencias Jugadores
                  if (rdJugadoresRegistroSi.Checked)
                      gestorEdicion.edicion.preferencias.jugadores = true;
                  if (rdJugadoresGolesSi.Checked)
                      gestorEdicion.edicion.preferencias.golesJugadores = true;
                  if (rdJugadoresTarjetasSi.Checked)
                      gestorEdicion.edicion.preferencias.tarjetasJugadores = true;
                  if (rdJugadoresCambiosSi.Checked)
                      gestorEdicion.edicion.preferencias.cambiosJugadores = true;

                  //Preferencias Árbitros
                  if (rdArbitrosSi.Checked)
                      gestorEdicion.edicion.preferencias.arbitros = true;
                  if (rdArbitrosPorPartidoSi.Checked)
                      gestorEdicion.edicion.preferencias.asignaArbitros = true;
                  if (rdArbitroDesempenioSi.Checked)
                      gestorEdicion.edicion.preferencias.desempenioArbitros = true;

                  //Preferencias Sanciones
                  if (rdSancionesEquiposSi.Checked)
                      gestorEdicion.edicion.preferencias.sanciones = true;
                  if (rdSancionesJugadoresSi.Checked)
                      gestorEdicion.edicion.preferencias.sancionesJugadores = true;

                  //Preferencia Canchas
                  if (rdCanchasComplejos.Checked)
                      gestorEdicion.edicion.preferencias.canchaUnica = true;

                  //Registro de configuraciones
                  gestorEdicion.registrarConfiguraciones();

                  limpiarModalEdicion();
                  cargarRepeaterTorneos();
              }
              catch (Exception ex)
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "activarTab", "activaTab('tabsModalEdicion','tabPersonalizacionEdicion');", true);     
                  litFracasoEdicion.Text = ex.Message;
                  panFracasoEdicion.Visible = true;
              }
          }
    }
}