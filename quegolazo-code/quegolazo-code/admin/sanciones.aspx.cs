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

namespace quegolazo_code.admin
{
    public partial class sanciones : System.Web.UI.Page
    {
        GestorSancion gestorSancion;
        GestorEdicion gestorEdicion;
        GestorFase gestorFase;        
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorSancion = Sesion.getGestorSancion();
            gestorEdicion = Sesion.getGestorEdicion();
            gestorFase = Sesion.getGestorFase();
            try
            {
                if (!Page.IsPostBack)
                {
                    //limpiarPaneles();
                    cargarComboEdiciones(); 
                }
            }
            catch (Exception ex)
            {
 
            }
            //{ mostrarPanelFracasoListaArbitros(ex.Message); }
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
                int idEdicion = Validador.castInt(ddlEdiciones.SelectedValue);
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                ////HAY QUE CAMBIARLO. TIENE QUE TRAER LA FASE ACTUAL
                gestorEdicion.gestorFase.faseActual = gestorEdicion.edicion.fases[0];                
                cargarComboFechas();
                cargarComboMotivos();
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

        protected void ddlFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPartido.Items.Clear();
            GestorControles.cargarComboList(ddlPartido, gestorSancion.obtenerPartidosDeFecha(ddlFecha.SelectedValue),
                "idPartido", "nombreCompleto", "Seleccionar Partido", false);
        }

        protected void ddlPartido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEquipo.Items.Clear();
            GestorControles.cargarComboList(ddlEquipo, gestorSancion.obtenerEquiposDePartido(ddlPartido.SelectedValue),
                "idEquipo", "nombre", "Seleccionar Equipo", false);
        }

        protected void ddlEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlJugador.Items.Clear();
            GestorControles.cargarComboList(ddlJugador, gestorSancion.obtenerJugadoresDeEquipo(ddlEquipo.SelectedValue),
                "idJugador", "nombre", "Seleccionar Jugador", false);
        }        
    }
}