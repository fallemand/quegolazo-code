using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace Utils
{
    public class GestorImagen
    {
        private static string pathImagenes = System.Web.HttpContext.Current.Server.MapPath("/resources/img/");
        private static double tamanioMax = 1048576; // 1MB = 1048576bytes
        private static string extension = ".jpg";
        private static string pathTorneos = "torneos/";
        private static string pathTemp = "temp/";

        /// <summary>
        /// Guarda las imágenes validándolas. Recibe por parámetro el archivo, el id, y que tipo de image es (torneo, equipo, etc)
        /// autor: Facundo Allemand
        /// </summary>
        public static bool guardarImagenTorneo(HttpPostedFile file, int idTorneo) {
            if (!Directory.Exists(pathImagenes + pathTemp))
                Directory.CreateDirectory(pathImagenes + pathTemp);
            if (!Directory.Exists(pathImagenes + pathTorneos))
                Directory.CreateDirectory(pathImagenes + pathTorneos);
            string rutaImagenTemporal = pathImagenes + pathTemp + "img.temp";
            try
            {
                if (file.ContentLength > tamanioMax)
                    throw new Exception("El tamaño del archivo es mayor a 1024kb");
                file.SaveAs(rutaImagenTemporal);
                validarImagen(rutaImagenTemporal);
                //Definimos los tamaños de imagenes a crear
                List<Imagen> imagenes = new List<Imagen>();
                imagenes.Add(new Imagen() { abreviacion = "-sm", height = 50, width = 50 });
                imagenes.Add(new Imagen() { abreviacion = "-nm", height = 200, width = 200 });
                imagenes.Add(new Imagen() { abreviacion = "-bg", height = 500, width = 500 });
                foreach (Imagen imagen in imagenes)
                {
                    Bitmap img = CrearImagen(rutaImagenTemporal, imagen.width, imagen.height);
                    img.Save(pathImagenes + pathTorneos + idTorneo + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                System.IO.File.Delete(rutaImagenTemporal);
            }
        }

        /// <summary>
        /// Se encarga de borrar las imágenes. Recibe por parámetro elel id, y que tipo de image es (torneo, equipo, etc)
        /// autor: Facundo Allemand
        /// </summary>
        public static void borrrarImagenTorneo(int idTorneo)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(pathImagenes + pathTorneos, idTorneo + "*" + extension, System.IO.SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    foreach (string imagen in files)
                    {
                        System.IO.File.Delete(imagen);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Valida si un archivo es una imagen. No se fija en la extensión sino en el fichero.
        /// autor: Facundo Allemand
        /// </summary>
        public static void validarImagen(string rutaImagen)
        {
            Image img=null;
            try
            {
                using (img = Image.FromFile(rutaImagen))
                {
                    if (!img.RawFormat.Equals(ImageFormat.Bmp) &&
                        !img.RawFormat.Equals(ImageFormat.Gif) &&
                        !img.RawFormat.Equals(ImageFormat.Jpeg) &&
                        !img.RawFormat.Equals(ImageFormat.Png))
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("El formato de imagen no es válido");
            }
            finally
            {
                if (img != null)
                    img.Dispose();
            }
        }

        /// <summary>
        /// Crea una imagen del tamaño indicado a partir de otra imagen
        /// autor: Facundo Allemand
        /// </summary>
        public static Bitmap CrearImagen(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return bmpOut;
        }
    }

    /// <summary>
    /// Struct que define la abreviación de la imagen, el ancho y el alto.
    /// autor: Facundo Allemand
    /// </summary>
    public struct Imagen
    {
        public string abreviacion;
        public int width;
        public int height;
    }
}
