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
        /// <param name="equipo">El objeto Equipo que se va a registrar</param>
        /// <param name="idTorneo">el id del torneo al que pertenece el equipo</param>
        /// <returns>El id del equipo registrado</returns>
        public int registrarEquipo(Equipo equipo, int idTorneo) 
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
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
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
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
                   
            }
        }

        /// <summary>
        /// Obtiene una lista genérica del objeto Equipo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista genérica del objeto Equipo</returns>
        public List<Equipo> obtenerEquiposDeUnTorneo(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Equipo> respuesta = new List<Equipo>();
            Equipo equipo = null;
            bool noHayEquiposRegistrados = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();                
                cmd.Connection = con;
                string sql = @"SELECT * 
                             FROM Equipos
                             WHERE idTorneo = @idTorneo
                             ORDER BY idEquipo DESC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idTorneo", idTorneo));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    noHayEquiposRegistrados = true;
                    throw new Exception("No hay equipos registrados de ese torneo");
                }
                DAODelegado daoDelegado = new DAODelegado();
                DAOTorneo daoTorneo = new DAOTorneo();
                while (dr.Read())
                {
                    equipo = new Equipo()
                    {
                        idEquipo = Int32.Parse(dr["idEquipo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        colorCamisetaPrimario = dr["colorCamisetaPrimario"].ToString(),
                        colorCamisetaSecundario = dr["colorCamisetaSecundario"].ToString(),
                        directorTecnico = dr["directorTecnico"].ToString(),
                        delegadoPrincipal = daoDelegado.obtenerDelegadoPorId(Int32.Parse(dr["idDelegadoPrincipal"].ToString())),
                        delegadoOpcional = daoDelegado.obtenerDelegadoPorId(Int32.Parse(dr["idDelegadoPrincipal"].ToString())),                        
                    };
                    respuesta.Add(equipo);
                }
                if (dr != null)
                    dr.Close();

                return respuesta;
            }
            catch (Exception ex)
            {
                if (!noHayEquiposRegistrados)
                    throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
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
        /// Obtiene equipo por id
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEquipo">id del equipo</param>
        /// <returns>Objeto Equipo</returns>
        public Equipo obtenerEquipoPorId(int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            Equipo respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Equipos
                                WHERE idEquipo = @idEquipo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                DAODelegado daoDelegado = new DAODelegado();
                DAOTorneo daoTorneo = new DAOTorneo();                
                while (dr.Read())
                {
                    respuesta = new Equipo();
                    respuesta.idEquipo = Int32.Parse(dr["idEquipo"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.directorTecnico = dr["directorTecnico"].ToString();
                    respuesta.colorCamisetaPrimario = dr["colorCamisetaPrimario"].ToString();
                    respuesta.colorCamisetaSecundario = dr["colorCamisetaSecundario"].ToString();
                    respuesta.delegadoPrincipal = daoDelegado.obtenerDelegadoPorId(Int32.Parse(dr["idDelegadoPrincipal"].ToString()));                                    
                    respuesta.delegadoOpcional = (dr["idDelegadoOpcional"] != System.DBNull.Value) ? daoDelegado.obtenerDelegadoPorId(Int32.Parse(dr["idDelegadoOpcional"].ToString())) : null;
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Modifica en la BD el equipo
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="equipo">Objeto Equipo a modificar con sus nuevos datos</param>
        public void modificarEquipo(Equipo equipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;      
            bool nuevoDelegado = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
        
                DAODelegado daoDelegado = new DAODelegado();
                if (equipo.delegadoPrincipal != null)
                    daoDelegado.modificarDelegado(equipo.delegadoPrincipal, con, trans);
                if (equipo.delegadoOpcional != null)
                {
                    if (equipo.delegadoOpcional.idDelegado != 0)
                        daoDelegado.modificarDelegado(equipo.delegadoOpcional, con, trans);
                    else 
                    {
                        equipo.delegadoOpcional.idDelegado = daoDelegado.registrarDelegado(equipo.delegadoOpcional, con, trans);
                        nuevoDelegado = true;
                    }                  
                }                            
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"UPDATE Equipos
                                     SET nombre = @nombreEquipo, colorCamisetaPrimario = @colorCamisetaPrimario,
                                     colorCamisetaSecundario = @colorCamisetaSecundario, directorTecnico = @directorTecnico ";
                if (nuevoDelegado)
                {
                    sql += " , idDelegadoOpcional = @idDelegadoOpcional ";
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", equipo.delegadoOpcional.idDelegado);
                }
                sql += " WHERE idEquipo = @idEquipo"; 
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreEquipo", equipo.nombre);
                cmd.Parameters.AddWithValue("@colorCamisetaPrimario", equipo.colorCamisetaPrimario);
                cmd.Parameters.AddWithValue("@colorCamisetaSecundario", equipo.colorCamisetaSecundario);
                cmd.Parameters.AddWithValue("@directorTecnico", equipo.directorTecnico);                                         
                cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                //Si ya existe un equipo con ese nombre en ese torneo
                if (e.Message.Contains("unique_nombre_idTorneo"))
                {
                    throw new Exception("No se pudo modificar el equipo: Ya existe un equipo registrado con este nombre, por favor cambielo e intente nuevamente.");
                }
                throw new Exception("No se pudo modificar el equipo: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();                
            }
        }
            

    }
}
