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
            btnAgregarDelegado.Text = "Agregar Delegado";

            if (!Page.IsPostBack)
            {
                delegados = new List<Delegado>();
                Session["listaDelegados"] = (List<Delegado>)delegados;
                cargarRepeaterDelegados();                
            }

        }

        /// <summary>
        /// cargar el repeater de los delegados con el nombre
        /// autor: Paula Pedrosa
        /// </summary>             
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
            bool noHayDelegados = false;
            try
            {
                GestorEquipo gestorEquipo = new GestorEquipo();
                GestorDelegado gestorDelegado = new GestorDelegado();
                Equipo equipoNuevo = null;
                Delegado delegadoPrincipal = null;
                Delegado delegadoOpcional = null;
                equipoNuevo = obtenerEquipoDelFormulario();

                if (equipoNuevo != null) // si es null es porque no ingresó delegados
                {
                    delegadoPrincipal = equipoNuevo.delegadoPrincipal;
                    delegadoOpcional = equipoNuevo.delegadoOpcional;
                }
                else
                {
                    //no ingesó delegados
                    noHayDelegados = true;
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
                if (noHayDelegados)
                    mostrarPanelFracaso("Debe ingresar los datos del delegado y del equipo para continuar");
                else
                    mostrarPanelExito(ex.Message);
                               
            }
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
            delegados = (List<Delegado>)Session["listaDelegados"];
            delegados.Clear();
            cargarRepeaterDelegados();
                       
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
        /// Obtiene los datos del formulario y los encapsula en un objeto Equipo
        /// autor: Paula Pedrosa
        /// <returns>el Equipo del formulario o null sino ingreso delegados</returns>
        private Equipo obtenerEquipoDelFormulario()
        {
            GestorTorneo gestorTorneo = new GestorTorneo();
            GestorDelegado gestorDelegado = new GestorDelegado();
            Delegado delegadoPrincipal = null;
            Delegado delegadoOpcional = null;
            //Torneo torneo = (Torneo) Session["torneo"];
            Torneo torneo = gestorTorneo.obtenerTorneoPorId(87);

            delegados = (List<Delegado>)Session["listaDelegados"];

            bool esPrimeraVez = false; 

            if (delegados.Count != 0)
            {
                for (int i = 0; i < delegados.Count; i++)
                {
                    if (!esPrimeraVez)
                    {
                        esPrimeraVez = true;
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
        /// <returns>Objeto Delegado obtenido de forumlario</returns>
        private Delegado obtenerDelegadoDelFormulario()
        {
            return new Delegado() { nombre = txtNombreDelegado.Value, email = txtEmailDelegado.Value, telefono = txtTelefonoDelegado.Value, domicilio = txtDireccionDelegado.Value };
        }

        /// <summary>
        /// Agrega un delegado a una lista genérica de delegados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregarDelegado_Click(object sender, EventArgs e)
        {
            GestorDelegado gestorDelegado = new GestorDelegado();
            delegados = (List<Delegado>)Session["listaDelegados"];
            Delegado delegadoDelFormulario = null;
            bool existeDelegado = false;

            //si hay un delegado a modificar
            if ((Delegado)Session["delegadoAModificar"] != null)
            {
                Delegado delegadoAModificar = (Delegado)Session["delegadoAModificar"];

                foreach (Delegado delegado in delegados)
                {
                    if (delegado.nombre.Equals(delegadoAModificar.nombre))
                    {
                        delegado.nombre = txtNombreDelegado.Value;
                        delegado.telefono = txtTelefonoDelegado.Value;
                        delegado.domicilio = txtDireccionDelegado.Value;
                        delegado.email = txtEmailDelegado.Value;
                        break;
                    }
                }

                Session["delegadoAModificar"] = (Delegado) null;
                mostrarPanelExito("El Delegado fue modificado exitosamente");

            }
            else 
            {
                if (delegados.Count < 2)
                {
                    delegadoDelFormulario = obtenerDelegadoDelFormulario();
                    foreach (Delegado delegado in delegados)
                    {
                        //valida que no esté cargado ese nombre de delegado para ese equipo
                        if (delegado.nombre.Equals(delegadoDelFormulario.nombre))
                        {
                            existeDelegado = true;
                            break;
                        }
                    }

                    if (!existeDelegado) // agrega a la lista el delegado
                    { 
                        delegados.Add(delegadoDelFormulario);
                        mostrarPanelExito("El Delegado fue agregado exitosamente");
                    }
                    else
                        mostrarPanelFracaso("Ese delegado ya fue registrado. Ingrese otro Nombre.");

                }
                else
                    mostrarPanelFracaso("Puede ingresar hasta dos delegados.");
            }

            Session["listaDelegados"] = (List<Delegado>) delegados;
            cargarRepeaterDelegados();
            limpiarCamposDelegado();
        }

        /// <summary>
        /// Método para gestionar la eliminación y modificación de los delegados
        /// autor: Paula Pedrosa
        /// </summary>
        protected void rptDelegados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Si elimina el delegado
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

            //Si modifica el delegado
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

                        Session["delegadoAModificar"] = (Delegado) delegado; // Guarda el objeto delegado a modificar
                        break;                        
                    }
                    i++;
                }

                btnAgregarDelegado.Text = "Modificar Delegado";
                cargarRepeaterDelegados();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDelegados();", true);

            }
     
            
        }

     

      
    }
}