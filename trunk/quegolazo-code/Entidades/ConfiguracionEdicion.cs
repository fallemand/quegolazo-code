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
        public bool jugadoresCambios { get; set; }
        public bool jugadoresGoles { get; set; }
        public bool jugadoresTarjetas { get; set; }
        public bool jugadoresXPartido { get; set; }

        //Arbitros
        public bool arbitros { get; set; }
        public bool arbitrosAsignaXPartido { get; set; }
        public bool arbitrosRegistraDesempenio { get; set; }

        //Sanciones
        public bool sanciones{ get; set; }
        public bool sancionesEquipos{ get; set; }
        public bool sancionesJugadores { get; set; }

        //Complejos
        public bool canchas { get; set; } 
        public bool canchaJueganEnComplejo { get; set; } 
    }

}
