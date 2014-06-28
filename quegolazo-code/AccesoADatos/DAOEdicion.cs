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
        /// Obtiene una lista de ediciones de un determinado torneo
      /// </summary>
      /// <param name="idTorneo">Id del torneo</param>
      /// <returns>Lista de Objeto Ediciones, o null sino existen ediciones de ese torneo</returns>
        public List<Edicion> obtenerEdicionesPorIdTorneo(int idTorneo)
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
                DAOTipoSuperficie gestorTipoSuperficie = new DAOTipoSuperficie();
                DAOEstado gestorEstado = new DAOEstado();      
                DAOTorneo gestorTorneo = new DAOTorneo();


                while (dr.Read())
                {
                    edicion = new Edicion();
                    edicion.idEdicion = Int32.Parse(dr["idEdicion"].ToString());
                    edicion.nombre = dr["nombre"].ToString();
                    edicion.tamanioCancha = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(dr["idTamanioCancha"].ToString()));
                    edicion.formaPuntuacion = gestorEdicion.obtenerFormaPuntuacionPorId(Int32.Parse(dr["idFormaPuntuacion"].ToString()));
                    edicion.tipoSuperficie = gestorTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(dr["idTipoSuperficie"].ToString()));
                    edicion.estado = gestorEstado.obtenerUnEstadoPorId(Int32.Parse(dr["idEstado"].ToString()));
                    edicion.cancha = gestorCancha.obtenerCanchasDeEdicion(Int32.Parse(dr["idEdicion"].ToString()));
                    edicion.torneo = gestorTorneo.obtenerTorneoPorId(Int32.Parse(dr["idTorneo"].ToString()));
                    respuesta.Add(edicion);
  
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar las Ediciones de un Torneo: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        /// <summary>
        /// Busca una FormaPuntuacion por su id
        /// </summary>
        /// <param name="idFormaPuntuacion">id de Forma puntuación</param>
        /// <returns>Objeto FormaPuntuacion, o null sino lo encuentra</returns>
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
                                FROM FormasPuntuacion
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
