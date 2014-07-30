using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Web;

namespace Logica
{
    public class GestorTorneo
    {
       
        /// <summary>
        /// Obtiene los torneos de un usuario
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idUsuario">id del usuario</param>
        /// <returns>Lista genérica de objetos Torneos</returns>
        public List<Torneo> obtenerTorneosDeUnUsuario(int idUsuario)
        {
            try
            {
                DAOTorneo daoTorneo = new DAOTorneo();
                List<Torneo> torneos = daoTorneo.obtenerTorneosDeUnUsuario(idUsuario);

                return torneos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }


        /// <summary>
        /// Asigna la ruta adecuada a cada torneo checkeando si existe o no la imagen que le corresponde. 
        /// En caso de que la imagen no se haya subido, se carga la ruta por defecto de una imagen generica.
        /// </summary>
        /// <param name="listaDeTorneos">La lista de torneos que se desea modificar</param>
        /// <param name="dimension">El tamaño de la imagen que se desea guardar</param>
       public void asignarRutaDeImagenATorneos(ref List<Torneo> listaDeTorneos, GestorImagen.enumDimensionImagen dimension) { 
           string abreviatura = GestorImagen.devolverAbreviaturaDeImagen(dimension).ToString();
           foreach (Torneo item in listaDeTorneos)
            {
               string rutaTest = System.Web.HttpContext.Current.Server.MapPath("/resources/img/torneos/");
               rutaTest += item.idTorneo.ToString() + abreviatura + ".jpg";
               if (GestorImagen.existeImagen(rutaTest))
               {
                   item.rutaImagen = "/resources/img/torneos/" + item.idTorneo.ToString() + abreviatura + ".jpg"; 
               }
               else {
                   item.rutaImagen = "/resources/img/torneos/default" + abreviatura + ".jpg";
               }
	        }
        
        }

        
      

        /// <summary>
        /// Obtener torneo por Id del torneo y el id usuario
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Objeto Torneo</returns>
        public Torneo obtenerTorneoPorIdYUsuario(int idTorneo, int idUsuario)
        {
            try
            {
                DAOTorneo daoTorneo = new DAOTorneo();
                Torneo torneo = null;
                torneo = daoTorneo.obtenerTorneoPorIdYUsuario(idTorneo, idUsuario);

                return torneo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtener un Torneo por Id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del Torneo</param>
        /// <returns>Objeto Torneo</returns>
        public Torneo obtenerTorneoPorId(int idTorneo)
        {
            try
            {
                DAOTorneo daoTorneo = new DAOTorneo();
                Torneo torneo = null;
                torneo = daoTorneo.obtenerTorneoPorId(idTorneo);

                return torneo;

            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Registra un nuevo Torneo 
        /// </summary>
        /// <param name="torneoCambiado">Objeto Torneo</param>
        /// <param name="usuario">Objeto Usuario</param>
        /// <returns>El id del torneo que se acaba de registrar</returns>
        public int registrarTorneo(string nombre, string descripcion, string nick, string idUsuario)
        {
            try
            {
                DAOTorneo daoTorneo = new DAOTorneo();
                Torneo torneoNuevo = validarTorneo(nombre, descripcion, nick);
                torneoNuevo.usuario.idUsuario = Validador.castInt(idUsuario);
                int idTorneoRegistrado = daoTorneo.registrarTorneo(torneoNuevo);
                return idTorneoRegistrado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Modifica un torneo existente 
        /// </summary>
        /// <param name="torneoNuevo">Objeto Torneo</param>
        /// <param name="usuario">Objeto Usuario</param>
        /// <returns>El id del torneo que se acaba de modificar</returns>
        public void modificarTorneo(string nombre, string descripcion, int idTorneo)
        {
            try
            {
                Torneo torneoCambiado = validarTorneo(nombre, descripcion, idTorneo);   
                DAOTorneo daoTorneo = new DAOTorneo();
                daoTorneo.modificarTorneo(torneoCambiado);
             }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Valida los datos del torneo para una modificacion
        /// </summary>
        /// <param name="nombre">nombre nuevo del torneo</param>
        /// <param name="descrip">descripcion nueva del torneo</param>
        /// <param name="idTorneo">id del torneo a modificar</param>
        /// <returns>Un objeto torneo con los datos validados, o bien lanza una excepcion</returns>
        private Torneo validarTorneo(string nombre, string descrip, int idTorneo)
        {
            try
            {
                return new Torneo() { nombre = Validador.isEmpty(nombre), descripcion = descrip, idTorneo = idTorneo };
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Valida los datos de un torneo para su registracion
        /// </summary>
        /// <param name="nombre">nombre del torneo a registrar</param>
        /// <param name="descrip">descripcion del torneo a registrar</param>
        /// <param name="nick">url unica del torneo</param>
        /// <returns>Devuelve un objeto Torneo con los datos validados, o bien lanza la excepcion correspondiente</returns>
        private Torneo validarTorneo(string nombre, string descrip, string nick)
        {
            try
            {
                return new Torneo() { nombre = Validador.isEmpty(nombre), descripcion = descrip, nick = Validador.validarCadenaSinEspacios(nick) };

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

     
    }
}
