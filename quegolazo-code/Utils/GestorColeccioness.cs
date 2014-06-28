using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class GestorColecciones
    {
       
        /// <summary>
        /// Desordena los elementos de una lista generica aleatoriamente
        /// </summary>
        /// <typeparam name="T">Tipo de lista de entrada</typeparam>
        /// <param name="listaDeEntrada">La lista que se desea desordenar</param>
        /// <returns>Una lista del mismo tipo que la lista de entrada, desordenada aleatoriamente.</returns>
        public static List<T> desordenarLista<T>(List<T> listaDeEntrada)
        {
            List<T> listaOrdenada = listaDeEntrada;
            List<T> listaDesordenada = new List<T>();

            Random randNum = new Random();
            while (listaOrdenada.Count > 0)
            {
                int val = randNum.Next(listaOrdenada.Count);
                listaDesordenada.Add(listaOrdenada[val]);
                listaOrdenada.RemoveAt(val);
            }
            return listaDesordenada;

        }
    }
}
