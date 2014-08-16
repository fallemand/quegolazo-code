using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
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
            try
            {
                if (cancha == null)
                    cancha = new Cancha();
                cancha.nombre = nombre;
                cancha.domicilio = domicilio;
                cancha.telefono = telefono;
                int idTorneo = ((Torneo)System.Web.HttpContext.Current.Session["torneo"]).idTorneo;
                DAOCancha daoCancha = new DAOCancha();
                cancha.idCancha = daoCancha.registrarCancha(cancha, idTorneo);
                //cancha = new Cancha();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de Canchas de un torneo en particular
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica de objeto Cancha</returns>
        public List<Cancha> obtenerCanchasDeUnTorneo()
        {
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                int idTorneo = ((Torneo)System.Web.HttpContext.Current.Session["torneo"]).idTorneo;
                List<Cancha> canchas = daoCancha.obtenerCanchasDeUnTorneo(idTorneo);
                return canchas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la Cancha a modificar por su Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de Cancha a modificar</param>
        public void obtenerCanchaAModificar(int idCancha)
        {
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                cancha = daoCancha.obtenerCanchaPorId(idCancha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
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
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                cancha.idCancha = idCancha;
                cancha.nombre = nombre;
                cancha.domicilio = domicilio;
                cancha.telefono = telefono;
                daoCancha.modificarCancha(cancha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Obtiene todos los tamaños de cancha
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica del objeto TamanioCancha</returns>
        public List<TamanioCancha> obtenerTodos()
        {
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                List<TamanioCancha> tamaniosCancha = new List<TamanioCancha>();
                tamaniosCancha = daoCancha.obtenerTodos();
                return tamaniosCancha;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de Cancha por Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTamanioCancha">Id del tamaño de cancha</param>
        /// <returns>Objeto Tamaño Cancha</returns>
        public TamanioCancha obtenerTamanioCanchaPorId(int idTamanioCancha)
        {
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                TamanioCancha tamanioCancha = null;
                tamanioCancha = daoCancha.obtenerTamanioCanchaPorId(idTamanioCancha);
                return tamanioCancha;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una cancha de la Bd
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de la cancha a eliminar</param>
        public void eliminarCancha(int idCancha)
        {
            try
            {
                DAOCancha daoCancha = new DAOCancha();
                daoCancha.eliminarCancha(idCancha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
