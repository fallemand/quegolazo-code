using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;
using System.Web.Script.Serialization;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web13 : System.Web.UI.Page
    {
        protected int idEdicion, idFase, idGrupo, idFecha;
        protected string nickTorneo, nombreTorneo;
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        private JavaScriptSerializer serializer;
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
                    nickTorneo = torneo.nick;
                    nombreTorneo = torneo.nombre;
                    ViewState["nickTorneo"] = nickTorneo;
                    ViewState["nombreTorneo"] = nombreTorneo;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    idEdicion = edicion.idEdicion;
                    ViewState["idEdicion"] = idEdicion;

                    serializer = new JavaScriptSerializer();
                    ViewState["gestorEdicion"] = serializer.Serialize(gestorEdicion);

                    ViewState["idFase"] = idFase;
                    ViewState["idFecha"] = idFecha;
                    litFase.Text = idFase.ToString();
                    litFecha.Text = idFecha.ToString();
                    litLnkFase.Text = idFase.ToString();
                    litLnkFecha.Text = idFecha.ToString();

                    GestorControles.cargarRepeaterList(rptFases, gestorEdicion.edicion.fases);
                    cargarFase();
                }
                else {
                    nickTorneo = ViewState["nickTorneo"].ToString();
                    nombreTorneo = ViewState["nombreTorneo"].ToString();
                    serializer = new JavaScriptSerializer();
                    gestorEdicion = serializer.Deserialize<GestorEdicion>(ViewState["gestorEdicion"].ToString());
                    idEdicion = int.Parse(ViewState["idEdicion"].ToString());
                    idFase = int.Parse(ViewState["idFase"].ToString());
                    idFecha = int.Parse(ViewState["idFecha"].ToString());
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void cargarFase()
        {
            try
            {
                Fase fase = gestorEdicion.edicion.fases[idFase - 1];
                gestorEdicion.faseActual = fase;
                sinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, fase.obtenerFechas());
            }
            catch (Exception) { throw new Exception("No existe esa fase"); }
            try
            {
                if (gestorEdicion.edicion.fases[idFase - 1].grupos[0].fechas[idFecha - 1].estado.idEstado != Estado.fechaREGISTRADA)
                {
                    sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[idFase - 1].grupos);
                }
            }
            catch (Exception) { throw new Exception("No existe esa fecha"); }
        }

        protected void rptFechas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarFecha")
                {
                    idFecha = int.Parse(e.CommandArgument.ToString());
                    litFecha.Text = e.CommandArgument.ToString();
                    litLnkFecha.Text = e.CommandArgument.ToString();
                    if (gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos[0].fechas[idFecha - 1].estado.idEstado != Estado.fechaREGISTRADA)
                    {
                        sinGrupos.Visible = !GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[int.Parse(ViewState["idFase"].ToString()) - 1].grupos);
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
                sinPartidos.Visible = !GestorControles.cargarRepeaterList(rptPartidos, ((Grupo)e.Item.DataItem).fechas[idFecha - 1].partidos);
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }  
        }

        protected void rptFases_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarFase")
                {
                    idFase = int.Parse(e.CommandArgument.ToString());
                    ViewState["idFase"] = idFase;
                    gestorEdicion.faseActual = gestorEdicion.edicion.fases[idFase - 1];
                    litFase.Text = e.CommandArgument.ToString();
                    litLnkFase.Text = e.CommandArgument.ToString();
                    sinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, gestorEdicion.faseActual.obtenerFechas());
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }    
        }
    }
}