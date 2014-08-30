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
    public class GestorEdicion
    {
        public Edicion edicion = new Edicion();
        /// <summary>
        /// Obtener ediciones de un torneo en particular
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Edicion> obtenerEdicionesPorTorneo(int idTorneo)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            List<Edicion> ediciones = daoEdicion.obtenerEdicionesPorIdTorneo(idTorneo);
            return ediciones;           
        }

        /// <summary> 
        /// Registra una nueva edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public void registrarEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.registrarEdicion(edicion, Sesion.getTorneo().idTorneo);
        }

        /// <summary> 
        /// Carga los datos en el objeto edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public void cargarDatos(string nombre, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string ptosPerdido, string idGeneroEdicion)
        {
            edicion.estado.idEstado = Estado.REGISTRADA;
            edicion.estado.ambito.idAmbito = Ambito.EDICION; 
            edicion.puntosGanado = Validador.castInt(ptosGanado);
            edicion.puntosPerdido = Validador.castInt(ptosPerdido);
            edicion.puntosEmpatado = Validador.castInt(ptosEmpatado);
            if ((edicion.puntosGanado < edicion.puntosEmpatado || edicion.puntosEmpatado < edicion.puntosPerdido) || (edicion.puntosGanado == edicion.puntosEmpatado))
                throw new Exception("Los puntos por ganar, empatar y perder son incorrectos.");
            edicion.nombre = Validador.isNotEmpty(nombre);
            edicion.tamanioCancha.idTamanioCancha = Validador.castInt(idTamanioCancha);
            edicion.tipoSuperficie.idTipoSuperficie = Validador.castInt(idSuperficie);
            edicion.generoEdicion.idGeneroEdicion = Validador.castInt(idGeneroEdicion);
        }     

        /// <summary>
        /// Registrar equipos para una edición
        /// </summary>
        /// <param name="idEquipo">Id del Equipo</param>
        /// <param name="idEdicion">Id de la Edición</param>
        public void registrarEquiposEnEdicion(List<Equipo> equipos, int idEdicion)
        {
            DAOEquipo daoEquipo = new DAOEquipo();
            daoEquipo.registrarEquiposEnEdicion(equipos, idEdicion);                
        }

        /// <summary>
        /// Agrega los equipos recibidos a la edición
        /// </summary>
        public void agregarEquiposEnEdicion(string equipos)
        {
            if (equipos == "")
                throw new Exception("No hay equipos seleccionados");
            //quita la última coma de la cadena
            string cadena = equipos.Substring(0, equipos.Length - 1);
            //transforma la cadena en una lista de enteros
            List<int> listaIdsSeleccionados = cadena.Split(',').Select(Int32.Parse).ToList();
            GestorEquipo gestorEquipo = new GestorEquipo();
            foreach (int id in listaIdsSeleccionados)
                edicion.equipos.Add(gestorEquipo.obtenerEquipoPorId(id));
        }

        /// <summary>
        /// Modifica de la BD una edición
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarEdicion(int idEdicion,string nombre, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string ptosPerdido, string idGeneroEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            edicion = daoEdicion.obtenerEdicionPorId(idEdicion);
            edicion.nombre = nombre;
            edicion.puntosGanado = Validador.castInt(ptosGanado);
            edicion.puntosPerdido = Validador.castInt(ptosPerdido);
            edicion.puntosEmpatado = Validador.castInt(ptosEmpatado);
            if ((edicion.puntosGanado < edicion.puntosEmpatado || edicion.puntosEmpatado < edicion.puntosPerdido) || (edicion.puntosGanado == edicion.puntosEmpatado))
                throw new Exception("Los puntos por ganar, empatar y perder son incorrectos.");
            edicion.nombre = Validador.isNotEmpty(nombre);
            edicion.tamanioCancha.idTamanioCancha = Validador.castInt(idTamanioCancha);
            edicion.tipoSuperficie.idTipoSuperficie = Validador.castInt(idSuperficie);
            edicion.generoEdicion.idGeneroEdicion = Validador.castInt(idGeneroEdicion);
            daoEdicion.modificarEdicion(edicion);
        }

        /// <summary>
        /// Obtiene una Edición por Id
        /// autor: Pau Pedrosa
        /// </summary>
        public Edicion obtenerEdicionPorId(int idEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            return daoEdicion.obtenerEdicionPorId(idEdicion);
        }

        /// <summary>
        /// Elimina una edición de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarEdicion(int idEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.eliminarEdicion(idEdicion, Estado.REGISTRADA);
        }

        /// <summary>
        /// Obtiene los Generos Edicion de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public List<GeneroEdicion> obtenerGenerosEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            //List<GeneroEdicion> generosEdicion = new List<GeneroEdicion>();
            //generosEdicion = daoEdicion.obtenerGenerosEdicion();
            return daoEdicion.obtenerGenerosEdicion();
        }

        /// <summary>
        /// Obtiene Genero Edicion por Id
        /// autor: Pau Pedrosa
        /// </summary>
        public GeneroEdicion obtenerGeneroEdicionPorId(int idGeneroEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            return daoEdicion.obtenerGeneroEdicionPorId(idGeneroEdicion);
        }
    }
}
