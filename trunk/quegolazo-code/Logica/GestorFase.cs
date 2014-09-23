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
  public class GestorFase
    {
      public List<Equipo> equipos { get; set; }
      public Fase faseActual { get; set; }
      public List<Fase> fases { get; set; }
      public IGenerarFixture generadorFixture { get; set; }

     
      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture()
      {
          foreach (Fase f in fases)
          {
              if (f != null)
              {
                  if (f.tipoFixture.idTipoFixture == "TCT")//Todos contra todos ida
                  {
                      generadorFixture = new GenerarTodosContraTodos();
                      generadorFixture.setCantidadRondas(1);
                  }
                  if (f.tipoFixture.idTipoFixture == "TCT-IV")//Todos contra todos ida y vuelta
                  {
                      generadorFixture = new GenerarTodosContraTodos();
                      generadorFixture.setCantidadRondas(2);
                  }

                  int i = 1;
                  foreach (Grupo g in f.grupos)
                  {
                      g.idGrupo = i;
                      g.fixture = generadorFixture.generarFixture(g.equipos);
                      i++;
                  }
              }
          }

      }

      public GestorFase()
      {
          equipos = new List<Equipo>();
          faseActual = new Fase();
          fases = new List<Fase>();
      }

      public void registrarFase()
      {
          DAOFase daoFase = new DAOFase();
          daoFase.registrarFase(fases);
      }

      


    }
}
