﻿using System;
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
                                es.nombre AS 'Estado',
                                p.idGrupo AS 'Grupo'
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
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal ,  eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre,  p.fecha, es.nombre, p.idGrupo
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
                                    es.nombre AS 'Estado',
                                    p.idGrupo AS 'Grupo'
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
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal, eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre, p.fecha, es.nombre, p.idGrupo
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
                                    es.nombre AS 'Estado',
                                    p.idGrupo AS 'Grupo'
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
	                            GROUP BY f.idFecha, elocal.nombre, p.golesLocal, eVisitante.nombre, p.golesVisitante, ar.nombre, can.nombre, p.fecha, es.nombre, p.idGrupo
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

        public DataTable obtenerTablaPosicionesAcotada(int idEdicion, int idFase, int top)
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
                string sql = @"SELECT TOP 5 Equipo, idGrupo, idEquipo, COUNT(CASE idEstadoPartido WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',  
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
                cmd.Parameters.Add(new SqlParameter("@top", top));
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

        /// <summary>
        /// Devuelve el nombre del goleador de un equipo apra una edicion
        /// autor: Flor Rojas
        /// </summary>
        public DataTable obtenerGoleadorEquipo(int idEdicion, int idEquipo)
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
                string sql = @" SELECT TOP 1 j.idJugador AS 'IDJUGADOR', j.nombre AS 'JUGADOR', count(g.idGol) AS 'GOLES',count(DISTINCT p.idPartido) AS 'PARTIDOS'
                                    FROM Goles g
	                                     JOIN Equipos e ON e.idEquipo = g.idEquipo 
	                                     JOIN Jugadores j ON g.idJugador = j.idJugador
	                                     JOIN Partidos p ON p.idPartido = g.idPartido
										 WHERE p.idEquipoLocal=@idEquipo OR p.idEquipoVisitante=@idEquipo
	                                     GROUP BY j.idJugador, p.idEdicion, j.nombre, e.nombre, e.idEquipo 
	                                     HAVING p.idEdicion = @idEdicion 
										 ORDER BY Goles DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
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

        public DataTable obtenerVersus(int idEquipoLocal, int idEquipoVisitante, int idEdicion, int idTorneo)
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
                string sql = @"SELECT 
                                equipo1.idEquipo AS 'Id Equipo Local', equipo1.nombre AS 'Equipo Local', 
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante)) THEN 1 ELSE NULL END) AS 'PJ Equipo Local',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND p.idGanador = equipo1.idEquipo AND (equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'PG Equipo Local',
                                SUM(CASE WHEN (p.idEdicion = @idEdicion AND p.idEquipoLocal= equipo1.idEquipo AND p.golesLocal IS NOT NULL AND equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante)) THEN p.golesLocal ELSE 0 END) + SUM(CASE WHEN (p.idEdicion = @idEdicion AND p.idEquipoVisitante = equipo1.idEquipo AND p.golesVisitante IS NOT NULL) THEN p.golesVisitante ELSE 0 END) AS 'GF Equipo Local',	
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND p.idGanador = equipo1.idEquipo AND equipo1.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante)) THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE WHEN p.idEdicion = @idEdicion AND p.empate = 1 AND equipo1.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante) THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE WHEN (p.idEdicion = @idEdicion AND p.idPerdedor = equipo1.idEquipo AND equipo1.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante)) THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos Local',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND t.tipoTarjeta = 'A' AND equipo1.idEquipo = t.idEquipo) THEN 1 ELSE NULL END) AS 'AMARILLAS Equipo Local',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND t.tipoTarjeta = 'R' AND equipo1.idEquipo = t.idEquipo) THEN 1 ELSE NULL END) AS 'ROJAS Equipo Local',
                                equipo2.idEquipo AS 'Id Equipo Visitante',
                                equipo2.nombre AS 'Equipo Visitante', 
                                COUNT(CASE WHEN ((p.idEdicion = @idEdicion AND equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'PJ Equipo Visitante',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND equipo2.idEquipo = p.idGanador AND (equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'PG Equipo Visitante',
                                SUM(CASE WHEN (p.idEdicion = @idEdicion AND p.idEquipoLocal = equipo2.idEquipo AND p.golesLocal IS NOT NULL AND equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante)) THEN p.golesLocal ELSE 0 END) + SUM(CASE WHEN (p.idEdicion = @idEdicion AND p.idEquipoVisitante = equipo2.idEquipo AND p.golesVisitante IS NOT NULL) THEN p.golesVisitante ELSE 0 END) AS 'GF Equipo Visitante',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND p.idGanador = equipo2.idEquipo AND equipo2.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante)) THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE WHEN p.idEdicion = @idEdicion AND p.empate = 1 AND equipo2.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante) THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE WHEN (p.idEdicion = @idEdicion AND p.idPerdedor = equipo2.idEquipo AND equipo2.idEquipo IN (p.idEquipoLocal, p.idEquipoVisitante)) THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos Visitante', 
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND t.tipoTarjeta = 'A' AND equipo2.idEquipo = t.idEquipo) THEN 1 ELSE NULL END) AS 'AMARILLAS Equipo Visitante',
                                COUNT(CASE WHEN (p.idEdicion = @idEdicion AND t.tipoTarjeta = 'R' AND equipo2.idEquipo = t.idEquipo) THEN 1 ELSE NULL END) AS 'ROJAS Equipo Visitante',
                                COUNT(CASE WHEN (p.idEdicion <> @idEdicion AND p.idGanador = equipo1.idEquipo AND (equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'PG Equipo Local Previos',
                                COUNT(CASE WHEN (p.idEdicion <> @idEdicion AND equipo2.idEquipo = p.idGanador AND (equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'PG Equipo Visitante Previos',
                                COUNT(CASE WHEN ((p.idEdicion <> @idEdicion AND equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante) AND equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'Cant Enfrentamientos Previos',
                                COUNT(CASE WHEN ((p.idEdicion <> @idEdicion AND p.empate = 1 AND equipo1.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante) AND equipo2.idEquipo IN(p.idEquipoLocal, p.idEquipoVisitante))) THEN 1 ELSE NULL END) AS 'Empates Previos'
                                FROM Partidos p
                                INNER JOIN Equipos equipo1 ON (equipo1.idEquipo = p.idEquipoLocal OR equipo1.idEquipo = p.idEquipoVisitante)
                                INNER JOIN Equipos equipo2 ON (equipo2.idEquipo = p.idEquipoLocal OR equipo2.idEquipo = p.idEquipoVisitante)
                                INNER JOIN Ediciones edicionActual ON p.idEdicion = edicionActual.idEdicion	
                                LEFT JOIN Tarjetas t ON t.idPartido = p.idPartido	
                                WHERE 
                                equipo1.idEquipo = @idEquipoLocal AND equipo2.idEquipo = @idEquipoVisitante
                                AND p.idEstado = @estadoJugado AND
                                edicionActual.idEdicion IN (SELECT idEdicion
				                                            FROM Ediciones
				                                            WHERE idTorneo = @idTorneo)
                                GROUP BY equipo1.idEquipo, equipo1.nombre, equipo2.idEquipo, equipo2.nombre, edicionActual.puntosGanado, edicionActual.puntosEmpatado, edicionActual.puntosPerdido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.Parameters.Add(new SqlParameter("@idEquipoLocal", idEquipoLocal));
                cmd.Parameters.Add(new SqlParameter("@idEquipoVisitante", idEquipoVisitante));
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

        public DataTable estadisticasDeUnEquipo(int idEquipo, int idEdicion)
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
                string sql = @"SELECT 
                                equipo1.idEquipo AS 'Id Equipo', equipo1.nombre AS 'Equipo', 
                                COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',
                                COUNT(CASE WHEN (p.idGanador = equipo1.idEquipo) THEN 1 ELSE NULL END) AS 'PG',
                                COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END) AS 'PE',
                                COUNT(CASE WHEN (p.idPerdedor = equipo1.idEquipo) THEN 1 ELSE NULL END) AS 'PP',
                                SUM(CASE WHEN idEquipoLocal = equipo1.idEquipo AND golesLocal IS NOT NULL THEN golesLocal ELSE 0 END) + SUM(CASE WHEN idEquipoVisitante = equipo1.idEquipo AND golesVisitante IS NOT NULL THEN golesVisitante ELSE 0 END) AS 'GF',
                                SUM(CASE WHEN idEquipoLocal = equipo1.idEquipo AND golesVisitante IS NOT NULL THEN golesVisitante ELSE 0 END)+ SUM(CASE WHEN idEquipoVisitante = equipo1.idEquipo AND golesLocal IS NOT NULL THEN golesLocal ELSE 0 END) AS 'GC',
                                COUNT(CASE idGanador WHEN equipo1.idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE idPerdedor WHEN equipo1.idEquipo THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos',
                                (SELECT COUNT(CASE WHEN t.tipoTarjeta = 'A' THEN 1 ELSE NULL END) FROM Tarjetas t WHERE t.idEquipo = @idEquipo AND t.idPartido IN (SELECT p1.idPartido FROM Partidos p1 WHERE p1.idEdicion = @idEdicion)) AS 'TA',
                                (SELECT COUNT(CASE WHEN t.tipoTarjeta = 'R' THEN 1 ELSE NULL END) FROM Tarjetas t WHERE t.idEquipo = @idEquipo AND t.idPartido IN (SELECT p1.idPartido FROM Partidos p1 WHERE p1.idEdicion = @idEdicion )) AS 'TR' 
                                FROM Partidos p
                                INNER JOIN Equipos equipo1 ON (equipo1.idEquipo = p.idEquipoLocal OR equipo1.idEquipo = p.idEquipoVisitante)
                                INNER JOIN Ediciones edicionActual ON p.idEdicion = edicionActual.idEdicion	
                                WHERE 
                                equipo1.idEquipo = @idEquipo AND
                                edicionActual.idEdicion = @idEdicion
                                GROUP BY equipo1.idEquipo, equipo1.nombre, edicionActual.puntosGanado, edicionActual.puntosEmpatado, edicionActual.puntosPerdido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
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

        public DataTable ultimosPartidosDeUnEquipo(int idEquipo, int idEdicion)
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
                string sql = @"SELECT TOP 5 equipoLocal.nombre AS 'Equipo Local', 
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante', 
                                p.fecha AS 'Fecha', idGanador, idPerdedor, 
                                CASE  
					                WHEN p.idGanador = 14 THEN 'Ganado' 
					                WHEN p.idPerdedor = 14 THEN 'Perdido'  
					                ELSE 'Empatado' 
					            END AS 'Resultado' 
                                FROM Partidos p 
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE p.idEdicion = @idEdicion 
                                AND (p.idEquipoLocal = @idEquipo OR p.idEquipoVisitante = @idEquipo)
                                AND p.idEstado = @estadoJugado
                                ORDER BY idPartido DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
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

        public DataTable ultimoPartidoPrevioDeUnEquipo(int idEquipo, int idEdicion, int idPartido)
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
                string sql = @"SELECT TOP 3 equipoLocal.nombre AS 'Equipo Local', equipoLocal.idEquipo AS 'Id Equipo Local',
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                p.idFecha AS 'Fecha'
                                FROM Partidos p 
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE p.idEdicion = @idEdicion 
                                AND (p.idEquipoLocal = @idEquipo OR p.idEquipoVisitante = @idEquipo)
                                AND p.idEstado = @estadoJugado
                                AND idPartido <> @idPartido 
                                AND idPartido < @idPartido                               
                                ORDER BY idPartido DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
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

        public DataTable estadisticasDeUnJugador(int idJugador, int idEdicion)
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
                string sql = @"select COUNT(Distinct g.idGol) AS 'Goles Convertidos',
                                COUNT(Distinct tarjetasAmarillas.idTarjeta) AS 'Amarillas',
                                COUNT(Distinct tarjetasRojas.idTarjeta) AS 'Rojas',
                                COUNT(Distinct txp.idPartido) AS 'PARTIDOS JUGADOS'    
                                from Jugadores j 
                                left join goles g on (g.idJugador = j.idJugador AND g.idPartido IN (SELECT idPartido FROM Partidos WHERE idEdicion = @idEdicion))
                                left JOIN Tarjetas tarjetasAmarillas ON (tarjetasAmarillas.idJugador = j.idJugador AND tarjetasAmarillas.tipoTarjeta = 'A'
                                AND tarjetasAmarillas.idPartido IN (SELECT idPartido FROM Partidos WHERE idEdicion = @idEdicion))
                                left JOIN Tarjetas tarjetasRojas ON (tarjetasRojas.idJugador = j.idJugador AND tarjetasRojas.tipoTarjeta = 'R'
                                AND tarjetasRojas.idPartido IN (SELECT idPartido FROM Partidos WHERE idEdicion = @idEdicion))
                                LEFT JOIN TitularesXPartido txp ON (txp.idJugador = j.idJugador AND txp.idPartido IN (SELECT idPartido FROM Partidos WHERE idEdicion = @idEdicion))
                                where j.idJugador = @idJugador";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
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

        public DataTable ultimosPartidosDeJugadorComoTitular(int idJugador, int idEdicion)
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
                string sql = @"SELECT p.idPartido, eLocal.idEquipo AS 'idEquipoLocal', eLocal.nombre AS 'Equipo Local', p.golesLocal AS 'Goles Local',
                                p.golesVisitante AS 'Goles Visitante', eVisitante.nombre AS 'Equipo Visitante', eVisitante.idEquipo AS 'idEquipoVisitante',
                                p.penalesLocal AS 'PenalesLocal', p.penalesVisitante AS 'PenalesVisitante',
                                CASE  
					                WHEN p.idGanador = txp.idEquipo THEN 'Ganado' 
					                WHEN p.idPerdedor = txp.idEquipo THEN 'Perdido'  
					                ELSE 'Empatado' 
					            END AS 'Resultado' 
                                FROM Partidos p
                                INNER JOIN TitularesXPartido txp ON p.idPartido = txp.idPartido
                                INNER JOIN Equipos eLocal ON p.idEquipoLocal = eLocal.idEquipo
                                INNER JOIN Equipos eVisitante ON p.idEquipoVisitante = eVisitante.idEquipo
                                WHERE txp.idJugador = @idJugador
                                AND p.idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
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

        public DataTable golesJugadorEnEdicionesAnteriores(int idJugador, int idEdicion)
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
                string sql = @"SELECT e.idEdicion, e.nombre AS 'Edición', COUNT(Distinct g.idGol) AS 'Goles convertidos'
                                FROM Jugadores j
                                INNER JOIN EquipoXEdicion exe ON exe.idEquipo = j.idEquipo
                                INNER JOIN Ediciones e ON exe.idEdicion = e.idEdicion
                                INNER JOIN Goles g ON (g.idJugador = j.idJugador AND g.idPartido IN(SELECT idPartido FROM Partidos WHERE idEdicion = e.idEdicion))
                                WHERE j.idJugador = @idJugador
                                GROUP BY e.idEdicion, e.nombre
                                ORDER BY e.idEdicion DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
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
        /// Permite obtener todos los partidos que arbitró un arbitro por Id de árbitro
        /// Si idEdicion = null -> Trae todos los partidos que arbitró dicho árbitro de TODAS las ediciones del torneo asociado
        /// Si idEdición != null -> Trae todos los partidos que arbitró dicho árbitro de UNA las ediciones del torneo asociado
        /// autor: Pau Pedrosa
        /// </summary>
        public DataTable partidosQueArbitroUnArbitro(int idArbitro, int? idEdicion)
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
                string sql = @"SELECT a.idArbitro AS 'ID ÁRBITRO', a.nombre AS 'ÁRBITRO', equipoLocal.nombre AS 'Equipo Local',
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante'
                                FROM Arbitros a
                                INNER JOIN Partidos p ON p.idArbitro = a.idArbitro
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE a.idArbitro = @idArbitro ";
                if(idEdicion != null)
                    sql +="AND p.idEdicion = @idEdicion ";
                sql += "GROUP BY a.idArbitro, a.nombre, equipoLocal.nombre, p.golesLocal, p.golesVisitante, equipoVisitante.nombre";                                
                                
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idArbitro", idArbitro));
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

        //Evolucion de puntos de un equipo por edicion y por fase
        //autor: Pau Pedrosa
        public DataTable evolucionPuntosDeUnEquipo(int idEdicion, int idFase, int idEquipo)
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
                string sql = @"SELECT partido.idFecha AS 'Fecha', COUNT(CASE idGanador WHEN @idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE idPerdedor WHEN @idEquipo  THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos'
                                FROM Partidos partido
                                INNER JOIN Ediciones edicion ON partido.idEdicion = edicion.idEdicion
                                WHERE (partido.idEquipoLocal = @idEquipo  OR partido.idEquipoVisitante = @idEquipo) and partido.idEstado=@estadoPartido
                                AND edicion.idEdicion = @idEdicion
                                AND partido.idFase = @idFase
                                GROUP BY partido.idFecha, edicion.puntosGanado, edicion.puntosEmpatado, edicion.puntosPerdido
                                ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.Parameters.Add(new SqlParameter("@estadoPartido", Estado.partidoJUGADO));
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

        //Últimos goles de un jugador
        //autor: Pau Pedrosa
        public DataTable ultimosGolesDeUnJugador(int idJugador, int idEdicion)
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
                string sql = @"SELECT TOP 5 jugador.idJugador AS 'Id Jugador', jugador.nombre AS 'Nombre Jugador', 
                                partido.idFase AS 'Fase', partido.idFecha AS 'Fecha', partido.idPartido AS 'idPartido',
                                gol.minuto AS 'Minuto', tipoGol.nombre AS 'Tipo Gol', otroEquipo.nombre AS 'Otro Equipo',
                                otroEquipo.idEquipo AS 'Id Otro equipo'
                                FROM Goles gol
                                INNER JOIN Jugadores jugador ON gol.idJugador = jugador.idJugador
                                INNER JOIN partidos partido ON gol.idPartido = partido.idPartido
                                INNER JOIN TiposGol tipoGol ON gol.idTipoGol = tipoGol.idTipoGol
                                INNER JOIN Equipos equipoJugador ON equipoJugador.idEquipo = jugador.idEquipo
                                INNER JOIN Equipos otroEquipo ON otroEquipo.idEquipo = (SELECT TOP 1 eq.idEquipo
														                                FROM Equipos eq inner join partidos	p
														                                on (p.idEquipoLocal = eq.idEquipo OR p.idEquipoVisitante = eq.idEquipo)
														                                WHERE eq.idEquipo <> equipoJugador.idEquipo
														                                AND p.idPartido = partido.idPartido
														                                )
                                WHERE jugador.idJugador = @idJugador
                                AND partido.idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
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

        public DataTable cantidadDeGolesPorTipoGol(int idJugador, int idEdicion)
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
                string sql = @"SELECT tiposGol.nombre AS 'Tipo Gol', 
                                COUNT(CASE goles.idTipoGol WHEN tiposGol.idTipoGol THEN 1 ELSE NULL END) AS 'Cantidad Goles'
                                FROM Goles goles
                                INNER JOIN TiposGol tiposGol ON goles.idTipoGol = tiposGol.idTipoGol
                                INNER JOIN Partidos partidos ON goles.idPartido = partidos.idPartido
                                WHERE goles.idJugador = @idJugador
                                AND partidos.idEdicion = @idEdicion
                                GROUP BY tiposGol.idTipoGol, tiposGol.nombre";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idJugador", idJugador));
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

        //Cantidad de goles de cada equipo de una edición
        //autor: Pau Pedrosa
        public DataTable cantidadGolesPorEquipo(int idEdicion, bool paraGrafico)
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
                string sql = @"SELECT ";
                if(!paraGrafico)
                    sql +="equipo.idEquipo AS 'Id equipo',";
                sql += @" equipo.nombre AS 'Equipo', SUM(CASE WHEN partido.idEquipoLocal = equipo.idEquipo AND partido.golesLocal IS NOT NULL THEN partido.golesLocal ELSE 0 END) + SUM(CASE WHEN partido.idEquipoVisitante = equipo.idEquipo AND partido.golesVisitante IS NOT NULL THEN partido.golesVisitante ELSE 0 END) AS 'Goles'
                        FROM Equipos equipo
                        INNER JOIN EquipoXEdicion exe ON equipo.idEquipo = exe.idEquipo
                        LEFT JOIN Partidos partido ON ((partido.idEquipoLocal = equipo.idEquipo OR partido.idEquipoVisitante = equipo.idEquipo) AND partido.idEdicion = @idEdicion AND partido.idEstado = @partidoJugado)
                        WHERE exe.idEdicion = @idEdicion
                        GROUP BY equipo.idEquipo, equipo.nombre
                        ORDER BY Goles DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@partidoJugado", Estado.partidoJUGADO));
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

        //Cantidad de goles de por tipo de gol por edición
        //autor: Pau Pedrosa
        public DataTable cantidadGolesPorTipoGol(int idEdicion, bool paraGrafico)
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
                string sql = @"SELECT ";
                if(!paraGrafico)
                    sql += @"CASE  
					            WHEN tipoGol.idTipoGol = 1 THEN 'football110' 
					            WHEN tipoGol.idTipoGol = 2 THEN 'football61'
                                WHEN tipoGol.idTipoGol = 3 THEN 'football118'  
                                WHEN tipoGol.idTipoGol = 4 THEN 'football102'  
                                WHEN tipoGol.idTipoGol = 5 THEN 'football120'  
                                WHEN tipoGol.idTipoGol = 6 THEN 'football51'    
					        END AS 'tipo', tipoGol.idTipoGol AS 'Id Tipo Gol', ";
                sql += @" tipoGol.nombre AS 'Tipo Gol',
                            COUNT(CASE gol.idTipoGol WHEN tipoGol.idTipoGol THEN 1 ELSE NULL END) AS 'Goles'
                            FROM TiposGol tipoGol
                            LEFT JOIN Goles gol ON (gol.idTipoGol = tipoGol.idTipoGol AND gol.idPartido IN (SELECT p.idPartido FROM Partidos p WHERE p.idEdicion = @idEdicion AND p.idEstado = @partidoJugado))
                            GROUP BY tipoGol.idTipoGol, tipoGol.nombre
                            ORDER BY Goles DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@partidoJugado", Estado.partidoJUGADO));
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

        //Tabla de goleadores por fase
        //autor: Pau Pedrosa
        public DataTable goleadoresPorFaseDeEdicion(int idEdicion, int idFase)
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
                string sql = @"SELECT TOP 10 j.idJugador AS 'IDJUGADOR', j.nombre AS 'JUGADOR', e.idEquipo AS 'IDEQUIPO', e.nombre AS 'EQUIPO', 
                                COUNT(g.idGol) AS 'GOLES'
                                FROM Goles g
                                JOIN Equipos e ON e.idEquipo = g.idEquipo 
                                JOIN Jugadores j ON g.idJugador = j.idJugador
                                JOIN Partidos p ON p.idPartido = g.idPartido
                                GROUP BY j.idJugador, p.idEdicion, p.idFase, j.nombre, e.nombre, e.idEquipo
                                HAVING p.idEdicion = @idEdicion
                                AND p.idFase = @idFase
                                ORDER BY 'GOLES' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
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

        public DataTable tablaPosicionesEdicion(int idEdicion)
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
                string sql = @"select e.idEquipo AS 'idEquipo', e.nombre AS 'equipo', e.directorTecnico,
                                e.colorCamisetaPrimario, e.colorCamisetaSecundario, del.nombre AS 'delegadoPrincipal',
                                COUNT(CASE p.idEstado WHEN @partidoJugado THEN 1 ELSE NULL END) AS 'PJ',  
                                COUNT(CASE p.idGanador WHEN e.idEquipo THEN 1 ELSE NULL END) AS 'PG',
                                COUNT(CASE p.empate WHEN 1 THEN 1 ELSE NULL END) AS 'PE',
                                COUNT(CASE p.idPerdedor WHEN e.idEquipo THEN 1 ELSE NULL END) AS 'PP',
                                SUM(CASE WHEN p.idEquipoLocal = e.idEquipo AND p.golesLocal IS NOT NULL THEN p.golesLocal ELSE 0 END) + SUM(CASE WHEN p.idEquipoVisitante = e.idEquipo AND p.golesVisitante IS NOT NULL THEN p.golesVisitante ELSE 0 END) AS 'GF',
                                SUM(CASE WHEN p.idEquipoLocal = e.idEquipo AND p.golesVisitante IS NOT NULL THEN p.golesVisitante ELSE 0 END)+ SUM(CASE WHEN p.idEquipoVisitante = e.idEquipo AND p.golesLocal IS NOT NULL THEN p.golesLocal ELSE 0 END) AS 'GC',
                                COUNT(CASE p.idGanador WHEN e.idEquipo THEN 1 ELSE NULL END) * puntosGanado + COUNT(CASE empate WHEN 1 THEN 1 ELSE NULL END)*puntosEmpatado+ COUNT(CASE p.idPerdedor WHEN e.idEquipo THEN 1 ELSE NULL END)*puntosPerdido as 'Puntos'
                                from Equipos e
                                inner join EquipoXEdicion exe ON exe.idEquipo = e.idEquipo
                                inner join Ediciones ed ON exe.idEdicion = ed.idEdicion
                                inner join Delegados del ON del.idDelegado = e.idDelegadoPrincipal
                                left join Partidos p on (e.idEquipo = p.idEquipoLocal OR e.idEquipo = p.idEquipoVisitante)
                                where exe.idEdicion = @idEdicion and p.idEdicion = @idEdicion
                                GROUP BY e.nombre, e.directorTecnico, e.colorCamisetaPrimario, e.colorCamisetaSecundario, e.idEquipo, ed.idEdicion, puntosGanado, puntosPerdido, puntosEmpatado, del.nombre
                                ORDER BY 'Puntos' DESC , 'PG' DESC, 'GF' DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@partidoJugado", Estado.partidoJUGADO));
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

        public DataTable ultimosGolesDeEdicion(int idEdicion)
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
                string sql = @"SELECT equipo.idEquipo AS 'Id Equipo', equipo.nombre AS 'Equipo', jugador.idJugador AS 'Id Jugador',
                                jugador.nombre AS 'Jugador', tipoGol.nombre AS 'Tipo Gol', partido.idPartido AS 'Id Partido'
                                FROM Goles gol INNER JOIN Jugadores jugador ON jugador.idJugador = gol.idJugador
                                INNER JOIN Equipos equipo ON jugador.idEquipo = equipo.idEquipo
                                INNER JOIN TiposGol tipoGol ON gol.idTipoGol = tipoGol.idTipoGol
                                INNER JOIN Partidos partido ON gol.idPartido = partido.idPartido
                                WHERE partido.idEdicion = @idEdicion
                                ORDER BY partido.fecha DESC, gol.idGol DESC";
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

        public DataTable ultimosTarjetasDeEdicion(int idEdicion)
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
                string sql = @"SELECT equipo.idEquipo AS 'Id Equipo', equipo.nombre AS 'Equipo', jugador.idJugador AS 'Id Jugador',
                                jugador.nombre AS 'Jugador', 
                                CASE  
	                                WHEN tarjeta.tipoTarjeta = 'R' THEN 'roja' 
	                                WHEN tarjeta.tipoTarjeta = 'A' THEN 'amarilla'
                                END AS 'Tipo Tarjeta',
                                partido.idPartido AS 'Id Partido'
                                FROM Tarjetas tarjeta INNER JOIN Jugadores jugador ON jugador.idJugador = tarjeta.idJugador
                                INNER JOIN Equipos equipo ON jugador.idEquipo = equipo.idEquipo
                                INNER JOIN Partidos partido ON tarjeta.idPartido = partido.idPartido
                                WHERE partido.idEdicion = @idEdicion
                                ORDER BY partido.fecha DESC, tarjeta.idTarjeta DESC";
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

        public DataTable estadisticasDeEdicion(int idEdicion)
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
                string sql = @"SELECT 
                                COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',
                                SUM(CASE WHEN p.golesLocal IS NOT NULL THEN p.golesLocal ELSE 0 END) + SUM(CASE WHEN p.golesVisitante IS NOT NULL THEN p.golesVisitante ELSE 0 END) AS 'Goles Convertidos',
                                (SELECT COUNT(CASE WHEN t.tipoTarjeta = 'A' THEN 1 ELSE NULL END) FROM Tarjetas t WHERE t.idPartido IN (SELECT p1.idPartido FROM Partidos p1 WHERE p1.idEdicion = @idEdicion)) AS 'TA',
                                (SELECT COUNT(CASE WHEN t.tipoTarjeta = 'R' THEN 1 ELSE NULL END) FROM Tarjetas t WHERE t.idPartido IN (SELECT p1.idPartido FROM Partidos p1 WHERE p1.idEdicion = @idEdicion)) AS 'TR' 
                                FROM Partidos p
                                INNER JOIN Ediciones edicionActual ON p.idEdicion = edicionActual.idEdicion	
                                WHERE edicionActual.idEdicion = @idEdicion";
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

        public DataTable obtenerTablaPosicionesFinal(int idEdicion)
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
                string sql = @"SELECT TOP 3 posicion, idEquipo
                                FROM TablaPosicionesFinal tablaFinal
                                WHERE tablaFinal.idEdicion = @idEdicion ORDER BY posicion";
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

        public DataTable estadisticasDeArbitro(int idTorneo, int idEdicion)
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
                string sql = @"SELECT a.idArbitro AS 'idArbitro', a.nombre AS 'nombreArbitro', 
                                a.matricula AS 'matriculaArbitro', 
                                COUNT(CASE partidosEdicion.idEdicion WHEN @idEdicion THEN 1 ELSE NULL END) AS 'CantPartidosArbitradosEdicion',
                                (SELECT COUNT(p.idPartido) FROM Partidos p WHERE p.idArbitro = a.idArbitro AND p.idEdicion IN (SELECT e.idEdicion FROM Ediciones e WHERE e.idTorneo = @idTorneo)) AS 'CantPartidosArbitradosTorneo'
                                FROM Arbitros a 
                                LEFT JOIN Partidos partidosEdicion ON partidosEdicion.idArbitro = a.idArbitro
                                WHERE idTorneo = @idTorneo
                                GROUP BY a.idArbitro, a.nombre, a.matricula";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
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

        public DataTable vallaMenosVencida(int idEdicion)
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
                string sql = @"SELECT e.idEquipo AS 'idEquipo', e.nombre AS 'equipo', 
                                COUNT(CASE p.idEstado WHEN @estadoJugado THEN 1 ELSE NULL END) AS 'PJ',  
                                SUM(CASE WHEN p.idEquipoLocal = e.idEquipo AND p.golesVisitante IS NOT NULL THEN p.golesVisitante ELSE 0 END)+ SUM(CASE WHEN p.idEquipoVisitante = e.idEquipo AND p.golesLocal IS NOT NULL THEN p.golesLocal ELSE 0 END) AS 'GC',
                                CASE  
	                                WHEN COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END) = 0 THEN 0   
	                                ELSE (SUM(CASE WHEN p.idEquipoLocal = e.idEquipo AND p.golesVisitante IS NOT NULL THEN p.golesVisitante ELSE 0 END)+ SUM(CASE WHEN p.idEquipoVisitante = e.idEquipo AND p.golesLocal IS NOT NULL THEN p.golesLocal ELSE 0 END) *1.00)/(COUNT(CASE p.idEstado WHEN 13 THEN 1 ELSE NULL END)*1.00) 
                                END AS 'promedio' 
                                from Equipos e
                                inner join EquipoXEdicion exe ON exe.idEquipo = e.idEquipo
                                left join Partidos p on (e.idEquipo = p.idEquipoLocal OR e.idEquipo = p.idEquipoVisitante)
                                where exe.idEdicion = @idEdicion and p.idEdicion = @idEdicion
                                GROUP BY e.nombre, e.idEquipo
                                ORDER BY 'GC' ASC";
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

        public DataTable rankingFairPlay(int idEdicion)
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
                string sql = @"SELECT e.idEquipo AS 'idEquipo', e.nombre AS 'equipo',
                                (COUNT(tr.idTarjeta) + COUNT(ta.idTarjeta)) AS  'cantidad',
                                COUNT(tr.idTarjeta) AS 'TR', COUNT(ta.idTarjeta) AS 'TA'
                                FROM Equipos e
                                INNER JOIN EquipoXEdicion exe ON exe.idEquipo = e.idEquipo
                                LEFT JOIN Tarjetas tr ON (tr.idEquipo = e.idEquipo AND tr.tipoTarjeta ='R' AND tr.idPartido IN (SELECT p.idPartido FROM Partidos p WHERE p.idEdicion = @idEdicion))
                                LEFT JOIN Tarjetas ta ON (ta.idEquipo = e.idEquipo AND ta.tipoTarjeta ='A' AND ta.idPartido IN (SELECT p.idPartido FROM Partidos p WHERE p.idEdicion = @idEdicion))
                                WHERE exe.idEdicion = @idEdicion
                                GROUP BY e.idEquipo, e.nombre
                                ORDER BY 'cantidad' ASC";
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
