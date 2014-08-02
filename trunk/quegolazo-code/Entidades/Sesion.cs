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
        public static Usuario obtenerUsuario()
        {
            Usuario usuario = (Usuario)System.Web.HttpContext.Current.Session["usuario"];
            if (usuario == null)
                throw new Exception("No se pudo obtener el usuario");
            return usuario;
        }

        /// <summary>
        /// Obtiene el torneo de Session
        /// </summary>
        public static Torneo obtenerTorneo()
        {
            Torneo torneo = (Torneo)System.Web.HttpContext.Current.Session["torneo"];
            if (torneo == null)
                throw new Exception("No se pudo obtener el torneo");
            return torneo;
        }
    }
}
