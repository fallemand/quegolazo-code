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

        /// <summary>
        /// Modifica un delegado
        /// </summary>
        /// <param name="delegado">Delegado a modificar</param>
        public void modificarDelegado(Delegado delegado)
        {
            try
            {
                DAODelegado daoDelegado = new DAODelegado();
                daoDelegado.modificarDelegado(delegado);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int obtenerDelegadoPorNombre(string nombre)
        {
            try
            {
                DAODelegado daoDelegado = new DAODelegado();
                int idDelegado = daoDelegado.obtenerDelegadoPorNombre(nombre);
                return idDelegado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina de la Base de Datos un delegado
        /// </summary>
        /// <param name="delegado">Delegado a eliminar</param>
        public void eliminarDelegado(Delegado delegado)
        {
            try
            {
                DAODelegado daoDelegado = new DAODelegado();
                daoDelegado.eliminarDelegado(delegado);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
