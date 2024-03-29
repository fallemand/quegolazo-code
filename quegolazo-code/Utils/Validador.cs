﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES"); //dd/MM/yyyy
                DateTime date;
                var formatStrings = new string[] { "MM/dd/yyyy hh:mm:ss tt", "M/d/yyyy h:m:ss tt", "M/d/yyyy hh:mm:ss tt", "MM/dd/yyyy h:mm:ss tt", "MM/dd/yyyy h:m:ss tt", "yyyy-MM-dd hh:mm:ss", "m/d/yyyy h:m:ss tt", "m/d/yyyy", "dd/MM/yyyy", "dd/MM/yyyy", "dd/MM/yyyy H:mm:ss", "dd/MM/yyyy" };
                if (!DateTime.TryParseExact(fecha, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                    throw new Exception("Error al parsear la fecha");
                }
                return date;
            }
            catch (Exception ex)
            {
                throw new Exception(fecha + ex.Message);
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
                throw new Exception("El valor ingresado no puede estar vacío");
            else
                return cadena;
        }

        public static int isNotEmptyAndCastInt(string cadena)
        {
            if (cadena.Equals(""))
                throw new Exception("El valor ingresado no puede estar vacío");
            else
            {
                try
                {
                    return int.Parse(cadena);
                }
                catch (Exception)
                {
                    throw new Exception("El valor ingresado no es un número");
                } 
            } 
        }

        public static DateTime isNotEmptyAndCastDate(string cadena)
        {
            if (cadena.Equals(""))
                throw new Exception("El valor ingresado no puede estar vacío");
            else
            {
                try
                {
                    return Utils.Validador.castDate(cadena);
                }
                catch (Exception)
                {
                    throw new Exception("El valor ingresado no es una fecha");
                }
            }
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
