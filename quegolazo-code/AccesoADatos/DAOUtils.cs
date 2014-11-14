using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public class DAOUtils
    {
        /// <summary>
        /// Permite valuar una variable que obtiene por parámetro (value).
        /// Valua si value es null o está vacía. En este caso devuelve NULL de la BD
        /// Si no es null o no está vacía, devuelve el valor de dicha variable
        /// autor: Facu Allemand
        /// </summary>
        public static Object dbValueNull(Object value)
        {
            if (value == null || value.Equals(""))
                return DBNull.Value;
            return value;
        }

        /// <summary>
        /// Permite valuar una variable que obtiene por parámetro (value).
        /// Valua si value es 0. En este caso devuelve NULL de la BD
        /// Si no es 0, devuelve el valor de dicha variable
        /// autor: Pau Pedrosa
        /// </summary>
        public static Object dbValueInt(Object value)
        {
            if ((int) value == 0)
                return DBNull.Value;
            return value;
        }
    }
}
