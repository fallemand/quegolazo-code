using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoADatos;
using Entidades;
using Logica;

namespace quegolazo_code
{
    public partial class registrarEquipoByPau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Registra un nuevo equipo en la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnRegistrarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                GestorEquipo gestorEquipo = new GestorEquipo();
                GestorDelegado gestorDelegado = new GestorDelegado();
                Equipo equipoNuevo = obtenerEquipoDelFormulario();

                equipoNuevo.idEquipo = gestorEquipo.registrarEquipo(equipoNuevo, equipoNuevo.torneo, equipoNuevo.delegadoPrincipal, equipoNuevo.delegadoOpcional);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            //Torneo torneo = gestorTorneo.obtenerTorneoPorId(Int32.Parse(txtIdTorneo.Value));

            Torneo torneo = gestorTorneo.obtenerTorneoPorId(25);
            Delegado delegadoPrincipal = gestorDelegado.obtenerDelegadoPorId(Int32.Parse(txtDelegadoPrincipalAgregado.Value));
            Delegado delegadoOpcional = gestorDelegado.obtenerDelegadoPorId(Int32.Parse(txtDelegadoOpcionalAgregado.Value));
            
            return new Equipo() { nombre = txtNombreEquipo.Value, colorCamisetaPrimario = txtColorCamisetaPrimario.Value, colorCamisetaSecundario = txtColorCamisetaSecundario.Value, directorTecnico = txtDirectorTecnico.Value, delegadoPrincipal = delegadoPrincipal, delegadoOpcional = delegadoOpcional, torneo = torneo };
        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Delegado
        /// autor: Paula Pedrosa
        /// </summary>
        /// <returns></returns>
        private Delegado obtenerDelegadoPrincipalDelFormulario()
        {
            return new Delegado() { nombre = txtNombreDelegadoPrincipal.Value, email = txtEmailDelegadoPrincipal.Value, telefono = txtTelefonoDelegadoPrincipal.Value, domicilio = txtDomicilioDelegadoPrincipal.Value};
        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Delegado
        /// autor: Paula Pedrosa
        /// </summary>
        private Delegado obtenerDelegadoOpcionalDelFormulario()
        {
           return new Delegado() { nombre = txtNombreDelegadoOpcional.Value, email = txtEmailDelegadoOpcional.Value, telefono = txtTelefonoDelegadoOpcional.Value, domicilio = txtDomicilioDelegadoOpcional.Value };
        }

        /// <summary>
        /// Agrega un delegado
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnAgregarDelegadoPrincipal_Click(object sender, EventArgs e)
        {
            GestorDelegado gestorDelegado = new GestorDelegado();
            int idDelegadoPrincipal = gestorDelegado.registrarDelegado(obtenerDelegadoPrincipalDelFormulario());
            txtDelegadoPrincipalAgregado.Value = idDelegadoPrincipal.ToString();
        }

        /// <summary>
        /// Agrega un delegado
        /// autor: Paula Pedrosa
        /// </summary>
        protected void btnAgregarDelegadoOpcional_Click(object sender, EventArgs e)
        {
            GestorDelegado gestorDelegado = new GestorDelegado();
            int idDelegadoOpcional = gestorDelegado.registrarDelegado(obtenerDelegadoOpcionalDelFormulario());
            txtDelegadoOpcionalAgregado.Value = idDelegadoOpcional.ToString();
        }

        

    }
}