using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class canchas : System.Web.UI.Page
    {
        GestorCancha gestorCancha = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //si no existe gestor en Session lo carga
            if (Session["gestorCancha"] == null)
                Session["gestorCancha"] = new GestorCancha();
            //obtiene en gestor de la Session.
            gestorCancha = (GestorCancha)Session["gestorCancha"];

            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            Session["torneo"] = new Torneo
            {
                idTorneo = 87,
            };
            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            try
            {
                cargarRepeaterCanchas();
            }
            catch (Exception ex)
            {
                lblMensajeCanchas.Text = ex.Message;
            }
            limpiarPaneles();
        }

        /// <summary>
        /// Permite Registrar una Nueva Cancha para un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistrarCancha_Click(object sender, EventArgs e)
        {
            try
            {
                gestorCancha.registrarCancha(txtNombreCancha.Value, txtDomicilio.Value, txtTelefono.Value);
                GestorImagen.guardarImagen(gestorCancha.cancha.idCancha, GestorImagen.COMPLEJO);
                limpiarCamposCancha();                
                mostrarPanelExito("Cancha registrada con éxito!");
                lblMensajeCanchas.Text = "";
                cargarRepeaterCanchas();
                gestorCancha.cancha = null; // le setea null a la cancha
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
                txtNombreCancha.Focus();
            }
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de canchas de un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        private void cargarRepeaterCanchas()
        {
            rptCanchas.DataSource = gestorCancha.obtenerCanchasDeUnTorneo();
            rptCanchas.DataBind();
        }

        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el panel.</param>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelExito.Visible = false;
            panelFracaso.Visible = true;
        }

        /// <summary>
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el panel.</param>
        private void mostrarPanelExito(string mensaje)
        {
            litExito.Text = mensaje;
            panelExito.Visible = true;
            panelFracaso.Visible = false;
        }

        /// <summary>
        /// Limpia los campos de la cancha
        /// autor: Pau Pedrosa
        /// </summary>
        public void limpiarCamposCancha()
        {
            txtNombreCancha.Value = "";
            txtDomicilio.Value = "";
            txtTelefono.Value = "";
        }

        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// autor: Pau Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
        }
    }
}