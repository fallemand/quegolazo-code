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
    public partial class noticias : System.Web.UI.Page
    {
        GestorNoticia gestorNoticia;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia = Sesion.getGestorNoticia();
                if (!Page.IsPostBack)
                {
                        cargarComboEdiciones();
                        //limpiarPaneles();
                        cargarRepeaterNoticias(); 
                }
            }
            catch (Exception ex){mostrarPanelFracasoListaNoticias(ex.Message);}
        }

        /// <summary>
        /// Registra en la BD una nueva noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.registrarNoticia(txtTituloNoticia.Value, ddlTipoNoticia.Value, txtDescripcionNoticia.Text, ddlEdicion.SelectedValue, txtFecha.Value);
                limpiarCamposNoticias();
                cargarRepeaterNoticias();
                gestorNoticia.noticia = null; // le setea null a la noticia
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Modificar o Eliminar una noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptNoticias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {//por CommandArgument recibe el ID de la noticia a modificar o a eliminar
                gestorNoticia.obtenerNoticiaPorId(Int32.Parse(e.CommandArgument.ToString()));
                if (e.CommandName == "editarNoticia")
                {
                    ddlEdicion.SelectedValue = gestorNoticia.noticia.idEdicion.ToString();
                    txtTituloNoticia.Value = gestorNoticia.noticia.titulo;
                    txtFecha.Value = DateTime.Parse(gestorNoticia.noticia.fecha.ToString()).ToShortDateString();
                    txtDescripcionNoticia.Text = gestorNoticia.noticia.descripcion;
                    ddlTipoNoticia.Value = gestorNoticia.noticia.tipoNoticia;
                    ddlEdicion.Enabled = false;
                    btnRegistrarNoticia.Visible = false;
                    btnModificarNoticia.Visible = true;
                    btnCancelarModificacionNoticia.Visible = true;
                }
                if (e.CommandName == "eliminarNoticia")
                {
                    litTituloNoticia.Text = gestorNoticia.noticia.titulo;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarNoticia');", true);
                }
            }
            catch (Exception ex){mostrarPanelFracasoListaNoticias(ex.Message); }
        }

        /// <summary>
        /// Modifica en la BD una noticia existente
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.modificarNoticia(gestorNoticia.noticia.idNoticia, txtTituloNoticia.Value, ddlTipoNoticia.Value, txtDescripcionNoticia.Text, txtFecha.Value);
                limpiarCamposNoticias();
                cargarRepeaterNoticias();
                gestorNoticia.noticia = null;
                //lo manda a la solapa de agregar una noticia
                btnRegistrarNoticia.Visible = true;
                btnModificarNoticia.Visible = false;
                btnCancelarModificacionNoticia.Visible = false;
                ddlEdicion.Enabled = true;
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Eliminar una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.eliminarNoticia(gestorNoticia.noticia.idNoticia);
                cargarRepeaterNoticias();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarArbitro", "closeModal('eliminarNoticia');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaNoticias(ex.Message);
            }
        }

        /// <summary>
        /// Cancela la Modificación de la noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionNoticia_Click(object sender, EventArgs e)
        {
            limpiarCamposNoticias();
            btnRegistrarNoticia.Visible = true;
            btnModificarNoticia.Visible = false;
            btnCancelarModificacionNoticia.Visible = false;
            gestorNoticia.noticia = null;// le setea null a la noticia
        }
        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el Repeater de noticias
        /// </summary>
        private void cargarRepeaterNoticias()
        {
            sinNoticias.Visible = (GestorControles.cargarRepeaterTable(rptNoticias, gestorNoticia.obtenerNoticiasDeUnTorneo())) ?
                false : true;
        }
        /// <summary>
        /// Limpia los campos de noticia
        /// </summary>
        public void limpiarCamposNoticias()
        {
            txtTituloNoticia.Value = "";
            txtDescripcionNoticia.Text = "";
            txtFecha.Value = "";
        }
        /// <summary>
        /// Panel Fracaso
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }
        /// <summary>
        /// Panel Fracaso de lista Noticia
        /// </summary>
        private void mostrarPanelFracasoListaNoticias(string mensaje)
        {
            litFracasoListaNoticias.Text = mensaje;
            panelFracasoListaNoticias.Visible = true;
        }
        /// <summary>
        /// Carga Combos de Edicion
        /// </summary>
        public void cargarComboEdiciones()
        {
            GestorEdicion gestorEdicion = new GestorEdicion();
            GestorControles.cargarComboList(ddlEdicion, gestorEdicion.obtenerEdicionesPorTorneo(
                Sesion.getTorneo().idTorneo), "idEdicion", "nombre");
        }
        /// <summary>
        /// Limpia Paneles
        /// </summary>
        private void limpiarPaneles()
        {
            panelFracaso.Visible = false;
            panelFracasoListaNoticias.Visible = false;
            litFracaso.Text = "";
            litFracasoListaNoticias.Text = "";
        }
    }
}