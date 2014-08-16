using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class equipos : System.Web.UI.Page
    {
        GestorEquipo gestorEquipo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //si no existe gestor en Session lo carga
            if (Session["gestorEquipo"] == null)
                Session["gestorEquipo"] = new GestorEquipo();
            //obtiene en gestor de la Session.
            gestorEquipo = (GestorEquipo)Session["gestorEquipo"];

            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            Session["torneo"] = new Torneo
            {
                idTorneo = 88,
            };
            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            try
            {
                cargarRepeaterEquipos();
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaEquipos(ex.Message);
            }                        
            limpiarPaneles();
        }     

        /// <summary>
        /// Registra un nuevo equipo en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarEquipo_Click(object sender, EventArgs e)
        {
            try
            {               
                gestorEquipo.registrarEquipo(txtNombreEquipo.Value, txtColorPrimario.Value, txtColorSecundario.Value, txtNombreDirector.Value);
                GestorImagen.guardarImagen(gestorEquipo.equipo.idEquipo, GestorImagen.EQUIPO);
                limpiarCamposEquipo();
                mostrarPanelExito("Equipo registrado con éxito!");
                cargarRepeaterEquipos();
                gestorEquipo.equipo = null; // le setea null al equipo
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);            
            }
        }

        /// <summary>
        /// Agrega un delegado a una lista genérica de delegados
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnAgregarDelegado_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo.agregarDelegado(txtNombreDelegado.Value, txtEmailDelegado.Value, txtTelefonoDelegado.Value, txtDireccionDelegado.Value);
                limpiarCamposDelegado();
                cargarRepeaterDelegados();
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Solo puede cargar dos delegados"))
                    limpiarCamposDelegado();
                if (ex.Message.Contains("Ya existe un delegado con ese nombre"))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDelegados();", true);
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Modificar un delegado
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnModificarDelegado_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo.modificarDelegado(txtNombreDelegado.Value, txtEmailDelegado.Value, txtTelefonoDelegado.Value, txtDireccionDelegado.Value);
                Session["delegadoAModificar"] = null;
                cargarRepeaterDelegados();
                limpiarCamposDelegado();    
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Ya existe un delegado con ese nombre"))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDelegados();", true);
                mostrarPanelFracaso(ex.Message);
            }
        }
      
        /// <summary>
        /// Método para gestionar la eliminación y modificación de los delegados
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptDelegados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                //Si elimina el delegado
                if (e.CommandName == "eliminarDelegado")
                {   //por CommandArgument recibe el NOMBRE del delegado a eliminar
                    string nombre = e.CommandArgument.ToString();
                    gestorEquipo.eliminarDelegado(nombre);
                    cargarRepeaterDelegados();
                }
                //Si modifica el delegado
                if (e.CommandName == "modificarDelegado")
                {   //por CommandArgument recibe el NOMBRE del delegado a modificar
                    string nombre = e.CommandArgument.ToString();
                    Delegado delegado = gestorEquipo.obtenerDelegadoPorNombre(nombre);
                    Session["delegadoAModificar"] = delegado;
                    txtNombreDelegado.Value = delegado.nombre;
                    txtTelefonoDelegado.Value = delegado.telefono;
                    txtDireccionDelegado.Value = delegado.domicilio;
                    txtEmailDelegado.Value = delegado.email;
                    btnAgregarDelegado.Visible = false;
                    btnModificarDelegado.Visible = true;
                    btnCancelarDelegado.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDelegados();", true);
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }            
        }     

        /// <summary>
        /// Permite editar un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptEquipos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "editarEquipo")
            {   //por CommandArgument recibe el ID del equipo a modificar               
                gestorEquipo.obtenerEquipoAModificar(Int32.Parse(e.CommandArgument.ToString()));                
                txtNombreEquipo.Value = gestorEquipo.equipo.nombre;
                txtNombreDirector.Value = gestorEquipo.equipo.directorTecnico;
                txtColorPrimario.Value = gestorEquipo.equipo.colorCamisetaPrimario;
                txtColorSecundario.Value = gestorEquipo.equipo.colorCamisetaSecundario;
                rptDelegados.DataSource = gestorEquipo.obtenerDelegados();
                rptDelegados.DataBind();
                btnRegistrarEquipo.Visible = false;
                btnModificarEquipo.Visible = true;
                btnCancelarModificacionEquipo.Visible = true;
                imagenpreview.Src = gestorEquipo.equipo.obtenerImagenMediana();              
            } 
        }

        /// <summary>
        /// Modifica el equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                int idEquipoAModificar = gestorEquipo.equipo.idEquipo;                
                gestorEquipo.modificarEquipo(idEquipoAModificar, txtNombreEquipo.Value, txtColorPrimario.Value, txtColorSecundario.Value, txtNombreDirector.Value);
                GestorImagen.guardarImagen(gestorEquipo.equipo.idEquipo, GestorImagen.EQUIPO);
                limpiarCamposEquipo();
                limpiarCamposDelegado();
                mostrarPanelExito("Equipo modificado con éxito!");
                cargarRepeaterEquipos();
                gestorEquipo.equipo = null; // le setea null al equipo  
                //lo manda a la solapa de agregar un equipo
                btnRegistrarEquipo.Visible = true;
                btnModificarEquipo.Visible = false;
                btnCancelarModificacionEquipo.Visible = false;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Cancelar la carga de delegado
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnCancelarDelegado_Click(object sender, EventArgs e)
        {
            limpiarCamposDelegado();
        }

        /// <summary>
        /// Cancelar la Modificación de un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionEquipo_Click(object sender, EventArgs e)
        {
            limpiarCamposEquipo();
            limpiarCamposDelegado();
            btnRegistrarEquipo.Visible = true;
            btnModificarEquipo.Visible = false;
            btnCancelarModificacionEquipo.Visible = false;
            gestorEquipo.equipo = null; // le setea null al equipo 
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de Equipos
        /// autor: Pau Pedrosa
        /// </summary>
        private void cargarRepeaterEquipos()
        {
            rptEquipos.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo();
            rptEquipos.DataBind();
            sinequipos.Visible = (rptEquipos.Items.Count > 0) ? false : true;
        }

        /// <summary>
        /// Permite cargar el repeater de los delegados con el nombre del delegado
        /// autor: Pau Pedrosa
        /// </summary>             
        private void cargarRepeaterDelegados()
        {
            rptDelegados.DataSource = gestorEquipo.obtenerDelegados();
            rptDelegados.DataBind();
        }

        /// <summary>
        /// Permite ir a agregar un nuevo equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void lnkNuevoEquipo_Click(object sender, EventArgs e)
        {
            limpiarCamposEquipo();
            limpiarCamposDelegado();
            btnRegistrarEquipo.Visible = true;
            btnModificarEquipo.Visible = false;
            btnCancelarModificacionEquipo.Visible = false;
        }

        /// <summary>
        /// limpia los campos del alta de delegado
        /// autor: Pau Pedrosa
        /// </summary>
        public void limpiarCamposDelegado()
        {
            txtNombreDelegado.Value = "";
            txtEmailDelegado.Value = "";
            txtTelefonoDelegado.Value = "";
            txtDireccionDelegado.Value = "";
            btnAgregarDelegado.Visible = true;
            btnModificarDelegado.Visible = false;
            btnCancelarDelegado.Visible = true;
        }             

        /// <summary>
        /// Limpia los campos del alta de equipo
        /// autor: Pau Pedrosa
        /// </summary>
        public void limpiarCamposEquipo()
        {
            txtNombreEquipo.Value = "";
            txtNombreDirector.Value = "";
            txtColorPrimario.Value = "#E1E1E1";
            txtColorSecundario.Value = "#E1E1E1";
            limpiarCamposDelegado();
            rptDelegados.DataSource = null;
            rptDelegados.DataBind();
        }

        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// autor: Pau Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            panelFracasoListaEquipos.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
            litFracasoListaEquipos.Text = "";
        }

        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelExito.Visible = false;
            panelFracaso.Visible = true;
        }

        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelFracasoListaEquipos(string mensaje)
        {
            litFracasoListaEquipos.Text = mensaje;
            panelFracasoListaEquipos.Visible = true;
        }

        /// <summary>
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelExito(string mensaje)
        {
            litExito.Text = mensaje;
            panelExito.Visible = true;
            panelFracaso.Visible = false;
        }        
    }
}