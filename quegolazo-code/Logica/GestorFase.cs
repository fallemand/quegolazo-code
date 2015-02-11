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
      
     
      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture(List<Fase> fases)
      {
          foreach (Fase f in fases)
          {
              if (f != null)//s
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
          quitarFechasGenericas(fases);
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
                  //si es todos contra todos
                  if (fase.tipoFixture.idTipoFixture == "TCT")
                      grupo.fechas = null;
                  else
                  {
                      foreach (Fecha fecha in grupo.fechas)
                      {
                          foreach (Partido p in fecha.partidos)
                          {
                              if (p.local.jugadores != null && p.visitante.jugadores != null)
                              {
                                  p.local.jugadores = null;
                                  p.visitante.jugadores = null;
                              }
                          }
                      }
                  }
              }
          }
      }

      //public void registrarFase()
      //{
      //    DAOFase daoFase = new DAOFase();
      //    daoFase.registrarFase(fases);
      //}
      public void cerrarFase(Fase fase)
      {
         fase.estado.idEstado = Estado.faseFINALIZADA;         
         new  DAOFase().cerrarFase(fase.idFase, fase.idEdicion);
      }

       //autor:Florencia Rojas
       //Este metodo crea las fecha sy partidos que se necesitan para un torneo eliminatorio y los guarda en la bd
      public void crearPartidosSiguientes(Fase fase)
      {
          //seteo el estado de la fecha 1 en diagramada
          fase.grupos[0].fechas[0].estado.idEstado = Estado.fechaDIAGRAMADA;
          //Calcular cantidad de partidos a crear en la siguiente fecha
          int cantidad = fase.grupos[0].fechas[0].partidos.Count() /2;
          int nroFecha = fase.grupos[0].fechas[0].idFecha + 1;          
          //carga todas las rondas excepto la final y el partido opr tercer puesto
          while (cantidad % 2 == 0)
          {
              Fecha fecha = new Fecha();
              fecha.idFecha = nroFecha;
              fecha.estado.idEstado = (nroFecha > 1) ? Estado.fechaREGISTRADA : Estado.fechaDIAGRAMADA;
              for (int i = 0; i < cantidad; i++)
              {
                  Partido p = new Partido();
                  fecha.partidos.Add(p);
              }
              nroFecha++;
              fase.grupos[0].fechas.Add(fecha);
              cantidad = cantidad / 2;
          } 
          //ahora carga el partido de la final y del tercer puesto, primero la final y luego el tercer puesto
          for (int i = 0; i < 2; i++)
              {
                  Fecha fecha = new Fecha();
                  fecha.idFecha = nroFecha;
                  fecha.estado.idEstado = (nroFecha > 1) ? Estado.fechaREGISTRADA : Estado.fechaDIAGRAMADA;
                  Partido p = new Partido();
                  p.estado.idEstado = Estado.partidoDIAGRAMADO;
                  fecha.partidos.Add(p);
                  fase.grupos[0].fechas.Add(fecha);
                  nroFecha++;
              }
              
          
      }
      /// <summary>
      /// quita las fechas que tienen estado REGISTRADA, que corresponden a las fechas genericas de una fase eliminatoria.
      /// </summary>
      /// <param name="fases">La lista de fases de una edicion.</param>
      public void quitarFechasGenericas(List<Fase> fases)
      {
          List<Fecha> fechasAEliminar =  new List<Fecha>();
          foreach (Fase fase in fases)
          {
             if(fase.grupos.Count >0)
                 foreach (Fecha fecha in fase.grupos[0].fechas)
                 {
                     if (fecha.estado.idEstado == Estado.fechaREGISTRADA)
                         fechasAEliminar.Add(fecha);
                 }
             foreach (Fecha fechaEliminar in fechasAEliminar)
             {
                 fase.grupos[0].fechas.Remove(fechaEliminar);
             }
          }
         
      }
    
    }
}
