using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fecha
    {
        public int idFecha { get; set; }
        public List<IPartido> partidos { get; set; }
        public int idGrupo { get; set; }
        public int idFase { get; set; }
        public int idEdicion { get; set; }
    }
}
