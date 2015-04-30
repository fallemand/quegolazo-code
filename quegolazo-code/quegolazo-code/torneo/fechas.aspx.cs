using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web13 : System.Web.UI.Page
    {
        protected int idEdicion, idFase, idGrupo, idFecha;
        protected string nickTorneo;
        protected GestorTorneo gestorTorneo;
        protected GestorEdicion gestorEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                idEdicion = int.Parse(Request["idEdicion"]);
                nickTorneo = Request["nickTorneo"].ToString();
                idFase =(Request["idFase"]!=null) ? int.Parse(Request["idFase"]):0;
                idGrupo = (Request["idFase"] != null) ? int.Parse(Request["idGrupo"]) : 0;
                idFecha = (Request["idFase"] != null) ? int.Parse(Request["idFecha"]) : 0;
            }
            catch (Exception)
            {
                //TODO redireccionar a pagina de error
                throw;
            }
            gestorTorneo = Sesion.getGestorTorneo();
            gestorEdicion = Sesion.getGestorEdicion();
            gestorEdicion.edicion = new GestorEdicion().obtenerEdicionPorId(idEdicion);//2010
            gestorTorneo.torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo); //jockeyClub
            
        }
    }
}