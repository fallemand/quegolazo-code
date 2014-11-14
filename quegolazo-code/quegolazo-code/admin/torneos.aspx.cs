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
        GestorTorneo gestorTorneo;
        GestorEdicion gestorEdicion;
        protected void Page_Load(object sender, EventArgs e)        
        {

            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion(); 
            if (!Page.IsPostBack)
            {
                try
                {
                    cargarCombos();
                    cargarRepeaterTorneos();           
                }
                catch (Exception ex)
                {
                    mostrarPanelFracaso(ex.Message);
                }                             
            }
        }

        /// <summary>
        /// Limpia y muestra el modal para registrar un torneo.
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnRegistrarNuevoTorneo_Click(object sender, EventArgs e)
        {
            limpiarModalTorneo();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
        }

        /// <summary>
        /// Carga las ediciones de un torneo en particular
        /// autor: Paula Pedrosa
        /// </summary>
        protected void rptTorneosItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptEdiciones = (Repeater)e.Item.FindControl("rptEdiciones");
                int idTorneo = ((Torneo)e.Item.DataItem).idTorneo;
                rptEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorTorneo(idTorneo);
                rptEdiciones.DataBind();
                Panel panelSinEdiciones = e.Item.FindControl("panelSinEdiciones") as Panel;
                panelSinEdiciones.Visible = (rptEdiciones.Items.Count > 0) ? false : true;
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
                GestorImagen.guardarImagen(idTorneo, GestorImagen.TORNEO);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalTorneo');", true);
                cargarRepeaterTorneos();            
                limpiarModalTorneo();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "removeClass", "removeClass('modalTorneo','fade');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.TORNEO, GestorImagen.MEDIANA);
                mostrarPanelFracasoTorneo(ex.Message);
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
                gestorEdicion.cargarDatos(txtNombreEdicion.Value, ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue, txtPuntosPorGanar.Value,txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value, ddlGenero.SelectedValue);
                gestorEdicion.registrarEdicion();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalEdicion');", true);
                cargarRepeaterTorneos(); 
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoEdicion(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el torneo donde se va agregar la edición
        /// autor: Paula Pedrosa
        /// </summary>
        protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                gestorTorneo.torneo = gestorTorneo.obtenerTorneoPorId(int.Parse(e.CommandArgument.ToString()));
                if (e.CommandName == "agregarEdicion")
                {
                    limpiarModalEdicion();
                    txtTorneoAsociado.Value = gestorTorneo.torneo.nombre;
                    Sesion.setTorneo(gestorTorneo.torneo);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalEdicion');", true);
                }
                if (e.CommandName == "editarTorneo")
                {
                    lblTituloModalTorneo.Text = "Modificar Torneo";
                    txtUrlTorneo.Disabled = true;
                    btnRegistrarTorneo.Visible = false;
                    btnModificarTorneo.Visible = true;
                    txtUrlTorneo.Value = gestorTorneo.torneo.nick;
                    txtNombreTorneo.Value = gestorTorneo.torneo.nombre;
                    txtDescripcion.Value = gestorTorneo.torneo.descripcion;
                    imagenpreview.Src = gestorTorneo.torneo.obtenerImagenMediana();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
                }
                if (e.CommandName == "administrarTorneo")
                {
                    Sesion.setTorneo(gestorTorneo.torneo);
                    Response.Redirect(GestorUrl.aINDEX);
                }
                if (e.CommandName == "eliminarTorneo")
                {
                    litNombreTorneo.Text = gestorTorneo.torneo.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarTorneo');", true);
                    limpiarPaneles();
                }                
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
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
                GestorImagen.guardarImagen(gestorTorneo.torneo.idTorneo, GestorImagen.TORNEO);
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
        /// Permite Editar Edición
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptEdiciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editarEdicion")
                {
                    gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(int.Parse(e.CommandArgument.ToString()));
                    lblTituloModalEdicion.Text = "Modificar Edición";
                    btnSiguienteEdicion.Visible = false;
                    btnModificarEdicion.Visible = true;
                    txtNombreEdicion.Value = gestorEdicion.edicion.nombre;
                    ddlTamañoCancha.SelectedValue = gestorEdicion.edicion.tamanioCancha.idTamanioCancha.ToString();
                    ddlTipoSuperficie.SelectedValue = gestorEdicion.edicion.tipoSuperficie.idTipoSuperficie.ToString();
                    ddlGenero.SelectedValue = gestorEdicion.edicion.generoEdicion.idGeneroEdicion.ToString();
                    txtPuntosPorEmpatar.Value = gestorEdicion.edicion.puntosEmpatado.ToString();
                    txtPuntosPorGanar.Value = gestorEdicion.edicion.puntosGanado.ToString();
                    txtPuntosPorPerder.Value = gestorEdicion.edicion.puntosPerdido.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalEdicion');", true);
                }
                if (e.CommandName == "eliminarEdicion")
                {
                    gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(int.Parse(e.CommandArgument.ToString()));
                    litNombreEdicion.Text = gestorEdicion.edicion.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarEdicion');", true);
                }
                if (e.CommandName == "configurarEdicion")
                {
                    gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(int.Parse(e.CommandArgument.ToString()));
                    if (gestorEdicion.edicion.estado.idEstado == Estado.edicionCONFIGURADA) // Si la edicion esta personalizada
                    {
                        gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                        gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                        gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();                        
                    }
                    Sesion.setGestorEdicion(gestorEdicion);
                    Sesion.setTorneo(gestorTorneo.obtenerTorneoPorId(gestorEdicion.obtenerIdTorneo()));
                    Response.Redirect(GestorUrl.eCONFIGURAR); 
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Permite modificar de la Bd una Edición
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                gestorEdicion.modificarEdicion(gestorEdicion.edicion.idEdicion, txtNombreEdicion.Value, ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue, txtPuntosPorGanar.Value, txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value, ddlGenero.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalEdicion');", true);
                cargarRepeaterTorneos();
                limpiarModalEdicion();
                lblTituloModalEdicion.Text = "Agregar Nueva Edición";
                btnSiguienteEdicion.Visible = true;
                btnModificarEdicion.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalEdicion');", true);
                litFracasoEdicion.Text = ex.Message;
                panFracasoEdicion.Visible = true;
            }
        }

        /// <summary>
        /// Elimina de la Bd una edición
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEdicion.eliminarEdicion(gestorEdicion.edicion.idEdicion);
                cargarRepeaterTorneos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarEdicion", "closeModal('eliminarEdicion');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Elimina de la BD un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminarTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                gestorTorneo.eliminarTorneo(gestorTorneo.torneo.idTorneo);
                cargarRepeaterTorneos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarTorneo", "closeModal('eliminarTorneo');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el Repeater de los torneos de un usuario con una lista de Torneos
        /// autor: Paula Pedrosa
        /// </summary>
        private void cargarRepeaterTorneos()
        {
            rptTorneos.DataSource = gestorTorneo.obtenerTorneosPorUsuario();
            rptTorneos.DataBind();
            panelSinTorneos.Visible = (rptTorneos.Items.Count>0) ? false : true;
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

            ddlGenero.DataSource = gestorEdicion.obtenerGenerosEdicion();
            ddlGenero.DataValueField = "idGeneroEdicion";
            ddlGenero.DataTextField = "nombre";
            ddlGenero.DataBind();
        }

        /// <summary>
        /// Oculta los paneles de Errores y limpia los Literal
        /// </summary>
        protected void limpiarPaneles()
        {
            panFracaso.Visible = false;
            litFracaso.Text = "";
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
            lblTituloModalTorneo.Text = "Crear nuevo torneo";
            txtUrlTorneo.Value = "";
            txtUrlTorneo.Disabled = false;
            txtNombreTorneo.Value = "";
            txtDescripcion.Value = "";
            btnModificarTorneo.Visible = false;
            btnRegistrarTorneo.Visible = true;
            panFracasoTorneo.Visible = false;
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.TORNEO,GestorImagen.MEDIANA);
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
            ddlGenero.ClearSelection();
            txtPuntosPorGanar.Value = "3";
            txtPuntosPorEmpatar.Value = "1";
            txtPuntosPorPerder.Value = "0";
            panFracasoEdicion.Visible = false;           
            btnSiguienteEdicion.Visible = true;
        }

        /// <summary>
        /// Muestra el panel de error de la pag principal
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panFracaso.Visible = true;
        }

        /// <summary>
        /// Muestra el panel de error del modal de Torneo
        /// </summary>
        private void mostrarPanelFracasoTorneo(string mensaje)
        {
            litFracasoTorneo.Text = mensaje;
            panFracasoTorneo.Visible = true;
        }

        /// <summary>
        /// Muestra el panel de error del modal de Edicion
        /// </summary>
        private void mostrarPanelFracasoEdicion(string mensaje)
        {
            litFracasoEdicion.Text = mensaje;
            panFracasoEdicion.Visible = true;
        }       
    }
}