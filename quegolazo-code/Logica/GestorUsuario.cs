using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Security.Cryptography;
using AccesoADatos;
using Logica;

namespace Logica
{
   public class GestorUsuario
    {
       public Usuario usuario;
       public string mailUsuario;

       public GestorUsuario()
       {
           usuario = new Usuario();
       }


       /// <summary>
       /// Método para tomar los datos de la pantalla y crear la entidad Usuario
       /// autor: Flor Rojas
       /// </summary>
       public string registrarUsuario(string apellido, string nombre, string mail, string contrasenia)
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

       /// <summary>
       /// Método para encriptar clave
       /// autor: Flor Rojas
       /// </summary>
       public string encriptarContrasenia(string claveSinEncriptar)
       {
           string result = string.Empty;
           byte[] encryted = System.Text.Encoding.Unicode.GetBytes(claveSinEncriptar);
           result = Convert.ToBase64String(encryted);
           return result;
       }

       /// <summary>
       /// Método para llamar al activarUsuario
       /// autor: Flor Rojas
       /// </summary>
       /// <param name="claveSinencriptar"></param>
       /// <returns></returns>
       public void activarUsuario(string codigo)
       {
            DAOUsuario gestorBD = new DAOUsuario();
            gestorBD.ActivarCuenta(codigo);
       }

       /// <summary>
       /// Metodo para obtener el usuario a partir del id
       /// autor: Flor Rojas
       /// </summary>
       /// <param name="idUsuario">Id de usuario a obtener</param>
       /// <returns>Objeto Usuario</returns>
       public Usuario obtenerUsuario(int idUsuario)
       {
            DAOUsuario gestorBD = new DAOUsuario();
            Usuario usuario = gestorBD.obtenerUsuarioPorId(idUsuario);
            return usuario;
       }

       /// <summary>
       /// Metodo para crear codigo de activación
       /// autor: Flor Rojas
       /// </summary>
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
       /// autor: Facu Allemand
       /// </summary>
       public Usuario validarUsuario(string email, string clave)
       {
           clave = encriptarContrasenia(clave);
           DAOUsuario daoUsuario = new DAOUsuario();
           Usuario usuario = daoUsuario.obtenerUsuarioPorEmailyContrasenia(email, clave);
           if (usuario == null)
               throw new Exception("No existe un usuario con ese email y contraseña");
           if (usuario.esActivo == false)
               throw new Exception("Debes activar tu cuenta para poder ingresar: <a href='activar.usuario.aspx?idUsuario=" + usuario.idUsuario + "'>Activar aquí</a>");
           return usuario;
       }

       /// <summary>
       /// Metodo para obtener los roles del suaruio
       /// autor: Facu Allemand
       /// </summary>
       /// <param name="Largo de la clave"></param>
       /// <returns></returns>
       public string[] obtenerRolesDelUsuario(string email)
       {
           DAOUsuario daoUsuario = new DAOUsuario();
           Usuario usuario = daoUsuario.obtenerUsuarioPorEmail(email);
           return new string[] { usuario.tipoUsuario.nombre };
       }

       /// <summary>
       /// Metodo que trae un objeto usuario a partir de su e-mail
       /// autor: Flor Rojas
       /// </summary>
       public Usuario obtenerUsuario(string mail)
       {
            DAOUsuario gestorBD = new DAOUsuario();
            Usuario usuario = gestorBD.obtenerUsuarioPorEmail(mail);
            if (usuario == null)
                throw new Exception("No existe ningún usuario con ese e-mail.");
            return usuario;
       }

       public Usuario obtenerUsuarioPorId(int idUsuario)
       {
           DAOUsuario gestorBD = new DAOUsuario();
           Usuario usuario = gestorBD.obtenerUsuarioPorId(idUsuario);
           if (usuario == null)
               throw new Exception("No se encuentra registrado ningún usuario.");
           return usuario;
       }

       /// <summary>
       /// Metodo para generar codigo de recuperacicion cuando el usuario olvido su clave
       /// autor: Flor Rojas
       /// </summary>
       public string generarCodigoRecuperacion(string mail)
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

       /// <summary>
       /// Metodo para encriptar nueva clave y llamar al metodo apra guardarla en la bd
       /// autor: Flor Rojas
       /// </summary>
       public int reestablecerContrasenia(string codigo, string clave)
       {
           try
           {
               string claveEncriptada = encriptarContrasenia(clave);
               DAOUsuario gestorBD = new DAOUsuario();
               int idUsuario = gestorBD.restablecerContrasenia(codigo, claveEncriptada);
               return idUsuario;
           }
           catch
           {
               throw new Exception("El código de Recuperación no es válido o ya fue utilizado.");
           }
       }
       /// <summary>
       /// Método para modificar datos del usuario
       /// autor: Flor Rojas
       /// </summary>
       public string modificarUsuario(string apellido, string nombre, string mail, string contrasenia , string contraseniaNueva)
       {
               usuario.idUsuario = Sesion.getUsuario().idUsuario;
               usuario.apellido = apellido;
               usuario.nombre = nombre;
               if (Sesion.getUsuario().email != mail)
               {
                   usuario.codigo = crearCodigo();
                   usuario.emailNuevo = mail;
               }
               validarContrasenia(contrasenia);
               if(contraseniaNueva != String.Empty)
               usuario.contrasenia= encriptarContrasenia(contraseniaNueva);
               else
               usuario.contrasenia = encriptarContrasenia(contrasenia);
               DAOUsuario gestorBD = new DAOUsuario();
               gestorBD.modificarUsuario(usuario);//guarda en la BD
               return usuario.codigo;  
       }

       private void validarContrasenia(string contrasenia)
       {
           try
           {
               if (Sesion.getUsuario().contrasenia != encriptarContrasenia(contrasenia))
                   throw new Exception();   
           }
           catch
           {
               throw new Exception("La contraseña ingresada es incorrecta. No se ha podido actualizar sus datos.");
           }
       }

   


    }
}
