using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Tarjeta
    {
        public int idTarjeta { get; set; }
        public int idEquipo { get; set; }
        public int idJugador { get; set; }
        public char tipoTarjeta { get; set; }
        public int? minuto { get; set; }
    }
}
