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
        /// Registrar Delegado de un Equipo
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nuevoDelegado">Nuevo delegado a registrar</param>
        /// <returns>Id del delegado registrado</returns>
        public int registrarDelegado(Delegado nuevoDelegado)
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

                string sql = @"INSERT INTO Delegados (nombre, email, telefono, domicilio)
                                              VALUES (@nombre, @email, @telefono, @domicilio) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nuevoDelegado.nombre);
                cmd.Parameters.AddWithValue("@email", nuevoDelegado.email);
                cmd.Parameters.AddWithValue("@telefono", nuevoDelegado.telefono);

                if (nuevoDelegado.domicilio == null)
                    cmd.Parameters.AddWithValue("@domicilio", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@domicilio", nuevoDelegado.domicilio);


                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo registrar el delegado: " + e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    cmd.Connection = con;
                }
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
            SqlDataReader dr = new SqlDataReader();

            Delegado respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM Delegados
                                WHERE idDelegado = @idDelegado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDelegado", idDelegado);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    respuesta = new Delegado();
                    respuesta.idDelegado = Int32.Parse(dr["idDelegado"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.email = dr["email"].ToString();
                    respuesta.telefono = dr["telefono"].ToString();

                    if (dr["domicilio"] != System.DBNull.Value)
                        respuesta.domicilio = dr["domicilio"].ToString();
                    else
                        respuesta.domicilio = null;
                    
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el delegado: " + ex.Message);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }


                       
    }
}
