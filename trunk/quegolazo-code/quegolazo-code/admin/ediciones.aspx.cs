using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;


namespace quegolazo_code.admin
{
    public partial class Ediciones : System.Web.UI.Page
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
                    cargarRepeaterEdiciones();
                }
                catch (Exception ex)
                {
                    mostrarPanelFracaso(ex.Message);
                }
            }
        }

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
                    Response.Redirect(GestorUrl.eCONFIGURAR);
                }
            }
            catch (Exception ex)
            {
               //
            }

        }

        private void cargarRepeaterEdiciones()
        {

            rptEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo);
            rptEdiciones.DataBind();
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
            panFracasoEdicion.Visible = false;
            litFracasoEdicion.Text = "";
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

        protected void btnSiguienteEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                gestorEdicion.cargarDatos(txtNombreEdicion.Value, ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue, txtPuntosPorGanar.Value, txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value, ddlGenero.SelectedValue);
                gestorEdicion.registrarEdicion();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalEdicion');", true);
                cargarRepeaterEdiciones();
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        protected void btnModificarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarPaneles();
                gestorEdicion.modificarEdicion(gestorEdicion.edicion.idEdicion, txtNombreEdicion.Value, ddlTamañoCancha.SelectedValue, ddlTipoSuperficie.SelectedValue, txtPuntosPorGanar.Value, txtPuntosPorEmpatar.Value, txtPuntosPorPerder.Value, ddlGenero.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalEdicion');", true);
                cargarRepeaterEdiciones();
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

        protected void btnEliminarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEdicion.eliminarEdicion(gestorEdicion.edicion.idEdicion);
                cargarRepeaterEdiciones();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarEdicion", "closeModal('eliminarEdicion');", true);
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        protected void rptEdiciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptEquipos = (Repeater)e.Item.FindControl("rptEquipos");
                gestorEdicion.edicion =(Edicion) e.Item.DataItem;
                rptEquipos.DataSource = gestorEdicion.obtenerEquipos();
                rptEquipos.DataBind();
            }
        }

        /// <summary>
        /// Muestra el panel de error de la pag principal
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panFracaso.Visible = true;
        }
    }
}