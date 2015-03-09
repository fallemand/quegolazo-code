using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class canchas : System.Web.UI.Page
    {
        GestorCancha gestorCancha = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorCancha = Sesion.getGestorCancha();       
                limpiarPaneles();
                cargarRepeaterCanchas();
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}
        }       

        /// <summary>
        /// Permite Registrar una Nueva Cancha para un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarCancha_Click(object sender, EventArgs e)
        {
            try
            {
                gestorCancha.registrarCancha(txtNombreCancha.Value, txtDomicilio.Value, txtTelefono.Value);
                GestorImagen.guardarImagen(gestorCancha.cancha.idCancha, GestorImagen.COMPLEJO);
                limpiarCamposCancha();                
                cargarRepeaterCanchas();
                gestorCancha.cancha = null; // le setea null a la cancha
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarCancha');", true);
            }
            catch (Exception ex)
            {
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.COMPLEJO, GestorImagen.MEDIANA);
                mostrarPanelFracaso(ex.Message);
                txtNombreCancha.Focus();
            }
        }

        /// <summary>
        /// Método del Repeater para traer la Cancha a modificar o eliminar
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptCanchas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editarCancha")
                {   //por CommandArgument recibe el ID de la cancha a modificar               
                    gestorCancha.obtenerCanchaAModificar(Int32.Parse(e.CommandArgument.ToString()));
                    txtNombreCancha.Value = gestorCancha.cancha.nombre;
                    txtDomicilio.Value = gestorCancha.cancha.domicilio;
                    txtTelefono.Value = gestorCancha.cancha.telefono;
                    btnRegistrarCancha.Visible = false;
                    btnModificarCancha.Visible = true;
                    btnCancelarModificacionCancha.Visible = true;
                    imagenpreview.Src = gestorCancha.cancha.obtenerImagenMediana();
                }
                if (e.CommandName == "eliminarCancha")
                {
                    gestorCancha.cancha = gestorCancha.obtenerCanchaPorId(int.Parse(e.CommandArgument.ToString()));
                    litNombreCancha.Text = gestorCancha.cancha.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarCancha');", true);
                }
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}            
        }

        /// <summary>
        /// Permite cancelar la modificación de la Cancha
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionCancha_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposCancha();
                btnRegistrarCancha.Visible = true;
                btnModificarCancha.Visible = false;
                btnCancelarModificacionCancha.Visible = false;
                gestorCancha.cancha = null; // le setea null a la cancha
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarCancha');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }  
        }

        /// <summary>
        /// Permite modificar una cancha
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarCancha_Click(object sender, EventArgs e)
        {
            try
            {
                int idCanchaAModificar = gestorCancha.cancha.idCancha;
                gestorCancha.modificarCancha(idCanchaAModificar, txtNombreCancha.Value, txtDomicilio.Value, txtTelefono.Value);
                GestorImagen.guardarImagen(idCanchaAModificar, GestorImagen.COMPLEJO);
                limpiarCamposCancha();
                cargarRepeaterCanchas();
                gestorCancha.cancha = null; //le setea null a la cancha
                //lo manda a la solapa de agregar una cancha
                btnRegistrarCancha.Visible = true;
                btnModificarCancha.Visible = false;
                btnCancelarModificacionCancha.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarCancha');", true);
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Permite eliminar un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorCancha.eliminarCancha(gestorCancha.cancha.idCancha);
                cargarRepeaterCanchas();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarCancha", "closeModal('eliminarCancha');", true);
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de canchas de un torneo
        /// </summary>
        private void cargarRepeaterCanchas()
        {
            sinCanchas.Visible = !GestorControles.cargarRepeaterList(rptCanchas, gestorCancha.obtenerCanchasDeUnTorneo());
        }
        /// <summary>
        /// Muestra Panel Fracaso
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }      
        /// <summary>
        /// Limpia los campos de la cancha
        /// </summary>
        public void limpiarCamposCancha()
        {
            txtNombreCancha.Value = "";
            txtDomicilio.Value = "";
            txtTelefono.Value = "";
        }
        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// </summary>
        private void limpiarPaneles()
        {
            //panelFracaso.Visible = false;
            //panelFracasoListaCanchas.Visible = false;
            //litFracaso.Text = "";
            //litFracasoListaCanchas.Text = "";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.COMPLEJO, GestorImagen.MEDIANA);
        }
    }
}