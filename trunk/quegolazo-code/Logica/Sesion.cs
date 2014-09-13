using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Logica
{
    public static class Sesion
    {
        /// <summary>
        /// Obtiene el usuario de Session
        /// </summary>
        public static Usuario getUsuario()
        {
            Usuario usuario = (Usuario)System.Web.HttpContext.Current.Session["usuario"];
            //if (usuario == null)
            //    throw new Exception("No se pudo obtener el usuario");
            return usuario;
        }
        /// <summary>
        /// Obtiene la edicion de Session
        /// </summary>
        public static Edicion getEdicion()
        {
            Edicion edicion = (Edicion)System.Web.HttpContext.Current.Session["edicion"];
            if (edicion == null)
                throw new Exception("No se pudo obtener la edición");
            return edicion;
        }
        /// <summary>
        /// Obtiene el equipo de Session
        /// </summary>
        public static Equipo getEquipo()
        {
            Equipo equipo = (Equipo)System.Web.HttpContext.Current.Session["equipo"];
            if (equipo == null)
                throw new Exception("No se pudo obtener el equipo");
            return equipo;
        }
        /// <summary>
        /// Obtiene el torneo de Session
        /// </summary>
        public static Torneo getTorneo()
        {
            Torneo torneo = (Torneo)System.Web.HttpContext.Current.Session["torneo"];
            if (torneo == null)
                throw new Exception("No se pudo obtener el torneo");
            return torneo;
        }
        /// <summary>
        /// Setea el torneo en Session
        /// </summary>
        public static void setTorneo(Torneo torneo)
        {
            System.Web.HttpContext.Current.Session["torneo"] = torneo;
        }
        /// <summary>
        /// Setea la edicion en Session
        /// </summary>
        public static void setEdicion(Edicion edicion)
        {
            System.Web.HttpContext.Current.Session["edicion"] = edicion;
        }
        /// <summary>
        /// Setea el torneo en Session
        /// </summary>
        public static void setUsuario(Usuario usuario)
        {
            System.Web.HttpContext.Current.Session["usuario"] = usuario;
        }
        /// <summary>
        /// Setea el equipo en Session
        /// </summary>
        public static void setEquipo(Equipo equipo)
        {
            System.Web.HttpContext.Current.Session["equipo"] = equipo;
        }       
        /// <summary>
        /// Devuelve el objeto GestorEquipo que esta en la sesion, si es nulo, crea uno nuevo.
        /// </summary>       
        public static GestorEquipo getGestorEquipo()
        {
            if (System.Web.HttpContext.Current.Session["gestorEquipo"] == null)
                System.Web.HttpContext.Current.Session["gestorEquipo"] = new GestorEquipo();
            return (GestorEquipo)System.Web.HttpContext.Current.Session["gestorEquipo"];               
        }
        /// <summary>
        /// Devuelve el objeto GestorTorneo que se encuentra en Session, si es nulo, crea uno nuevo.
        /// </summary>
        /// <returns></returns>
        public static GestorTorneo getGestorTorneo() {
            if (System.Web.HttpContext.Current.Session["gestorTorneo"] == null)
                System.Web.HttpContext.Current.Session["gestorTorneo"] = new GestorTorneo();
            return (GestorTorneo)System.Web.HttpContext.Current.Session["gestorTorneo"];
        }    
        /// <summary>
        /// devuelve el objeto GestorEdicion que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorEdicion getGestorEdicion() {
            if (System.Web.HttpContext.Current.Session["gestorEdicion"] == null)
                System.Web.HttpContext.Current.Session["gestorEdicion"] = new GestorEdicion();
            return (GestorEdicion)System.Web.HttpContext.Current.Session["gestorEdicion"];
        }   
        /// <summary>
        /// Devuelve el objeto cancha que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorCancha getGestorCancha() {
            if (System.Web.HttpContext.Current.Session["gestorCancha"] == null)
                System.Web.HttpContext.Current.Session["gestorCancha"] = new GestorCancha();
            return (GestorCancha)System.Web.HttpContext.Current.Session["gestorCancha"];
        }
        /// <summary>
        /// Devuelve el objeto GestorJugador que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorJugador getGestorJugador()
        {
            if (System.Web.HttpContext.Current.Session["gestorJugador"] == null)
                System.Web.HttpContext.Current.Session["gestorJugador"] = new GestorJugador();
            return (GestorJugador)System.Web.HttpContext.Current.Session["gestorJugador"];
        }
        /// <summary>
        /// Devuelve el objeto GestorArbitro que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorArbitro getGestorArbitro()
        {
            if (System.Web.HttpContext.Current.Session["gestorArbitro"] == null)
                System.Web.HttpContext.Current.Session["gestorArbitro"] = new GestorArbitro();
            return (GestorArbitro)System.Web.HttpContext.Current.Session["gestorArbitro"];
        }
        /// <summary>
        /// Devuelve el objeto GestorNoticia que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorNoticia getGestorNoticia()
        {
            if (System.Web.HttpContext.Current.Session["gestorNoticia"] == null)
                System.Web.HttpContext.Current.Session["gestorNoticia"] = new GestorNoticia();
            return (GestorNoticia)System.Web.HttpContext.Current.Session["gestorNoticia"];
        }
        /// <summary>
        /// Devuelve el objeto GestorUsuario que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorUsuario getGestorUsuario()
        {
            if (System.Web.HttpContext.Current.Session["gestorUsuario"] == null)
                System.Web.HttpContext.Current.Session["gestorUsuario"] = new GestorUsuario();
            return (GestorUsuario)System.Web.HttpContext.Current.Session["gestorUsuario"];
        }

        /// <summary>
        /// devuelve el objeto GestorFase que se encuentra en Session, si es nulo, crea uno nuevo
        /// </summary>        
        public static GestorFase getGestorFase()
        {
            if (System.Web.HttpContext.Current.Session["gestorFase"] == null)
                System.Web.HttpContext.Current.Session["gestorFase"] = new GestorFase();
            return (GestorFase)System.Web.HttpContext.Current.Session["gestorFase"];
        }

        /// <summary>
        /// Setea el gestorEdición en Session
        /// </summary>
        public static void setGestorEdicion(GestorEdicion gestorEdicion)
        {
            System.Web.HttpContext.Current.Session["gestorEdicion"] = gestorEdicion;
        }
        /// <summary>
        /// Setea el gestorFase en Session
        /// </summary>
        public static void setGestorFase(GestorFase gestorFase)
        {
            System.Web.HttpContext.Current.Session["gestorFase"] = gestorFase;
        }       

    }
}
