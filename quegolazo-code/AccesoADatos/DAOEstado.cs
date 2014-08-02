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
    public class DAOEstado
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Obtiene un estado de la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nombre">El nombre del estado</param>
        /// <param name="ambito">El ambito del estado</param>
        /// <returns>Un objeto de tipo Estado</returns>
        public Estado obtenerEstadoPorNombreYAmbito(Estado.enumNombre nombre, Estado.enumAmbito ambito)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;

                string sql = @"SELECT idEstado, nombre, ambito 
                             FROM Estados
                             WHERE nombre = @nombre and ambito = @ambito";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nombre.ToString());
                cmd.Parameters.AddWithValue("@ambito", ambito.ToString());
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                Estado respuesta = null;
                while (dr.Read())
                {
                    Estado nuevoEstado = new Estado()
                    {
                        idEstado = Int32.Parse(dr["idEstado"].ToString()),
                        ambito = obtenerAmbito(dr["ambito"].ToString()),
                        nombre = obtenerNombre(dr["nombre"].ToString())
                    };
                    respuesta = nuevoEstado;
                }
                dr.Close();                
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene un estado de la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idEstado">id del Estado</param>
        /// <returns>Un objeto de tipo Estado</returns>
        public Estado obtenerEstadoPorId(int idEstado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;

                string sql = @"SELECT idEstado, nombre, ambito 
                             FROM Estados
                             WHERE idEstado = @idEstado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                Estado respuesta = null;
                while (dr.Read())
                {
                    Estado nuevoEstado = new Estado()
                    {
                        idEstado = Int32.Parse(dr["idEstado"].ToString()),
                        ambito = obtenerAmbito(dr["ambito"].ToString()),
                        nombre = obtenerNombre(dr["nombre"].ToString())
                    };
                    respuesta = nuevoEstado;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene un estado de la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idEstado">id del Estado</param>
        /// <returns>Un objeto de tipo Estado</returns>
        public Estado obtenerEstadoPorNombre(string nombre, string ambito)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;

                string sql = @"SELECT idEstado, nombre, ambito 
                             FROM Estados
                             WHERE nombre = @nombre AND ambito = @ambito";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ambito", ambito);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                Estado respuesta = null;
                while (dr.Read())
                {
                    Estado nuevoEstado = new Estado()
                    {
                        idEstado = Int32.Parse(dr["idEstado"].ToString()),
                        ambito = obtenerAmbito(dr["ambito"].ToString()),
                        nombre = obtenerNombre(dr["nombre"].ToString())
                    };
                    respuesta = nuevoEstado;
                }

                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }



        /// <summary>
        /// Devuelve el enumerado correspondiente al ambito del estado
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="ambito">El ambito del estado guardado en la base de datos</param>
        /// <returns>un objeto tipo Enum con el nombre del ambito</returns>
        private Entidades.Estado.enumAmbito obtenerAmbito(string ambito)
        {
            //seteamos la respuesta con cualquier valor
            Estado.enumAmbito respuesta = Estado.enumAmbito.TORNEO;
            foreach (Estado.enumAmbito nombreDelEnumerado in Enum.GetValues(typeof(Estado.enumAmbito)))
            {
                if (ambito.Equals(nombreDelEnumerado.ToString()))
                {
                    //seteamos la respuesta con el valor que corresponde
                    respuesta = nombreDelEnumerado;
                    break;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Devuelve el enumerado correspondiente al nombre del estado
        /// </summary>
        /// <param name="nombre">El ambito del estado guardado en la base de datos</param>
        /// <returns>un objeto tipo Estado.enumNombre con el nombre del estado</returns>
        private Entidades.Estado.enumNombre obtenerNombre(string nombre)
        {
            //seteamos la respuesta con cualquier valor
            Estado.enumNombre respuesta = Estado.enumNombre.REGISTRADO;
            foreach (Estado.enumNombre nombreDelEnumerado in Enum.GetValues(typeof(Estado.enumNombre)))
            {
                if (nombre.Equals(nombreDelEnumerado.ToString()))
                {
                    //seteamos la respuesta con el valor que corresponde
                    respuesta = nombreDelEnumerado;
                    break;
                }
            }
            return respuesta;
        }
       

    }

}
