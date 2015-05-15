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
                string sql = @"INSERT INTO Noticias (titulo, idEdicion, descripcion, fecha, idCategoriaNoticia)
                                    VALUES (@titulo, @idEdicion, @descripcion, getDate(), @idCategoriaNoticia)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@idCategoriaNoticia", noticia.categoria.idCategoriaNoticia);
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
                string sql = @"SELECT n.idNoticia, CONVERT (char(10), n.fecha, 103)  AS 'fecha', n.titulo AS 'titulo', n.descripcion, c.nombre AS 'nombre'
                                FROM Noticias n INNER JOIN CategoriasNoticia c ON n.idCategoriaNoticia = c.idCategoriaNoticia
                                WHERE n.idEdicion = @idEdicion
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
                    respuesta.categoria = obtenerCategoriaNoticiaPorId(Int32.Parse(dr["idCategoriaNoticia"].ToString()));
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
                                SET titulo = @titulo, descripcion = @descripcion, idCategoriaNoticia = @idCategoriaNoticia
                                WHERE idNoticia = @idNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idNoticia", noticia.idNoticia);
                cmd.Parameters.AddWithValue("@titulo", noticia.titulo);
                cmd.Parameters.AddWithValue("@descripcion", DAOUtils.dbValueNull(noticia.descripcion));
                cmd.Parameters.AddWithValue("@idCategoriaNoticia", noticia.categoria.idCategoriaNoticia);
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

        public List<CategoriaNoticia> obtenerCategoriasNoticia()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<CategoriaNoticia> respuesta = new List<CategoriaNoticia>();
            CategoriaNoticia categoriaNoticia = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM CategoriasNoticia";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    categoriaNoticia = new CategoriaNoticia()
                    {
                        idCategoriaNoticia = Int32.Parse(dr["idCategoriaNoticia"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    respuesta.Add(categoriaNoticia);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar las Categorías de Noticias: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public CategoriaNoticia obtenerCategoriaNoticiaPorId(int idCategoriaNoticia)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            CategoriaNoticia respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM CategoriasNoticia
                                WHERE idCategoriaNoticia = @idCategoriaNoticia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idCategoriaNoticia", idCategoriaNoticia);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new CategoriaNoticia();
                    respuesta.idCategoriaNoticia = Int32.Parse(dr["idCategoriaNoticia"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar la Categoria Noticia: " + ex.Message);
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
        public List<Noticia> obtenerNoticiasXCategoria(int idEdicion,int idCategoria)
        {
            List<Noticia> noticias = new List<Noticia>();
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT n.idNoticia, CONVERT (char(10), n.fecha, 103)  AS 'fecha', n.titulo AS 'titulo', n.descripcion AS 'descripcion', c.idCategoriaNoticia AS 'idCategoriaNoticia', c.nombre AS 'nombre'
                                FROM Noticias n INNER JOIN CategoriasNoticia c ON n.idCategoriaNoticia = c.idCategoriaNoticia
                                WHERE n.idEdicion = @idEdicion and n.idCategoriaNoticia = @idCategoriaNoticia
                                ORDER BY fecha DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.Parameters.Add(new SqlParameter("@idCategoriaNoticia", idCategoria));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Noticia noticia = new Noticia();
                    noticia.categoria = new CategoriaNoticia { idCategoriaNoticia = Int32.Parse(dr["idCategoriaNoticia"].ToString()), nombre = dr["nombre"].ToString() };
                    noticia.descripcion = dr["descripcion"].ToString();
                    noticia.titulo = dr["titulo"].ToString();
                    noticia.fecha = DateTime.Parse(dr["fecha"].ToString());
                    noticia.idNoticia = int.Parse(dr["idNoticia"].ToString());
                    noticias.Add(noticia);
                }
                con.Close();
                return noticias;
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
        /// Obtener noticias de un torneo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Noticia> obtenerNoticiasList(int idEdicion)
        {
            List<Noticia> noticias = new List<Noticia>();
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT n.idNoticia, CONVERT (char(10), n.fecha, 103)  AS 'fecha', n.titulo AS 'titulo', n.descripcion AS 'descripcion', c.idCategoriaNoticia AS 'idCategoriaNoticia', c.nombre AS 'nombre'
                                FROM Noticias n INNER JOIN CategoriasNoticia c ON n.idCategoriaNoticia = c.idCategoriaNoticia
                                WHERE n.idEdicion = @idEdicion
                                ORDER BY fecha DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEdicion", idEdicion));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Noticia noticia = new Noticia();
                    noticia.categoria = new CategoriaNoticia { idCategoriaNoticia = Int32.Parse(dr["idCategoriaNoticia"].ToString()), nombre = dr["nombre"].ToString() };
                    noticia.descripcion = dr["descripcion"].ToString();
                    noticia.titulo = dr["titulo"].ToString();
                    noticia.fecha = DateTime.Parse(dr["fecha"].ToString());
                    noticia.idNoticia = int.Parse(dr["idNoticia"].ToString());
                    noticias.Add(noticia);
                }
                con.Close();
                return noticias;
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
    }
}
