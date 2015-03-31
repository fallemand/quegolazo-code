using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Mvc;

namespace quegolazo_code.admin
{
    public partial class fechas : System.Web.UI.Page
    {
        public static GestorEdicion gestorEdicion;
        protected GestorPartido gestorPartido;
        private static GestorEstadisticas gestorEstadistica;
        public bool segundaVuelta = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ViewState["SegundaVuelta"] = segundaVuelta;
                gestorEdicion = Sesion.getGestorEdicion();
                gestorPartido = Sesion.getGestorPartido();
                gestorEstadistica = Sesion.getGestorEstadisticas();
                if (!Page.IsPostBack)
                {
                    gestorPartido.partido = null;
                    obtenerEdiciónSeleccionada();
                    cargarRepeaterFases();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('administrarPartido');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Eventos para botones del repeater de fases
        /// autor: Facu Allemand
        /// </summary>
        protected void rptFases_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "finalizarFase")
                {
                    gestorEdicion.faseActual = gestorEdicion.edicion.fases[int.Parse(e.CommandArgument.ToString()) - 1];
               
                    hfEquiposSeleccionados.Value = string.Empty;
                    if (gestorEdicion.esUltimaFase(gestorEdicion.faseActual.idFase))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "openModal('modalFinalizarEdicion');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideDiv", "hideDiv('modalConfirmarFinalizarFase');", true);
                        if (!gestorEdicion.gestorFase.estaFinalizada(gestorEdicion.faseActual.idFase, gestorEdicion.faseActual.idEdicion))
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDiv", "showDiv('panelFaseNoCompleta2');", true);
                    }
                    else
                    {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "openModal('modalConfirmarFinalizarFase');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "hideDiv('modalFinalizarEdicion');", true);
                         if (!gestorEdicion.gestorFase.estaFinalizada(gestorEdicion.faseActual.idFase, gestorEdicion.faseActual.idEdicion))
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDiv", "showDiv('panelFaseNoCompleta');", true);
                    
                    }
                }
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
                    LinkButton lnkFinalizarFase = (LinkButton)e.Item.FindControl("lnkFinalizarFase");
                    Panel panelEstadoFase = (Panel)e.Item.FindControl("panelEstadoFase");
                    if (((Fase)e.Item.DataItem).estado.idEstado == Estado.faseINICIADA)
                        lnkFinalizarFase.Visible = true;
                    else
                        panelEstadoFase.Visible = true;
                    Repeater rptFechas = (Repeater)e.Item.FindControl("rptFechas");
                    int idFase = ((Fase)e.Item.DataItem).idFase;
                    gestorEdicion.faseActual = ((Fase)e.Item.DataItem);
                    Panel panelSinFechas = e.Item.FindControl("panelSinFechas") as Panel;
                    panelSinFechas.Visible = !GestorControles.cargarRepeaterList(rptFechas, ((Fase)e.Item.DataItem).obtenerFechas());
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
                    Panel panelSinPartidos = e.Item.FindControl("panelSinPartidos") as Panel;
                    panelSinPartidos.Visible = !GestorControles.cargarRepeaterList(rptPartidos, ((Fecha)e.Item.DataItem).partidos);
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
                    btnCancelar.Visible = true;
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
                if (gestorPartido.partido == null)
                    throw new Exception("Debe seleccionar un partido desde la lista de fechas");
                mostrarFechaCollapsablePanel();
                gestorPartido.modificarPartido(txtFecha.Value, txtGolesLocal.Value, txtGolesVisitante.Value, cbPenales.Checked, txtPenalesLocal.Value, txtPenalesVisitante.Value, ddlArbitros.SelectedValue, ddlCanchas.SelectedValue, obtenerTitularesLocal(), obtenerTitularesVisitante());                
                mostrarPanelExito("Partido Modificado con éxito");
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                cargarRepeaterFases();
                btnCancelar.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('administrarPartido');", true);
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
            try
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
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
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
                List<Jugador> jugadores = new List<Jugador>();
                if (gestorPartido.partido.local.idEquipo == int.Parse(ddlGolesEquipos.SelectedValue))
                    jugadores = gestorPartido.partido.local.jugadores;
                else
                    jugadores = gestorPartido.partido.visitante.jugadores;
                GestorControles.cargarComboList(ddlGolesJugadores, jugadores, "idJugador", "nombre", "Sin asignar", true);
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
            txtFecha.Value = (gestorPartido.partido.fecha != null) ? gestorPartido.partido.fecha.Value.ToString("dd/MM/yyyy HH:mm") : "";
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
            GestorControles.cargarComboList(ddlTarjetasJugadores, gestorPartido.partido.local.jugadores, "idJugador", "nombre");
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
            GestorControles.cargarComboList(ddlCambiosJugadoresEntra, gestorPartido.partido.local.jugadores, "idJugador", "nombre");
            GestorControles.cargarComboList(ddlCambiosJugadoresSale, gestorPartido.partido.local.jugadores, "idJugador", "nombre");
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
            GestorControles.cargarComboList(ddlGolesJugadores, gestorPartido.partido.local.jugadores, "idJugador", "nombre", "Sin asignar", true);
            GestorControles.cargarComboList(ddlGolesTipos, gestorPartido.obtenerTiposGol(), "idTipoGol", "nombre");
        }

        /// <summary>
        /// Carga el repeater de Fases
        /// autor: Facu Allemand
        /// </summary>
        private void cargarRepeaterFases()
        {
            new GestorFase().quitarFechasGenericas(gestorEdicion.edicion.fases);
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
            panelSinJugadoresLocal.Visible = !GestorControles.cargarCheckBoxList(cblJugadoresEquipoLocal,
                    gestorPartido.partido.local.jugadores, "idJugador", "nombre");
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
            panelSinJugadoresVisitante.Visible = !GestorControles.cargarCheckBoxList(cblJugadoresEquipoVisitante,
                    gestorPartido.partido.visitante.jugadores, "idJugador", "nombre");
            foreach (ListItem item in cblJugadoresEquipoVisitante.Items)
                if (gestorPartido.esTitularVisitante(Int32.Parse(item.Value)))
                    item.Selected = true;
        }

        /// <summary>
        /// Obtiene la Edición de Sesión
        /// autor: Facu Allemand
        /// </summary>
        private static void obtenerEdiciónSeleccionada()
        {
            if (gestorEdicion.edicion != null && gestorEdicion.edicion.idEdicion > 0)
            {
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(gestorEdicion.edicion.idEdicion);
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
            }
        }

        /// <summary>
        /// Carga Combo Ediciones
        /// </summary>
        private void cargarComboEdiciones()
        {
            GestorControles.cargarComboList(ddlEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo),
                "idEdicion", "nombre", "Seleccionar Edicion", false);
            ddlEdiciones.SelectedValue = (gestorEdicion.edicion.idEdicion > 0) ? gestorEdicion.edicion.idEdicion.ToString() : "";
        }

        /// <summary>
        /// Carga Combo Arbitros
        /// </summary>
        private void cargarComboArbitros()
        {
            GestorArbitro gestorArbitro = new GestorArbitro();
            GestorControles.cargarComboList(ddlArbitros, gestorArbitro.obtenerArbitrosDeUnTorneo(),
                "idArbitro", "nombre", "Sin Árbitro Asignado", true);
        }

        /// <summary>
        /// Carga Combo Arbitros
        /// </summary>
        private void cargarComboCanchas()
        {
            GestorCancha gestorCancha = new GestorCancha();
            GestorControles.cargarComboList(ddlCanchas, gestorCancha.obtenerCanchasDeUnTorneo(),
                "idCancha", "nombre", "Sin Cancha Asignada", true);
        }

        /// <summary>
        /// Habilita el panel de fracaso, y muestra el mensaje.
        /// autor: Facu Allemand
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }

        /// <summary>
        /// Habilita el panel de exito, y muestra el mensaje.
        /// autor: Facu Allemand
        /// </summary>
        private void mostrarPanelExito(string mensaje)
        {
            GestorError.mostrarPanelExito(mensaje);
        }

        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// </summary>
        private void limpiarPaneles()
        {
            //panelFracaso.Visible = false;
            //litFracaso.Text = "";
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
            GestorControles.cleanControls(new List<Object>{txtEquipoLocal,txtEquipoVisitante,txtGolesLocal,txtGolesVisitante,
                    txtPenalesLocal, txtPenalesVisitante,txtFecha,ddlArbitros,ddlCanchas});
            cbPenales.Checked = false;
            rptCambios.DataSource = null;
            rptCambios.DataBind();
            rptGoles.DataSource = null;
            rptGoles.DataBind();
            rptTarjetas.DataSource = null;
            rptTarjetas.DataBind();
            cblJugadoresEquipoLocal.Items.Clear();
            cblJugadoresEquipoVisitante.Items.Clear();
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

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "openModal('modalFinalizarFase');", true);           
            cargarEquipos();
        }

        /// <summary>
        /// Carga los equipos
        /// autor: 
        /// </summary>
        public void cargarEquipos()
        {
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
            GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[gestorEdicion.faseActual.idFase-1].grupos);
            GestorControles.cargarRepeaterTable(rptEquipos, gestorEstadisticas.obtenerTablaPosiciones(gestorEdicion.faseActual.idFase));
        }

        /// <summary>
        /// Carga la tabla de posiciones para finalizar una edición
        /// autor: 
        /// </summary>
        public void cargarPosicionesFinales()
        {
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
            GestorControles.cargarRepeaterTable(rptPosiciones, gestorEstadisticas.obtenerTablaPosiciones(gestorEdicion.faseActual.idFase));
        }

        /// <summary>
        /// Ver si el partido es una fecha libre o un partido normal
        /// autor: Facu Allemand
        /// </summary>
        protected void rptPartidos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
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
                        if (partido.faseAsociada.tipoFixture.idTipoFixture == "ELIM")
                        {
                            if (partido.local != null || partido.visitante != null)
                            {
                                Panel panelPartidoEliminatorioIncompleto = (Panel)e.Item.FindControl("panelPartidoEliminatorioIncompleto");
                                panelPartidoEliminatorioIncompleto.Visible = true;
                                Literal litLibre = (Literal)e.Item.FindControl("litEquipo1");
                                litLibre.Text = partido.local.nombre;
                            }

                        }
                        else
                        {
                            Panel panelPartidoLibre = (Panel)e.Item.FindControl("panelPartidoLibre");
                            Literal litLibre = (Literal)e.Item.FindControl("litLibre");
                            if (partido.local != null)
                                litLibre.Text = "Libre: "+ partido.local.nombre;
                            if (partido.visitante != null)
                                litLibre.Text = "Libre: " + partido.visitante.nombre;
                            panelPartidoLibre.Visible = true;
                        }
                    }
                    LinkButton lnkAdministrarPartido = (LinkButton)e.Item.FindControl("lnkAdministrarPartido");
                    lnkAdministrarPartido.Visible = ((partido.estado.idEstado == Estado.partidoJUGADO || partido.estado.idEstado == Estado.partidoCANCELADO) && (partido.faseAsociada.estado.idEstado == Estado.faseFINALIZADA || partido.faseAsociada.estado.idEstado == Estado.faseCANCELADA)) ? false : true;

                    if (partido.faseAsociada.tipoFixture.idTipoFixture == "ELIM")
                    {
                        Label lblPrimerPuesto = (Label)e.Item.FindControl("lblPrimerPuesto");
                        Label lblTercerPuesto = (Label)e.Item.FindControl("lblTercerPuesto");
                        if (ViewState["SegundaVuelta"] != null && bool.Parse(ViewState["SegundaVuelta"].ToString()) == true)
                        {
                            lblPrimerPuesto.Visible = false;
                            lblTercerPuesto.Visible = true;
                        }
                        if (partido.idPartidoPosterior == 0 && bool.Parse(ViewState["SegundaVuelta"].ToString()) == false)
                        {
                            lblPrimerPuesto.Visible = true;
                            lblTercerPuesto.Visible = false;
                            segundaVuelta = true;
                            ViewState["SegundaVuelta"] = segundaVuelta;
                        }
                    }
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        protected void btnConfigurarFase_Click(object sender, EventArgs e)
        {
            try
            {                
            obtenerEdiciónSeleccionada();
            gestorEdicion.actualizarFaseActual();
            List<Fase> fasesParaElWidget = (List<Fase>)GestorColecciones.clonarLista(gestorEdicion.edicion.fases);                
            hfEquiposSeleccionados.Value = string.Empty;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("CantidadEquiposInvalida")) {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalCantidadEquipos2", "openModal('modalCambioEnCantidades');", true); 
                }
                else
                    GestorError.mostrarPanelFracaso("Se ha producido un error: " + ex.Message);
               
            }
        }
        /// <summary>
        /// Valida si la fase que sigue estaba creada genericamente o no, y en base a eso analiza si mostrar o no un mensaje al usuario en caso de que haya conflicto con la cantida de equipos que ha elegido.
        /// </summary>
        /// <param name="fasesParaElWidget">Las fases que se van a validar</param>
        private static void validarFases(List<Fase> fasesParaElWidget, string idEquipos)
        {
            if (gestorEdicion.verificarProximaFase(fasesParaElWidget, gestorEdicion.faseActual.idFase+1))
            { //si tenia una fase generica configurada, verificamos las cantidades de equipos.
                if (!new GestorFase().validarCantidadEquipos(idEquipos, gestorEdicion.faseActual.idFase+1, fasesParaElWidget))
                {
                    System.Web.HttpContext.Current.Session["fasesParaElWidget"]= fasesParaElWidget;                    
                    throw new Exception("CantidadEquiposInvalida");
                }                    
            }
        }
        /// <summary>
        /// Renderiza el widget de configuracion de fases en la pantalla
        /// </summary>
        /// <param name="fasesParaElWidget">EL conjunto de fases que se van a </param>
        /// <param name="faseActual"></param>
        private static string armarFases(List<Fase> fasesParaElWidget, string idEquipos, bool eliminaFasesPosteriores)
        {
            Fase faseActual = gestorEdicion.getFaseActual(fasesParaElWidget);
            GestorFase gestorFase= new GestorFase();
            gestorEdicion.agregarEquiposEnFase(fasesParaElWidget, idEquipos, faseActual.idFase);
            gestorFase.reducirFases(fasesParaElWidget);            
            if (eliminaFasesPosteriores)
            {
                gestorFase.eliminarFasesPosteriores(fasesParaElWidget, faseActual);
                faseActual = gestorEdicion.getFaseActual(fasesParaElWidget);
            }
            return gestorFase.armarJsonParaWidget(fasesParaElWidget, gestorEdicion.edicion.idEdicion, gestorEdicion.edicion.equipos, ((faseActual != null) ? faseActual.idFase : 1), false);
         }

        protected void btnFinalizarEdicion_Click(object sender, EventArgs e)
        {
            cargarPosicionesFinales();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModalFinalizarEdicion", "openModal('modalSeleccionarGanadores');ordenarTabla();", true);
            
        }

        [System.Web.Services.WebMethod(enableSession: true)]
        public static string finalizarFase(object idEquipos)
        {
            try
            {
                obtenerEdiciónSeleccionada();
                gestorEdicion.actualizarFaseActual();
                List<Fase> fasesParaElWidget = (List<Fase>)GestorColecciones.clonarLista(gestorEdicion.edicion.fases);     
                JavaScriptSerializer s = new JavaScriptSerializer();
                string equipos = s.ConvertToType<string>(idEquipos);                             
                validarFases(fasesParaElWidget,equipos);
                string fases = armarFases(fasesParaElWidget, equipos, false); 
                return fases;

            }
            catch (Exception ex)
            {
                
                return "Error: " + ex.Message;
                
            }
        }



        [WebMethod(enableSession: true)]
        public static object guardarPosicionesEquipos(object idEquipos)
        {
            try
            {
                JavaScriptSerializer serializador = new JavaScriptSerializer();
                List<Int64> ids = serializador.ConvertToType<List<Int64>>(idEquipos);
                GestorEdicion gestorEdicion = Sesion.getGestorEdicion();
                //Guarda en la tabla TablaPosicionesFinal los equipos ganadores de acuerdo al orden que estableció el usuario
                gestorEstadistica.guardarTablaPosicionesFinal(ids, gestorEdicion.edicion.idEdicion);
                //Cierra la edición
                gestorEdicion.cerrarEdicion(gestorEdicion.edicion.idEdicion);
                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Ha ocurrido un error en el servidor: '" + ex.Message + "'");
            }
        }

        protected void btnConfirmarFinalizacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "claseModal", "closeModal('modalSeleccionarGanadores');", true);
            Response.Redirect(GestorUrl.aFECHAS);
        }

        protected void btnCambioEnCantidadEquipos_Click(object sender, EventArgs e)
        {
            try
            {
                List<Fase> fasesParaElWidget = (List<Fase>)Session["fasesParaElWidget"];
                string datosWidget = armarFases(fasesParaElWidget, hfEquiposSeleccionados.Value, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cambioEnEquipos(); $('#contenedorFases').generadorDeFases(" + datosWidget + "); ", true);             
           
            }
            catch (Exception ex)
            {

                GestorError.mostrarPanelFracaso(ex.Message);
            }
           
        }


        /// <summary>
        /// Guarda en sesión la configuración de fases
        /// autor: Antonio Herrera
        /// </summary>
        [WebMethod(enableSession: true)]
        public static object guardarFases(object JSONFases)
        {
            try
            {
                JavaScriptSerializer serializador = new JavaScriptSerializer();
                List<Fase> fases = serializador.ConvertToType<List<Fase>>(JSONFases);
                GestorEdicion gestor = Sesion.getGestorEdicion();
                gestor.edicion.fases = fases;
                gestor.faseActual = gestor.getFaseActual(fases);
                gestor.actualizarFasesLuegoDeCerrarUna(gestor.edicion, gestor.faseActual.idFase);
                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Ha ocurrido un error en el servidor: '" + ex.Message + "'");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.aFECHAS);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCampos();
                btnCancelar.Visible = false;
                gestorPartido.partido = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('administrarPartido');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }
    }
}