using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
  public  class GestorFixture
    {
      public List<Equipo> equipos { get; set; }

      public List<Fecha> generarFixture(List<Equipo> EquiposDelGrupo)
      {
          return new List<Fecha>();
      }
    }
}
