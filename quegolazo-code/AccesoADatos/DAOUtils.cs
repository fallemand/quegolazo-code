using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public class DAOUtils
    {
        public static Object dbValueNull(Object value)
        {
            if (value == null || value.Equals(""))
                return DBNull.Value;
            return value;
        }

        public static Object dbValueInt(Object value)
        {
            if ((int) value == 0)
                return DBNull.Value;
            return value;
        }
    }
}
