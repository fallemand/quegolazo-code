using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web16 : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;    
        GestorEstadisticas gestorEstadistica;
        protected int idEdicion, idEquipo;
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
                    gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                    idEdicion = edicion.idEdicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();

                    gestorEstadistica = new GestorEstadisticas(edicion);
                    gestorEstadistica.edicion = edicion;

                  
                    GestorControles.cargarRepeaterList(rptCanchas, new GestorCancha().obtenerCanchasDeUnTorneoPorId(torneo.idTorneo));
                    GestorControles.cargarRepeaterTable(rptAribitros, gestorEstadistica.estadisticasDeArbitro(torneo.idTorneo));
                    msjFairPLay.Visible = !GestorControles.cargarRepeaterTable(rptFairPlay, gestorEstadistica.rankingFairPlay());
                    msjValla.Visible = !GestorControles.cargarRepeaterTable(rptVallaMenosVencida, gestorEstadistica.vallaMenosVencida()); 
              
                 }
            }
            catch (Exception ex) { 
                GestorError.mostrarPanelFracaso(ex.Message);
            }
        

        }






    }
 }
