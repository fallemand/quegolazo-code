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
        /// Registrar un nuevo Equipo en un torneo, junto a sus delegados
        /// autor: Paula Pedrosa
        /// </summary>
        /// <returns>El id del equipo registrado</returns>
        public int registrarEquipo(Equipo equipo) 
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans=con.BeginTransaction();

                DAODelegado daoDelegado = new DAODelegado();
                if(equipo.delegadoPrincipal!=null)
                    equipo.delegadoPrincipal.idDelegado=daoDelegado.registrarDelegado(equipo.delegadoPrincipal, con, trans);
                if(equipo.delegadoOpcional!=null)
                    equipo.delegadoOpcional.idDelegado=daoDelegado.registrarDelegado(equipo.delegadoOpcional, con, trans);

                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Equipos (nombre, colorCamisetaPrimario, colorCamisetaSecundario, directorTecnico, idDelegadoPrincipal, idDelegadoOpcional, idTorneo)
                                              VALUES (@nombre, @colorCamisetaPrimario, @colorCamisetaSecundario, @directorTecnico , @idDelegadoPrincipal, @idDelegadoOpcional, @idTorneo) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", equipo.nombre);
                cmd.Parameters.AddWithValue("@colorCamisetaPrimario", equipo.colorCamisetaPrimario);
                cmd.Parameters.AddWithValue("@colorCamisetaSecundario", equipo.colorCamisetaSecundario);
                cmd.Parameters.AddWithValue("@directorTecnico",equipo.directorTecnico);
                if(equipo.delegadoPrincipal!=null)
                    cmd.Parameters.AddWithValue("@idDelegadoPrincipal", equipo.delegadoPrincipal.idDelegado);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoPrincipal", DBNull.Value);
                if (equipo.delegadoOpcional != null)
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", equipo.delegadoOpcional.idDelegado);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", DBNull.Value);
                cmd.Parameters.AddWithValue("@idTorneo", equipo.torneo.idTorneo);
                cmd.CommandText = sql;
                int idEquipo= int.Parse(cmd.ExecuteScalar().ToString());
                trans.Commit();
                return idEquipo;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                if (ex.Class == 14 && ex.Number == 2601)
                    throw new Exception("El equipo " + equipo.nombre + " ya se encuentra registrado. Ingrese otro nombre para el equipo.");
                else
                    throw new Exception("No se pudo registrar el equipo: " + ex.Message);
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
