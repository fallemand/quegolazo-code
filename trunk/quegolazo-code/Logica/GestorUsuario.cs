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
       public Usuario usuario;

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
        public string registrarUsuario(string apellido, string nombre, string mail, string contrasenia)
        {
            try
            {
                Usuario u = new Usuario
                {
                    apellido = apellido,
                    nombre = nombre,
                    email = mail,
                    contrasenia = encriptarContrasenia(contrasenia),
                    codigo = crearCodigo(),
                    tipoUsuario = new TipoUsuario { idTipoUsuario = 1, nombre = "Cliente" },
                };

                DAOUsuario gestorBD = new DAOUsuario();
                gestorBD.registrarUsuario(u);//guarda en la BD
                return u.codigo;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No se puede insertar una clave duplicada"))
                {
                    throw new Exception("El usuario con el mail: " + mail + " Ya se encuentra registrado. Por favor ingrese una cuenta de correo diferente.");
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            
            ;
        }


        /// <summary>
        /// Método para encriptar clave
        /// autor=Flor
        /// </summary>
        /// <param name="claveSinencriptar"></param>
        /// <returns></returns>
        public string encriptarContrasenia(string claveSinEncriptar)
        {

            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(claveSinEncriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }


        /// <summary>
        /// Método para llamar al activarUsuario
        /// autor=Flor
        /// </summary>
        /// <param name="claveSinencriptar"></param>
        /// <returns></returns>
        public int activarUsuario(string codigo)
        {
            try
            {
                DAOUsuario gestorBD = new DAOUsuario();
                int idUsuario=gestorBD.ActivarCuenta(codigo);
                return idUsuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


       /// <summary>
       /// Metodo para obtener el usuario a partir del id
       /// </summary>
       /// <param name="idUsuario"></param>
       /// <returns>Usuario</returns>
        public Usuario obtenerUsuario(int idUsuario)
        {
            try
        {
            DAOUsuario gestorBD = new DAOUsuario();
                Usuario usuario= gestorBD.obtenerUsuarioPorId(idUsuario);
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


       /// <summary>
       /// Metodo para crear codigo de activación
       /// autor=Flor
       /// </summary>
       /// <param name="Largo de la clave"></param>
       /// <returns></returns>
        public string crearCodigo()
        {
            string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            Byte[] randomBytes = new Byte[60];
            char[] chars = new char[60];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < chars.Length; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        /// <summary>
        /// Metodo para validar el usuario
        /// autor=Facu
        /// </summary>
        /// <param name="Largo de la clave"></param>
        /// <returns></returns>
        public Usuario validarUsuario(string email, string clave)
        {
            clave=encriptarContrasenia(clave);
            DAOUsuario daoUsuario= new DAOUsuario();
            Usuario usuario= daoUsuario.obtenerUsuarioPorEmailyContrasenia(email, clave);
            if (usuario == null)
                throw new Exception("No existe un usuario con ese email y contraseña");
            if (usuario.esActivo == false)
                throw new Exception("Debes activar tu cuenta para poder ingresar: <a href='activar.usuario.aspx?idUsuario="+usuario.idUsuario+"'>Activar aquí</a>");
            return usuario;
        }

        /// <summary>
        /// Metodo para obtener los roles del suaruio
        /// autor=Facu
        /// </summary>
        /// <param name="Largo de la clave"></param>
        /// <returns></returns>
        public string[] obtenerRolesDelUsuario(string email)
        {
            DAOUsuario daoUsuario=new DAOUsuario();
            Usuario usuario = daoUsuario.obtenerUsuarioPorEmail(email);
            return new string[] { usuario.tipoUsuario.nombre };
        }


       /// <summary>
       /// autor=Flor
       /// metodo que trae un objeto usuario a partir de su e-mail
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public Usuario obtenerUsuario(string mail)
        {
            try
            {
                DAOUsuario gestorBD = new DAOUsuario();
                Usuario usuario = gestorBD.obtenerUsuarioPorEmail(mail);
                if(usuario==null)
                throw new Exception("No se encuentra registrado ningún usuario con ese e-mail.");

                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


       /// <summary>
       /// Metodo para generar codigo de recuperacicion cuando el usuario olvido su clave
       /// autor=Flor
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public string generarCodigoRecuperacion(string mail)
        {
            try
            {
                Usuario u = this.obtenerUsuario(mail);
                if (u.esActivo)
                {
                    u.codigoRecuperacion = this.crearCodigo();
                    DAOUsuario dUsuario = new DAOUsuario();
                    dUsuario.registrarCodigoRecuperacion(u);

                }
                else
                    throw new Exception("Debes activar tu cuenta para poder ingresar: <a href='activar.usuario.aspx?idUsuario=" + u.idUsuario + "'>Activar aquí</a>");

                return u.codigoRecuperacion;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }


        }


       /// <summary>
       /// Metodo para encriptar nueva clave y llamar al metodo apra guardarla en la bd
       /// autor=Flor
       /// </summary>
       /// <param name="codigo"></param>
       /// <param name="clave"></param>
       /// <returns></returns>
        public int reestablecerContrasenia(string codigo, string clave)
        {
            try
            {
                string claveEncriptada = encriptarContrasenia(clave);
                DAOUsuario gestorBD = new DAOUsuario();
                int idUsuario = gestorBD.RestablecerContrasenia(codigo,claveEncriptada);
                return idUsuario;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
