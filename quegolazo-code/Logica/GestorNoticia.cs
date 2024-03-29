﻿using System;
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
        public void registrarNoticia(string titulo, string descripcion, string idEdicion, string idCategoriaNoticia)
        {
            if (Validador.castInt(idEdicion) == 0)
                throw new Exception("Debe seleccionar primero una edición!");
            if (noticia == null)
                noticia = new Noticia();
            noticia.titulo = titulo;
            noticia.descripcion = descripcion;
            noticia.categoria.idCategoriaNoticia = Int32.Parse(idCategoriaNoticia);
            DAONoticia daoNoticia = new DAONoticia();
            daoNoticia.registrarNoticia(noticia, Validador.castInt(idEdicion));
        }

        /// <summary>
        /// Obtener noticias de un torneo de la BD
        /// autor: Pau Pedrosa
        /// </summary>
        public DataTable obtenerNoticias(int idEdicion)
        {
            DAONoticia daoNoticia = new DAONoticia();
            DataTable noticias = daoNoticia.obtenerNoticias(idEdicion);
            return noticias;
        }

        public List<Noticia> obtenerNoticiasXCategoria(int idEdicion, int idCategoria)
        {
            DAONoticia daoNoticia = new DAONoticia();
            return daoNoticia.obtenerNoticiasXCategoria(idEdicion, idCategoria);
            
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
        public void modificarNoticia(int idNoticia, string titulo, string descripcion, string categoria)
        {
            DAONoticia daoNoticia = new DAONoticia();
            noticia.idNoticia = idNoticia;
            noticia.titulo = titulo;
            noticia.descripcion = descripcion;
            noticia.categoria.idCategoriaNoticia = int.Parse(categoria);
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

        public List<CategoriaNoticia> obtenerCategoriasNoticia()
        {
            DAONoticia daoNoticia = new DAONoticia();
            return daoNoticia.obtenerCategoriasNoticia();
        }
        public List<Noticia> obtenerListaDeNoticiasDeLaEdicion(int idEdicion)
        {
            DAONoticia daoNoticia = new DAONoticia();
            return daoNoticia.obtenerNoticiasList(idEdicion);
        }
    }
}
