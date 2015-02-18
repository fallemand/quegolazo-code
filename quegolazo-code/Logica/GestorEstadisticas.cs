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
       Edicion edicion;
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

       public DataTable obtenerFixtureFecha(int idFecha)
       {
           return DaoEstadisticas.obtenerFixtureFecha(edicion.idEdicion, idFecha);
       }

       public DataTable obtenerFixtureUltimaFecha()
       {
           return DaoEstadisticas.obtenerFixtureUltimaFecha(edicion.idEdicion);
       }

       public DataTable obtenerAvanceFecha()
       {    
           return DaoEstadisticas.obtenerAvanceFecha(edicion.idEdicion);
       }

       public DataTable obtenerAvanceEdicion()
       {
           return DaoEstadisticas.obtenerAvanceEdicion(edicion.idEdicion);
       }

       public DataTable obtenerAvanceEdicion(int idEdicion)
       {
           return DaoEstadisticas.obtenerAvanceEdicion(idEdicion);
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
