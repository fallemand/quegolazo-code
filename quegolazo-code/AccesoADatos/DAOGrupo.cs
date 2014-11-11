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
                   
                }

                registrarEquiposPorGrupo(fase, con, trans);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el grupo:" + ex.Message);
            }
        }

        private void registrarEquiposPorGrupo(Fase fase, SqlConnection con, SqlTransaction trans)
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
                    string sql = @"INSERT INTO EquiposXGrupo (idEquipo,idGrupo,idFase,idEdicion)
                                VALUES (@idEquipo,@idGrupo,@idFase,@idEdicion) 
                                SELECT SCOPE_IDENTITY()";
                    foreach(Equipo e in g.equipos)
                    {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idEquipo", e.idEquipo);
                    cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                    cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                    cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
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

        public void obtenerGrupos(Fase fase, SqlConnection con, SqlTransaction trans)
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans; 
                            string sql = @"SELECT * 
                                            FROM Grupos
                                            WHERE idFase=@idFase AND idEdicion = @idEdicion";
                            cmd.Parameters.Clear();            
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
                                    //equipos = obtenerEquipos(con, trans, int.Parse(dr["idGrupo"].ToString()), fase.idEdicion, fase.idFase)
                                };
                                fase.grupos.Add(grupo);           
                            }
                            if (dr != null)
                                dr.Close();
                            obtenerEquipos(con, trans, fase);
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

         public void obtenerEquipos(SqlConnection con, SqlTransaction trans, Fase fase)
        {
           
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                foreach (Grupo g in fase.grupos)
                {

                    string sql = @"SELECT eg.idEquipo, e.nombre
                                        FROM EquiposXGrupo eg join Equipos  e on eg.idEquipo=e.idEquipo 
                                        WHERE eg.idFase=@idFase AND eg.idEdicion=@idEdicion AND eg.idGrupo=@idGrupo";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idFase", fase.idFase);
                    cmd.Parameters.AddWithValue("@idEdicion", fase.idEdicion);
                    cmd.Parameters.AddWithValue("@idGrupo", g.idGrupo);
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Equipo equipo = new Equipo()
                        {
                            idEquipo = int.Parse(dr["idEquipo"].ToString()),
                            nombre = dr["nombre"].ToString(),
                        };
                        g.equipos.Add(equipo);
                    }
                    if (dr != null)
                        dr.Close();
                }
                            
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los datos del Equipo" + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }


    }
}
