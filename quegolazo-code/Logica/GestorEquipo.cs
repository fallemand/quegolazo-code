using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEquipo
    {
        /// <summary>
        /// Registra un nuevo equipo en la base de datos
        /// autor: Paula Pedrosa
        /// </summary>
        /// <param name="nuevoEquipo">Nuevo Equipo a registrar</param>
        /// <param name="torneo">Torneo para el que se va a registrar el equipo</param>
        /// <param name="delegadoPrincipal">Delegado Principal del equipo</param>
        /// <param name="delegadoOpcional">Delegado Opcional del equipo, o null sino tiene</param>
        /// <returns>Id del nuevo equipo registrado</returns>
        public int registrarEquipo(Equipo nuevoEquipo, Torneo torneo, Delegado delegadoPrincipal, Delegado delegadoOpcional)
        {
            try
            {
                DAOEquipo daoEquipo = new DAOEquipo();
                int idNuevoEquipo = daoEquipo.registrarEquipo(nuevoEquipo, torneo, delegadoPrincipal, delegadoOpcional);
              
                return idNuevoEquipo;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No se puede insertar una fila de clave duplicada"))
                {
                    throw new Exception("El equipo: " + nuevoEquipo.nombre + " Ya se encuentra registrado en ese torneo.");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
                
                
            } 
        }
    }
}
