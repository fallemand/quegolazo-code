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
        public Equipo equipo = new Equipo();
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
            if (equipo.delegadoOpcional != null)
                throw new Exception("Solo puede cargar dos delegados");
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
            if (equipo.delegadoPrincipal != null && equipo.delegadoPrincipal.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                return equipo.delegadoPrincipal;
            else if (equipo.delegadoOpcional != null && equipo.delegadoOpcional.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
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
            delegado = obtenerDelegadoPorNombre(delegado.nombre);// delegado a modificar
            if ((equipo.delegadoPrincipal != null && nombre.Equals(equipo.delegadoPrincipal.nombre, StringComparison.OrdinalIgnoreCase)) || (equipo.delegadoOpcional != null && nombre.Equals(equipo.delegadoOpcional.nombre, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ya existe un delegado con ese nombre"); 
            delegado.nombre = nombre;
            delegado.email = email;
            delegado.telefono = telefono;
            delegado.domicilio = domicilio;
        }              

        /// <summary>
        /// Obtiene los Equipos de un Torneo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica de objetos Equipos</returns>
        public List<Equipo> obtenerEquiposDeUnTorneo()
        {
            try
            {
                DAOEquipo daoEquipo = new DAOEquipo();
                int idTorneo = ((Torneo)System.Web.HttpContext.Current.Session["torneo"]).idTorneo;
                List<Equipo> equipos = daoEquipo.obtenerEquiposDeUnTorneo(idTorneo);
                return equipos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        /// <summary>
        /// Obtiene el equipo a modificar
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEquipo">id del equipo a modificar</param>
        public void obtenerEquipoAModificar(int idEquipo)
        {
            DAOEquipo daoEquipo = new DAOEquipo();
            equipo = daoEquipo.obtenerEquipoPorId(idEquipo);
        }

        /// <summary>
        /// Modifica el equipo 
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEquipo">id del equipo</param>
        /// <param name="nombre">nombre del equipo</param>
        /// <param name="colorCamisetaPrimario">color camiseta 1° del equipo</param>
        /// <param name="colorCamisetaSecundario">color camiseta 2° del equipo</param>
        /// <param name="directorTecnico">director tecnico del equipo</param>
        public void modificarEquipo(int idEquipo, string nombre, string colorCamisetaPrimario, string colorCamisetaSecundario, string directorTecnico)
        {
            try
            {
                DAOEquipo daoEquipo = new DAOEquipo();
                DAODelegado daoDelegado = new DAODelegado();
                List<Delegado> delegadosModificados = obtenerDelegados();
                if(delegadosModificados.Count == 0)
                    throw new Exception("Debe ingresar al menos un delegado");
                equipo = daoEquipo.obtenerEquipoPorId(idEquipo);// Obtiene el equipo a modificar de la BD
                // Elimina los delegados de la BD, y setea NULL en las claves foráneas de la tabla Equipo
                daoDelegado.eliminarDelegadosPorEquipo(equipo); 
                equipo.nombre = nombre;
                equipo.colorCamisetaPrimario = colorCamisetaPrimario;
                equipo.colorCamisetaSecundario = colorCamisetaSecundario;
                equipo.directorTecnico = directorTecnico;
                //le setea null a los delegados, para sobreescribirlos con los nuevos
                equipo.delegadoPrincipal = null;
                equipo.delegadoOpcional = null;
                int i = 0;
                foreach (Delegado delegado in delegadosModificados)
                {
                    if (i == 0) // primera vez que entra al foreach
                        equipo.delegadoPrincipal = delegado;
                    else
                        equipo.delegadoOpcional = delegado;
                    i++;
                }
                daoEquipo.modificarEquipo(equipo);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
