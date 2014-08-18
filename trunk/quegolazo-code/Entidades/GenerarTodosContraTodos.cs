using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class GenerarTodosContraTodos: IGenerarFixture
    {
        public List<Fecha> generarFixture(List<Equipo> equiposParticipantes)
        {
            //List<Fecha> listajornadas = new List<Fecha>();

            //for (int y = 1; y < equiposParticipantes.Count; y++) // for para Fechas
            //{
            //    Fecha fecha = new Fecha();

            //    fecha.idFecha = (y);

            //    for (int x = 1; x <= equiposParticipantes.Count / 2; y++)               // for para Partidos
            //    {
            //        PartidoComun partido = new PartidoComun();

            //        // Aquí como hago para añadir los enfrentamientos de acuerdo a las reglas que puse previamente?

            //        jornada.enfrentamientos.Add(enfrentamiento);
            //    }

            //    listajornadas.Add(jornada);
            //}
        }
    }
}
