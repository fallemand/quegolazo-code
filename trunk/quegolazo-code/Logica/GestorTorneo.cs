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
        public Torneo torneo = new Torneo();

        /// <summary>
        /// Obtiene los torneos de un usuario
        /// autor: Paula Pedrosa
        /// </summary>
        public List<Torneo> obtenerTorneosPorUsuario()
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            List<Torneo> torneos = daoTorneo.obtenerTorneosPorUsuario(Sesion.getUsuario().idUsuario);
            return torneos;
        }      

        /// <summary>
        /// Obtener torneo por Id del torneo y el id usuario
        /// autor: Paula Pedrosa
        /// </summary>
        public Torneo obtenerTorneoPorIdYUsuario(int idTorneo, int idUsuario)
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            torneo = daoTorneo.obtenerTorneoPorIdYUsuario(idTorneo, idUsuario);
            return torneo;
        }

        /// <summary>
        /// Obtener un Torneo por Id
        /// autor: Paula Pedrosa
        /// </summary>
        public Torneo obtenerTorneoPorId(int idTorneo)
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            torneo = daoTorneo.obtenerTorneoPorId(idTorneo);
            return torneo;
        }

        /// <summary>
        /// Registra un nuevo Torneo 
        /// </summary>
        /// <returns>El id del torneo que se acaba de registrar</returns>
        public int registrarTorneo(string nombre, string descripcion, string nick)
        {
            torneo.nombre = Validador.isNotEmpty(nombre);
            torneo.nick = Validador.isNotEmpty(nick);
            torneo.descripcion = descripcion;
            DAOTorneo daoTorneo = new DAOTorneo();
            torneo.idTorneo = daoTorneo.registrarTorneo(torneo, Sesion.getUsuario().idUsuario);
            return torneo.idTorneo;
        }

        /// <summary>
        /// Modifica un torneo existente 
        /// </summary>
        public void modificarTorneo(string nombre, string descripcion)
        {
            torneo.nombre = Validador.isNotEmpty(nombre);
            torneo.descripcion = descripcion;
            DAOTorneo daoTorneo = new DAOTorneo();
            daoTorneo.modificarTorneo(torneo);
        }
    }
}
