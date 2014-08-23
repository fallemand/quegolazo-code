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
    public class DAOJugador
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Registra un nuevo Jugador en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public int registrarJugador(Jugador jugador, int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Jugadores (nombre, dni, fechaNacimiento, telefono, email, facebook, sexo, tieneFichaMedica, idEquipo)
                                    VALUES (@nombre, @dni, @fechaNacimiento, @telefono, @email, @facebook, @sexo, @tieneFichaMedica, @idEquipo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", jugador.nombre);
                cmd.Parameters.AddWithValue("@dni", jugador.dni);
                if(jugador.fechaNacimiento != null)
                    cmd.Parameters.AddWithValue("@fechaNacimiento", jugador.fechaNacimiento);
                else
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                if (jugador.telefono != null)
                    cmd.Parameters.AddWithValue("@telefono", jugador.telefono);
                else
                    cmd.Parameters.AddWithValue("@telefono", DBNull.Value);
                if (jugador.email != null)
                    cmd.Parameters.AddWithValue("@email", jugador.email);
                else
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                if (jugador.facebook != null)
                    cmd.Parameters.AddWithValue("@facebook", jugador.facebook);
                else
                    cmd.Parameters.AddWithValue("@facebook", DBNull.Value);
                cmd.Parameters.AddWithValue("@sexo", jugador.sexo);
                cmd.Parameters.AddWithValue("@tieneFichaMedica", jugador.tieneFichaMedica);
                cmd.Parameters.AddWithValue("@idEquipo", idEquipo);             
                cmd.CommandText = sql;
                int idJugador = int.Parse(cmd.ExecuteScalar().ToString());
                return idJugador; //retorna el id del jugador generado por la BD
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_idEquipo"))
                    throw new Exception("El jugador " + jugador.nombre + " ya se encuentra registrado. Ingrese otro nombre.");
                if (ex.Message.Contains("unique_dni"))
                    throw new Exception("Ese DNI ya existe");
                throw new Exception("No se pudo registrar el jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
               
        /// <summary>
        /// Obtiene todos los jugadores de un Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Jugador> obtenerJugadoresDeUnEquipo(int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Jugador> respuesta = new List<Jugador>();
            Jugador jugador = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM Jugadores
                                WHERE idEquipo = @idEquipo
                                ORDER BY idJugador DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    jugador = new Jugador()
                    {
                        idJugador = Int32.Parse(dr["idJugador"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        dni = dr["dni"].ToString(),
                        fechaNacimiento = DateTime.Parse(dr["fechaNacimiento"].ToString()),
                        telefono = dr["telefono"].ToString(),
                        email = dr["email"].ToString(),
                        facebook = dr["facebook"].ToString(),
                        sexo = dr["sexo"].ToString(),
                        tieneFichaMedica = bool.Parse(dr["tieneFichaMedica"].ToString())
                    };
                    respuesta.Add(jugador);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los jugadores:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene un Jugador por Id
        /// autor: Pau Pedrosa
        /// </summary>
        public Jugador obtenerJugadorPorId(int idJugador)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Jugador respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Jugadores
                                WHERE idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idJugador", idJugador);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Jugador();
                    respuesta.idJugador = Int32.Parse(dr["idJugador"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.dni = dr["dni"].ToString();
                    if (dr["fechaNacimiento"] != System.DBNull.Value)
                        respuesta.fechaNacimiento = DateTime.Parse(dr["fechaNacimiento"].ToString());                                 
                    else
                        respuesta.fechaNacimiento = null;
                    if (dr["telefono"] != System.DBNull.Value)
                        respuesta.telefono = dr["telefono"].ToString();
                    else
                        respuesta.telefono = null;
                    if (dr["email"] != System.DBNull.Value)
                        respuesta.email = dr["email"].ToString();
                    else
                        respuesta.email = null;
                    if (dr["facebook"] != System.DBNull.Value)
                        respuesta.facebook = dr["facebook"].ToString();
                    else
                        respuesta.facebook = null;
                    respuesta.sexo = dr["sexo"].ToString();
                    respuesta.tieneFichaMedica = bool.Parse(dr["tieneFichaMedica"].ToString());
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Permite modificar un jugador en la BD 
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarJugador(Jugador jugador)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Jugadores
                                SET nombre = @nombre, dni = @dni, fechaNacimiento = @fechaNacimiento, telefono = @telefono, email = @email, facebook = @facebook,
                                sexo = @sexo, tieneFichaMedica = @tieneFichaMedica
                                WHERE idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", jugador.nombre);
                cmd.Parameters.AddWithValue("@dni", jugador.dni);
                cmd.Parameters.AddWithValue("@idJugador", jugador.idJugador);
                if (jugador.fechaNacimiento != null)
                    cmd.Parameters.AddWithValue("@fechaNacimiento", jugador.fechaNacimiento);
                else
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                if (jugador.telefono != null)
                    cmd.Parameters.AddWithValue("@telefono", jugador.telefono);
                else
                    cmd.Parameters.AddWithValue("@telefono", DBNull.Value);
                if (jugador.email != null)
                    cmd.Parameters.AddWithValue("@email", jugador.email);
                else
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                if (jugador.facebook != null)
                    cmd.Parameters.AddWithValue("@facebook", jugador.facebook);
                else
                    cmd.Parameters.AddWithValue("@facebook", DBNull.Value);
                cmd.Parameters.AddWithValue("@sexo", jugador.sexo);
                cmd.Parameters.AddWithValue("@tieneFichaMedica", jugador.tieneFichaMedica);               
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Si ya existe un jugador con ese nombre en ese equipo
                if (ex.Message.Contains("unique_nombre_idEquipo"))
                    throw new Exception("Ya existe un jugador registrado con este nombre, por favor cambielo e intente nuevamente.");
                throw new Exception("No se pudo modificar el jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Eliminar un jugador de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarJugador(int idJugador)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Jugadores
                                WHERE idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idJugador", idJugador);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el Jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
