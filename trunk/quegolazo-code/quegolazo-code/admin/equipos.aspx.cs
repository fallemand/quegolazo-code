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
        GestorEquipo gestorEquipo=null;

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
                lblMensajeEquipos.Text = ex.Message;
            }
                        
            limpiarPaneles();
        }


       /// <summary>
       /// carga el repeater de Equipos
       /// autor: Pau Pedrosa
       /// </summary>
        private void cargarRepeaterEquipos()
        {
            rptEquipos.DataSource = gestorEquipo.obtenerEquiposDeUnTorneo();
            rptEquipos.DataBind();
        }


        /// <summary>
        /// cargar el repeater de los delegados con el nombre
        /// autor: Paula Pedrosa
        /// </summary>             
        private void cargarRepeaterDelegados()
        {
            rptDelegados.DataSource = gestorEquipo.obtenerDelegados();
            rptDelegados.DataBind();
        }

        /// <summary>
        /// Registra un nuevo equipo en la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnRegistrarEquipo_Click(object sender, EventArgs e)
        {
            try
            {               
                gestorEquipo.registrarEquipo(txtNombreEquipo.Value, txtColorPrimario.Value, txtColorSecundario.Value, txtNombreDirector.Value);
                GestorImagen.guardarImagenTorneo(fuLog.PostedFile, gestorEquipo.equipo.idEquipo, GestorImagen.EQUIPO);
                limpiarCamposEquipo();
                mostrarPanelExito("Equipo registrado con éxito!");
                lblMensajeEquipos.Text = "";
                cargarRepeaterEquipos();
            }
            catch (Exception ex)
            {
                    mostrarPanelFracaso(ex.Message);            
            }
        }

        /// <summary>
        /// Agrega un delegado a una lista genérica de delegados
        /// </summary>
        protected void btnAgregarDelegado_Click(object sender, EventArgs e)
        {
            try
            {
                gestorEquipo.agregarDelegado(txtNombreDelegado.Value, txtEmailDelegado.Value, txtTelefonoDelegado.Value, txtDireccionDelegado.Value);
                limpiarCamposDelegado();
                cargarRepeaterDelegados();
            }
            catch (Exception ex)
            {
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
        /// Método para gestionar la eliminación y modificación de los delegados
        /// autor: Paula Pedrosa
        /// </summary>
        protected void rptDelegados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                //Si elimina el delegado
                if (e.CommandName == "eliminarDelegado")
                {
                    string nombre = e.CommandArgument.ToString();
                    gestorEquipo.eliminarDelegado(nombre);
                    cargarRepeaterDelegados();
                }

                //Si modifica el delegado
                if (e.CommandName == "modificarDelegado")
                {
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
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el panel.</param>
        private void mostrarPanelExito(string mensaje)
        {
            litExito.Text = mensaje;
            panelExito.Visible = true;
            panelFracaso.Visible = false;
        }

        /// <summary>
        /// Habilita el panel de fraaso y deshabilita el panel de exito.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el panel.</param>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelExito.Visible = false;
            panelFracaso.Visible = true;
        }

        /// <summary>
        /// limpia los paneles de éxito y fracaso
        /// autor: Paula Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
        }
        /// <summary>
        /// limpia los campos del alta de equipo
        /// autor: Paula Pedrosa
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
        /// limpia los campos del alta de delegado
        /// autor: Paula Pedrosa
        /// </summary>
        public void limpiarCamposDelegado()
        {
            txtNombreDelegado.Value = "";
            txtEmailDelegado.Value = "";
            txtTelefonoDelegado.Value = "";
            txtDireccionDelegado.Value = "";
            btnAgregarDelegado.Visible = true;
            btnModificarDelegado.Visible = false;
            btnCancelarDelegado.Visible = false;
        }

        /// <summary>
        /// Permite editar un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptEquipos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "editarEquipo")
            {                
                gestorEquipo.obtenerEquipoAModificar(Int32.Parse(e.CommandArgument.ToString()));                
                txtNombreEquipo.Value = gestorEquipo.equipo.nombre;
                txtNombreDirector.Value = gestorEquipo.equipo.directorTecnico;
                txtColorPrimario.Value = gestorEquipo.equipo.colorCamisetaPrimario;
                txtColorSecundario.Value = gestorEquipo.equipo.colorCamisetaSecundario;
                rptDelegados.DataSource = gestorEquipo.obtenerDelegados();
                rptDelegados.DataBind();
                btnRegistrarEquipo.Visible = false;
                btnModificarEquipo.Visible = true;
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
                GestorImagen.guardarImagenTorneo(fuLog.PostedFile, gestorEquipo.equipo.idEquipo, GestorImagen.EQUIPO);
                limpiarCamposEquipo();
                mostrarPanelExito("Equipo modificado con éxito!");
                cargarRepeaterEquipos();
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }

        }

        /// <summary>
        /// Permite ir a agregar un nuevo equipo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void lnkNuevoEquipo_Click(object sender, EventArgs e)
        {
            limpiarCamposEquipo();
            btnRegistrarEquipo.Visible = true;
            btnModificarEquipo.Visible = false;
        }
    }
}