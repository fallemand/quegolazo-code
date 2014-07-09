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
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista de Objeto Ediciones, o null sino existen ediciones de ese torneo</returns>
        public List<Edicion> obtenerEdicionesPorIdTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            List<Edicion> respuesta = new List<Edicion>();
            Edicion edicion = null;
            bool b = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM Ediciones
                                WHERE idTorneo = @idTorneo
                                ORDER BY idEdicion DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOCancha daoCancha = new DAOCancha();
                DAOEdicion daoEdicion = new DAOEdicion();
                DAOTipoSuperficie daoTipoSuperficie = new DAOTipoSuperficie();
                DAOEstado daoEstado = new DAOEstado();
                DAOTorneo daoTorneo = new DAOTorneo();

                if (!dr.HasRows)
                {
                    b = true;
                    throw new Exception("No hay ediciones registradas");

                }

                while (dr.Read())
                {
                    edicion = new Edicion();
                    edicion.idEdicion = Int32.Parse(dr["idEdicion"].ToString());
                    edicion.nombre = dr["nombre"].ToString();
                    edicion.tamanioCancha = daoCancha.obtenerTamanioCanchaPorId(Int32.Parse(dr["idTamanioCancha"].ToString()));
                    edicion.tipoSuperficie = daoTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(dr["idTipoSuperficie"].ToString()));
                    edicion.estado = daoEstado.obtenerUnEstadoPorId(Int32.Parse(dr["idEstado"].ToString()));
                    edicion.cancha = daoCancha.obtenerCanchasDeEdicion(Int32.Parse(dr["idEdicion"].ToString()));
                    edicion.torneo = daoTorneo.obtenerTorneoPorId(Int32.Parse(dr["idTorneo"].ToString()));
                    edicion.puntosEmpatado = Int32.Parse(dr["puntosEmpatado"].ToString());
                    edicion.puntosGanado = Int32.Parse(dr["puntosGanado"].ToString());
                    edicion.puntosPerdido = Int32.Parse(dr["puntosPerdido"].ToString());
                    respuesta.Add(edicion);

                }
                return respuesta;
            }
            catch (Exception ex)
            {
                if (!b)
                    throw new Exception("Error al intentar recuperar las Ediciones de un Torneo: " + ex.Message);
                else
                    throw new Exception(ex.Message);

            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

        }



        /// <summary>
        /// Registrar una Nueva Edición
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="edicionNueva">Objeto nueva Edición</param>
        public void registrarEdicion(Edicion edicionNueva)
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

                string sql = @"INSERT INTO Ediciones (nombre, idTamanioCancha, idTipoSuperficie, idEstado, idTorneo, puntosGanado, puntosPerdido, puntosEmpatado)
                                              VALUES (@nombre, @idTamanioCancha, @idTipoSuperficie, @idEstado, @idTorneo, @puntosGanado, @puntosPerdido, @puntosEmpatado )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", edicionNueva.nombre);     
                cmd.Parameters.AddWithValue("@idTamanioCancha", edicionNueva.tamanioCancha.idTamanioCancha);
                cmd.Parameters.AddWithValue("@idTipoSuperficie", edicionNueva.tipoSuperficie.idTipoSuperficie);
                cmd.Parameters.AddWithValue("@idEstado", edicionNueva.estado.idEstado);
                cmd.Parameters.AddWithValue("@idTorneo", edicionNueva.torneo.idTorneo);
                cmd.Parameters.AddWithValue("@puntosEmpatado", edicionNueva.puntosEmpatado);
                cmd.Parameters.AddWithValue("@puntosGanado", edicionNueva.puntosGanado);
                cmd.Parameters.AddWithValue("@puntosPerdido", edicionNueva.puntosPerdido);
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo registrar la edición: " + e.Message);
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