using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Security.Cryptography;
using AccesoADatos;

namespace Logica
{
   public class GestorUsuario
    {
        /// <summary>
        /// autor=Flor
        /// Método para tomar los datos de la pantalla y crear la entidad Usuario
        /// </summary>
        /// <param name="apellido"></param>
        /// <param name="nombre"></param>
        /// <param name="mail"></param>
        /// <param name="telefono"></param>
        /// <param name="contrasenia"></param>
        /// <returns>Usuario</returns>
        public void registrarUsuario(string apellido, string nombre, string mail, string contrasenia)
        {
            try
            {
                Usuario u = new Usuario
                {
                    apellido = apellido,
                    nombre = nombre,
                    email = mail,
                    contrasenia = encriptarContrasenia(contrasenia),
                    codigo = "123456789",//Falta metodo para generar codigo único
                    tipoUsuario = new TipoUsuario { idTipoUsuario = 1, nombre = "Administrador" },
                };

                DAOUsuario gestorBD = new DAOUsuario();
                gestorBD.registrarUsuario(u);//guarda en la BD
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            ;
        }

        /// <summary>
        /// Método para encriptar clave
        /// autor=Flor
        /// </summary>
        /// <param name="claveSinencriptar"></param>
        /// <returns></returns>
        private string encriptarContrasenia(string claveSinencriptar)
        {

            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(claveSinencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }


        /// <summary>
        /// Método para llamar al activarUsuario
        /// autor=Flor
        /// </summary>
        /// <param name="claveSinencriptar"></param>
        /// <returns></returns>
        public void activarUsuario(int IdUsuario)
        {
            DAOUsuario gestorBD = new DAOUsuario();
            gestorBD.ActivarCuenta(IdUsuario);
        }
         
    }
}
