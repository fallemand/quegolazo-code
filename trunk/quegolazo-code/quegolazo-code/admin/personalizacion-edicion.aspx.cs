using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace quegolazo_code.admin
{
    public partial class personalizacion_edicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["idEdicion"] = 14;
            
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            GestorEdicion gestorEdicion = new GestorEdicion();

            gestorEdicion.edicion.idEdicion= int.Parse(Session["idEdicion"].ToString());

            if (rbJugadores_si.Checked)
                gestorEdicion.edicion.preferencias.jugadores = true;
            if (rb3_si.Checked)
                gestorEdicion.edicion.preferencias.golesJugadores = true;
            if (rb4_si.Checked)
                gestorEdicion.edicion.preferencias.tarjetasJugadores = true;
            if (rb2_si.Checked)
                gestorEdicion.edicion.preferencias.cambiosJugadores = true;
            if (rbArbitros_si.Checked)
            {
                gestorEdicion.edicion.preferencias.arbitros = true;
                gestorEdicion.edicion.preferencias.cantidadArbitros=int.Parse(txt_cantidadArbitros.Text);
            }
            if (rb7_si.Checked)
                gestorEdicion.edicion.preferencias.asignaArbitros = true;
            if (rb8_si.Checked)
                gestorEdicion.edicion.preferencias.desempenioArbitros = true;
            if (rbSanciones_si.Checked)
                gestorEdicion.edicion.preferencias.sanciones= true;
            if (rb6_si.Checked)
                gestorEdicion.edicion.preferencias.sancionesJugadores= true;
            if (rb_ComplejosEdicion.Checked)
                gestorEdicion.edicion.preferencias.canchaUnica = true;

            gestorEdicion.registrarConfiguraciones();


        }

        protected void rbJugadores_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJugadores_si.Checked)
            {
                Panel_jugadores.Visible = true;
            }
            else
            {
                Panel_jugadores.Visible = false;
            }
        }

        protected void rbSanciones_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSanciones_si.Checked)
            {
                Panel_sanciones.Visible = true;
            }
            else
            {
                Panel_sanciones.Visible = false;
            }
        }

        protected void rbArbitros_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArbitros_si.Checked)
            {
                Panel_Arbitros.Visible = true;
            }
            else
            {
                Panel_Arbitros.Visible = false;
            }

        }

        protected void rbCanchas_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCanchas_si.Checked)
            {
                Panel_Canchas.Visible = true;
            }
            else
            {
                Panel_Canchas.Visible = false;
            }
        }

       




    }
}