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
        /// autor: Paula Pedrosa
        /// </summary>
        /// <parameters>id de Usuario</parameters>
        /// <returns>Lista genérica de Torneos</returns>
        public List<Torneo> obtenerTorneosDeUnUsuario(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Torneo> torneos = new List<Torneo>();
            Torneo respuesta = null;
            
            bool b = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }
                string sql = @"SELECT * 
                             FROM Torneos
                             WHERE idUsuario = @idUsuario
                             ORDER BY idTorneo DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    b = true;
                    throw new Exception("No hay torneos registrados de ese usuario");
                    
                }

                DAOUsuario gestorUsuario = new DAOUsuario();                
                while (dr.Read())
                {
                    respuesta = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        usuario = gestorUsuario.obtenerUsuarioPorId(Int32.Parse(dr["idUsuario"].ToString()))

                    };
                    torneos.Add(respuesta);
                    
                }

               
                return torneos;
            }
            catch (Exception ex)
            {
                if(!b)
                   throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            finally
            {
               if(con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Busca un Torneo con un Id determinado en la base de datos.
        /// autor: Paula Pedrosa
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
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT idTorneo, nombre, nick, idUsuario
                                FROM Torneos
                                WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
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
                        usuario = daoUsuario.obtenerUsuarioPorId(Int32.Parse(dr["idUsuario"].ToString()))
                   
                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el torneo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Busca un Torneo por un nombre y usuario determinado en la base de datos.
        /// autor: Paula Pedrosa
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
                {
                    con.Open();
                    cmd.Connection = con;
                }

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
                        usuario = daoUsuario.obtenerUsuarioPorId(Int32.Parse(dr["idUsuario"].ToString()))

                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el torneo: " + ex.Message);
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
        /// <param name="usuario">El usuario al cual le pertenece el torneo</param>
        public int registrarTorneo(Torneo torneoNuevo, Usuario usuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"INSERT INTO Torneos (nombre, descripcion, nick, idUsuario)
                                              VALUES (@nombre, @descripcion, @nick, @idUsuario) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", torneoNuevo.nombre);
                cmd.Parameters.AddWithValue("@descripcion", torneoNuevo.descripcion);
                cmd.Parameters.AddWithValue("@nick", torneoNuevo.nick);
                cmd.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception e)
            {
                //Si ya existe un torneo con ese nombre
                if(e.Message.Contains("unique_nombre"))
                {                    
                    throw new Exception("No se pudo registrar el torneo: Ya existe un torneo registrado con este nombre, por favor cambielo e intente nuevamente.");
                }
                //Si ya existe un torneo con ese nick
                if (e.Message.Contains("unique_nick"))
                {
                    throw new Exception("No se pudo registrar el torneo: Ya existe un torneo registrado con esta URL, por favor cambiela e intente nuevamente.");
                }
                throw new Exception("No se pudo registrar el torneo: " + e.Message);
            }
            finally 
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                  
                }
            }
        }

    }
}
