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
          
                gestorEquipo = Sesion.getGestorEquipo();
                gestorEdicion = Sesion.getGestorEdicion();
                limpiarPaneles();
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
            try
            {
                gestorEdicion.agregarEquiposEnEdicion(hfEquiposSeleccionados.Value);
                Response.Redirect(GestorUrl.eFASES);
            }
            catch (Exception ex)
            {
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
    }
}