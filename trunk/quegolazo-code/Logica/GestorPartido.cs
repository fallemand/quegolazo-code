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
    }
}
