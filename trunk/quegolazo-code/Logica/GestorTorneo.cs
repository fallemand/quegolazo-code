using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

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
        /// <param name="torneoNuevo">Objeto Torneo</param>
        /// <param name="usuario">Objeto Usuario</param>
        /// <returns>El id del torneo que se acaba de registrar</returns>
        public int registrarTorneo(Torneo torneoNuevo, Usuario usuario)
        {
            try
            {
                DAOTorneo daoTorneo = new DAOTorneo();
                int idTorneoRegistrado = daoTorneo.registrarTorneo(torneoNuevo, usuario);

                return idTorneoRegistrado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
