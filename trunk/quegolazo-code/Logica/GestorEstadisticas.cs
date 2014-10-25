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
           if (edicion == null)
            edicion = buscarUltimaEdicionTorneo();
           Sesion.setEdicion(edicion);
       }

       public DataTable obtenerTablaPosiciones()
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion);
       }

       public DataTable obtenerTablaGoleadores()
       {
           return DaoEstadisticas.obtenerTablaGoleadores(edicion.idEdicion);
       }

       public DataTable obtenerFixtureFecha(int idFecha)
       {
           return DaoEstadisticas.obtenerFixtureFecha(edicion.idEdicion, idFecha);
       }

       public DataTable obtenerFixtureUltimaFecha(int idEstadoFecha)//8 o 9
       {
           return DaoEstadisticas.obtenerFixtureUltimaFecha(edicion.idEdicion,idEstadoFecha);
       }

       public DataTable obtenerAvanceFecha(int idEstadoFecha)//8 o 9
       {
           return DaoEstadisticas.obtenerAvanceFecha(edicion.idEdicion,idEstadoFecha);
       }

       public DataTable obtenerAvanceEdicion()
       {
           return DaoEstadisticas.obtenerAvanceEdicion(edicion.idEdicion);
       }

       private Edicion buscarUltimaEdicionTorneo()
       {
           DAOEdicion daoEdicion = new DAOEdicion();
           return daoEdicion.obtenerUltimaEdicionTorneo(Sesion.getTorneo().idTorneo);
       }

    }
}
