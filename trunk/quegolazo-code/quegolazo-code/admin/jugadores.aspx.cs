﻿using System;
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
     
        /// <summary>
        /// Registra un Nuevo Jugador en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                gestorJugador.registrarJugador(txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtNumeroCamiseta.Value, txtTelefono.Value, txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked);
                GestorImagen.guardarImagen(gestorJugador.jugador.idJugador, GestorImagen.JUGADOR);
                limpiarCampos();
                cargarRepeaterJugadores();
                gestorJugador.jugador = null;
            }
            catch (Exception ex)
            {

                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.JUGADOR, GestorImagen.MEDIANA);
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Para Modifica y/o Eliminar un Jugador
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptJugadores_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                gestorJugador.obtenerJugadorPorId(Int32.Parse(e.CommandArgument.ToString()));
                if (e.CommandName == "editarJugador")
                {                        
                    txtNombreJugador.Value = gestorJugador.jugador.nombre;
                    txtDni.Value = gestorJugador.jugador.dni;
                    txtFechaNacimiento.Value = (gestorJugador.jugador.fechaNacimiento == null)? "" : DateTime.Parse(gestorJugador.jugador.fechaNacimiento.ToString()).ToShortDateString();
                    txtNumeroCamiseta.Value = (gestorJugador.jugador.numeroCamiseta == null) ? "" : Int32.Parse(gestorJugador.jugador.numeroCamiseta.ToString()).ToString();
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
                    imagenpreview.Src = gestorJugador.jugador.obtenerImagenMediana();   
                }
                if (e.CommandName == "eliminarJugador")
                {
                    litNombreJugador.Text = gestorJugador.jugador.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarJugador');", true);
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }     
        }           

        /// <summary>
        /// Cancela la modificación de un jugador
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionJugador_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            btnRegistrarJugador.Visible = true;
            btnModificarJugador.Visible = false;
            btnCancelarModificacionJugador.Visible = false;
            gestorJugador.jugador = null; // le setea null al jugador
        }

        /// <summary>
        /// Permite modificar en la BD un jugador
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                int idJugadorAModificar = gestorJugador.jugador.idJugador;
                gestorJugador.modificarJugador(idJugadorAModificar, txtNombreJugador.Value, txtDni.Value, txtFechaNacimiento.Value, txtNumeroCamiseta.Value, txtTelefono.Value,txtEmail.Value, txtFacebook.Value, rdSexoMasculino.Checked, rdTieneFichaMedicaSi.Checked);
                GestorImagen.guardarImagen(idJugadorAModificar, GestorImagen.JUGADOR);
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

        /// <summary>
        /// Permite Seleccionar Equipo
        /// autor: Facu Allemand
        /// </summary>
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorJugador.eliminarJugador(gestorJugador.jugador.idJugador);
                cargarRepeaterJugadores();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarJugador", "closeModal('eliminarJugador');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaJugadores(ex.Message);
            }
        }  

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Habilita Campos
        /// </summary>
        private void habilitarCampos()
        {
            txtNombreJugador.Disabled = false;
            txtDni.Disabled = false;
            txtFechaNacimiento.Disabled = false;
            txtNumeroCamiseta.Disabled = false;
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
        /// <summary>
        /// Carga Combo Equipos
        /// </summary>
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
        /// <summary>
        /// Obtiene Equipo Seleccionado
        /// autor: Facu Allemand
        /// </summary>
        private void obtenerEquipoSeleccionado()
        {
            if (Request.QueryString["idEquipo"] != null)
                gestorEquipo.getEquipo().idEquipo = Validador.castInt(Request.QueryString["idEquipo"]);
            else
                gestorEquipo.getEquipo().idEquipo = 0;
        }
        /// <summary>
        /// Limpiar Campos
        /// </summary>
        public void limpiarCampos()
        {
            txtNombreJugador.Value = "";
            txtDni.Value = "";
            txtFechaNacimiento.Value = "";
            txtNumeroCamiseta.Value = "";
            txtTelefono.Value = "";
            txtEmail.Value = "";
            txtFacebook.Value = "";
            rdSexoFemenino.Checked = false;
            rdSexoMasculino.Checked = false;
            rdTieneFichaMedicaSi.Checked = false;
            rdTieneFichaMedicaNo.Checked = false;
        }
        /// <summary>
        /// Panel Fracaso Lista Jugadores
        /// </summary>
        private void mostrarPanelFracasoListaJugadores(string mensaje)
        {
            litFracasoListaJugadores.Text = mensaje;
            panelFracasoListaJugadores.Visible = true;
        }
        /// <summary>
        /// Carga Repeater Jugadores
        /// </summary>
        private void cargarRepeaterJugadores()
        {
            rptJugadores.DataSource = gestorJugador.obtenerJugadoresDeUnEquipo();
            rptJugadores.DataBind();
            sinJugadores.Visible = (rptJugadores.Items.Count > 0) ? false : true;
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
        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }

       
    }
}