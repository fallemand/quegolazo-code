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
    public class DAOSancion
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        
        public int registrarSancion(Sancion sancion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DECLARE @idGrupo AS int = (SELECT idGrupo FROM Grupos WHERE idEdicion = @idEdicion AND idFase = @idFase)
                                INSERT INTO Sanciones (idEdicion, idFase, idGrupo, idFecha, idPartido, idEquipo, idJugador, fechaSancion, idMotivoSancion, observacion, puntosAQuitar, cantidadFechasSuspendidas)
                                    VALUES (@idEdicion, @idFase, @idGrupo, @idFecha, @idPartido, @idEquipo, @idJugador, @fechaSancion, @idMotivoSancion, @observacion, @puntosAQuitar, @cantidadFechasSuspendidas)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", DAOUtils.dbValueNull(sancion.idEdicion));
                cmd.Parameters.AddWithValue("@idFase", DAOUtils.dbValueNull(sancion.idFase));
                cmd.Parameters.AddWithValue("@idFecha", DAOUtils.dbValueNull(sancion.idFecha));
                cmd.Parameters.AddWithValue("@idPartido", DAOUtils.dbValueNull(sancion.idPartido));
                cmd.Parameters.AddWithValue("@idEquipo", DAOUtils.dbValueNull(sancion.idEquipo));
                cmd.Parameters.AddWithValue("@idJugador", DAOUtils.dbValueNull(sancion.idJugador));
                cmd.Parameters.AddWithValue("@fechaSancion", DAOUtils.dbValueNull(sancion.fechaSancion));
                cmd.Parameters.AddWithValue("@idMotivoSancion", DAOUtils.dbValueNull(sancion.motivoSancion.idMotivoSancion));
                cmd.Parameters.AddWithValue("@observacion", DAOUtils.dbValueNull(sancion.observacion));
                cmd.Parameters.AddWithValue("@puntosAQuitar", DAOUtils.dbValueNull(sancion.puntosAQuitar));
                cmd.Parameters.AddWithValue("@cantidadFechasSuspendidas", DAOUtils.dbValueNull(sancion.cantidadFechasSuspendidas));
                cmd.CommandText = sql;
                int idSancion = int.Parse(cmd.ExecuteScalar().ToString());
                return idSancion; //retorna el id de la sanción generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la sanción: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public List<MotivoSancion> obtenerMotivos()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<MotivoSancion> respuesta = new List<MotivoSancion>();
            MotivoSancion motivoSancion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                               FROM MotivosSancion";
                cmd.Parameters.Clear();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    motivoSancion = new MotivoSancion();
                    motivoSancion.idMotivoSancion = Int32.Parse(dr["idMotivoSancion"].ToString());
                    motivoSancion.nombre = dr["nombre"].ToString();
                    respuesta.Add(motivoSancion);
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

        public List<Sancion> obtenerSancionesDeUnaEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Sancion> respuesta = new List<Sancion>();
            Sancion sancion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT s.idSancion, s.idFecha AS 'Fecha', s.fechaSancion AS 'FechaSancion', e.nombre AS 'NombreEquipo', j.nombre AS 'NombreJugador',
                                m.nombre AS 'MotivoSancion', s.puntosAQuitar AS 'PtosAQuitar', s.cantidadFechasSuspendidas AS 'CantFechas'
                                FROM Sanciones s 
                                INNER JOIN Equipos e ON e.idEquipo = s.idEquipo
                                INNER JOIN Jugadores j ON j.idJugador = s.idJugador
                                INNER JOIN MotivosSancion m ON s.idMotivoSancion = m.idMotivoSancion
                                WHERE s.idEdicion = @idEdicion
                                ORDER BY s.idSancion DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sancion = new Sancion()
                    {
                        idSancion = Int32.Parse(dr["s.idSancion"].ToString()),
                        idEdicion = Int32.Parse(dr["sidEdicion"].ToString()),
                        idFase = (dr["idFase"] != DBNull.Value) ? (int?)int.Parse(dr["idFase"].ToString()) : null,
                        idGrupo = (dr["idGrupo"] != DBNull.Value) ? (int?)int.Parse(dr["idGrupo"].ToString()) : null,
                        idFecha = (dr["idFecha"] != DBNull.Value) ? (int?)int.Parse(dr["idFecha"].ToString()) : null,
                        idPartido = (dr["idPartido"] != DBNull.Value) ? (int?)int.Parse(dr["idPartido"].ToString()) : null,
                        idEquipo = Int32.Parse(dr["idEquipo"].ToString()),
                        idJugador = (dr["idJugador"] != DBNull.Value) ? (int?)int.Parse(dr["idJugador"].ToString()) : null,
                        fechaSancion = (dr["fechaSancion"] != DBNull.Value) ? (DateTime?)DateTime.Parse(dr["fechaSancion"].ToString()) : null,
                        motivoSancion = obtenerMotivoSancionPorId(int.Parse(dr["idMotivoSancion"].ToString())),
                        observacion = dr["observacion"].ToString(),
                        puntosAQuitar = (dr["puntosAQuitar"] != DBNull.Value) ? (int?)int.Parse(dr["puntosAQuitar"].ToString()) : null,
                        cantidadFechasSuspendidas = (dr["cantidadFechasSuspendidas"] != DBNull.Value) ? (int?)int.Parse(dr["cantidadFechasSuspendidas"].ToString()) : null,
                    };
                    respuesta.Add(sancion);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los árbitros:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public DataTable obtenerSancionesDeEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT s.idSancion, s.idFecha AS 'Fecha', s.fechaSancion AS 'FechaSancion', e.nombre AS 'NombreEquipo', j.nombre AS 'NombreJugador',
                                m.nombre AS 'MotivoSancion', s.puntosAQuitar AS 'PtosAQuitar', s.cantidadFechasSuspendidas AS 'CantFechas',
                                s.idEdicion AS 'IdEdicion'
                                FROM Sanciones s 
                                LEFT OUTER JOIN Equipos e ON e.idEquipo = s.idEquipo
                                LEFT OUTER JOIN Jugadores j ON j.idJugador = s.idJugador
                                LEFT OUTER JOIN MotivosSancion m ON s.idMotivoSancion = m.idMotivoSancion
                                WHERE s.idEdicion = 15
                                ORDER BY s.idSancion DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDatos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los árbitros:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }        

        public MotivoSancion obtenerMotivoSancionPorId(int idMotivoSancion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            MotivoSancion motivoSancion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM MotivosSancion
                                WHERE idMotivoSancion = @idMotivoSancion";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idMotivoSancion", idMotivoSancion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    motivoSancion = new MotivoSancion()
                    {
                        idMotivoSancion = Int32.Parse(dr["idMotivoSancion"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                }
                if (dr != null)
                    dr.Close();
                return motivoSancion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Motivos Sanción:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
