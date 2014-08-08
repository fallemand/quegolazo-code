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
    public class DAOTipoUsuario
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Obtiene el Tipo Usuario por id de Tipo usuario
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTipoUsuario">id de Tipo usuario que se desea buscar</param>
        /// <returns>Un Objeto Tipo Usuario, o null sino lo encuentra</returns>
        public TipoUsuario obtenerTipoUsuarioPorId(int idTipoUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            TipoUsuario respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open(); 
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TiposUsuario
                                WHERE idTipoUsuario = @idTipoUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new TipoUsuario()
                    {
                        idTipoUsuario = Int32.Parse(dr["idTipoUsuario"].ToString()),
                        nombre = dr["nombre"].ToString(),
                    };
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el tipo de usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
     }
}
