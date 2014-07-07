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
    }
}
