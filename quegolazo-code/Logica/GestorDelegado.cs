using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorDelegado
    {
        /// <summary>
        /// Registra un nuevo delegado en la Base de Datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nuevoDelegado">Objeto Delegado a registrar</param>
        /// <returns>Id del nuevo Delegado registrado</returns>
        public int registrarDelegado(Delegado nuevoDelegado)
        {
            try
            {
                DAODelegado daoDelegado = new DAODelegado();
                int idNuevoDelegado = daoDelegado.registrarDelegado(nuevoDelegado);
                return idNuevoDelegado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }


        /// <summary>
        /// Obtiene un Delegado por id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idDelegado">Id del delegado a obtener</param>
        /// <returns>Objeto delegado</returns>
        public Delegado obtenerDelegadoPorId(int idDelegado)
        {
            try
            {
                DAODelegado daoDelegado = new DAODelegado();
                Delegado delegado = daoDelegado.obtenerDelegadoPorId(idDelegado);
                return delegado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

              
    }
}
