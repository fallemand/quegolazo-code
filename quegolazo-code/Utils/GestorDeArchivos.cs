using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Utils
{
    public class GestorDeArchivos
    {
        
        public void guardarImagen(HttpPostedFile file, string ruta, string nombreConExtension)
        {
            try
            {
                //Si el directorio no existe, crearlo
                if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);
                string archivo = String.Format("{0}\\{1}", ruta, nombreConExtension);               
                //Verificar que el archivo no exista
                if (File.Exists(archivo))
                { }// error MensajeError(String.Format("Ya existe una imagen con nombre\"{0}\".", file.FileName));
                else
                {
                    file.SaveAs(archivo);
                }
            }
            catch (Exception)
            {
                
                throw new Exception("No se pudo guardar la imagen. Por favor verifique la integridad del archivo e intente nuevamente.");
            }
           
        }
   
        ///<summary>
        ///Compara el encabezado del archivo para verificar si es una imagen valida y no otro tipo de archivo.
        ///</summary>
        ///<param name="bytes">los bytes que componen la imagen</param>
        ///<returns>True si es una imagen valida, false si no lo es.</returns>
        public  bool validarImagen(HttpPostedFile postedFile)
        {
            const int ImageMinimumBytes = 512;
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                throw new Exception("El archivo que desea subir no es una imagen valida.");
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                throw new Exception("El archivo que desea subir no es una imagen valida.");
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    throw new Exception("El archivo que desea subir no es una imagen valida.");
                }

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    throw new Exception("El archivo que desea subir no es una imagen valida.");
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    throw new Exception("El archivo que desea subir no es una imagen valida.");
                }
            }
            catch (Exception)
            {
                throw new Exception("El archivo que desea subir no es una imagen valida.");
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                throw  new Exception("El archivo que desea subir no es una imagen valida.");
            }

            return true;
        }
                                       
    }
}
