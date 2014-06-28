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
    public class DAOEdicion
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

          

        /// <summary>
        /// Busca un Usuario con un Id determinado en la base de datos.
        /// </summary>
        /// <param name="idUsuario"> Id del Usuario que se quiere buscar </param>
        /// <returns>Un objeto Torneo, o null si no encuentra el Torneo.</returns>
        public List<Edicion> obtenerEdicionesPorIdCampeonato(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            List<Edicion> respuesta = new List<Edicion>();
            Edicion edicion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM Ediciones
                                WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOCancha gestorCancha = new DAOCancha();
                DAOEdicion gestorEdicion = new DAOEdicion();


                while (dr.Read())
                {
                    edicion = new Edicion();
                    edicion.idEdicion = Int32.Parse(dr["idEdicion"].ToString());
                    edicion.tamanioCancha = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(dr["idTamanioCancha"].ToString()));
                    edicion.formaPuntuacion = gestorEdicion.obtenerFormaPuntuacionPorId(Int32.Parse(dr["idFormaPuntuacion"].ToString()));


                    
                    respuesta.Add(edicion);
  
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el usuario: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        public FormaPuntuacion obtenerFormaPuntuacionPorId(int idFormaPuntuacion)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();

            FormaPuntuacion formaPuntuacion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM FormaPunteacion
                                WHERE idFormaPuntuacion = @idFormaPuntuacion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idFormaPuntuacion", idFormaPuntuacion);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                

                while (dr.Read())
                {
                    formaPuntuacion = new FormaPuntuacion();
                    formaPuntuacion.idFormaPuntuacion = Int32.Parse(dr["idFormaPuntuacion"].ToString());
                    formaPuntuacion.ganado = Int32.Parse(dr["ganado"].ToString());
                    formaPuntuacion.empatado = Int32.Parse(dr["empatado"].ToString());
                    formaPuntuacion.perdido = Int32.Parse(dr["perdido"].ToString());
                    
                }
                return formaPuntuacion;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar la forma de puntuación: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
