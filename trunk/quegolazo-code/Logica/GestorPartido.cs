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
    public class GestorPartido
    {
        public Partido partido;
        public DAOPartido daoPartido= new DAOPartido();

        /// <summary>
        /// Constructores
        /// </summary>
        public GestorPartido(Partido partido)
        {
            this.partido = partido;
        }
        public GestorPartido()
        {
            partido = new Partido();
        }

        /// <summary>
        /// Modifica los datos del partido
        /// autor: Facundo Allemand
        /// </summary>
        public void modificarPartido(string fechaHora, string txtGolesLocal, string txtGolesVisitante, bool huboPenales, string txtPenalesLocal, string txtPenalesVisitante,string idArbitro, string idCancha, List<int> titularesLocal, List<int> titularesVisitante)
        {
            if (!txtGolesLocal.Equals("") && txtGolesVisitante.Equals("") || txtGolesLocal.Equals("") && !txtGolesVisitante.Equals(""))
                throw new Exception("Debe cargar los goles de ambos equipos o ninguno de ellos");
            partido.golesLocal = (!txtGolesLocal.Equals("")) ? (int?)Validador.castInt(txtGolesLocal) : null;
            partido.golesVisitante = (!txtGolesVisitante.Equals("")) ? (int?)Validador.castInt(txtGolesVisitante) : null;
            if (partido.golesLocal < 0 || partido.golesVisitante<0)
                throw new Exception("Los goles no pueden ser negativos");
            cargarPenales(huboPenales, txtPenalesLocal, txtPenalesVisitante);
            validarGoles();
            partido.fecha = (!fechaHora.Equals("")) ? (DateTime?)Validador.castDate(fechaHora) : null;
            partido.arbitro = (!idArbitro.Equals("")) ? new Arbitro() : null;
            if(partido.arbitro!=null)
                partido.arbitro.idArbitro = Validador.castInt(idArbitro);
            partido.cancha = (!idCancha.Equals("")) ? new Cancha() : null;
            if(partido.cancha!=null)
                partido.cancha.idCancha = Validador.castInt(idCancha);
            partido.titularesLocal.Clear();
            foreach (int idJugador in titularesLocal)
                partido.titularesLocal.Add(new Jugador() { idJugador = idJugador });
            partido.titularesVisitante.Clear();
            foreach (int idJugador in titularesVisitante)
                partido.titularesVisitante.Add(new Jugador() { idJugador = idJugador });
            calcularGanador();
            GestorEdicion gestorEdicion = Sesion.getGestorEdicion();
            if (gestorEdicion.faseActual.tipoFixture.idTipoFixture.Contains("ELIM") && partido.empate == true && partido.golesLocal != null && partido.golesVisitante != null)
                throw new Exception("Debe definir un Ganador");
            if (partido.golesLocal != null && partido.golesVisitante != null)
                partido.estado.idEstado = Estado.partidoJUGADO;
            else 
            {
                if (partido.fecha != null)
                    partido.estado.idEstado = Estado.partidoPROGRAMADO;
                else
                    partido.estado.idEstado = Estado.partidoDIAGRAMADO;
                partido.idGanador = null;
                partido.idPerdedor = null;
                partido.empate = null;
                partido.penalesLocal = null;
                partido.penalesVisitante = null;
                partido.huboPenales = null;            
            }             
            daoPartido.modificarPartido(partido);          
            (new DAOFecha()).actualizarFecha(partido.idPartido);
            (new DAOFase()).actualizarEstadoFase(partido.idPartido);
            (new DAOEdicion()).actualizarEstadoEdicion(partido.idPartido);          
        }


       


        /// <summary>
        /// Valida y carga los penales
        /// autor: Facundo Allemand
        /// </summary>
        private void cargarPenales(bool huboPenales, string txtPenalesLocal, string txtPenalesVisitante)
        {
            if (huboPenales == true)
            {
                if (partido.golesLocal != partido.golesVisitante)
                    throw new Exception("Para cargar penales el resultado debe ser un empate");
                if (txtPenalesLocal.Equals("") || txtPenalesVisitante.Equals(""))
                    throw new Exception("Debe cargar los penales de ambos equipos");
                partido.huboPenales = huboPenales;
                partido.penalesLocal = Validador.castInt(txtPenalesLocal);
                partido.penalesVisitante = Validador.castInt(txtPenalesVisitante);
                if (partido.penalesLocal == partido.penalesVisitante)
                    throw new Exception("Un equipo debe ganar en definición por penales");
                if (partido.penalesLocal < 0 || partido.penalesVisitante <0)
                    throw new Exception("Los penales no pueden ser negativos");
            }
            else
            {
                partido.huboPenales = false;
                partido.penalesLocal = null;
                partido.penalesVisitante = null;
            }
        }

        /// <summary>
        /// Calcula cual es el equipo ganador y guarda los atributos asociados
        /// autor: Facundo Allemand
        /// </summary>
        public void calcularGanador()
        {
            if (partido.golesLocal > partido.golesVisitante || (partido.huboPenales == true && partido.penalesLocal > partido.penalesVisitante))
            {
                partido.empate = false;
                partido.idGanador = partido.local.idEquipo;
                partido.idPerdedor = partido.visitante.idEquipo;
            }
            else if (partido.golesLocal < partido.golesVisitante || (partido.huboPenales == true && partido.penalesLocal < partido.penalesVisitante))
            {
                partido.empate = false;
                partido.idGanador = partido.visitante.idEquipo;
                partido.idPerdedor = partido.local.idEquipo;
            }
            else
                partido.empate = true;
        }

        /// <summary>
        /// Valida que si hay goles cargados, coincida con el resultado.
        /// autor: Facundo Allemand
        /// </summary>
        public void validarGoles()
        {
            if (partido.goles.Count > 0)
            {
                int golesLocal = 0;
                int golesVisitante = 0;
                foreach (Gol gol in partido.goles)
                {
                    if (gol.equipo.idEquipo == partido.local.idEquipo || (gol.tipoGol.idTipoGol == TipoGol.EN_CONTRA && gol.equipo.idEquipo == partido.visitante.idEquipo))
                        golesLocal++;
                    else if (gol.equipo.idEquipo == partido.visitante.idEquipo || (gol.tipoGol.idTipoGol == TipoGol.EN_CONTRA && gol.equipo.idEquipo == partido.local.idEquipo))
                        golesVisitante++;
                }
                if (golesLocal != partido.golesLocal || golesVisitante != partido.golesVisitante)
                    throw new Exception("El resultado no coincide con los goles cargados");
            }
        }

        /// <summary>
        /// Obtiene un partido con todos sus objetos desde la BD
        /// autor: Facundo Allemand
        /// </summary>
        public void obtenerPartidoporId(string idPartido)
        {
            DAOJugador daoJugadores = new DAOJugador();
            DAOCancha daoCancha = new DAOCancha();
            DAOSancion daoSancion = new DAOSancion();
            partido = new Partido();
            partido.idPartido = Validador.castInt(idPartido);
            partido = daoPartido.obtenerPartidoPorId(partido.idPartido);
            partido.goles = daoPartido.obtenerGoles(partido.idPartido);
            partido.tarjetas = daoPartido.obtenerTarjetas(partido.idPartido);
            partido.cambios = daoPartido.obtenerCambios(partido.idPartido);
            partido.sanciones = daoSancion.obtenerSancionesDeUnPartido(partido.idPartido);
            partido.local.jugadores = daoJugadores.obtenerJugadoresDeUnEquipo(partido.local.idEquipo);
            partido.visitante.jugadores = daoJugadores.obtenerJugadoresDeUnEquipo(partido.visitante.idEquipo);
            partido.titularesLocal = daoPartido.obtenerTitularesDeUnPartido(partido.idPartido, partido.local.idEquipo);
            partido.titularesVisitante = daoPartido.obtenerTitularesDeUnPartido(partido.idPartido, partido.visitante.idEquipo);
        }

        /// <summary>
        /// Retorna si un jugador es titular en el equipo local
        /// autor: Facundo Allemand
        /// </summary>
        public bool esTitularLocal(int idJugador)
        {
            foreach (Jugador jugador in partido.titularesLocal)
                if (idJugador == jugador.idJugador)
                    return true;
            return false;
        }

        /// <summary>
        /// Retorna si un jugador es titular en el equipo visitante
        /// autor: Facundo Allemand
        /// </summary>
        public bool esTitularVisitante(int idJugador)
        {
            foreach (Jugador jugador in partido.titularesVisitante)
                if (idJugador == jugador.idJugador)
                    return true;
            return false;
        }

        /// <summary>
        /// Obtiene los tipos de gol desde la BD
        /// autor: Facundo Allemand
        /// </summary>
        public List<TipoGol> obtenerTiposGol()
        {
            return daoPartido.obtenerTiposGol();
        }

        /// <summary>
        /// Obtiene el tipo de gol desde la BD
        /// autor: Facundo Allemand
        /// </summary>
        public TipoGol obtenerTipoGolPorId(int idTipoGol)
        {
            return daoPartido.obtenerTipoGolPorId(idTipoGol);
        }

        /// <summary>
        /// Agrega un gol en el objeto partido.goles
        /// autor: Facundo Allemand
        /// </summary>
        public void agregarGol(string idEquipo, string idJugador, string idTipoGol, string minuto)
        {
            GestorJugador gestorJugador = new GestorJugador();
            GestorEquipo gestorEquipo = new GestorEquipo();
            Gol gol = new Gol();
            gol.equipo =gestorEquipo.obtenerEquipoPorId(Validador.castInt(Validador.isNotEmpty(idEquipo)));
            gol.jugador = (idJugador!="") ? gestorJugador.obtenerJugadorPorId(Validador.castInt(idJugador)) : null;
            gol.tipoGol = (idTipoGol!="") ? obtenerTipoGolPorId(Validador.castInt(idTipoGol)) : null;
            gol.minuto = (minuto!="") ? (int?)Validador.castInt(minuto) : null;
            partido.goles.Add(gol);
        }

        /// <summary>
        /// Agrega una tarjeta en el objeto partido.tarjetas
        /// autor: Facundo Allemand
        /// </summary>
        public void agregarTarjeta(string idEquipo, string idJugador, string tipoTarjeta, string minuto)
        {
            validarAgregarTarjeta(Validador.castInt(Validador.isNotEmpty(idJugador)), tipoTarjeta);
            GestorJugador gestorJugador = new GestorJugador();
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.equipo.idEquipo = Validador.castInt(Validador.isNotEmpty(idEquipo));
            tarjeta.jugador = gestorJugador.obtenerJugadorPorId(Validador.castInt(Validador.isNotEmpty(idJugador)));
            tarjeta.tipoTarjeta = Validador.castChar(Validador.isNotEmpty(tipoTarjeta));
            if (minuto != "")
                tarjeta.minuto = Validador.castInt(minuto);
            partido.tarjetas.Add(tarjeta);
        }

        /// <summary>
        /// Agrega un cambio en el objeto partido.cambios
        /// autor: Facundo Allemand
        /// </summary>
        public void agregarCambio(string idEquipo, string idJugadorEntra, string idJugadorSale, string minuto)
        {
            if (Validador.castInt(Validador.isNotEmpty(idJugadorEntra)) == Validador.castInt(Validador.isNotEmpty(idJugadorSale)))
                throw new Exception("El jugador que entra no puedo ser el mismo que sale");
            GestorJugador gestorJugador = new GestorJugador();
            Cambio cambio = new Cambio();
            cambio.equipo.idEquipo = Validador.castInt(Validador.isNotEmpty(idEquipo));
            cambio.jugadorEntra = gestorJugador.obtenerJugadorPorId(Validador.castInt(Validador.isNotEmpty(idJugadorEntra)));
            cambio.jugadorSale = gestorJugador.obtenerJugadorPorId(Validador.castInt(Validador.isNotEmpty(idJugadorSale)));
            if (minuto != "")
                cambio.minuto = Validador.castInt(minuto);
            partido.cambios.Add(cambio);
        }

        public List<Gol> obtenerGolesPorEquipo(int idEquipo)
        {
            List<Gol> golesEquipo = new List<Gol>();
            foreach (Gol gol in partido.goles)
            {
                if(gol.equipo.idEquipo == idEquipo)
                    golesEquipo.Add(gol);
            }
            return golesEquipo;
        }

        public List<Tarjeta> obtenerTarjetasRojasPorEquipo(int idEquipo)
        {
            List<Tarjeta> tarjetasRojas = new List<Tarjeta>();
            foreach (Tarjeta tarjeta in partido.tarjetas)
            {
                if (tarjeta.equipo.idEquipo == idEquipo && tarjeta.tipoTarjeta.Equals('R'))
                    tarjetasRojas.Add(tarjeta);
            }
            return tarjetasRojas;
        }

        public List<Tarjeta> obtenerTarjetasAmarillasPorEquipo(int idEquipo)
        {
            List<Tarjeta> tarjetasAmarillas = new List<Tarjeta>();
            foreach (Tarjeta tarjeta in partido.tarjetas)
            {
                if (tarjeta.equipo.idEquipo == idEquipo && tarjeta.tipoTarjeta.Equals('A'))
                    tarjetasAmarillas.Add(tarjeta);
            }
            return tarjetasAmarillas;
        }

        public List<Sancion> obtenerSancionesPorEquipo(int idEquipo)
        {
            List<Sancion> sanciones = new List<Sancion>();
            foreach (Sancion sancion in partido.sanciones)
            {
                if (sancion.equipo.idEquipo == idEquipo)
                    sanciones.Add(sancion);
            }
            return sanciones;
        }


        public List<Cambio> obtenerCambiosPorEquipo(int idEquipo)
        {
            List<Cambio> cambios = new List<Cambio>();
            foreach (Cambio cambio in partido.cambios)
            {
                if (cambio.equipo.idEquipo == idEquipo)
                    cambios.Add(cambio);
            }
            return cambios;
        }

        /// <summary>
        /// Valida la cantidad de tarjetas para el jugador
        /// autor: Facundo Allemand
        /// </summary>
        public void validarAgregarTarjeta(int idJugador, string tipoTarjeta)
        {
            if (!tipoTarjeta.Equals("A") && !tipoTarjeta.Equals("R"))
                throw new Exception("El tipo de tarjeta seleccionada no es válida");
            int cantRojas=0;
            int cantAmarillas=0;
            foreach (Tarjeta tarjeta in partido.tarjetas)
            {
                if (idJugador == tarjeta.jugador.idJugador && tarjeta.tipoTarjeta.ToString().Equals("A"))
                    cantAmarillas++;
                else if (idJugador == tarjeta.jugador.idJugador && tarjeta.tipoTarjeta.ToString().Equals("R"))
                    cantRojas++;
            }
            if (cantAmarillas >= 2 || cantRojas >= 1)
                throw new Exception("Ya no puede agregar más tarjetas");
        }

        /// <summary>
        /// Elimina un gol del objeto partido.goles
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarGol(string idGolTemp)
        {
            int idGol = Validador.castInt(idGolTemp);
            Gol golAEliminar = new Gol();
            foreach (Gol gol in partido.goles)
                if (gol.idGol == idGol)
                    golAEliminar = gol;
            partido.goles.Remove(golAEliminar);
        }

        /// <summary>
        /// Elimina un cambio del objeto partido.cambios
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarCambio(string idCambioTemp)
        {
            int idCambio = Validador.castInt(idCambioTemp);
            Cambio cambioAEliminar = new Cambio();
            foreach (Cambio cambio in partido.cambios)
                if (cambio.idCambio == idCambio)
                    cambioAEliminar = cambio;
            partido.cambios.Remove(cambioAEliminar);
        }

        /// <summary>
        /// Elimina una tarjeta del objeto partido.tarjetas
        /// autor: Facundo Allemand
        /// </summary>
        public void eliminarTarjeta(string idTarjetaTemp)
        {
            int idTarjeta = Validador.castInt(idTarjetaTemp);
            Tarjeta tarjetaAEliminar = new Tarjeta();
            foreach (Tarjeta tarjeta in partido.tarjetas)
                if (tarjeta.idTarjeta == idTarjeta)
                    tarjetaAEliminar = tarjeta;
            partido.tarjetas.Remove(tarjetaAEliminar);
        }
    }
}
