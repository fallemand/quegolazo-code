using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Utils
{
    public static class OperacionesAccesoADatos
    {
        /// <summary>
        /// Realiza una conexiona a la base de datos, siempre y cuando la misma se encuentr en estado "Closed"
        /// </summary>
        /// <param name="conexion">El objeto SqlConnection que se quiere conectar.</param>
        /// <param name="comando">El objeto SqlCommand que se quiere asociar a la conexion.</param>
        public static void conectar(SqlConnection conexion, SqlCommand comando)
        {
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
                comando.Connection = conexion;
            }
        }

    }
}
