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
                        string sql = @"INSERT INTO Fases (idFase, idEdicion, tipoFixture, idEstado, cantidadEquipos, cantidadGrupos)
                                        VALUES (@idFase, @idEdicion, @tipoFixture, @idEstado, @cantidadEquipos, @cantidadGrupos)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@tipoFixture", fase.tipoFixture.idTipoFixture);
                        cmd.Parameters.AddWithValue("@idEstado", Estado.faseDIAGRAMADA);
                        if (fase.esGenerica)
                        {
                            cmd.Parameters.AddWithValue("@cantidadEquipos", fase.equipos.Count);
                            cmd.Parameters.AddWithValue("@cantidadGrupos", fase.grupos.Count);
                        }
                        else {
                            cmd.Parameters.AddWithValue("@cantidadEquipos", DBNull.Value);
                            cmd.Parameters.AddWithValue("@cantidadGrupos", DBNull.Value);
                        }                                                   
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        if (fase.grupos.Count != 0 && !fase.esGenerica)
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
             List<Fase> fases = new List<Fase>();
             DAOGrupo daoGrupo= new DAOGrupo();
             DAOFecha daoFecha=new DAOFecha();
             DAOPartido daoPartido=new DAOPartido();
             try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();               
                 cmd.Connection = con;                 
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
                          tipoFixture = new TipoFixture(dr["tipoFixture"].ToString()),
                          equipos = new List<Equipo>(),
                          cantidadDeEquipos = (dr["cantidadEquipos"] != DBNull.Value) ? int.Parse(dr["cantidadEquipos"].ToString()) : 0,
                          cantidadDeGrupos = (dr["cantidadGrupos"] != DBNull.Value) ? int.Parse(dr["cantidadGrupos"].ToString()) : 0,
                          esGenerica = (dr["cantidadEquipos"] != DBNull.Value) // si el valor del campo cantidadEquipos es Null, entonecs se trata de una fase generica.
                      };        
                     fases.Add(fase);
                 }
                 if (dr != null)
                     dr.Close();

                 foreach (Fase fase in fases)
                 {
                     if (!fase.esGenerica)
                     {
                         daoGrupo.obtenerGrupos(fase, con, true);
                         daoFecha.obtenerFechas(fase, con);
                         daoPartido.obtenerPartidos(fase, con);
                         foreach (Grupo grupo in fase.grupos)
                         {
                             fase.equipos.AddRange(grupo.equipos);
                         }  
                     }
                    
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
        /// Obtiene las fases  de una edición por Id. Obteniendo solo los valores necesarios para mostrar la informacion básica de la fase y sus equipos.
        /// autor: Antonio Herrera
        /// </summary>
        public List<Fase> obtenerFasesReducidas(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;           
            List<Fase> fases = new List<Fase>();
            DAOGrupo daoGrupo = new DAOGrupo();
            DAOFecha daoFecha = new DAOFecha();
            DAOPartido daoPartido = new DAOPartido();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();           
                cmd.Connection = con;
               
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
                        tipoFixture = new TipoFixture(dr["tipoFixture"].ToString()),
                        equipos = new List<Equipo>(),
                        cantidadDeEquipos = (dr["cantidadEquipos"] != DBNull.Value) ? int.Parse(dr["cantidadEquipos"].ToString()) : 0,
                        cantidadDeGrupos = (dr["cantidadGrupos"] != DBNull.Value) ? int.Parse(dr["cantidadGrupos"].ToString()) : 0,
                        esGenerica = (dr["cantidadEquipos"] != DBNull.Value) // si el valor del campo cantidadEquipos no es Null, entonecs se trata de una fase generica.
                    };
                    fases.Add(fase);
                }
                if (dr != null)
                    dr.Close();

                foreach (Fase fase in fases)
                {
                    if (!fase.esGenerica)
                    {
                        daoGrupo.obtenerGrupos(fase, con, false);
                        foreach (Grupo grupo in fase.grupos)
                        {
                            fase.equipos.AddRange(grupo.equipos);
                        }  
                    }
                    
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
        /// Elimina las fases de una edicion y guarda las nuevas fases que se pasan por parametros.
        /// autor: Antonio Herrera
        /// </summary>
        /// <param name="fases">Lista de Fases</param>
        /// <param name="con">Conexión</param>
        /// <param name="trans">Transacción</param>
        public void actualizarFase(List<Fase> fases, SqlConnection con, SqlTransaction trans)
        {            
            try
            {
                eliminarFases(fases[0].idEdicion);
                registrarFase(fases, con, trans);
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar la Fase: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina las fases de la edicion que tiene el id que se pasa como parametro.
        /// autor: Antonio Herrera
        /// </summary>
        /// <param name="fases">Lista de Fases</param>
        public void eliminarFases(int idEdicion)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;          
             
                        string sqlEliminacion = "DELETE FROM Fases WHERE idEdicion = @idEdicion";
                        cmd.Parameters.Clear();                        
                        cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                        cmd.CommandText = sqlEliminacion;
                        cmd.ExecuteNonQuery();           
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("Error al intentar actualizar la edición: " + ex.Message);
            }
        }
        
         /// <summary>
       /// Cambia el estado de la fase a Cerrada cuando se jugaron todos los partidos y devuelve true si se cerró y false si aún no
       /// autor: Flor Rojas
       /// </summary>
//        public bool cerrarFase(int idPartido)
//        {
//            SqlConnection con = new SqlConnection(cadenaDeConexion);
//            SqlCommand cmd = new SqlCommand();
//            try
//            {
//                if (con.State == ConnectionState.Closed)
//                    con.Open();
//             cmd.Connection = con;
//             string sql = @"                            
//                            DECLARE @idFase AS int = (SELECT idFase FROM Partidos WHERE idPartido = @idPartido)
//                            DECLARE @idEdicion AS int = (SELECT idEdicion FROM Partidos WHERE idPartido = @idPartido)
//                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Partidos p WHERE  p.idEdicion = @idEdicion AND p.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 4 AND idEstado<>13  ))
//	                            if(@cantidad=0)
//	                             BEGIN
//	                             UPDATE Fases SET idEstado =6 WHERE idFase = @idFase AND idEdicion = @idEdicion
//                                 END";
//             cmd.Parameters.Clear();
//             cmd.Parameters.AddWithValue("@idPartido", idPartido);
//             cmd.Parameters.AddWithValue("@idEstado", Estado.faseCERRADA);
//             cmd.CommandText=sql;
//             bool b= (cmd.ExecuteNonQuery() > 0) ? true : false;
//             return b;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("No se pudo cerrar la fase: " + ex.Message);
//            }
//            finally
//            {
//                if (con != null && con.State == ConnectionState.Open)
//                    con.Close();
//            }
//        }

        /// <summary>
        /// Cambia el estado de la fase a Cerrada cuando se jugaron todos los partidos y devuelve true si se cerró y false si aún no
        /// autor: Flor Rojas
        /// </summary>
        public bool finalizoFase(int idFase,int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"         
                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Partidos p WHERE  p.idEdicion = @idEdicion AND p.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 4 AND idEstado<>13  ))
	                            if(@cantidad=0)
	                             BEGIN
	                             SELECT 1
                                 END";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idFase", idFase);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion); 
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                return dr.HasRows;
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

        public bool finalizoFase(int idPartido)
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
	                             SELECT 1
                                 END";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                return dr.HasRows;
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



         //<summary>
         //Cambia el estado de la fase a Cerrada cuando se jugaron todos los partidos 
         //autor: Flor Rojas
         //</summary>
                public void cerrarFase(int idFase,int idEdicion)
                {
                    SqlConnection con = new SqlConnection(cadenaDeConexion);
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                     if (con.State == ConnectionState.Closed)
                        con.Open();
                     cmd.Connection = con;
                     string sql = @"  UPDATE Fases SET idEstado =@idEstado WHERE idFase = @idFase AND idEdicion = @idEdicion";
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@idFase", idFase);
                     cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                     cmd.Parameters.AddWithValue("@idEstado", Estado.faseCERRADA);
                     cmd.CommandText=sql;
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


                /// <summary>
                /// Cambia el estado de la fase a Completa cuando se jugaron todos los partidos y devuelve true si se completó la fase
                /// autor: Flor Rojas
                /// </summary>
                public bool actualizarEstadoFase(int idPartido)
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
                            DECLARE @cantidad AS int = (SELECT COUNT(*) FROM Fechas f WHERE f.idGrupo = @idGrupo AND f.idFase=@idFase AND f.idEdicion = @idEdicion AND f.idEstado IN (SELECT idEstado FROM Estados WHERE idAmbito = 3 AND idEstado<>8  ))
					                            if(@cantidad=0)
						           BEGIN
							                UPDATE Fase SET idEstado = @idEstado WHERE idFase = @idFase AND idEdicion = @idEdicion
						           END";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idPartido", idPartido);
                        cmd.Parameters.AddWithValue("@idEstado", Estado.faseCERRADA);
                        cmd.CommandText = sql;
                        return (cmd.ExecuteNonQuery() > 0);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No se pudo actualizar el estado de la fase: " + ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == ConnectionState.Open)
                            con.Close();
                    }
                }

    }
}
