using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Data;

namespace Logica
{
   public  class GestorEstadisticas
    {
       //DAOEstadisticas
       public Edicion edicion;
       DAOEstadisticas DaoEstadisticas = new DAOEstadisticas();

       public GestorEstadisticas()
       {
           edicion = Sesion.getGestorEdicion().edicion;
           if(edicion.idEdicion==0)
           edicion = buscarUltimaEdicionTorneo();
           Sesion.setEdicion(edicion);
       }

       public DataTable obtenerTablaPosiciones(int idFase, int idGrupo)
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion, idFase, idGrupo);
       }

       public DataTable obtenerTablaPosiciones(int idFase)
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion, idFase);
       }

       public DataTable obtenerTablaGoleadores()
       {
           return DaoEstadisticas.obtenerTablaGoleadores(edicion.idEdicion);
       }

       public DataTable obtenerTablaTarjetas()
       {
           return DaoEstadisticas.obtenerTablaTarjetas(edicion.idEdicion);
       }

       public DataTable obtenerFixtureFecha(int idFecha)
       {
           return DaoEstadisticas.obtenerFixtureFecha(edicion.idEdicion, idFecha);
       }

       public DataTable obtenerFixtureUltimaFecha(int idFase)
       {
           return DaoEstadisticas.obtenerFixtureUltimaFecha(edicion.idEdicion, idFase);
       }

       public DataTable obtenerAvanceFecha(int idFase)
       {    
           return DaoEstadisticas.obtenerAvanceFecha(edicion.idEdicion, idFase);
       }

       public DataTable obtenerAvanceEdicion()
       {
           return DaoEstadisticas.obtenerAvanceEdicion(edicion.idEdicion);
       }

       public DataTable obtenerAvanceEdicion(int idEdicion)
       {
           return DaoEstadisticas.obtenerAvanceEdicion(idEdicion);
       }

       public DataTable obtenerPartidosPorArbitro(int idEdicion)
       {
           return DaoEstadisticas.obtenerPartidosPorArbitro(idEdicion);
       }

       public DataTable obtenerEstadisticasEquipo(int idEquipo)
       {
           return DaoEstadisticas.estadisticasDeUnEquipo(idEquipo, edicion.idEdicion);
       }

       public DataTable obtenerUltimosPartidosEquipo(int idEquipo)
       {
           return DaoEstadisticas.ultimosPartidosDeUnEquipo(idEquipo, edicion.idEdicion);
       }

       public DataTable obtenerGoleadoresDeUnEquipo(int idEquipo)
       {
           return DaoEstadisticas.obtenerGoleadorEquipo(idEquipo, edicion.idEdicion);
       }

       public void guardarTablaPosicionesFinal(List<Int64> idEquipos, int idEdicion)
       {
           List<Equipo> equipos = new List<Equipo>();
           for (int i = 0; i < idEquipos.Count; i++)
           {
               Equipo equipo = new Equipo();
               equipo.idEquipo = int.Parse(idEquipos[i].ToString());
               equipos.Add(equipo);
           }
           DaoEstadisticas.guardarTablaPosiciones(equipos, idEdicion);
       }

       private Edicion buscarUltimaEdicionTorneo()
       {
           DAOEdicion daoEdicion = new DAOEdicion();
           return daoEdicion.obtenerUltimaEdicionTorneo(Sesion.getTorneo().idTorneo);
       }

    }
}
