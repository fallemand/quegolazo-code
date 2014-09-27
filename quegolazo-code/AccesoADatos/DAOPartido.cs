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
                    foreach (Fecha f in g.fixture)
                    {
                         foreach (Partido p in f.partidos)
                         {
                             
                                 string sql = @"INSERT INTO Partidos (idPartido,idFecha,idGrupo,idFase,idEdicion,idEquipoLocal,idEquipoVisitante, idEstado)
                                     VALUES (@idPartido,@idFecha,@idGrupo,@idFase,@idEdicion,@idEquipoLocal,@idEquipoVisitante, @idEstado)";
                                 cmd.Parameters.Clear();
                                 cmd.Parameters.AddWithValue("@idPartido", p.idPartido);
                                 cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                                 cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                                 cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                                 cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                                 if (p.local.idEquipo != 0)
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoLocal", p.local.idEquipo);
                                 }
                                 else
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoLocal", DBNull.Value);
                                 }
                                 if (p.visita.idEquipo != 0)
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoVisitante", p.visita.idEquipo);
                                 }
                                 else
                                 {
                                     cmd.Parameters.AddWithValue("@idEquipoVisitante", DBNull.Value);
                                 }
                                 cmd.Parameters.AddWithValue("@idEstado", p.estado.idEstado);
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

        public void obtenerPartidos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
             try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 cmd.Connection = con;
                 cmd.Transaction = trans;
                 foreach (Grupo g in fase.grupos)
                 {
                     foreach (Fecha f in g.fixture)
                     {
                         foreach (Partido p in f.partidos)
                         {
                             cmd.Parameters.Clear();
                             cmd.Connection = con;
                             string sql = @"SELECT * 
                                                    FROM Partidos
                                                    WHERE idFecha=@idFecha AND idGrupo=@idGrupo AND idFase=@idFase AND idEdicion=@idEdicion";
                             cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                             cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                             cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                             cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                             cmd.CommandText = sql;
                             dr = cmd.ExecuteReader();
                             while (dr.Read())
                             {
                                 Partido partido = new Partido()
                                 {
                                    
                                     idPartido = int.Parse(dr["idPartido"].ToString()),
                                     fecha = DateTime.Parse( dr["fecha"].ToString()).Date.ToString(),
                                     hora = DateTime.Parse(dr["fecha"].ToString()).Hour.ToString(),
                                     estado= new Estado(){ idEstado= int.Parse(dr["idEstado"].ToString())},
                                     local=new Equipo(){ idEquipo= int.Parse(dr["idEquipoLocal"].ToString())},
                                     visita = new Equipo() { idEquipo = int.Parse(dr["idEquipoVisitante"].ToString())}, 
                                 };
                                 f.partidos.Add(partido);
                             }
                             if (dr != null)
                                 dr.Close();  
                         }
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

        public int registrarGol(Gol gol, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Goles (minuto, idJugador, idEquipo, idPartido, idTipoGol)
                                    VALUES (@minuto, @idJugador, @idEquipo, @idPartido, @idTipoGol)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@minuto", gol.minuto);
                if (gol.idJugador != null)
                    cmd.Parameters.AddWithValue("@idJugador", gol.idJugador);
                else
                    cmd.Parameters.AddWithValue("@idJugador", DBNull.Value);
                if (gol.idEquipo != null)
                    cmd.Parameters.AddWithValue("@idEquipo", gol.idEquipo);
                else
                    cmd.Parameters.AddWithValue("@idEquipo", DBNull.Value);
                if(gol.tipoGol != null)
                    cmd.Parameters.AddWithValue("@idTipoGol", gol.tipoGol.idTipoGol);
                else
                    cmd.Parameters.AddWithValue("@idTipoGol", DBNull.Value);
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                int idGol = int.Parse(cmd.ExecuteScalar().ToString());
                return idGol; //retorna el id del gol generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el gol: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int registrarCambio(Cambio cambio, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Cambios (idEquipo, idJugadorEntra, idJugadorSale, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugadorEntra, @idJugadorSale, @minuto, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();                
                cmd.Parameters.AddWithValue("@idEquipo", cambio.idEquipo);
                cmd.Parameters.AddWithValue("@idJugadorEntra", cambio.idJugadorEntra);
                cmd.Parameters.AddWithValue("@idJugadorSale", cambio.idJugadorSale);
                if(cambio.minuto != null)
                    cmd.Parameters.AddWithValue("@minuto", cambio.minuto);  
                else
                    cmd.Parameters.AddWithValue("@minuto", DBNull.Value); 
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                int idCambio = int.Parse(cmd.ExecuteScalar().ToString());
                return idCambio; //retorna el id del cambio generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el cambio: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int registrarTarjeta(Tarjeta tarjeta, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Tarjetas (idEquipo, idJugador, tipoTarjeta, minuto, idPartido)
                                    VALUES (@idEquipo, @idJugador, @tipoTarjeta, @minuto, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", tarjeta.idEquipo);
                cmd.Parameters.AddWithValue("@idJugador", tarjeta.idJugador);
                cmd.Parameters.AddWithValue("@tipoTarjeta", tarjeta.tipoTarjeta);
                if (tarjeta.minuto != null)
                    cmd.Parameters.AddWithValue("@minuto", tarjeta.minuto);
                else
                    cmd.Parameters.AddWithValue("@minuto", DBNull.Value);
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                int idTarjeta = int.Parse(cmd.ExecuteScalar().ToString());
                return idTarjeta; //retorna el id de la tarjeta generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la tarjeta: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int registrarSancion(Sancion sancion, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Sanciones (idEquipo, idJugador, motivo, idPartido)
                                    VALUES (@idEquipo, @idJugador, @motivo, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", sancion.idEquipo);                
                if(sancion.idJugador != null)
                    cmd.Parameters.AddWithValue("@idJugador", sancion.idJugador);
                else
                    cmd.Parameters.AddWithValue("@idJugador", DBNull.Value);                
                if (sancion.motivo != null)
                    cmd.Parameters.AddWithValue("@motivo", sancion.motivo);
                else
                    cmd.Parameters.AddWithValue("@motivo", DBNull.Value);
                if(idPartido != null)
                    cmd.Parameters.AddWithValue("@idPartido", idPartido);
                else
                    cmd.Parameters.AddWithValue("@idPartido", DBNull.Value); 
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

        public void registrarTitularesAPartido(List<Jugador> jugadores, int idEquipo, int idPartido)
        {
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
                trans.Commit();
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar los jugadores titulares: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
