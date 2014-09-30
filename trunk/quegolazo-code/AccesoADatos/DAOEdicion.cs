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
    public class DAOEdicion
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Obtiene una lista de ediciones de un determinado torneo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista de Objeto Ediciones, o null sino existen ediciones de ese torneo</returns>
        public List<Edicion> obtenerEdicionesPorIdTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Edicion> respuesta = new List<Edicion>();
            Edicion edicion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;

                string sql = @"SELECT *
                                FROM Ediciones
                                WHERE idTorneo = @idTorneo
                                ORDER BY idEdicion DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DAOCancha daoCancha = new DAOCancha();
                DAOEdicion daoEdicion = new DAOEdicion();
                DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();
                DAOEstado daoEstado = new DAOEstado();
                DAOTorneo daoTorneo = new DAOTorneo();
                while (dr.Read())
                {
                    edicion = new Edicion();
                    edicion.idEdicion = Int32.Parse(dr["idEdicion"].ToString());
                    edicion.nombre = dr["nombre"].ToString();
                    edicion.tamanioCancha = daoCancha.obtenerTamanioCanchaPorId(Int32.Parse(dr["idTamanioCancha"].ToString()));
                    edicion.tipoSuperficie = daoTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(dr["idTipoSuperficie"].ToString()));
                    edicion.estado = daoEstado.obtenerEstadoPorId(Int32.Parse(dr["idEstado"].ToString()));
                    edicion.cancha = daoCancha.obtenerCanchasDeEdicion(Int32.Parse(dr["idEdicion"].ToString()));
                    edicion.puntosEmpatado = Int32.Parse(dr["puntosEmpatado"].ToString());
                    edicion.puntosGanado = Int32.Parse(dr["puntosGanado"].ToString());
                    edicion.puntosPerdido = Int32.Parse(dr["puntosPerdido"].ToString());
                    edicion.generoEdicion = daoEdicion.obtenerGeneroEdicionPorId(Int32.Parse(dr["idGeneroEdicion"].ToString()));
                    respuesta.Add(edicion);
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        /// <summary>
        /// Registrar una Nueva Edición
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="edicionNueva">Objeto nueva Edición</param>
        /// <param name="idTorneo">El id del torneo al cual se agregara la edicion</param>
        public void registrarEdicion(Edicion edicionNueva, int idTorneo)
        {          
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;               
                string sql = @"INSERT INTO Ediciones (nombre, idTamanioCancha, idTipoSuperficie, idEstado, idTorneo, puntosGanado, puntosPerdido, puntosEmpatado, idGeneroEdicion)
                                              VALUES (@nombre, @idTamanioCancha, @idTipoSuperficie, @idEstado, @idTorneo, @puntosGanado, @puntosPerdido, @puntosEmpatado, @idGeneroEdicion ) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", edicionNueva.nombre);     
                cmd.Parameters.AddWithValue("@idTamanioCancha", edicionNueva.tamanioCancha.idTamanioCancha);
                cmd.Parameters.AddWithValue("@idTipoSuperficie", edicionNueva.tipoSuperficie.idTipoSuperficie);
                cmd.Parameters.AddWithValue("@idEstado", edicionNueva.estado.idEstado);
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.Parameters.AddWithValue("@puntosEmpatado", edicionNueva.puntosEmpatado);
                cmd.Parameters.AddWithValue("@puntosGanado", edicionNueva.puntosGanado);
                cmd.Parameters.AddWithValue("@puntosPerdido", edicionNueva.puntosPerdido);
                cmd.Parameters.AddWithValue("@idGeneroEdicion", edicionNueva.generoEdicion.idGeneroEdicion);
                cmd.CommandText = sql;
                cmd.ExecuteScalar();                
            }
            catch (SqlException ex)
            {
                if (ex.Class == 14 && ex.Number == 2601)
                    throw new Exception("La edición " + edicionNueva.nombre + " ya se encuentra registrada. Ingrese otro nombre para la misma.");
                else
                    throw new Exception("No se pudo registrar la edición: " + ex.Message);
            }
            catch (Exception e)
            {                
                throw new Exception("No se pudo registrar la edición: " + e.Message);
            }               
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        /// <summary>
        /// Registrar Configuracion
        /// autor: Florencia Rojas
        /// </summary>
        /// <param name="edicion">Objeto Edición</param>
        public void registrarPreferencias(Edicion edicion, SqlConnection con, SqlTransaction trans)
        {
            //SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            //trans = con.BeginTransaction();
            cmd.Connection = con;
            cmd.Transaction = trans;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO ConfiguracionesEdicion (jugadores,cambiosJugadores, tarjetasJugadores, golesJugadores,asignacionArbitros, desempenioArbitros,canchaUnica,sancionesEquipos, sancionesJugadores, arbitros ,sanciones , idEdicion, jugadorXPartido )
                                              VALUES (@jugadores,@cambiosJugadores, @tarjetasJugadores, @golesJugadores, @asignacionArbitros, @desempenioArbitros, @canchaUnica, @sancionesEquipos, @sancionesJugadores, @arbitros ,@sanciones , @idEdicion, @jugadorXPartido)";
                cmd.Parameters.Clear();
                //Jugadores
                cmd.Parameters.AddWithValue("@jugadores", edicion.preferencias.jugadores);
                cmd.Parameters.AddWithValue("@cambiosJugadores", edicion.preferencias.cambiosJugadores);
                cmd.Parameters.AddWithValue("@tarjetasJugadores", edicion.preferencias.tarjetasJugadores);
                cmd.Parameters.AddWithValue("@golesJugadores", edicion.preferencias.golesJugadores);
                cmd.Parameters.AddWithValue("@jugadorXPartido", edicion.preferencias.jugadoresXPartido);
                //Arbitros
                cmd.Parameters.AddWithValue("@arbitros", edicion.preferencias.arbitros);
                cmd.Parameters.AddWithValue("@asignacionArbitros", edicion.preferencias.asignaArbitros);
                cmd.Parameters.AddWithValue("@desempenioArbitros", edicion.preferencias.desempenioArbitros);
                //Cancha
                cmd.Parameters.AddWithValue("@canchaUnica", edicion.preferencias.canchaUnica);
                //Sanciones
                cmd.Parameters.AddWithValue("@sancionesEquipos", edicion.preferencias.sancionesEquipos);
                cmd.Parameters.AddWithValue("@sancionesJugadores", edicion.preferencias.sancionesJugadores);
                cmd.Parameters.AddWithValue("@sanciones", edicion.preferencias.sanciones);
                //idEdición
                cmd.Parameters.AddWithValue("@idEdicion", edicion.idEdicion);
                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar las preferencias: " + e.Message);
            }
        }

        /// <summary>
        /// Transacción que permite confirmar una edición.
        /// Permite registrar preferencias en la BD
        /// Permite registrar Equipos en la Edición
        /// Permite registrar Fase
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="edicion">Edición con las preferencias, con lista de equipos y con lista de fases</param>
        public void confirmarEdicion(Edicion edicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
                registrarPreferencias(edicion, con, trans);
                DAOEquipo daoEquipo = new DAOEquipo();
                daoEquipo.registrarEquiposEnEdicion(edicion.equipos, edicion.idEdicion, con, trans);
                DAOFase daoFase = new DAOFase();
                daoFase.registrarFase(edicion.fases, con, trans);
                trans.Commit();
            }
            catch (Exception e)
            {                
                throw new Exception("No se pudo registrar la confirmación de la edición: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            } 
        }

        /// <summary>
        /// Obtiene de la BD una edición por Id
        /// autor: Paula Pedrosa
        /// </summary>
        public Edicion obtenerEdicionPorId(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Edicion respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Ediciones
                                WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                DAOTipoSuperficie daoTipoSupericie = new DAOTipoSuperficie();
                DAOCancha daoCancha = new DAOCancha();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Edicion();
                    respuesta.idEdicion = int.Parse(dr["idEdicion"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.tipoSuperficie = daoTipoSupericie.obtenerTipoSuperficiePorId(int.Parse(dr["idTipoSuperficie"].ToString()));
                    respuesta.tamanioCancha = daoCancha.obtenerTamanioCanchaPorId(int.Parse(dr["idTamanioCancha"].ToString()));
                    respuesta.puntosPerdido = int.Parse(dr["puntosPerdido"].ToString());
                    respuesta.puntosEmpatado = int.Parse(dr["puntosEmpatado"].ToString());
                    respuesta.puntosGanado = int.Parse(dr["puntosGanado"].ToString());
                    respuesta.generoEdicion = obtenerGeneroEdicionPorId(int.Parse(dr["idGeneroEdicion"].ToString()));
                    respuesta.estado = new Estado() { ambito=new Ambito(){idAmbito=Ambito.EDICION}, idEstado = int.Parse(dr["idEstado"].ToString()) };
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar la Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica de la Bd una edición
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarEdicion(Edicion edicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Ediciones
                                SET nombre = @nombre, idTamanioCancha = @idTamanioCancha,
                                idTipoSuperficie = @idTipoSuperficie, puntosEmpatado = @puntosEmpatado,
                                puntosGanado = @puntosGanado, puntosPerdido = @puntosPerdido, idGeneroEdicion = @idGeneroEdicion
                                WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", edicion.nombre);
                cmd.Parameters.AddWithValue("@idTamanioCancha", edicion.tamanioCancha.idTamanioCancha);
                cmd.Parameters.AddWithValue("@idTipoSuperficie", edicion.tipoSuperficie.idTipoSuperficie);
                cmd.Parameters.AddWithValue("@puntosEmpatado", edicion.puntosEmpatado);
                cmd.Parameters.AddWithValue("@puntosGanado", edicion.puntosGanado);
                cmd.Parameters.AddWithValue("@puntosPerdido", edicion.puntosPerdido);
                cmd.Parameters.AddWithValue("@idEdicion", edicion.idEdicion);
                cmd.Parameters.AddWithValue("@idGeneroEdicion", edicion.generoEdicion.idGeneroEdicion);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_torneo"))
                    throw new Exception("Ya existe ese nombre de Edición");
                throw new Exception("No se pudo modificar la Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina de la Bd una edición
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEdicion">Id de la edición a eliminar</param>
        /// <param name="idEstado">Id del estado</param>
        public void eliminarEdicion(int idEdicion, int idEstado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Ediciones
                                WHERE idEdicion = @idEdicion 
                                AND idEstado = @idEstado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la cancha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene todos los Géneros de Edición de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Lista genérica de objeto GeneroEdicion</returns>
        public List<GeneroEdicion> obtenerGenerosEdicion()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<GeneroEdicion> respuesta = new List<GeneroEdicion>();
            GeneroEdicion generoEdicion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM GenerosEdicion";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    generoEdicion = new GeneroEdicion()
                    {
                        idGeneroEdicion = Int32.Parse(dr["idGeneroEdicion"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    respuesta.Add(generoEdicion);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los Géneros de Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene un GeneroEdicion por Id de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idGeneroEdicion">Id del Género Edicion</param>
        /// <returns>Objeto GeneroEdicion</returns>
        public GeneroEdicion obtenerGeneroEdicionPorId(int idGeneroEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            GeneroEdicion respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM GenerosEdicion
                                WHERE idGeneroEdicion = @idGeneroEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idGeneroEdicion", idGeneroEdicion);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new GeneroEdicion()
                    {
                        idGeneroEdicion = Int32.Parse(dr["idGeneroEdicion"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los géneros de edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene ídTorneo
        /// autor: Flor Rojas
        /// </summary>
        public int obtenerTorneoPorId(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT idTorneo
                                FROM Ediciones
                                WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                int idTorneo =int.Parse(cmd.ExecuteScalar().ToString());
                return idTorneo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el idTorneo " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        
        /// <summary>
        /// Obtiene las preferencias de una edición por Id. Si no tiene devuelve null
        /// autor: Florencia Rojas
        /// </summary>
        public ConfiguracionEdicion obtenerPreferenciasPorId(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            ConfiguracionEdicion preferencias=new ConfiguracionEdicion();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM ConfiguracionesEdicion
                                WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                DAOTipoSuperficie daoTipoSupericie = new DAOTipoSuperficie();
                DAOCancha daoCancha = new DAOCancha();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    preferencias = new ConfiguracionEdicion();
                    //Jugadores
                    preferencias.jugadores = bool.Parse(dr["jugadores"].ToString());
                    preferencias.jugadoresXPartido = bool.Parse(dr["jugadorXPartido"].ToString());
                    preferencias.golesJugadores = bool.Parse(dr["golesJugadores"].ToString());
                    preferencias.tarjetasJugadores = bool.Parse(dr["tarjetasJugadores"].ToString());
                    preferencias.cambiosJugadores = bool.Parse(dr["cambiosJugadores"].ToString());
                    //Arbitros
                    preferencias.arbitros = bool.Parse(dr["arbitros"].ToString());
                    preferencias.asignaArbitros = bool.Parse(dr["asignacionArbitros"].ToString());
                    preferencias.desempenioArbitros = bool.Parse(dr["desempenioArbitros"].ToString());
                    //Sanciones
                    preferencias.sanciones = bool.Parse(dr["sanciones"].ToString());
                    preferencias.sancionesEquipos = bool.Parse(dr["sancionesEquipos"].ToString());
                    preferencias.sancionesJugadores = bool.Parse(dr["sancionesJugadores"].ToString());
                    //Canchas
                    preferencias.canchas = bool.Parse(dr["canchaUnica"].ToString());
                    preferencias.canchaUnica = bool.Parse(dr["canchaUnica"].ToString());
                }
                if (dr != null)
                    dr.Close();
                return preferencias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar las preferencias de una Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene los equipos  de una edición por Id. Si no tiene devuelve lista vacia
        /// autor: Florencia Rojas
        /// </summary>
        public List<Equipo> obtenerEquiposPorId(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Equipo> equipos = new List<Equipo>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT e.idEquipo,e.nombre 
                                FROM Equipos e,EquipoXEdicion ee 
                                WHERE e.idEquipo=ee.idEquipo and ee.idEdicion=@idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Equipo equipo = new Equipo()
                        {   idEquipo = int.Parse(dr["idEquipo"].ToString()),
                            nombre = dr["nombre"].ToString(),
                        };
                    equipos.Add(equipo);
                }
                if (dr != null)
                    dr.Close();
                return equipos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los equipos de una Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Permite determinar si una edición pertenece a un usuario
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>True: Si pertenece - False: No pertenece</returns>
        public bool perteneceAUsuario(int idEdicion, int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT e.idEdicion 
                                FROM Ediciones e INNER JOIN Torneos t ON e.idTorneo = t.idTorneo 
                                INNER JOIN Usuarios u ON t.idUsuario = u.idUsuario
                                WHERE e.idEdicion = @idEdicion AND u.idUsuario = @idUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Cambia de Estado a la Edición a PERSONALIZADA
        /// autor: Pau Pedrosa
        /// </summary>
        public void cambiarEstadoAPersonalizada(int idEdicion, int idEstado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Ediciones
                                SET idEstado = @idEstado
                                WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cambiar el estado de la Edición: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}