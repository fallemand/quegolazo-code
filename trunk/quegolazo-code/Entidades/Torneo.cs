using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool tieneImagen()
        {
            return GestorImagen.tieneImagen(this.idTorneo, GestorImagen.TORNEO, GestorImagen.GRANDE);
        }
        public string iniciales()
        {
            // first remove all: punctuation, separator chars, control chars, and numbers (unicode style regexes)
            string initials = Regex.Replace(this.nombre, @"[\p{P}\p{S}\p{C}\p{N}]+", "");

            // Replacing all possible whitespace/separator characters (unicode style), with a single, regular ascii space.
            initials = Regex.Replace(initials, @"\p{Z}+", " ");

            // Remove all Sr, Jr, I, II, III, IV, V, VI, VII, VIII, IX at the end of names
            initials = Regex.Replace(initials.Trim(), @"\s+(?:DE|LA|LAS|DEL|)$", "", RegexOptions.IgnoreCase);

            //Obtener la primera letra de cada palabra
            Regex regex = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
            initials = regex.Replace(initials, "$1");

            if (initials.Length > 3)
            {
                // Worst case scenario, everything failed, just grab the first two letters of what we have left.
                initials = initials.Substring(0, 3);
            }

            return initials.ToUpperInvariant();
        }
        public string lastNumber()
        {
            return (this.idTorneo % 10).ToString();
        }
    }
}
