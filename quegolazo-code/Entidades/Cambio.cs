using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cambio
    {
        public int idCambio { get; set; }
        public int idEquipo { get; set; }
        public int idJugadorEntra { get; set; }
        public int idJugadorSale { get; set; }
        public int? minuto { get; set; }
    }
}
