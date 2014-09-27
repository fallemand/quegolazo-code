using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fase
    {
        public int idFase { get; set; }
        public int idEdicion { get; set; }
        public List<Grupo> grupos { get; set; }
        public TipoFixture tipoFixture { get; set; }
        public Estado estado { get; set; }
        public int? cantidadDeEquipos { get; set; }
        
        public Fase()
        {
            tipoFixture = new TipoFixture();
            estado = new Estado();
            grupos = new List<Grupo>();
        }

        public List<Fecha> obtenerFechas()
        {
            List<Fecha> fechasFase = new List<Fecha>();
            int cantFechas=0;
            bool primerGrupo = true;

            //Obtener la cantidad total de fechas
            //Busco el grupo con más fechas.
            foreach (Grupo grupo in grupos)
            {
                if(primerGrupo)
                    cantFechas = grupo.fechas.Count;
                if (cantFechas < grupo.fechas.Count)
                    cantFechas = grupo.fechas.Count;
            }

            //Para cada fecha, agregar todos los partidos de la misma fecha de todos los grupos
            for (int i = 1; i <= cantFechas; i++)
            {
                Fecha fechaFase = new Fecha();
                fechaFase.idFecha = i;

                //Busco los partidos para la fecha i en cada grupo.
                foreach (Grupo grupo in grupos)
	            {
                        foreach (Fecha fecha in grupo.fechas)
                        {
                            //Si el número de fecha coincide con la fecha actual
                            if (fecha.idFecha == i)
                            {
                                fechaFase.nombre = fecha.nombre;
                                fechaFase.estado = fecha.estado;
                                foreach (Partido partido in fecha.partidos)
                                {
                                    fechaFase.partidos.Add(partido);
                                }
                            }
                        }
	            }
                fechasFase.Add(fechaFase);
            }
            return fechasFase;
        }
    }
}
