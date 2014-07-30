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
        private static double tamanioMax = 1048576; // 1MB = 1048576bytes
        private static string extension = ".jpg";

        //Paths de Imágenes
        private static string pathImagenes = System.Web.HttpContext.Current.Server.MapPath("/resources/img/");
        private static string pathTorneos = "torneos/";
        private static string pathEquipos = "equipos/";
        private static string pathComplejos = "complejos/";
        private static string pathJugadores = "jugadores/";
        private static string pathTemp = "temp/";

        //Tipos de Imágenes
        public const int TORNEO = 1;
        public const int EQUIPO = 2;
        public const int COMPLEJO = 3;
        public const int JUGADOR = 4;

        /// <summary>
        /// Representa los 3 tamaños de imagenes que se guardan en el sistema.
        /// Autor: Antonio Herrera
        /// </summary>
        public enum enumDimensionImagen
        {
            GRANDE, MEDIANA, CHICA
        }

        /// <summary>
        /// Guarda las imágenes validándolas. Recibe por parámetro el archivo, el id, y que tipo de image es (torneo, equipo, etc)
        /// autor: Facundo Allemand
        /// </summary>
        public static bool guardarImagenTorneo(HttpPostedFile file, int id, int tipoImagen) {
            if (file.ContentLength > 0)
            {
                crearDirectorios();
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
                        switch (tipoImagen)
                        {
                            case TORNEO: img.Save(pathImagenes + pathTorneos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case EQUIPO: img.Save(pathImagenes + pathEquipos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case COMPLEJO: img.Save(pathImagenes + pathComplejos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case JUGADOR: img.Save(pathImagenes + pathJugadores + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        }
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
            return false;
        }

        private static void crearDirectorios()
        {
            if (!Directory.Exists(pathImagenes + pathTemp))
                Directory.CreateDirectory(pathImagenes + pathTemp);
            if (!Directory.Exists(pathImagenes + pathTorneos))
                Directory.CreateDirectory(pathImagenes + pathTorneos);
            if (!Directory.Exists(pathImagenes + pathEquipos))
                Directory.CreateDirectory(pathImagenes + pathEquipos);
            if (!Directory.Exists(pathImagenes + pathComplejos))
                Directory.CreateDirectory(pathImagenes + pathComplejos);
            if (!Directory.Exists(pathImagenes + pathJugadores))
                Directory.CreateDirectory(pathImagenes + pathJugadores);
        }

        /// <summary>
        /// Se encarga de borrar las imágenes. Recibe por parámetro elel id, y que tipo de image es (torneo, equipo, etc)
        /// autor: Facundo Allemand
        /// </summary>
        public static void borrrarImagenTorneo(int id, int tipoImagen)
        {
            try
            {
                string[] files = null;
                switch (tipoImagen)
                {
                    case TORNEO: files = System.IO.Directory.GetFiles(pathImagenes + pathTorneos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case EQUIPO: files = System.IO.Directory.GetFiles(pathImagenes + pathEquipos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case COMPLEJO: files = System.IO.Directory.GetFiles(pathImagenes + pathComplejos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case JUGADOR: files = System.IO.Directory.GetFiles(pathImagenes + pathJugadores, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                }
                if (files.Length > 0)
                {
                    foreach (string imagen in files)
                        System.IO.File.Delete(imagen);
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

        /// <summary>
        /// Veifica si la imagen esta disponible en el sistema de archivos para ser mostrada
        /// Autor: Antonio Herrera
        /// </summary>
        /// <param name="ubicacionDeLaImagen">Ruta de la imagen a verificar</param>
        /// <returns>True si existe, false en caso contrario</returns>
        public static bool existeImagen(string ubicacionDeLaImagen) {
            return File.Exists(ubicacionDeLaImagen);
        }

        /// <summary>
        /// Dveuelve la abreviatura correspondiente al nombre de la imagen segun su tamaño.
        /// Autor: Antonio Herrera
        /// </summary>
        /// <param name="dimension">El tamaño de la imagen que se desea obtener</param>
        /// <returns>un string con la abreviatura correspondiente</returns>
        public static string devolverAbreviaturaDeImagen(enumDimensionImagen dimension)
        {
            switch (dimension)
            {
                case enumDimensionImagen.GRANDE:
                    return "-bg";
                case enumDimensionImagen.MEDIANA:
                    return "-nm";
                case enumDimensionImagen.CHICA:
                    return "-sm";
                default:
                    return "-bg";
            }
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
