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
                gestorTorneo = Sesion.getGestorTorneo();
                gestorEdicion = Sesion.getGestorEdicion();
                idEdicion = gestorEdicion.edicion.idEdicion;
                nickTorneo = gestorTorneo.torneo.nick;
                if (!Page.IsPostBack)
                {
                    idEdicion = int.Parse(Request["idEdicion"]);
                    nickTorneo = Request["nickTorneo"].ToString();
                    idFase = (Request["idFase"] != null) ? int.Parse(Request["idFase"]) : 1;
                    idFecha = (Request["idFecha"] != null) ? int.Parse(Request["idFecha"]) : 1;
                    ViewState["idFecha"] = idFecha;
                    ViewState["idFase"] = idFase;
                    litFase.Text=idFase.ToString();
                    litFecha.Text= idFecha.ToString();
                    litLnkFase.Text=idFase.ToString();
                    litLnkFecha.Text = idFecha.ToString();
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


        protected void rptFechas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarFecha")
            {
                ViewState["idFecha"] = e.CommandArgument.ToString();
                litFecha.Text = e.CommandArgument.ToString();
                litLnkFecha.Text = e.CommandArgument.ToString();
                GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos);
            }
        }

        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    tipoFixture = new TipoFixture();
                    tipoFixture.idTipoFixture = ((Fase)e.Item.DataItem).tipoFixture.idTipoFixture;

                    if (((Fase)e.Item.DataItem).tipoFixture.idTipoFixture.ToString().Contains("TCT") && ((Fase)e.Item.DataItem).idFase ==int.Parse(ViewState["idFase"].ToString()))
                    {
                       sinFechas.Visible= !GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
                       sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, ((Fase)e.Item.DataItem).grupos);
                    }
                    else
                    {
                        
                       
                    }
                }
            }
            catch (Exception ex) {  }
        }

        protected void rptGrupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
                Repeater rptPartidos = (Repeater)e.Item.FindControl("rptPartidos");
                Literal sinPartidos = (Literal)e.Item.FindControl("sinPartidos");
                sinPartidos.Visible = !GestorControles.cargarRepeaterList(rptPartidos, ((Grupo)e.Item.DataItem).fechas[int.Parse(ViewState["idFecha"].ToString()) - 1].partidos);
        }

        protected void rptFases_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarFase")
            {
                ViewState["idFase"] = int.Parse(e.CommandArgument.ToString());
                litFase.Text = e.CommandArgument.ToString();
                litLnkFase.Text = e.CommandArgument.ToString();
                sinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].obtenerFechas());
                sinGrupos.Visible= ! GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos);
            }
        }

    }
}