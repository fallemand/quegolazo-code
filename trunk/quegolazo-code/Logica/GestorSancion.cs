using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorSancion
    {
        public Sancion sancion = new Sancion();
        public List<Partido> partidos = new List<Partido>();
        public List<Equipo> equipos = new List<Equipo>();
        public List<Jugador> jugadores = new List<Jugador>();
        public List<Partido> obtenerPartidosDeFecha(string idFecha)
        {
            List<Partido> todosPartidos = new List<Partido>();
            partidos.Clear();
            GestorEdicion gestorEdicion = Sesion.getGestorEdicion();
            foreach (Fecha fecha in gestorEdicion.gestorFase.faseActual.obtenerFechas())
            {
                if (fecha.idFecha == int.Parse(idFecha))
                    todosPartidos = fecha.partidos;
            }
            foreach (Partido partido in todosPartidos)
            {
                if (partido.local != null && partido.visitante != null)
                    partidos.Add(partido);
            }
            return partidos;
        }

        public List<Equipo> obtenerEquiposDePartido(string idPartido)
        {
            equipos.Clear();
            foreach (Partido partido in partidos)
            {
                if (partido.idPartido == int.Parse(idPartido))
                {
                    equipos.Add(partido.local);
                    equipos.Add(partido.visitante);
                }
            }
            return equipos;
        }

        public List<Jugador> obtenerJugadoresDeEquipo(string idEquipo)
        {
            jugadores.Clear();
            foreach (Equipo equipo in equipos)
            {
                if (equipo.idEquipo == int.Parse(idEquipo))
                    jugadores = equipo.jugadores;                
            }
            return jugadores;
        }

        public List<MotivoSancion> obtenerMotivos()
        {
            DAOSancion daoSancion = new DAOSancion();
            List<MotivoSancion> motivos = new List<MotivoSancion>();
            motivos = daoSancion.obtenerMotivos();
            return motivos;
        }
    }
}
