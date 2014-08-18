using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;

namespace Logica
{
  public  class GestorFase
    {
      public List<Equipo> equipos { get; set; }
      public Fase fase { get; set; }


      public List<Fecha> generarFixture(List<Equipo> EquiposDelGrupo, string tipoFixture)
      {
          return new List<Fecha>();
      }

      public void registrarFase( string tipoFixture)
      {
          fase= new Fase();
          fase.tipoFixture.nombre = tipoFixture;
          fase.grupos = new List<Grupo>();
      }


    }
}
