﻿using System;
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
            gestorFase = Sesion.getGestorFase();
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
                rptFases.DataSource = gestorFase.fases;
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
                gestorEdicion.edicion.fases = gestorFase.fases;
                if (gestorEdicion.edicion.estado.idEstado == Estado.REGISTRADA)
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
                Literal litCantEquipo = (Literal)e.Item.FindControl("litCantidadEquipos");
                Literal litCantEquipoGrupo = (Literal)e.Item.FindControl("litCantidadEquiposGrupo");
                litCantEquipo.Text = gestorEdicion.edicion.equipos.Count.ToString();
                litCantEquipoGrupo.Text = (gestorEdicion.edicion.equipos.Count / ((Fase)e.Item.DataItem).grupos.Count).ToString();
            }
        }
    }
}