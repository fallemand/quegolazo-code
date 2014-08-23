using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Jugador
    {
        public int idJugador { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string sexo { get; set; }
        public bool tieneFichaMedica { get; set; }
    }
}
