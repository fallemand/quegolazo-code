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
                string sql = @"SELECT COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'Partidos Jugados',
                                      COUNT(p.idPartido) AS 'Partidos', 
                                      COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END)*100/(COUNT(p.idPartido)) AS 'porcentajeAvance'
                                      FROM Partidos p where p.idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
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
        public DataTable obtenerAvanceFecha(int idEdicion, int idFase)
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
                string sql = @"SELECT top 1 f.idFecha, COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'Partidos Jugados',
                                            COUNT(p.idPartido) AS 'Partidos', 
                                            COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END)*100/(COUNT(p.idPartido)) AS 'porcentajeAvance'
                                            FROM Partidos p 
	                                        INNER JOIN Fechas f ON p.idFecha = f.idFecha
	                                        WHERE p.idEdicion = @idEdicion AND p.idFase = @idFase
                                            AND f.idFecha =(SELECT TOP 1 idFecha FROM Fechas WHERE idEstado = @estadoIncompleta AND idEdicion = p.idEdicion AND idFase = p.idFase ORDER BY idFecha DESC)
	                                        GROUP BY f.idFecha 
	                                        ORDER BY f.idFecha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@estadoIncompleta", Estado.fechaINCOMPLETA));
                cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    tablaDeDatos.Load(dr);
                else
                {
                    if (dr != null)
                        dr.Close();
                    sql = @"SELECT top 1 f.idFecha, COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'Partidos Jugados',
                                            COUNT(p.idPartido) AS 'Partidos', 
                                            COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END)*100/(COUNT(p.idPartido)) AS 'porcentajeAvance'
                                            FROM Partidos p 
	                                        INNER JOIN Fechas f ON p.idFecha=f.idFecha 
	                                        WHERE p.idEdicion = @idEdicion AND p.idFase = @idFase
                                            AND f.idFecha =(SELECT TOP 1 idFecha FROM Fechas WHERE idEstado = @estadoCompleta AND idEdicion = p.idEdicion AND idFase = p.idFase ORDER BY idFecha DESC)
	                                        GROUP BY f.idFecha 
	                                        ORDER BY f.idFecha";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                    cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                    cmd.Parameters.Add(new SqlParameter("@estadoCompleta", Estado.fechaCOMPLETA));
                    cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    tablaDeDatos.Load(dr);
                }
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
        public DataTable obtenerFixtureUltimaFecha(int idEdicion, int idFase)
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
                string sql = @" SELECT f.idFecha, elocal.nombre AS 'Local', p.golesLocal AS 'GolesLocal', 
                                p.golesVisitante AS 'GolesVisitante', eVisitante.nombre AS 'Visitante',
                                ar.nombre AS 'Arbitro', 
                                can.nombre AS 'Complejo/Cancha',
                                CONVERT (char(10), p.fecha, 103) AS 'FechaPartido', 
                                es.nombre AS 'Estado'
	                            FROM Partidos p 
	                            INNER JOIN Fechas f on p.idFecha = f.idFecha
	                            INNER JOIN EquipoXEdicion exe on exe.idEdicion = p.idEdicion  
	                            INNER JOIN Equipos elocal on p.idEquipoLocal = elocal.idEquipo
	                            INNER JOIN Equipos eVisitante on p.idEquipoVisitante = eVisitante.idEquipo
	                            LEFT JOIN Arbitros ar ON p.idArbitro = ar.idArbitro
	                            LEFT JOIN Canchas can ON p.idCancha = can.idCancha
	                            LEFT JOIN Estados es ON p.idEstado = es.idEstado
	                            WHERE p.idEdicion = @idEdicion AND p.idFase = @idFase
                                AND f.idFecha = (SELECT TOP 1 idFecha FROM Fechas WHERE idEstado = @idEstadoFecha AND idEdicion = @idEdicion AND idFase = @idFase ORDER BY idFecha DESC)
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal ,  eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre,  p.fecha, es.nombre
	                            ORDER BY f.idFecha";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@idEstadoFecha", Estado.fechaINCOMPLETA));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    tablaDeDatos.Load(dr);
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    sql = @" SELECT f.idFecha, elocal.nombre AS 'Local', p.golesLocal, 
                                    p.golesVisitante, eVisitante.nombre AS 'Visitante',
		                            ar.nombre AS 'Arbitro', 
                                    can.nombre AS 'Complejo/Cancha',
                                    CONVERT (char(10), p.fecha, 103) AS 'FechaPartido', 
                                    es.nombre AS 'Estado'
	                            FROM Partidos p 
	                            INNER JOIN Fechas f on p.idFecha = f.idFecha
	                            INNER JOIN EquipoXEdicion exe on exe.idEdicion = p.idEdicion  
	                            INNER JOIN Equipos elocal on p.idEquipoLocal = elocal.idEquipo
	                            INNER JOIN Equipos eVisitante on p.idEquipoVisitante = eVisitante.idEquipo
	                            LEFT JOIN Arbitros ar ON p.idArbitro = ar.idArbitro
	                            LEFT JOIN Canchas can ON p.idCancha = can.idCancha
	                            LEFT JOIN Estados es ON p.idEstado = es.idEstado
	                            WHERE p.idEdicion = @idEdicion AND p.idFase = @idFase
                                AND f.idFecha = (SELECT TOP 1 idFecha FROM Fechas WHERE idEstado = @idEstadoFecha AND idEdicion = @idEdicion AND idFase = @idFase ORDER BY idFecha DESC)
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal, eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre, p.fecha, es.nombre
	                            ORDER BY f.idFecha";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                    cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                    cmd.Parameters.Add(new SqlParameter("@idEstadoFecha", Estado.fechaCOMPLETA));
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        tablaDeDatos.Load(dr);
                        dr.Close();
                    }
                    else
                    {
                        dr.Close();
                        sql = @" SELECT f.idFecha, elocal.nombre AS 'Local', p.golesLocal, 
                                    p.golesVisitante, eVisitante.nombre AS 'Visitante',
		                            ar.nombre AS 'Arbitro', 
                                    can.nombre AS 'Complejo/Cancha',
                                    CONVERT (char(10), p.fecha, 103) AS 'FechaPartido',
                                    es.nombre AS 'Estado'
	                            FROM Partidos p 
	                            INNER JOIN Fechas f on p.idFecha = f.idFecha
	                            INNER JOIN EquipoXEdicion exe on exe.idEdicion = p.idEdicion  
	                            INNER JOIN Equipos elocal on p.idEquipoLocal = elocal.idEquipo
	                            INNER JOIN Equipos eVisitante on p.idEquipoVisitante = eVisitante.idEquipo
	                            LEFT JOIN Arbitros ar ON p.idArbitro = ar.idArbitro
	                            LEFT JOIN Canchas can ON p.idCancha = can.idCancha
	                            LEFT JOIN Estados es ON p.idEstado = es.idEstado
	                            WHERE p.idEdicion = @idEdicion AND p.idFase = @idFase
                                AND f.idFecha = (SELECT TOP 1 idFecha FROM Fechas WHERE idEstado = @idEstadoFecha AND idEdicion = @idEdicion AND idFase = @idFase ORDER BY idFecha ASC)
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal, eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre, p.fecha, es.nombre
	                            ORDER BY f.idFecha";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                        cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                        cmd.Parameters.Add(new SqlParameter("@idEstadoFecha", Estado.fechaDIAGRAMADA));
                        cmd.CommandText = sql;
                        dr = cmd.ExecuteReader();
                        tablaDeDatos.Load(dr);
                        dr.Close();
                    }
                }
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
        public DataTable obtenerTablaPosiciones(int idEdicion, int idFase, int idGrupo)
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
                string sql = @"SELECT Equipo, idGrupo, idEquipo, COUNT(CASE idEstadoPartido WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',  
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) AS 'PG',
                                COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END) AS 'PE',
                                COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END) AS 'PP',
                                SUM(CASE WHEN (idEquipoLocal = idEquipo AND golesLocal IS NOT NULL) THEN golesLocal ELSE 0 END) + SUM(CASE WHEN (idEquipoVisitante = idEquipo AND golesVisitante IS NOT NULL) THEN golesVisitante ELSE 0 END) AS 'GF',
                                SUM(CASE WHEN (idEquipoLocal = idEquipo AND golesVisitante IS NOT NULL) THEN golesVisitante ELSE 0 END) + SUM(CASE WHEN (idEquipoVisitante = idEquipo AND golesLocal IS NOT NULL) THEN golesLocal ELSE 0 END) AS 'GC',
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos'
                                FROM dbo.joinTablaDePosiciones(@idEdicion, @idFase, @idGrupo)
                                GROUP BY Equipo, idEdicion,idEquipo, nombre, puntosGanado, puntosPerdido, puntosEmpatado
                                ORDER BY 'Puntos' DESC , 'PG' DESC, 'GF' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
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
        public DataTable obtenerTablaPosiciones(int idEdicion, int idFase)
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
                string sql = @"SELECT Equipo, idGrupo, idEquipo, COUNT(CASE idEstadoPartido WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',  
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) AS 'PG',
                                COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END) AS 'PE',
                                COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END) AS 'PP',
                                SUM(CASE WHEN idEquipoLocal = idEquipo AND golesLocal IS NOT NULL THEN golesLocal ELSE 0 END) + SUM(CASE WHEN idEquipoVisitante = idEquipo AND golesVisitante IS NOT NULL THEN golesVisitante ELSE 0 END) AS 'GF',
                                SUM(CASE WHEN idEquipoLocal = idEquipo AND golesVisitante IS NOT NULL THEN golesVisitante ELSE 0 END)+ SUM(CASE WHEN idEquipoVisitante = idEquipo AND golesLocal IS NOT NULL THEN golesLocal ELSE 0 END) AS 'GC',
                                COUNT(CASE idGanador WHEN idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE idPerdedor WHEN idEquipo THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos'
                                FROM dbo.joinTablaDePosicionesCompleta(@idEdicion, @idFase)
                                GROUP BY Equipo, idEquipo, idGrupo, idEdicion, nombre, puntosGanado, puntosPerdido, puntosEmpatado
                                ORDER BY 'Puntos' DESC , 'PG' DESC, 'GF' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
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
        /// Guarda la tabla de posiciones final de una edición 
        /// autor: Flor Rojas
        /// </summary>
        public void guardarTablaPosiciones(List<Equipo> listaEquipos, int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                int i = 1;
                foreach (Equipo equipo in listaEquipos)
                {
                    string sql = @"INSERT INTO TablaPosicionesFinal (posicion, idEquipo, idEdicion)
                                        VALUES (@posicion, @idEquipo, @idEdicion)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@posicion",i));
                    cmd.Parameters.Add(new SqlParameter("@idEquipo", equipo.idEquipo));
                    cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    i++;
                }                
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
        /// Permite obtener el ranking de tarjetas amarillas y rojas por equipo y jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public DataTable obtenerTablaTarjetas(int idEdicion)
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
                string sql = @"SELECT TOP 10 e.idEquipo AS 'IDEQUIPO', e.nombre AS 'EQUIPO', 
                                j.idJugador AS 'IDJUGADOR', j.nombre AS 'JUGADOR',
                                COUNT(CASE t.tipoTarjeta WHEN 'A' THEN 1 ELSE NULL END) AS 'AMARILLAS',
                                COUNT(CASE t.tipoTarjeta WHEN 'R' THEN 1 ELSE NULL END) AS 'ROJAS'
                                FROM Tarjetas t
                                INNER JOIN Equipos e ON t.idEquipo = e.idEquipo
                                INNER JOIN Jugadores j ON t.idJugador = j.idJugador
                                INNER JOIN Partidos p ON t.idPartido = p.idPartido
                                WHERE p.idEdicion = @idEdicion
                                GROUP BY e.idEquipo, e.nombre, j.idJugador, j.nombre
                                ORDER BY 'AMARILLAS' DESC, 'ROJAS' DESC";
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


        public DataTable obtenerPartidosPorArbitro(int idEdicion)
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
                string sql = @"SELECT a.idArbitro, a.nombre, p.idPartido, eqLocal.nombre AS 'EquipoLocal', 
                                eqVisitante.nombre AS 'EquipoVisitante', p.golesLocal, p.golesVisitante
                                FROM Partidos p 
                                INNER JOIN Arbitros a ON p.idArbitro = a.idArbitro
                                INNER JOIN Ediciones e ON e.idTorneo = a.idTorneo
                                INNER JOIN Equipos eqLocal ON p.idEquipoLocal = eqLocal.idEquipo
                                INNER JOIN Equipos eqVisitante ON p.idEquipoVisitante = eqVisitante.idEquipo
                                WHERE e.idEdicion = @idEdicion
                                GROUP BY a.idArbitro, a.nombre, p.idPartido,eqLocal.nombre, eqVisitante.nombre, 
                                golesLocal, golesVisitante";
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
