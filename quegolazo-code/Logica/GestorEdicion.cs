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
        public GestorFase gestorFase = new GestorFase();
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
        /// Registra una nueva edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        //public void registrarPreferencias()
        //{
        //    DAOEdicion daoEdicion = new DAOEdicion();
        //    daoEdicion.registrarPreferencias(edicion);
        //}
        
        /// <summary> 
        /// Carga los datos en el objeto edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public void cargarDatos(string nombre, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string ptosPerdido, string idGeneroEdicion)
        {
            edicion.estado.idEstado = Estado.edicionREGISTRADA;
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
        //public void registrarEquiposEnEdicion()
        //{
        //    DAOEquipo daoEquipo = new DAOEquipo();
        //    daoEquipo.registrarEquiposEnEdicion(edicion.equipos, edicion.idEdicion);
        //}

        /// <summary>
        /// Agrega los equipos recibidos a la edición
        /// </summary>
        public void agregarEquiposEnEdicion(string equipos)
        {
            //primero limpiamos la lista para evitar que se acumulen cuando el usuario apriete siguiente mas de una vez por algun motivo.
            edicion.equipos.Clear();
            if (equipos == "")
                throw new Exception("No hay equipos seleccionados");
            //quita la última coma de la cadena
            string cadena = equipos.Substring(0, equipos.Length - 1);
            //transforma la cadena en una lista de enteros
            List<int> listaIdsSeleccionados = cadena.Split(',').Select(Int32.Parse).ToList();
            //valido que tenga 3 o más equipos
            if (listaIdsSeleccionados.Count < 2)
                throw new Exception("Tiene que seleccionar al menos 2 equipos");
             //agrego los equipos al equipos a la edición
            GestorEquipo gestorEquipo = new GestorEquipo();
            foreach (int id in listaIdsSeleccionados)
                edicion.equipos.Add(gestorEquipo.obtenerEquipoReducidoPorId(id));
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
        /// Permite confirmar la Edición
        /// autor: Pau Pedrosa
        /// </summary>
        public void confirmarEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.confirmarEdicion(edicion); 
        }

        public void actualizarconfirmacionEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.actualizarconfirmacionEdicion(edicion);    
        }

        /// <summary>
        /// Elimina una edición de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarEdicion(int idEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.eliminarEdicion(idEdicion);
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

        /// <summary>
        /// Obtiene id de una torneo a partir de la edición
        /// autor: Flor Rojas
        /// </summary>
        public int obtenerIdTorneo()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            return daoEdicion.obtenerTorneoPorId(edicion.idEdicion);
        }

        /// <summary>
        /// Obtiene las preferencias de una edicion
        /// AUTOR: FLOR ROJAS
        /// </summary>
        /// <returns>configuracion/preferencias de la edicion</returns>
        public ConfiguracionEdicion obtenerPreferencias()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            return daoEdicion.obtenerPreferenciasPorId(edicion.idEdicion);
        }

        /// <summary>
        /// Obtiene los equipos de una edicion
        /// AUTOR: FLOR ROJAS
        /// </summary>
        /// <returns>lista de equipos</returns>
        public List<Equipo> obtenerEquipos()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            return daoEdicion.obtenerEquiposPorIdEdicion(edicion.idEdicion);
        }

        /// <summary>
        /// Obtiene las fases de una edicion
        /// AUTOR: FLOR ROJAS
        /// </summary>
        /// <returns>lista de fases</returns>
        public List<Fase> obtenerFases()
        {
            DAOFase daoFase = new DAOFase(); 
            return daoFase.obtenerFases(edicion.idEdicion);
        }

        public void perteneceAUsuario(int idEdicion)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.perteneceAUsuario(idEdicion, Sesion.getUsuario().idUsuario);
        }

        public void cambiarEstadoAConfigurada()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.cambiarEstadoAConfigurada(edicion.idEdicion); 
        }

        /// <summary>
        ///
        /// </summary>
        public void verificarCambiosDeEquipos(string equipos)
        {
           string cadena = equipos.Substring(0, equipos.Length - 1);
            List<int> listaIdsSeleccionados = cadena.Split(',').Select(Int32.Parse).ToList();
            List<int> listaIdsEquiposAlmacenados = new List<int>();
            foreach (Equipo e in edicion.equipos)
            {
                listaIdsEquiposAlmacenados.Add(e.idEquipo);
            }
            listaIdsSeleccionados.Sort();
            listaIdsEquiposAlmacenados.Sort();
            bool areEqual = listaIdsEquiposAlmacenados.SequenceEqual(listaIdsSeleccionados);
            if (!areEqual)
                throw new Exception("Modificación de equipos!!!");
            
        }
    }
}
