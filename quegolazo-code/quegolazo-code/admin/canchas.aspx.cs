using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class canchas : System.Web.UI.Page
    {
        GestorCancha gestorCancha = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //si no existe gestor en Session lo carga
            if (Session["gestorCancha"] == null)
                Session["gestorCancha"] = new GestorCancha();
            //obtiene en gestor de la Session.
            gestorCancha = (GestorCancha)Session["gestorCancha"];

            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            Session["torneo"] = new Torneo
            {
                idTorneo = 88,
            };
            //TORNEO HARDCODEADOOO
            //TORNEO HARDCODEADOOO
            try
            {
                cargarRepeaterCanchas();
                imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.COMPLEJO, GestorImagen.MEDIANA);
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaCanchas(ex.Message);
            }
            limpiarPaneles();
        }

        /// <summary>
        /// Permite Registrar una Nueva Cancha para un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarCancha_Click(object sender, EventArgs e)
        {
            try
            {
                gestorCancha.registrarCancha(txtNombreCancha.Value, txtDomicilio.Value, txtTelefono.Value);
                GestorImagen.guardarImagen(gestorCancha.cancha.idCancha, GestorImagen.COMPLEJO);
                limpiarCamposCancha();                
                mostrarPanelExito("Cancha registrada con éxito!");
                cargarRepeaterCanchas();
                gestorCancha.cancha = null; // le setea null a la cancha
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
                txtNombreCancha.Focus();
            }
        }

        /// <summary>
        /// Método del Repeater para traer la Cancha a modificar o eliminar
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptCanchas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editarCancha")
                {   //por CommandArgument recibe el ID de la cancha a modificar               
                    gestorCancha.obtenerCanchaAModificar(Int32.Parse(e.CommandArgument.ToString()));
                    txtNombreCancha.Value = gestorCancha.cancha.nombre;
                    txtDomicilio.Value = gestorCancha.cancha.domicilio;
                    txtTelefono.Value = gestorCancha.cancha.telefono;
                    btnRegistrarCancha.Visible = false;
                    btnModificarCancha.Visible = true;
                    btnCancelarModificacionCancha.Visible = true;
                    imagenpreview.Src = gestorCancha.cancha.obtenerImagenMediana();
                }

                if (e.CommandName == "eliminarCancha")
                {   //por CommandArgument recibe el ID de la cancha a eliminar              
                    gestorCancha.eliminarCancha(Int32.Parse(e.CommandArgument.ToString()));
                    cargarRepeaterCanchas();
                    limpiarCamposCancha();
                }
            }
            catch (Exception ex)
            {
                mostrarPanelFracasoListaCanchas(ex.Message);                
            }            
        }

        /// <summary>
        /// Permite cancelar la modificación de la Cancha
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionCancha_Click(object sender, EventArgs e)
        {
            limpiarCamposCancha();
            btnRegistrarCancha.Visible = true;
            btnModificarCancha.Visible = false;
            btnCancelarModificacionCancha.Visible = false;
            gestorCancha.cancha = null; // le setea null a la cancha
        }

        /// <summary>
        /// Permite modificar una cancha
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarCancha_Click(object sender, EventArgs e)
        {
            try
            {
                int idCanchaAModificar = gestorCancha.cancha.idCancha;
                gestorCancha.modificarCancha(idCanchaAModificar, txtNombreCancha.Value, txtDomicilio.Value, txtTelefono.Value);
                GestorImagen.guardarImagen(idCanchaAModificar, GestorImagen.COMPLEJO);
                limpiarCamposCancha();
                mostrarPanelExito("Cancha modificada con éxito!");
                cargarRepeaterCanchas();
                gestorCancha.cancha = null; //le setea null a la cancha
                //lo manda a la solapa de agregar una cancha
                btnRegistrarCancha.Visible = true;
                btnModificarCancha.Visible = false;
                btnCancelarModificacionCancha.Visible = false;
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Permite la apertura de la ventana de confirmación de la eliminación de una cancha
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptCanchas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnk = (LinkButton)e.Item.FindControl("lnkEliminarCancha");
                lnk.Attributes.Add("onclick", "return confirm('¿Desea eliminar esta Cancha?');");
            }
        }   

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el repeater de canchas de un torneo
        /// autor: Pau Pedrosa
        /// </summary>
        private void cargarRepeaterCanchas()
        {
            rptCanchas.DataSource = gestorCancha.obtenerCanchasDeUnTorneo();
            rptCanchas.DataBind();
            sinCanchas.Visible = (rptCanchas.Items.Count > 0) ? false : true;
        }

        /// <summary>
        /// Habilita el panel de fracaso y deshabilita el panel de exito.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            litFracaso.Text = mensaje;
            panelExito.Visible = false;
            panelFracaso.Visible = true;
        }

        /// <summary>
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelExito(string mensaje)
        {
            litExito.Text = mensaje;
            panelExito.Visible = true;
            panelFracaso.Visible = false;
        }

        /// <summary>
        /// Habilita el panel de exito y deshabilita el panel de fracaso.
        /// autor: Pau Pedrosa
        /// </summary>
        private void mostrarPanelFracasoListaCanchas(string mensaje)
        {
            litFracasoListaCanchas.Text = mensaje;
            panelFracasoListaCanchas.Visible = true;
        }

        /// <summary>
        /// Limpia los campos de la cancha
        /// autor: Pau Pedrosa
        /// </summary>
        public void limpiarCamposCancha()
        {
            txtNombreCancha.Value = "";
            txtDomicilio.Value = "";
            txtTelefono.Value = "";
        }

        /// <summary>
        /// Limpia los paneles de éxito y fracaso
        /// autor: Pau Pedrosa
        /// </summary>
        private void limpiarPaneles()
        {
            panelExito.Visible = false;
            panelFracaso.Visible = false;
            panelFracasoListaCanchas.Visible = false;
            litFracaso.Text = "";
            litExito.Text = "";
            litFracasoListaCanchas.Text = "";            
        } 
    }
}