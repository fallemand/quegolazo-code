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
    public class DAOGrupo
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Grupo, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="delegado">Nuevo delegado a registrar</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarGrupos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Grupo g in fase.grupos)
                {
                    string sql = @"INSERT INTO Grupos (idGrupo,idFase,idEdicion,nombre)
                                    VALUES (@idGrupo,@idFase,@idEdicion,@nombre) 
                                    SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                    cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                    cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                    cmd.Parameters.AddWithValue("@nombre", g.nombre);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    foreach (Equipo equipo in g.equipos)
                    {
                        sql = @"INSERT INTO EquiposXGrupo (idEquipo, idGrupo, idFase, idEdicion)
                                VALUES (@idEquipo,@idGrupo, @idFase, @idEdicion)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@idEquipo", equipo.idEquipo);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el grupo:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los grupos de una fase, si el parametro equiposCompletos es True, devuelve todos los atributos de los equipos del grupo, sino solo el id y nombre
        /// autor: Flor Rojas
        /// </summary>
        public void obtenerGrupos(Fase fase, SqlConnection con, bool equiposCompletos)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;                
                string sql = @"SELECT * 
                                FROM Grupos
                                WHERE idFase = @idFase AND idEdicion = @idEdicion";
                cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Grupo grupo=new Grupo()
                    {
                        idGrupo = int.Parse(dr["idGrupo"].ToString()),
                        idEdicion=fase.idEdicion,
                        idFase=fase.idFase,
                        nombre= int.Parse(dr["nombre"].ToString()),                                 
                    };
                    grupo.equipos = obtenerEquiposDeUnGrupo(grupo.idGrupo, grupo.idFase, grupo.idEdicion, equiposCompletos);
                    fase.grupos.Add(grupo);
                }
                if (dr != null)
                    dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los datos del grupo" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene todos equipos de un determinado grupo. Si el parametro equiposCompletos es True, devuelve todos los atributos completos de un equipo, sino devuelve solo el id y el nombre
        /// autor: Antonio Herrera
        /// </summary>
        public List<Equipo> obtenerEquiposDeUnGrupo(int idGrupo, int idFase, int idEdicion, bool equiposCompletos)
        {
           try
            {
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            List<Equipo> respuesta = new List<Equipo>();          
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT idEquipo 
                               FROM EquiposXGrupo 
                               WHERE idEdicion=@idEdicion AND idFase=@idFase AND idGrupo=@idGrupo ";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@idFase", idFase);
                cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int idEquipo = int.Parse(dr["idEquipo"].ToString());
                    respuesta.Add((equiposCompletos) ? new DAOEquipo().obtenerEquipoPorId(idEquipo) : new DAOEquipo().obtenerEquipoReducidoPorId(idEquipo));   
                }
                con.Close();
                return respuesta;                
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los equipos del grupo" + ex.Message);
            
            }
            finally
            {
              
                    
            }
        }
    }
}
