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
        /// <summary>
        /// Obtiene todos los tamaños de cancha
        /// autor: Paula Pedrosa
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
        /// autor: Paula Pedrosa
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
    }
}
