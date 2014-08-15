using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Cancha
    {
        public int idCancha { get; set; }
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }

        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idCancha, GestorImagen.COMPLEJO, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idCancha, GestorImagen.COMPLEJO, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idCancha, GestorImagen.COMPLEJO, GestorImagen.GRANDE);
        }
    }
}
