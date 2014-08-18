using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorTipoSuperficie
    {
        /// <summary>
        /// Obtiene todos los Tipos de Superficies de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica de objeto TipoSuperficie</returns>
        public List<TipoSuperficie> obtenerTodos()
        {
            DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();
            List<TipoSuperficie> tiposSuperficie = new List<TipoSuperficie>();
            tiposSuperficie = daoTipoSuperficie.obtenerTodos();
            return tiposSuperficie;
        }

        /// <summary>
        /// Obtiene un Tipo de Superficie por Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTipoSuperficie">Id del Tipo de Superficie a obtener</param>
        /// <returns>Objeto Tipo de Superficie</returns>
        public TipoSuperficie obtenerTipoSuperficiePorId(int idTipoSuperficie)
        {
            DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();
            TipoSuperficie tipoSuperficie = null;
            tipoSuperficie = daoTipoSuperficie.obtenerTipoSuperficiePorId(idTipoSuperficie);
            return tipoSuperficie;
        }
     }    
}
