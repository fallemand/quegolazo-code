using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEquipo
    {
        public Equipo equipo=new Equipo();

        /// <summary>
        /// Registra en la Bd el objeto equipo actual.
        /// autor: Facundo Allemand
        /// </summary>  
        public void registrarEquipo(string nombre, string colorCamisetaPrimario, string colorCamisetaSecundario, string directorTecnico)
        {
            try
            {
                if (equipo.delegadoPrincipal == null && equipo.delegadoOpcional == null)
                    throw new Exception("Debe cargar al menos un delegado");
                equipo.nombre=nombre;
                equipo.colorCamisetaPrimario = colorCamisetaPrimario;
                equipo.colorCamisetaSecundario = colorCamisetaSecundario;
                equipo.directorTecnico = directorTecnico;
                int idTorneo =((Torneo)System.Web.HttpContext.Current.Session["torneo"]).idTorneo;
                DAOEquipo daoEquipo = new DAOEquipo();
                equipo.idEquipo = daoEquipo.registrarEquipo(equipo, idTorneo);
                equipo = new Equipo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Genera una lista a partir de los delegados del objeto equipo
        /// autor: Facundo Allemand
        /// </summary>  
        public List<Delegado> obtenerDelegados()
        {
            List<Delegado> listaDelegados = new List<Delegado>();
            if (equipo != null)
            {
                if (equipo.delegadoPrincipal != null)
                    listaDelegados.Add(equipo.delegadoPrincipal);
                if (equipo.delegadoOpcional != null)
                    listaDelegados.Add(equipo.delegadoOpcional);
            }
            return listaDelegados;
        }

        /// <summary>
        /// Agrega un delegado al objeto de clase equipo
        /// autor: Facundo Allemand
        /// </summary>  
        public void agregarDelegado(string nombre, string email, string telefono, string domicilio)
        {
            validarDelegado(nombre);
            Delegado delegadoNuevo = new Delegado {
                nombre = nombre,
                email = email,
                telefono = telefono,
                domicilio = domicilio
            };
            if(equipo.delegadoPrincipal==null) 
                equipo.delegadoPrincipal =delegadoNuevo;
            else if (equipo.delegadoOpcional == null) 
                equipo.delegadoOpcional =delegadoNuevo;
        }

        /// <summary>
        /// Valida si el objeto equipo cuenta con un delegado con ese nombre
        /// autor: Facundo Allemand
        /// </summary>  
        protected void validarDelegado(string nombre)
        {
            if((equipo.delegadoPrincipal!=null &&nombre.Equals(equipo.delegadoPrincipal.nombre,StringComparison.OrdinalIgnoreCase)) || (equipo.delegadoOpcional!=null && nombre.Equals(equipo.delegadoOpcional.nombre,StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ya existe un delegado con ese nombre");
            if (equipo.delegadoPrincipal != null && equipo.delegadoOpcional != null)
                throw new Exception("Solo puede cargar 2 delegados");
        }

        /// <summary>
        /// Elimina el delegado del objeto de clase equipo
        /// autor: Facundo Allemand
        /// </summary>  
        public void eliminarDelegado(string nombre)
        {
            Delegado delegado = obtenerDelegadoPorNombre(nombre);
            if (equipo.delegadoPrincipal == delegado)
                equipo.delegadoPrincipal = null;
            else if (equipo.delegadoOpcional == delegado)
                equipo.delegadoOpcional = null;
        }

        /// <summary>
        /// Obtiene el delegado del objeto de clase equipo
        /// autor: Facundo Allemand
        /// </summary>  
        public Delegado obtenerDelegadoPorNombre(string nombre)
        {
            if (equipo.delegadoPrincipal.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                return equipo.delegadoPrincipal;
            else if (equipo.delegadoOpcional.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                return equipo.delegadoOpcional;
            else
                throw new Exception("No existe ningún delegado con ese nombre");
        }

        /// <summary>
        /// Modifica el delegado del objeto de clase equipo. No graba en Bd
        /// autor: Facundo Allemand
        /// </summary>  
        public void modificarDelegado(string nombre, string email, string telefono, string domicilio)
        {
            Delegado delegado = (Delegado)System.Web.HttpContext.Current.Session["delegadoAModificar"];
            delegado = obtenerDelegadoPorNombre(delegado.nombre);
            delegado.nombre = nombre;
            delegado.email = email;
            delegado.telefono = telefono;
            delegado.domicilio = domicilio;
        }
    }
}
