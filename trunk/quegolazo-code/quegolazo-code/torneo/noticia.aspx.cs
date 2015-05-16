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
    public partial class Formulario_web14 : System.Web.UI.Page
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
                    noticia = GestorUrl.validarNoticia(torneo.nick, edicion.idEdicion);  
                    nickTorneo = torneo.nick;
                    idEdicion = edicion.idEdicion;
                    GestorNoticia gestorNoticia = new GestorNoticia();
                    GestorControles.cargarRepeaterList(rptUltimasNoticias, (gestorNoticia.obtenerNoticiasXCategoria(edicion.idEdicion, noticia.categoria.idCategoriaNoticia).Count > 2) ? gestorNoticia.obtenerNoticiasXCategoria(edicion.idEdicion, noticia.categoria.idCategoriaNoticia).AsEnumerable().Take(3).ToList() : gestorNoticia.obtenerNoticiasXCategoria(edicion.idEdicion, noticia.categoria.idCategoriaNoticia));
                }
            }
            catch (Exception ex) { GestorError.mostrarPanelFracaso(ex.Message); }
        }
    }
}