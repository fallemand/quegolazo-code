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
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eCONFIGURAR);
        }

        public void cargarEquipos()
        {
            lstEquiposSeleccionados.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo();
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
        
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if(gestorEdicion.edicion.estado.idEstado==Estado.CONFIGURADA)
                    gestorEdicion.verificarCambiosDeEquipos(hfEquiposSeleccionados.Value);
                gestorEdicion.agregarEquiposEnEdicion(hfEquiposSeleccionados.Value);
                Response.Redirect(GestorUrl.eFASES);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Modificación de equipos!!!")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modificarEquipos');", true);                   
                    
                }
                else
                    mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Habilita el panel de fracaso y muestra el error
        /// autor: Facundo Allemand
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            panelFracaso.Visible = true;
            litFracaso.Text = mensaje;
        }

        /// <summary>
        /// Limpiar panel de error
        /// autor: Facundo Allemand
        /// </summary>
        private void limpiarPaneles()
        {
            panelFracaso.Visible = false;
            litFracaso.Text = "";
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modificarEquipos", "closeModal('modificarEquipos');", true);
                gestorEdicion.agregarEquiposEnEdicion(hfEquiposSeleccionados.Value);
                gestorEdicion.edicion.fases = null;
                Response.Redirect(GestorUrl.eFASES);
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }

        }
    }
}