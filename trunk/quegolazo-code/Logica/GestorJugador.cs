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
        public void registrarJugador(string nombre, string dni, string fechaNacimiento, string email, string facebook, bool sexo, bool tieneFichaMedica, int idEquipo)
        {
            if (jugador == null)
                jugador = new Jugador();
            jugador.nombre = nombre;
            jugador.dni = dni;
            jugador.fechaNacimiento = Validador.castDate(fechaNacimiento);
            jugador.email = email;
            jugador.facebook = facebook;
            jugador.sexo = (sexo == true) ? "Masculino" : "Femenino";
            jugador.tieneFichaMedica = (tieneFichaMedica == true) ? true : false;
            DAOJugador daoJugador = new DAOJugador();
            jugador.idJugador = daoJugador.registrarJugador(jugador, idEquipo);
        }
    }
}
