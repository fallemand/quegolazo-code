﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEdicion
    {
        public Edicion edicion;

        public GestorEdicion()
        {
            edicion = new Edicion();
        }

        /// <summary>
        /// Obtener ediciones de un torneo en particular
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="idTorneo">Id del torneo</param>
        /// <returns>Lista genérica del objeto Ediciones</returns>
        public List<Edicion> obtenerEdicionesPorIdTorneo(int idTorneo)
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                List<Edicion> ediciones = daoEdicion.obtenerEdicionesPorIdTorneo(idTorneo);
                return ediciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
 
        }

        
        /// <summary>
        /// Registra una nueva edicion
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="edicionNueva">Objeto Edicion</param>
        public int registrarEdicion(Edicion edicionNueva)
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                return daoEdicion.registrarEdicion(edicionNueva);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_torneo")) {
                    throw new Exception("Ya existe una edición llamada " + edicionNueva.nombre + " para este campeonato. Por favor introduzca otro nombre.");
                }
               throw new Exception(ex.Message);
            }
 
        }


        /// <summary>
        /// Metodo para registar las configuraciones
        /// autor=Flor
        /// </summary>
        public void registrarConfiguraciones()
        {
            try
            {
                DAOEdicion daoEdicion = new DAOEdicion();
                daoEdicion.registrarPreferencias(this.edicion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
