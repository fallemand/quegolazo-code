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

      public IGenerarFixture generadorFixture { get; set; }
      public Fase faseActual{ get; set; }
     
      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture(List<Fase> fases)
      {
          foreach (Fase f in fases)
          {
              if (f != null && f==fases[0])
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
                  if (f.tipoFixture.idTipoFixture == "ELIM" || f.tipoFixture.idTipoFixture == "ELIM-IV")
                      break;

                  int i = 1;
                  foreach (Grupo g in f.grupos)
                  {
                      g.idGrupo = i;
                      g.fechas = generadorFixture.generarFixture(g.equipos);
                      i++;
                  }
              }
          }

      }

      public GestorFase()
      {
          faseActual =  new Fase();
      }

      public void armarpija(List<Object> lista){
         
      }
      //public void registrarFase()
      //{
      //    DAOFase daoFase = new DAOFase();
      //    daoFase.registrarFase(fases);
      //}
    }
}
