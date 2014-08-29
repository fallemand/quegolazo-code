using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorArbitro
    {
        public Arbitro arbitro = new Arbitro();
        /// <summary>
        /// Permite registrar un árbitro en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarArbitro(string nombre, string celular, string email, string matricula)
        {
            if (arbitro == null)
                arbitro = new Arbitro();
            arbitro.nombre = nombre;
            arbitro.celular = celular;
            arbitro.email = email;
            arbitro.matricula = matricula;
            int idTorneo = Sesion.getTorneo().idTorneo;
            DAOArbitro daoArbitro = new DAOArbitro();
            arbitro.idArbitro = daoArbitro.registrarArbitro(arbitro, idTorneo);
        }

        /// <summary>
        /// Obtiene todos los árbitros de un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Arbitro> obtenerArbitrosDeUnTorneo()
        {
            DAOArbitro daoArbitro = new DAOArbitro();
            int idTorneo = Sesion.getTorneo().idTorneo;
            List<Arbitro> arbitros = daoArbitro.obtenerArbitrosDeUnTorneo(idTorneo);
            return arbitros;
        }

        /// <summary>
        /// Obtiene un árbitro por id
        /// autor: Pau Pedrosa
        /// </summary>
        public void obtenerArbitroPorId(int idArbitro)
        {
            DAOArbitro daoArbitro = new DAOArbitro();
            arbitro = daoArbitro.obtenerArbitroPorId(idArbitro);
        }

        /// <summary>
        /// Modifica en la BD un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarArbitro(int idArbitro, string nombre, string celular, string email, string matricula)
        {
            DAOArbitro daoArbitro = new DAOArbitro();
            arbitro.idArbitro = idArbitro;
            arbitro.nombre = nombre;
            arbitro.email = email;
            arbitro.matricula = matricula;
            daoArbitro.modificarArbitro(arbitro);
        }

        /// <summary>
        /// Elimina de la BD un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarArbitro(int idArbitro)
        {
            DAOArbitro daoArbitro = new DAOArbitro();
            daoArbitro.eliminarArbitro(idArbitro);
        }
    }
}
