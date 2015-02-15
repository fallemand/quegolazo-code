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
        /// Registrar un nuevo Equipo en un torneo en la BD, junto a sus delegados
        /// autor: Pau Pedrosa        
        /// </summary>
        /// <param name="equipo">El objeto Equipo que se va a registrar</param>
        /// <param name="idTorneo">Id del torneo al que pertenece el equipo</param>
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
                //registra delegado en la BD
                if(equipo.delegadoPrincipal!=null)
                    equipo.delegadoPrincipal.idDelegado=daoDelegado.registrarDelegado(equipo.delegadoPrincipal, con, trans);
                if(equipo.delegadoOpcional!=null)
                    equipo.delegadoOpcional.idDelegado=daoDelegado.registrarDelegado(equipo.delegadoOpcional, con, trans);
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Equipos (nombre, colorCamisetaPrimario, colorCamisetaSecundario, directorTecnico, idDelegadoPrincipal, idDelegadoOpcional, idTorneo)
                                    VALUES (@nombre, @colorCamisetaPrimario, @colorCamisetaSecundario, @directorTecnico , @idDelegadoPrincipal, @idDelegadoOpcional, @idTorneo)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", equipo.nombre);
                cmd.Parameters.AddWithValue("@colorCamisetaPrimario", equipo.colorCamisetaPrimario);
                cmd.Parameters.AddWithValue("@colorCamisetaSecundario", equipo.colorCamisetaSecundario);
                cmd.Parameters.AddWithValue("@directorTecnico",equipo.directorTecnico);               
                if(equipo.delegadoPrincipal != null)
                    cmd.Parameters.AddWithValue("@idDelegadoPrincipal", equipo.delegadoPrincipal.idDelegado);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoPrincipal", DBNull.Value);
                if (equipo.delegadoOpcional != null)
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", equipo.delegadoOpcional.idDelegado);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", DBNull.Value);
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                int idEquipo = int.Parse(cmd.ExecuteScalar().ToString()); 
                trans.Commit();
                return idEquipo; //retorna el id del equipo generado por la BD
            }
            catch (SqlException ex)
            {   //excepción de BD, por clave unique
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
                throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
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
            SqlDataReader dr;
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
                DAODelegado daoDelegado = new DAODelegado();
                DAOJugador daoJugador = new DAOJugador();
                DAOTorneo daoTorneo = new DAOTorneo();
                dr = cmd.ExecuteReader();
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
                    respuesta.jugadores = daoJugador.obtenerJugadoresDeUnEquipo(Int32.Parse(dr["idEquipo"].ToString()));
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

          /// <summary>
        /// Obtiene un Equipo por Id, pero el objeto no tiene todos sus atributos, solo el nombre y el ID.
        /// autor: Antonio Herrera
        /// </summary>
        /// <param name="idEquipo">id del equipo</param>
        /// <returns>Objeto Equipo</returns>
        public Equipo obtenerEquipoReducidoPorId(int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
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
                DAODelegado daoDelegado = new DAODelegado();
                DAOJugador daoJugador = new DAOJugador();
                DAOTorneo daoTorneo = new DAOTorneo();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Equipo();
                    respuesta.idEquipo = Int32.Parse(dr["idEquipo"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                }
                if (dr != null)
                    dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el Equipo: " + ex.Message);
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
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
                //registra los delegados en la BD
                DAODelegado daoDelegado = new DAODelegado();
                if(equipo.delegadoPrincipal != null)
                    equipo.delegadoPrincipal.idDelegado = daoDelegado.registrarDelegado(equipo.delegadoPrincipal, con, trans);
                if(equipo.delegadoOpcional != null)
                    equipo.delegadoOpcional.idDelegado = daoDelegado.registrarDelegado(equipo.delegadoOpcional, con, trans);
                      
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.Parameters.Clear();
                string sql = @"UPDATE Equipos
                                SET nombre = @nombreEquipo, colorCamisetaPrimario = @colorCamisetaPrimario,
                                colorCamisetaSecundario = @colorCamisetaSecundario, directorTecnico = @directorTecnico,
                                idDelegadoPrincipal = @idDelegadoPrincipal, idDelegadoOpcional = @idDelegadoOpcional
                                WHERE idEquipo = @idEquipo";                 
                cmd.Parameters.AddWithValue("@nombreEquipo", equipo.nombre);
                cmd.Parameters.AddWithValue("@colorCamisetaPrimario", equipo.colorCamisetaPrimario);
                cmd.Parameters.AddWithValue("@colorCamisetaSecundario", equipo.colorCamisetaSecundario);
                cmd.Parameters.AddWithValue("@directorTecnico", equipo.directorTecnico);
                cmd.Parameters.AddWithValue("@idDelegadoPrincipal", equipo.delegadoPrincipal.idDelegado);                       
                cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                if(equipo.delegadoOpcional != null)
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", equipo.delegadoOpcional.idDelegado);
                else
                    cmd.Parameters.AddWithValue("@idDelegadoOpcional", DBNull.Value);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                //Si ya existe un equipo con ese nombre en ese torneo
                if (ex.Message.Contains("unique_nombre_idTorneo"))
                    throw new Exception("Ya existe un equipo registrado con este nombre, por favor cambielo e intente nuevamente.");
                throw new Exception("No se pudo modificar el equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();                
            }
        }

        /// <summary>
        /// Elimina un equipo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idCancha">Id del equipo a eliminar</param>
        public void eliminarEquipo(int idEquipo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"DELETE FROM Equipos
                                WHERE idEquipo = @idEquipo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("EquiposXGrupo"))
                    throw new Exception("No puede eliminar ese Equipo: ESTÁ ASIGNADO A UN PARTIDO.");
                throw new Exception("No se pudo eliminar el equipo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Registrar equipos para una edición
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarEquiposEnEdicion(List<Equipo> equipos, int idEdicion, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //trans = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = trans;    

                foreach (Equipo equipo in equipos)
                {
                    string sql = @"INSERT INTO EquipoXEdicion (idEdicion, idEquipo)
                                    VALUES (@idEdicion, @idEquipo)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                    cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();                    
                }
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar el equipo: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza en la Bd equipos a participar en la edición
        /// autor: Pau Pedrosa
        /// </summary>
        public void actualizarEquiposEnEdicion(List<Equipo> equipos, int idEdicion, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;

                string sqlEliminacion = @"DELETE FROM EquipoXEdicion 
                                            WHERE idEdicion = @idEdicion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.CommandText = sqlEliminacion;
                cmd.ExecuteNonQuery();

                foreach (Equipo equipo in equipos)
                {
                    string sqlInsercion = @"INSERT INTO EquipoXEdicion (idEdicion, idEquipo)
                                                VALUES (@idEdicion, @idEquipo)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                    cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                    cmd.CommandText = sqlInsercion;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("No se pudo registrar el equipo: " + ex.Message);
            }
        }
    }
}


