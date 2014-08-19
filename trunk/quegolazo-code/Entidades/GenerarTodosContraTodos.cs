using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class GenerarTodosContraTodos: IGenerarFixture
    {
        /// <summary>
        /// Método par generar fixture
        /// autor=Flor
        /// </summary>
        /// <param name="equiposParticipantes"></param>
        /// <returns></returns>
        public List<Fecha> generarFixture(List<Equipo> equiposParticipantes)
        {
            prepararListaDeEquipos(ref equiposParticipantes);
            int cantidadFechas = (equiposParticipantes.Count - 1);
            int cantidadPartidos = equiposParticipantes.Count / 2;
            Equipo equipoPivot = (Equipo)equiposParticipantes[0].Clone();
            List<Fecha> fechas = new List<Fecha>();

            for (int i = 0; i < cantidadFechas; i++)
            {
                Fecha fechaNueva = new Fecha() {  idFecha = i + 1 };
                for (int j = 0, k = equiposParticipantes.Count - 1; j < cantidadPartidos && j < k; j++, k--)
                {
                    PartidoComun partidoNuevo = new PartidoComun()
                    {
                        idPartido=j+1,
                        local = equiposParticipantes[j],
                        visita = equiposParticipantes[k],                      
                       // estado = new Estado() { ambito = Estado.enumAmbito.PARTIDO, nombre = Estado.enumNombre.NO_JUGADO }
                    };
                    fechaNueva.partidos.Add(partidoNuevo);
                }
                fechas.Add(fechaNueva);
                intercambiarPosiciones(ref equiposParticipantes);
            }          
            return fechas;
        }

        /// <summary>
        ///  Intercambia la posicion de los equipos, pasando el ultimo equipo al segundo lugar, y adelantando los demas desde el segundo lugar (Round Robin)
        /// </summary>
        /// <param name="equiposParticipantes"></param>
        private void intercambiarPosiciones(ref List<Equipo> equiposParticipantes)
        {
            Equipo cambio = (Equipo)equiposParticipantes[equiposParticipantes.Count - 1].Clone();
            for (int i = equiposParticipantes.Count - 1; i > 1; i--)
            {
                equiposParticipantes[i] = equiposParticipantes[i - 1];
            }
            equiposParticipantes[1] = cambio;
        }

        /// <summary>
        /// Método para completar la lista de equipos en caso que no sea par
        /// </summary>
        /// <param name="equiposParticipantes"></param>
        private void prepararListaDeEquipos(ref List<Equipo> equiposParticipantes)
        {
            if (equiposParticipantes.Count % 2 != 0)
            {
                Equipo libre = new Equipo() { nombre = "LIBRE" };
                equiposParticipantes.Add(libre);
            }
        }

     
    }
}
