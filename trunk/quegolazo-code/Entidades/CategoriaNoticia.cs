using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CategoriaNoticia
    {
        public int idCategoriaNoticia { get; set; }
        public string nombre { get; set; }

        public const int noticiaEVENTOS = 2;
        public const int noticiaBOLETIN = 1;
    }
}
