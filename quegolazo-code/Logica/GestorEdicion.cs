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
        public void cargarDatos(string nombre, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string ptosPerdido)
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

            foreach (int id in listaIdsSeleccionados)
                edicion.equipos.Add(new Equipo { idEquipo = id });
        }
    }
}
