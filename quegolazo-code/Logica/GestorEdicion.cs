using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEdicion
    {
        /// <summary>
        /// Obtener ediciones de un torneo en particular
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista genérica del objeto Ediciones</returns>
        public List<Edicion> obtenerEdicionesPorIdTorneo(int idTorneo)
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                List<Edicion> ediciones = daoEdicion.obtenerEdicionesPorIdTorneo(idTorneo);
                return ediciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
 
        }

        /// <summary>
        /// Obtiene la Forma de Puntuacion
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="ganado">Cantidad de puntos por partido ganado</param>
        /// <param name="perdido">Cantidad de puntos por partido perdido</param>
        /// <param name="empatado">Cantidad de puntos por empatado</param>
        /// <returns>Objeto FormaPuntuacion</returns>
        public FormaPuntuacion obtenerFormaPuntuacionPorGanadoEmpatadoPerdido(int ganado, int perdido, int empatado)
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                FormaPuntuacion formaPuntuacion = null;
                formaPuntuacion = daoEdicion.obtenerFormaPuntuacionPorGanadoEmpatadoPerdido(ganado, perdido, empatado);

                return formaPuntuacion;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Registra una nueva edicion
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="edicionNueva">Objeto Edicion</param>
        public void registrarEdicion(Edicion edicionNueva)
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                daoEdicion.registrarEdicion(edicionNueva);
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
 
        }
    }
}
