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
        public void modificarPartido(string fechaHora, string idArbitro, string idCancha, List<int> titularesLocal, List<int> titularesVisitante)
        {
            partido.fecha = Validador.castDate(fechaHora);
            partido.arbitro.idArbitro = Validador.castInt(idArbitro);
            partido.cancha.idCancha = Validador.castInt(idArbitro);
            foreach (int idJugador in titularesLocal)
                partido.titularesLocal.Add(new Jugador() { idJugador = idJugador });
            foreach (int idJugador in titularesVisitante)
                partido.titularesVisitante.Add(new Jugador() { idJugador = idJugador });
            daoPartido.modificarPartido(partido);
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
