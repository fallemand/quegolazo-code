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
            try
            {
                gestorTorneo = Sesion.getGestorTorneo();
                gestorEdicion = Sesion.getGestorEdicion();
                if (!Page.IsPostBack)
                {
                        cargarRepeaterTorneos();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Limpia y muestra el modal para registrar un torneo.
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnRegistrarNuevoTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModalTorneo();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalTorneo');", true);
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
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
                    Panel panelSinEdiciones = e.Item.FindControl("panelSinEdiciones") as Panel;
                    panelSinEdiciones.Visible = !(GestorControles.cargarRepeaterList(rptEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(idTorneo)));
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
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
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.TORNEO, GestorImagen.MEDIANA);
                GestorError.mostrarPanelFracaso(ex.Message);
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
                    Sesion.setEdicion(null);
                    Sesion.setGestorEdicion(null);
                    Response.Redirect(GestorUrl.aINDEX);
                }
                if (e.CommandName == "eliminarTorneo")
                {
                    litNombreTorneo.Text = gestorTorneo.torneo.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarTorneo');", true);
                    limpiarPaneles();
                }                
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
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
                GestorError.mostrarPanelFracaso(ex.Message);
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
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el Repeater de los torneos de un usuario con una lista de Torneos
        /// </summary>
        private void cargarRepeaterTorneos()
        {
            panelSinTorneos.Visible = !(GestorControles.cargarRepeaterList(rptTorneos, gestorTorneo.obtenerTorneosPorUsuario(Sesion.getUsuario().idUsuario)));
        }

        /// <summary>
        /// Oculta los paneles de Errores y limpia los Literal
        /// </summary>
        protected void limpiarPaneles()
        {
            txtNombreTorneo.Disabled = false;
            //GestorControles.cleanControls(new List<Object> { panFracaso, panFracasoTorneo });
            //GestorControles.hideControls(new List<Object> { panFracaso, litFracasoTorneo });
        }

        /// <summary>
        /// setea vacias las cadenas del modal torneo.
        /// </summary>
        protected void limpiarModalTorneo()
        {
            lblTituloModalTorneo.Text = "Crear nuevo torneo";
            GestorControles.cleanControls(new List<Object> { txtUrlTorneo, txtNombreTorneo, txtDescripcion });
            txtUrlTorneo.Disabled = false;
            GestorControles.showControls(new List<Object> { btnRegistrarTorneo });
            GestorControles.hideControls(new List<Object> { btnModificarTorneo });
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.TORNEO,GestorImagen.MEDIANA);
        }        
    }
}