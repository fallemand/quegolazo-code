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
    public class DAOTipoUsuario
    {
        public string cadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

        public TipoUsuario obtenerTipoUsuarioPorId(int idTipoUsuario)
        {
            SqlConnection con = new SqlConnection(cadenaDeConexion);
            SqlCommand cmd = new SqlCommand();


            TipoUsuario respuesta = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    cmd.Connection = con;
                }

                string sql = @"SELECT *
                                FROM TiposUsuario
                                WHERE idTipoUsuario = @idTipoUsuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    respuesta = new TipoUsuario()
                    {
                        idTipoUsuario = Int32.Parse(dr["idTipoUsuario"].ToString()),
                        nombre = dr["nombre"].ToString(),

                    };
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar recuperar el tipo de usuario: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
