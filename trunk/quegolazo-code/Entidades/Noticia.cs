using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Noticia
    {
        public int idNoticia { get; set; }
        public string titulo { get; set; }
        public string tipoNoticia { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public int idEdicion { get; set; }
    }
}
