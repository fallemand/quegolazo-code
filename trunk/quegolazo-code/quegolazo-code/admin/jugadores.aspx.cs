using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using Utils;

namespace quegolazo_code.admin.edicion
{
    public partial class jugadores : System.Web.UI.Page
    {
        GestorJugador gestorJugador = null;
        GestorEquipo gestorEquipo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorJugador = Sesion.getGestorJugador();
                gestorEquipo = Sesion.getGestorEquipo();
                limpiarPaneles();
                if (!Page.IsPostBack)
                {
                    obtenerEquipoSeleccionado();
                    cargarComboEquipos();
                    cargarRepeaterJugadores();
                    if (gestorEquipo.getEquipo().idEquipo > 0)
                        habilitarCampos();
                    else
                        imagenUpload.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }
        }

        private void habilitarCampos()
        {
            txtNombreJugador.Disabled = false;
            txtDni.Disabled = false;
            txtFechaNacimiento.Disabled = false;
            txtTelefono.Disabled = false;
            txtEmail.Disabled = false;
            txtFacebook.Disabled = false;
            rdSexoFemenino.Disabled = false;
            rdSexoMasculino.Disabled = false;
            rdTieneFichaMedicaSi.Disabled = false;
            rdTieneFichaMedicaNo.Disabled = false;
            btnRegistrarJugador.Enabled = true;
            imagenUpload.Enabled = true;
        }

        private void obtenerEquipoSeleccionado()
        {
            if (Request.QueryString["idEquipo"] != null)
                gestorEquipo.getEquipo().idEquipo = Validador.castInt(Request.QueryString["idEquipo"]);
            else
                gestorEquipo.getEquipo().idEquipo = 0;
        }

        protected void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                gestorJugador.registrarJugador(txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtTelefono.Value, txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked);
                limpiarCampos();
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
                    txtFechaNacimiento.Value = (gestorJugador.jugador.fechaNacimiento == null)? "" : DateTime.Parse(gestorJugador.jugador.fechaNacimiento.ToString()).ToShortDateString();
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
            panelFracaso.Visible = true;
        }
       
        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// autor: Pau Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelFracaso.Visible = false;
            panelFracasoListaJugadores.Visible = false;
            litFracaso.Text = "";
            litFracasoListaJugadores.Text = "";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.JUGADOR, GestorImagen.MEDIANA);
            if (ddlEquipos.Items.Count > 0)
            {
                ddlEquipos.Items.FindByValue("").Attributes.Add("disabled", "disabled");
            }
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

        private void cargarComboEquipos()
        {
            ddlEquipos.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo();
            ddlEquipos.DataTextField = "nombre";
            ddlEquipos.DataValueField = "idEquipo";
            ddlEquipos.DataBind();
            ListItem itemSeleccionarEquipo = new ListItem("Seleccionar Equipo", "", true);
            itemSeleccionarEquipo.Attributes.Add("disabled", "disabled");
            ddlEquipos.Items.Insert(0, itemSeleccionarEquipo);
            if (gestorEquipo.equipo.idEquipo > 0)
                ddlEquipos.SelectedValue = gestorEquipo.equipo.idEquipo.ToString();
            else
                itemSeleccionarEquipo.Selected = true;
        }

        protected void btnSeleccionarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                int idEquipo = Validador.castInt(ddlEquipos.SelectedValue);
                gestorEquipo.getEquipo().idEquipo = idEquipo;
                cargarRepeaterJugadores();
                habilitarCampos();
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }
        }  
    }
}