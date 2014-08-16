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
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                List<Edicion> ediciones = daoEdicion.obtenerEdicionesPorIdTorneo(idTorneo);
                return ediciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }            
        }

        /// <summary> 
        /// Registra una nueva edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public void registrarEdicion()
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                daoEdicion.registrarEdicion(edicion, Sesion.getTorneo().idTorneo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            } 
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
            try
            {
                DAOEquipo daoEquipo = new DAOEquipo();
                daoEquipo.registrarEquiposEnEdicion(equipos, idEdicion);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
    }
}
