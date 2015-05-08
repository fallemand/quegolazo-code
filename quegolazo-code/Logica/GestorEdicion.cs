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
        /// Actualiza la fase actual de una edición, basandose en los estados, se considera fase actual a la primera fase que encuentre en estado Registrada o Diagramada
        /// </summary>   
        public void actualizarFaseActual()
        {
            if (this.edicion.fases.Count == 0)
            {
                faseActual = null;
                return;
            }
            else
            {
                foreach (Fase fase in this.edicion.fases)
                {
                    if (fase.estado.idEstado == Estado.faseREGISTRADA || fase.estado.idEstado == Estado.faseDIAGRAMADA || fase.estado.idEstado == Estado.faseINICIADA)
                    {
                        this.faseActual = fase;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza la fase actual de una edición, basandose en los estados, se considera fase actual a la primera fase que encuentre en estado Diagramada o iniciada
        /// </summary>
        /// <param name="gestor">El gestor que se va a actualizar</param>
        public void getFaseActual()
        {
            bool hayFaseIniciadaODiagramada = false;
            if (this.edicion.fases.Count == 0)
            {
                faseActual = null;
                return;
            }
            else
            {
                foreach (Fase fase in this.edicion.fases)
                {
                    if (fase.estado.idEstado == Estado.faseDIAGRAMADA || fase.estado.idEstado == Estado.faseINICIADA || fase.estado.idEstado == Estado.faseREGISTRADA)
                    {
                       this.faseActual = fase;
                       hayFaseIniciadaODiagramada = true;
                       break;
                    }
                }
                if (!hayFaseIniciadaODiagramada)
                    this.faseActual = this.edicion.fases[this.edicion.fases.Count - 1];
            }
        }

        /// <summary>
        /// Devuelve la fase actual de una lista de fases, basandose en los estados, se considera fase actual a la primera fase que encuentre en estado Registrada
        /// </summary>
        /// <param name="fases">La lista que se quiere verificar</param>
        public Fase getFaseActual(List<Fase> fases)
        {
            Fase respuesta = null;
            if (fases.Count == 0)
            {              
                return respuesta;
            }
            else
            {
                foreach (Fase fase in fases)
                {
                    if (fase.estado.idEstado == Estado.faseREGISTRADA || fase.estado.idEstado == Estado.faseDIAGRAMADA || fase.estado.idEstado == Estado.faseINICIADA)
                    {
                        respuesta = fase;
                        break;
                    }
                }
            }
            return respuesta;
        }




        /// <summary> 
        /// Registra una nueva edicion
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public void registrarEdicion(int idTorneo)
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            daoEdicion.registrarEdicion(edicion, idTorneo);
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
            //quita la última coma de la cadena
            string cadena = equipos.Substring(0, equipos.Length - 1);
            //transforma la cadena en una lista de enteros
            List<int> listaIdsSeleccionados = cadena.Split(',').Select(Int32.Parse).ToList();
            //valido que tenga 3 o más equipos
            if (listaIdsSeleccionados.Count < 2)
                throw new Exception("Tiene que seleccionar al menos 2 equipos");
             //agrego los equipos al equipos a la edición
            GestorEquipo gestorEquipo = new GestorEquipo();
            edicion.equipos = null; // lo agreguè yo pau
            edicion.equipos = new List<Equipo>();
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
        /// Permite confirmar la Edición, para la primera vez que se crea, solo tiene en cuenta la primera fase.
        /// autor: Pau Pedrosa
        /// </summary>
        public void confirmarEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            //verificarPrimeraFaseEliminatoria();
            daoEdicion.confirmarEdicion(edicion); 
        }
        ///// <summary>
        ///// Verifica si la primera fase tiene fixture tipo Eliminatorio, y si es asi crea los partidos para toda la llave hasta la final y el tercer puesto.
        ///// </summary>
        //private void verificarPrimeraFaseEliminatoria()
        //{
        //    if (edicion.fases[0].tipoFixture.idTipoFixture.Contains("ELIM"))
        //        gestorFase.crearPartidosSiguientes(edicion.fases[0]);
        //}

        public void actualizarconfirmacionEdicion()
        {
            DAOEdicion daoEdicion = new DAOEdicion();
            //verificarPrimeraFaseEliminatoria();
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

        public bool fasesFinalizadas()
        {
            int fasesFinalizadas = 0;
            bool edicionFinalizada = false;
            foreach (Fase itemFase in obtenerFases())
            {
                if (itemFase.estado.idEstado == Estado.faseFINALIZADA)
                    fasesFinalizadas++;                    
            }
            if (obtenerFases().Count == fasesFinalizadas)
                edicionFinalizada = true;
            return edicionFinalizada;
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
        //agrega los equipos en una fase determinada, de una lista de fases dada.
        public void agregarEquiposEnFase(List<Fase> fases, string equipos,int idFaseNueva)
        {
            try
            {
                int indiceFase = idFaseNueva - 1;
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
                fases[indiceFase].equipos.Clear();
                foreach (int id in listaIdsSeleccionados)
                    fases[indiceFase].equipos.Add(gestorEquipo.obtenerEquipoReducidoPorId(id));
                fases[indiceFase].esGenerica = false;
                fases[indiceFase].tipoFixture = new TipoFixture("TCT");
                fases[indiceFase].estado = new Estado() { idEstado = Estado.faseDIAGRAMADA };     
        }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }  
        }


       /// <summary>
        /// Este método crea una nueva fase en caso de que no exista una siguiente, en una lista de fases dada. Devuelve true si la fase siguiente estaba creada genericamente y false en caso de que haya creado una nueva.
       /// </summary>
       /// <param name="fases"></param>
       /// <param name="idFaseNueva"></param>
        /// <returns>Devuelve true si la fase siguiente estaba creada genericamente y false en caso de que haya creado una nueva.</returns>
       public bool verificarProximaFase(List<Fase> fases, int idFaseNueva)
        {
           bool creoFaseNueva = idFaseNueva > fases.Count;
            bool existeFase = false;
            foreach (Fase f in fases)
            {
                if (!creoFaseNueva && f.idFase == idFaseNueva)
                {
                    existeFase = true;
                    break;
                }
            }
            if (!existeFase)
                fases.Add(new Fase { idFase = creoFaseNueva ? idFaseNueva : idFaseNueva, idEdicion = edicion.idEdicion, estado = new Estado(Estado.faseDIAGRAMADA) });
            //cierro la fase anterior a la nueva, si idFaseNueva es del tamaño de la lista de fases, es xq el usuario creo una nueva fase, entonces cambio el estado de esa fase, sino de la que tiene el indice anterior a la actual.
            int indiceFase = idFaseNueva-2 ;
           fases[indiceFase].estado.idEstado = Estado.faseFINALIZADA;
            return existeFase;
        }

        /// <summary>
        /// Cuando se juega un partido, verifica si se debe cerrar la fase
        /// </summary>
        /// <param name="idPartido"></param>
        public void actualizarEstado(int idPartido)
        {
            new DAOEdicion().actualizarEstadoEdicion(idPartido);
        }


        /// <summary>
        /// Verifica si es la ultima fase, si es q el id de la afse es el mayor de todos
        /// </summary>
        /// <param name="idFase"></param>
        /// <returns></returns>
        public bool esUltimaFase(int idFase)
        {
            bool esUltima = false;
            foreach (Fase f in edicion.fases)
            {
                esUltima = (f.idFase <= idFase) ? true : false;
            }
            return esUltima;
        }

        /// <summary>
        /// Este metodo crea los grupos y los equipos que se guardaran al finalizar la edicion
        /// </summary>
        /// <param name="equipos"></param>
        /// <param name="idGrupo"></param>
        public void guardarEquiposEnGrupo(string equipos,int idGrupo)
        {
            Grupo grupo = new Grupo { idGrupo=idGrupo};
            if (equipos == "")
                throw new Exception("No hay equipos seleccionados");
            //quita la última coma de la cadena
            string cadena = equipos.Substring(0, equipos.Length - 1);
            //transforma la cadena en una lista de enteros
            List<int> listaIdsSeleccionados = cadena.Split(',').Select(Int32.Parse).ToList();
            foreach (int id in listaIdsSeleccionados)
            {
                Equipo equipo = new Equipo { idEquipo = id };
                grupo.equipos.Add(equipo);
            }
        }

        public void cambiarEstadoAFase(int idEdicion, int idFase, int idEstado)
        {
            DAOFase daoFase = new DAOFase();
            daoFase.cambiarEstado(idEdicion, idFase, idEstado);
        }

        public void cerrarEdicion(int idEdicion)
        {
            cambiarEstado(Estado.edicionFINALIZADA); //Cambia estado edición: de Iniciada a FINALIZADA
            cambiarEstadoAFase(edicion.idEdicion, faseActual.idFase, Estado.faseFINALIZADA); // Cambiar estado Fase a FINALIZADA
            DAOPartido daoPartido = new DAOPartido();
            DAOFecha daoFecha = new DAOFecha();
            daoPartido.cambiarEstadosAPartidos(Estado.partidoCANCELADO, edicion.idEdicion); //De todos los partidos que no han  sido jugados, les cambia el estado a CANCELADO           
            daoFecha.cambiarEstadosAFechas(Estado.fechaCOMPLETA, edicion.idEdicion); // Pone a todas las fechas como COMPLETAS
        }

        /// <summary>
        /// Cierra una fase (la anterior a la fase actual), y actualiza toda la configuracón de fases basándose en lo que generó el usuario.
        /// </summary>
        /// <param name="edicion">LA edicion con las fases a actualizar</param>
        /// <param name="idFaseActual">La fase que se acaba de modificar.</param>
        public void actualizarFasesLuegoDeCerrarUna(Edicion edicion, int idFaseActual)
        {
            new GestorFase().generarFixtureDeUnaFase(edicion.fases[idFaseActual - 1]);
            new DAOFase().cerrarFaseYActualizarPosteriores(edicion, idFaseActual);
        }
        public void cancelarEdicion(int idEdicion)
        {
            if (edicion.estado.idEstado != Estado.edicionREGISTRADA)            
            {//la edición está CONFIGURADA O INICIADA                
                DAOFase daoFase = new DAOFase();
                DAOFecha daoFecha = new DAOFecha();
                DAOPartido daoPartido = new DAOPartido();
                //Le pone estado Cancelada a todas las fases que no estén finalizadas
                daoFase.cambiarEstadoAFasesIncompletasYDiagramadas(edicion.idEdicion, Estado.edicionCANCELADA);                
                //Le pone estado Cancelado a todos los partidos que no están jugados
                daoPartido.cambiarEstadosAPartidos(Estado.partidoCANCELADO, edicion.idEdicion);
                daoFecha.cambiarEstadosAFechasIncompletas(Estado.fechaCANCELADA, edicion.idEdicion);
            }
            cambiarEstado(Estado.edicionCANCELADA);//Cambia estado edición: de Iniciada a FINALIZADA            
        }
       

        ///// <summary>
        ///// Actualiza la fase actual de una edición, basandose en los estados, se considera fase actual a la primera fase que encuentre en estado Registrada
        ///// </summary>
        ///// <param name="gestor">El gestor que se va a actualizar</param>
        //public void getFechaActual()
        //{
        //    if (this.edicion.fases.Count != 0)
        //    {
        //        foreach (Fase fase in this.edicion.fases)
        //        {
        //            foreach(Fecha fecha in fase.grupos[0].fechas)
        //            {
        //                if (fecha.estado.idEstado == Estado.fechaINCOMPLETA)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        }

    }

