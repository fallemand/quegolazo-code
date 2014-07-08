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
    public class DAOEquipo
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        
        /// <summary>
        /// Registrar un nuevo Equipo en un torneo
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nuevoEquipo">Objeto nuevo a registrar</param>
        /// <param name="torneo">Torneo en donde se va a registrar el equipo</param>
        /// <param name="delegadoPrincipal">Delegado Principal</param>
        /// <param name="delegadoOpcional">Delegado opcional o null en caso que no tenga</param>
        /// <returns>El id del equipo registrado</returns>
        public int registrarEquipo(Equipo nuevoEquipo, Torneo torneo, Delegado delegadoPrincipal, Delegado delegadoOpcional) 
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

                string sql = @"INSERT INTO Equipos (nombre, colorCamisetaPrimario, colorCamisetaSecundario, directorTecnico, idDelegadoPrincipal, idDelegadoOpcional, idTorneo)
                                              VALUES (@nombre, @colorCamisetaPrimario, @colorCamisetaSecundario, @directorTecnico , @idDelegadoPrincipal, @idDelegadoOpcional, @idTorneo) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nuevoEquipo.nombre);
                cmd.Parameters.AddWithValue("@colorCamisetaPrimario", nuevoEquipo.colorCamisetaPrimario);
                cmd.Parameters.AddWithValue("@colorCamisetaSecundario", nuevoEquipo.colorCamisetaSecundario);

                if(nuevoEquipo.directorTecnico == null)
                    cmd.Parameters.AddWithValue("@directorTecnico", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@directorTecnico", nuevoEquipo.directorTecnico);

                cmd.Parameters.AddWithValue("@idDelegadoPrincipal", delegadoPrincipal.idDelegado);

                if(delegadoOpcional == null)
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", delegadoOpcional.idDelegado);

                cmd.Parameters.AddWithValue("@idTorneo", torneo.idTorneo);

                                                
                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo registrar el equipo: " + e.Message);
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
