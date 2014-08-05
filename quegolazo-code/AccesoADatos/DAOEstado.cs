using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoADatos
{
    public class DAOEstado
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Obtiene un estado de la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idEstado">id del Estado</param>
        /// <returns>Un objeto de tipo Estado</returns>
        public Estado obtenerEstadoPorId(int idEstado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT e.idEstado, e.nombre, e.idAmbito, a.nombre as nombreAmbito
                             FROM Estados e, EstadoAmbitos a
                             WHERE e.idEstado = @idEstado AND e.idAmbito=a.idAmbito";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                Estado respuesta = null;
                while (dr.Read())
                {
                    Estado nuevoEstado = new Estado()
                    {
                        idEstado = Int32.Parse(dr["idEstado"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        ambito = new Ambito() {
                            idAmbito = Int32.Parse(dr["idAmbito"].ToString()),
                            nombre = dr["nombreAmbito"].ToString()
                        }
                    };
                    respuesta = nuevoEstado;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
