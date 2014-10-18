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
           edicion = Sesion.getEdicion();
       }

       public DataTable obtenerTablaPosiciones()
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion);
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

    }
}
