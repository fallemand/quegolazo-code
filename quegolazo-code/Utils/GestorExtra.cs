using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class GestorExtra
    {
        public static string truncarString(string texto, int cantCaracteres)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }
            if (texto.Length > cantCaracteres)
            {
                return texto.Substring(0, cantCaracteres) + "..";
            }
            else
            {
                return texto;
            }
        }

        //Permite devolver el nombre del Mes en base al Número del Mes
        public static string nombreMes(int numeroMes)
        {
            System.Globalization.CultureInfo ci = null;
            System.Globalization.DateTimeFormatInfo dtfi = null;
            ci = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            dtfi = ci.DateTimeFormat;
            return dtfi.GetMonthName(numeroMes);
        }
        //Permite devolver el nombre del día de la semana
        public static string nombreDia(DateTime date)
        {
            System.Globalization.CultureInfo ci = null;
            ci = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            return date.ToString("dddd", ci).ToUpper();
        }
    }
}
