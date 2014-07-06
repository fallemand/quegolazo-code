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
            /// Obtiene todos los tipos de superficie
            /// autor: Paula Pedrosa
            /// </summary>
            /// <returns>Lista genérica del objeto TipoSuperficie</returns>
            public List<TipoSuperficie> obtenerTodos()
            {
                try
                {
                     DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();

                List<TipoSuperficie> tiposSuperficie = new List<TipoSuperficie>();
                tiposSuperficie = daoTipoSuperficie.obtenerTodos();

                return tiposSuperficie;
                }
                catch (Exception ex)
                {
                    
                   throw new Exception(ex.Message);
                }
            }

        /// <summary>
        /// Obtiene un Tipo de Superficie por un Id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTipoSuperficie">Id Tipo de Superficie</param>
        /// <returns>Objeto Tipo de Superficie</returns>
            public TipoSuperficie obtenerTipoSuperficiePorId(int idTipoSuperficie)
            {
                try
                {
                    DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();
                    TipoSuperficie tipoSuperficie = null;
                    tipoSuperficie = daoTipoSuperficie.obtenerTipoSuperficiePorId(idTipoSuperficie);

                    return tipoSuperficie;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
 
            }
     }
    
}
