using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class GenerarTodosContraTodos: IGenerarFixture
    {
       public int cantidadRondas;
        /// <summary>
        /// Método par generar fixture
        /// autor=Flor
        /// </summary>
        /// <param name="equiposParticipantes"></param>
        /// <returns></returns>
        public List<Fecha> generarFixture(List<Equipo> equipos)
        {
            List<Equipo> equiposParticipantes = prepararListaDeEquipos(ref equipos);
            int cantidadFechas = (equiposParticipantes.Count - 1) * getCantidadRondas();
            int cantidadPartidos = equiposParticipantes.Count / 2;
            Equipo equipoPivot = (Equipo)equiposParticipantes[0].Clone();
            List<Fecha> fechas = new List<Fecha>();

            for (int i = 0; i < cantidadFechas; i++)
            {
                Fecha fechaNueva = new Fecha() { idFecha = i + 1, estado = new Estado { ambito = new Ambito { idAmbito=Ambito.FECHA},idEstado=7 } };

                for (int j = 0, k = equiposParticipantes.Count - 1; j < cantidadPartidos && j < k; j++, k--)
                {
                    Partido partidoNuevo = new Partido()
                    {
                        //idPartido = j + 1,
                        local = equiposParticipantes[j],
                        visitante = equiposParticipantes[k],
                        estado = new Estado { ambito = new Ambito { idAmbito = Ambito.PARTIDO,},idEstado=10 },

                    };
                    fechaNueva.partidos.Add(partidoNuevo);
                }
                fechas.Add(fechaNueva);
                intercambiarPosiciones(ref equiposParticipantes);
            }
            //si tiene 2 rondas (ida y vuelta)
            if (getCantidadRondas() == 2) 
                reordenarLocalias(ref fechas);
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
        private List<Equipo> prepararListaDeEquipos(ref List<Equipo> equiposParticipantes)
        {
            List<Equipo> equipos = new List<Equipo>();
            foreach (Equipo e in equiposParticipantes)
            {
                equipos.Add(e);
            }

            if (equiposParticipantes.Count % 2 != 0)
            {
                Equipo libre = new Equipo() { idEquipo=0, nombre = "LIBRE" };
                equipos.Add(libre);
            }
            return equipos;
        }

        /// <summary>
        /// Intercambia los equipos locales y visitantes para la segunda ronda de un campeonato
        /// </summary>
        /// <param name="fechas">Las fechas a reordenar</param>
        private void reordenarLocalias(ref List<Fecha> fechas)
        {
            int mitad = fechas.Count / 2;
            for (int i = mitad; i < fechas.Count; i++)
            {
                for (int j = 0; j < fechas[i].partidos.Count; j++)
                {
                    Equipo copia = (Equipo)fechas[i].partidos[j].local.Clone();
                    fechas[i].partidos[j].local = (Equipo)fechas[i].partidos[j].visitante.Clone();
                    fechas[i].partidos[j].visitante = copia;
                }
            }

        }
        public int getCantidadRondas()
        {
            return cantidadRondas;
        }
        public void setCantidadRondas(int cantidadRondas)
        {
            this.cantidadRondas = cantidadRondas;
        }
    }
}
