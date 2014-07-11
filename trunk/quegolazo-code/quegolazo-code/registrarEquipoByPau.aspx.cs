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

namespace quegolazo_code
{
    public partial class registrarEquipoByPau : System.Web.UI.Page
    {
        List<Delegado> delegados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                delegados = new List<Delegado>();
                Session["listaDelegados"] = (List<Delegado>) delegados;
            }
            
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
                              
                if (delegadoOpcional != null)
                    delegadoOpcional.idDelegado = gestorDelegado.registrarDelegado(delegadoOpcional);  
                
                equipoNuevo.idEquipo = gestorEquipo.registrarEquipo(equipoNuevo, equipoNuevo.torneo, equipoNuevo.delegadoPrincipal, equipoNuevo.delegadoOpcional);
                GestorImagen.guardarImagenTorneo(fluImagen.PostedFile, equipoNuevo.idEquipo, GestorImagen.EQUIPO);
            }
            catch (Exception ex)
            {
                if (b)
                    lblMensaje.Text = "Debe ingresar un delegado";
                else
                    lblMensaje.Text = ex.Message;
            }
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

                return new Equipo() { nombre = txtNombreEquipo.Value, colorCamisetaPrimario = txtColorCamisetaPrimario.Value, colorCamisetaSecundario = txtColorCamisetaSecundario.Value, directorTecnico = txtDirectorTecnico.Value, delegadoPrincipal = delegadoPrincipal, delegadoOpcional = delegadoOpcional, torneo = torneo };
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
            return new Delegado() { nombre = txtNombreDelegadoPrincipal.Value, email = txtEmailDelegadoPrincipal.Value, telefono = txtTelefonoDelegadoPrincipal.Value, domicilio = txtDomicilioDelegadoPrincipal.Value};
        }

        
        /// <summary>
        /// Agrega un delegado
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnAgregarDelegadoPrincipal_Click(object sender, EventArgs e)
        {
            GestorDelegado gestorDelegado = new GestorDelegado();
            delegados = (List<Delegado>) Session["listaDelegados"];
            
            if (delegados.Count < 2)
                delegados.Add(obtenerDelegadoDelFormulario()); 
            else
                lblMensaje.Text = "Puede ingresar hasta dos delegados";

            Session["listaDelegados"] = (List<Delegado>)delegados;
                         
        }

    }
}