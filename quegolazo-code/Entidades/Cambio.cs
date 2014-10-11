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
        public Equipo equipo { get; set; }
        public Jugador jugadorEntra { get; set; }
        public Jugador jugadorSale { get; set; }
        public int? minuto { get; set; }

        public Cambio()
        {
            equipo = new Equipo();
            jugadorEntra = new Jugador();
            jugadorSale = new Jugador();
        }
    }
}
