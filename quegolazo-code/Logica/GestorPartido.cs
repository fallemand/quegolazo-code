﻿using System;
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
            //daoPartido.(partido);
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

        public void eliminarGol(string idGolTemp)
        {
            int idGol = Validador.castInt(idGolTemp);
            Gol golAEliminar = new Gol();
            foreach (Gol gol in partido.goles)
                if (gol.idGol == idGol)
                    golAEliminar = gol;
            partido.goles.Remove(golAEliminar);
        }

        public void agregarCambio(string idEquipo, string idJugadorEntra, string idJugadorSale, string minuto)
        {
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