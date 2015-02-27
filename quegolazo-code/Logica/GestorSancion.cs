using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Data;

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
            foreach (Fecha fecha in gestorEdicion.faseActual.obtenerFechas())
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

        public List<Jugador> obtenerJugadoresDeEquipo2(string idEquipo)
        {
            List<Jugador> listaJugadores = new List<Jugador>();
            DAOJugador daoJugador = new DAOJugador();
            listaJugadores = daoJugador.obtenerJugadoresDeUnEquipo(int.Parse(idEquipo));
            return listaJugadores;
        }

        public List<MotivoSancion> obtenerMotivos()
        {
            DAOSancion daoSancion = new DAOSancion();
            List<MotivoSancion> motivos = new List<MotivoSancion>();
            motivos = daoSancion.obtenerMotivos();
            return motivos;
        }

        public void registrarSancion(string idEdicion, string idFecha, string idPartido, string idEquipo, string idJugador, string FechaSancion, string idMotivoSancion, string observacion, string puntosAQuitar, string cantidadFechasSuspendidas, string idFase)
        {
            sancion = null;
            sancion = new Sancion();
            sancion.idEdicion = Validador.isNotEmptyAndCastInt(idEdicion);
            sancion.idFecha = (!idFecha.Equals("")) ? (int?)Validador.castInt(idFecha) : null;
            sancion.idPartido = (!idPartido.Equals("")) ? (int?)Validador.castInt(idPartido) : null;
            sancion.idEquipo = Validador.isNotEmptyAndCastInt(idEquipo);
            sancion.idJugador = (!idJugador.Equals("")) ? (int?)Validador.castInt(idJugador) : null;
            sancion.fechaSancion = (!FechaSancion.Equals("")) ? (DateTime?)Validador.castDate(FechaSancion) : null;
            sancion.motivoSancion.idMotivoSancion = (!idMotivoSancion.Equals("")) ? (int?)Validador.castInt(idMotivoSancion) : null;
            sancion.observacion = observacion;
            sancion.puntosAQuitar = (!puntosAQuitar.Equals("")) ? (int?)Validador.castInt(puntosAQuitar) : null;
            sancion.cantidadFechasSuspendidas = (!cantidadFechasSuspendidas.Equals("")) ? (int?)Validador.castInt(cantidadFechasSuspendidas) : null;
            sancion.idFase = Validador.isNotEmptyAndCastInt(idFase);
            DAOSancion daoSancion = new DAOSancion();
            daoSancion.registrarSancion(sancion);
        }

        public DataTable obtenerSancionesDeUnaEdicion(string idEdicion)
        {
            DAOSancion daoSancion = new DAOSancion();
            return daoSancion.obtenerSancionesDeEdicion(int.Parse(idEdicion));
        }

        public Sancion obtenerSancionPorId(string idSancion)
        {
            DAOSancion daoSancion = new DAOSancion();
            sancion = daoSancion.obtenerSancionPorId(int.Parse(idSancion));
            return sancion;
        }

        public void eliminarSancion(int idSancion)
        {
            DAOSancion daoSancion = new DAOSancion();
            daoSancion.eliminarSancion(idSancion);
        }

        public void modificarSancion(string idSancion, string idFecha, string idPartido, string idEquipo, string idJugador, string fechaSancion, string idMotivoSancion, string observacion, string puntosAQuitar, string cantidadFechasSuspendidas)
        {
            DAOSancion daoSancion = new DAOSancion();
            sancion.idSancion = int.Parse(idSancion);
            sancion.idFecha = (idFecha != null) ? (int?)int.Parse(idFecha) : null;
            sancion.idPartido = (idPartido != null) ? (int?)int.Parse(idPartido) : null;
            sancion.idEquipo = int.Parse(idEquipo);
            sancion.idJugador = (idJugador != null) ? (int?)int.Parse(idJugador) : null;
            if (fechaSancion.Equals(""))
                fechaSancion = null;
            if (observacion.Equals(""))
                observacion = null;
            if (puntosAQuitar.Equals(""))
                puntosAQuitar = null;
            if (cantidadFechasSuspendidas.Equals(""))
                cantidadFechasSuspendidas = null;
            sancion.fechaSancion = (fechaSancion != null) ? (DateTime?)DateTime.Parse(fechaSancion) : null;
            sancion.motivoSancion.idMotivoSancion = (idMotivoSancion != null) ? (int?)int.Parse(idMotivoSancion) : null;
            sancion.observacion = observacion;
            sancion.puntosAQuitar = (puntosAQuitar != null) ? (int?)int.Parse(puntosAQuitar) : null;
            sancion.cantidadFechasSuspendidas = (cantidadFechasSuspendidas != null) ? (int?)int.Parse(cantidadFechasSuspendidas) : null;
            daoSancion.modificarSancion(sancion);
        }

        public string manipulaSancionar(Edicion edicionSeleccionada)
        {
            string mensaje = "";
            switch (edicionSeleccionada.estado.idEstado)
            {
                case Estado.edicionREGISTRADA:
                    mensaje = "La edición seleccionada no se encuentra Configurada. Por favor, configure la edición primero para continuar";
                    break;
                case Estado.edicionCANCELADA:
                    mensaje = "La edición seleccionada está " + edicionSeleccionada.estado.nombre + ". No puede registrar Sanciones.";
                    break;
                case Estado.edicionFINALIZADA:
                    mensaje = "La edición seleccionada está " + edicionSeleccionada.estado.nombre + ". No puede registrar Sanciones.";
                    break;
                case Estado.edicionCONFIGURADA:
                    if (!(edicionSeleccionada.preferencias.sancionesEquipos && edicionSeleccionada.preferencias.sancionesJugadores))
                        mensaje = "La edición seleccionada no permite registrar sanciones. Actualice sus preferencias.";
                    break;
                case Estado.edicionINICIADA:
                    if (!(edicionSeleccionada.preferencias.sancionesEquipos && edicionSeleccionada.preferencias.sancionesJugadores))
                        mensaje = "La edición seleccionada no permite registrar sanciones.";
                    break;
            }
            return mensaje;
        }
    }
}
