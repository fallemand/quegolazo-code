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
    public class DAOEstadisticas
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

       
        public DataTable obtenerAvanceEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END) AS 'Partidos Jugados',
                                      COUNT(p.idPartido) AS 'Partidos', 
                                      COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END)*100/(COUNT(p.idPartido)) AS 'porcentajeAvance'
                                      FROM Partidos p where p.idEdicion=@idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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

        public DataTable obtenerAvanceFecha(int idEdicion, int idEstadoFecha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT top 1 f.idFecha, COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END) AS 'Partidos Jugados',
                                            COUNT(p.idPartido) AS 'Partidos', 
                                            COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END)*100/(COUNT(p.idPartido)) AS 'porcentajeAvance',
		                                    COUNT(CASE T.tipoTarjeta WHEN 'A' THEN 1 ELSE NULL END) AS 'AMARILLAS', 
                                            COUNT(CASE T.tipoTarjeta WHEN 'R' THEN 1 ELSE NULL END) AS 'ROJAS'
                                            FROM Partidos p 
	                                        INNER JOIN Fechas f ON p.idFecha=f.idFecha
	                                        LEFT JOIN Tarjetas t ON p.idPartido = t.idPartido
	                                        LEFT JOIN Sanciones s ON s.idPartido = s.idPartido
	                                        WHERE p.idEdicion=@idEdicion AND f.idFecha =(SELECT TOP 1 idFecha FROM Fechas WHERE idEstado=@idEstadoFecha ORDER BY idFecha DESC)
	                                        GROUP BY f.idFecha 
	                                        ORDER BY f.idFecha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEstadoFecha", idEstadoFecha));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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

        public DataTable obtenerFixtureUltimaFecha(int idEdicion, int idEstadoFecha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @" SELECT f.idFecha, elocal.nombre as 'Local', p.golesLocal , p.golesVisitante, eVisitante.nombre as 'Visitante',
		                            ar.nombre as 'arbitro', 
                                    can.nombre as 'Complejo/Cancha',
                                    p.fecha, 
                                    es.nombre as 'Estado'
	                            FROM Partidos p 
	                            INNER JOIN Fechas f on p.idFecha=f.idFecha
	                            INNER JOIN EquipoXEdicion exe on exe.idEdicion=p.idEdicion  
	                            INNER JOIN Equipos elocal on p.idEquipoLocal=elocal.idEquipo
	                            INNER JOIN Equipos eVisitante on p.idEquipoVisitante=eVisitante.idEquipo
	                            LEFT JOIN Arbitros ar ON p.idArbitro=ar.idArbitro
	                            LEFT JOIN Canchas can ON p.idCancha=can.idCancha
	                            LEFT JOIN Estados es ON p.idEstado=es.idEstado
	                            WHERE p.idEdicion=@idEdicion AND f.idFecha=(SELECT TOP 1 idFecha FROM Fechas WHERE idEstado=@idEstadoFecha ORDER BY idFecha)
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal ,  eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre,  p.fecha, es.nombre
	                            ORDER BY f.idFecha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEstadoFecha", idEstadoFecha));//9 incompleta , 8 completa 
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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

        public DataTable obtenerFixtureFecha(int idEdicion, int idFecha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @" SELECT f.idFecha, elocal.nombre as 'Local', p.golesLocal , p.golesVisitante, eVisitante.nombre as 'Visitante',
		                                ar.nombre as 'Árbitro', can.nombre as 'Complejo/Cancha', p.fecha, es.nombre as 'Estado'
	                               FROM Partidos p  
	                               INNER JOIN Fechas f on p.idFecha=f.idFecha
	                               INNER JOIN EquipoXEdicion exe on exe.idEdicion=p.idEdicion  
	                               INNER JOIN Equipos elocal on p.idEquipoLocal=elocal.idEquipo
	                               INNER JOIN Equipos eVisitante on p.idEquipoVisitante=eVisitante.idEquipo
	                               LEFT JOIN Arbitros ar ON p.idArbitro=ar.idArbitro
	                               LEFT JOIN Canchas can ON p.idCancha=can.idCancha
	                               LEFT JOIN Estados es ON p.idEstado=es.idEstado
	                               WHERE p.idEdicion=@idEdicion and f.idFecha=@idFecha
	                               GROUP BY f.idFecha, elocal.nombre, p.golesLocal ,  eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre,  p.fecha, es.nombre
	                               ORDER BY f.idFecha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFecha", idFecha));//9 incompleta , 8 completa 
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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

        public DataTable obtenerTablaPosiciones(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @" SELECT e.nombre as 'Equipo', COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END) as 'PJ',  
				                        COUNT(CASE p.idGanador WHEN e.idEquipo THEN 1 ELSE NULL END) as 'PG',
				                        COUNT(CASE P.empate WHEN 1 THEN 1 ELSE NULL END) as 'PE',
				                        COUNT(CASE p.idPerdedor WHEN e.idEquipo THEN 1 ELSE NULL END) as 'PP',
				                        SUM(CASE p.idEquipoLocal WHEN e.idEquipo THEN p.golesLocal ELSE 0 END)+ SUM(CASE p.idEquipoVisitante WHEN e.idEquipo THEN p.golesVisitante ELSE 0 END) as 'GF',
				                        SUM(CASE p.idEquipoLocal WHEN e.idEquipo THEN p.golesVisitante ELSE 0 END)+ SUM(CASE p.idEquipoVisitante WHEN e.idEquipo THEN p.golesLocal ELSE 0 END) as 'GC',
				                        COUNT(CASE p.idGanador WHEN e.idEquipo THEN 1 ELSE NULL END) * ed.puntosGanado + COUNT(CASE P.empate WHEN 1 THEN 1 ELSE NULL END)*ed.puntosEmpatado+ COUNT(CASE p.idPerdedor WHEN e.idEquipo THEN 1 ELSE NULL END)*ed.puntosPerdido as 'Puntos'
                                FROM  Partidos p 
	                            INNER JOIN EquipoXEdicion exe ON p.idEdicion=exe.idEdicion
	                            INNER JOIN Equipos e ON exe.idEquipo=e.idEquipo 
	                            INNER JOIN Ediciones ed ON ed.idEdicion=exe.idEdicion 
	                            WHERE e.idEquipo=p.idEquipoLocal OR e.idEquipo=p.idEquipoVisitante
	                            GROUP BY p.idEdicion, e.nombre, ed.puntosGanado, ed.puntosPerdido, ed.puntosEmpatado
								HAVING p.idEdicion=@idEdicion 
	                            ORDER BY 'Puntos' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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

        /// <summary>
        /// Devueleve la tabla de goleadores de una edición
        /// </summary>
        /// <param name="idEdicion">id Edicion</param>
        /// <returns></returns>
        public DataTable obtenerTablaGoleadores(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @" SELECT  j.nombre AS 'JUGADOR', e.nombre AS 'EQUIPO', count(g.idGol) AS 'GOLES'
                                    FROM Goles g
	                                     JOIN Equipos e ON e.idEquipo=g.idEquipo 
	                                     JOIN Jugadores j ON g.idJugador=j.idJugador
	                                     JOIN Partidos p ON p.idPartido=g.idPartido
	                                     GROUP BY p.idEdicion, j.nombre, E.nombre 
	                                     HAVING p.idEdicion=@idEdicion
	                                     ORDER BY 'GOLES' desc";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                tablaDeDatos.Load(dr);
                if (dr != null)
                    dr.Close();
                return tablaDeDatos;
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
