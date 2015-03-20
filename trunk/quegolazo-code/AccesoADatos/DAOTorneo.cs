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
    public class DAOTorneo
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Obtiene todos los Torneos de un Usuario
        /// autor: Pau Pedrosa
        /// </summary>
        /// <parameters>id de Usuario</parameters>
        /// <returns>Lista genérica de Torneos</returns>
        public List<Torneo> obtenerTorneosPorUsuario(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Torneo> respuesta = new List<Torneo>();
            Torneo torneo = null;            
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();                
                cmd.Connection = con;
                string sql = @"SELECT * 
                             FROM Torneos
                             WHERE idUsuario = @idUsuario
                             ORDER BY idTorneo DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();                                       
                while (dr.Read())
                {
                    torneo = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        descripcion = dr["descripcion"].ToString()                        
                    };
                    respuesta.Add(torneo);
                  }               
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Torneos del usuario: " + ex.Message);
            }
            finally
            {
               if(con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Busca un Torneo con un Id determinado en la base de datos.
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo"> Id del Torneo que se quiere buscar </param>
        /// <returns>Un objeto Torneo, o null si no encuentra el Torneo.</returns>
        public Torneo obtenerTorneoPorId(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            Torneo respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                  con.Open();
                cmd.Connection = con; 
                string sql = @"SELECT idTorneo, nombre, nick, idUsuario, descripcion
                                FROM Torneos
                                WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();             
                while (dr.Read())
                {
                    respuesta = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        descripcion = dr["descripcion"].ToString()  
                    };
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el Torneo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Busca un Torneo por un nombre y usuario determinado en la base de datos.
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo"> Id del Torneo que se quiere buscar </param>
        /// <returns>Un objeto Torneo, o null si no encuentra el Torneo.</returns>
        public Torneo obtenerTorneoPorIdYUsuario(int idTorneo, int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            Torneo respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();                
                cmd.Connection = con;
                string sql = @"SELECT idTorneo, nombre, nick, idUsuario
                                FROM Torneos
                                WHERE idUsuario = @idUsuario AND idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOUsuario daoUsuario = new DAOUsuario();

                while (dr.Read())
                {
                    respuesta = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        descripcion = dr["descripcion"].ToString()  
                    };
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el Torneo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Registra un torneo nuevo para un determinado usuario.
        /// </summary>
        /// <param name="torneoNuevo">El torneo que se va a registrar</param>
        /// <param name="idUsuario">El id del usuario al cual le pertenece el torneo</param>
        public int registrarTorneo(Torneo torneoNuevo, int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();                
                cmd.Connection = con;
                string sql = @"INSERT INTO Torneos (nombre, descripcion, nick, idUsuario)
                                              VALUES (@nombre, @descripcion, @nick, @idUsuario) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", torneoNuevo.nombre);
                cmd.Parameters.AddWithValue("@descripcion", torneoNuevo.descripcion);
                cmd.Parameters.AddWithValue("@nick", torneoNuevo.nick);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                //Si ya existe un torneo con ese nombre
                if(e.Message.Contains("unique_nombre"))
                    throw new Exception("No se pudo registrar el Torneo: Ya existe un Torneo registrado con este nombre, por favor cambielo e intente nuevamente.");
                //Si ya existe un torneo con ese nick
                if (e.Message.Contains("unique_nick"))
                    throw new Exception("No se pudo registrar el Torneo: Ya existe un Torneo registrado con esta URL, por favor cambiela e intente nuevamente.");
                throw new Exception("No se pudo registrar el Torneo: " + e.Message);
            }
            finally 
            {
                if (con.State == ConnectionState.Open)
                    con.Close();                  
            }
        }

        /// <summary>
        /// Registra un torneo nuevo para un determinado usuario.
        /// Autor: Antonio Herrera
        /// </summary>
        /// <param name="torneoNuevo">El torneo que se modifica con sus nuevos datos</param>
        /// <param name="usuario">El usuario al cual le pertenece el torneo</param>
        public void modificarTorneo(Torneo torneoNuevo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();                
                cmd.Connection = con;
                string sql = @"UPDATE Torneos 
                                     SET descripcion = @descripcion, nombre = @nombre 
                                     WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", torneoNuevo.nombre);
                cmd.Parameters.AddWithValue("@descripcion", torneoNuevo.descripcion);
                cmd.Parameters.AddWithValue("@idTorneo", torneoNuevo.idTorneo);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Si ya existe un torneo con ese nombre
                if (e.Message.Contains("unique_nombre"))
                    throw new Exception("No se pudo modificar el Torneo: ya existe un Torneo registrado con este nombre, por favor cambielo e intente nuevamente.");              
                throw new Exception("No se pudo registrar el Torneo: " + e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }        

        /// <summary>
        /// Permite eliminar de la BD un Torneo (sin ediciones asociadas)
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo a eliminar</param>
        public void eliminarTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Torneos
                                WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {// si tiene ediciones asociadas
                if (ex.Message.Contains("Ediciones"))
                    throw new Exception("No se puede eliminar ese Torneo: TIENE EDICIONES ASOCIADAS");
                if (ex.Message.Contains("Arbitros"))
                    throw new Exception("No se puede eliminar ese Torneo: TIENE ÁRBITROS ASOCIADOS");
                if (ex.Message.Contains("Canchas"))
                    throw new Exception("No se puede eliminar ese Torneo: TIENE CANCHAS ASOCIADAS");
                if (ex.Message.Contains("Equipos"))
                    throw new Exception("No se puede eliminar ese Torneo: TIENE EQUIPOS ASOCIADOS");
                throw new Exception("No se pudo eliminar el Torneo");
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene el último torneo del usuario por Id de usuario
        /// autor: Flor Rojas
        /// </summary>
        public Torneo obtenerUltimoTorneoDelUsuario(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Torneo torneo = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT TOP 1 * 
                             FROM Torneos
                             WHERE idUsuario = @idUsuario
                             ORDER BY idTorneo DESC"; 
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    torneo = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        descripcion = dr["descripcion"].ToString()
                    };
                }
                return torneo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Torneo del usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Registra la configuracion visual generada por un usuario para un torneo determinado, analiza si existe una configuracion guardada, si no es asi, crea una nueva.
        /// </summary>
        public void registrarConfiguracionVisual(Torneo torneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"declare @configuracion int = (select COUNT(c.idTorneo) from ConfiguracionesVisuales c WHERE idTorneo = @idTorneo);
                                IF @configuracion > 0 
                                    BEGIN
                                        UPDATE ConfiguracionesVisuales 
	                                    SET
	                                    colorDeFondo = @colorDeFondo,
	                                    patronDeFondo = @patronDeFondo,
	                                    estiloPagina = @estiloPagina,
	                                    colorDestacado = @colorDestacado
	                                    WHERE idTorneo = @idTorneo
                                    END
                                    ELSE
                                    BEGIN
                                       INSERT INTO ConfiguracionesVisuales (colorDeFondo, patronDeFondo, estiloPagina,colorDestacado)
		                                      VALUES (@colorDeFondo, @patronDeFondo, @estiloPagina, @colorDestacado)
                                    END";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", torneo.idTorneo);
                cmd.Parameters.AddWithValue("@colorDeFondo", torneo.configuracionVisual.colorDeFondo);
                cmd.Parameters.AddWithValue("@patronDeFondo", torneo.configuracionVisual.patronDeFondo);
                cmd.Parameters.AddWithValue("@estiloPagina", torneo.configuracionVisual.estiloPagina);
                cmd.Parameters.AddWithValue("@colorDestacado", torneo.configuracionVisual.colorDestacado);                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
               throw new Exception("OCURRIÓ UN ERROR, INTENTA MAS TARDE");              
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene la configuracion visual registrada para un torneo
        /// 
        /// </summary>
        public ConfiguracionVisual obtenerConfiguracionVisual(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            ConfiguracionVisual respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT  * 
                             FROM ConfiguracionesVisuales
                             WHERE idTorneo = @idTorneo";                            
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new ConfiguracionVisual()
                    {
                        colorDeFondo = dr["colorDeFondo"].ToString(),
                        colorHeader = dr["colorHeader"].ToString(),
                        patronDeFondo = dr["patronDeFondo"].ToString(),
                        patronHeader = dr["patronHeader"].ToString(),
                        colorDestacado = dr["colorDestacado"].ToString(),
                        estiloPagina = dr["estiloPagina"].ToString(),
                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Torneo del usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
