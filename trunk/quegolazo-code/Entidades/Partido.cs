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
        public Equipo visita { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public int idPartido { get; set; }
        public Estado estado { get; set; }

        public Partido()
        {
            local = new Equipo();
            visita = new Equipo();
            estado = new Estado();
        }


    }
}

 

