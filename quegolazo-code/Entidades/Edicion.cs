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
        public TipoSuperficie tipoSuperficie { get; set; }
       // public List<Fase> fases { get; set; }
        public Estado estado { get; set; }
        public List<Cancha> cancha { get; set; }
        public Torneo torneo { get; set; }
        public int puntosGanado { get; set; }
        public int puntosEmpatado { get; set; }
        public int puntosPerdido { get; set; }
        public ConfiguracionEdicion preferencias { get; set; }


        public Edicion()
        {
            preferencias = new ConfiguracionEdicion(); 
        }
    }
}
