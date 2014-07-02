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
    }
}
