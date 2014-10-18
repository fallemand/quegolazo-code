using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                try
                {
                    GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
                    gvPosiciones.DataSource = gestorEstadisticas.obtenerTablaPosiciones();
                    gvPosiciones.DataBind();
                    gvFixture.DataSource = gestorEstadisticas.obtenerFixtureUltimaFecha(8);
                    gvFixture.DataBind();
                   // LitEdicion.Text = Sesion.getEdicion().nombre;
                }
                catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
            }
        }

        /// <summary>
        /// Habilita el panel de fracaso, y muestra el mensaje.
        /// autor: Flor Rojas
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelFracaso.Visible = true;
        }
    }
}