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

        protected void btnRegistrarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                GestorEquipo gestorEquipo = new GestorEquipo();
                GestorDelegado gestorDelegado = new GestorDelegado();
                Equipo equipoNuevo = obtenerEquipoDelFormulario();
                equipoNuevo.delegadoPrincipal.idDelegado = gestorDelegado.registrarDelegado(equipoNuevo.delegadoPrincipal);
                equipoNuevo.idEquipo = gestorEquipo.registrarEquipo(equipoNuevo, equipoNuevo.torneo, equipoNuevo.delegadoPrincipal, equipoNuevo.delegadoOpcional);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Equipo
        /// <returns></returns>
        private Equipo obtenerEquipoDelFormulario()
        {
            Delegado delegadoPrincipal = obtenerDelegadoPrincipalDelFormulario();
            Delegado delegadoOpcional = null;
            GestorTorneo gestorTorneo = new GestorTorneo();
            //Torneo torneo = gestorTorneo.obtenerTorneoPorId(Int32.Parse(txtIdTorneo.Value));
            Torneo torneo = gestorTorneo.obtenerTorneoPorId(25);

            return new Equipo() { nombre = txtNombreEquipo.Value, colorCamisetaPrimario = txtColorCamisetaPrimario.Value, colorCamisetaSecundario = txtColorCamisetaSecundario.Value, directorTecnico = txtDirectorTecnico.Value, delegadoPrincipal = delegadoPrincipal, delegadoOpcional = delegadoOpcional, torneo = torneo };
        }

        /// <summary>
        /// Obtiene los datos del formulario y los encapsula en un objeto Delegado
        /// </summary>
        /// <returns></returns>
        private Delegado obtenerDelegadoPrincipalDelFormulario()
        {
            return new Delegado() { nombre = txtNombreDelegadoPrincipal.Value, email = txtEmailDelegadoPrincipal.Value, telefono = txtTelefonoDelegadoPrincipal.Value, domicilio = txtDomicilioDelegadoPrincipal.Value};
        }

    }
}