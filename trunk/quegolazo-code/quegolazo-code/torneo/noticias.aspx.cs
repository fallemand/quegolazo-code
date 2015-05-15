using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;

namespace quegolazo_code.torneo
{
    public partial class Formulario_web15 : System.Web.UI.Page
    {
 
        protected GestorNoticia gestorNoticia;
        protected Noticia noticia;
        protected Torneo torneo;
        protected Edicion edicion;
        protected int idNoticia, idEdicion;
        protected string nickTorneo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    torneo = GestorUrl.validarTorneo();
                    edicion = GestorUrl.validarEdicion(torneo.nick);                     
                    nickTorneo = torneo.nick;
                    idEdicion = edicion.idEdicion;
                    cargarNoticias();
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }

        private void cargarNoticias()
        {
            List<Noticia> noticias = new GestorNoticia().obtenerListaDeNoticiasDeLaEdicion(edicion.idEdicion);
            if (noticias != null && noticias.Count > 0)
            {
                GestorControles.cargarRepeaterList(rptUltimasNoticias, noticias);
            }
            else
            {
                msjNoticias.Visible = true;
            }
        }
    }
}
