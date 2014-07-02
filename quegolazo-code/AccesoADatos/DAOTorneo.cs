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
    public class DAOTorneo
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Obtiene todos los Torneos de un Usuario
        /// autor: Paula Pedrosa
        /// </summary>
        /// <parameters>id de Usuario</parameters>
        /// <returns>Lista genérica de Torneos</returns>
        public List<Torneo> obtenerTorneosDeUnUsuario(int idUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            List<Torneo> torneos = new List<Torneo>();
            Torneo respuesta = null;
            
            bool b = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }
                string sql = @"SELECT * 
                             FROM Torneos
                             WHERE idUsuario = @idUsuario
                             ORDER BY nombre ASC";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    b = true;
                    throw new Exception("No hay torneos registrados de ese usuario");
                    
                }

                DAOUsuario gestorUsuario = new DAOUsuario();                
                while (dr.Read())
                {
                    respuesta = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        usuario = gestorUsuario.obtenerUsuarioPorId(Int32.Parse(dr["idUsuario"].ToString()))

                    };
                    torneos.Add(respuesta);
                    
                }

               
                return torneos;
            }
            catch (Exception ex)
            {
                if(!b)
                   throw new Exception("Ocurrió un problema al cargar los datos: " + ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            finally
            {
               if(con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Busca un Torneo con un Id determinado en la base de datos.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo"> Id del Torneo que se quiere buscar </param>
        /// <returns>Un objeto Torneo, o null si no encuentra el Torneo.</returns>
        public Torneo obtenerTorneoPorId(int idTorneo)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            Torneo respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT idTorneo, nombre, nick, idUsuario
                                FROM Torneos
                                WHERE idTorneo = @idTorneo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();

                DAOUsuario gestorUsuario = new DAOUsuario();
             
                while (dr.Read())
                {
                    respuesta = new Torneo()
                    {
                        idTorneo = Int32.Parse(dr["idTorneo"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        nick = dr["nick"].ToString(),
                        usuario = gestorUsuario.obtenerUsuarioPorId(Int32.Parse(dr["idUsuario"].ToString()))
                   
                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el torneo: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Registra un torneo nuevo para un determinado usuario.
        /// </summary>
        /// <param name="torneoNuevo">El torneo que se va a registrar</param>
        /// <param name="usuario">El usuario al cual le pertenece el torneo</param>
//        public void registrarTorneo(Torneo torneoNuevo, Usuario usuario)
//        {
//            SqlConnection con = new SqlConnection(cadenaDeConexion);
//            SqlCommand cmd = new SqlCommand();
//            try
//            {
//                if (con.State == ConnectionState.Closed)
//                {
//                    con.Open();
//                    cmd.Connection = con;
//                }

//                string sql = @"INSERT INTO Torneos (nombre, descripcion
//                                    ";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
//                cmd.CommandText = sql;
//                SqlDataReader dr = cmd.ExecuteReader();

//            }
//        }

    }
}