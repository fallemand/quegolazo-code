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

      public GestorFase()
      {
          fase = new Fase();
          fase.idFase = 1;
          fase.idEdicion = 14;
          fase.grupos = new List<Grupo>();
          fase.estado = new Estado { idEstado = Estado.CREADA, ambito = new Ambito() {idAmbito=Ambito.FASE, } };

          Grupo g = new Grupo();
          g.equipos = new List<Equipo>();
          g.equipos.Add(new Equipo { idEquipo = 1 });
          g.equipos.Add(new Equipo { idEquipo = 2 });
          fase.grupos.Add(g);

          generarFixture("TODOS CONTRA TODOS");
      }

      /// <summary>
      /// método para generar fixture
      /// autor=Flor
      /// </summary>
      public void generarFixture(string tipoFixture)
      {
          //fase.idEdicion = Sesion.getGestorEdicion().edicion.idEdicion;

          fase.tipoFixture.nombre = tipoFixture;

          if (fase.tipoFixture.nombre == "TODOS CONTRA TODOS")
              generadorFixture = new GenerarTodosContraTodos();

          int i = 1;
          foreach(Grupo g in fase.grupos)
          {
             g.idGrupo = i;
             g.fixture = generadorFixture.generarFixture(g.equipos);
             i++;
          }

          registrarFase();

      }


      public void registrarFase()
      {
          DAOFase daoFase = new DAOFase();
          daoFase.registrarFase(this.fase);
      }


    }
}
