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
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTamanioCancha">Id del tamaño de cancha</param>
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
                                WHERE idTamanioCancha = @idTamanioCancha";
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
        /// Obtiene Todos los Tamanios de Cancha
        /// autor: Pau Pedrosa
        /// </summary>
       /// <returns>Una lista de objeto TamanioCancha, o null sino lo encuentra</returns>
        public List<TamanioCancha> obtenerTodos()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<TamanioCancha> respuesta = new List<TamanioCancha>();
            TamanioCancha tamanioCancha = null;
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
                    tamanioCancha = new TamanioCancha()
                    {
                        idTamanioCancha = Int32.Parse(dr["idTamanioCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        cantidadJugadores = Int32.Parse(dr["cantidadJugadores"].ToString())
                    };
                    respuesta.Add(tamanioCancha);
                }
                dr.Close();
                return respuesta;
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
        /// Obtiene todas las canchas de una Edición
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEdicion">Id de la Edición</param>
        /// <returns>Lista de objetos Cancha, o null sino existen canchas de esa edición</returns>
        public List<Cancha> obtenerCanchasDeEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Cancha> respuesta = new List<Cancha>();
            Cancha cancha = null;
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
                   cancha = new Cancha();
                   cancha.idCancha = Int32.Parse(dr["idCancha"].ToString());
                   cancha.nombre = dr["nombre"].ToString();
                   cancha.telefono = dr["telefono"].ToString();
                   cancha.domicilio = dr["domicilio"].ToString();
                   respuesta.Add(cancha);
                }
                dr.Close();
                return respuesta;
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
