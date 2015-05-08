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
        GestorEquipo gestorEquipo=new GestorEquipo();
        GestorEdicion gestorEdicion = new GestorEdicion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo = Sesion.getGestorEquipo();
                gestorEdicion = Sesion.getGestorEdicion();
                limpiarPaneles();
                if (!Page.IsPostBack)
                    cargarEquipos();
            }
            catch (Exception ex) {GestorError.mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Volver al paso anterior
        /// </summary>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eCONFIGURAR);
        }

        /// <summary>
        /// Carga los equipos seleccionados para la edición
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfEquiposSeleccionados.Value == "")
                    throw new Exception("No hay equipos seleccionados");
                if (gestorEdicion.edicion.equipos != null && gestorEdicion.edicion.equipos.Count > 1)
                    gestorEdicion.verificarCambiosDeEquipos(hfEquiposSeleccionados.Value);                
                gestorEdicion.agregarEquiposEnEdicion(hfEquiposSeleccionados.Value);
                //se limpian las fases que hayan sido generadas anteriormente
                //gestorEdicion.edicion.fases = new List<Fase>();
                Sesion.setGestorFase(new GestorFase());
                Response.Redirect(GestorUrl.eFASES);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Modificación de equipos!!!")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modificarEquipos');", true);
                else
                    GestorError.mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Modifica los Equipos seleccionados de la edición
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modificarEquipos", "closeModal('modificarEquipos');", true);
                gestorEdicion.agregarEquiposEnEdicion(hfEquiposSeleccionados.Value);
                new GestorFase().eliminarConfiguracionGuardada(gestorEdicion.edicion.fases);
                Response.Redirect(GestorUrl.eFASES);
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// -------------------------------------------------------------------------
        /// --------------------------Metodos Extra---------------------------------
        /// -------------------------------------------------------------------------
        /// </summary>

        /// <summary>
        /// Limpiar panel de error
        /// autor: Facundo Allemand
        /// </summary>
        private void limpiarPaneles()
        {
            panelFracaso.Visible = false;
            litFracaso.Text = "";
        }

        /// <summary>
        /// Carga los equipos
        /// autor: Facundo Allemand
        /// </summary>
        public void cargarEquipos()
        {
            lstEquiposSeleccionados.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo(Sesion.getTorneo().idTorneo);
            lstEquiposSeleccionados.DataValueField = "idEquipo";
            lstEquiposSeleccionados.DataTextField = "nombre";
            lstEquiposSeleccionados.DataBind();
            List<int> equipos = new List<int>();
            if (gestorEdicion.edicion.equipos.Count > 0)
            {
                foreach (Equipo equipo in gestorEdicion.edicion.equipos)
                    equipos.Add(equipo.idEquipo);

                foreach (ListItem item in lstEquiposSeleccionados.Items)
                {
                    if (equipos.Contains(int.Parse(item.Value)))
                    {
                        item.Attributes.Add("selected", "selected");

                    }
                }
                string equiposInt = string.Join(",", equipos.ToArray());
                equiposInt += ",";
                hfEquiposSeleccionados.Value = equiposInt;
            }
        }
    }
}