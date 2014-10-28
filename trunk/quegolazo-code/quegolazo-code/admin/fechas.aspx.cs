﻿using System;
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
            try
            {
                if (!Page.IsPostBack)
                {
                    verificarTorneoExistente();
                    obtenerEdiciónSeleccionada();
                    cargarComboEdiciones();
                    cargarComboArbitros();
                    cargarComboCanchas();
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Carga todas las referencias de una edición en sesión, y carga todas las fases
        /// autor: Facu Allemand
        /// </summary>
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
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Cada vez que se genera una fase, generar todas las fechas
        /// autor: Facu Allemand
        /// </summary>
        protected void rptFases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
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
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Cada vez que se genera una fecha, generar todos los partidos
        /// autor: Facu Allemand
        /// </summary>
        protected void rptFechas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
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
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Opciones para cada partido
        /// autor: Facu Allemand
        /// </summary>
        protected void rptPartidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                //obtengo el command argument y el id del panel collapsable seleccionado para abrirlo.
                string[] paramentros = e.CommandArgument.ToString().Split(new char[] { ';' });
                string commandArgument = paramentros[0];
                string idPanelCollapse = paramentros[1];
                Session["idPanelCollapse"] = idPanelCollapse;
                if (e.CommandName == "administrarPartido")
                {
                    limpiarCampos();
                    gestorPartido.obtenerPartidoporId(commandArgument);
                    cargarPartido();
                    mostrarFechaCollapsablePanel();
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Registra la modificación del partido
        /// autor: Facu Allemand
        /// </summary>
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                gestorPartido.modificarPartido(txtFecha.Value, txtGolesLocal.Value,txtGolesVisitante.Value,cbPenales.Checked, txtPenalesLocal.Value,txtPenalesVisitante.Value, ddlArbitros.SelectedValue, ddlCanchas.SelectedValue, obtenerTitularesLocal(), obtenerTitularesVisitante());
                mostrarPanelExito("Partido Modificado con éxito");
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                cargarRepeaterFases();
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Subform Tarjetas
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Cada vez que se selecciona un equipo, cargar los jugadores de ese equipo
        /// autor: Facu Allemand
        /// </summary>
        protected void ddlTarjetasEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                if (gestorPartido.partido.local.idEquipo == int.Parse(ddlTarjetasEquipos.SelectedValue))
                    ddlTarjetasJugadores.DataSource = gestorPartido.partido.local.jugadores;
                else
                    ddlTarjetasJugadores.DataSource = gestorPartido.partido.visitante.jugadores;
                ddlTarjetasJugadores.DataValueField = "idJugador";
                ddlTarjetasJugadores.DataTextField = "nombre";
                ddlTarjetasJugadores.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSubForm", "showSubformFast('tarjetas');", true);
                
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Agrega una tarjeta en el objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void btnTarjetaAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                gestorPartido.agregarTarjeta(ddlTarjetasEquipos.SelectedValue, ddlTarjetasJugadores.SelectedValue, ddlTarjetasTipo.SelectedValue, txtTarjetasMinuto.Value);
                cargarRepeaterTarjetas();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Elimina una tarjeta del objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void rptTarjetas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eliminarTarjeta")
                    gestorPartido.eliminarTarjeta(e.CommandArgument.ToString());
                cargarRepeaterTarjetas();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Cada vez que se genera una tarjeta en el repeater, mostrar la imagen de tarjeta roja o amarilla
        /// autor: Facu Allemand
        /// </summary>
        protected void rptTarjetas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                char tipoTarjeta = ((Tarjeta)e.Item.DataItem).tipoTarjeta;
                if (tipoTarjeta.ToString().Equals("R"))
                {
                    Label panelTarjetaRoja = e.Item.FindControl("panelTarjetaRoja") as Label;
                    panelTarjetaRoja.Visible = true;
                }
                else if (tipoTarjeta.ToString().Equals("A"))
                {
                    Label panelTarjetaAmarilla = e.Item.FindControl("panelTarjetaAmarilla") as Label;
                    panelTarjetaAmarilla.Visible = true;
                }
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Subform Goles
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Cada vez que se selecciona un equipo, cargar los jugadores
        /// autor: Facu Allemand
        /// </summary>
        protected void ddlGolesEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                if (gestorPartido.partido.local.idEquipo == int.Parse(ddlGolesEquipos.SelectedValue))
                    ddlGolesJugadores.DataSource = gestorPartido.partido.local.jugadores;
                else
                    ddlGolesJugadores.DataSource = gestorPartido.partido.visitante.jugadores;
                ddlGolesJugadores.DataValueField = "idJugador";
                ddlGolesJugadores.DataTextField = "nombre";
                ddlGolesJugadores.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSubForm", "showSubformFast('goles');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Agregar un gol en el objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void btnGolAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                gestorPartido.agregarGol(ddlGolesEquipos.SelectedValue, ddlGolesJugadores.SelectedValue, ddlGolesTipos.SelectedValue, txtGolesMinuto.Value);
                cargarRepeaterGoles();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Elimina un gol del objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void rptGoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eliminarGol")
                    gestorPartido.eliminarGol(e.CommandArgument.ToString());
                cargarRepeaterGoles();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Subform Cambios
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Cuando se selecciona un equipo, carga los jugadores del equipo
        /// autor: Facu Allemand
        /// </summary>
        protected void ddlCambiosEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                if (gestorPartido.partido.local.idEquipo == int.Parse(ddlCambiosEquipos.SelectedValue))
                {
                    ddlCambiosJugadoresEntra.DataSource = gestorPartido.partido.local.jugadores;
                    ddlCambiosJugadoresSale.DataSource = gestorPartido.partido.local.jugadores;
                }
                else
                {
                    ddlCambiosJugadoresEntra.DataSource = gestorPartido.partido.visitante.jugadores;
                    ddlCambiosJugadoresSale.DataSource = gestorPartido.partido.visitante.jugadores;
                }
                ddlCambiosJugadoresEntra.DataValueField = "idJugador";
                ddlCambiosJugadoresEntra.DataTextField = "nombre";
                ddlCambiosJugadoresEntra.DataBind();
                ddlCambiosJugadoresSale.DataValueField = "idJugador";
                ddlCambiosJugadoresSale.DataTextField = "nombre";
                ddlCambiosJugadoresSale.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSubForm", "showSubformFast('cambios');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Agrega un cambio en el objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void btnCambiosAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                mostrarFechaCollapsablePanel();
                gestorPartido.agregarCambio(ddlCambiosEquipos.SelectedValue, ddlCambiosJugadoresEntra.SelectedValue, ddlCambiosJugadoresSale.SelectedValue, txtCambiosMinuto.Value);
                cargarRepeaterCambios();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Elimina un cambio del objeto partido del gestorPartido
        /// autor: Facu Allemand
        /// </summary>
        protected void rptCambios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eliminarCambio")
                    gestorPartido.eliminarCambio(e.CommandArgument.ToString());
                cargarRepeaterCambios();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// -------------------------------------------------------------------------
        /// --------------------------Metodos Extra---------------------------------
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
            txtGolesLocal.Value = (gestorPartido.partido.golesLocal!=null) ? gestorPartido.partido.golesLocal.ToString() : "";
            txtGolesVisitante.Value = (gestorPartido.partido.golesVisitante!=null) ? gestorPartido.partido.golesVisitante.ToString() : "";
            cbPenales.Checked = (gestorPartido.partido.huboPenales != null && gestorPartido.partido.huboPenales == true) ? true : false;
            txtPenalesLocal.Value = (gestorPartido.partido.penalesLocal!=null) ? gestorPartido.partido.penalesLocal.ToString(): "";
            txtPenalesVisitante.Value = (gestorPartido.partido.penalesVisitante != null) ? gestorPartido.partido.penalesVisitante.ToString() : "";
            txtFecha.Value = (gestorPartido.partido.fecha != null) ? gestorPartido.partido.fecha.Value.ToString("dd/mm/yyyy HH:mm") : "";
            ddlArbitros.SelectedValue = (gestorPartido.partido.arbitro != null) ? gestorPartido.partido.arbitro.idArbitro.ToString() : "";
            ddlCanchas.SelectedValue = (gestorPartido.partido.cancha != null) ? gestorPartido.partido.cancha.idCancha.ToString() : "";
            cargarListaJugadoresEquipoLocal();
            cargarListaJugadoresEquipoVisitante();
            cargarRepeaterGoles();
            cargarABMGoles();
            cargarRepeaterCambios();
            cargarABMCambios();
            cargarRepeaterTarjetas();
            cargarABMTarjetas();
        }

        /// <summary>
        /// Carga todos los controles del subform de Tarjetas
        /// autor: Facu Allemand
        /// </summary>
        private void cargarABMTarjetas()
        {
            ddlTarjetasEquipos.Items.Clear();
            ddlTarjetasEquipos.Items.Add(new ListItem(gestorPartido.partido.local.nombre, gestorPartido.partido.local.idEquipo.ToString()));
            ddlTarjetasEquipos.Items.Add(new ListItem(gestorPartido.partido.visitante.nombre, gestorPartido.partido.visitante.idEquipo.ToString()));
            ddlTarjetasJugadores.DataSource = gestorPartido.partido.local.jugadores;
            ddlTarjetasJugadores.DataValueField = "idJugador";
            ddlTarjetasJugadores.DataTextField = "nombre";
            ddlTarjetasJugadores.DataBind();
        }

        /// <summary>
        /// Carga todos los controles del subform de Cambios
        /// autor: Facu Allemand
        /// </summary>
        private void cargarABMCambios()
        {
            ddlCambiosEquipos.Items.Clear();
            ddlCambiosEquipos.Items.Add(new ListItem(gestorPartido.partido.local.nombre, gestorPartido.partido.local.idEquipo.ToString()));
            ddlCambiosEquipos.Items.Add(new ListItem(gestorPartido.partido.visitante.nombre, gestorPartido.partido.visitante.idEquipo.ToString()));
            ddlCambiosJugadoresEntra.DataSource = gestorPartido.partido.local.jugadores;
            ddlCambiosJugadoresEntra.DataValueField = "idJugador";
            ddlCambiosJugadoresEntra.DataTextField = "nombre";
            ddlCambiosJugadoresEntra.DataBind();
            ddlCambiosJugadoresSale.DataSource = gestorPartido.partido.local.jugadores;
            ddlCambiosJugadoresSale.DataValueField = "idJugador";
            ddlCambiosJugadoresSale.DataTextField = "nombre";
            ddlCambiosJugadoresSale.DataBind();
        }

        /// <summary>
        /// Carga todos los controles del subform de Goles
        /// autor: Facu Allemand
        /// </summary>
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
            rptTarjetas.DataSource = gestorPartido.partido.tarjetas;
            rptTarjetas.DataBind();
        }

        /// <summary>
        /// Carga el repeater de jugadores equipo local
        /// autor: Facu Allemand
        /// </summary>
        private void cargarListaJugadoresEquipoLocal()
        {
            cblJugadoresEquipoLocal.DataSource = gestorPartido.partido.local.jugadores;
            cblJugadoresEquipoLocal.DataTextField = "nombre";
            cblJugadoresEquipoLocal.DataValueField = "idJugador";
            cblJugadoresEquipoLocal.DataBind();
            foreach (ListItem item in cblJugadoresEquipoLocal.Items)
                if (gestorPartido.esTitularLocal(Int32.Parse(item.Value)))
                    item.Selected = true;
        }

        /// <summary>
        /// Carga el repeater de jugadores equipo visitante
        /// autor: Facu Allemand
        /// </summary>
        private void cargarListaJugadoresEquipoVisitante()
        {
            cblJugadoresEquipoVisitante.DataSource = gestorPartido.partido.visitante.jugadores;
            cblJugadoresEquipoVisitante.DataTextField = "nombre";
            cblJugadoresEquipoVisitante.DataValueField = "idJugador";
            cblJugadoresEquipoVisitante.DataBind();
            foreach (ListItem item in cblJugadoresEquipoVisitante.Items)
                if (gestorPartido.esTitularVisitante(Int32.Parse(item.Value)))
                    item.Selected = true;
        }

        /// <summary>
        /// Obtiene la Edición de Sesión
        /// autor: Facu Allemand
        /// </summary>
        private void obtenerEdiciónSeleccionada()
        {
            if (gestorEdicion.edicion != null && gestorEdicion.edicion.idEdicion > 0)
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showError", "showPanelFracaso();", true);
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }

        /// <summary>
        /// Habilita el panel de exito, y muestra el mensaje.
        /// autor: Facu Allemand
        /// </summary>
        private void mostrarPanelExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showExito", "showPanelExito();", true);
            litExito.Text = mensaje;
            panelExito.Visible = true;
        }

        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// </summary>
        private void limpiarPaneles()
        {
            panelFracaso.Visible = false;
            litFracaso.Text = "";
        }

        /// <summary>
        /// Obtiene la lista de jugadores titulares del equipo local
        /// </summary>
        private List<int> obtenerTitularesLocal()
        {
            List<int> idTitulares = new List<int>();
            foreach (ListItem item in cblJugadoresEquipoLocal.Items)
                if (item.Selected)
                    idTitulares.Add(Validador.castInt(item.Value));
            return idTitulares;
        }

        /// <summary>
        /// Obtiene la lista de jugadores titulares del equipo visitante
        /// </summary>
        private List<int> obtenerTitularesVisitante()
        {
            List<int> idTitulares = new List<int>();
            foreach (ListItem item in cblJugadoresEquipoVisitante.Items)
                if (item.Selected)
                    idTitulares.Add(Validador.castInt(item.Value));
            return idTitulares;
        }

        /// <summary>
        /// Limpiar todos los campos del formulario de Administración de Partidos
        /// </summary>
        private void limpiarCampos()
        {
            txtEquipoLocal.Value = "";
            txtEquipoVisitante.Value = "";
            txtGolesLocal.Value = "";
            txtGolesVisitante.Value = "";
            txtPenalesLocal.Value = "";
            txtPenalesVisitante.Value = "";
            txtFecha.Value = "";
            ddlArbitros.SelectedValue = "";
            ddlCanchas.SelectedValue = "";
            cbPenales.Checked = false;
        }

        /// <summary>
        /// Abre el collapsable de fases y fechas para la fecha clickeada
        /// autor: Facu Allemand
        /// </summary>
        private void mostrarFechaCollapsablePanel()
        {
            if (Session["idPanelCollapse"] != null)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPanelCollapse", "showCollapsablePanel('" + Session["idPanelCollapse"] + "', true)", true);
        }
    }
}