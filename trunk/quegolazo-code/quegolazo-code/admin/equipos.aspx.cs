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
        List<Delegado> delegados;
        Usuario usuarioLogueado;
        Torneo torneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            limpiarPaneles();
            if (!Page.IsPostBack)
            {
                delegados = new List<Delegado>();
                Session["listaDelegados"] = (List<Delegado>)delegados;
                cargarRepeaterDelegados();
                
            }

        }

        public int eliminarDelegado()
        {
            int codigo = 0;
            return codigo;
        }
                
        private void cargarRepeaterDelegados()
        {
            usuarioLogueado = (Usuario)Session["usuario"];
            torneo = (Torneo)Session["torneo"];
            GestorTorneo gestorTorneo = new GestorTorneo();
            List<Delegado> delegadosDelEquipo = (List<Delegado>)Session["listaDelegados"];

            rptDelegados.DataSource = delegadosDelEquipo;
            rptDelegados.DataBind();
        }


        /// <summary>
        /// Registra un nuevo equipo en la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnRegistrarEquipo_Click(object sender, EventArgs e)
        {
            bool b = false;
            try
            {
                GestorEquipo gestorEquipo = new GestorEquipo();
                GestorDelegado gestorDelegado = new GestorDelegado();
                Equipo equipoNuevo = null;
                Delegado delegadoPrincipal = null;
                Delegado delegadoOpcional = null;
                equipoNuevo = obtenerEquipoDelFormulario();

                if (equipoNuevo != null)
                {
                    delegadoPrincipal = equipoNuevo.delegadoPrincipal;
                    delegadoOpcional = equipoNuevo.delegadoOpcional;
                }
                else
                {
                    b = true;
                    throw new Exception();
                }

                delegadoPrincipal.idDelegado = gestorDelegado.registrarDelegado(delegadoPrincipal);

                if (delegadoOpcional != null)
                    delegadoOpcional.idDelegado = gestorDelegado.registrarDelegado(delegadoOpcional);

                equipoNuevo.idEquipo = gestorEquipo.registrarEquipo(equipoNuevo, equipoNuevo.torneo, equipoNuevo.delegadoPrincipal, equipoNuevo.delegadoOpcional);
                GestorImagen.guardarImagenTorneo(fuLog.PostedFile, equipoNuevo.idEquipo, GestorImagen.EQUIPO);

                limpiarCamposEquipo();
                limpiarCamposDelegado();
                mostrarPanelExito("El equipo " + equipoNuevo.nombre + " fue registrado exitosamente");
            }
            catch (Exception ex)
            {
                if (b)
                    mostrarPanelFracaso("Debe ingresar un delegado");
                else
                    mostrarPanelExito(ex.Message);
                               
            }
        }

        public void limpiarCamposEquipo()
        {
            txtNombreEquipo.Value = "";
            txtNombreDirector.Value = "";
            txtColorPrimario.Value = "#E1E1E1";
            txtColorSecundario.Value = "#E1E1E1";
            delegados = (List<Delegado>)Session["listaDelegados"];
            delegados.Clear();
            cargarRepeaterDelegados();
                       
        }

        public void limpiarCamposDelegado()
        {
            txtNombreDelegado.Value = "";
            txtEmailDelegado.Value = "";
            txtTelefonoDelegado.Value = "";
            txtDireccionDelegado.Value = "";
        }

        /// <summary>
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
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
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en el panel.</param>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelExito.Visible = false;
            panelFracaso.Visible = true;
        }

        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
 
        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Equipo
        /// autor: Paula Pedrosa
        /// <returns></returns>
        private Equipo obtenerEquipoDelFormulario()
        {
            GestorTorneo gestorTorneo = new GestorTorneo();
            GestorDelegado gestorDelegado = new GestorDelegado();
            Delegado delegadoPrincipal = null;
            Delegado delegadoOpcional = null;
            bool b = false;
            //Torneo torneo = gestorTorneo.obtenerTorneoPorId(Int32.Parse(txtIdTorneo.Value));

            Torneo torneo = gestorTorneo.obtenerTorneoPorId(87);
            delegados = (List<Delegado>)Session["listaDelegados"];

            if (delegados.Count != 0)
            {
                for (int i = 0; i < delegados.Count; i++)
                {
                    if (!b)
                    {
                        b = true;
                        delegadoPrincipal = delegados.ElementAt(i);
                    }
                    else
                        delegadoOpcional = delegados.ElementAt(i);
                }
                
                return new Equipo() { nombre = txtNombreEquipo.Value, colorCamisetaPrimario = txtColorPrimario.Value, colorCamisetaSecundario = txtColorSecundario.Value, directorTecnico = txtNombreDirector.Value, delegadoPrincipal = delegadoPrincipal, delegadoOpcional = delegadoOpcional, torneo = torneo };
            }
            else
                return null;

        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Delegado
        /// autor: Paula Pedrosa
        /// </summary>
        /// <returns></returns>
        private Delegado obtenerDelegadoDelFormulario()
        {
            return new Delegado() { nombre = txtNombreDelegado.Value, email = txtEmailDelegado.Value, telefono = txtTelefonoDelegado.Value, domicilio = txtDireccionDelegado.Value };
        }

        protected void btnAgregarDelegado_Click(object sender, EventArgs e)
        {
            GestorDelegado gestorDelegado = new GestorDelegado();
            delegados = (List<Delegado>)Session["listaDelegados"];
            Delegado delegadoDelFormulario = null;
            bool b = false;

            if (delegados.Count < 2)
            {
                delegadoDelFormulario = obtenerDelegadoDelFormulario();
                foreach (Delegado delegado in delegados)
                {
                    if (delegado.nombre.Equals(delegadoDelFormulario.nombre))
                    {
                        b = true;
                        break;
                    } 

                }

                if(!b)
                    delegados.Add(delegadoDelFormulario);
                else
                    mostrarPanelFracaso("Ese delegado ya fue registrado. Ingrese otro Nombre");

            }    
            else
                mostrarPanelFracaso("Puede ingresar hasta dos delegados");
            
         
            Session["listaDelegados"] = (List<Delegado>)delegados;
            cargarRepeaterDelegados();
            limpiarCamposDelegado();
        }

        protected void rptDelegados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "eliminarDelegado")
            {
                string nombreDelegado = e.CommandArgument.ToString();
                delegados = (List<Delegado>)Session["listaDelegados"];
                int i = 0;
                
                foreach (Delegado delegado in delegados)
                {
                    if (delegado.nombre.Equals(nombreDelegado))
                    {
                        delegados.RemoveAt(i);
                        break;
                    }

                    i++;
                       
                }

                cargarRepeaterDelegados();

            }

            if (e.CommandName == "modificarDelegado")
            {
                string nombreDelegado = e.CommandArgument.ToString();
                delegados = (List<Delegado>)Session["listaDelegados"];
                int i = 0;

                foreach (Delegado delegado in delegados)
                {
                    if (delegado.nombre.Equals(nombreDelegado))
                    {
                        txtNombreDelegado.Value = delegado.nombre;
                        txtTelefonoDelegado.Value = delegado.telefono;
                        txtDireccionDelegado.Value = delegado.domicilio;
                        txtEmailDelegado.Value = delegado.email;
                        delegados.RemoveAt(i);
                        break;
                        
                    }

                    i++;

                }

                cargarRepeaterDelegados();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDelegados();", true);

            }
     
            
        }

     

      
    }
}