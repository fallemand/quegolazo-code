using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using System.Globalization;

namespace quegolazo_code.admin
{
    public partial class index : System.Web.UI.Page
    {
        GestorEdicion gestorEdicion = new GestorEdicion();
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion.setEdicion(gestorEdicion.obtenerEdicionPorId(14));
            if (!Page.IsPostBack)
            {
                try
                {
                    gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(14);                    
                    Sesion.setGestorEdicion(gestorEdicion);
                    GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
                    gvPosiciones.DataSource = gestorEstadisticas.obtenerTablaPosiciones();
                    gvPosiciones.DataBind();
                    gvFixture.DataSource = gestorEstadisticas.obtenerFixtureUltimaFecha(8);
                    gvFixture.DataBind();
                    cargarPorcentajeDeAvanceEdicion();
                }
                catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
            }
        }
        /// <summary>
        /// Carga el porcentaje de avance de la edicion en la pantalla usando el plugin "pecentageLoader"
        /// </summary>
        private void cargarPorcentajeDeAvanceEdicion() {
            GestorEstadisticas gestorEstadisticas = new GestorEstadisticas();
            var valores = gestorEstadisticas.obtenerAvanceEdicion();
            var otro = gestorEstadisticas.obtenerAvanceFecha(8);
            double avance = double.Parse(valores.Rows[0]["porcentajeAvance"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#avanceTorneo').percentageLoader({ width : 180, height : 180, progress :" + (avance / 100).ToString("0.00", CultureInfo.InvariantCulture) + ", value : ''});", true);
           
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