using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Utils;

namespace quegolazo_code.admin
{
    /// <summary>
    /// Permite subir una imagen sin realizar postback
    /// </summary>
    public class AjaxFileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string msg = "{";
            try
            {
                if (context.Request.Files.Count > 0 && context.Request.Files[0].ContentLength>0)
                {
                    GestorImagen.guardarImagenTemporal(context.Request.Files[0]);
                    msg += string.Format("msg:'{0}'\n", "Imagen Correta!");
                }
            }
            catch (Exception ex)
            {
                msg += (ex.GetType().Name.Equals("HttpException")) ? string.Format("error:'{0}',\n", "El tamaño de la imagen es muy grande!") : string.Format("error:'{0}',\n", ex.Message);
            }
            finally
            {
                msg += "}";
                context.Response.Write(msg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}