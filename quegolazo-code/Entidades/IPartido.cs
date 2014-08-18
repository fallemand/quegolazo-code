using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public interface IPartido
    {
        Equipo local { get; set; }
        Equipo visita { get; set; }
        string fecha { get; set; }
        string hora { get; set; }

        public int idPartido { get; set; }
        public int idFecha { get; set; }
        public int idGrupo { get; set; }
        public int idFase { get; set; }
        public int idEdicion { get; set; }
    }
}
