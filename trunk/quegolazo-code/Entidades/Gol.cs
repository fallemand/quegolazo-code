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
        public int? minuto { get; set; }
        public Jugador jugador { get; set; }
        public Equipo equipo { get; set; }
        public TipoGol tipoGol { get; set; }

        public Gol()
        {
            jugador = new Jugador();
            equipo = new Equipo();
            tipoGol = new TipoGol();
        }
    }
}
