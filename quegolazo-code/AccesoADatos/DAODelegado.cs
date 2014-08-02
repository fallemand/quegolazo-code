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
    public class DAODelegado
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        /// <summary>
        /// Registrar Delegado de un Equipo, es parte de una transaccion al registrar un equipo.
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="delegado">Nuevo delegado a registrar</param>
        /// <param name="con">La conexion abierta de la transaccion.</param>
        /// <param name="trans">La transaccion de registro de equipo</param>
        /// <returns>Id del delegado registrado</returns>
        public int registrarDelegado(Delegado delegado, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                cmd.Transaction = trans;
                string sql = @"INSERT INTO Delegados (nombre, email, telefono, domicilio)
                                              VALUES (@nombre, @email, @telefono, @domicilio) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", delegado.nombre);
                cmd.Parameters.AddWithValue("@email", delegado.email);
                cmd.Parameters.AddWithValue("@telefono", delegado.telefono);
                cmd.Parameters.AddWithValue("@domicilio", delegado.domicilio);
                cmd.CommandText = sql;
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el delegado: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un Delegado por id
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idDelegado">Id del delegado a obtener</param>
        /// <returns>Objeto delegado</returns>
        public Delegado obtenerDelegadoPorId(int idDelegado)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            Delegado respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Connection = con;
                string sql = @"SELECT *
                                FROM Delegados
                                WHERE idDelegado = @idDelegado";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDelegado", idDelegado);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    respuesta = new Delegado();
                    respuesta.idDelegado = Int32.Parse(dr["idDelegado"].ToString());
                    respuesta.nombre = dr["nombre"].ToString();
                    respuesta.email = dr["email"].ToString();
                    respuesta.telefono = dr["telefono"].ToString();
                    respuesta.domicilio = dr["domicilio"].ToString();                  
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar recuperar el delegado: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }              
    }
}