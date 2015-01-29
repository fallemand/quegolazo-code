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
        public Fase faseActual { get; set; }
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
        /// Actualiza la fase actual de una edición, basandose en los estados, se considera fase actual a la primera fase que encuentre en estado Registrada
        /// </summary>
        /// <param name="gestor">El gestor que se va a actualizar</param>
        public void actualizarFaseActual(GestorEdicion gestor) {
            if (gestor.edicion.fases.Count == 0)
            {
                faseActual = null;
                return;
            }
            else
            {
                foreach (Fase fase in gestor.edicion.fases)
                {
                    if (fase.estado.idEstado == Estado.faseREGISTRADA)
                    {
                        gestor.faseActual = fase;
                        break;
                    }
                }
            }
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
            edicion = new Edicion();
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
            daoEdicion.cambiarEstado(edicion.idEdicion, Estado.edicionCONFIGURADA); 
        }

        public void cambiarEstado(int idEstado)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.cambiarEstado(edicion.idEdicion, idEstado);
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

        public void agregarEquiposEnFase(string equipos,int idFaseNueva)
        {
            int indiceFase = idFaseNueva -1;
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
            edicion.fases[indiceFase].equipos.Add(gestorEquipo.obtenerEquipoReducidoPorId(id));
            edicion.fases[indiceFase].esGenerica = false;
            edicion.fases[indiceFase].tipoFixture = new TipoFixture("TCT");
            edicion.fases[indiceFase].estado = new Estado() { idEstado= Estado.faseREGISTRADA};
            //if (edicion.fases[idFaseNueva - 1].equipos.Count() == edicion.fases[idFaseNueva - 1].cantidadDeEquipos && edicion.fases[idFaseNueva - 1].cantidadDeEquipos!=0)
            //    throw new Exception("La cantidad de equipos no coincide con la indicada para esta fase");
        }


        public void verificarProximaFase(int idFaseNueva)
        {
            bool existeFase=false;
            foreach (Fase f in edicion.fases)
            {
                if (f.idFase == idFaseNueva)
                {
                    existeFase = true;
                    break;
                }
            }
            if (!existeFase)
                edicion.fases.Add(new Fase { idFase = idFaseNueva,idEdicion=edicion.idEdicion});
        }

        /// <summary>
        /// Cuando se juega un partido, verifica si se debe cerrar la fase
        /// </summary>
        /// <param name="idPartido"></param>
        public void actualizarEstado(int idPartido)
        {
            new DAOEdicion().actualizarEstadoEdicion(idPartido);
        }

    }
}
