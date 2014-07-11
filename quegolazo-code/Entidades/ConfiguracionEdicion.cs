using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConfiguracionEdicion
    {
        public bool jugadores { get; set; }
        public bool cambiosJugadores { get; set; }
        public bool golesJugadores { get; set; }
        public bool tarjetasJugadores { get; set; }
        public bool arbitros { get; set; }
        public bool asignaArbitros { get; set; }
        public bool desempenioArbitros { get; set; }
        public int cantidadArbitros { get; set; }
        public bool sanciones{ get; set; }
        public bool sancionesJugadores { get; set; }
        public bool canchaUnica { get; set; }



       
    }
}
