using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public class ValidacionDeTextos
    {
       /// <summary>
       /// Valida si una cadena es un numero entero valido
       /// </summary>
       /// <param name="numero">la cadena a validar</param>
       /// <returns>True si es un numero entero valido, false de lo contrario</returns>
        public bool validarNumeroEntero(string numero) {
            try
            {
                Int64.Parse(numero);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Valida si una cadena es un numero decimal valido
        /// </summary>
        /// <param name="numero">la cadena a validar</param>
        /// <returns>True si es un numero entero valido, false de lo contrario</returns>
        public bool validarNumeroDecimal(string numero) {
            try
            {
                Double.Parse(numero);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

         /// <summary>
       /// verifica si una cadena esta vacia. devuelve true si es asi.
       /// </summary>
          public bool estaVacio(string cadena) {
            return cadena == "";
        }

          /// <summary>
          /// Valida si la direccion de email es valida devolviendo true en ese caso, y false en caso contrario
          /// </summary>
          public bool validarEmail(string email) {
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
    }

}
