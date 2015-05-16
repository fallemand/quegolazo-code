using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;
using Logica;
using Entidades;

namespace quegolazo_code.torneo
{
    public partial class posiciones : System.Web.UI.Page
    {
        protected GestorEdicion gestorEdicion;
        protected GestorTorneo gestorTorneo;
        protected GestorEquipo gestorEquipo = new GestorEquipo();
        protected int idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);

                    gestorTorneo = new GestorTorneo();
                    gestorTorneo.torneo = torneo;
                    nickTorneo = torneo.nick;

                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                    idEdicion = edicion.idEdicion;

                    if (gestorEdicion.edicion.fases.Count > 0)
                        cargarEquipos();
                    else
                        sinEquipos.Visible = true;
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        public void cargarEquipos()
        {
            gestorEdicion.getFaseActual();
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas(gestorEdicion.edicion);
            GestorControles.cargarRepeaterList(rptGrupos, gestorEdicion.edicion.fases[gestorEdicion.faseActual.idFase - 1].grupos);
            sinEquipos.Visible = !GestorControles.cargarRepeaterTable(rptEquipos, gestorEstadisticas.obtenerTablaPosiciones(gestorEdicion.faseActual.idFase));
            GestorControles.cargarRepeaterList(rptListaEquipos, gestorEdicion.edicion.equipos);
        }
    }
}