using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEstado
    {
        /// <summary>
        /// Obtener un Estado por Nombre 
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nombre">Nombre del Estado</param>
        /// <param name="ambito">Nombre del Ámbito</param>
        /// <returns>Objeto Estado</returns>
        public Estado obtenerUnEstadoPorNombre(Estado.enumNombre nombre, Estado.enumAmbito ambito)
        {
            try
            {
                DAOEstado daoEstado = new DAOEstado();
                Estado estado = null;
                estado = daoEstado.obtenerUnEstadoPorNombreYAmbito(nombre, ambito);

                return estado;
            }
            catch (Exception ex) 
            {
                 throw new Exception(ex.Message);
            }
        }
    }
}
