using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class fechas : System.Web.UI.Page
    {
        protected GestorEdicion gestorEdicion;
        protected GestorTorneo gestorTorneo;
        protected String nickTorneo;
        protected int idEdicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {        
                if (!Page.IsPostBack)
                {
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;
                    nickTorneo = torneo.nick;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    idEdicion = edicion.idEdicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();

                    cargarRepeaterFases();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Ver si el partido es una fecha libre o un partido normal
        /// autor: Facu Allemand
        /// </summary>
        protected void rptPartidos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Partido partido = ((Partido)e.Item.DataItem);
                Edicion edicionAsociada = gestorEdicion.edicion;

                if (partido.local != null && partido.visitante != null)
                {
                    Panel panelPartidoNormal = (Panel)e.Item.FindControl("panelPartidoNormal");
                    panelPartidoNormal.Visible = true;
                }
                else
                {
                    Panel panelPartidoLibre = (Panel)e.Item.FindControl("panelPartidoLibre");
                    Literal litLibre = (Literal)e.Item.FindControl("litLibre");
                    if (partido.local != null)
                        litLibre.Text = "Libre: " + partido.local.nombre;
                    if (partido.visitante != null)
                        litLibre.Text = "Libre: " + partido.visitante.nombre;
                    panelPartidoLibre.Visible = true;
                }    
            }
        }

        /// <summary>
        /// Carga el repeater de Fases
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterFases()
        {
            panelSinFases.Visible = !GestorControles.cargarRepeaterList(rptFases, gestorEdicion.edicion.fases);
        }

        /// <summary>
        /// Cada vez que se genera una fase, generar todas las fechas
        /// autor: Facu Allemand
        /// </summary>
        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel panelEstadoFase = (Panel)e.Item.FindControl("panelEstadoFase");
                if (((Fase)e.Item.DataItem).estado.idEstado == Estado.faseINICIADA)
                    panelEstadoFase.Visible = true;
                if (((Fase)e.Item.DataItem).tipoFixture.idTipoFixture.ToString().Contains("TCT"))
                {
                    Panel panelTCT = e.Item.FindControl("panelTCT") as Panel;
                    panelTCT.Visible = true;
                    Panel panelLlaves = e.Item.FindControl("panelLlaves") as Panel;
                    panelLlaves.Visible = false;
                    Repeater rptFechas = (Repeater)e.Item.FindControl("rptFechas");
                    int idFase = ((Fase)e.Item.DataItem).idFase;
                    gestorEdicion.faseActual = ((Fase)e.Item.DataItem);
                    Panel panelSinFechas = e.Item.FindControl("panelSinFechas") as Panel;
                    panelSinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
                }
                else
                {
                       
                    Panel panelTCT = (Panel)e.Item.FindControl("panelTCT");
                    panelTCT.Visible = false;
                    Panel panelLlaves = (Panel)e.Item.FindControl("panelLlaves");
                    panelLlaves.Visible = true;
                    string llaves = new GestorFase().armarLlavesDeUnaFase((Fase)e.Item.DataItem);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "llaves", "$('#divLlaves" + ((Fase)e.Item.DataItem).idFase + "').generadorDeLlaves("+llaves+");", true);
                }
            }
        }

        protected void rptFechas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptPartidos = (Repeater)e.Item.FindControl("rptPartidos");
                int idFecha = ((Fecha)e.Item.DataItem).idFecha;
                Panel panelSinPartidos = e.Item.FindControl("panelSinPartidos") as Panel;
                panelSinPartidos.Visible = !GestorControles.cargarRepeaterList(rptPartidos, ((Fecha)e.Item.DataItem).partidos);
            }
        }
    }
}