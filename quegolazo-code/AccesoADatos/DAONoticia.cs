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
        public int registrarNoticia(Noticia noticia, int idEdicion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Noticias (titulo, tipoNoticia, idEdicion, descripcion, fecha)
                                    VALUES (@titulo, @tipoNoticia, @idEdicion, @descripcion, @fecha)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@tipoNoticia", noticia.tipoNoticia);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@descripcion", DAOUtils.dbValueNull(noticia.descripcion));
                cmd.Parameters.AddWithValue("@fecha", noticia.fecha);   
                cmd.CommandText = sql;
                int idNoticia = int.Parse(cmd.ExecuteScalar().ToString());
                return idNoticia; //retorna el id de la noticia generado por la BD
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
        public DataTable obtenerNoticiasDeUnTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT n.idNoticia AS 'idNoticia', CONVERT (char(10), n.fecha, 103)  AS 'fecha', n.titulo AS 'titulo', n.tipoNoticia AS 'tipoNoticia', e.nombre AS 'nombre'
                                FROM Noticias n INNER JOIN Ediciones e ON n.idEdicion = e.idEdicion
                                WHERE e.idTorneo = @idTorneo
                                ORDER BY n.idNoticia DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                con.Close();
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los árbitros:" + ex.Message);
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
                    respuesta.tipoNoticia = dr["tipoNoticia"].ToString();
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
                                SET titulo = @titulo, tipoNoticia = @tipoNoticia, descripcion = @descripcion, fecha = @fecha
                                WHERE idNoticia = @idNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idNoticia", noticia.idNoticia);
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@tipoNoticia", noticia.tipoNoticia);
                cmd.Parameters.AddWithValue("@descripcion", DAOUtils.dbValueNull(noticia.descripcion));
                cmd.Parameters.AddWithValue("@fecha", noticia.fecha);               
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
