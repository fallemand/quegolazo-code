using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estado
    {
        public int idEstado { get; set; }
        public enumNombre nombre { get; set; }
        public enumAmbito ambito { get; set; }

        /// <summary>
        /// Enumerado de nombres de estado, que corresponde a los nombres que se encuentran en los registros de la tabla Estados en la base de datos
        /// </summary>
        enum enumNombre
        {
            
        }
        /// <summary>
        /// Enumerado de ámbitos de estado, que corresponde a los ambitos que se encuentran en los registros de la tabla Estados en la base de datos
        /// </summary>
        enum enumAmbito
        {
            PARTIDO, CAMPEONATO, FECHA, EDICION
        }
    }
}
