using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Equipo : ICloneable    
    {
        public int idEquipo { get; set; }
        public string nombre { get; set; }
        public string colorCamisetaPrimario { get; set; }
        public string colorCamisetaSecundario { get; set; }
        public string directorTecnico { get; set; }
        public List<Jugador> jugadores { get; set; } 
        public Delegado delegadoPrincipal { get; set; }
        public Delegado delegadoOpcional { get; set; }
       
      
        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.GRANDE);
        }
        public bool tieneImagen()
        {
            return GestorImagen.tieneImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.GRANDE);
        }
        public object Clone()
        {
            return (Equipo)this.MemberwiseClone();
        }
    }
}
