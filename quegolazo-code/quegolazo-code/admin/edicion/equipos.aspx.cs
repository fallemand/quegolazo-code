using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;

namespace quegolazo_code.admin.edicion
{
    public partial class equipos : System.Web.UI.Page
    {
        GestorEquipo gestorEquipo = null;
        GestorEdicion gestorEdicion = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //si no existe gestor en Session lo carga
            if (Session["gestorEquipo"] == null)
                Session["gestorEquipo"] = new GestorEquipo();
            //obtiene en gestor de la Session.
            gestorEquipo = (GestorEquipo)Session["gestorEquipo"];

            //si no existe gestor en Session lo carga
            if (Session["gestorEdicion"] == null)
                Session["gestorEdicion"] = new GestorEdicion();
            //obtiene en gestor de la Session.
            gestorEdicion = (GestorEdicion)Session["gestorEdicion"];

            //TORNEO HARDCODEADO
            //TORNEO HARDCODEADO
            Session["torneo"] = new Torneo
            {
                idTorneo = 88,
            };
            //TORNEO HARDCODEADO
            //TORNEO HARDCODEADO

            //EDICION HARDCODEADA
            //EDICION HARDCODEADA
            Session["edicion"] = new Edicion
            {
                idEdicion = 14,
            };
            //EDICION HARDCODEADA
            //EDICION HARDCODEADA

            cargarEquipos();
        }

        public void cargarEquipos()
        {
            lstEquiposSeleccionados.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo();
            lstEquiposSeleccionados.DataValueField = "idEquipo";
            lstEquiposSeleccionados.DataTextField = "nombre";
            lstEquiposSeleccionados.DataBind();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            //int cant = ddlEquiposSeleccionados.Items.Count;
            foreach (ListItem item in lstEquiposSeleccionados.Items)
            {
                if (item.Selected)
                {
                    if (gestorEdicion.edicion == null)
                        gestorEdicion.edicion = new Edicion();
                    gestorEdicion.edicion.equipos.Add(new Equipo { idEquipo = Int32.Parse(item.Value) });
                }
            }
        }
    }
}