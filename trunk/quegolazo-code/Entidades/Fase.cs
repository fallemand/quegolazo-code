using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fase
    {
        public int idFase { get; set; }
        public int idEdicion { get; set; }
        public List<Grupo> grupos { get; set; }
        public TipoFixture tipoFixture { get; set; }
        public Estado estado { get; set; }

        public Fase()
        {
            tipoFixture = new TipoFixture();
            estado = new Estado();
        }
    }
}
