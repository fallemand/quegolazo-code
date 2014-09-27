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
    public partial class fechas : System.Web.UI.Page
    {
        GestorEdicion gestorEdicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEdicion = Sesion.getGestorEdicion();
            if (!Page.IsPostBack)
            {
                try
                {
                    Sesion.getTorneo();
                    cargarComboEdiciones();
                    cargarComboArbitros();
                    cargarComboCanchas();
                }
                catch (Exception)
                {
                    Response.Redirect(GestorUrl.aTORNEOS);
                }
            }
        }

        protected void btnSeleccionarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                int idEdicion = Validador.castInt(ddlEquipos.SelectedValue);
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                rptFases.DataSource = gestorEdicion.edicion.fases;
                rptFases.DataBind();
                panelSinFases.Visible = (rptFases.Items.Count > 0) ? false : true;
                //cargarRepeaterFases();
                //habilitarCampos();
            }
            catch (Exception ex)
            {
                //mostrarPanelFracasoListaJugadores(ex.Message);
            }
        }

        /// <summary>
        /// Carga Combo Equipos
        /// </summary>
        private void cargarComboEdiciones()
        {
            ddlEquipos.DataSource = gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo);
            ddlEquipos.DataTextField = "nombre";
            ddlEquipos.DataValueField = "idEdicion";
            ddlEquipos.DataBind();
            ListItem itemSeleccionarEdicion = new ListItem("Seleccionar Edicion", "", true);
            itemSeleccionarEdicion.Attributes.Add("disabled", "disabled");
            ddlEquipos.Items.Insert(0, itemSeleccionarEdicion);
            if (gestorEdicion.edicion.idEdicion > 0)
                ddlEquipos.SelectedValue = gestorEdicion.edicion.idEdicion.ToString();
            else
                itemSeleccionarEdicion.Selected = true;
        }

        /// <summary>
        /// Carga Combo Arbitros
        /// </summary>
        private void cargarComboArbitros()
        {
            GestorArbitro gestorArbitro = new GestorArbitro();
            ddlArbitros.DataSource = gestorArbitro.obtenerArbitrosDeUnTorneo();
            ddlArbitros.DataTextField = "nombre";
            ddlArbitros.DataValueField = "idArbitro";
            ddlArbitros.DataBind();
            ListItem itemSinArbitro = new ListItem("Sin Árbitro Asignado", "", true);
            ddlArbitros.Items.Insert(0, itemSinArbitro);
        }

        /// <summary>
        /// Carga Combo Arbitros
        /// </summary>
        private void cargarComboCanchas()
        {
            GestorCancha gestorCancha = new GestorCancha();
            ddlCanchas.DataSource = gestorCancha.obtenerCanchasDeUnTorneo();
            ddlCanchas.DataTextField = "nombre";
            ddlCanchas.DataValueField = "idCancha";
            ddlCanchas.DataBind();
            ListItem itemSinCancha = new ListItem("Sin Cancha Asignada", "", true);
            ddlCanchas.Items.Insert(0, itemSinCancha);
        }

        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptFechas = (Repeater)e.Item.FindControl("rptFechas");
                int idFase = ((Fase)e.Item.DataItem).idFase;
                gestorEdicion.gestorFase.faseActual = ((Fase)e.Item.DataItem);
                rptFechas.DataSource = ((Fase)e.Item.DataItem).obtenerFechas();
                rptFechas.DataBind();
                Panel panelSinFechas = e.Item.FindControl("panelSinFechas") as Panel;
                panelSinFechas.Visible = (rptFechas.Items.Count > 0) ? false : true;
            }
        }

        protected int idFaseActual()
        {
            return gestorEdicion.gestorFase.faseActual.idFase;
        }

        protected void rptFechas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptPartidos = (Repeater)e.Item.FindControl("rptPartidos");
                int idFecha = ((Fecha)e.Item.DataItem).idFecha;
                rptPartidos.DataSource = ((Fecha)e.Item.DataItem).partidos;
                rptPartidos.DataBind();
                Panel panelSinPartidos = e.Item.FindControl("panelSinPartidos") as Panel;
                panelSinPartidos.Visible = (rptPartidos.Items.Count > 0) ? false : true;
            }
        }
    }
}