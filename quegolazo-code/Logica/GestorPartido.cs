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

        public GestorPartido(Partido partido)
        {
            this.partido = partido;
        }

        public GestorPartido()
        {
            partido = new Partido();
        }

        //public Partido obtenerPartidoPorId(int idPartido) {
        //    daoPartido.obtenerPartidos
        //}

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
            }
            else
            {
                partido.huboPenales = false;
                partido.penalesLocal = null;
                partido.penalesVisitante = null;
            }
            validarGoles();
            if (!fechaHora.Equals(""))
                partido.fecha = Validador.castDate(fechaHora);
            if (!idArbitro.Equals(""))
            {
                if (partido.arbitro == null)
                    partido.arbitro = new Arbitro();
                partido.arbitro.idArbitro = Validador.castInt(idArbitro);
            }
            if (!idCancha.Equals(""))
            {
                if (partido.cancha == null)
                    partido.cancha = new Cancha();
                partido.cancha.idCancha = Validador.castInt(idCancha);
            }
            foreach (int idJugador in titularesLocal)
                partido.titularesLocal.Add(new Jugador() { idJugador = idJugador });
            foreach (int idJugador in titularesVisitante)
                partido.titularesVisitante.Add(new Jugador() { idJugador = idJugador });
            calcularGanador();
            daoPartido.modificarPartido(partido);
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
                    if (gol.equipo.idEquipo == partido.local.idEquipo)
                        golesLocal++;
                    else if (gol.equipo.idEquipo == partido.visitante.idEquipo)
                        golesVisitante++;
                }
                if (golesLocal != partido.golesLocal || golesVisitante != partido.golesVisitante)
                    throw new Exception("El resultado no coincide con los goles cargados");
            }
        }

        public void obtenerPartidoporId(string idPartido)
        {
            DAOJugador daoJugadores = new DAOJugador();
            partido.idPartido = Validador.castInt(idPartido);
            partido = daoPartido.obtenerPartidoPorId(partido.idPartido);
            partido.goles = daoPartido.obtenerGoles(partido.idPartido);
            partido.tarjetas = daoPartido.obtenerTarjetas(partido.idPartido);
            partido.cambios = daoPartido.obtenerCambios(partido.idPartido);
            partido.local.jugadores = daoJugadores.obtenerJugadoresDeUnEquipo(partido.local.idEquipo);
            partido.visitante.jugadores = daoJugadores.obtenerJugadoresDeUnEquipo(partido.visitante.idEquipo);
        }

        public List<TipoGol> obtenerTiposGol()
        {
            return daoPartido.obtenerTiposGol();
        }

        public void agregarGol(string idEquipo, string idJugador, string idTipoGol, string minuto)
        {
            GestorJugador gestorJugador = new GestorJugador();
            Gol gol = new Gol();
            gol.equipo.idEquipo = Validador.castInt(Validador.isNotEmpty(idEquipo));
            gol.jugador = gestorJugador.obtenerJugadorPorId(Validador.castInt(Validador.isNotEmpty(idJugador)));
            if(idTipoGol!="")
                gol.tipoGol.idTipoGol = Validador.castInt(idTipoGol);
            if(minuto!="")
                gol.minuto = Validador.castInt(minuto);
            partido.goles.Add(gol);
        }

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

        public void eliminarGol(string idGolTemp)
        {
            int idGol = Validador.castInt(idGolTemp);
            Gol golAEliminar = new Gol();
            foreach (Gol gol in partido.goles)
                if (gol.idGol == idGol)
                    golAEliminar = gol;
            partido.goles.Remove(golAEliminar);
        }

        public void eliminarCambio(string idCambioTemp)
        {
            int idCambio = Validador.castInt(idCambioTemp);
            Cambio cambioAEliminar = new Cambio();
            foreach (Cambio cambio in partido.cambios)
                if (cambio.idCambio == idCambio)
                    cambioAEliminar = cambio;
            partido.cambios.Remove(cambioAEliminar);
        }

        public void eliminarTarjeta(string idTarjetaTemp)
        {
            int idTarjeta = Validador.castInt(idTarjetaTemp);
            Tarjeta tarjetaAEliminar = new Tarjeta();
            foreach (Tarjeta tarjeta in partido.tarjetas)
                if (tarjeta.idTarjeta == idTarjeta)
                    tarjetaAEliminar = tarjeta;
            partido.tarjetas.Remove(tarjetaAEliminar);
        }

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
    }
}
