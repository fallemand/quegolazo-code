using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public string iniciales()
        {
            // first remove all: punctuation, separator chars, control chars, and numbers (unicode style regexes)
            string initials = Regex.Replace(this.nombre, @"[\p{P}\p{S}\p{C}\p{N}]+", "");

            // Replacing all possible whitespace/separator characters (unicode style), with a single, regular ascii space.
            initials = Regex.Replace(initials, @"\p{Z}+", " ");

            // Remove all Sr, Jr, I, II, III, IV, V, VI, VII, VIII, IX at the end of names
            initials = Regex.Replace(initials.Trim(), @"\s+(?:DE|LA|LAS|DEL|)$", "", RegexOptions.IgnoreCase);

            // Extract up to 2 initials from the remaining cleaned name.
            initials = Regex.Replace(initials, @"^(\p{L})[^\s]*(?:\s+(?:\p{L}+\s+(?=\p{L}))?(?:(\p{L})\p{L}*)?)?$", "$1$2").Trim();

            if (initials.Length > 3)
            {
                // Worst case scenario, everything failed, just grab the first two letters of what we have left.
                initials = initials.Substring(0, 3);
            }

            return initials.ToUpperInvariant();
        }
        public string lastNumber()
        {
            return (this.idJugador % 10).ToString();
        }
    }
}
