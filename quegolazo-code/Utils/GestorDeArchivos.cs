using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
    public class GestorDeArchivos
    {

        public void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio
            string ruta = @"T:\";

            // Si el directorio no existe, crearlo
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string archivo = String.Format("{0}\\{1}", ruta, file.FileName);

            // Verificar que el archivo no exista
            if (File.Exists(archivo))
            { }// error MensajeError(String.Format("Ya existe una imagen con nombre\"{0}\".", file.FileName));
            else
            {
                file.SaveAs(archivo);
            }
        }
        
                                       
    }
}
