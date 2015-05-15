using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public const string t404 = "/torneo/404.aspx";
        public static string urlPartido(string nickTorneo,int idEdicion, string idPartido)
        {
            return "/"+nickTorneo+"/edicion-"+idEdicion+"/partido-"+idPartido;
        }
        public static string urlEdiciones(string nickTorneo)
        {
            return "/" + nickTorneo + "/ediciones";
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

        public static string urlFechasGenerico(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/fechas";
        }

        public static string urlFechasFase(string nickTorneo, int idEdicion, int idFase)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/fase-" + idFase;
        }

        public static string urlGoleadores(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/goleadores";
        }
        public static string urlFixture(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/fixture";
        }
        public static string urlPosiciones(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/posiciones";
        }
        public static string urlSanciones(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/sanciones";
        }
        public static string urlEquipos(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/equipos";
        }
        public static string urlNoticias(string nickTorneo, int idEdicion)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/noticias";
        }
        public static string urlNoticia(string nickTorneo, int idEdicion, int idNoticia)
        {
            return "/" + nickTorneo + "/edicion-" + idEdicion + "/noticia-" + idNoticia;
        }
        //Forms en el root 
        public const string rINDEX = "/index.aspx";

        //Carpetas Resources
        public const string rCSS = "/resources/css";
        public const string rJS = "/resources/js";
        public const string rIMG = "/resources/img";

        //==================================================================
        //-------------------------Validaciones---------------------------
        //==================================================================

        //Validar Torneo
        public static Torneo validarTorneo() 
        {
            String nickTorneo = HttpContext.Current.Request["nickTorneo"];
            if (nickTorneo == null)
                HttpContext.Current.Response.Redirect(GestorUrl.t404);
            GestorTorneo gestorTorneo = new GestorTorneo();
            Torneo torneo = new GestorTorneo().obtenerTorneoPorNick(nickTorneo);
            if (torneo == null)
                HttpContext.Current.Response.Redirect(GestorUrl.t404);
            return torneo;
        }

        //Validar Edicion
        public static Edicion validarEdicion(string nickTorneo)
        {
            int idEdicion=-1;
            try { 
                idEdicion = int.Parse(HttpContext.Current.Request["idEdicion"]); 
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(GestorUrl.urlEdiciones(nickTorneo));
            }
            GestorEdicion gestorEdicion = new GestorEdicion();
            Edicion edicion = gestorEdicion.obtenerEdicionPorId(idEdicion);
            if (edicion == null)
                HttpContext.Current.Response.Redirect(GestorUrl.urlEdiciones(nickTorneo));
            return edicion;
        }

        //Validar Fase
        public static int validarFase(string nickTorneo, int idEdicion)
        {
            try
            {
                int idFase = int.Parse(HttpContext.Current.Request["idFase"]);
                return idFase;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        //Validar Fecha
        public static int validarFecha(string nickTorneo, int idEdicion, int idFase)
        {
            try
            {
                int idFecha = int.Parse(HttpContext.Current.Request["idFecha"]);
                return idFecha;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        //Validar Equipo
        public static Equipo validarEquipo(string nickTorneo, int idEdicion)
        {
            int idEquipo = -1;
            try
            {
                idEquipo = int.Parse(HttpContext.Current.Request["idEquipo"]);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(GestorUrl.urlEquipos(nickTorneo, idEdicion));
            }
            GestorEquipo gestorEquipo = new GestorEquipo();
            Equipo equipo = gestorEquipo.obtenerEquipoPorId(idEquipo);
            if (equipo == null)
                HttpContext.Current.Response.Redirect(GestorUrl.urlEquipos(nickTorneo, idEdicion));
            return equipo;
        }

        //Validar Partido
        public static Partido validarPartido(string nickTorneo, int idEdicion)
        {
            Partido partido = null;
            try
            {
                int idPartido = int.Parse(HttpContext.Current.Request["idPartido"]);
                GestorPartido gestorPartido = new GestorPartido();
                gestorPartido.obtenerPartidoporId(idPartido.ToString());
                partido = gestorPartido.partido;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(GestorUrl.urlFixture(nickTorneo, idEdicion));
            }
            return partido;
        }

        //Validar Jugador
        public static Jugador validarJugador(string nickTorneo, int idEdicion, int idEquipo)
        {
            int idJugador = -1;
            try
            {
                idJugador = int.Parse(HttpContext.Current.Request["idJugador"]);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(GestorUrl.urlEquipo(nickTorneo, idEdicion, idEquipo));
            }
            GestorJugador gestorJugador = new GestorJugador();
            Jugador jugador = gestorJugador.obtenerJugadorPorId(idJugador);
            if (jugador == null)
                HttpContext.Current.Response.Redirect(GestorUrl.urlEquipo(nickTorneo, idEdicion, idEquipo));
            return jugador;
        }
        //Validar Equipo
        public static Noticia validarNoticia(string nickTorneo, int idEdicion)
        {
            int idNoticia = -1;
            try
            {
                idNoticia = int.Parse(HttpContext.Current.Request["idNoticia"]);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(GestorUrl.urlNoticias(nickTorneo, idEdicion));
            }
            GestorNoticia gestorNoticia = new GestorNoticia();
            gestorNoticia.obtenerNoticiaPorId(idNoticia);
            if (gestorNoticia.noticia == null)
                HttpContext.Current.Response.Redirect(GestorUrl.urlNoticias(nickTorneo, idEdicion));
            return gestorNoticia.noticia;
        }
        
    }
}
