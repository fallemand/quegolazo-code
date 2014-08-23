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
    public class DAOUsuario
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Registra un nuevo usuario en la BD
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="usuarioNuevo">Objeto usuario</param>
        public void registrarUsuario(Usuario usuarioNuevo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)                
                   con.Open();
                cmd.Connection = con;
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
            catch (SqlException ex)
            {// Excepción de BD, clave unique
                if (ex.Class == 14 && ex.Number == 2627)
                    throw new Exception("El usuario con el mail: " + usuarioNuevo.email + " Ya se encuentra registrado. Por favor ingrese una cuenta de correo diferente.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        /// <summary>
        /// Busca un Usuario con por un email determinado en la base de datos.
        /// autor: Pau Pedrosa y Flor Rojas
        /// </summary>
        /// <param name="idUsuario">Email del Usuario que se quiere buscar </param>
        /// <returns>Un objeto Usuario, o null si no encuentra el Usuario.</returns>
        public Usuario obtenerUsuarioPorEmail(string email)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Usuario respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();                
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Usuarios
                                WHERE email = @email";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();
                while (dr.Read())
                {
                    respuesta = new Usuario(){
                    idUsuario = Int32.Parse(dr["idUsuario"].ToString()),
                    nombre = dr["nombre"].ToString(),
                    apellido = dr["apellido"].ToString(),
                    email = dr["email"].ToString(),
                    codigo = dr["codigo"].ToString(),
                    esActivo = bool.Parse(dr["esActivo"].ToString()),
                    tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString()))};
                }
                if (dr != null)
                    dr.Close();
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
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="codigo"></param>
        /// Retorna el Id del usuario activado
        public void ActivarCuenta(string codigo)
        {           
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                if (codigo != string.Empty)
                {
                    cmd.Connection = con;
                    string sql = @"UPDATE Usuarios SET EsActivo=1, codigo = @codigoVacio 
                                    WHERE codigo = @userCodigo";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userCodigo", codigo);
                    cmd.Parameters.AddWithValue("@codigoVacio", DBNull.Value);
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("El link de Activación no es válido o ya fue utilizado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un problema y no se ha podido activar tu cuenta: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)                
                    con.Close();               
            }            
        }
        /// <summary>
        /// Busca un Usuario con un Id determinado en la base de datos.
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idUsuario"> Id del Usuario que se quiere buscar </param>
        /// <returns>Un objeto Usuario, o null si no encuentra el Usuario.</returns>
        public Usuario obtenerUsuarioPorId(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Usuario respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();                
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Usuarios
                                WHERE idUsuario = @idUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();
                while (dr.Read())
                {
                    respuesta = new Usuario();
                    respuesta.idUsuario = Int32.Parse(dr["idUsuario"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.apellido = dr["apellido"].ToString();
                    respuesta.email = dr["email"].ToString();
                    respuesta.contrasenia = dr["contrasenia"].ToString();
                    respuesta.esActivo = bool.Parse(dr["esActivo"].ToString());                   
                    respuesta.codigo = dr["codigo"].ToString();
                    respuesta.tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString()));
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
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
        /// Obtiene el usuario con ese mail y contrasenia
        /// </summary>
        /// <parameters>Email y contrasenia</parameters>
        /// <returns>Usuario</returns>
        public Usuario obtenerUsuarioPorEmailyContrasenia(string email, string contrasenia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Usuario respuesta = null;
            try
            {
                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM Usuarios
                                WHERE email = @email AND contrasenia = @contrasenia";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@contrasenia", contrasenia));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Usuario()
                    {
                        nombre = dr["nombre"].ToString(),
                        apellido = dr["apellido"].ToString(),
                        email = dr["email"].ToString(),
                        idUsuario = dr.GetInt32(dr.GetOrdinal("idUsuario")),
                        tipoUsuario = gestorTipoUsuario.obtenerTipoUsuarioPorId(Int32.Parse(dr["idTipoUsuario"].ToString())),
                        esActivo = bool.Parse(dr["esActivo"].ToString())
                    };
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
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
        /// <summary>
        /// Metodo para registrar el codigo de recuperacion de contraseña
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="usuario">Objeto Usuario</param>
        public void registrarCodigoRecuperacion(Usuario usuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)                
                    con.Open();  
                cmd.Connection = con;
                string sql = @"UPDATE Usuarios SET codigoRecuperacion = @codigo 
                                WHERE idUsuario = @idUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigo", usuario.codigoRecuperacion);
                cmd.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() == 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un problema y no se ha podido completar la operación");
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)                
                    con.Close();                
            }
        }
        /// <summary>
        /// Metodo para grabar nueva clave, borrar el codigo. Devuelve el Id del usuario afectado
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="codigo">codigo univoco crear el link para restrablecer contraseña</param>
        /// <param name="contrasenia">nueva contraseña del usuario</param>
        /// <returns></returns>
        public int restablecerContrasenia(string codigo, string contrasenia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                int idUsuario = 0;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                if (codigo != string.Empty)
                {                    
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    string sql = @"UPDATE Usuarios SET contrasenia = @clave, codigoRecuperacion = @codigoVacio 
                                    WHERE codigoRecuperacion = @userCodigo";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@clave", contrasenia);
                    cmd.Parameters.AddWithValue("@userCodigo", codigo);
                    cmd.Parameters.AddWithValue("@codigoVacio", DBNull.Value);
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("El link para reestablecer la clave no es válido o ya fue utilizado");
                }
                return idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un problema y no se ha podido activar tu cuenta: " + ex.Message);
            }
             finally
            {
                if (con != null && con.State == ConnectionState.Open)                
                    con.Close();               
            }
        }

        /// <summary>
        /// Modifica el usuario en la BD
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="usuarioNuevo">Objeto usuario</param>
        public void modificarUsuario(Usuario usuarioModificado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Usuarios 
                                SET nombre=@nombre, apellido=@apellido, email=@email, contrasenia=@contrasenia, codigo=@codigo, esActivo=@esActivo
                                WHERE idUsuario=@idUsuario";
     
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idUsuario", usuarioModificado.idUsuario));
                cmd.Parameters.Add(new SqlParameter("@nombre", usuarioModificado.nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", usuarioModificado.apellido));
                cmd.Parameters.Add(new SqlParameter("@email", usuarioModificado.email));
                if(usuarioModificado.codigo != string.Empty)
                cmd.Parameters.Add(new SqlParameter("@codigo", usuarioModificado.codigo));
                else
                    cmd.Parameters.Add(new SqlParameter("@codigo", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@esActivo", usuarioModificado.esActivo));
                cmd.Parameters.Add(new SqlParameter("@contrasenia", usuarioModificado.contrasenia));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {// Excepción de BD, clave unique
                if (ex.Class == 14 && ex.Number == 2627)
                    throw new Exception("El usuario con el mail: " + usuarioModificado.email + " Ya se encuentra registrado. Por favor ingrese una cuenta de correo diferente.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo modificar el usuario: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        /// <summary>
        /// Validar contrasenia para corroborar identidad del usuario
        /// </summary>
        /// <parameters>contrasenia</parameters>
        /// <returns>Usuario</returns>
        public void validarUsuario(string idUsuario, string contrasenia)
        {
//            SqlConnection con = new SqlConnection(cadenaDeConexion);
//            SqlCommand cmd = new SqlCommand();
//            SqlDataReader dr;
//            Usuario respuesta = null;
//            try
//            {
//                DAOTipoUsuario gestorTipoUsuario = new DAOTipoUsuario();
//                if (con.State == ConnectionState.Closed)
//                    con.Open();
//                cmd.Connection = con;
//                string sql = @"SELECT * 
//                                FROM Usuarios
//                                WHERE idUsuario = @idUsuario AND contrasenia = @contrasenia";
//                cmd.Parameters.Clear();
//                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
//                cmd.Parameters.Add(new SqlParameter("@contrasenia", contrasenia));
//                cmd.CommandText = sql;
//                //ifcmd.ExecuteReader();
//                while (dr.Read())
//                {
           
//                }
               
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Ocurrio un error al obtener el usuario" + ex.Message);
//            }
//            finally
//            {
//                if (con != null && con.State == ConnectionState.Open)
//                    con.Close();
//            }
        }
    }
}

