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
    public class DAOCancha
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        public TamanioCancha obtenerTamanioCanchaPorId(int idTamanioCancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            TamanioCancha respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM TamaniosCancha
                                WHERE idTamanioCancha= @idTamanioCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTamanioCancha", idTamanioCancha);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    respuesta = new TamanioCancha()
                    {
                        idTamanioCancha = Int32.Parse(dr["idTamanioCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        cantidadDeJugadores = Int32.Parse(dr["cantidadDeJugadores"].ToString())

                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el tamaño de cancha: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

    }
}
