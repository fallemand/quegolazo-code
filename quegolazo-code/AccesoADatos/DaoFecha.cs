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
   public class DAOFecha
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Fechas de un Partido, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="delegado">Nuevo delegado a registrar</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarFechas(Fase fase, SqlConnection con, SqlTransaction trans)
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
                        string sql = @"INSERT INTO Fechas (idFecha, idGrupo, idFase, idEdicion, nombre, idEstado)
                                        VALUES (@idFecha, @idGrupo, @idFase, @idEdicion, @nombre, @idEstado)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                        cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@idEstado", Estado.fechaINCOMPLETA);
                        cmd.Parameters.AddWithValue("@nombre", DAOUtils.dbValueNull(f.nombre));
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la fecha:" + ex.Message);
            }
        }

       /// <summary>
       /// Obtiene las fechas de una fase
       /// autor: Flor Rojas
       /// </summary>
        public void obtenerFechas(Fase fase, SqlConnection con)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;                
                foreach (Grupo g in fase.grupos)
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    string sql = @"SELECT * 
                                    FROM Fechas
                                    WHERE idGrupo = @idGrupo AND idFase = @idFase AND idEdicion = @idEdicion";
                    cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                    cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                    cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Fecha fecha = new Fecha()
                        {
                            idFecha = int.Parse(dr["idFecha"].ToString()),
                            estado = new Estado() { idEstado = int.Parse(dr["idEstado"].ToString()) },
                            nombre = dr["nombre"].ToString(),
                            nombreCompleto ="Fecha " + int.Parse(dr["idFecha"].ToString())
                        };
                        g.fechas.Add(fecha);
                    }
                    if (dr != null)
                        dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los datos de la fecha" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

       /// <summary>
       /// Cambia el estado de la fecha a Completa cuando se jugaron todos los partidos y devuelve true si se completó la fecha
       /// autor: Flor Rojas
       /// </summary>
        public bool actualizarFecha(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
             cmd.Connection = con;
             string sql = @"                            
                            DECLARE @idFecha AS int = (SELECT idFecha FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @idGrupo AS int = (SELECT idGrupo FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @idFase AS int = (SELECT idFase FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @idEdicion AS int = (SELECT idEdicion FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Partidos p WHERE p.idFecha = @idFecha AND p.idGrupo=@idGrupo AND p.idEdicion = @idEdicion AND p.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 4 AND idEstado<>13  ))
					                            if(@cantidad=0)
						                            BEGIN
							UPDATE Fechas SET idEstado = @idEstado WHERE idFecha = @idFecha AND idGrupo = @idGrupo AND idFase = @idFase AND idEdicion = @idEdicion
                            END
						    ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.Parameters.AddWithValue("@idEstado", Estado.fechaCOMPLETA);
                cmd.CommandText=sql;
                return (cmd.ExecuteNonQuery() > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el estado de la fecha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

//        /// <summary>
//        /// Verifica si es el primer partido e inicia la fecha, la fase y la edicion si es necesario
//        /// autor: Flor Rojas
//        /// </summary>
//        public bool iniciarFecha(int idFecha,int idGrupo, int idFase, int idEdicion)
//        {
//            SqlConnection con = new SqlConnection(cadenaDeConexion);
//            SqlCommand cmd = new SqlCommand();
//            try
//            {
//                if (con.State == ConnectionState.Closed)
//                    con.Open();
//                cmd.Connection = con;
//                string sql = @"    
//                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Partidos p WHERE p.idFecha = @idFecha AND idGrupo=@idGrupo AND p.idEdicion = @idEdicion AND p.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 4 AND idEstado<>13  ))
//					                            if(@cantidad=0)
//						                            BEGIN
//							UPDATE Fechas SET idEstado = @idEstado WHERE idFecha = @idFecha AND idGrupo = @idGrupo AND idFase = @idFase AND idEdicion = @idEdicion
//						    ";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@idPartido", idPartido);
//                cmd.Parameters.AddWithValue("@idEstado", Estado.fechaCOMPLETA);
//                cmd.CommandText = sql;
//                return (cmd.ExecuteNonQuery() > 0);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("No se pudo actualizar el estado de la fecha: " + ex.Message);
//            }
//            finally
//            {
//                if (con != null && con.State == ConnectionState.Open)
//                    con.Close();
//            }
//        }
    }
}
