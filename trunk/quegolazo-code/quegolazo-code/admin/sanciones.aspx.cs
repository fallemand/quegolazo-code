using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using Utils;
using AccesoADatos;
using System.Data;

namespace quegolazo_code.admin
{
    public partial class sanciones : System.Web.UI.Page
    {
        GestorSancion gestorSancion;
        GestorEdicion gestorEdicion;
        GestorFase gestorFase;        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorSancion = Sesion.getGestorSancion();
                gestorEdicion = Sesion.getGestorEdicion();
                gestorFase = Sesion.getGestorFase();
                if (!Page.IsPostBack)
                {
                    cargarComboEdiciones();                    
                }
            }
            catch (Exception ex)
            { 
            }
        }

        public void cargarComboEdiciones()
        {
            GestorControles.cargarComboList(ddlEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo),
                "idEdicion", "nombre", "Seleccionar Edicion", false);
            ddlEdiciones.SelectedValue = (gestorEdicion.edicion.idEdicion > 0) ? gestorEdicion.edicion.idEdicion.ToString() : ""; 
        }

        protected void btnSeleccionarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeshabilitaPanel", "deshabilitarPanel();", true);
                int idEdicion = Validador.castInt(ddlEdiciones.SelectedValue);
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
                cargarRepeaterSanciones(ddlEdiciones.SelectedValue);
                cargarComboEquipos();
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                ////HAY QUE CAMBIARLO. TIENE QUE TRAER LA FASE ACTUAL
                //gestorEdicion.faseActual = gestorEdicion.edicion.fases[0];
                gestorEdicion.actualizarFaseActual(gestorEdicion);
                cargarComboFechas();
                cargarComboMotivos();
                rdEquipos.Checked = true;
                rdSinDefinir.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "equipoYSinDefinir()", "equipoYSinDefinir();", true);
            }
            catch (Exception ex) 
            { 
                //mostrarPanelFracaso(ex.Message); 
            }
        }

        public void cargarComboFechas()
        {
            GestorControles.cargarComboList(ddlFecha, gestorEdicion.faseActual.obtenerFechas(),
                "idFecha", "nombreCompleto", "Seleccionar Fecha", false);
        }

        public void cargarComboMotivos()
        {
            GestorControles.cargarComboList(ddlMotivo, gestorSancion.obtenerMotivos(),
                "idMotivoSancion", "nombre", "Seleccionar Motivo", false);
        }

        public void cargarComboEquipos()
        {
            GestorControles.cargarComboList(ddlEquipoSinPartido, gestorEdicion.obtenerEquipos(),
                "idEquipo", "nombre", "Seleccionar Equipo", false);
        }

        protected void ddlFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cambioPartido1", "cambioPartido();", true);
            ddlPartido.Items.Clear();
            GestorControles.cargarComboList(ddlPartido, gestorSancion.obtenerPartidosDeFecha(ddlFecha.SelectedValue),
                "idPartido", "nombreCompleto", "Seleccionar Partido", false);
        }

        protected void ddlPartido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cambioPartido1", "cambioPartido();", true);
            ddlEquipo.Items.Clear();
            GestorControles.cargarComboList(ddlEquipo, gestorSancion.obtenerEquiposDePartido(ddlPartido.SelectedValue),
                "idEquipo", "nombre", "Seleccionar Equipo", false);
        }

        protected void ddlEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cambioPartido3", "cambioPartido();", true);
            ddlJugador.Items.Clear();
            GestorControles.cargarComboList(ddlJugador, gestorSancion.obtenerJugadoresDeEquipo(ddlEquipo.SelectedValue),
                "idJugador", "nombre", "Seleccionar Jugador", false);
        }
        
        protected void btnRegistrarSancion_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ddlEquipo.SelectedValue.Equals(string.Empty) && ddlEquipoSinPartido.SelectedValue.Equals(string.Empty)))
                    throw new Exception("Debe seleccionar un equipo"); 
                if (txtFecha.Value.Equals(string.Empty))
                    throw new Exception("Debe ingresar la fecha de registro de la sanción"); 
                if (ddlMotivo.SelectedValue.Equals(string.Empty))
                    throw new Exception("Debe seleccionar un motivo");                              

                if(rdEquipos.Checked && rdSinDefinir.Checked) //CASO MÁS SIMPLE
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, string.Empty, string.Empty, ddlEquipoSinPartido.SelectedValue, string.Empty, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.faseActual.idFase.ToString());
                if(rdEquipos.Checked && rdPartido.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, string.Empty, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.faseActual.idFase.ToString());
                if(rdJugadores.Checked && rdSinDefinir.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, string.Empty, string.Empty, ddlEquipoSinPartido.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.faseActual.idFase.ToString());
                if(rdJugadores.Checked && rdPartido.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.faseActual.idFase.ToString());
                 
                cargarRepeaterSanciones(ddlEdiciones.SelectedValue);
                gestorSancion.sancion = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpcionPorDefecto", "deshabilitarPanel(); limpiarCombos(); equipoYSinDefinir();", true);
                cargarComboEquipos();
                cargarComboFechas();
                cargarComboMotivos();
                rdEquipos.Checked = true;
                rdSinDefinir.Checked = true;
                limpiarCamposSanciones();
            }
            catch (Exception ex)
            {
                GestorError.mostrarPanelFracaso(ex.Message);
                rdEquipos.Checked = true;
                rdSinDefinir.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "equipoYSinDefinir()", "equipoYSinDefinir();", true);
            }
        }

        public void cargarRepeaterSanciones(string idEdicion)
        {          
            sinSanciones.Visible = !GestorControles.cargarRepeaterTable(rptSanciones, gestorSancion.obtenerSancionesDeUnaEdicion(idEdicion));
        }

        protected void ddlEquipoSinPartido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cambioJugadores", "cambioJugadores();", true);
            ddlJugador.Items.Clear();
            GestorControles.cargarComboList(ddlJugador, gestorSancion.obtenerJugadoresDeEquipo2(ddlEquipoSinPartido.SelectedValue),
                "idJugador", "nombre", "Seleccionar Jugador", false);
        }

        public void limpiarCamposSanciones()
        {
            txtFecha.Value = "";
            txtObservacion.Value = "";
            txtPuntosAQuitar.Value = "";
            txtCantidadFechasSuspendidas.Value = "";
        }

        protected void rptSanciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                gestorSancion.obtenerSancionPorId(e.CommandArgument.ToString());
                if (e.CommandName == "editarSancion")
                {
                    btnRegistrarSancion.Visible = false;
                    btnModificarSancion.Visible = true;
                    btnCancelarModificacionSancion.Visible = true;
                    //lo común para los cuatro casos
                    ddlMotivo.SelectedValue = gestorSancion.sancion.motivoSancion.idMotivoSancion.ToString();
                    txtFecha.Value = gestorSancion.sancion.fechaSancion.ToString();
                    txtObservacion.Value = gestorSancion.sancion.observacion;
                    txtCantidadFechasSuspendidas.Value = gestorSancion.sancion.cantidadFechasSuspendidas.ToString();

                    //Lo particular de cada caso dividido por caso
                    //CASO JUGADOR Y PARTIDO
                    if (gestorSancion.sancion.idJugador != null && gestorSancion.sancion.idPartido != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "edicionJugadorYPartido", "jugadorYPartido();", true);
                        ddlFecha.SelectedValue = gestorSancion.sancion.idFecha.ToString();
                        GestorControles.cargarComboList(ddlPartido, gestorSancion.obtenerPartidosDeFecha(ddlFecha.SelectedValue),
                        "idPartido", "nombreCompleto", "Seleccionar Partido", false);
                        ddlPartido.SelectedValue = gestorSancion.sancion.idPartido.ToString();
                        GestorControles.cargarComboList(ddlEquipo, gestorSancion.obtenerEquiposDePartido(ddlPartido.SelectedValue),
                        "idEquipo", "nombre", "Seleccionar Equipo", false);
                        ddlEquipo.SelectedValue = gestorSancion.sancion.idEquipo.ToString();
                        GestorControles.cargarComboList(ddlJugador, gestorSancion.obtenerJugadoresDeEquipo2(ddlEquipo.SelectedValue),
                        "idJugador", "nombre", "Seleccionar Jugador", false);
                        ddlJugador.SelectedValue = gestorSancion.sancion.idJugador.ToString();                        
                        txtPuntosAQuitar.Value = gestorSancion.sancion.puntosAQuitar.ToString();
                        ddlJugador.Enabled = false;
                        ddlEquipo.Enabled = false;
                        ddlFecha.Enabled = false;
                        ddlPartido.Enabled = false;
                    }
                    //CASO EQUIPO - SIN DEFINIR
                    if (gestorSancion.sancion.idJugador == null && gestorSancion.sancion.idPartido == null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "edicionEquipoYSinDefinir", "equipoYSinDefinir();", true);
                        ddlEquipoSinPartido.SelectedValue = gestorSancion.sancion.idEquipo.ToString();
                        txtFecha.Value = gestorSancion.sancion.fechaSancion.ToString();
                        txtPuntosAQuitar.Value = gestorSancion.sancion.puntosAQuitar.ToString();
                        ddlEquipoSinPartido.Enabled = false;
                    }
                    //CASO EQUIPO - PARTIDO
                    if (gestorSancion.sancion.idJugador == null && gestorSancion.sancion.idPartido != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "edicionEquipoYPartido()", "equipoYPartido();", true);
                        ddlFecha.SelectedValue = gestorSancion.sancion.idFecha.ToString();
                        GestorControles.cargarComboList(ddlPartido, gestorSancion.obtenerPartidosDeFecha(ddlFecha.SelectedValue),
                        "idPartido", "nombreCompleto", "Seleccionar Partido", false);
                        ddlPartido.SelectedValue = gestorSancion.sancion.idPartido.ToString();
                        GestorControles.cargarComboList(ddlEquipo, gestorSancion.obtenerEquiposDePartido(ddlPartido.SelectedValue),
                        "idEquipo", "nombre", "Seleccionar Equipo", false);
                        ddlEquipo.SelectedValue = gestorSancion.sancion.idEquipo.ToString();
                        txtPuntosAQuitar.Value = gestorSancion.sancion.puntosAQuitar.ToString();
                        ddlEquipo.Enabled = false;
                        ddlFecha.Enabled = false;
                        ddlPartido.Enabled = false;
                    }
                    //CASO JUGADOR Y SIN DEFINIR
                    if (gestorSancion.sancion.idJugador != null && gestorSancion.sancion.idPartido == null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "edicionJugadorYSinDefinir()", "jugadorYSinDefinir();", true);
                        ddlEquipoSinPartido.SelectedValue = gestorSancion.sancion.idEquipo.ToString();
                        GestorControles.cargarComboList(ddlJugador, gestorSancion.obtenerJugadoresDeEquipo2(ddlEquipoSinPartido.SelectedValue),
                        "idJugador", "nombre", "Seleccionar Jugador", false);
                        ddlJugador.SelectedValue = gestorSancion.sancion.idJugador.ToString();
                        ddlEquipoSinPartido.Enabled = false;
                        ddlJugador.Enabled = false;
                    }
                }
                if (e.CommandName == "eliminarSancion")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpcionPorDefecto", "deshabilitarPanel(); limpiarCombos(); equipoYSinDefinir();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarSancion');", true);  
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }  
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorSancion.eliminarSancion(gestorSancion.sancion.idSancion);
                cargarRepeaterSanciones(gestorEdicion.edicion.idEdicion.ToString());                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarSancion", "closeModal('eliminarSancion');", true);                
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        protected void btnModificarSancion_Click(object sender, EventArgs e)
        {
            try
            {
                int idSancionAModificar = gestorSancion.sancion.idSancion;
                if (gestorSancion.sancion.idJugador != null && gestorSancion.sancion.idPartido != null)
                    gestorSancion.modificarSancion(idSancionAModificar.ToString(), ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value);
                if (gestorSancion.sancion.idJugador == null && gestorSancion.sancion.idPartido == null)
                    gestorSancion.modificarSancion(idSancionAModificar.ToString(), null, null, ddlEquipoSinPartido.SelectedValue, null, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value);            
                
                if(gestorSancion.sancion.idJugador == null && gestorSancion.sancion.idPartido != null)
                    gestorSancion.modificarSancion(idSancionAModificar.ToString(), ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, null, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value);
                if (gestorSancion.sancion.idJugador != null && gestorSancion.sancion.idPartido == null)
                    gestorSancion.modificarSancion(idSancionAModificar.ToString(), null, null, ddlEquipoSinPartido.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, null, txtCantidadFechasSuspendidas.Value);
                gestorSancion.sancion = null;
                cargarRepeaterSanciones(gestorEdicion.edicion.idEdicion.ToString());
                btnRegistrarSancion.Visible = true;
                btnModificarSancion.Visible = false;
                btnCancelarModificacionSancion.Visible = false;
                limpiarCampos();
                habilitarCombos();
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        protected void btnCancelarModificacionSancion_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCampos();
                habilitarCombos();
                btnRegistrarSancion.Visible = true;
                btnModificarSancion.Visible = false;
                btnCancelarModificacionSancion.Visible = false;
                gestorSancion.sancion = null;
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); } 
        }

        public void limpiarCampos()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpcionPorDefecto", "deshabilitarPanel(); limpiarCombos(); equipoYSinDefinir();", true);
            cargarComboEquipos();
            cargarComboFechas();
            cargarComboMotivos();
            txtCantidadFechasSuspendidas.Value = "";
            txtFecha.Value = "";
            txtObservacion.Value = "";
            txtPuntosAQuitar.Value = "";
        }

        public void habilitarCombos()
        {
            ddlEquipoSinPartido.Enabled = true;
            ddlEquipo.Enabled = true;
            ddlFecha.Enabled = true;
            ddlPartido.Enabled = true;
            ddlJugador.Enabled = true;
        }
    }
}