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
    public class GestorJugador
    {
        public Jugador jugador = new Jugador();
        /// <summary>
        /// Permite registrar un jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarJugador(string nombre, string dni, string fechaNacimiento, string numeroCamiseta, string telefono, string email, string facebook, bool sexo, bool tieneFichaMedica)
        {
            if (jugador == null)
                jugador = new Jugador();
            jugador.nombre = nombre;
            jugador.dni = dni;
            if (!fechaNacimiento.Equals(""))
                jugador.fechaNacimiento = Validador.castDate(fechaNacimiento);
            else
                jugador.fechaNacimiento = null;
            if (!numeroCamiseta.Equals(""))
                jugador.numeroCamiseta = Validador.castInt(numeroCamiseta);
            else
                jugador.numeroCamiseta = null;
            jugador.telefono = telefono;
            jugador.email = email;
            jugador.facebook = facebook;
            jugador.sexo = (sexo == true) ? "Masculino" : "Femenino";
            jugador.tieneFichaMedica = (tieneFichaMedica == true) ? true : false;
            int idEquipo = Sesion.getGestorEquipo().getEquipo().idEquipo;//obtiene el Id del equipo que está en Session
            if (idEquipo <= 0)
                throw new Exception("No hay un equipo seleccionado");
            DAOJugador daoJugador = new DAOJugador();
            jugador.idJugador = daoJugador.registrarJugador(jugador, idEquipo);
        }

        /// <summary>
        /// Obtiene todos los jugadores de un equipo
        /// autor: Pau Pedrosa
        /// </summary>
        public List<Jugador> obtenerJugadoresDeUnEquipo()
        {
            DAOJugador daoJugador = new DAOJugador();
            List<Jugador> jugadores = null;
            int idEquipo = Sesion.getGestorEquipo().equipo.idEquipo;//obtiene el Id del equipo que está en Session
            if(idEquipo>0)
                jugadores = daoJugador.obtenerJugadoresDeUnEquipo(idEquipo);
            return jugadores;
        }

        /// <summary>
        /// Obtiene un Jugador por Id
        /// autor: Pau Pedrosa
        /// </summary>
        public Jugador obtenerJugadorPorId(int idJugador)
        {
            DAOJugador daoJugador = new DAOJugador();
            jugador = daoJugador.obtenerJugadorPorId(idJugador);
            return jugador;
        }

        /// <summary>
        /// Modifica un Jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarJugador(int idJugador, string nombre, string dni, string fechaNacimiento, string numeroCamiseta, string telefono, string email, string facebook, bool sexo, bool tienefichaMedica)
        {
            DAOJugador daoJugador = new DAOJugador();
            jugador.idJugador = idJugador;
            jugador.nombre = nombre;
            jugador.dni = dni;
            if (!fechaNacimiento.Equals(""))
                jugador.fechaNacimiento = Validador.castDate(fechaNacimiento);
            else
                jugador.fechaNacimiento = null;
            if (!numeroCamiseta.Equals(""))
                jugador.numeroCamiseta = Validador.castInt(numeroCamiseta);
            else
                jugador.numeroCamiseta = null;
            jugador.telefono = telefono;
            jugador.email = email;
            jugador.facebook = facebook;
            jugador.sexo = (sexo == true) ? "Masculino" : "Femenino";
            jugador.tieneFichaMedica = (tienefichaMedica == true) ? true : false;
            daoJugador.modificarJugador(jugador);        
        }

        /// <summary>
        /// Elimina un jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarJugador(int idJugador)
        {
            DAOJugador daoJugador = new DAOJugador();
            daoJugador.eliminarJugador(idJugador);
        }
    }
}
