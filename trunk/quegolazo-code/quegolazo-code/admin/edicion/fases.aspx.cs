using System;
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
       private static GestorEdicion gestorEdicion = new GestorEdicion();
       private static GestorFase gestorFase = new GestorFase();
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEdicion = Sesion.getGestorEdicion();
            //si no tiene mas de dos equipos y solo tiene una fase, lo manda a seleccionar equipos.
            if(gestorEdicion.edicion.equipos.Count<2 && gestorEdicion.edicion.fases.Count <2)
            Response.Redirect(GestorUrl.eEQUIPOS);
            //actualizamos la fase actual del gestor
            new GestorEdicion().actualizarFaseActual(gestorEdicion);

            if (!IsPostBack) {
               gestorFase.reducirFases(gestorEdicion.edicion.fases);
               string equipos = (new JavaScriptSerializer()).Serialize(gestorEdicion.edicion.equipos);
               string fases = (new JavaScriptSerializer()).Serialize(gestorEdicion.edicion.fases);
               //TODO aca el id de la edicion esta harcodeado debe ser reemplazado por el de la sesion cuando se defina desde donde va a llegar a la pantalla de conf de ediciones.
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#contenedorFases').generadorDeFases({ equiposDeLaEdicion: " + equipos + ", fases: " + fases + ", idEdicion:" + gestorEdicion.edicion.idEdicion + ", idFaseEditable:" + gestorEdicion.faseActual.idFase + "});", true);
             
            }
            
        }

        /// <summary>
        /// Siguiente paso: Confirmar
        /// </summary>
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eCONFIRMAR);
        }

        /// <summary>
        /// Paso anterior: Seleccionar equipos
        /// </summary>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect(GestorUrl.eEQUIPOS);
        }

        /// <summary>
        /// Guarda en sesión la configuración de fases
        /// autor: Antonio Herrera
        /// </summary>
        [WebMethod(enableSession : true)]        
        public static object guardarFases(object JSONFases)
        {
            try
            {
                JavaScriptSerializer serializador = new JavaScriptSerializer();
                List<Fase> fases = serializador.ConvertToType<List<Fase>>(JSONFases);
                gestorFase = Sesion.getGestorEdicion().gestorFase;
                gestorEdicion.edicion.fases = fases;             
                gestorFase.generarFixture(gestorEdicion.edicion.fases);                                           
                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex) 
            {
                return new HttpStatusCodeResult(500, "Ha ocurrido un error en el servidor: '"+ex.Message +"'");
            }
        }
    }
}