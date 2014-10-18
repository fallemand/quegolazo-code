using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class Validador
    {
       /// <summary>
       /// Castea la cadena a un numero entero valido
       /// </summary>
       /// <param name="numero">cadena a castear</param>
       /// <returns>True si es un numero entero valido, false de lo contrario</returns>
        public static int castInt(string numero) {
            try
            {
                return int.Parse(numero);
            }
            catch (Exception)
            {
                throw new Exception("El valor ingresado no es un número");
            }
        }

        /// <summary>
        /// Castea la cadena a un char
        /// </summary>
        public static char castChar(string cadena)
        {
            try
            {
                return char.Parse(cadena);
            }
            catch (Exception)
            {
                throw new Exception("El valor ingresado no es un caracter");
            }
        }

        /// <summary>
        /// Castea la cadena a una fecha valida
        /// </summary>
        /// <param name="numero">cadena a castear</param>
        /// <returns>True si es un numero entero valido, false de lo contrario</returns>
        public static DateTime castDate(string fecha)
        {
            try
            {
                return DateTime.Parse(fecha);
            }
            catch (Exception)
            {
                throw new Exception("El valor ingresado no es una fecha");
            }
        }

        /// <summary>
        /// Castea la cadenaa a un numero entero valido
        /// </summary>
        /// <param name="numero">cadena a castear</param>
        /// <returns>True si es un numero entero valido, false de lo contrario</returns>
        public static decimal castDecimal(string numero)
        {
            try
            {
                return decimal.Parse(numero, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new Exception("El valor ingresado no es una número decimal");
            }
        }

       /// <summary>
       /// verifica si una cadena esta vacia. devuelvo la cadena sino está vacío
       /// </summary>
        public static string isNotEmpty(string cadena) {
            if (cadena.Equals(""))
                throw new Exception("El valor ingresado no puede estarvacío");
            else
                return cadena;
        }

        /// <summary>
        /// Valida si la direccion de email es valida devolviendo true en ese caso, y false en caso contrario
        /// </summary>
        public static bool validarEmail(string email) {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Valida si la cadena contiene espacios en blanco
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <returns>La cadena si no tiene espacios vacios, o lanza una excepción en caso que si</returns>
          public static string validarCadenaSinEspacios(string cadena) {

              if (cadena.Contains(" ") || cadena.Equals(string.Empty))
                  throw new Exception("El valor ingresado no puede contener espacios en blanco, ni estar vacío");
              else
                  return cadena;
          
          }
    }
}
