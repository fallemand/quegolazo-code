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
        protected static TipoFixture tipoFixture;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);
                    idFase = GestorUrl.validarFase(torneo.nick, edicion.idEdicion);
                    idFecha = GestorUrl.validarFecha(torneo.nick, edicion.idEdicion, idFase);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    idEdicion = edicion.idEdicion;

                    ViewState["idFecha"] = idFecha;
                    ViewState["idFase"] = idFase;
                    litFase.Text=idFase.ToString();
                    litFecha.Text= idFecha.ToString();
                    litLnkFase.Text=idFase.ToString();
                    litLnkFecha.Text = idFecha.ToString();

                    GestorControles.cargarRepeaterList(rptFases, gestorEdicion.edicion.fases);
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }


        protected void rptFechas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarFecha")
                {
                    ViewState["idFecha"] = e.CommandArgument.ToString();
                    litFecha.Text = e.CommandArgument.ToString();
                    litLnkFecha.Text = e.CommandArgument.ToString();
                    if (gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos[0].fechas[int.Parse(ViewState["idFecha"].ToString()) - 1].estado.idEstado != Estado.fechaREGISTRADA)
                    {
                        sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos);
                    }
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }  
        }

        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //tipoFixture = new TipoFixture();
                    //tipoFixture.idTipoFixture = ((Fase)e.Item.DataItem).tipoFixture.idTipoFixture;

                    if (((Fase)e.Item.DataItem).tipoFixture.idTipoFixture.ToString().Contains("TCT") && ((Fase)e.Item.DataItem).idFase ==int.Parse(ViewState["idFase"].ToString()))
                    {
                        gestorEdicion.faseActual = (Fase)e.Item.DataItem;
                       sinFechas.Visible= !GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
                       if (gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos[0].fechas[int.Parse(ViewState["idFecha"].ToString()) - 1].estado.idEstado != Estado.fechaREGISTRADA)
                       {
                           sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, ((Fase)e.Item.DataItem).grupos);
                       }                   
                    }
                    else
                    {
                        
                       
                    }
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }  
        }

        protected void rptGrupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptPartidos = (Repeater)e.Item.FindControl("rptPartidos");
                Literal sinPartidos = (Literal)e.Item.FindControl("sinPartidos");
                sinPartidos.Visible = !GestorControles.cargarRepeaterList(rptPartidos, ((Grupo)e.Item.DataItem).fechas[int.Parse(ViewState["idFecha"].ToString()) - 1].partidos);
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }  
        }

        protected void rptFases_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarFase")
                {
                    ViewState["idFase"] = int.Parse(e.CommandArgument.ToString());
                    gestorEdicion.faseActual = gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1];
                    litFase.Text = e.CommandArgument.ToString();
                    litLnkFase.Text = e.CommandArgument.ToString();
                    sinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, gestorEdicion.faseActual.obtenerFechas());
                    sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos);
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }    
        }
    }
}