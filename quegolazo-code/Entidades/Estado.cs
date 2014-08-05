using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estado
    {
        public int idEstado { get; set; }
        public string nombre { get; set; }
        public Ambito ambito { get; set; }

        public Estado()
        {
            ambito = new Ambito();
        }

        //Estados Torneo
        public const int REGISTRADO = 1;

        //Estados Edicion
        public const int REGISTRADA = 2;
    }
    /// <summary>
    /// Struct que define los ambitos.
    /// autor: Facundo Allemand
    /// </summary>
    public class Ambito
    {
        public int idAmbito;
        public string nombre;

        //Ambitos
        public const int TORNEO = 1;
        public const int EDICION = 2;
        public const int FECHA = 3;
        public const int PARTIDO = 4;
    }
}
