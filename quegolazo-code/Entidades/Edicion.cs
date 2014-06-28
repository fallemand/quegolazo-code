using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Edicion
    {
        public int idEdicion { get; set; }
        public string nombre { get; set; }
        public TamanioCancha tamanioCancha { get; set; }
        public FormaPuntuacion formaPuntuacion { get; set; }
        public TipoSuperficie tipoSuperficie { get; set; }
       // public List<Fase> fases { get; set; }
        public Estado estado { get; set; }
        public List<Cancha> cancha { get; set; }
        public Torneo torneo { get; set; }
    }
}
