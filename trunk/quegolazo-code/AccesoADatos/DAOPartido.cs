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
   public class DAOPartido
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Partido de las Fechas de un Partido, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="fase">Fase que se esta grabando</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarPartidos(Fase fase, SqlConnection con, SqlTransaction trans)
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
                    foreach (Fecha f in g.fixture)
                    {
                         foreach (Partido p in f.partidos)
                         {
                             if (p.idPartido != 0)
                             {
                                 string sql = @"INSERT INTO Partidos (idPartido,idFecha,idGrupo,idFase,idEdicion,idEquipoLocal,idEquipoVisitante, idEstado)
                                     VALUES (@idPartido,@idFecha,@idGrupo,@idFase,@idEdicion,@idEquipoLocal,@idEquipoVisitante, @idEstado)";
                                 cmd.Parameters.Clear();
                                 cmd.Parameters.AddWithValue("@idPartido", p.idPartido);
                                 cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                                 cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                                 cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                                 cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                                 cmd.Parameters.AddWithValue("@idEquipoLocal", p.local.idEquipo);
                                 cmd.Parameters.AddWithValue("@idEquipoVisitante", p.visita.idEquipo);
                                 cmd.Parameters.AddWithValue("@idEstado", 1);
                                 cmd.CommandText = sql;
                                 cmd.ExecuteNonQuery();
                             }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el partido" + ex.Message);
            }
        }
    }
}
