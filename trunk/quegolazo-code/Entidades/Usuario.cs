using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string contrasenia { get; set; }
        public bool esActivo { get; set; }
        public string codigo { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
    }
}
