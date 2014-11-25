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
        /// <summary>
        /// Permite obtener el avance de la edición a partir de su Id
        /// autor: Flor Rojas
        /// </summary>      
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
                                      FROM Partidos p where p.idEdicion = @idEdicion";
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
        /// Permite obtener el avance de la edición a partir de su Id
        /// autor: Flor Rojas
        /// </summary>  
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
	                                        WHERE p.idEdicion=@idEdicion AND f.idFecha =(SELECT TOP 1 idFecha FROM Fechas WHERE idEstado=@idEstadoFecha ORDER BY idFecha)
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

        /// <summary>
        /// Obtiene el Fixture de la última fecha
        /// autor: Flor Rojas
        /// </summary>
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

        /// <summary>
        /// Obtiene el fixture de la fecha
        /// autor: Flor Rojas
        /// </summary>
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

        /// <summary>
        /// Obtiene la tabla de Posiciones
        /// autor: Flor Rojas
        /// </summary>
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
                string sql = @"SELECT Equipo, COUNT(CASE idEstadoPartido WHEN 13 THEN 1 ELSE NULL END) AS 'PJ',  
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) AS 'PG',
                                COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END) AS 'PE',
                                COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END) AS 'PP',
                                SUM(CASE idEquipoLocal WHEN idEquipo THEN golesLocal ELSE 0 END) + SUM(CASE idEquipoVisitante WHEN idEquipo THEN golesVisitante ELSE 0 END) AS 'GF',
                                SUM(CASE idEquipoLocal WHEN idEquipo THEN golesVisitante ELSE 0 END)+ SUM(CASE idEquipoVisitante WHEN idEquipo THEN golesLocal ELSE 0 END) AS 'GC',
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos'
                                FROM viewTablaPosiciones
                                GROUP BY Equipo, idEdicion, nombre, puntosGanado, puntosPerdido, puntosEmpatado
                                HAVING idEdicion = @idEdicion
                                ORDER BY 'Puntos' DESC , 'PG' DESC, 'GF' DESC";
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
        /// Devuelve la tabla de goleadores de una edición
        /// autor: Flor Rojas
        /// </summary>
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
                string sql = @" SELECT TOP 10  j.nombre AS 'JUGADOR', e.nombre AS 'EQUIPO', count(g.idGol) AS 'GOLES'
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
