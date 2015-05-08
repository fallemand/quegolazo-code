using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{
    public class Edicion
    {
        public int idEdicion { get; set; }
        public string nombre { get; set; }
        public TamanioCancha tamanioCancha { get; set; }       
        public TipoSuperficie tipoSuperficie { get; set; }
        public List<Fase> fases { get; set; }
        public Estado estado { get; set; }
        public List<Cancha> cancha { get; set; }        
        public int puntosGanado { get; set; }
        public int puntosEmpatado { get; set; }
        public int puntosPerdido { get; set; }
        public GeneroEdicion generoEdicion { get; set; }
        public ConfiguracionEdicion preferencias { get; set; }
        public List<Equipo> equipos { get; set; }

        public Edicion()
        {
            preferencias = new ConfiguracionEdicion();
            tamanioCancha = new TamanioCancha();
            tipoSuperficie = new TipoSuperficie();
            generoEdicion = new GeneroEdicion();
            fases = new List<Fase>();
            estado = new Estado();
            equipos = new List<Equipo>();
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
            return (this.idEdicion % 10).ToString();
        }
    }
}
