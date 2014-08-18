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
            if (usuario == null)
                throw new Exception("No se pudo obtener el usuario");
            return usuario;
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
        /// Setea el torneo en Session
        /// </summary>
        public static void setUsuario(Usuario usuario)
        {
            System.Web.HttpContext.Current.Session["usuario"] = usuario;
        }
        /// <summary>
        /// si no existe gestorEquipo en Session, lo carga
        /// </summary>
        /// <param name="gestorEquipo">El gesor a cargar en sesion</param>
        public static void setGestorEquipo()
        {           
            if (System.Web.HttpContext.Current.Session["gestorEquipo"] == null)
                System.Web.HttpContext.Current.Session["gestorEquipo"] = new GestorEquipo();
        }
        /// <summary>
        /// Devuelve el objeto GestorEquipo que esta en la sesion.
        /// </summary>       
        public static GestorEquipo getGestorEquipo()
        {
            return (GestorEquipo)System.Web.HttpContext.Current.Session["gestorEquipo"];               
        }
        /// <summary>
        /// Devuelve el objeto GestorTorneo que se encuentra en Session.
        /// </summary>
        /// <returns></returns>
        public static GestorTorneo getGestorTorneo() {           
           return (GestorTorneo)System.Web.HttpContext.Current.Session["gestorTorneo"];
        }
        /// <summary>
        /// Si no se encuentra el objeto GestorTorneo en Session, lo carga.
        /// </summary>
        public static void setGestorTorneo() {
            if (System.Web.HttpContext.Current.Session["gestorTorneo"] == null)
                System.Web.HttpContext.Current.Session["gestorTorneo"] = new GestorTorneo();
        }
        /// <summary>
        /// si no existe gestorEdicion en Session, lo carga
        /// </summary>
        public static void setGestorEdicion() {
            if (System.Web.HttpContext.Current.Session["gestorEdicion"] == null)
                System.Web.HttpContext.Current.Session["gestorEdicion"] = new GestorEdicion();
        }
        /// <summary>
        /// devuelve el objeto GestorEdicion que se encuentra en Session.
        /// </summary>        
        public static GestorEdicion getGestorEdicion() {
            return (GestorEdicion)System.Web.HttpContext.Current.Session["gestorEdicion"];
        }
        /// <summary>
        /// si el gestor cancha no se encuentra en Session, lo crea.
        /// </summary>
        public static void setGestorCancha() {
            if (System.Web.HttpContext.Current.Session["gestorCancha"] == null)
                System.Web.HttpContext.Current.Session["gestorCancha"] = new GestorCancha();
        }
        /// <summary>
        /// Devuelve el objeto cancha que se encuentra en Session
        /// </summary>        
        public static GestorCancha getGestorCancha() {
            return (GestorCancha)System.Web.HttpContext.Current.Session["gestorCancha"];
        }
    }
}
