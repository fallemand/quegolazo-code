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
    public class DAONoticia
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registra una Nueva Noticia en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarNoticia(Noticia noticia, int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Noticias (titulo,  idEdicion, descripcion, fecha, tipoNoticia)
                                    VALUES (@titulo, @idEdicion, @descripcion, getDate(), 1)
                                    SELECT SCOPE_IDENTITY()";//el 1 esta hardcodeado, no se alarmen, es porque actualmente no usamos este campo, pero tal vez en el futuro si, y la bd no permite nullos, y generar una nueva versión para q admita nullos, no valia la pena
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@descripcion", DAOUtils.dbValueNull(noticia.descripcion));
                cmd.CommandText = sql;
                noticia.idNoticia = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la noticia: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtener noticias de un torneo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public DataTable obtenerNoticias(int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT idNoticia, CONVERT (char(10), fecha, 103)  AS 'fecha', titulo, descripcion
                                FROM Noticias n
                                WHERE idEdicion = @idEdicion
                                ORDER BY fecha DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                con.Close();
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las noticias:" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene una Noticia por Id de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public Noticia obtenerNoticiaPorId(int idNoticia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Noticia respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Noticias
                                WHERE idNoticia = @idNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idNoticia", idNoticia);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Noticia();
                    respuesta.idNoticia = Int32.Parse(dr["idNoticia"].ToString());
                    respuesta.titulo = dr["titulo"].ToString();
                    respuesta.idEdicion = Int32.Parse(dr["idEdicion"].ToString());
                    respuesta.fecha = DateTime.Parse(dr["fecha"].ToString());
                    respuesta.descripcion = (dr["descripcion"] != System.DBNull.Value) ? dr["descripcion"].ToString() : null;
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar la Noticia: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarNoticia(Noticia noticia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Noticias
                                SET titulo = @titulo, descripcion = @descripcion
                                WHERE idNoticia = @idNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idNoticia", noticia.idNoticia);
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@descripcion", DAOUtils.dbValueNull(noticia.descripcion));               
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo modificar la noticia: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarNoticia(int idNoticia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Noticias
                                WHERE idNoticia = @idNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idNoticia", idNoticia);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la Noticia: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
