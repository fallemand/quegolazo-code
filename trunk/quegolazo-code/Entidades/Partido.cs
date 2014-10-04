using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Partido
    {
        public Equipo local { get; set; }
        public Equipo visitante { get; set; }
        public Arbitro arbitro { get; set; }
        public Cancha cancha { get; set; }
        public DateTime fechaHora { get; set; }
        public List<Jugador> titularesLocal { get; set; } //listo
        public List<Jugador> titularesVisitante { get; set; } //listo
        public List<Gol> goles { get; set; } //listo
        public List<Tarjeta> tarjetas { get; set; } // listo
        public List<Cambio> cambios { get; set; } // listo
        public int idPartido { get; set; }
        public Estado estado { get; set; }
        public int golesLocal { get; set; }
        public int golesVisitante { get; set; } 

        public Partido()
        {
            local = new Equipo();
            visitante = new Equipo();
            estado = new Estado();
            titularesLocal = new List<Jugador>();
            goles = new List<Gol>();
            tarjetas = new List<Tarjeta>();
            cambios = new List<Cambio>();
        }
    }
}

 

