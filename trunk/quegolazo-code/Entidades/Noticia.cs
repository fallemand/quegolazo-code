using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Noticia
    {
        public int idNoticia { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int idEdicion { get; set; }
        public DateTime fecha { get; set; }
        public CategoriaNoticia categoria { get; set; }

        public Noticia()
        {
            categoria = new CategoriaNoticia();
        }

        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idNoticia, GestorImagen.NOTICIA, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idNoticia, GestorImagen.NOTICIA, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idNoticia, GestorImagen.NOTICIA, GestorImagen.GRANDE);
        }
    }
}
