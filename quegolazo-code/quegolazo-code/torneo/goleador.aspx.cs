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
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion; 
        private List<Jugador> goleadoresDelEquipo;
        private DataTable datosPrincipalesEquipo;        
        GestorEstadisticas gestorEstadistica;
        protected int idEdicion, idEquipo;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorTorneo = Sesion.getGestorTorneo();
                gestorEdicion = Sesion.getGestorEdicion();
                //TODO falta agregarle el try/catch y que redirija a una pagina de error...
                idEdicion = int.Parse(Request["idEdicion"]);
                nickTorneo = Request["nickTorneo"];
                gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(idEdicion);
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
                gestorEstadistica = new GestorEstadisticas();
                if (!Page.IsPostBack)
                {
                    GestorEquipo gestorEsuipo = new GestorEquipo();
                    gestorEsuipo.equipo = gestorEsuipo.obtenerEquipoPorId(11);
                    GestorControles.cargarRepeaterList(rptGoleadores, gestorEsuipo.equipo.jugadores);
                }

            }
            catch (Exception)
            {
                //TODO redireccionar a pagina de error
                throw;
            }

        }

        protected void rptGoleadores_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

    }
}