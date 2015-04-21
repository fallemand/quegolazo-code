using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Jugador
    {
        public int idJugador { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public int? numeroCamiseta { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string sexo { get; set; }
        public bool tieneFichaMedica { get; set; }
        public int? cantidadGoles { get; set; }
        public int? cantidadAmarillas { get; set; }
        public int? cantidadRojas { get; set; }
        public int? PJ { get; set; }

        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idJugador, GestorImagen.JUGADOR, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idJugador, GestorImagen.JUGADOR, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idJugador, GestorImagen.JUGADOR, GestorImagen.GRANDE);
        }
        public bool tieneImagen()
        {
            return GestorImagen.tieneImagen(idJugador, GestorImagen.JUGADOR, GestorImagen.GRANDE);
        }
    }
}
