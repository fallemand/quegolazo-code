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
    public class DAODelegado
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Delegado de un Equipo, es parte de una transaccion al registrar un equipo.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="delegado">Nuevo delegado a registrar</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public int registrarDelegado(Delegado delegado, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Delegados (nombre, email, telefono, domicilio)
                                              VALUES (@nombre, @email, @telefono, @domicilio) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", delegado.nombre);
                cmd.Parameters.AddWithValue("@email", delegado.email);
                cmd.Parameters.AddWithValue("@telefono", delegado.telefono);
                cmd.Parameters.AddWithValue("@domicilio", delegado.domicilio);
                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el delegado: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un Delegado por id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idDelegado">Id del delegado a obtener</param>
        /// <returns>Objeto delegado</returns>
        public Delegado obtenerDelegadoPorId(int idDelegado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            Delegado respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Delegados
                                WHERE idDelegado = @idDelegado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDelegado", idDelegado);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Delegado();
                    respuesta.idDelegado = Int32.Parse(dr["idDelegado"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.email = dr["email"].ToString();
                    respuesta.telefono = dr["telefono"].ToString();
                    respuesta.domicilio = dr["domicilio"].ToString();                  
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el delegado: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica en la BD el delegado
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="delegado">Objeto Delegado con sus nuevos dato</param>
        /// <param name="con">Conexion</param>
        /// <param name="trans">Transaccion</param>
        public void modificarDelegado(Delegado delegado, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"UPDATE Delegados 
                                     SET nombre = @nombre, email = @email, telefono = @telefono, domicilio = @domicilio
                                     WHERE idDelegado = @idDelegado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", delegado.nombre);
                cmd.Parameters.AddWithValue("@email", delegado.email);
                cmd.Parameters.AddWithValue("@telefono", delegado.telefono);
                cmd.Parameters.AddWithValue("@domicilio", delegado.domicilio);
                cmd.Parameters.AddWithValue("@idDelegado", delegado.idDelegado);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el delegado: " + ex.Message);
            }                                  
        }
        
        /// <summary>
        /// Elimina los delegados de un Equipo. 
        /// Actualiza la tabla Equipos: Le setea null a las claves foráneas (idDelegadoPrincipal y idDelegadoOpcional)
        /// Elimina los delegados de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="equipo">Equipo con los delegados a eliminar</param>
        public void eliminarDelegadosPorEquipo(Equipo equipo)
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
                string sql = @"UPDATE Equipos 
                                      SET idDelegadoPrincipal = @idDelegadoPrincipal, idDelegadoOpcional = @idDelegadoOpcional
                                      WHERE idEquipo = @idEquipo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDelegadoPrincipal", DBNull.Value);
                cmd.Parameters.AddWithValue("@idDelegadoOpcional", DBNull.Value);
                cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                eliminarDelegado(equipo.delegadoPrincipal, con, trans); // elimina delegado principal de la BD
                if (equipo.delegadoOpcional != null)
                    eliminarDelegado(equipo.delegadoOpcional, con, trans); // elimina delegado opcional de la BD
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
       
        /// <summary>
        /// Elimina un delegado de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="delegado">Delegado a eliminar</param>
        /// <param name="con">Objeto Conexión</param>
        /// <param name="trans">Objeto Transacción</param>
        public void eliminarDelegado(Delegado delegado, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"DELETE FROM Delegados 
                                     WHERE idDelegado = @idDelegado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDelegado", delegado.idDelegado);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el delegado: " + ex.Message);
            }
        }
    }
}
