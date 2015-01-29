using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;

namespace quegolazo_code.admin.edicion
{
    public partial class confirmar : System.Web.UI.Page
    {
       GestorEdicion gestorEdicion = new GestorEdicion();
       GestorFase gestorFase = new GestorFase();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Tomar gestores de la sesión
            gestorEdicion = Sesion.getGestorEdicion();
            gestorFase = gestorEdicion.gestorFase;
            if (gestorEdicion.edicion.equipos.Count < 2 && gestorEdicion.edicion.fases.Count < 2)
            {
                Response.Redirect(GestorUrl.eEQUIPOS);
            }
            if (!Page.IsPostBack)
            {
                //Edición
                LitEdicion.Text = gestorEdicion.edicion.nombre;

                //Equipos
                rptEquipos.DataSource = gestorEdicion.edicion.equipos;
                rptEquipos.DataBind();

                //Sanciones
                if (gestorEdicion.edicion.preferencias.sanciones)
                    rSancionesSi.Visible = true;
                else
                    rSancionesNo.Visible = true;

                //Arbitros
                if (gestorEdicion.edicion.preferencias.arbitros)
                    rArbitrosSi.Visible = true;
                else
                    rArbitrosNo.Visible = true;

                //Jugadores
                if (gestorEdicion.edicion.preferencias.jugadores)
                    rJugadoresSi.Visible = true;
                else
                    rJugadoresNo.Visible = true;

                //Cancha
                if (gestorEdicion.edicion.preferencias.canchas)
                    rCanchasSi.Visible = true;
                else
                    rCanchasNo.Visible = true;

                //Fases
                rptFases.DataSource = gestorEdicion.edicion.fases;
                rptFases.DataBind();
            }
        }

        /// <summary>
        /// Metodo para Confirmar la Edición
        /// Permite Registrar las preferencias, Registrar equipos en la edición y Registrar Fases
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ////Tomar gestores de la sesión
            //gestorEdicion = Sesion.getGestorEdicion();
            //gestorFase = Sesion.getGestorFase();
            try
            {
                //gestorEdicion.edicion.fases = gestorFase.fases;
                if (gestorEdicion.edicion.estado.idEstado == Estado.edicionREGISTRADA)
                {
                    gestorEdicion.confirmarEdicion(); //Registra las preferencias, Registra equipos en la edición y Registra Fases
                    gestorEdicion.cambiarEstadoAConfigurada(); // Cambia el estado de la Edición "REGISTRADA" a "PERSONALIZADA" 
                }
                else
                    gestorEdicion.actualizarconfirmacionEdicion(); //Actualiza las Preferencias, Actualiza equipos en la edicion y Actualiza Fases
                Response.Redirect(GestorUrl.aFECHAS);
            }
            catch(Exception ex)
            {
                panelFracaso.Visible = true;
                litFracaso.Text = ex.Message;
            }
        }

        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal LitCantidadGrupos = (Literal)e.Item.FindControl("LitCantidadGrupos");
                Literal litCantEquipo = (Literal)e.Item.FindControl("litCantidadEquipos");
                Literal litCantEquipoGrupo = (Literal)e.Item.FindControl("litCantidadEquiposGrupo");
                Literal LitCantidadFechas = (Literal)e.Item.FindControl("LitCantidadFechas");
                Literal LitPartidosPorFecha = (Literal)e.Item.FindControl("LitPartidosPorFecha");
            
                litCantEquipo.Text = (gestorEdicion.edicion.equipos.Count>0) ? gestorEdicion.edicion.equipos.Count.ToString() : "S/D";
                litCantEquipoGrupo.Text = (((Fase)e.Item.DataItem).grupos.Count > 0) ?(gestorEdicion.edicion.equipos.Count / ((Fase)e.Item.DataItem).grupos.Count).ToString() : "S/D";
                LitCantidadGrupos.Text = (((Fase)e.Item.DataItem).grupos.Count>0) ? ((Fase)e.Item.DataItem).grupos.Count.ToString() : "S/D";
                LitCantidadFechas.Text = (((Fase)e.Item.DataItem).grupos.Count > 0) ? ((Fase)e.Item.DataItem).grupos[0].fechas.Count.ToString() : "S/D";
                //LitPartidosPorFecha.Text = (((Fase)e.Item.DataItem).grupos.Count > 0 && ((Fase)e.Item.DataItem).grupos.Count!=null) ? ((Fase)e.Item.DataItem).grupos[0].fechas[0].partidos.Count.ToString() : "S/D";
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eFASES);
        }
    }
}