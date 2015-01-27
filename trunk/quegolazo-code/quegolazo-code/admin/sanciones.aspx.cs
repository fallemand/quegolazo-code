﻿using System;
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
                gestorEdicion.gestorFase.faseActual = gestorEdicion.edicion.fases[0];
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
            GestorControles.cargarComboList(ddlFecha, gestorEdicion.gestorFase.faseActual.obtenerFechas(),
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
                    throw new Exception("\nDebe ingresar la fecha de registro de la sanción"); 
                if (ddlMotivo.SelectedValue.Equals(string.Empty))
                    throw new Exception("\nDebe seleccionar un motivo");                              

                if(rdEquipos.Checked && rdSinDefinir.Checked) //CASO MÁS SIMPLE
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, string.Empty, string.Empty, ddlEquipoSinPartido.SelectedValue, string.Empty, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.gestorFase.faseActual.idFase.ToString());
                if(rdEquipos.Checked && rdPartido.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, string.Empty, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.gestorFase.faseActual.idFase.ToString());
                if(rdJugadores.Checked && rdSinDefinir.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, string.Empty, string.Empty, ddlEquipoSinPartido.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.gestorFase.faseActual.idFase.ToString());
                if(rdJugadores.Checked && rdPartido.Checked)
                    gestorSancion.registrarSancion(ddlEdiciones.SelectedValue, ddlFecha.SelectedValue, ddlPartido.SelectedValue, ddlEquipo.SelectedValue, ddlJugador.SelectedValue, txtFecha.Value, ddlMotivo.SelectedValue, txtObservacion.Value, txtPuntosAQuitar.Value, txtCantidadFechasSuspendidas.Value, gestorEdicion.gestorFase.faseActual.idFase.ToString());
                 
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
                mostrarPanelFracaso(ex.Message);
                rdEquipos.Checked = true;
                rdSinDefinir.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "equipoYSinDefinir()", "equipoYSinDefinir();", true);
            }
        }

        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
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
    }
}