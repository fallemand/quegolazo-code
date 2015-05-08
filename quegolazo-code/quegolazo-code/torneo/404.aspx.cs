using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;
using Entidades;

namespace quegolazo_code.torneo
{
    public partial class ediciones2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GestorTorneo gestorTorneo = new GestorTorneo();
            if (!Page.IsPostBack)     
            {
                List<Torneo> listaTorneos = gestorTorneo.obtenerTorneos();
                litCantTorneos.Text = listaTorneos.Count.ToString();
                GestorControles.cargarRepeaterList(rptTorneos, gestorTorneo.obtenerTorneos());
            }
        }
    }
}