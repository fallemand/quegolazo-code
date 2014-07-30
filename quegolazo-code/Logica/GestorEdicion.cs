using System;
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
        /// autor: Antonio Herrera
        /// </summary>
        /// <returns>El id de la edicion que se registro.</returns>
        public int registrarEdicion(string nombre, string idTorneo, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string ptosPerdido)
        {            
            try
            {
                Edicion edicionNueva = validarEdicion(nombre, idTorneo, idTamanioCancha, idSuperficie, ptosGanado, ptosEmpatado, ptosPerdido);
                DAOEdicion daoEdicion = new DAOEdicion();
                return daoEdicion.registrarEdicion(edicionNueva);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_nombre_torneo")) {
                    throw new Exception("Ya existe una edición llamada <strong>" + nombre + "</strong> para este campeonato. Por favor introduzca otro nombre.");
                }
               throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// valida si los datos introducidos en el formulario son correctos, si es asi devuelve el objeto Edicion correspondiente, si no lo son lanza excepciones con el mensaje correspondiente.
        /// autor: Antonio Herrera
        /// </summary> 
        public Edicion validarEdicion(string nombre, string idTorneo, string idTamanioCancha, string idSuperficie, string ptosGanado, string ptosEmpatado, string puntosPerdido)
        {
           try
            {
                int ganado, empatado, perdido;
                ganado = int.Parse(ptosGanado);
                perdido = int.Parse(puntosPerdido);
                empatado = int.Parse(ptosEmpatado);
                if (!(((ganado > empatado) && (empatado > perdido)) && ((ganado * empatado * perdido) >= 0)))
                {
                    throw new Exception("Los puntajes por ganar, empatar o perder son incorrectos.");
                }
                if (nombre == string.Empty)
                    throw new Exception("La edición no puede tener el nombre vacio.");

                //si paso hasta esta parte, ya se validaron los datos. Ahora se crea el objeto Edicion.
                DAOCancha gestorCancha = new DAOCancha();
                DAOTipoSuperficie gestorTipoSuperficie = new DAOTipoSuperficie();
                DAOTorneo gestorTorneo = new DAOTorneo();
                DAOEstado gestorEstado = new DAOEstado();
                
                TamanioCancha tamanioCancha = gestorCancha.obtenerTamanioCanchaPorId(Int32.Parse(idTamanioCancha));
                TipoSuperficie tipoSuperficie = gestorTipoSuperficie.obtenerTipoSuperficiePorId(Int32.Parse(idSuperficie));

;
                Estado estado = gestorEstado.obtenerUnEstadoPorNombreYAmbito(Estado.enumNombre.REGISTRADA, Estado.enumAmbito.EDICION);
                Torneo torneo = gestorTorneo.obtenerTorneoPorId(Int32.Parse(idTorneo));

                return new Edicion()
                {
                    nombre = nombre,
                    tamanioCancha = tamanioCancha,
                    tipoSuperficie = tipoSuperficie,
                    puntosGanado = ganado,
                    puntosPerdido = perdido,
                    puntosEmpatado = empatado,
                    estado = estado,
                    torneo = torneo
                };

            }
            catch (FormatException)
            {
                throw new Exception("Los campos de puntajes deben ser numeros enteros.");
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
