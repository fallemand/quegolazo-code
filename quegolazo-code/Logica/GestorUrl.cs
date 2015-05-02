using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class GestorUrl
    {
        //Forms en carpeta Admin
        public const string aARBITROS = "/admin/arbitros.aspx";
        public const string aCANCHAS = "/admin/canchas.aspx";
        public const string aEQUIPOS = "/admin/equipos.aspx";
        public const string aJUGADORES = "/admin/jugadores.aspx";
        public const string aINDEX = "/admin/index.aspx";
        public const string aTORNEOS = "/admin/torneos.aspx";
        public const string aNOTICIAS = "/admin/noticias.aspx";
        public const string aFECHAS = "/admin/fechas.aspx";
        public const string aEDICIONES = "/admin/ediciones.aspx";
        public const string aSANCIONES = "/admin/sanciones.aspx";

        //Forms en carpeta Edición
        public const string eCONFIGURAR = "/admin/edicion/configurar.aspx";
        public const string eEQUIPOS = "/admin/edicion/equipos.aspx";
        public const string eFASES = "/admin/edicion/fases.aspx";
        public const string eCONFIRMAR = "/admin/edicion/confirmar.aspx";
        public const string eJUGADORES = "/admin/edicion/jugadores.aspx";

        //Forms en carpeta Usuario
        public const string uACTIVAR = "/usuario/activar.aspx";
        public const string uLOGIN = "/usuario/login.aspx";
        public const string uRECUPERARCONTRASENIA = "/usuario/recuperar-contrasenia.aspx";
        public const string uRECUPERAR = "/usuario/recuperar.aspx";
        public const string uREGISTRO = "/usuario/registro.aspx";
        public const string uMODIFICAR = "/usuario/modificar.aspx";

        //Forms en carpeta Torneo
        public static string urlPartido(string nickTorneo,int idEdicion, string idPartido)
        {
            return "/"+nickTorneo+"/edicion-"+idEdicion+"/partido-"+idPartido;
        }
        public static string urlTorneo(string nickTorneo)
        {
            return "/" + nickTorneo;
        }
        public static string urlEdicion(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion;
        }
        public static string urlEquipo(string nickTorneo, int idEdicion, int idEquipo)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/equipo-" + idEquipo;
        }
        public static string urlJugador(string nickTorneo, int idEdicion, int idEquipo, int idJugador)
        {
            idEquipo = (idEquipo != 0) ? idEquipo : new GestorJugador().obtenerIdEquipo(idJugador);
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/equipo-" + idEquipo + "/jugador-" + idJugador;
        }

        public static string urlFechas(string nickTorneo, int idEdicion, int idFase, int idFecha)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/fase-" + idFase + "/fecha-" + idFecha;
        }

        public static string urlGoleadores(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/goleadores";
        }

        public static string urlGoleadores(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/goleadores";
        }

        //Forms en el root 
        public const string rINDEX = "/index.aspx";

        //Carpetas Resources
        public const string rCSS = "/resources/css";
        public const string rJS = "/resources/js";
        public const string rIMG = "/resources/img";
    }
}
