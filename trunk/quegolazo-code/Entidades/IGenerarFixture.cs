using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public interface IGenerarFixture
    {
       List<Fecha> generarFixture(List<Equipo> equiposParticipantes);

       int getCantidadRondas();

       void setCantidadRondas(int cantidadRondas);
    }
}
