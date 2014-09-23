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
    public class DAOFase
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary> 
        /// Registrar una nueva Fase 
        /// autor: Florencia Rojas        
        /// </summary>
        /// <param name="equipo">El objeto Equipo que se va a registrar</param>
        /// <param name="idTorneo">Id del torneo al que pertenece el equipo</param>
        /// <returns>El id del equipo registrado</returns>
        public void registrarFase(List<Fase> fases)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                trans = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = trans;

                foreach (Fase fase in fases)
                {
                    if (fase != null)
                    {
                        string sql = @"INSERT INTO Fases (idFase,idEdicion,tipoFixture,idEstado)
                                    VALUES (@idFase,@idEdicion,@tipoFixture,@idEstado)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@tipoFixture", fase.tipoFixture.nombre);
                       // cmd.Parameters.AddWithValue("@idEstado", fase.estado.idEstado);
                        cmd.Parameters.AddWithValue("@idEstado", 4);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        if (fase.grupos.Count != 0)
                        {
                            DAOGrupo DaoGrupo = new DAOGrupo();
                            DaoGrupo.registrarGrupos(fase, con, trans);

                            DAOFecha DaoFecha = new DAOFecha();
                            DaoFecha.registrarFechas(fase, con, trans);

                            DAOPartido DaoPartido = new DAOPartido();
                            DaoPartido.registrarPartidos(fase, con, trans);
                        }
                    }
                }

                trans.Commit();
            }
            catch (SqlException ex)
            { 
                trans.Rollback();
                throw new Exception("No se pudo registrar la Fase: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Obtiene las fases  de una edición por Id. Si no tiene devuelve lista vacia
        /// autor: Florencia Rojas
        /// </summary>
        public List<Fase> obtenerFases(int idEdicion)
        {
             SqlConnection con = new SqlConnection(cadenaDeConexion);
             SqlCommand cmd = new SqlCommand();
             SqlDataReader dr;
             SqlTransaction trans = null;
             List<Fase> fases = new List<Fase>();
             DAOGrupo DaoGrupo= new DAOGrupo();
             DAOFecha DaoFecha=new DAOFecha();
             DAOPartido DaoPartido=new DAOPartido();
             try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 trans = con.BeginTransaction();
                 cmd.Connection = con;
                 cmd.Transaction = trans;
                 string sql = @"SELECT * 
                                FROM  Fases
                                WHERE idEdicion=@idEdicion";
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@idEdicion", idEdicion);
                 cmd.CommandText = sql;
                 dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {
                     Fase fase = new Fase()
                      {
                          idFase = int.Parse(dr["idFase"].ToString()),
                          idEdicion = idEdicion,
                          estado = new Estado() { idEstado = int.Parse(dr["idEstado"].ToString()) },
                          tipoFixture = new TipoFixture() { nombre = dr["tipoFixture"].ToString() },
                      };        
                     fases.Add(fase);
                 }
                 if (dr != null)
                     dr.Close();

                 foreach (Fase fase in fases)
                 {
                     DaoGrupo.obtenerGrupos(fase, con, trans);
                     DaoFecha.obtenerFechas(fase, con, trans);
                     DaoPartido.obtenerPartidos(fase, con, trans);
                 }
                 return fases;
             }
             catch (Exception ex)
             {
                 throw new Exception("Error al intentar recuperar los equipos de una Edición: " + ex.Message);
             }
             finally
             {
                 if (con != null && con.State == ConnectionState.Open)
                     con.Close();
             }
        }
    }
}
