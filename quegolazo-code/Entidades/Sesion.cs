using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
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
            System.Web.HttpContext.Current.Session["torneo"]=torneo;
        }

        /// <summary>
        /// Setea el torneo en Session
        /// </summary>
        public static void setUsuario(Usuario usuario)
        {
            System.Web.HttpContext.Current.Session["usuario"] = usuario;
        }
    }
}
