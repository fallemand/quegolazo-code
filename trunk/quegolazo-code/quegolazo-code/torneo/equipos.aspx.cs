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
                    Torneo torneo = GestorUrl.validarTorneo();
                    Edicion edicion = GestorUrl.validarEdicion(torneo.nick);
                    gestorEstadistica = new GestorEstadisticas(edicion);
                    nickTorneo = torneo.nick;
                    GestorControles.cargarRepeaterTable(rptEquipos, gestorEstadistica.tablaPosicionesEdicion());
                    gestorEdicion = new GestorEdicion();
                    gestorEdicion.edicion = edicion;
                    idEdicion = edicion.idEdicion;
                    gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();


                    gestorPartido = new GestorPartido();
                    

               
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        protected void rptEquipos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                bool tieneImagen= new GestorEquipo().obtenerEquipoPorId(int.Parse(row["idEquipo"].ToString())).tieneImagen();
                Panel divCamiseta = (Panel)e.Item.FindControl("panelCamiseta");
                Panel imgEquipo = (Panel)e.Item.FindControl("panelImagen");
                divCamiseta.Visible = !tieneImagen;
                imgEquipo.Visible = tieneImagen;
                
                
            }
        }

    }
}