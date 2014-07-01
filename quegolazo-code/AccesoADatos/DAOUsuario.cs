using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace AccesoADatos
{
  public  class DAOUsuario
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        public DAOUsuario()
        {

        }



      /// <summary>
      /// autor=Flor
      /// registra un nuevo usuario en la BD
      /// </summary>
      /// <param name="usuarioNuevo"></param>
        public void registrarUsuario(Usuario usuarioNuevo)
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

                string sql = @"INSERT INTO Usuarios (nombre, apellido, email, contrasenia, codigo, idTipoUsuario, esActivo)
                              VALUES (@nombre, @apellido, @email, @contrasenia, @codigo, @idTipoUsuario, 0)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@nombre", usuarioNuevo.nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", usuarioNuevo.apellido));
                cmd.Parameters.Add(new SqlParameter("@email", usuarioNuevo.email));
                cmd.Parameters.Add(new SqlParameter("@contrasenia", usuarioNuevo.contrasenia));
                cmd.Parameters.Add(new SqlParameter("@codigo", usuarioNuevo.codigo));
                cmd.Parameters.Add(new SqlParameter("@idTipoUsuario", usuarioNuevo.tipoUsuario.idTipoUsuario));

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el Usuario: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        /// <summary>
        /// Busca un Usuario con por un email determinado en la base de datos.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idUsuario"> Email del Usuario que se quiere buscar </param>
        /// <returns>Un objeto Usuario, o null si no encuentra el Usuario.</returns>
        public Usuario buscarUsuarioPorEmail(string email)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            Usuario respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM Usuarios
                                WHERE email = @email";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();

                while (dr.Read())
                {
                    respuesta = new Usuario();
                    respuesta.idUsuario = Int32.Parse(dr["idUsuario"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.apellido = dr["apellido"].ToString();
                    respuesta.email = dr["email"].ToString();

                    respuesta.contrasenia = dr["contrasenia"].ToString();

                    if (dr["esActivo"].ToString().Equals("1"))
                        respuesta.esActivo = true;
                    else
                        respuesta.esActivo = false;

                    respuesta.codigo = dr["codigo"].ToString();
                    respuesta.tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString()));

                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el campeonato: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }



/// <summary>
/// Metodo para activar cuenta
/// autor: Flor
/// </summary>
/// <param name="codigo"></param>
/// Retorna el id del usuario activado
        public int ActivarCuenta(string codigo)
        {
           
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;

            try
            {
                int idUsuario=0;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                if (codigo != string.Empty)
                {
                  
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;

                    //Activación de Usuario
                    string sql = @"UPDATE Usuarios SET EsActivo=1 
                                    WHERE codigo=@UserCodigo";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserCodigo", codigo);
                    cmd.CommandText = sql;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();


                    //Obtener id de usuario
                     sql = @"SELECT idUsuario
                                FROM Usuarios
                                WHERE codigo=@UserCodigo";
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@UserCodigo", codigo);
                     cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                     while (dr.Read())
                     {
                         idUsuario= Int32.Parse(dr["idUsuario"].ToString());
                     }
                     dr.Close();

                    //Borrar código de activación
                    sql = @"UPDATE Usuarios SET codigo=@codigo
                            WHERE idUsuario=@idUsuario";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@codigo", DBNull.Value);
                    cmd.Parameters.AddWithValue("@idUsuario",idUsuario);
                    cmd.CommandText = sql;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
                return idUsuario;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Ha ocurrido un problema y no se ha podido activar tu cuenta. Comunícate con nuestro soporte técnico.");
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                  
                }

            }
            
        }

        /// <summary>
        /// Busca un Usuario con un Id determinado en la base de datos.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idUsuario"> Id del Usuario que se quiere buscar </param>
        /// <returns>Un objeto Usuario, o null si no encuentra el Usuario.</returns>
        public Usuario obtenerUsuarioPorId(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            Usuario usuario = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM Usuarios
                                WHERE idUsuario = @idUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();

                while (dr.Read())
                {
                    usuario = new Usuario();
                    usuario.idUsuario = Int32.Parse(dr["idUsuario"].ToString());
                    usuario.nombre = dr["nombre"].ToString();
                    usuario.apellido = dr["apellido"].ToString();
                    usuario.email = dr["email"].ToString();
                    usuario.contrasenia = dr["contrasenia"].ToString();

                    if (dr["esActivo"].ToString().Equals("0"))
                        usuario.esActivo = false;
                    else
                        usuario.esActivo = true;

                    usuario.codigo = dr["codigo"].ToString();
                    usuario.tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString()));


                }
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

        }



        /// <summary>
        /// Obtiene el usuario con ese mail y contraseña
        /// </summary>
        /// <parameters>email y contrasenia</parameters>
        /// <returns>Usuario</returns>
        public Usuario obtenerUsuarioPorEmailyContrasenia(string email, string contrasenia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Usuario usuario = null;
            try
            {
                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }
                string sql = @"SELECT * 
                             FROM Usuarios
                             WHERE email = @email AND contrasenia=@contrasenia";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@contrasenia", contrasenia));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    usuario = new Usuario()
                    {
                        nombre = dr["nombre"].ToString(),
                        apellido = dr["apellido"].ToString(),
                        email = dr["email"].ToString(),
                        idUsuario = dr.GetInt32(dr.GetOrdinal("idUsuario")),
                        tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString())),
                        esActivo = bool.Parse(dr["esActivo"].ToString())
                    };
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el usuario" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
