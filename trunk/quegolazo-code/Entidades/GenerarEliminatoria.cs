using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    class GenerarEliminatoria : IGenerarFixture
    {

        public List<Fecha> generarFixture(List<Equipo> equiposParticipantes)
        {
            List<Fecha> fechas = new List<Fecha>();
            
            return fechas;
        }

        public int getCantidadRondas()
        {
            throw new NotImplementedException();
        }

        public void setCantidadRondas(int cantidadRondas)
        {
            throw new NotImplementedException();
        }
    }
}
