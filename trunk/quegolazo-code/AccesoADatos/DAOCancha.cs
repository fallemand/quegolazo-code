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
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTamanioCancha">id del tamaño de cancha</param>
        /// <returns>objeto TamanioCancha, o null sino lo encuentra</returns>
        public TamanioCancha obtenerTamanioCanchaPorId(int idTamanioCancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            TamanioCancha respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;                
                string sql = @"SELECT *
                                FROM TamaniosCancha
                                WHERE idTamanioCancha= @idTamanioCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTamanioCancha", idTamanioCancha);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new TamanioCancha()
                    {
                        idTamanioCancha = Int32.Parse(dr["idTamanioCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        cantidadJugadores = Int32.Parse(dr["cantidadJugadores"].ToString())

                    };
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el tamaño de cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        /// <summary>
        /// Obtiene Todos los Tamaños de Cancha
        /// autor: Paula Pedrosa
        /// </summary>
       /// <returns>Una lista de objeto TamanioCancha, o null sino lo encuentra</returns>
        public List<TamanioCancha> obtenerTodos()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            List<TamanioCancha> tamaniosCancha = new List<TamanioCancha>();
            SqlDataReader dr;
            TamanioCancha respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TamaniosCancha";
                cmd.Parameters.Clear();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new TamanioCancha()
                    {
                        idTamanioCancha = Int32.Parse(dr["idTamanioCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        cantidadJugadores = Int32.Parse(dr["cantidadJugadores"].ToString())
                    };
                    tamaniosCancha.Add(respuesta);
                }
                dr.Close();
                return tamaniosCancha;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los tamaños de cancha: " + ex.Message);
            }
            finally
            {                
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        /// <summary>
        /// Obtiene todas las canchas de una edición
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idEdicion">id de la Edición</param>
        /// <returns>Lista de objeto Cancha, o null sino existen canchas de esa edición</returns>
        public List<Cancha> obtenerCanchasDeEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Cancha> canchas = null;
            Cancha unaCancha = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT c.*
                               FROM Canchas c INNER JOIN CanchaXEdicion ce 
                               ON c.idCancha = ce.idCancha
                               WHERE ce.idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   canchas = new List<Cancha>();
                   unaCancha = new Cancha();
                   unaCancha.idCancha = Int32.Parse(dr["idCancha"].ToString());
                   unaCancha.nombre = dr["nombre"].ToString();
                   unaCancha.telefono = dr["telefono"].ToString();
                   unaCancha.domicilio = dr["domicilio"].ToString();
                   canchas.Add(unaCancha);
                }
                dr.Close();
                return canchas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

        }

    }
}
