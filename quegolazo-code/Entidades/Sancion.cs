using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sancion
    {
        public int idSancion { get; set; }
        public int idEquipo { get; set; }
        public int? idJugador { get; set; }
        public int? idPartido { get; set; }
        public string motivo { get; set; }
    }
}
