using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Web;
using System.Web.Script.Serialization;

namespace Logica
{
    public class GestorTorneo
    {
        public Torneo torneo = new Torneo();

        /// <summary>
        /// Obtiene el ultimo torneo del usuario, si no tiene devuelve null.
        /// autor: Facu Allemand
        /// </summary>
        public Torneo obtenerUltimoTorneoDelUsuario()
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            Torneo torneo = daoTorneo.obtenerUltimoTorneoDelUsuario(Sesion.getUsuario().idUsuario);
            return torneo;
        }

        /// <summary>
        /// Obtiene los torneos de un usuario
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Torneo> obtenerTorneosPorUsuario()
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            List<Torneo> torneos = daoTorneo.obtenerTorneosPorUsuario(Sesion.getUsuario().idUsuario);
            return torneos;
        }      

        /// <summary>
        /// Obtener torneo por Id del torneo y el id usuario
        /// autor: Pau Pedrosa
        /// </summary>
        public Torneo obtenerTorneoPorIdYUsuario(int idTorneo, int idUsuario)
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            torneo = daoTorneo.obtenerTorneoPorIdYUsuario(idTorneo, idUsuario);
            return torneo;
        }

        /// <summary>
        /// Obtener un Torneo por Id
        /// autor: Pau Pedrosa
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

        /// <summary>
        /// Elimina un torneo 
        /// </summary>
        public void eliminarTorneo(int idTorneo)
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            daoTorneo.eliminarTorneo(idTorneo);
            if (torneo.idTorneo == idTorneo)
                torneo = null;
        }
        /// <summary>
        /// Guarda la configuracion visual que genero un usuario para un torneo en particular.
        /// </summary>
        /// <param name="configuracion"></param>
        public void registrarConfiguracionVisual(object configuracion) {
            JavaScriptSerializer s = new JavaScriptSerializer();
            ConfiguracionVisual conf = s.ConvertToType<ConfiguracionVisual>(configuracion);
            GestorTorneo gestor = Sesion.getGestorTorneo();
            gestor.torneo.configuracionVisual = conf;
            new DAOTorneo().registrarConfiguracionVisual(gestor.torneo);
        }

        public ConfiguracionVisual obtenerConfiguracionVisual(int idTorneo) {
            return new DAOTorneo().obtenerConfiguracionVisual(idTorneo);
        }

        /// <summary>
        /// Obtener un Torneo por nick
        /// autor: Pau Pedrosa
        /// </summary>
        public Torneo obtenerTorneoPorNick(string nick)
        {
            DAOTorneo daoTorneo = new DAOTorneo();
            return daoTorneo.obtenerTorneoPorNick(nick);           
        }
    }
}