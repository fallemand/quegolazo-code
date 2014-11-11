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
    public class DAOTipoSuperficie
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Ontiene un TipoSuperficie por su id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTipoSuperficie">id del Tipo de Superficie</param>
        /// <returns>Un Objeto TipoSuperficie o null sino lo encuentra</returns>
        public TipoSuperficie obtenerTipoSuperficiePorId(int idTipoSuperficie)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            TipoSuperficie respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TiposSuperficie
                                WHERE idTipoSuperficie = @idTipoSuperficie";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTipoSuperficie", idTipoSuperficie);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new TipoSuperficie()
                    {
                        idTipoSuperficie = Int32.Parse(dr["idTipoSuperficie"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Tipo de Superficie: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Ontiene todos los Tipos de Superficie
        /// autor: Pau Pedrosa
        /// </summary>
        /// <returns>Una lista de Objeto TipoSuperficie o null sino lo encuentra</returns>
        public List<TipoSuperficie> obtenerTodos()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<TipoSuperficie> respuesta = new List<TipoSuperficie>(); 
            TipoSuperficie tipoSuperficie = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM TiposSuperficie"; 
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tipoSuperficie = new TipoSuperficie()
                    {
                        idTipoSuperficie = Int32.Parse(dr["idTipoSuperficie"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    respuesta.Add(tipoSuperficie);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los Tipos de Superficie: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
