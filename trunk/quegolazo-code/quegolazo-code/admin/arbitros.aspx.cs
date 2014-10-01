using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using Utils;
using AccesoADatos;

namespace quegolazo_code.admin
{
    public partial class arbitros : System.Web.UI.Page
    {
        GestorArbitro gestorArbitro = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorArbitro = Sesion.getGestorArbitro();
            try
            {
                limpiarPaneles();
                cargarRepeaterArbitros();
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaArbitros(ex.Message);
            }
        }

        /// <summary>
        /// Permite Registrar un árbitro en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarArbitro_Click(object sender, EventArgs e)
        {
            try
            {
                gestorArbitro.registrarArbitro(txtNombreArbitro.Value, txtCelular.Value, txtEmail.Value, txtMatricula.Value);
                GestorImagen.guardarImagen(gestorArbitro.arbitro.idArbitro, GestorImagen.ARBITRO);
                limpiarCamposArbitros();
                cargarRepeaterArbitros();
                gestorArbitro.arbitro = null; // le setea null al arbitro
            }
            catch (Exception ex)
            {
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.ARBITRO, GestorImagen.MEDIANA);
                mostrarPanelFracaso(ex.Message);
                txtNombreArbitro.Focus();
            }
        }

        /// <summary>
        /// Editar y/o eliminar árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptArbitros_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {//por CommandArgument recibe el ID del árbitro a modificar o a eliminar
                gestorArbitro.obtenerArbitroPorId(Int32.Parse(e.CommandArgument.ToString()));
                if (e.CommandName == "editarArbitro")
                {
                    txtNombreArbitro.Value = gestorArbitro.arbitro.nombre;
                    txtCelular.Value = gestorArbitro.arbitro.celular;
                    txtEmail.Value = gestorArbitro.arbitro.email;
                    txtMatricula.Value = gestorArbitro.arbitro.matricula;
                    btnRegistrarArbitro.Visible = false;
                    btnModificarArbitro.Visible = true;
                    btnCancelarModificacionArbitro.Visible = true;
                    imagenpreview.Src = gestorArbitro.arbitro.obtenerImagenMediana();
                }
                if (e.CommandName == "eliminarArbitro")
                {
                    litNombreArbitro.Text = gestorArbitro.arbitro.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarArbitro');", true);
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaArbitros(ex.Message);
            }
        }

        /// <summary>
        /// Permite cancelar la  modificación un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionArbitro_Click(object sender, EventArgs e)
        {
            limpiarCamposArbitros();
            btnRegistrarArbitro.Visible = true;
            btnModificarArbitro.Visible = false;
            btnCancelarModificacionArbitro.Visible = false;
            gestorArbitro.arbitro = null; // le setea null al árbitro
        }

        /// <summary>
        /// Permite modificar de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarArbitro_Click(object sender, EventArgs e)
        {
            try
            {
                int idArbitroAModificar = gestorArbitro.arbitro.idArbitro;
                gestorArbitro.modificarArbitro(idArbitroAModificar, txtNombreArbitro.Value, txtCelular.Value, txtEmail.Value, txtMatricula.Value);
                GestorImagen.guardarImagen(idArbitroAModificar, GestorImagen.ARBITRO);
                limpiarCamposArbitros();
                cargarRepeaterArbitros();
                gestorArbitro.arbitro = null;
                //lo manda a la solapa de agregar un arbitro
                btnRegistrarArbitro.Visible = true;
                btnModificarArbitro.Visible = false;
                btnCancelarModificacionArbitro.Visible = false;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Permite eliminar un árbitro de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorArbitro.eliminarArbitro(gestorArbitro.arbitro.idArbitro);
                cargarRepeaterArbitros();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarArbitro", "closeModal('eliminarArbitro');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaArbitros(ex.Message);
            }
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de árbitros
        /// </summary>
        private void cargarRepeaterArbitros()
        {
            rptArbitros.DataSource = gestorArbitro.obtenerArbitrosDeUnTorneo();
            rptArbitros.DataBind();
            sinArbitros.Visible = (rptArbitros.Items.Count > 0) ? false : true;
        }
        /// <summary>
        /// Limpia los campos
        /// </summary>
        public void limpiarCamposArbitros()
        {
            txtNombreArbitro.Value = "";
            txtCelular.Value = "";
            txtEmail.Value = "";
            txtMatricula.Value = "";
        }
        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }
        /// <summary>
        /// Muestra panel de fracaso
        /// </summary>
        private void mostrarPanelFracasoListaArbitros(string mensaje)
        {
            litFracasoListaArbitros.Text = mensaje;
            panelFracasoListaArbitros.Visible = true;
        }
        /// <summary>
        /// Limpia los paneles
        /// </summary>
        public void limpiarPaneles()
        {           
            panelFracaso.Visible = false;
            panelFracasoListaArbitros.Visible = false;
            litFracaso.Text = "";
            litFracasoListaArbitros.Text = "";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.ARBITRO, GestorImagen.MEDIANA);
        }
    }
}