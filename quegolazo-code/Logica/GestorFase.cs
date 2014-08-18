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
      public IGenerarFixture generadorFixture { get; set; }

      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture(string tipoFixture)
      {
          fase.tipoFixture.nombre = tipoFixture;

          if (fase.tipoFixture.nombre == "TODOS CONTRA TODOS")
              generadorFixture = new GenerarTodosContraTodos();
          
          foreach(Grupo g in fase.grupos)
          {
             g.fixture = generadorFixture.generarFixture(g.equipos);
          }

      }


      public void registrarFase()
      {
         
      }


    }
}
