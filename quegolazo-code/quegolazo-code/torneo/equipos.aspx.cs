using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;
using Utils;
using System.Data;
using System.Web.UI.HtmlControls;

namespace quegolazo_code.torneo
{
        
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;
        protected GestorPartido gestorPartido;
        protected Torneo torneo;
        GestorEstadisticas gestorEstadistica;
        protected int idEquipo, idEdicion;
        protected string nickTorneo;
        protected Equipo equipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    gestorTorneo = new GestorTorneo();
                    gestorEdicion = new GestorEdicion();
                    torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);
                    gestorEstadistica = new GestorEstadisticas(edicion);
                    nickTorneo = torneo.nick;
                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    idEdicion = edicion.idEdicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                    gestorPartido = new GestorPartido();   
                    sinEquipos.Visible = !GestorControles.cargarRepeaterTable(rptEquipos, gestorEstadistica.tablaPosicionesEdicion());                     
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }


    }
}