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

        /// <summary>
        /// Busca un TamanioCancha por su Id
        /// </summary>
        /// <param name="idTamanioCancha">id del tamaño de cancha</param>
        /// <returns>objeto TamanioCancha, o null sino lo encuentra</returns>
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
                        cantidadJugadores = Int32.Parse(dr["cantidadJugadores"].ToString())

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


        /// <summary>
        /// Obtiene todas las canchas de una edición
        /// </summary>
        /// <param name="idEdicion">id de la Edición</param>
        /// <returns>Lista de objeto Cancha, o null sino existen canchas de esa edición</returns>
        public List<Cancha> obtenerCanchasDeEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            List<Cancha> canchas = null;
            Cancha c = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT c.*
                               FROM Canchas c INNER JOIN CanchaXEdicion ce 
                               ON c.idCancha = ce.idCancha
                               WHERE ce.idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                   canchas = new List<Cancha>();
                   c = new Cancha();
                   c.idCancha = Int32.Parse(dr["idCancha"].ToString());
                   c.nombre = dr["nombre"].ToString();
                   c.telefono = dr["telefono"].ToString();
                   c.domicilio = dr["domicilio"].ToString();

                   canchas.Add(c);
                }
                return canchas;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar los datos: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

    }
}
