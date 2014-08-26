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
        private static string pathImagenes = "/resources/img/";
        private static string pathImagenesDisco = System.Web.HttpContext.Current.Server.MapPath(pathImagenes);
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

        //Tamaños de Imagen
        public const string CHICA = "-sm";
        public const string MEDIANA = "-nm";
        public const string GRANDE = "-bg";

        /// <summary>
        /// Crea imágenes a partir de la imagen temporal, las guarda en sus respectivos directorios y luego elimina la imagen temporal.
        /// autor: Facundo Allemand
        /// </summary>
        public static void guardarImagen(int id, int tipoImagen)
        {
            string pathImagenTemporal = pathImagenesDisco + pathTemp + System.Web.HttpContext.Current.Session.SessionID + extension;
            try
            {
                crearDirectorios();
                if (File.Exists(pathImagenTemporal))
                {
                    //Definimos los tamaños de imagenes a crear
                    List<Imagen> imagenes = new List<Imagen>();
                    imagenes.Add(new Imagen() { abreviacion = CHICA, height = 50, width = 50 });
                    imagenes.Add(new Imagen() { abreviacion = MEDIANA, height = 200, width = 200 });
                    imagenes.Add(new Imagen() { abreviacion = GRANDE, height = 500, width = 500 });
                    foreach (Imagen imagen in imagenes)
                    {
                        Bitmap img = CrearImagen(pathImagenTemporal, imagen.width, imagen.height);
                        switch (tipoImagen)
                        {
                            case TORNEO: img.Save(pathImagenesDisco + pathTorneos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case EQUIPO: img.Save(pathImagenesDisco + pathEquipos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case COMPLEJO: img.Save(pathImagenesDisco + pathComplejos + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case JUGADOR: img.Save(pathImagenesDisco + pathJugadores + id + imagen.abreviacion + extension, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        }
                        img.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                System.IO.File.Delete(pathImagenTemporal);
            }
        }

        /// <summary>
        /// Guarda la imagen en un directorio temporal con el nombre del id de session.
        /// autor: Facundo Allemand
        /// </summary>
        public static void guardarImagenTemporal(HttpPostedFile file, string idSesion)
        {
            if (file != null && file.ContentLength > 0)
            {
                crearDirectorios();
                string rutaImagenTemporal = pathImagenesDisco + pathTemp + idSesion + extension;
                try
                {
                    file.SaveAs(rutaImagenTemporal);
                    validarImagen(rutaImagenTemporal);
                }
                catch (Exception)
                {
                    System.IO.File.Delete(rutaImagenTemporal);
                    throw;
                }
            }
        }

        /// <summary>
        /// Crea los directorios en caso que no esten creados
        /// autor: Facundo Allemand
        /// </summary>
        private static void crearDirectorios()
        {
            if (!Directory.Exists(pathImagenesDisco + pathTemp))
                Directory.CreateDirectory(pathImagenesDisco + pathTemp);
            if (!Directory.Exists(pathImagenesDisco + pathTorneos))
                Directory.CreateDirectory(pathImagenesDisco + pathTorneos);
            if (!Directory.Exists(pathImagenesDisco + pathEquipos))
                Directory.CreateDirectory(pathImagenesDisco + pathEquipos);
            if (!Directory.Exists(pathImagenesDisco + pathComplejos))
                Directory.CreateDirectory(pathImagenesDisco + pathComplejos);
            if (!Directory.Exists(pathImagenesDisco + pathJugadores))
                Directory.CreateDirectory(pathImagenesDisco + pathJugadores);
        }

        /// <summary>
        /// Se encarga de borrar las imágenes. Recibe por parámetro elel id, y que tipo de image es (torneo, equipo, etc)
        /// autor: Facundo Allemand
        /// </summary>
        public static void borrrarImagen(int id, int tipoImagen)
        {
            try
            {
                string[] files = null;
                switch (tipoImagen)
                {
                    case TORNEO: files = System.IO.Directory.GetFiles(pathImagenesDisco + pathTorneos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case EQUIPO: files = System.IO.Directory.GetFiles(pathImagenesDisco + pathEquipos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case COMPLEJO: files = System.IO.Directory.GetFiles(pathImagenesDisco + pathComplejos, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
                    case JUGADOR: files = System.IO.Directory.GetFiles(pathImagenesDisco + pathJugadores, id + "*" + extension, System.IO.SearchOption.TopDirectoryOnly); break;
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
            Image img = null;
            try
            {
                FileInfo file = new FileInfo(rutaImagen);
                if (file.Length > tamanioMax)
                    throw new Exception("El tamaño del archivo es muy grande");
                using (img = Image.FromFile(rutaImagen))
                {
                    if (!img.RawFormat.Equals(ImageFormat.Bmp) &&
                        !img.RawFormat.Equals(ImageFormat.Gif) &&
                        !img.RawFormat.Equals(ImageFormat.Jpeg) &&
                        !img.RawFormat.Equals(ImageFormat.Png))
                    {
                        throw new Exception("El formato de imagen no es válido");
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                throw new Exception("El formato de imagen no es válido");
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
            return bmpOut;
        }

        /// <summary>
        /// Retorna el path de la imagen si existe, sino la imagen default.
        /// autor: Facundo Allemand
        /// </summary>
        public static string obtenerImagen(int id, int tipo, string tamañoImagen)
        {
            string pathImagen;
            switch (tipo)
            {
                case TORNEO:
                    pathImagen = pathImagenes + pathTorneos + id + tamañoImagen + extension;
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(pathImagen)))
                        return pathImagen;
                    else
                        return (pathImagenes + pathTorneos + "default" + tamañoImagen + extension);
                case EQUIPO:
                    pathImagen = pathImagenes + pathEquipos + id + tamañoImagen + extension;
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(pathImagen)))
                        return pathImagen;
                    else
                        return (pathImagenes + pathEquipos + "default" + tamañoImagen + extension);
                case COMPLEJO:
                    pathImagen = pathImagenes + pathComplejos + id + tamañoImagen + extension;
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(pathImagen)))
                        return pathImagen;
                    else
                        return (pathImagenes + pathComplejos + "default" + tamañoImagen + extension);
                case JUGADOR:
                    pathImagen = pathImagenes + pathJugadores + id + tamañoImagen + extension;
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(pathImagen)))
                        return pathImagen;
                    else
                        return (pathImagenes + pathJugadores + "default" + tamañoImagen + extension);
            }
            throw new Exception("Error al obtener la imagen");
        }

        /// <summary>
        /// Obtener Imagen Temporal
        /// autor: Facundo Allemand
        /// </summary>
        public static string obtenerImagenTemporal(int tipoImagen, string tamañoImagen)
        {
            string pathImagen = pathImagenes + pathTemp + System.Web.HttpContext.Current.Session.SessionID + extension; ;
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(pathImagen)))
                return pathImagen;
            else
                pathImagen = obtenerImagenDefault(tipoImagen, tamañoImagen);
            return pathImagen;
        }

        /// <summary>
        /// Obtener Imagen Temporal
        /// autor: Facundo Allemand
        /// </summary>
        public static string obtenerImagenDefault(int tipoImagen, string tamañoImagen)
        {
            string pathImagen="asdasasd";
            switch (tipoImagen)
            {
                case TORNEO:
                    pathImagen = pathImagenes + pathTorneos + "default" + tamañoImagen + extension; break;
                case EQUIPO:
                    pathImagen = pathImagenes + pathEquipos + "default" + tamañoImagen + extension; break;
                case COMPLEJO:
                    pathImagen = pathImagenes + pathComplejos + "default" + tamañoImagen + extension; break;
                case JUGADOR:
                    pathImagen = pathImagenes + pathJugadores + "default" + tamañoImagen + extension; break;
            }
            return pathImagen;
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
