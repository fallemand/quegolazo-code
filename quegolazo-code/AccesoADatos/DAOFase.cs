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
    public class DAOFase
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary> 
        /// Registrar una nueva Fase 
        /// autor: Florencia Rojas        
        /// </summary>
        /// <param name="equipo">El objeto Equipo que se va a registrar</param>
        /// <param name="idTorneo">Id del torneo al que pertenece el equipo</param>
        /// <returns>El id del equipo registrado</returns>
        public void registrarFase(List<Fase> fases, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Fase fase in fases)
                {
                    if (fase != null)
                    {
                        string sql = @"INSERT INTO Fases (idFase, idEdicion, tipoFixture, idEstado)
                                        VALUES (@idFase, @idEdicion, @tipoFixture, @idEstado)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@tipoFixture", fase.tipoFixture.idTipoFixture);
                        cmd.Parameters.AddWithValue("@idEstado", Estado.faseDIAGRAMADA);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        if (fase.grupos.Count != 0)
                        {
                            DAOGrupo daoGrupo = new DAOGrupo();
                            daoGrupo.registrarGrupos(fase, con, trans);

                            DAOFecha daoFecha = new DAOFecha();
                            daoFecha.registrarFechas(fase, con, trans);

                            DAOPartido daoPartido = new DAOPartido();
                            daoPartido.registrarPartidos(fase, con, trans);
                        }
                    }
                }
            }
            catch (SqlException ex)
            { 
                trans.Rollback();
                throw new Exception("No se pudo registrar la Fase: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene las fases  de una edición por Id. Si no tiene devuelve lista vacia
        /// autor: Florencia Rojas
        /// </summary>
        public List<Fase> obtenerFases(int idEdicion)
        {
             SqlConnection con = new SqlConnection(cadenaDeConexion);
             SqlCommand cmd = new SqlCommand();
             SqlDataReader dr;
             SqlTransaction trans = null;
             List<Fase> fases = new List<Fase>();
             DAOGrupo daoGrupo= new DAOGrupo();
             DAOFecha daoFecha=new DAOFecha();
             DAOPartido daoPartido=new DAOPartido();
             try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 trans = con.BeginTransaction();
                 cmd.Connection = con;
                 cmd.Transaction = trans;
                 string sql = @"SELECT * 
                                FROM  Fases
                                WHERE idEdicion = @idEdicion";
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                 cmd.CommandText = sql;
                 dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {
                     Fase fase = new Fase()
                      {
                          idFase = int.Parse(dr["idFase"].ToString()),
                          idEdicion = idEdicion,
                          estado = new Estado() { idEstado = int.Parse(dr["idEstado"].ToString()) },
                          tipoFixture = new TipoFixture() { nombre = dr["tipoFixture"].ToString() },
                          equipos =
                      };        
                     fases.Add(fase);
                 }
                 if (dr != null)
                     dr.Close();

                 foreach (Fase fase in fases)
                 {
                     daoGrupo.obtenerGrupos(fase, con, trans);
                     daoFecha.obtenerFechas(fase, con, trans);
                     daoPartido.obtenerPartidos(fase, con, trans);
                 }
                 return fases;
             }
             catch (Exception ex)
             {
                 throw new Exception("Error al intentar recuperar las fases de una Edición: " + ex.Message);
             }
             finally
             {
                 if (con != null && con.State == ConnectionState.Open)
                     con.Close();
             }
        }

        /// <summary>
        /// Actualiza las fases
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="fases">Lista de Fases</param>
        /// <param name="con">Conexión</param>
        /// <param name="trans">Transacción</param>
        public void actualizarFase(List<Fase> fases, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;

                
                foreach (Fase fase in fases)
                {
                    if (fase != null)
                    {
                        string sqlEliminacion = "DELETE FROM Fases WHERE idFase = @idFase AND idEdicion = @idEdicion";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.CommandText = sqlEliminacion;
                        cmd.ExecuteNonQuery();

                        string sql = @"INSERT INTO Fases (idFase,idEdicion,tipoFixture,idEstado)
                                        VALUES (@idFase,@idEdicion,@tipoFixture,@idEstado)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@tipoFixture", fase.tipoFixture.nombre);
                        cmd.Parameters.AddWithValue("@idEstado", Estado.faseDIAGRAMADA);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        if (fase.grupos.Count != 0)
                        {
                            DAOGrupo daoGrupo = new DAOGrupo();
                            daoGrupo.registrarGrupos(fase, con, trans);

                            DAOFecha daoFecha = new DAOFecha();
                            daoFecha.registrarFechas(fase, con, trans);

                            DAOPartido daoPartido = new DAOPartido();
                            daoPartido.registrarPartidos(fase, con, trans);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar la Fase: " + ex.Message);
            }
        }


         /// <summary>
       /// Cambia el estado de la fase a Cerrada cuando se jugaron todos los partidos y devuelve true si se cerró y false si aún no
       /// autor: Flor Rojas
       /// </summary>
        public bool cerrarFase(int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
             cmd.Connection = con;
             string sql = @"                            
                            DECLARE @idFase AS int = (SELECT idFase FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @idEdicion AS int = (SELECT idEdicion FROM Partidos WHERE idPartido = @idPartido)
                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Partidos p WHERE  p.idEdicion = @idEdicion AND p.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 4 AND idEstado<>13  ))
	                            if(@cantidad=0)
	                             BEGIN
	                             UPDATE Fases SET idEstado =6 WHERE idFase = @idFase AND idEdicion = @idEdicion
                                 END";
             cmd.Parameters.Clear();
             cmd.Parameters.AddWithValue("@idPartido", idPartido);
             cmd.Parameters.AddWithValue("@idEstado", Estado.faseCERRADA);
             cmd.CommandText=sql;
             bool b= (cmd.ExecuteNonQuery() > 0) ? true : false;
             return b;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cerrar la fase: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
