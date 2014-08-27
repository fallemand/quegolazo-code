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
      public void generarFixture(string tipoFixture)
      {
          faseActual.tipoFixture.nombre = tipoFixture;

          if (faseActual.tipoFixture.nombre == "TODOS CONTRA TODOS")
              generadorFixture = new GenerarTodosContraTodos();

          int i = 1;
          foreach(Grupo g in faseActual.grupos)       
          {
             g.idGrupo = i;
             g.fixture = generadorFixture.generarFixture(g.equipos);
             i++;
          }

          registrarFase();

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
          daoFase.registrarFase(this.faseActual);
      }


    }
}
