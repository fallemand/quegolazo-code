using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Arbitro
    {
        public int idArbitro { get; set; }
        public string nombre { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string matricula { get; set; }

        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idArbitro, GestorImagen.ARBITRO, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idArbitro, GestorImagen.ARBITRO, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idArbitro, GestorImagen.ARBITRO, GestorImagen.GRANDE);
        }
    }
}
