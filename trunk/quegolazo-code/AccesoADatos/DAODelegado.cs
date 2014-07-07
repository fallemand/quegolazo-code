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
    }
}
