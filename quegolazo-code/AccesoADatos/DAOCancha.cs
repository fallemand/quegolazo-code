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
                string sql = @"SELECT *
                               FROM CanchaXEdicion 
                               WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cancha = new Cancha();
                    cancha = obtenerCanchaPorId(Int32.Parse(dr["idCancha"].ToString()));
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
        
        /// <summary>
        /// Registra en la BD una nueva Cancha
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="cancha">Objeto Cancha a registrar</param>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>El id de la cancha generado por la BD</returns>
        public int registrarCancha(Cancha cancha, int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Canchas (nombre, domicilio, telefono, idTorneo)
                                    VALUES (@nombre, @domicilio, @telefono, @idTorneo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", cancha.nombre);
                cmd.Parameters.AddWithValue("@domicilio", DAOUtils.dbValueNull(cancha.domicilio));
                cmd.Parameters.AddWithValue("@telefono", DAOUtils.dbValueNull(cancha.telefono));              
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                int idCancha = int.Parse(cmd.ExecuteScalar().ToString());
                return idCancha; //retorna el id de la cancha generado por la BD
            }
            catch (SqlException ex)
            {   //excepción de BD, por clave unique
                if (ex.Class == 14 && ex.Number == 2601)
                    throw new Exception("La Cancha " + cancha.nombre + " ya se encuentra registrada. Ingrese otro nombre.");
                else
                    throw new Exception("No se pudo registrar la cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene de la Bd las canchas de un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista genérica de objetos Cancha</returns>
        public List<Cancha> obtenerCanchasDeUnTorneo(int idTorneo)
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
                string sql = @"SELECT * 
                                FROM Canchas
                                WHERE idTorneo = @idTorneo
                                ORDER BY idCancha DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cancha = new Cancha()
                    {
                        idCancha = Int32.Parse(dr["idCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        domicilio = dr["domicilio"].ToString(),
                        telefono = dr["telefono"].ToString()
                    };
                    respuesta.Add(cancha);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                    throw new Exception("Error al obtener las canchas:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene de la Bd la cancha por id
        /// autor: Facundo Allemand
        /// </summary>
        public Cancha obtenerCanchasPorId(int idCancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Cancha cancha = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM Canchas
                                WHERE idCancha = @idCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idCancha", idCancha));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cancha = new Cancha()
                    {
                        idCancha = Int32.Parse(dr["idCancha"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        domicilio = dr["domicilio"].ToString(),
                        telefono = dr["telefono"].ToString()
                    };
                }
                if (dr != null)
                    dr.Close();
                return cancha;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cancha:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica en la BD una Cancha registrada
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="cancha">Objeto Cancha con los datos modificados</param>
        public void modificarCancha(Cancha cancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Canchas
                                SET nombre = @nombre, domicilio = @domicilio, telefono = @telefono
                                WHERE idCancha = @idCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", cancha.nombre);
                cmd.Parameters.AddWithValue("@idCancha", cancha.idCancha);
                cmd.Parameters.AddWithValue("@domicilio", DAOUtils.dbValueNull(cancha.domicilio));
                cmd.Parameters.AddWithValue("@telefono", DAOUtils.dbValueNull(cancha.telefono));                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Si ya existe una cancha con ese nombre en ese torneo
                if (ex.Message.Contains("unique_nombreCancha_idTorneo"))
                    throw new Exception("Ya existe una cancha registrada con este nombre, por favor cambielo e intente nuevamente.");
                throw new Exception("No se pudo modificar la cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene una cancha de la BD por su Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de la Cancha a buscar</param>
        /// <returns>Objeto Cancha</returns>
        public Cancha obtenerCanchaPorId(int idCancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Cancha respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Canchas
                                WHERE idCancha = @idCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idCancha", idCancha);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Cancha();
                    respuesta.idCancha = Int32.Parse(dr["idCancha"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.domicilio = (dr["domicilio"] != DBNull.Value) ? dr["domicilio"].ToString() : null;
                    respuesta.telefono = (dr["telefono"] != DBNull.Value) ? dr["telefono"].ToString() : null;
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar la Cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina una cancha de la Bd 
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id de la cancha a eliminar</param>
        public void eliminarCancha(int idCancha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Canchas
                                WHERE idCancha = @idCancha";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idCancha", idCancha);                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {// si tiene edicion asociada
                if (ex.Message.Contains("FK_CanchaXEdicion_Canchas"))
                    throw new Exception("La Cancha que desea eliminar está asociada a alguna Edición");
                throw new Exception("No se pudo eliminar la cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
