using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConfiguracionEdicion
    {
        //Jugadores
        public bool jugadores { get; set; }
        public bool cambiosJugadores { get; set; }
        public bool golesJugadores { get; set; }
        public bool tarjetasJugadores { get; set; }
        public bool jugadoresXPartido { get; set; }

        //Arbitros
        public bool arbitros { get; set; }
        public bool asignaArbitros { get; set; }
        public bool desempenioArbitros { get; set; }

        //Sanciones
        public bool sanciones{ get; set; }
        public bool sancionesEquipos{ get; set; }
        public bool sancionesJugadores { get; set; }

        //Complejos
        public bool canchas { get; set; } 
        public bool canchaUnica { get; set; } 
    }

}
