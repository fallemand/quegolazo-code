using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Grupo
    {
        public int idGrupo { get; set; }
        public int idFase { get; set; }
        public int idEdicion { get; set; }
        public List<Equipo> equipos { get; set; }
        public List<Fecha> fixture {get; set;}
        public int nombre { get; set; }
        public Grupo()
        {
            equipos = new List<Equipo>();
            fixture = new List<Fecha>();
        }
    }
}
