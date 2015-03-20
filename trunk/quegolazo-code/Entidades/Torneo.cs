using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Torneo
    {
        public int idTorneo { get; set; }
        public string nombre { get; set; }
        public string nick { get; set; }
        public List<Equipo> equipos { get; set; }
        public List<Edicion> ediciones { get; set; }
        public List<Cancha> canchas { get; set; } 
        public string descripcion { get; set; }
        public ConfiguracionVisual configuracionVisual {get;set;}
        
        public Torneo()
        {            
            equipos = new List<Equipo>();
            ediciones = new List<Edicion>();
            canchas = new List<Cancha>();
            configuracionVisual = new ConfiguracionVisual();
        }

        public string obtenerImagenChicha() {
            return GestorImagen.obtenerImagen(idTorneo, GestorImagen.TORNEO, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idTorneo, GestorImagen.TORNEO, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idTorneo, GestorImagen.TORNEO, GestorImagen.GRANDE);
        }
    }
}
