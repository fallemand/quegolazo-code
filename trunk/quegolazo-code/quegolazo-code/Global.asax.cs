using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Logica;
using System.Security.Principal;

namespace quegolazo_code
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    GestorUsuario gestorUsuario = new GestorUsuario();
                    string[] roles = gestorUsuario.obtenerRolesDelUsuario(HttpContext.Current.User.Identity.Name);
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    HttpContext.Current.User = new GenericPrincipal(id, roles);
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}