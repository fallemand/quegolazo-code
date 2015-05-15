using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class goleadores : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        private GestorJugador gestorJugador;
        private List<Jugador> goleadoresDelEquipo;
        private DataTable datosPrincipalesEquipo;        
        GestorEstadisticas gestorEstadistica;
        protected int idEdicion, idEquipo;
        protected string nickTorneo;

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

                    gestorEstadistica = new GestorEstadisticas(edicion);
                    gestorEstadistica.edicion = edicion;

                    gestorJugador = new GestorJugador();
                    GestorControles.cargarRepeaterTable(rptGoleadores, gestorEstadistica.obtenerTablaGoleadores());
                    cargarGoleadoresFases();
                    sinGoleadoresTodas.Visible = !GestorControles.cargarRepeaterTable(rptGoleadoresTodasLasFases, gestorEstadistica.obtenerTablaGoleadores());                                                             
                    sinEquipos.Visible = !GestorControles.cargarRepeaterTable(rptEquiposQueConvirtieron, gestorEstadistica.cantidadGolesPorEquipo(false));
                    sinTiposDeGoles.Visible = !GestorControles.cargarRepeaterTable(rptGolesPorTipoGol, gestorEstadistica.cantidadGolesPorTipoGol(false));
                    cargarGraficos();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void cargarGoleadoresFases()
        {            
            if (gestorEdicion.edicion.fases.Count > 1)
            {
                rptFasesEdicion.Visible = true;
                rptFasesIndividuales.Visible = true;
                GestorControles.cargarRepeaterList(rptFasesEdicion, gestorEdicion.edicion.fases);
                GestorControles.cargarRepeaterList(rptFasesIndividuales, gestorEdicion.edicion.fases);                
            }                 
        }

      

        protected void rptGoleadoresTodasLasFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litPosicionJugador = (Literal)e.Item.FindControl("litPosicionJugador");
                litPosicionJugador.Text = (e.Item.ItemIndex + 1).ToString();
            }           
        }

        protected void rptFasesIndividuales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptFaseHija = (Repeater)e.Item.FindControl("rptFaseHija");
                int idFase = ((Fase)e.Item.DataItem).idFase;
                Panel pnlSinGoleadoresFaseIndividual = (Panel)e.Item.FindControl("pnlSinGoleadoresFaseIndividual");
                pnlSinGoleadoresFaseIndividual.Visible = !GestorControles.cargarRepeaterTable(rptFaseHija, gestorEstadistica.goleadoresPorFaseDeEdicion(idFase));
            }
        }

        protected void rptFaseHija_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litPosicionJugador = (Literal)e.Item.FindControl("litPosicionJugador");
                litPosicionJugador.Text = (e.Item.ItemIndex + 1).ToString();               
            }
        }

        private void cargarGraficos()
        {
            DataTable datosGolesPorEquipo = gestorEstadistica.cantidadGolesPorEquipo(true);
            if (datosGolesPorEquipo.Rows.Count > 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "golesDeEquipo", "var golesDeEquipo = " + gestorEstadistica.generarDatosParaGraficoDeTorta(datosGolesPorEquipo) + ";", true);
            else
            {
                noGraphicsEquipos.Visible = true;
                pnlGraficoEquipos.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "golesDeEquipo", "var golesDeEquipo = null;", true);
            } 

            DataTable datosTiposGol = gestorEstadistica.cantidadGolesPorTipoGol(true);
            if (datosTiposGol != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tiposDeGol", "var tiposDeGol = " + gestorEstadistica.generarDatosParaGraficoDeTorta(datosTiposGol) + ";", true);                
            }
            else
            {
                noGraphicsTipos.Visible = true;
                pnlGraficoTipos.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tiposDeGol", "var tiposDeGol = null;", true);
            }            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "datosFases","var datosFases = " + gestorEstadistica.generarJsonParaGraficoBarraGoleadores() + ";", true); 
        }
    }
}