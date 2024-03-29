﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Utils;

namespace quegolazo_code.admin
{
    public partial class noticias : System.Web.UI.Page
    {
       public GestorNoticia gestorNoticia;
       public GestorEdicion gestorEdicion;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gestorEdicion = Sesion.getGestorEdicion();
                gestorNoticia = Sesion.getGestorNoticia();               

                if (!Page.IsPostBack)
                {
                    cargarComboEdiciones();
                    cargarRepeaterNoticias();                    
                    cargarComboCategoriasNoticia();
                    imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.NOTICIA, GestorImagen.MEDIANA);
                }
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Obtiene la Edición de Sesión
        /// autor: Facu Allemand
        /// </summary>
        private void obtenerEdiciónSeleccionada()
        {
            if (gestorEdicion.edicion != null && gestorEdicion.edicion.idEdicion > 0)
            {
                gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(gestorEdicion.edicion.idEdicion);
            }
        }

        /// <summary>
        /// Registra en la BD una nueva noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnRegistrarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.registrarNoticia(txtTituloNoticia.Value, txtDescripcionNoticia.Text, gestorEdicion.edicion.idEdicion.ToString(), ddlCategoriaNoticia.SelectedValue);
                GestorImagen.guardarImagen(gestorNoticia.noticia.idNoticia, GestorImagen.NOTICIA);
                limpiarCamposNoticias();
                cargarRepeaterNoticias();
                GestorError.mostrarPanelExito("Se registró exitosamente la noticia");
                gestorNoticia.noticia = null; // le setea null a la noticia
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarNoticia');", true);
                
            }
            catch (Exception ex)
            {
                mostrarPanelFracaso(ex.Message);
            }
        }

        /// <summary>
        /// Modificar o Eliminar una noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void rptNoticias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                gestorNoticia.obtenerNoticiaPorId(Int32.Parse(e.CommandArgument.ToString()));
                if (e.CommandName == "editarNoticia")
                {
                    txtTituloNoticia.Value = gestorNoticia.noticia.titulo;
                    txtDescripcionNoticia.Text = gestorNoticia.noticia.descripcion;
                    btnRegistrarNoticia.Visible = false;
                    btnModificarNoticia.Visible = true;
                    btnCancelarModificacionNoticia.Visible = true;
                    imagenpreview.Src = gestorNoticia.noticia.obtenerImagenMediana();
                }
                if (e.CommandName == "eliminarNoticia")
                {
                    litTituloNoticia.Text = gestorNoticia.noticia.titulo;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('eliminarNoticia');", true);
                }
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message); }
        }

        /// <summary>
        /// Modifica en la BD una noticia existente
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnModificarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.modificarNoticia(gestorNoticia.noticia.idNoticia, txtTituloNoticia.Value, txtDescripcionNoticia.Text, ddlCategoriaNoticia.SelectedValue);
                limpiarCamposNoticias();
                cargarRepeaterNoticias();
                gestorNoticia.noticia = null;
                //lo manda a la solapa de agregar una noticia
                btnRegistrarNoticia.Visible = true;
                btnModificarNoticia.Visible = false;
                btnCancelarModificacionNoticia.Visible = false;
                GestorError.mostrarPanelExito("Se modificó exitosamente la noticia");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarNoticia');", true);
            }
            catch (Exception ex){mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Eliminar una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                gestorNoticia.eliminarNoticia(gestorNoticia.noticia.idNoticia);
                cargarRepeaterNoticias();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "eliminarArbitro", "closeModal('eliminarNoticia');", true);
            }
            catch (Exception ex){
                imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.NOTICIA, GestorImagen.MEDIANA);
                mostrarPanelFracaso(ex.Message);}
        }

        /// <summary>
        /// Cancela la Modificación de la noticia
        /// autor: Pau Pedrosa
        /// </summary>
        protected void btnCancelarModificacionNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposNoticias();
                btnRegistrarNoticia.Visible = true;
                btnModificarNoticia.Visible = false;
                btnCancelarModificacionNoticia.Visible = false;
                gestorNoticia.noticia = null;// le setea null a la noticia
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideOnMobile", "hideOnMobile('agregarNoticia');", true);
            }
            catch (Exception ex) { 
                mostrarPanelFracaso(ex.Message);
                imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.NOTICIA, GestorImagen.MEDIANA);
            }
        }

        //------------------------------------------
        //--------------Metodos Extras--------------
        //------------------------------------------
        /// <summary>
        /// Carga el Repeater de noticias
        /// </summary>
        private void cargarRepeaterNoticias()
        {
            sinNoticias.Visible = (GestorControles.cargarRepeaterTable(rptNoticias, gestorNoticia.obtenerNoticias(Sesion.getGestorEdicion().edicion.idEdicion))) ?
                false : true;
        }
        private void cargarComboCategoriasNoticia()
        {
            GestorControles.cargarComboList(ddlCategoriaNoticia, gestorNoticia.obtenerCategoriasNoticia(), "idCategoriaNoticia", "nombre");
        }
        /// <summary>
        /// Limpia los campos de noticia
        /// </summary>
        public void limpiarCamposNoticias()
        {
            txtTituloNoticia.Value = "";
            txtDescripcionNoticia.Text = "";
            imagenpreview.Src = GestorImagen.obtenerImagenDefault(GestorImagen.NOTICIA, GestorImagen.MEDIANA);
        }
        /// <summary>
        /// Panel Fracaso
        /// </summary>
        private void mostrarPanelFracaso(string mensaje)
        {
            GestorError.mostrarPanelFracaso(mensaje);
        }
        
        /// <summary>
        /// Carga Combos de Edicion
        /// </summary>
        public void cargarComboEdiciones()
        {
            panelSinEdiciones.Visible = !GestorControles.cargarComboList(ddlEdiciones, gestorEdicion.obtenerEdicionesPorTorneo(Sesion.getTorneo().idTorneo),
                "idEdicion", "nombre", "Seleccionar Edicion", false);
            ddlEdiciones.SelectedValue = (gestorEdicion.edicion.idEdicion > 0) ? gestorEdicion.edicion.idEdicion.ToString() : "";
        }

        
        protected void btnSeleccionarEdicion_Click(object sender, EventArgs e)
        {
            int idEdicion = Validador.castInt(ddlEdiciones.SelectedValue);
            gestorEdicion.edicion = gestorEdicion.obtenerEdicionPorId(Validador.castInt(ddlEdiciones.SelectedValue));
            cargarRepeaterNoticias(); 
        }
    }
}