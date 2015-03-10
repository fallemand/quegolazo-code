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
        private GestorEquipo gestorEquipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo = Sesion.getGestorEquipo();
                limpiarPaneles();
                cargarRepeaterEquipos();
            }
            catch (Exception ex) {mostrarPanelFracaso(ex.Message);}                        
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
                cargarRepeaterEquipos();
                gestorEquipo.equipo = null; // le setea null al equipo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarEquipo');", true);
            }
            catch (Exception ex)
            {
                imagenpreview.Src = GestorImagen.obtenerImagenTemporal(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
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
        /// Permite editar y eliminar un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptEquipos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
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

                if (e.CommandName == "eliminarEquipo")
                {//por CommandArgument recibe el ID del equipo a eliminar   
                    gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(int.Parse(e.CommandArgument.ToString()));
                    litNombreEquipo.Text = gestorEquipo.equipo.nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarEquipo');", true);
                }
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }  
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
                cargarRepeaterEquipos();
                gestorEquipo.equipo = null; // le setea null al equipo  
                //lo manda a la solapa de agregar un equipo
                btnRegistrarEquipo.Visible = true;
                btnModificarEquipo.Visible = false;
                btnCancelarModificacionEquipo.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarEquipo');", true);
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Cancelar la carga de delegado
        /// autor: Facundo Allemand
        /// </summary>
        protected void btnCancelarDelegado_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposDelegado();
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Cancelar la Modificación de un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposEquipo();
                limpiarCamposDelegado();
                btnRegistrarEquipo.Visible = true;
                btnModificarEquipo.Visible = false;
                btnCancelarModificacionEquipo.Visible = false;
                gestorEquipo.equipo = null; // le setea null al equipo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarEquipo');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Elimina un Equipo de la Bd
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo.eliminarEquipo(gestorEquipo.equipo.idEquipo);
                cargarRepeaterEquipos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarEquipo", "closeModal('eliminarEquipo');", true);
            }
            catch (Exception ex) { mostrarPanelFracaso(ex.Message);}
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de Equipos
        /// </summary>
        private void cargarRepeaterEquipos()
        {
            sinequipos.Visible = !GestorControles.cargarRepeaterList(rptEquipos, gestorEquipo.obtenerEquiposDeUnTorneo());
        }
        /// <summary>
        /// Permite cargar el repeater de los delegados con el nombre del delegado
        /// </summary>             
        private void cargarRepeaterDelegados()
        {
            GestorControles.cargarRepeaterList(rptDelegados, gestorEquipo.obtenerDelegados());
        }
        /// <summary>
        /// limpia los campos del alta de delegado
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
        /// </summary>
        public void limpiarCamposEquipo()
        {
            txtNombreEquipo.Value = "";
            txtNombreDirector.Value = "";
            txtColorPrimario.Value = "#E1E1E1";
            txtColorSecundario.Value = "#E1E1E1";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
            limpiarCamposDelegado();
            rptDelegados.DataSource =null;
            rptDelegados.DataBind();
        }
        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// </summary>
        private void limpiarPaneles()
        {
            //panelFracaso.Visible = false;
            //panelFracasoListaEquipos.Visible = false;
            //litFracaso.Text = "";
            //litFracasoListaEquipos.Text = "";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.EQUIPO, GestorImagen.MEDIANA);
        }
        /// <summary>
        /// Muestra Panel Fracaso
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }      
    }
}