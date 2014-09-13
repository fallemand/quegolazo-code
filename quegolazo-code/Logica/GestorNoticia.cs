using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Data;

namespace Logica
{
    public class GestorNoticia
    {
        public Noticia noticia = new Noticia();
        /// <summary>
        /// Registra una Nueva Noticia en la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void registrarNoticia(string titulo, string tipoNoticia, string descripcion, string idEdicion, string fecha)
        {
            if (noticia == null)
                noticia = new Noticia();
            noticia.titulo = titulo;
            noticia.tipoNoticia = tipoNoticia;
            noticia.descripcion = descripcion;
            noticia.fecha = Validador.castDate(fecha);
            DAONoticia daoNoticia = new DAONoticia();
            noticia.idNoticia = daoNoticia.registrarNoticia(noticia, Validador.castInt(idEdicion));
        }

        /// <summary>
        /// Obtener noticias de un torneo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public DataTable obtenerNoticiasDeUnTorneo()
        {
            DAONoticia daoNoticia = new DAONoticia();
            int idTorneo = Sesion.getTorneo().idTorneo;
            DataTable noticias = new DataTable();
            noticias = daoNoticia.obtenerNoticiasDeUnTorneo(idTorneo);
            return noticias;
        }

        /// <summary>
        /// Obtiene una Noticia por Id de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void obtenerNoticiaPorId(int idNoticia)
        {
            DAONoticia daoNoticia = new DAONoticia();
            noticia = daoNoticia.obtenerNoticiaPorId(idNoticia);
        }

        /// <summary>
        /// Modifica una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void modificarNoticia(int idNoticia, string titulo, string tipoNoticia, string descripcion, string fecha)
        {
            DAONoticia daoNoticia = new DAONoticia();
            noticia.idNoticia = idNoticia;
            noticia.titulo = titulo;
            noticia.tipoNoticia = tipoNoticia;
            noticia.descripcion = descripcion;
            noticia.fecha = DateTime.Parse(fecha);
            daoNoticia.modificarNoticia(noticia);
        }

        /// <summary>
        /// Elimina una noticia de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public void eliminarNoticia(int idNoticia)
        {
            DAONoticia daoNoticia = new DAONoticia();
            daoNoticia.eliminarNoticia(idNoticia);
        }
    }
}
