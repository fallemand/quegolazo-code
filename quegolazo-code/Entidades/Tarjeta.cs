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
        public Equipo equipo { get; set; }
        public Jugador jugador { get; set; }
        public char tipoTarjeta { get; set; }
        public int? minuto { get; set; }

        public Tarjeta()
        {
            jugador = new Jugador();
            equipo = new Equipo();
        }
    }
}
