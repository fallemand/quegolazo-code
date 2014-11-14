using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using System.Globalization;
using Utils;

namespace quegolazo_code.admin
{
    public partial class index : System.Web.UI.Page
    {
        GestorEdicion gestorEdicion = new GestorEdicion();
        GestorEstadisticas gestorEstadisticas;
        protected void Page_Load(object sender, EventArgs e)
        {
            panelFracaso.Visible = false;
            try
            {
                gestorEdicion = Sesion.getGestorEdicion();
                gestorEstadisticas = new GestorEstadisticas();               
                gestorEdicion.edicion = Sesion.getEdicion();
                if (!Page.IsPostBack)
                {
                    obtenerEdiciónSeleccionada();
                    cargarComboEdiciones();
                    cargarTablaDePosiciones();
                    cargarGoleadoresDeLaEdicion();
                    cargarPorcentajeDeAvanceDeLaFecha();
                    cargarPorcentajeDeAvanceEdicion();
                    cargarUltimaFecha();                    
                }
             }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Obtiene la Edición de Sesión
        /// autor: Facu Allemand
        /// </summary>
        private void obtenerEdiciónSeleccionada()
        {
            if (gestorEdicion.edicion != null && gestorEdicion.edicion.idEdicion > 0)
            {
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(gestorEdicion.edicion.idEdicion);
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
            }
        }

        /// <summary>
        /// Carga Combo Ediciones
        /// </summary>
        private void cargarComboEdiciones()
        {
            GestorControles.cargarComboList(ddlEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo),
                "idEdicion", "nombre", "Seleccionar Edicion", false);
            ddlEdiciones.SelectedValue = (gestorEdicion.edicion.idEdicion > 0) ? 
                gestorEdicion.edicion.idEdicion.ToString() : "";
        }

        /// <summary>
        /// Carga todas las referencias de una edición en sesión, y carga todas las fases
        /// autor: Facu Allemand
        /// </summary>
        protected void btnSeleccionarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                int idEdicion = Validador.castInt(ddlEdiciones.SelectedValue);
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
                gestorEdicion.edicion.preferencias = gestorEdicion.obtenerPreferencias();
                gestorEdicion.edicion.equipos = gestorEdicion.obtenerEquipos();
                gestorEdicion.edicion.fases = gestorEdicion.obtenerFases();

                cargarTablaDePosiciones();
                cargarGoleadoresDeLaEdicion();
                cargarPorcentajeDeAvanceDeLaFecha();
                cargarPorcentajeDeAvanceEdicion();
                cargarUltimaFecha();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Carga la tabla de posiciones de la edicion que esta en la sesion.
        /// </summary>
        private void cargarTablaDePosiciones()
        {
            rptPosiciones.DataSource = gestorEstadisticas.obtenerTablaPosiciones();
            rptPosiciones.DataBind();
            sinequipos.Visible = (rptPosiciones.Items.Count > 0) ? false : true;
        }
        /// <summary>
        /// Carga la ultima fecha incompleta de la edicion que esta en sesion
        /// </summary>
        private void cargarUltimaFecha()
        {
            var ultimaFecha = gestorEstadisticas.obtenerFixtureUltimaFecha(Entidades.Estado.fechaINCOMPLETA);
            rptFecha.DataSource = ultimaFecha;
            rptFecha.DataBind();
            if (rptFecha.Items.Count == 0)
            {
                ultimaFecha = gestorEstadisticas.obtenerFixtureUltimaFecha(Entidades.Estado.fechaCOMPLETA);
                rptFecha.DataSource = ultimaFecha;
                rptFecha.DataBind();
            }

            ltFecha.Text = ultimaFecha.Rows[0]["idFecha"].ToString();
            noFixture.Visible = (rptFecha.Items.Count > 0) ? false : true;
            
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
            sinpartidosGoleadores.Visible = GestorControles.cargarRepeaterTable(rptGoleadores, gestorEstadisticas.obtenerTablaGoleadores()) ? 
                false : true;
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