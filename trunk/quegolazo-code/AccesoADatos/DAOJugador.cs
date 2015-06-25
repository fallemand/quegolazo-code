using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using Utils;

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
                string sql = @"INSERT INTO Jugadores (nombre, dni, fechaNacimiento, numeroCamiseta, telefono, email, facebook, sexo, tieneFichaMedica, idEquipo)
                                    VALUES (@nombre, @dni, @fechaNacimiento, @numeroCamiseta, @telefono, @email, @facebook, @sexo, @tieneFichaMedica, @idEquipo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", jugador.nombre);
                cmd.Parameters.AddWithValue("@dni", jugador.dni);
                cmd.Parameters.AddWithValue("@fechaNacimiento", DAOUtils.dbValueNull(jugador.fechaNacimiento));
                cmd.Parameters.AddWithValue("@numeroCamiseta", DAOUtils.dbValueNull(jugador.numeroCamiseta));
                cmd.Parameters.AddWithValue("@telefono", DAOUtils.dbValueNull(jugador.telefono));
                cmd.Parameters.AddWithValue("@email", DAOUtils.dbValueNull(jugador.email));
                cmd.Parameters.AddWithValue("@facebook", DAOUtils.dbValueNull(jugador.facebook));
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
                    jugador = new Jugador();
                    jugador.idJugador = Int32.Parse(dr["idJugador"].ToString());
                    jugador.nombre = dr["nombre"].ToString();
                    jugador.dni = dr["dni"].ToString();
                    jugador.fechaNacimiento = (dr["fechaNacimiento"] != DBNull.Value) ? (DateTime?) Validador.castDate(dr["fechaNacimiento"].ToString()) : null;
                    jugador.numeroCamiseta = (dr["numeroCamiseta"] != DBNull.Value) ? (Int32?) Int32.Parse(dr["numeroCamiseta"].ToString()) : null;                   
                    jugador.email = dr["email"].ToString();
                    jugador.facebook = dr["facebook"].ToString();
                    jugador.sexo = dr["sexo"].ToString();
                    jugador.tieneFichaMedica = bool.Parse(dr["tieneFichaMedica"].ToString());                   
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
                    respuesta.fechaNacimiento = (dr["fechaNacimiento"] != DBNull.Value) ? (DateTime?) Utils.Validador.castDate(dr["fechaNacimiento"].ToString()) : null;
                    respuesta.numeroCamiseta = (dr["numeroCamiseta"] != DBNull.Value) ? (Int32?) Int32.Parse(dr["numeroCamiseta"].ToString()) : null;
                    respuesta.telefono = (dr["telefono"] != DBNull.Value) ? dr["telefono"].ToString() : null;
                    respuesta.email = (dr["email"] != DBNull.Value) ? dr["email"].ToString() : null;
                    respuesta.facebook = (dr["facebook"] != DBNull.Value) ? dr["facebook"].ToString() : null;
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
                                SET nombre = @nombre, dni = @dni, fechaNacimiento = @fechaNacimiento, numeroCamiseta = @numeroCamiseta, telefono = @telefono, email = @email, facebook = @facebook,
                                sexo = @sexo, tieneFichaMedica = @tieneFichaMedica
                                WHERE idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", jugador.nombre);
                cmd.Parameters.AddWithValue("@dni", jugador.dni);
                cmd.Parameters.AddWithValue("@idJugador", jugador.idJugador);
                cmd.Parameters.AddWithValue("@fechaNacimiento", DAOUtils.dbValueNull(jugador.fechaNacimiento));
                cmd.Parameters.AddWithValue("@numeroCamiseta", DAOUtils.dbValueNull(jugador.numeroCamiseta));
                cmd.Parameters.AddWithValue("@telefono", DAOUtils.dbValueNull(jugador.telefono));
                cmd.Parameters.AddWithValue("@email", DAOUtils.dbValueNull(jugador.email));
                cmd.Parameters.AddWithValue("@facebook", DAOUtils.dbValueNull(jugador.facebook));
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
                if (ex.Message.Contains("Goles"))
                    throw new Exception("No se puede eliminar ese Jugador: TIENE GOLES ASOCIADOS");
                if (ex.Message.Contains("Cambios"))
                    throw new Exception("No se puede eliminar ese Jugador: TIENE CAMBIOS ASOCIADOS");
                if (ex.Message.Contains("Tarjetas"))
                    throw new Exception("No se puede eliminar ese Jugador: TIENE TARJETAS ASOCIADAS");
                //if (ex.Message.Contains("Titulares"))
                //    throw new Exception("No se puede eliminar ese Jugador: TIENE PARTIDOS JUGADOS");
                if (ex.Message.Contains("Sanciones"))
                    throw new Exception("No se puede eliminar ese Jugador: TIENE SANCIONES ASOCIADAS");
                throw new Exception("No se pudo eliminar el Jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public List<Jugador> obtenerJugadoresGoleadores(int idEdicion)
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
                string sql = @" SELECT TOP 10 j.idJugador AS 'IDJUGADOR', j.nombre AS 'JUGADOR', e.idEquipo AS 'IDEQUIPO', e.nombre AS 'EQUIPO', count(g.idGol) AS 'GOLES'
                                FROM Goles g
	                            JOIN Equipos e ON e.idEquipo = g.idEquipo 
	                            JOIN Jugadores j ON g.idJugador = j.idJugador
	                            JOIN Partidos p ON p.idPartido = g.idPartido
	                            GROUP BY j.idJugador, p.idEdicion, j.nombre, e.nombre, e.idEquipo 
	                            HAVING p.idEdicion = @idEdicion
	                            ORDER BY 'GOLES' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    jugador = new Jugador();
                    jugador.idJugador = Int32.Parse(dr["IDJUGADOR"].ToString());
                    jugador.nombre = dr["JUGADOR"].ToString();
                    jugador.cantidadGoles = Int32.Parse(dr["GOLES"].ToString());
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

        public int obtenerIdEquipo(int idJugador)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            int idEquipo = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT j.idEquipo AS 'IDEQUIPO'
                                FROM Jugadores j 
                                WHERE j.idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idEquipo = Int32.Parse(dr["IDEQUIPO"].ToString());
                }
                if (dr != null)
                    dr.Close();
                return idEquipo;
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
    }
}
