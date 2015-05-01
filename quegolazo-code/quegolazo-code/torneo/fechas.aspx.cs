using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web13 : System.Web.UI.Page
    {
        protected int idEdicion, idFase, idGrupo, idFecha;
        protected string nickTorneo;
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                idEdicion = int.Parse(Request["idEdicion"]);
                nickTorneo = Request["nickTorneo"].ToString();
                idFase =(Request["idFase"]!=null) ? int.Parse(Request["idFase"]):0;
                idGrupo = (Request["idGrupo"] != null) ? int.Parse(Request["idGrupo"]) : 0;
                idFecha = (Request["idFecha"] != null) ? int.Parse(Request["idFecha"]) : 0;
                gestorTorneo = Sesion.getGestorTorneo();
                gestorEdicion = Sesion.getGestorEdicion();
                if (!Page.IsPostBack)
                {
                    gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);//2010
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    gestorTorneo.torneo = gestorTorneo.obtenerTorneoPorNick(nickTorneo); //jockeyClub
                    GestorControles.cargarRepeaterList(rptFases, gestorEdicion.edicion.fases);
                }
            }
            catch (Exception)
            {
                //TODO redireccionar a pagina de error
                throw;
            }
        }

        protected void rptFases_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarFase")
            {
                GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
            }
        }

        protected void rptFechas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Panel panelEstadoFase = (Panel)e.Item.FindControl("panelEstadoFase");
                    //if (((Fase)e.Item.DataItem).estado.idEstado == Estado.faseINICIADA)
                    //    panelEstadoFase.Visible = true;
                    if (((Fase)e.Item.DataItem).tipoFixture.idTipoFixture.ToString().Contains("TCT") && ((Fase)e.Item.DataItem).idFase == idFase)
                    {
                        GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
                        GestorControles.cargarRepeaterList(rptGrupos, ((Fase)e.Item.DataItem).grupos);
                        
                    //    Panel panelTCT = e.Item.FindControl("panelTCT") as Panel;
                    //    panelTCT.Visible = true;
                    //    Panel panelLlaves = e.Item.FindControl("panelLlaves") as Panel;
                    //    panelLlaves.Visible = false;
                    //    Repeater rptFechas = (Repeater)e.Item.FindControl("rptFechas");
                    //    int idFase = ((Fase)e.Item.DataItem).idFase;
                    //    gestorEdicion.faseActual = ((Fase)e.Item.DataItem);
                    //    Panel panelSinFechas = e.Item.FindControl("panelSinFechas") as Panel;
                    //    panelSinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
                    }
                    else
                    {

                        //Panel panelTCT = (Panel)e.Item.FindControl("panelTCT");
                        //panelTCT.Visible = false;
                        //Panel panelLlaves = (Panel)e.Item.FindControl("panelLlaves");
                        //panelLlaves.Visible = true;
                        //string llaves = new GestorFase().armarLlavesDeUnaFase((Fase)e.Item.DataItem);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "llaves", "var datosLlaves = " + llaves + ";", true);
                    }
                }
            }
            catch (Exception ex) {  }

        }

        protected void rptGrupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (((Grupo)e.Item.DataItem).idGrupo == idGrupo)
            {  
                Repeater rptPartidos = (Repeater)e.Item.FindControl("rptPartidos");
                GestorControles.cargarRepeaterList(rptPartidos, ((Grupo)e.Item.DataItem).fechas[idFecha].partidos);
            }
        }
    }
}