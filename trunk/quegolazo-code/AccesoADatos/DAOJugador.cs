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
    public class DAOJugador
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        public int registrarJugador(Jugador jugador, int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"INSERT INTO Jugadores (nombre, dni, fechaNacimiento, email, facebook, sexo, tieneFichaMedica, idEquipo)
                                    VALUES (@nombre, @dni, @fechaNacimiento, @email, @facebook, @sexo, @tieneFichaMedica, @idEquipo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", jugador.nombre);
                cmd.Parameters.AddWithValue("@dni", jugador.dni);
                if(jugador.fechaNacimiento != null)
                    cmd.Parameters.AddWithValue("@fechaNacimiento", jugador.fechaNacimiento);
                else
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                if (jugador.email != null)
                    cmd.Parameters.AddWithValue("@email", jugador.email);
                else
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                if (jugador.facebook != null)
                    cmd.Parameters.AddWithValue("@facebook", jugador.facebook);
                else
                    cmd.Parameters.AddWithValue("@facebook", DBNull.Value);
                cmd.Parameters.AddWithValue("@sexo", jugador.sexo);
                if (jugador.tieneFichaMedica != null)
                    cmd.Parameters.AddWithValue("@tieneFichaMedica", jugador.tieneFichaMedica);
                else
                    cmd.Parameters.AddWithValue("@tieneFichaMedica", DBNull.Value);
                cmd.Parameters.AddWithValue("@idEquipo", idEquipo);             
                cmd.CommandText = sql;
                int idJugador = int.Parse(cmd.ExecuteScalar().ToString());
                return idJugador; //retorna el id del jugador generado por la BD
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el jugador: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
