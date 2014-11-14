using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoFixture
    {
        public string idTipoFixture { get; set; }
        public string nombre { get; set; }
        
        public TipoFixture(string id)
        {
            idTipoFixture = id;
            nombre= obtenerNombre(id);
        }
        public TipoFixture()
        {

        }
        /// <summary>
        /// Devuelve el nombre de un tipo de fixture basandose en su id
        /// </summary>
        private string obtenerNombre(string id)
{
    string nombre = "";
    switch (id)
    {
        case "TCT":
            nombre = "Todos contra todos";
            break;
        case "TCT-IV":
            nombre = "Todos contra todos, ida y vuelta";
            break;
        case "ELIM":
            nombre = "Eliminatorio";
            break;
        case "ELIM-IV":
            nombre = "Eliminatorio, ida y vuelta";
            break;
    }
    return nombre;
}

      
    }
}
