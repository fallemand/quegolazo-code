﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoADatos
{
   public class DAOFecha
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Registrar Fechas de un Partido, es parte de una transaccion al registrar Fase.
        /// autor: Flor Rojas
        /// </summary>
        /// <param name="delegado">Nuevo delegado a registrar</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public void registrarFechas(Fase fase, SqlConnection con, SqlTransaction trans)
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
                    foreach (Fecha f in g.fechas)
                    {
                        string sql = @"INSERT INTO Fechas (idFecha,idGrupo,idFase,idEdicion,nombre,idEstado)
                                     VALUES (@idFecha,@idGrupo,@idFase,@idEdicion,@nombre,@idEstado)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idFecha", f.idFecha);
                        cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                        cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                        cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                        cmd.Parameters.AddWithValue("@idEstado", 9);
                        if(f.nombre != null)
                            cmd.Parameters.AddWithValue("@nombre",  f.nombre );
                        else
                            cmd.Parameters.AddWithValue("@nombre",  DBNull.Value);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la fecha:" + ex.Message);
            }
        }

        public void obtenerFechas(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Grupo g in fase.grupos)
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    string sql = @"SELECT * 
                                                    FROM Fechas
                                                    WHERE idGrupo=@idGrupo AND idFase=@idFase AND idEdicion=@idEdicion";
                    cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                    cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                    cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Fecha fecha = new Fecha()
                        {
                            idFecha = int.Parse(dr["idFecha"].ToString()),
                            estado = new Estado() { idEstado = int.Parse(dr["idEstado"].ToString()) },
                            nombre = dr["nombre"].ToString(),
                        };
                        g.fechas.Add(fecha);
                    }
                    if (dr != null)
                        dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los datos de la fecha" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

       /// <summary>
       /// Cambia el estado de la fecha a Completa cuando se jugaron todos los partidos
       /// </summary>
       /// <param name="idPartido"></param>
        public void actualizarFecha(int idPartido)
        {

            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
             cmd.Connection = con;

             string sql = @"                            
                            declare @idFecha as int = (select idFecha from Partidos where idPartido=@idPartido)
                            declare @idGrupo as int = (select idFecha from Partidos where idPartido=@idPartido)
                            declare @idFase as int = (select idFecha from Partidos where idPartido=@idPartido)
                            declare @idEdicion as int = (select idEdicion from Partidos where idPartido=@idPartido)
                            declare @cantidad as int = (select count(*) from partidos p where p.idFecha=@idFecha and p.idEdicion=@idEdicion and p.idEstado not in (select idEstado from Estados where idAmbito=4 and idEstado<>13 ))
					                            if(@cantidad=0)
						                            begin
							update Fechas set idEstado=8 where idFecha=@idFecha and idGrupo=@idGrupo and idFase=@idFase and idEdicion=@idEdicion
						END";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idPartido", idPartido);
                cmd.CommandText=sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el estado de la fecha: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}