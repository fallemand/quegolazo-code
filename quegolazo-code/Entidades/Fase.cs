using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fase :ICloneable
    {
        public int idFase { get; set; }
        public int idEdicion { get; set; }
        public List<Grupo> grupos { get; set; }
        public TipoFixture tipoFixture { get; set; }
        public Estado estado { get; set; }
        public List<Equipo> equipos { get; set; }
        /// <summary>
        /// Indica si la fase fue generada por el sistema solo para mostrar la estructura de la edicion (true), o bien si fue generada por el usuario (false)
        /// </summary>
        public bool esGenerica { get; set; }
        public int cantidadDeGrupos { get; set; }
        public int cantidadDeEquipos { get; set; }


        public Fase()
        {
            try
            {
                tipoFixture = new TipoFixture();
                estado = new Estado();
                grupos = new List<Grupo>();
                equipos = new List<Equipo>();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }
        public object Clone()
        {
            return this.MemberwiseClone();
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
                                fechaFase.nombreCompleto = fecha.nombreCompleto;
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
