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
                con.Close();
            }
        }



/// <summary>
/// Metodo para activar cuenta
/// autor: Flor
/// </summary>
/// <param name="IdUsuario"></param>
        public void ActivarCuenta(int IdUsuario)
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

                if (IdUsuario != null)
                {   
                    string sql = "UPDATE Usuarios SET EsActivo=1 WHERE IdUsuario=@UserID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserID", IdUsuario);
                    cmd.CommandText = sql;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo activar su usuario. Comuníquese con nuestro soporte técnico.");
            }
            finally
            {
                con.Close();

            }
            
        }

        /// <summary>
        /// Busca un Usuario con un Id determinado en la base de datos.
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
                    usuario.telefono = dr["telefono"].ToString();
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
                con.Close();
            }

        }



    }
}
