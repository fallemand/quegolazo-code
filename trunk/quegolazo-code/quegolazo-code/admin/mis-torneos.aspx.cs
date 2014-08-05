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
        GestorTorneo gestorTorneo = new GestorTorneo();
        GestorEdicion gestorEdicion = new GestorEdicion();

        protected void Page_Load(object sender, EventArgs e)        
        {
            cargarGestorTorneo();
            cargarGestorEdicion();
            if (!Page.IsPostBack)
            {
                try
                {
                    cargarCombos();
                    cargarRepeaterTorneos();           
                }
                catch (Exception ex)
                {
                    lblMensajeTorneos.Visible = true;
                    lblMensajeTorneos.Text = ex.Message;
                }                             
            }
        }

        /// <summary>
        /// Carga las ediciones de un torneo en particular
        /// autor: Paula Pedrosa
        /// </summary>
        protected void rptTorneosItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater rptEdiciones = (Repeater)e.Item.FindControl("rptEdiciones");
                    int idTorneo = ((Torneo)e.Item.DataItem).idTorneo;
                    rptEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorTorneo(idTorneo);
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
        protected void btnResgitrarTorneo_Click(object sender, EventArgs e)
        {             
            try
            {                
                limpiarPaneles();   
                int idTorneo = gestorTorneo.registrarTorneo(txtNombreTorneo.Value, txtDescripcion.Value, txtUrlTorneo.Value.Replace(" ", "-"));
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

        /// <summary>
        /// Registra una nueva Edicion
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnSiguienteEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                gestorEdicion.cargarDatos(txtNombreEdicion.Value, ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue, txtPuntosPorGanar.Value,txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value);
                btnRegistrarOpciones.Visible = true;
                btnSiguienteEdicion.Visible = false;
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
            int idTorneo = int.Parse(e.CommandArgument.ToString());
            Torneo torneo = gestorTorneo.obtenerTorneoPorId(idTorneo);
            if (e.CommandName == "agregarEdicion")
            {
                limpiarModalEdicion();
                txtTorneoAsociado.Value = torneo.nombre;
                Sesion.setTorneo(torneo);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalEdicion');", true);
            }
            if (e.CommandName == "editarTorneo")
            {
                txtUrlTorneo.Disabled = true;
                btnResgitrarTorneo.Visible = false;
                btnModificarTorneo.Visible = true;
                txtUrlTorneo.Value = torneo.nick;
                txtNombreTorneo.Value = torneo.nombre;
                txtDescripcion.Value = torneo.descripcion;
                imagenpreview.Src = torneo.obtenerImagenMediana();
                gestorTorneo.torneo.idTorneo = idTorneo;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
            } 
        }
        
        /// <summary>
        /// modifica los datos de un campeonato (nombre, descripción o imagen)
        /// </summary>
        protected void btnModificarTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                gestorTorneo.modificarTorneo(txtNombreTorneo.Value, txtDescripcion.Value);
                GestorImagen.guardarImagenTorneo(imagenUpload.PostedFile, gestorTorneo.torneo.idTorneo, GestorImagen.TORNEO);
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
          protected void btnRegistrarOpcioens_Click(object sender, EventArgs e)
          {
              try
              {
                  //Preferencias Jugadores
                  gestorEdicion.edicion.preferencias.jugadores = rdJugadoresRegistroSi.Checked;
                  gestorEdicion.edicion.preferencias.golesJugadores = rdJugadoresGolesSi.Checked;
                  gestorEdicion.edicion.preferencias.tarjetasJugadores = rdJugadoresTarjetasSi.Checked;
                  gestorEdicion.edicion.preferencias.cambiosJugadores = rdJugadoresCambiosSi.Checked;
                  //Preferencias Árbitros
                  gestorEdicion.edicion.preferencias.arbitros = rdArbitrosSi.Checked;
                  gestorEdicion.edicion.preferencias.asignaArbitros = rdArbitrosPorPartidoSi.Checked;
                  gestorEdicion.edicion.preferencias.desempenioArbitros = rdArbitroDesempenioSi.Checked;
                  //Preferencias Sanciones
                  gestorEdicion.edicion.preferencias.sanciones = rdSancionesEquiposSi.Checked;
                  gestorEdicion.edicion.preferencias.sancionesJugadores = rdSancionesJugadoresSi.Checked;
                  //Preferencia Canchas
                  gestorEdicion.edicion.preferencias.canchaUnica = rdCanchasComplejos.Checked;
                  //Registro de configuraciones
                  gestorEdicion.registrarEdicion();

                  limpiarModalEdicion();
                  cargarRepeaterTorneos();
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalEdicion');", true);
              }
              catch (Exception ex)
              {
                  litFracasoEdicion.Text = ex.Message;
                  panFracasoEdicion.Visible = true;
              }
          }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el gestor de la clase
        /// autor: Facundo Allemand
        /// </summary>
        private void cargarGestorTorneo()
        {
            if (Session["gestorTorneo"] == null)
                Session["gestorTorneo"] = new GestorTorneo();
            gestorTorneo = (GestorTorneo)Session["gestorTorneo"];
        }
        /// <summary>
        /// Carga el gestor de la clase
        /// autor: Facundo Allemand
        /// </summary>
        private void cargarGestorEdicion()
        {
            if (Session["gestorEdicion"] == null)
                Session["gestorEdicion"] = new GestorEdicion();
            gestorEdicion = (GestorEdicion)Session["gestorEdicion"];
        }
        /// <summary>
        /// Carga el Repeater de los torneos de un usuario con una lista de Torneos
        /// autor: Paula Pedrosa
        /// </summary>
        private void cargarRepeaterTorneos()
        {
            rptTorneos.DataSource = gestorTorneo.obtenerTorneosPorUsuario();
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
        /// Oculta los paneles de Errores y limpia los Literal
        /// </summary>
        protected void limpiarPaneles()
        {
            panFracasoTorneo.Visible = false;
            litFracasoTorneo.Text = "";
            panFracasoEdicion.Visible = false;
            litFracasoEdicion.Text = "";
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
    }
}