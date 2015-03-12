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
        public DateTime? fecha { get; set; }
        public List<Jugador> titularesLocal { get; set; } 
        public List<Jugador> titularesVisitante { get; set; } 
        public List<Gol> goles { get; set; } 
        public List<Tarjeta> tarjetas { get; set; } 
        public List<Cambio> cambios { get; set; } 
        public int idPartido { get; set; }
        public Estado estado { get; set; }
        public int? golesLocal { get; set; }
        public int? golesVisitante { get; set; }
        public bool? huboPenales { get; set; }
        public int? penalesLocal { get; set; }
        public int? penalesVisitante { get; set; }
        public int? idGanador { get; set; }
        public int? idPerdedor { get; set; }
        public bool? empate { get; set; }
        public string nombreCompleto { get; set; }
        public int idPartidoPosterior { get; set; }
        public Fase faseAsociada { get; set; }
        public int? idGrupo { get; set; }

        public Partido()
        {
            local = new Equipo();
            visitante = new Equipo();
            estado = new Estado();
            titularesLocal = new List<Jugador>();
            titularesVisitante = new List<Jugador>();
            goles = new List<Gol>();
            tarjetas = new List<Tarjeta>();
            cambios = new List<Cambio>();
            arbitro = new Arbitro();
            cancha = new Cancha();
        }
    }
}

 

