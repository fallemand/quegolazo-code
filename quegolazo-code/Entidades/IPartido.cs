using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public interface IPartido
    {
        Equipo local { get; set; }
        Equipo visita { get; set; }
        string fecha { get; set; }
        string hora { get; set; }

       //int idPartido { get; set; }
       //int idFecha { get; set; }
       //int idGrupo { get; set; }
       //int idFase { get; set; }
       //int idEdicion { get; set; }
    }
}
