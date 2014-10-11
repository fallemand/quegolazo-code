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
        GestorPartido gestorPartido;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEdicion = Sesion.getGestorEdicion();
            gestorPartido = Sesion.getGestorPartido();
            if (!Page.IsPostBack)
            {
                verificarTorneoExistente();
                try
                {
                    obtenerEdiciónSeleccionada();
                    cargarComboEdiciones();
                    cargarComboArbitros();
                    cargarComboCanchas();
                }
                catch (Exception ex)
                {
                    mostrarPanelFracaso(ex.Message);
                }
            }
        }

        protected void btnSeleccionarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                int idEdicion = Validador.castInt(ddlEdiciones.SelectedValue);
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                cargarRepeaterFases();
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
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

        protected void rptPartidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "administrarPartido")
                {
                    gestorPartido.obtenerPartidoporId(e.CommandArgument.ToString());
                    cargarPartido(); 
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        protected void ddlGolesEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gestorPartido.partido.local.idEquipo == int.Parse(ddlGolesEquipos.SelectedValue)) 
                    ddlGolesJugadores.DataSource = gestorPartido.partido.local.jugadores;
                else
                    ddlGolesJugadores.DataSource = gestorPartido.partido.visitante.jugadores;
                ddlGolesJugadores.DataValueField = "idJugador";
                ddlGolesJugadores.DataTextField = "nombre";
                ddlGolesJugadores.DataBind();
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
            
        }

        /// <summary>
        /// -------------------------------------------------------------------------
        /// ------------------------- Metodos Extra---------------------------------
        /// -------------------------------------------------------------------------
        /// </summary>

        /// <summary>
        /// Carga todo el formulario los datos de un partido
        /// autor: Facu Allemand
        /// </summary>
        private void cargarPartido()
        {
            txtEquipoLocal.Value = gestorPartido.partido.local.nombre;
            txtEquipoVisitante.Value = gestorPartido.partido.visitante.nombre;
            txtFecha.Value = (gestorPartido.partido.fecha != null) ? gestorPartido.partido.fecha.Value.ToString("{0:dd/mm/yyyy HH:mm}") : "";
            ddlArbitros.SelectedValue = (gestorPartido.partido.arbitro != null) ? gestorPartido.partido.arbitro.idArbitro.ToString() : "";
            ddlCanchas.SelectedValue = (gestorPartido.partido.cancha != null) ? gestorPartido.partido.cancha.idCancha.ToString() : "";
            cargarRepeaterJugadoresEquipoLocal();
            cargarRepeaterJugadoresEquipoVisitante();
            cargarRepeaterGoles();
            cargarABMGoles();
            cargarRepeaterCambios();
            cargarABMCambios();
            cargarRepeaterTarjetas();
            cargarABMTarjetas();
        }

        private void cargarABMTarjetas()
        {
            
        }


        private void cargarABMCambios()
        {
            //throw new NotImplementedException();
        }

        private void cargarABMGoles()
        {
            ddlGolesEquipos.Items.Clear();
            ddlGolesEquipos.Items.Add(new ListItem(gestorPartido.partido.local.nombre, gestorPartido.partido.local.idEquipo.ToString()));
            ddlGolesEquipos.Items.Add(new ListItem(gestorPartido.partido.visitante.nombre, gestorPartido.partido.visitante.idEquipo.ToString()));
            ddlGolesJugadores.DataSource = gestorPartido.partido.local.jugadores;
            ddlGolesJugadores.DataValueField = "idJugador";
            ddlGolesJugadores.DataTextField = "nombre";
            ddlGolesJugadores.DataBind();
            ddlGolesTipos.DataSource = gestorPartido.obtenerTiposGol();
            ddlGolesTipos.DataValueField = "idTipoGol";
            ddlGolesTipos.DataTextField = "nombre";
            ddlGolesTipos.DataBind();
        }

        /// <summary>
        /// Carga el repeater de Fases
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterFases()
        {
            rptFases.DataSource = gestorEdicion.edicion.fases;
            rptFases.DataBind();
            panelSinFases.Visible = (rptFases.Items.Count > 0) ? false : true;
        }

        /// <summary>
        /// Carga el repeater de Goles
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterGoles()
        {
            rptGoles.DataSource = gestorPartido.partido.goles;
            rptGoles.DataBind();
        }

        /// <summary>
        /// Carga el repeater de Cambios
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterCambios()
        {
            rptCambios.DataSource = gestorPartido.partido.cambios;
            rptCambios.DataBind();
        }

        /// <summary>
        /// Carga el repeater de Cambios
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterTarjetas()
        {
            rptTarjetas.DataSource = gestorPartido.partido.cambios;
            rptTarjetas.DataBind();
        }

        /// <summary>
        /// Carga el repeater de jugadores equipo local
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterJugadoresEquipoLocal()
        {
            rptJugadoresEquipoLocal.DataSource = gestorPartido.partido.local.jugadores;
            rptJugadoresEquipoLocal.DataBind();
        }

        /// <summary>
        /// Carga el repeater de jugadores equipo visitante
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterJugadoresEquipoVisitante()
        {
            rptJugadoresEquipoLocal.DataSource = gestorPartido.partido.visitante.jugadores;
            rptJugadoresEquipoLocal.DataBind();
        }

        /// <summary>
        /// Obtiene la Edición de Sesión
        /// autor: Facu Allemand
        /// </summary>
        private void obtenerEdiciónSeleccionada()
        {
            if (gestorEdicion.edicion.idEdicion > 0)
            {
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(gestorEdicion.edicion.idEdicion);
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                cargarRepeaterFases();
            }
        }

        /// <summary>
        /// Carga Combo Ediciones
        /// </summary>
        private void cargarComboEdiciones()
        {
            ddlEdiciones.DataSource = gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo);
            ddlEdiciones.DataTextField = "nombre";
            ddlEdiciones.DataValueField = "idEdicion";
            ddlEdiciones.DataBind();
            ListItem itemSeleccionarEdicion = new ListItem("Seleccionar Edicion", "", true);
            itemSeleccionarEdicion.Attributes.Add("disabled", "disabled");
            ddlEdiciones.Items.Insert(0, itemSeleccionarEdicion);
            if (gestorEdicion.edicion.idEdicion > 0)
                ddlEdiciones.SelectedValue = gestorEdicion.edicion.idEdicion.ToString();
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

        /// <summary>
        /// Verifica si hay un torneo seleccionado
        /// autor: Facu Allemand
        /// </summary>
        private void verificarTorneoExistente()
        {
            try
            {
                Sesion.getTorneo();
            }
            catch (Exception)
            {
                Response.Redirect(GestorUrl.aTORNEOS);
            }
        }

        /// <summary>
        /// Habilita el panel de fracaso, y muestra el mensaje.
        /// autor: Facu Allemand
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }
    }
}