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
              if (f != null && f==fases[0])//s
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

      public bool estaFinalizada(int idFase, int idEdicion)
      {
        return  (new DAOFase()).finalizoFase(idFase,idEdicion);
      }

      /// <summary>
      /// Obtiene la minima informacion necesaria de las fases. Sin tener en cuenta el detalle de todos los objetos que la conforman.
      /// </summary>
      public List<Fase> obtenerFasesReducidas(int idEdicion) {
          return new DAOFase().obtenerFasesReducidas(idEdicion);
      }

      /// <summary>
      ///Elimina la configuracion de fases que tenia el usuario guardada en sesión y en la base de datos
      /// </summary>
      public void eliminarConfiguracionGuardada(List<Fase> fases)
      {
          Sesion.getGestorEdicion().edicion.fases = new List<Fase>();
          new DAOFase().eliminarFases(fases[0].idEdicion);
      }

      /// <summary>
      /// Reduce las fases para que sean procesadas en el editor (no necesita los datos detallados de equipos)
      /// </summary>
      public void reducirFases(List<Fase> fases)
      {
          foreach (Fase fase in fases)
          {
              foreach (Equipo equipo in fase.equipos)
              {
                  equipo.jugadores = null;
              }
              foreach (Grupo grupo in fase.grupos)
              {
                  foreach (Equipo equipo in grupo.equipos)
                  {
                      equipo.jugadores = null;   
                  }
              }
          }
      }

      //public void registrarFase()
      //{
      //    DAOFase daoFase = new DAOFase();
      //    daoFase.registrarFase(fases);
      //}
      public void cerrarFase(List<Fase> fases)
      {
          DAOFase daoFase = new DAOFase();
          foreach(Fase fase in fases)
          {
              if (fase.idFase == faseActual.idFase)
                  fase.estado.idEstado = Estado.faseCERRADA;
          }
          daoFase.cerrarFase(faseActual.idFase, faseActual.idEdicion);
      }
    }
}
