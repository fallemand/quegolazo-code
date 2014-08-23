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
            Sesion.setEquipo(gestorEquipo.obtenerEquipoPorId(2));
            //EQUIPO HARDCODEADO
            //EQUIPO HARDCODEADO
            try
            {
                limpiarPaneles();
                cargarRepeaterJugadores();
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }
        }       

        protected void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                gestorJugador.registrarJugador(txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtTelefono.Value, txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked);
                limpiarCampos();
                mostrarPanelExito("Jugador registrado con éxito!");
                cargarRepeaterJugadores();
                gestorJugador.jugador = null;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        protected void rptJugadores_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editarJugador")
                {   //por CommandArgument recibe el ID del jugador a modificar 
                    gestorJugador.obtenerJugadorPorId(Int32.Parse(e.CommandArgument.ToString()));
                    txtNombreJugador.Value = gestorJugador.jugador.nombre;
                    txtDni.Value = gestorJugador.jugador.dni;
                    txtFechaNacimiento.Value = gestorJugador.jugador.fechaNacimiento.ToString();
                    txtTelefono.Value = gestorJugador.jugador.telefono;
                    txtFacebook.Value = gestorJugador.jugador.facebook;
                    txtEmail.Value = gestorJugador.jugador.email;
                    if (gestorJugador.jugador.sexo.Equals("Masculino"))
                        rdSexoMasculino.Checked = true;
                    else
                        rdSexoFemenino.Checked = true;
                    if (gestorJugador.jugador.tieneFichaMedica)
                        rdTieneFichaMedicaSi.Checked = true;
                    else
                        rdTieneFichaMedicaNo.Checked = true;
                    btnRegistrarJugador.Visible = false;
                    btnModificarJugador.Visible = true;
                    btnCancelarModificacionJugador.Visible = true;
                }
                if (e.CommandName == "eliminarJugador")
                {
                    gestorJugador.eliminarJugador(Int32.Parse(e.CommandArgument.ToString()));
                    cargarRepeaterJugadores();
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }     
        }

        public void limpiarCampos()
        {
            txtNombreJugador.Value = "";
            txtDni.Value = "";
            txtFechaNacimiento.Value = "";
            txtTelefono.Value = "";
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
            panelFracasoListaJugadores.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
            litFracasoListaJugadores.Text = "";
        }

        private void mostrarPanelFracasoListaJugadores(string mensaje)
        {
            litFracasoListaJugadores.Text = mensaje;
            panelFracasoListaJugadores.Visible = true;
        }

        private void cargarRepeaterJugadores()
        {
            rptJugadores.DataSource = gestorJugador.obtenerJugadoresDeUnEquipo();
            rptJugadores.DataBind();
            sinJugadores.Visible = (rptJugadores.Items.Count > 0) ? false : true;
        }

        protected void btnCancelarModificacionJugador_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            btnRegistrarJugador.Visible = true;
            btnModificarJugador.Visible = false;
            btnCancelarModificacionJugador.Visible = false;
            gestorJugador.jugador = null; // le setea null al jugador
        }

        protected void btnModificarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                int idJugadorAModificar = gestorJugador.jugador.idJugador;
                gestorJugador.modificarJugador(idJugadorAModificar, txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtTelefono.Value,txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked);          
                limpiarCampos();
                mostrarPanelExito("Jugador modificado con éxito!");
                cargarRepeaterJugadores();
                gestorJugador.jugador = null; //le setea null al jugador
                //lo manda a la solapa de agregar un jugador
                btnRegistrarJugador.Visible = true;
                btnModificarJugador.Visible = false;
                btnCancelarModificacionJugador.Visible = false;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }       
    }
}