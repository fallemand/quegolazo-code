using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utils;
using AccesoADatos;

namespace Logica
{
    public class GestorCancha
    {
        public Cancha cancha = new Cancha();
        /// <summary>
        /// Registra en la Bd el objeto Cancha actual
        /// autor: Pau Pedrosa
        /// </summary>  
        public void registrarCancha(string nombre, string domicilio, string telefono)
        {
            if (cancha == null)
                cancha = new Cancha();
            cancha.nombre = nombre;
            cancha.domicilio = domicilio;
            cancha.telefono = telefono;
            int idTorneo = Sesion.getTorneo().idTorneo;
            DAOCancha daoCancha = new DAOCancha();
            cancha.idCancha = daoCancha.registrarCancha(cancha, idTorneo);
        }

        /// <summary>
        /// Obtiene una lista de Canchas de un torneo en particular
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Cancha> obtenerCanchasDeUnTorneo()
        {
            DAOCancha daoCancha = new DAOCancha();
            int idTorneo = Sesion.getTorneo().idTorneo;
            List<Cancha> canchas = daoCancha.obtenerCanchasDeUnTorneo(idTorneo);
            return canchas;
        }

        /// <summary>
        /// Obtiene la Cancha a modificar por su Id
        /// autor: Pau Pedrosa
        /// </summary>
        public void obtenerCanchaAModificar(int idCancha)
        {
            DAOCancha daoCancha = new DAOCancha();
            cancha = daoCancha.obtenerCanchaPorId(idCancha);
        }

        /// <summary>
        /// Modifica la Cancha
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de la Cancha a modificar</param>
        /// <param name="nombre">Nombre nuevo</param>
        /// <param name="domicilio">Domicilio nuevo</param>
        /// <param name="telefono">Telefono nuevo</param>
        public void modificarCancha(int idCancha, string nombre, string domicilio, string telefono)
        {
            DAOCancha daoCancha = new DAOCancha();
            cancha.idCancha = idCancha;
            cancha.nombre = nombre;
            cancha.domicilio = domicilio;
            cancha.telefono = telefono;
            daoCancha.modificarCancha(cancha);
        }
        
        /// <summary>
        /// Obtiene todos los tamaños de cancha
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica del objeto TamanioCancha</returns>
        public List<TamanioCancha> obtenerTodos()
        {
            DAOCancha daoCancha = new DAOCancha();
            List<TamanioCancha> tamaniosCancha = new List<TamanioCancha>();
            tamaniosCancha = daoCancha.obtenerTodos();
            return tamaniosCancha;
        }

        /// <summary>
        /// Obtiene el Tamaño de Cancha por Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTamanioCancha">Id del tamaño de cancha</param>
        /// <returns>Objeto Tamaño Cancha</returns>
        public TamanioCancha obtenerTamanioCanchaPorId(int idTamanioCancha)
        {
            DAOCancha daoCancha = new DAOCancha();
            TamanioCancha tamanioCancha = null;
            tamanioCancha = daoCancha.obtenerTamanioCanchaPorId(idTamanioCancha);
            return tamanioCancha;
        }

        /// <summary>
        /// Elimina una cancha de la Bd
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de la cancha a eliminar</param>
        public void eliminarCancha(int idCancha)
        {
            DAOCancha daoCancha = new DAOCancha();
            daoCancha.eliminarCancha(idCancha);
            GestorImagen.borrrarImagen(idCancha, GestorImagen.COMPLEJO);
        }

        public Cancha obtenerCanchaPorId(int idCancha)
        {
            DAOCancha daoCancha = new DAOCancha();
            return daoCancha.obtenerCanchaPorId(idCancha);
        }
    }
}
