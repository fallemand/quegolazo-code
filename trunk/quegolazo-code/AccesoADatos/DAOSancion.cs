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
    public class DAOSancion
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        
        public int registrarSancion(Sancion sancion, int idPartido)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Sanciones (idEquipo, idJugador, motivo, idPartido)
                                    VALUES (@idEquipo, @idJugador, @motivo, @idPartido)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", sancion.idEquipo);
                cmd.Parameters.AddWithValue("@idJugador", DAOUtils.dbValueNull(sancion.idJugador));
                cmd.Parameters.AddWithValue("@motivo", DAOUtils.dbValueNull(sancion.motivo));
                cmd.Parameters.AddWithValue("@idPartido", DAOUtils.dbValueNull(idPartido));
                cmd.CommandText = sql;
                int idSancion = int.Parse(cmd.ExecuteScalar().ToString());
                return idSancion; //retorna el id de la sanción generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la sanción: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public List<MotivoSancion> obtenerMotivos()
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<MotivoSancion> respuesta = new List<MotivoSancion>();
            MotivoSancion motivoSancion = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                               FROM MotivosSancion";
                cmd.Parameters.Clear();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    motivoSancion = new MotivoSancion();
                    motivoSancion.idMotivoSancion = Int32.Parse(dr["idMotivoSancion"].ToString());
                    motivoSancion.nombre = dr["nombre"].ToString();
                    respuesta.Add(motivoSancion);
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar los datos: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
