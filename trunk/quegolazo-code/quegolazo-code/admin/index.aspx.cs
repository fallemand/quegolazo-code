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
        GestorEstadisticas gestorEstadisticas;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEstadisticas = new GestorEstadisticas();
            //Sesion.setEdicion(gestorEdicion.obtenerEdicionPorId(14));
            if (!Page.IsPostBack)
            {
                try
                {
                    //gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(14);                    
                    //Sesion.setGestorEdicion(gestorEdicion);                    
                    rptPosiciones.DataSource = gestorEstadisticas.obtenerTablaPosiciones();
                    rptPosiciones.DataBind();
                    sinequipos.Visible = (rptPosiciones.Items.Count > 0) ? false : true;
                    cargarUltimaFecha();
                    cargarPorcentajeDeAvanceDeLaFecha();
                    cargarPorcentajeDeAvanceEdicion();                   
                    cargarGoleadoresDeLaEdicion();
                }
                catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
            }
        }

        private void cargarUltimaFecha()
        {
            var ultimaFecha = gestorEstadisticas.obtenerFixtureUltimaFecha(Entidades.Estado.DIAGRAMADA);
            rptFecha.DataSource = ultimaFecha;
            rptFecha.DataBind();
            ltFecha.Text = ultimaFecha.Rows[0]["idFecha"].ToString();
            
        }
        /// <summary>
        /// Carga el porcetnaje de avance de la ultima fecha incompleta
        /// </summary>
        private void cargarPorcentajeDeAvanceDeLaFecha()
        {
            double avance = 0;
            var valores = gestorEstadisticas.obtenerAvanceFecha(9);
            try { avance = double.Parse(valores.Rows[0]["porcentajeAvance"].ToString()); }
            catch (IndexOutOfRangeException) { }           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AvanceFecha", "$('#avanceFecha').percentageLoader({ width : 180, height : 180, progress :" + (avance / 100).ToString("0.00", CultureInfo.InvariantCulture) + ", value : ''});", true);
           
        }

        /// <summary>
        /// Carga el porcentaje de avance de la edicion que esta en sesion en la pantalla usando el plugin "pecentageLoader"
        /// </summary>
        private void cargarPorcentajeDeAvanceEdicion() {
            double avance = 0;
            var valores = gestorEstadisticas.obtenerAvanceEdicion(); 
            try{avance = double.Parse(valores.Rows[0]["porcentajeAvance"].ToString());}
            catch (IndexOutOfRangeException){}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AvanceEdicion", "$('#avanceTorneo').percentageLoader({ width : 180, height : 180, progress :" + (avance / 100).ToString("0.00", CultureInfo.InvariantCulture) + ", value : ''});", true);
           
        }

        /// <summary>
        /// Carga la tabla de goleadores de la edicion que esta en sesion.
        /// </summary>
        private void cargarGoleadoresDeLaEdicion() {
            rptGoleadores.DataSource = gestorEstadisticas.obtenerTablaGoleadores();
            rptGoleadores.DataBind();
            sinpartidosGoleadores.Visible = (rptPosiciones.Items.Count > 0) ? false : true;
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