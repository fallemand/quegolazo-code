﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Globalization;


namespace AccesoADatos
{
   public class DAOPartido
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Partido de las Fechas de un Partido, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="fase">Fase que se esta grabando</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarPartidos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Grupo g in fase.grupos)
                {
                    foreach (Fecha f in g.fechas)
                    {
                        foreach (Partido p in f.partidos)
                         {
                             string sql = @"INSERT INTO Partidos (idFecha, idGrupo, idFase, idEdicion, idEquipoLocal, idEquipoVisitante, idEstado)
                                                VALUES (@idFecha, @idGrupo, @idFase, @idEdicion, @idEquipoLocal, @idEquipoVisitante, @idEstado)";
                             cmd.Parameters.Clear();
                             cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                             cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                             cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                             cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                             cmd.Parameters.AddWithValue("@idEquipoLocal", DAOUtils.dbValueInt(p.local.idEquipo));
                             cmd.Parameters.AddWithValue("@idEquipoVisitante", DAOUtils.dbValueInt(p.visitante.idEquipo));                             
                             cmd.Parameters.AddWithValue("@idEstado", Estado.partidoDIAGRAMADO);
                             cmd.CommandText = sql;
                             cmd.ExecuteNonQuery();    
                        }
                    }
                }

               

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el partido" + ex.Message);
            }          
        }



        /// <summary>
        /// Obtiene todos los datos del Partido
        /// autor: Pau Pedrosa
        /// </summary>
        public void obtenerPartidos(Fase fase, SqlConnection con)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                 cmd.Connection = con;                 
                 foreach (Grupo grupo in fase.grupos)
                 {
                     foreach (Fecha fechaActual in grupo.fechas)
                     {
                         cmd.Parameters.Clear();
                         cmd.Connection = con;
                         string sql = @"SELECT *
                                        FROM Partidos p
                                        WHERE idFecha = @idFecha AND idGrupo = @idGrupo 
                                        AND idFase = @idFase AND idEdicion = @idEdicion";                         
                         cmd.Parameters.AddWithValue("@idFecha", fechaActual.idFecha);
                         cmd.Parameters.AddWithValue("@idGrupo", grupo.idGrupo);
                         cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                         cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                         cmd.CommandText = sql;
                         dr = cmd.ExecuteReader();
                         while (dr.Read())
                         {
                             DAOEquipo daoEquipo = new DAOEquipo();
                             DAOEstado daoEstado = new DAOEstado();
                             DAOArbitro daoArbitro = new DAOArbitro();
                             DAOCancha daoCancha = new DAOCancha();
                             DAOFase daoFase = new DAOFase();
                             Partido partido = new Partido();
                             partido.idPartido = int.Parse(dr["idPartido"].ToString());
                             partido.fecha = (dr["fecha"] != DBNull.Value) ? (DateTime?) Utils.Validador.castDate( dr["fecha"].ToString()) : null;
                             partido.golesLocal = (dr["golesLocal"] != DBNull.Value) ? (int?) int.Parse(dr["golesLocal"].ToString()) : null;
                             partido.golesVisitante = (dr["golesVisitante"] != DBNull.Value) ? (int?) int.Parse(dr["golesVisitante"].ToString()) : null;
                             partido.empate = (dr["empate"] != DBNull.Value) ? (bool?)bool.Parse(dr["empate"].ToString()) : null;
                             partido.huboPenales = (dr["huboPenales"] != DBNull.Value) ? (bool?) bool.Parse(dr["huboPenales"].ToString()) : null;
                             partido.penalesLocal = (dr["penalesLocal"] != DBNull.Value) ? (int?) int.Parse(dr["penalesLocal"].ToString()) : null;
                             partido.penalesVisitante = (dr["penalesVisitante"] != DBNull.Value) ? (int?) int.Parse(dr["penalesVisitante"].ToString()) : null;
                             partido.local = (dr["idEquipoLocal"] != DBNull.Value) ? daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoLocal"].ToString())) : null;
                             partido.visitante = (dr["idEquipoVisitante"] != DBNull.Value) ? daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoVisitante"].ToString())) : null;
                             partido.estado = daoEstado.obtenerEstadoPorId(int.Parse(dr["idEstado"].ToString()));
                             partido.arbitro = (dr["idArbitro"] != DBNull.Value) ? daoArbitro.obtenerArbitroPorId(int.Parse(dr["idArbitro"].ToString())) : null;
                             partido.cancha = (dr["idCancha"] != DBNull.Value) ? daoCancha.obtenerCanchaPorId(int.Parse(dr["idCancha"].ToString())) : null;
                             partido.faseAsociada = daoFase.obtenerFasePorId(int.Parse(dr["idEdicion"].ToString()), int.Parse(dr["idFase"].ToString()));
                             partido.idPartidoPosterior = (dr["idPartidoPosterior"] != DBNull.Value) ? int.Parse(dr["idPartidoPosterior"].ToString()) : 0;
                             partido.idGrupo = (dr["idGrupo"] != DBNull.Value) ? (int?)int.Parse(dr["idGrupo"].ToString()) : null;
                             partido.idFecha = int.Parse(dr["idFecha"].ToString());
                             if (dr["idEquipoLocal"] != DBNull.Value && dr["idEquipoVisitante"] != DBNull.Value)
                                partido.nombreCompleto = daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoLocal"].ToString())).nombre + " vs. " + daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoVisitante"].ToString())).nombre;
                             fechaActual.partidos.Add(partido);
                         }
                         if (dr != null)
                             dr.Close(); 
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("No se pudo obtener los datos del partido" + ex.Message);
             }
             finally
             {
                 if (con != null && con.State == ConnectionState.Open)
                     con.Close();
             }
        }
       
        /// <summary>s
        /// Obtiene solo los id de los Partidos eliminatorios que se crearon para configurar las llaves de una fase dada.
        /// autor: Flor Rojas
        /// </summary>
        public void obtenerIDPartidosEliminatorios(Fase fase, SqlConnection con, SqlTransaction tran)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {               
                cmd.Connection = con;
                cmd.Transaction = tran;
                Grupo grupo = fase.grupos[0];
                
               foreach (Fecha fechaActual in grupo.fechas)
                {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        string sql = @"SELECT idPartido
                                        FROM Partidos p
                                        WHERE idFecha = @idFecha AND idGrupo = @idGrupo 
                                        AND idFase = @idFase AND idEdicion = @idEdicion";
                        cmd.Parameters.AddWithValue("@idFecha", fechaActual.idFecha);
                        cmd.Parameters.AddWithValue("@idGrupo", grupo.idGrupo);
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.CommandText = sql;                                               
                        dr = cmd.ExecuteReader();
                        foreach (Partido p in fechaActual.partidos)
                        {
                            if (dr.Read())
                                p.idPartido = int.Parse(dr["idPartido"].ToString());
                            else
                                throw new Exception("Error al intentar guardar los partidos");
                        }                          
                        if (dr != null)
                            dr.Close();
              }
                
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener el id del partido" + ex.Message);
            }           
        }

       /// <summary>
       /// Registra un Gol en la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public void registrarGol(Gol gol, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Goles (minuto, idJugador, idEquipo, idPartido, idTipoGol)
                                    VALUES (@minuto, @idJugador, @idEquipo, @idPartido, @idTipoGol)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idJugador", DAOUtils.dbValueNull((gol.jugador != null) ? (int?)gol.jugador.idJugador : null));
                cmd.Parameters.AddWithValue("@idEquipo", DAOUtils.dbValueNull((gol.equipo != null) ? (int?)gol.equipo.idEquipo : null));
                cmd.Parameters.AddWithValue("@idTipoGol", DAOUtils.dbValueNull((gol.tipoGol != null) ? (int?)gol.tipoGol.idTipoGol : null));
                cmd.Parameters.AddWithValue("@minuto", DAOUtils.dbValueNull(gol.minuto));
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el gol: " + ex.Message);
            }
        }

       /// <summary>
       /// Registra un Cambio en la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public void registrarCambio(Cambio cambio, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Cambios (idEquipo, idJugadorEntra, idJugadorSale, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugadorEntra, @idJugadorSale, @minuto, @idPartido)";
                cmd.Parameters.Clear();                
                cmd.Parameters.AddWithValue("@idEquipo", cambio.equipo.idEquipo);
                cmd.Parameters.AddWithValue("@idJugadorEntra", cambio.jugadorEntra.idJugador);
                cmd.Parameters.AddWithValue("@idJugadorSale", cambio.jugadorSale.idJugador);
                cmd.Parameters.AddWithValue("@minuto", DAOUtils.dbValueNull(cambio.minuto));
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el cambio: " + ex.Message);
            }
        }

       /// <summary>
       /// Registra una Tarjeta en la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public void registrarTarjeta(Tarjeta tarjeta, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Tarjetas (idEquipo, idJugador, tipoTarjeta, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugador, @tipoTarjeta, @minuto, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", tarjeta.equipo.idEquipo);
                cmd.Parameters.AddWithValue("@idJugador", tarjeta.jugador.idJugador);
                cmd.Parameters.AddWithValue("@tipoTarjeta", tarjeta.tipoTarjeta);
                cmd.Parameters.AddWithValue("@minuto", DAOUtils.dbValueNull(tarjeta.minuto));
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la tarjeta: " + ex.Message);
            }
        }



       /// <summary>
       /// Registra una sanción en la BD
       /// autor: Pau Pedrosa
       /// </summary>
//        public int registrarSancion(Sancion sancion, int idPartido)
//        {
//            SqlConnection con = new SqlConnection(cadenaDeConexion);
//            SqlCommand cmd = new SqlCommand();
//            try
//            {
//                if (con.State == ConnectionState.Closed)
//                    con.Open();
//                cmd.Connection = con;
//                string sql = @"INSERT INTO Sanciones (idEquipo, idJugador, motivo, idPartido)
//                                    VALUES (@idEquipo, @idJugador, @motivo, @idPartido)
//                                    SELECT SCOPE_IDENTITY()";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@idEquipo", sancion.idEquipo);
//                cmd.Parameters.AddWithValue("@idJugador", DAOUtils.dbValueNull(sancion.idJugador));
//                cmd.Parameters.AddWithValue("@motivo", DAOUtils.dbValueNull(sancion.motivo));
//                cmd.Parameters.AddWithValue("@idPartido", DAOUtils.dbValueNull(idPartido));
//                cmd.CommandText = sql;
//                int idSancion = int.Parse(cmd.ExecuteScalar().ToString());
//                return idSancion; //retorna el id de la sanción generado por la BD
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("No se pudo registrar la sanción: " + ex.Message);
//            }
//            finally
//            {
//                if (con != null && con.State == ConnectionState.Open)
//                    con.Close();
//            }
//        }

       /// <summary>
       /// Registra los Titulares de un Partido en la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public void registrarTitularesAPartido(List<Jugador> jugadores, int idEquipo, int idPartido, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Jugador jugador in jugadores)
                {
                    string sql = @"INSERT INTO TitularesXPartido (idPartido, idJugador, idEquipo)
                                    VALUES (@idPartido, @idJugador, @idEquipo)
                                    SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idPartido", idPartido);
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    cmd.Parameters.AddWithValue("@idJugador", jugador.idJugador);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo registrar los jugadores titulares: " + ex.Message);
            }
        }

       /// <summary>
       /// Obtiene un partido por Id 
       /// autor: Pau Pedrosa
       /// </summary>
       /// <param name="idPartido">Id de Partido a obtener</param>
       /// <returns>
       /// Objeto Partido
       /// Devuelve: Equipos Local y Visitante - Árbitro - Cancha - Id, Hora y fecha del partido
       /// </returns>
        public Partido obtenerPartidoPorId(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Partido partido = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Partidos p
                                WHERE p.idPartido = @idPartido";
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    partido = new Partido();
                    DAOEquipo daoEquipo = new DAOEquipo();
                    DAOEstado daoEstado = new DAOEstado();
                    DAOArbitro daoArbitro = new DAOArbitro();
                    DAOCancha daoCancha = new DAOCancha();
                    DAOFase daoFase = new DAOFase();
                    partido.idPartido = int.Parse(dr["idPartido"].ToString());
                    partido.fecha = (dr["fecha"] != DBNull.Value) ? (DateTime?)Utils.Validador.castDate(dr["fecha"].ToString()) : null;
                    partido.golesLocal = (dr["golesLocal"] != DBNull.Value) ? (int?)int.Parse(dr["golesLocal"].ToString()) : null;
                    partido.golesVisitante = (dr["golesVisitante"] != DBNull.Value) ? (int?)int.Parse(dr["golesVisitante"].ToString()) : null;
                    partido.empate = (dr["empate"] != DBNull.Value) ? (bool?)bool.Parse(dr["empate"].ToString()) : null;
                    partido.huboPenales = (dr["huboPenales"] != DBNull.Value) ? (bool?)bool.Parse(dr["huboPenales"].ToString()) : null;
                    partido.penalesLocal = (dr["penalesLocal"] != DBNull.Value) ? (int?)int.Parse(dr["penalesLocal"].ToString()) : null;
                    partido.penalesVisitante = (dr["penalesVisitante"] != DBNull.Value) ? (int?)int.Parse(dr["penalesVisitante"].ToString()) : null;
                    partido.local = (dr["idEquipoLocal"] != DBNull.Value) ? daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoLocal"].ToString())) : null;
                    partido.visitante = (dr["idEquipoVisitante"] != DBNull.Value) ? daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoVisitante"].ToString())) : null;
                    partido.estado = daoEstado.obtenerEstadoPorId(int.Parse(dr["idEstado"].ToString()));
                    partido.arbitro = (dr["idArbitro"] != DBNull.Value) ? daoArbitro.obtenerArbitroPorId(int.Parse(dr["idArbitro"].ToString())) : null;
                    partido.cancha = (dr["idCancha"] != DBNull.Value) ? daoCancha.obtenerCanchaPorId(int.Parse(dr["idCancha"].ToString())) : null;
                    partido.faseAsociada = daoFase.obtenerFasePorId(int.Parse(dr["idEdicion"].ToString()), int.Parse(dr["idFase"].ToString()));
                    partido.idPartidoPosterior = (dr["idPartidoPosterior"] != DBNull.Value) ? int.Parse(dr["idPartidoPosterior"].ToString()) : 0;
                    partido.idGrupo = (dr["idGrupo"] != DBNull.Value) ? (int?)int.Parse(dr["idGrupo"].ToString()) : null;
                    partido.idFecha = int.Parse(dr["idFecha"].ToString());
                    if (dr["idEquipoLocal"] != DBNull.Value && dr["idEquipoVisitante"] != DBNull.Value)
                        partido.nombreCompleto = daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoLocal"].ToString())).nombre + " vs. " + daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoVisitante"].ToString())).nombre;
                }
                if (dr != null)
                    dr.Close();
                return partido;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Los Goles de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Gol</returns>
        public List<Gol> obtenerGoles(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Gol> goles = new List<Gol>();
            Gol gol = null; 
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Goles
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    gol = new Gol();
                    DAOJugador daoJugador = new DAOJugador();
                    DAOEquipo daoEquipo = new DAOEquipo();
                    gol.idGol = Int32.Parse(dr["idGol"].ToString());
                    gol.minuto = (dr["minuto"] != DBNull.Value) ? (Int32?)Int32.Parse(dr["minuto"].ToString()) : null;
                    gol.jugador = (dr["idJugador"] != DBNull.Value) ? daoJugador.obtenerJugadorPorId(Int32.Parse(dr["idJugador"].ToString())) : null;
                    gol.equipo = (dr["idEquipo"] != DBNull.Value) ? daoEquipo.obtenerEquipoPorId(Int32.Parse(dr["idEquipo"].ToString())) : null;
                    gol.tipoGol = obtenerTipoGolPorId(Int32.Parse(dr["idTipoGol"].ToString()));                    
                    goles.Add(gol);
                }
                if (dr != null)
                    dr.Close();
                return goles;            
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los goles del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Las Tarjetas de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Tarjeta</returns>
        public List<Tarjeta> obtenerTarjetas(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            Tarjeta tarjeta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Tarjetas 
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tarjeta = new Tarjeta();
                    DAOJugador daoJugador = new DAOJugador();
                    DAOEquipo daoEquipo = new DAOEquipo();
                    tarjeta.idTarjeta = Int32.Parse(dr["idTarjeta"].ToString());
                    tarjeta.minuto = (dr["minuto"] != DBNull.Value) ? (Int32?)Int32.Parse(dr["minuto"].ToString()) : null;
                    tarjeta.jugador = daoJugador.obtenerJugadorPorId(Int32.Parse(dr["idJugador"].ToString()));                    
                    tarjeta.equipo = daoEquipo.obtenerEquipoPorId(Int32.Parse(dr["idEquipo"].ToString()));
                    tarjeta.tipoTarjeta = char.Parse(dr["tipoTarjeta"].ToString());
                    tarjetas.Add(tarjeta);
                }
                if (dr != null)
                    dr.Close();
                return tarjetas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las tarjetas del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene Los Cambios de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Cambio</returns>
        public List<Cambio> obtenerCambios(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Cambio> cambios = new List<Cambio>();
            Cambio cambio = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM Cambios 
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cambio = new Cambio();
                    DAOJugador daoJugador = new DAOJugador();
                    DAOEquipo daoEquipo = new DAOEquipo();
                    cambio.idCambio = Int32.Parse(dr["idCambio"].ToString());
                    cambio.minuto = (dr["minuto"] != System.DBNull.Value) ? (Int32?)Int32.Parse(dr["minuto"].ToString()) : null;
                    cambio.jugadorEntra = daoJugador.obtenerJugadorPorId(Int32.Parse(dr["idJugadorEntra"].ToString()));
                    cambio.jugadorSale = daoJugador.obtenerJugadorPorId(Int32.Parse(dr["idJugadorSale"].ToString()));
                    cambio.equipo = daoEquipo.obtenerEquipoPorId(Int32.Parse(dr["idEquipo"].ToString()));
                    cambios.Add(cambio);
                }
                if (dr != null)
                    dr.Close();
                return cambios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los cambios del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }       

        /// <summary>
        /// Obtiene Los Titulares de un Partido
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idPartido">Id de Partido</param>
        /// <returns>Lista genérica de objeto Jugador</returns>
        public List<Jugador> obtenerTitularesDeUnPartido(int idPartido, int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Jugador> titulares = new List<Jugador>();
            Jugador jugador = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TitularesXPartido
                                WHERE idPartido = @idPartido AND idEquipo = @idEquipo";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    jugador = new Jugador();
                    DAOJugador daoJugador = new DAOJugador();
                    jugador = daoJugador.obtenerJugadorPorId(Int32.Parse(dr["idJugador"].ToString()));
                    titulares.Add(jugador);
                }
                if (dr != null)
                    dr.Close();
                return titulares;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los titulares del partido:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

       /// <summary>
       /// Modifica un partido en la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public void modificarPartido(Partido partido)
        {
            eliminarTitularesDePartido(partido.idPartido);
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"UPDATE Partidos
                                SET fecha = @fecha,
                                idEstado = @idEstado, idArbitro = @idArbitro, idCancha = @idCancha, golesLocal = @golesLocal, 
                                huboPenales = @huboPenales, penalesLocal = @penalesLocal, penalesVisitante = @penalesVisitante,
                                golesVisitante = @golesVisitante, idGanador = @idGanador, idPerdedor = @idPerdedor, empate = @empate
                                WHERE idPartido = @idPartido ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@fecha", DAOUtils.dbValueNull(partido.fecha));
                cmd.Parameters.AddWithValue("@idEstado", partido.estado.idEstado);
                cmd.Parameters.AddWithValue("@idArbitro", DAOUtils.dbValueNull((partido.arbitro != null) ? (int?)partido.arbitro.idArbitro : null));
                cmd.Parameters.AddWithValue("@idCancha", DAOUtils.dbValueNull((partido.cancha != null) ? (int?)partido.cancha.idCancha : null));
                cmd.Parameters.AddWithValue("@golesLocal", DAOUtils.dbValueNull(partido.golesLocal));
                cmd.Parameters.AddWithValue("@golesVisitante", DAOUtils.dbValueNull(partido.golesVisitante));
                cmd.Parameters.AddWithValue("@huboPenales", DAOUtils.dbValueNull(partido.huboPenales));
                cmd.Parameters.AddWithValue("@penalesLocal", DAOUtils.dbValueNull(partido.penalesLocal));
                cmd.Parameters.AddWithValue("@penalesVisitante", DAOUtils.dbValueNull(partido.penalesVisitante));
                cmd.Parameters.AddWithValue("@idGanador", DAOUtils.dbValueNull(partido.idGanador));
                cmd.Parameters.AddWithValue("@idPerdedor", DAOUtils.dbValueNull(partido.idPerdedor));
                cmd.Parameters.AddWithValue("@empate", DAOUtils.dbValueNull(partido.empate)); 
                cmd.Parameters.AddWithValue("@idPartido", partido.idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                eliminarGolesDePartido(partido.idPartido);
                foreach (Gol gol in partido.goles)
                {
                    registrarGol(gol, partido.idPartido, con, trans);
                }
                eliminarTarjetasDePartido(partido.idPartido);
                foreach (Tarjeta tarjeta in partido.tarjetas)
                {
                    registrarTarjeta(tarjeta, partido.idPartido, con, trans);
                }
                eliminarCambiosDePartido(partido.idPartido);
                foreach (Cambio cambio in partido.cambios)
                {
                    registrarCambio(cambio, partido.idPartido, con, trans);
                }
                if(partido.local.idEquipo != null)
                    registrarTitularesAPartido(partido.titularesLocal, partido.local.idEquipo, partido.idPartido, con, trans);//titulares del equipo local
                if(partido.visitante.idEquipo != null)
                    registrarTitularesAPartido(partido.titularesVisitante, partido.visitante.idEquipo, partido.idPartido, con, trans);//titulares del equipo visitante}      
               if(partido.faseAsociada.tipoFixture.idTipoFixture=="ELIM" && partido.idPartidoPosterior != 0)
                guardarEquipoEnLLaveSiguiente(partido, con, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {  
                trans.Rollback();
                throw new Exception("No se pudo registrar el equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
       
       /// <summary>
       /// Obtiene los Tipos Goles de la BD
       /// autor: Pau Pedrosa
       /// </summary>
        public List<TipoGol> obtenerTiposGol()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<TipoGol> respuesta = new List<TipoGol>();
            TipoGol tipoGol = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM TiposGol";
                cmd.Parameters.Clear();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tipoGol = new TipoGol()
                    {
                        idTipoGol = Int32.Parse(dr["idTipoGol"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    respuesta.Add(tipoGol);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
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
       /// Obtener Tipo gol por Id
       /// autor: Pau Pedrosa
       /// </summary>
        public TipoGol obtenerTipoGolPorId(int idTipoGol)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            TipoGol respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TiposGol
                                WHERE idTipoGol = @idTipoGol";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTipoGol", idTipoGol);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new TipoGol();
                    respuesta.idTipoGol = Int32.Parse(dr["idTipoGol"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();                    
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Tipo Gol: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina los goles de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarGolesDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Goles
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el gol: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Eliminar los titulares de un partido
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarTitularesDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM TitularesXPartido
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar los titulares del partido: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina los cambios de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarCambiosDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Cambios
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el cambio: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina las tarjetas de un partido de la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarTarjetasDePartido(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Tarjetas
                                WHERE idPartido = @idPartido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la tarjeta: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Graba el equipo ganador de un partido, en el partido de la llave siguiente (solo apra eliminatorios)
        /// autor: Florencia Rojas
        /// </summary>
        public void guardarEquipoEnLLaveSiguiente(Partido p, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();                
                cmd.Transaction = trans;
                cmd.Connection = con;
                string sql = @" 
                                DECLARE @nombreFecha AS varchar(10) = (SELECT f.nombre FROM Partidos p INNER JOIN Fechas f on (p.idFecha=f.idFecha and p.idGrupo=f.idGrupo and p.idFase = f.idFase AND p.idEdicion = f.idEdicion) WHERE p.idPartido=@idPartido )                                
                                DECLARE @idFecha AS int = (SELECT p.idFecha FROM Partidos p  WHERE p.idPartido=@idPartidoPosterior)
                                DECLARE @idGrupo AS int = (SELECT p.idGrupo FROM Partidos p  WHERE p.idPartido=@idPartidoPosterior)
                                DECLARE @idPartidoComplementario AS int = (SELECT idPartido FROM Partidos WHERE idPartidoPosterior = @idPartidoPosterior AND idPartido <> @idPartido)  
                                IF @idPartido < @idPartidoComplementario
                                    BEGIN
                                        UPDATE Partidos SET idEquipoLocal = @idGanador WHERE idPartido = @idPartidoPosterior
                                    END
                                    ELSE
                                        UPDATE Partidos SET idEquipoVisitante = @idGanador WHERE idPartido = @idPartidoPosterior 
		                               
	                            IF @nombreFecha like 'Semifinal'
	                                BEGIN
	                                    DECLARE @idPartidoTercerPuesto AS int = (SELECT idPartido FROM Partidos WHERE idFecha =@idFecha AND idFase=@idFase AND idGrupo=@idGrupo AND idEdicion=@idEdicion AND  idPartido <> @idPArtidoPosterior )
	                                    IF @idPartido < @idPartidoComplementario
		                                BEGIN
			                                UPDATE Partidos SET idEquipoLocal = @idPerdedor WHERE idPartido = @idPartidoTercerPuesto
		                                END
		                                ELSE
			                                UPDATE Partidos SET idEquipoVisitante = @idPerdedor WHERE idPartido = @idPartidoTercerPuesto
	                                END
                                ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido",p.idPartido);
                cmd.Parameters.AddWithValue("@idGanador", DAOUtils.dbValueNull(p.idGanador));
                cmd.Parameters.AddWithValue("@idPerdedor", DAOUtils.dbValueNull(p.idPerdedor));
                cmd.Parameters.AddWithValue("@idFase", DAOUtils.dbValueNull(p.faseAsociada.idFase));
                cmd.Parameters.AddWithValue("@idEdicion", DAOUtils.dbValueNull(p.faseAsociada.idEdicion));
                cmd.Parameters.AddWithValue("@idPartidoPosterior", DAOUtils.dbValueNull(p.idPartidoPosterior));
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    (new DAOFecha()).actualizarFechaEliminatorio(p.idPartido,  con, trans);                    
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo grabar equipo en el partido de la siguiente llave " + ex.Message);
            }
        }


        /// <summary>
        /// Actualiza los idPartido Posterior para las fases eliminatorias
        /// autor: Flor Rojas
        /// </summary>
        public void actualizarPartidosEliminatorios(Fase fase, SqlConnection con, SqlTransaction tran)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            asignarPartidosPosteriores(fase);
            try
            {       
                foreach (Fecha fechaaModificar in fase.grupos[0].fechas)
                {
                    foreach (Partido partido in fechaaModificar.partidos)
                    {
                        string sql = @"UPDATE Partidos
                                SET idPArtidoPosterior=@idPartidoPosterior
                                WHERE idPartido = @idPartido ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idPartido", partido.idPartido);
                        cmd.Parameters.AddWithValue("@idPartidoPosterior", partido.idPartidoPosterior);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("No se pudo registrar la fase eliminatoria: " + ex.Message);                
            }            
        }


       

        /// <summary>
        /// Asigna un valor para el atributo idPartidoPosterior para una fase eliminatoria
        /// </summary>
        /// <param name="fase"vos id>La fase con todos los partidos de la eliminatoria, con sus respectivos ID</param>
        public void asignarPartidosPosteriores(Fase faseEliminatoria)
        {
            foreach (Fecha f in faseEliminatoria.grupos[0].fechas.OrderByDescending(f => f.idFecha))
            {
                int j = 0;
                if (f.idFecha == faseEliminatoria.grupos[0].fechas.Count)
                {
                    Fecha fechaAnterior = faseEliminatoria.grupos[0].fechas[f.idFecha - 2];
                    fechaAnterior.partidos[j].idPartidoPosterior = f.partidos[0].idPartido;
                    if (fechaAnterior.partidos.Count > 1)
                    {
                        fechaAnterior.partidos[j + 1].idPartidoPosterior = f.partidos[0].idPartido;
                        j = j + 2;
                    }
                    continue;
                }
                else
                {
                    foreach (Partido p in f.partidos)
                    {

                        if (f.idFecha > 1)
                        {
                            Fecha fechaAnterior = faseEliminatoria.grupos[0].fechas[f.idFecha - 2];
                            fechaAnterior.partidos[j].idPartidoPosterior = p.idPartido;
                            if (fechaAnterior.partidos.Count > 1)
                            {
                                fechaAnterior.partidos[j + 1].idPartidoPosterior = p.idPartido;
                                j = j + 2;
                            }
                        }
                        else
                            break;
                    }
                }
            }
        }

        public void cambiarEstadosAPartidos(int idEstado, int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Partidos
                                SET idEstado = @idEstado
                                WHERE idEdicion = @idEdicion
                                AND idEstado NOT IN (@estadoJugado)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@estadoJugado", Estado.partidoJUGADO);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cambiar el estado de los partidos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void cambiarEstadosAPartidos(int idEstado, int idEdicion, int idFase, SqlCommand cmd)
        {
            try
            {
                string sql = @"UPDATE Partidos
                                SET idEstado = @idEstado
                                WHERE idEdicion = @idEdicion AND idFase = @idFase
                                AND idEstado NOT IN (@estadoJugado)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@idFase", idFase);
                cmd.Parameters.AddWithValue("@estadoJugado", Estado.partidoJUGADO);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cambiar el estado de los partidos: " + ex.Message);
            }
        }

        /// <summary>
        /// Permite obtener otros partidos de la fecha. Todos los partidos MENOS el que está siendo consultado
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Partido> otrosPartidosDeLaFecha(int idEdicion, int idFase, int idFecha, int idPartidoActual)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            DataTable tablaDeDatos = new DataTable();
            List<Partido> listaPartidos = new List<Partido>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT p.idPartido AS 'Id Partido', p.fecha AS 'Fecha Partido', 
                                equipoLocal.idEquipo AS 'Id Local', UPPER (SUBSTRING (equipoLocal.nombre, 1, 3)) AS 'Equipo Local Nombre Corto',
                                equipoLocal.nombre AS 'Equipo Local',
                                p.golesLocal AS 'Goles Local', p.penalesLocal AS 'Penales Local',
                                p.golesVisitante AS 'Goles Visitante', p.penalesVisitante AS 'Penales Visitante',
                                equipoVisitante.idEquipo AS 'Id Visitante', UPPER (SUBSTRING (equipoVisitante.nombre, 1, 3)) AS 'Equipo Visitante Nombre Corto',
                                equipoVisitante.nombre AS 'Equipo Visitante',
                                estado.nombre AS 'Estado Partido', p.huboPenales AS 'Hubo Penales'
                                FROM Partidos p
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                INNER JOIN Estados estado ON p.idEstado = estado.idEstado
                                WHERE p.idEdicion = @idEdicion AND p.idFecha = @idFecha AND p.idFase = @idFase
                                AND p.idPartido <> @idPartidoActual";

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idPartidoActual", idPartidoActual));
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@idFecha", idFecha));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Partido partido = new Partido();
                    partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                    partido.fecha = (dr["Fecha Partido"] != DBNull.Value) ? (DateTime?)Utils.Validador.castDate(dr["Fecha Partido"].ToString()) : null;
                    partido.golesLocal = (dr["Goles Local"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Local"].ToString()) : null;
                    partido.golesVisitante = (dr["Goles Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Visitante"].ToString()) : null;
                    partido.huboPenales = (dr["Hubo Penales"] != DBNull.Value) ? (bool?)bool.Parse(dr["Hubo Penales"].ToString()) : null;
                    partido.penalesLocal = (dr["Penales Local"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Local"].ToString()) : null;
                    partido.penalesVisitante = (dr["Penales Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Visitante"].ToString()) : null;
                    partido.local.idEquipo = int.Parse(dr["Id Local"].ToString());
                    partido.local.nombre = (dr["Equipo Local"].ToString());
                    partido.local.nombreCorto = (dr["Equipo Local Nombre Corto"].ToString());
                    partido.visitante.idEquipo = int.Parse(dr["Id Visitante"].ToString());
                    partido.visitante.nombreCorto = (dr["Equipo Visitante Nombre Corto"].ToString());
                    partido.visitante.nombre = (dr["Equipo Visitante"].ToString());
                    partido.estado.nombre = (dr["Estado Partido"].ToString());           
                    listaPartidos.Add(partido);
                }
                if (dr != null)
                    dr.Close();
                return listaPartidos;
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

        public List<Partido> ultimosPartidosPrevioDeUnEquipo(int idEquipo, int idEdicion, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Partido> listaPartidos = new List<Partido>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT TOP 5 equipoLocal.nombre AS 'Equipo Local', equipoLocal.idEquipo AS 'Id Equipo Local',
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                p.idFecha AS 'Fecha', p.idPartido AS 'Id Partido', p.huboPenales AS 'Hubo Penales', p.penalesLocal AS
                                'Penales Local', p.penalesVisitante AS 'Penales Visitante'
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
                while (dr.Read())
                {
                    Partido partido = new Partido();
                    partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                    partido.idFecha = int.Parse(dr["Fecha"].ToString());
                    partido.golesLocal = (dr["Goles Local"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Local"].ToString()) : null;
                    partido.golesVisitante = (dr["Goles Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Visitante"].ToString()) : null;
                    partido.huboPenales = (dr["Hubo Penales"] != DBNull.Value) ? (bool?)bool.Parse(dr["Hubo Penales"].ToString()) : null;
                    partido.penalesLocal = (dr["Penales Local"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Local"].ToString()) : null;
                    partido.penalesVisitante = (dr["Penales Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Visitante"].ToString()) : null;
                    partido.local.idEquipo = int.Parse(dr["Id Equipo Local"].ToString());
                    partido.local.nombre = (dr["Equipo Local"].ToString());
                    partido.visitante.idEquipo = int.Parse(dr["Id Equipo Visitante"].ToString());
                    partido.visitante.nombre = (dr["Equipo Visitante"].ToString());
                    listaPartidos.Add(partido);
                }
                if (dr != null)
                    dr.Close();
                return listaPartidos;
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

        public List<Partido> proximosPartidosDeUnEquipo(int idEquipo, int idEdicion, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Partido> listaPartidos = new List<Partido>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT TOP 5 equipoLocal.nombre AS 'Equipo Local', equipoLocal.idEquipo AS 'Id Equipo Local',
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                p.idFecha AS 'Fecha', p.idPartido AS 'Id Partido', p.huboPenales AS 'Hubo Penales', p.penalesLocal AS
                                'Penales Local', p.penalesVisitante AS 'Penales Visitante'
                                FROM Partidos p 
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE p.idEdicion = @idEdicion 
                                AND (p.idEquipoLocal = @idEquipo OR p.idEquipoVisitante = @idEquipo)
                                AND (p.idEstado = @estadoProgramado OR p.idEstado = @estadoDiagramado)
                                AND idPartido <> @idPartido 
                                AND idPartido > @idPartido                               
                                ORDER BY idPartido ASC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idEquipo", idEquipo));
                cmd.Parameters.Add(new SqlParameter("@idPartido", idPartido));
                cmd.Parameters.Add(new SqlParameter("@estadoProgramado", Estado.partidoPROGRAMADO));
                cmd.Parameters.Add(new SqlParameter("@estadoDiagramado", Estado.partidoDIAGRAMADO));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Partido partido = new Partido();
                    partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                    partido.idFecha = int.Parse(dr["Fecha"].ToString());
                    partido.golesLocal = (dr["Goles Local"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Local"].ToString()) : null;
                    partido.golesVisitante = (dr["Goles Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Visitante"].ToString()) : null;
                    partido.huboPenales = (dr["Hubo Penales"] != DBNull.Value) ? (bool?)bool.Parse(dr["Hubo Penales"].ToString()) : null;
                    partido.penalesLocal = (dr["Penales Local"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Local"].ToString()) : null;
                    partido.penalesVisitante = (dr["Penales Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Visitante"].ToString()) : null;
                    partido.local.idEquipo = int.Parse(dr["Id Equipo Local"].ToString());
                    partido.local.nombre = (dr["Equipo Local"].ToString());
                    partido.visitante.idEquipo = int.Parse(dr["Id Equipo Visitante"].ToString());
                    partido.visitante.nombre = (dr["Equipo Visitante"].ToString());
                    listaPartidos.Add(partido);
                }
                if (dr != null)
                    dr.Close();
                return listaPartidos;
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

        public List<Partido> ultimosPartidosDeUnEquipo(int idEquipo, int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Partido> listaPartidos = new List<Partido>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT TOP 10 p.idPartido AS 'Id Partido', equipoLocal.idEquipo AS 'Id Equipo Local',
                                equipoLocal.nombre AS 'Equipo Local', 
                                p.golesLocal AS 'Goles Local', p.golesVisitante AS 'Goles Visitante',
                                equipoVisitante.idEquipo AS 'Id Equipo Visitante', equipoVisitante.nombre AS 'Equipo Visitante', 
                                p.fecha AS 'Fecha', idGanador, idPerdedor, p.huboPenales AS 'Hubo Penales',
                                p.penalesLocal AS 'Penales Local', p.penalesVisitante AS 'Penales Visitante',
                                CASE  
					                WHEN p.idGanador = @idEquipo THEN 'Ganado' 
					                WHEN p.idPerdedor = @idEquipo THEN 'Perdido'  
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
                DAOEquipo daoEquipo = new DAOEquipo();
                while (dr.Read())
                {
                    Partido partido = new Partido();
                    partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                    partido.golesLocal = (dr["Goles Local"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Local"].ToString()) : null;
                    partido.golesVisitante = (dr["Goles Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Goles Visitante"].ToString()) : null;
                    partido.huboPenales = (dr["Hubo Penales"] != DBNull.Value) ? (bool?)bool.Parse(dr["Hubo Penales"].ToString()) : null;
                    partido.penalesLocal = (dr["Penales Local"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Local"].ToString()) : null;
                    partido.penalesVisitante = (dr["Penales Visitante"] != DBNull.Value) ? (int?)int.Parse(dr["Penales Visitante"].ToString()) : null;
                    partido.local = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Local"].ToString()));
                    partido.visitante= daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Visitante"].ToString()));
                    partido.resultadoParaUnEquipo = (dr["Resultado"].ToString());
                    listaPartidos.Add(partido);
                }
                if (dr != null)
                    dr.Close();
                return listaPartidos;
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

        public Partido proximoPartidoDeEdicion(int idEdicion, int idFase, int idFecha)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Partido partido = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT TOP 1 partido.fecha AS 'Fecha', equipoLocal.idEquipo AS 'Id Equipo Local', 
                                equipoLocal.nombre AS 'Equipo Local', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante',
                                partido.idPartido AS 'Id Partido'
                                FROM Partidos partido
                                INNER JOIN Equipos equipoLocal ON partido.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON partido.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE partido.idEstado = @estadoProgramado
                                AND partido.idEdicion = @idEdicion

                                AND partido.idFase = @idFase
                                AND partido.idFecha = @idFecha
                                AND partido.idEquipoLocal IS NOT NULL AND partido.idEquipoVisitante IS NOT NULL
                                ORDER BY partido.fecha ASC";
                                                //AND partido.fecha >= GETDATE()-> esto hay que agregarlo en esta consulta
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idFecha", idFecha));
                cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                cmd.Parameters.Add(new SqlParameter("@estadoProgramado", Estado.partidoPROGRAMADO));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DAOEquipo daoEquipo = new DAOEquipo();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        partido = new Partido();
                        partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                        partido.fecha = Utils.Validador.castDate(dr["Fecha"].ToString());
                        partido.local = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Local"].ToString()));
                        partido.visitante = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Visitante"].ToString()));
                    }
                }
                else
                {
                    if (dr != null)
                        dr.Close();
                    string sql2 = @"SELECT TOP 1 equipoLocal.idEquipo AS 'Id Equipo Local', 
                                equipoLocal.nombre AS 'Equipo Local', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante',
                                partido.idPartido AS 'Id Partido'
                                FROM Partidos partido
                                INNER JOIN Equipos equipoLocal ON partido.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON partido.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE partido.idEstado = @estadoDiagramado
                                AND partido.idEdicion = @idEdicion
                                AND partido.idFase = @idFase
                                AND partido.idFecha = @idFecha
                                AND partido.idEquipoLocal IS NOT NULL AND partido.idEquipoVisitante IS NOT NULL
                                ORDER BY partido.fecha ASC";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                    cmd.Parameters.Add(new SqlParameter("@idFecha", idFecha));
                    cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                    cmd.Parameters.Add(new SqlParameter("@estadoDiagramado", Estado.partidoDIAGRAMADO));
                    cmd.CommandText = sql2;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            partido = new Partido();
                            partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                            partido.local = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Local"].ToString()));
                            partido.visitante = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Visitante"].ToString()));
                        }
                    }
                    else
                    {
                        if (dr != null)
                            dr.Close();
                        string sql3 = @"SELECT TOP 1 equipoLocal.idEquipo AS 'Id Equipo Local', 
                                equipoLocal.nombre AS 'Equipo Local', equipoVisitante.idEquipo AS 'Id Equipo Visitante',
                                equipoVisitante.nombre AS 'Equipo Visitante',
                                partido.idPartido AS 'Id Partido'
                                FROM Partidos partido
                                INNER JOIN Equipos equipoLocal ON partido.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON partido.idEquipoVisitante = equipoVisitante.idEquipo
                                WHERE (partido.idEstado = @estadoDiagramado OR partido.idEstado = @estadoProgramado)
                                AND partido.idEdicion = @idEdicion
                                AND partido.idFase = @idFase
                                AND partido.idEquipoLocal IS NOT NULL AND partido.idEquipoVisitante IS NOT NULL
                                ORDER BY partido.fecha ASC";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                        cmd.Parameters.Add(new SqlParameter("@idFase", idFase));
                        cmd.Parameters.Add(new SqlParameter("@estadoDiagramado", Estado.partidoDIAGRAMADO));
                        cmd.Parameters.Add(new SqlParameter("@estadoProgramado", Estado.partidoPROGRAMADO));
                        cmd.CommandText = sql3;
                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                partido = new Partido();
                                partido.idPartido = int.Parse(dr["Id Partido"].ToString());
                                partido.local = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Local"].ToString()));
                                partido.visitante = daoEquipo.obtenerEquipoPorId(int.Parse(dr["Id Equipo Visitante"].ToString()));
                            }
                        } 
                    }
                }
                if (dr != null)
                    dr.Close();
                return partido;
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

        public List<Partido> ultimosPartidosDeUnaEdicion(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Partido> listaPartidos = new List<Partido>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT p.idPartido AS 'idPartido', equipoLocal.idEquipo AS 'idEquipoLocal',
                                p.golesLocal AS 'golesLocal', p.golesVisitante AS 'golesVisitante',
                                equipoLocal.nombre AS 'nombreEquipoLocal', equipoVisitante.idEquipo AS 'idEquipoVisitante',
                                equipoVisitante.nombre AS 'nombreEquipoVisitante', cancha.nombre AS 'cancha',
                                p.fecha AS 'fechaPartido', p.penalesLocal AS 'penalesLocal', p.penalesVisitante AS 'penalesVisitante',
                                arbitro.idArbitro AS 'idArbitro', arbitro.nombre AS 'arbitro', cancha.idCancha AS 'idCancha', cancha.nombre AS 'cancha'
                                FROM Partidos p 
                                INNER JOIN Equipos equipoLocal ON p.idEquipoLocal = equipoLocal.idEquipo
                                INNER JOIN Equipos equipoVisitante ON p.idEquipoVisitante = equipoVisitante.idEquipo
                                LEFT JOIN Arbitros arbitro ON p.idArbitro = arbitro.idArbitro
                                LEFT JOIN Canchas cancha ON p.idCancha = cancha.idCancha
                                WHERE p.idEdicion = @idEdicion
                                AND p.idEstado = @estadoJugado
                                ORDER BY idPartido DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@estadoJugado", Estado.partidoJUGADO));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DAOEquipo daoEquipo = new DAOEquipo();
                while (dr.Read())
                {
                    Partido partido = new Partido();
                    partido.idPartido = int.Parse(dr["idPartido"].ToString());
                    partido.fecha = (dr["fechaPartido"] != DBNull.Value) ? (DateTime?)Utils.Validador.castDate(dr["fechaPartido"].ToString()) : null;
                    partido.golesLocal = (dr["golesLocal"] != DBNull.Value) ? (int?)int.Parse(dr["golesLocal"].ToString()) : null;
                    partido.golesVisitante = (dr["golesVisitante"] != DBNull.Value) ? (int?)int.Parse(dr["golesVisitante"].ToString()) : null;
                    partido.penalesLocal = (dr["penalesLocal"] != DBNull.Value) ? (int?)int.Parse(dr["penalesLocal"].ToString()) : null;
                    partido.penalesVisitante = (dr["penalesVisitante"] != DBNull.Value) ? (int?)int.Parse(dr["penalesVisitante"].ToString()) : null;
                    partido.local = daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoLocal"].ToString()));
                    partido.visitante = daoEquipo.obtenerEquipoPorId(int.Parse(dr["idEquipoVisitante"].ToString()));
                    partido.arbitro.idArbitro = (dr["idArbitro"] != DBNull.Value) ? int.Parse(dr["idArbitro"].ToString()) : 0;
                    partido.arbitro.nombre = (dr["arbitro"] != DBNull.Value) ? dr["arbitro"].ToString() : "";
                    partido.cancha.idCancha = (dr["idCancha"] != DBNull.Value) ? int.Parse(dr["idCancha"].ToString()) : 0;
                    partido.cancha.nombre = (dr["cancha"] != DBNull.Value) ? dr["cancha"].ToString() : "";
                    listaPartidos.Add(partido);
                }
                if (dr != null)
                    dr.Close();
                return listaPartidos;
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
