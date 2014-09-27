using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Gol
    {
        public int idGol { get; set; }
        public int minuto { get; set; }
        public int? idJugador { get; set; }
        public int? idEquipo { get; set; }
        public TipoGol tipoGol { get; set; }
    }
}
