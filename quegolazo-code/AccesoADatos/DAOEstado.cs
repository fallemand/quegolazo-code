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
    public class DAOEstado
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        /// <summary>
        /// Obtiene un estado de la base de datos
        /// autor: Pau Pedrosa
        /// </summary>
        /// <param name="idEstado">id del Estado</param>
        /// <returns>Un objeto de tipo Estado</returns>
        public Estado obtenerEstadoPorId(int idEstado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM viewEstados
                                WHERE idEstado = @idEstado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                Estado respuesta = null;
                while (dr.Read())
                {
                    Estado nuevoEstado = new Estado()
                    {
                        idEstado = Int32.Parse(dr["idEstado"].ToString()),
                        nombre = dr["estado"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        ambito = new Ambito() {
                            idAmbito = Int32.Parse(dr["idAmbito"].ToString()),
                            nombre = dr["ambito"].ToString()
                        }
                    };
                    respuesta = nuevoEstado;
                }
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
        /// Obtiene id de estado por estado y por ambito
        /// autor: Flor Rojas
        /// </summary>
        public int obtenerIdEstadoPorEstadoYAmbito(string estado, string ambito)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM viewEstados
                                WHERE estado = @estado AND ambito = @ambito";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@ambito", ambito);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                int respuesta = 0;
                while (dr.Read())
                {
                    respuesta = int.Parse(dr["idEstado"].ToString());
                }
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
    }
}
