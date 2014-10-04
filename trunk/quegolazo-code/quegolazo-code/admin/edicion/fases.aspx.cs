﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;

namespace quegolazo_code.admin.edicion
{
    public partial class fases : System.Web.UI.Page
    {
       GestorEdicion gestorEdicion = new GestorEdicion();
       private static GestorFase gestorFase = new GestorFase();
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorFase = Sesion.getGestorFase();
            gestorEdicion = Sesion.getGestorEdicion();            
            string equipos = (new JavaScriptSerializer()).Serialize(gestorEdicion.edicion.equipos);
            if(!IsPostBack)
            //TODO aca el id de la edicion esta harcodeado debe ser reemplazado por el de la sesion cuando se defina desde donde va a llegar a la pantalla de conf de ediciones.
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#contenedorFases').generadorDeFases({ idEdicion:" + Sesion.getGestorEdicion().edicion.idEdicion + ", idTorneo:" + Sesion.getTorneo().idTorneo + " , equiposDeLaEdicion: " + equipos + "});", true);
            
        }             

        [WebMethod(enableSession : true)]        
        public static object guardarFases(object JSONFases)
        {
            try
            {
                JavaScriptSerializer serializador = new JavaScriptSerializer();
                List<Fase> fases = serializador.ConvertToType<List<Fase>>(JSONFases);
                gestorFase = Sesion.getGestorFase();
                gestorFase.fases = fases;
                gestorFase.generarFixture();
                Sesion.setGestorFase(gestorFase);                
                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Ha ocurrido un error, intenta nuevamente luego, si el problema persiste contactate con nuestro soporte tecnico. Detalle técnico :{"+ex.Message +"}");
            }
        }
          

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eCONFIRMAR);
        }

    }
}