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
    public class DAOArbitro
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registra en la BD un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="arbitro">Árbitro a registrar</param>
        /// <param name="idTorneo">Id del torneo </param>
        /// <returns>Id del nuevo árbitro generado por la BD</returns>
        public int registrarArbitro(Arbitro arbitro, int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Arbitros (nombre, celular, email, matricula, idTorneo)
                                    VALUES (@nombre, @celular, @email, @matricula, @idTorneo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", arbitro.nombre);
                cmd.Parameters.AddWithValue("@celular", DAOUtils.dbValueNull(arbitro.celular));
                cmd.Parameters.AddWithValue("@email", DAOUtils.dbValueNull(arbitro.email));
                cmd.Parameters.AddWithValue("@matricula", DAOUtils.dbValueNull(arbitro.matricula));
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                int idArbitro = int.Parse(cmd.ExecuteScalar().ToString());
                return idArbitro; //retorna el id del arbitro generado por la BD
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_idTorneo"))
                    throw new Exception("El Árbitro " + arbitro.nombre + " ya se encuentra registrado. Ingrese otro nombre.");
                else
                    throw new Exception("No se pudo registrar el árbitro: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene todos los árbitros de un torneo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista genérica de objetos árbitro</returns>
        public List<Arbitro> obtenerArbitrosDeUnTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Arbitro> respuesta = new List<Arbitro>();
            Arbitro arbitro = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT * 
                                FROM Arbitros
                                WHERE idTorneo = @idTorneo
                                ORDER BY idArbitro DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {                   
                    arbitro = new Arbitro()
                    {
                        idArbitro = Int32.Parse(dr["idArbitro"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        celular = (dr["celular"] != DBNull.Value) ? dr["celular"].ToString() : null,
                        email = (dr["email"] != DBNull.Value) ? dr["email"].ToString() : null,
                        matricula = (dr["matricula"] != DBNull.Value) ? dr["matricula"].ToString() : null,
                    };
                    respuesta.Add(arbitro);
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
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
        /// Obtiene un árbitro de la BD por Id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idArbitro">Id del árbitro a recuperar</param>
        /// <returns>objeto Árbitro</returns>
        public Arbitro obtenerArbitroPorId(int idArbitro)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Arbitro respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Arbitros
                                WHERE idArbitro = @idArbitro";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idArbitro", idArbitro);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Arbitro();
                    respuesta.idArbitro = Int32.Parse(dr["idArbitro"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.celular = (dr["celular"] != DBNull.Value) ? dr["celular"].ToString() : null;
                    respuesta.email = (dr["email"] != DBNull.Value) ? dr["email"].ToString() : null;
                    respuesta.matricula = (dr["matricula"] != DBNull.Value) ? dr["matricula"].ToString() : null;
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Árbitro: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica de la BD un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="arbitro">Árbitro a modificar</param>
        public void modificarArbitro(Arbitro arbitro)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"UPDATE Arbitros
                                SET nombre = @nombre, celular = @celular, email = @email, matricula = @matricula
                                WHERE idArbitro = @idArbitro";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", arbitro.nombre);
                cmd.Parameters.AddWithValue("@idArbitro", arbitro.idArbitro);
                cmd.Parameters.AddWithValue("@celular", DAOUtils.dbValueNull(arbitro.celular));
                cmd.Parameters.AddWithValue("@email", DAOUtils.dbValueNull(arbitro.email));
                cmd.Parameters.AddWithValue("@matricula", DAOUtils.dbValueNull(arbitro.matricula));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_idTorneo"))
                    throw new Exception("Ya existe un árbitro registrado con este nombre, por favor cambielo e intente nuevamente.");
                throw new Exception("No se pudo modificar el árbitro: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Elimina de la BD un árbitro
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idArbitro">Id del árbitro a eliminar</param>
        public void eliminarArbitro(int idArbitro)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Arbitros
                                WHERE idArbitro = @idArbitro";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idArbitro", idArbitro);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el árbitro: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
