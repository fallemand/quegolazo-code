using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;

namespace quegolazo_code.admin.edicion
{
    public partial class jugadores : System.Web.UI.Page
    {
        GestorJugador gestorJugador = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorJugador = Sesion.getGestorJugador(); 
            //EQUIPO HARDCODEADO
            //EQUIPO HARDCODEADO
            GestorEquipo gestorEquipo = new GestorEquipo();
            Sesion.setEquipo(gestorEquipo.obtenerEquipoPorId(1));
            //EQUIPO HARDCODEADO
            //EQUIPO HARDCODEADO
        }       

        protected void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                int idEquipo = Sesion.getEquipo().idEquipo;
                gestorJugador.registrarJugador(txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked, idEquipo);
                limpiarCampos();
                mostrarPanelExito("Jugador registrada con éxito!");
                gestorJugador.jugador = null;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        public void limpiarCampos()
        {
            txtNombreJugador.Value = "";
            txtDni.Value = "";
            txtFechaNacimiento.Value = "";
            txtEmail.Value = "";
            txtFacebook.Value = "";
            rdSexoFemenino.Checked = false;
            rdSexoMasculino.Checked = false;
            rdTieneFichaMedicaSi.Checked = false;
            rdTieneFichaMedicaNo.Checked = false;
        }

        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
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
        private void mostrarPanelExito(string mensaje)
        {
            litExito.Text = mensaje;
            panelExito.Visible = true;
            panelFracaso.Visible = false;
        }
        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// autor: Pau Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            panelFracasoListaCanchas.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
            litFracasoListaCanchas.Text = "";
        }
    }
}