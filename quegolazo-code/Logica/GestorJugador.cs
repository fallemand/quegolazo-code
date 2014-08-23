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
    public class GestorJugador
    {
        public Jugador jugador = new Jugador();
        /// <summary>
        /// Permite registrar un jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarJugador(string nombre, string dni, string fechaNacimiento, string telefono, string email, string facebook, bool sexo, bool tieneFichaMedica)
        {
            if (jugador == null)
                jugador = new Jugador();
            jugador.nombre = nombre;
            jugador.dni = dni;
            jugador.fechaNacimiento = Validador.castDate(fechaNacimiento);
            jugador.telefono = telefono;
            jugador.email = email;
            jugador.facebook = facebook;
            jugador.sexo = (sexo == true) ? "Masculino" : "Femenino";
            jugador.tieneFichaMedica = (tieneFichaMedica == true) ? true : false;
            int idEquipo = Sesion.getEquipo().idEquipo;//obtiene el Id del equipo que está en Session
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
            int idEquipo = Sesion.getEquipo().idEquipo;//obtiene el Id del equipo que está en Session
            List<Jugador> jugadores = daoJugador.obtenerJugadoresDeUnEquipo(idEquipo);
            return jugadores;
        }

        /// <summary>
        /// Obtiene un Jugador por Id
        /// autor: Pau Pedrosa
        /// </summary>
        public void obtenerJugadorPorId(int idJugador)
        {
            DAOJugador daoJugador = new DAOJugador();
            jugador = daoJugador.obtenerJugadorPorId(idJugador);
        }

        /// <summary>
        /// Modifica un Jugador
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarJugador(int idJugador, string nombre, string dni, string fechaNacimiento, string telefono, string email, string facebook, bool sexo, bool tienefichaMedica)
        {
            DAOJugador daoJugador = new DAOJugador();
            jugador.idJugador = idJugador;
            jugador.nombre = nombre;
            jugador.dni = dni;
            jugador.fechaNacimiento = Validador.castDate(fechaNacimiento);
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
