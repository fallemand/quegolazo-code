using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace Logica
{
  public class GestorFase
    {
      /// <summary>
      /// Objeto auxiliar creado para hacer mas entendible la creacion del widget de fases. Se usa para la serializacion de los datos.
      /// </summary>
      private class GeneradorDeFases { 
          public List<Fase> fases {get;set;}
          public int idEdicion {get;set;}
          public List<Equipo> equiposDeLaEdicion {get;set;} 
          public int idFaseEditable {get;set;}
          public bool asistente { get; set; }          
      }
      private class GeneradorDeLlaves
      {
          //si bien dice equipos, se debe pasar un conjunto de Partidos porque de ahi obtiene los equipos que se renderizarán en la llave
          public List<Partido> equipos { get; set; }
          public int numFase { get; set; }         
          public bool generica { get; set; }
          public bool mezclar { get; set; }
          public bool noSwap { get; set; }
          public bool scroll { get; set; }
          //la primera lista es una llave, la segunda una ronda, la tercera los resultados de esa ronda
          public List<List<List<int[]>>> resultados { get; set; }
          public GeneradorDeLlaves() {
              resultados = new List<List<List<int[]>>>();
              generica = false;
              mezclar = false;
              noSwap = true;
          }
      }
      public IGenerarFixture generadorFixture { get; set; }
      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture(List<Fase> fases)
      {
          foreach (Fase f in fases)
          {
              generarFixtureDeUnaFase(f);
          }
      }

      /// <summary>
      /// Genera el fixture si la fase es todos contra todos.
      /// </summary>
      /// <param name="f">La fase a la cual se le va a generar el fixture.</param>
      public void generarFixtureDeUnaFase(Fase f)
      {
          if (f != null && !f.esGenerica)//s
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
              if (f.tipoFixture.idTipoFixture == "ELIM")
                  crearPartidosSiguientesDeUnaEliminatoria(f);
              //if(f.tipoFixture.idTipoFixture == "ELIM-IV")
              //    crearPartidosSiguientes(f);

              int i = 1;
              if(f.tipoFixture.idTipoFixture.Contains("TCT"))
              foreach (Grupo g in f.grupos)
              {
                  g.idGrupo = i;
                  g.fechas = generadorFixture.generarFixture(g.equipos);
                  i++;
              }
          }
      }


      public bool estaFinalizada(int idFase, int idEdicion)
      {
        return  (new DAOFase()).finalizoFase(idFase,idEdicion);
      }

      /// <summary>
      /// Verifica si la fase generada por el usuario tiene la misma (o menor) cantidad de equipos que la que habia generado anteriormente, devuelve True si la fase generada es correcta, false si la cantidad de equipos que selecciono es menor a la anterior
      /// </summary>
      /// <param name="cantidadEquipos">La cantidad de equipos que seleccionó el usuario</param>
      /// <param name="idFase">el numero de fase a verificar</param>
      /// <param name="idFase">Las fases en donde se va a realizar la validacion</param>
      /// <returns>True si la fase generada es correcta, false si la cantidad de equipos que selecciono es menor a la anterior</returns>
      public bool validarCantidadEquipos(string listadoEquipos, int idFase, List<Fase> fases)
      {
          string[] listaIdsSeleccionados = listadoEquipos.Split(',');
          int cantidadSeleccionados = listaIdsSeleccionados.Length -1;
          if (cantidadSeleccionados < 2)
              throw new Exception("MenosDeDosEquipos");
          return fases[idFase - 1].cantidadDeEquipos <= cantidadSeleccionados; //-1 porque cuenta la ultima coma
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
          new DAOFase().eliminarFases(Sesion.getGestorEdicion().edicion.idEdicion);
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
                              if (p.local != null && p.visitante != null && p.local.jugadores != null && p.visitante.jugadores != null)
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


      /// <summary>
      /// Elimina las fases posteriores a la actual, es decir las genericas, para que el usuario genere una nueva fase.
      /// </summary>
      /// <param name="fases">La lista de fases a limpiar</param>
      /// <param name="faseActual">La fase que se va a editar</param>
      public void eliminarFasesPosteriores(List<Fase> fases, Fase faseActual)
      {
          List<Fase> fasesAEliminar = new List<Fase>();
          foreach (Fase fase in fases)
          {
              if (fase.idFase > faseActual.idFase)
                  fasesAEliminar.Add(fase);
          }
          foreach (Fase faseElim in fasesAEliminar)
          {
              fases.Remove(faseElim);
          }
      }

      
      /// <summary>
      /// Este metodo crea las fecha sy partidos que se necesitan para un torneo eliminatorio
      /// </summary>
      /// <param name="fase">La fase donde se van a crear los partidos siguientes</param>
      public void crearPartidosSiguientesDeUnaEliminatoria(Fase fase)
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
          Fecha ultimaFecha = new Fecha();
          ultimaFecha.idFecha = nroFecha;
          ultimaFecha.estado.idEstado = (nroFecha > 1) ? Estado.fechaREGISTRADA : Estado.fechaDIAGRAMADA;
          Partido partidoFinal = new Partido();
          partidoFinal.estado.idEstado = Estado.partidoDIAGRAMADO;
          ultimaFecha.partidos.Add(partidoFinal);
          Partido partidoTercerPuesto = new Partido();
          partidoTercerPuesto.estado.idEstado = Estado.partidoDIAGRAMADO;
          ultimaFecha.partidos.Add(partidoTercerPuesto);
          fase.grupos[0].fechas.Add(ultimaFecha);

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
              if (!fase.esGenerica && fase.grupos.Count > 0 && fase.grupos[0] != null && fase.grupos[0].fechas != null)
              {
           
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
     
          /// <summary>
      /// Crea un objeto JSON para enviar como respuesta al generador de Fases.
      /// </summary>   
      public string armarJsonParaWidget(List<Fase> fases, int idEdicion, List<Equipo> equiposDeLaEdicion, int idFaseEditable , bool asistente)
      {
         GeneradorDeFases generador = new GeneradorDeFases();         
         generador.equiposDeLaEdicion = equiposDeLaEdicion;
         generador.fases= fases;
         generador.idEdicion= idEdicion;
         generador.idFaseEditable = idFaseEditable;
         generador.asistente = asistente;        
         JavaScriptSerializer s = new JavaScriptSerializer();
         return s.Serialize(generador);
         
      }
      /// <summary>
      /// Crea un string serializado que va como entrada al widget de llaves, para mostrar los partidos y resultados de una fase eliminatoria
      /// </summary>
      public string armarLlavesDeUnaFase(Fase faseEliminatoria) {
          string res = string.Empty;
          GeneradorDeLlaves llaves = new GeneradorDeLlaves();
          llaves.equipos = faseEliminatoria.grupos[0].fechas[0].partidos;
          List<List<int[]>> resultadosParaWidget = new List<List<int[]>>();
          foreach (Fecha fecha  in faseEliminatoria.grupos[0].fechas)
          {            
            List<int[]> resultados = new List<int[]>();
            foreach (Partido partido in fecha.partidos)
            {
                int[] resultado = null;
                if(partido.estado.idEstado== Estado.partidoJUGADO)
                {
                    resultado = new int[2]; 
                    resultado[0] = (int)partido.golesLocal;
                    resultado[1] = (int)partido.golesVisitante;
                    resultados.Add(resultado);
                }                
            }
            resultadosParaWidget.Add(resultados);  
          }
          llaves.resultados.Add(resultadosParaWidget);
          llaves.numFase = faseEliminatoria.idFase;
          JavaScriptSerializer s = new JavaScriptSerializer();
          return s.Serialize(llaves);
      }
  
  
  }  

     
  }
         
  
      

 