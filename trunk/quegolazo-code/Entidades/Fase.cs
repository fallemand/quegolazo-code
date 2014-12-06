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
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }
        ///// <summary>
        ///// Crea una fase generica, con equipos y grupos genericos segun lo que se pase por parametro.
        ///// </summary>
        //public Fase(int cantidadEquipos, int cantidadGrupos) {
        //    this.equipos = generarEquipos(cantidadEquipos);
        //    this.grupos = generarGrupos(cantidadGrupos);
        //    this.esGenerica = true;
        //}

        /// <summary>
        /// Genera grupos para una fase generica
        /// </summary>
      //  private List<Grupo> generarGrupos(double cantEquipos,double cantidadGrupos)
        //{
        //    int cantidadEquiposxGrupo = Convert.ToInt16(cantEquipos/cantidadGrupos);
        //    int sobrantes = Convert.ToInt16(cantEquipos- cantidadEquiposxGrupo*cantidadGrupos);
        //    int limite = Convert.ToInt16

        //    List<Grupo> respuesta = new List<Grupo>();
        //    for (int i = 0; i < cantidadGrupos; i++)
        //    {
                
        //    }
        //    return respuesta;
        //         var widget = this;
        ////esta variable es la cantidad de equipos por grupo sin tener en cuenta los sobrantes        
        
        //var sobrantes = listaDeEquipos.length - cantidadEquiposxGrupo*cantGrupos ;
        //var limite = listaDeEquipos.length - sobrantes;
        //var grupos = [];
        //var indice = 0;
        ////se divide en una cantidad de grupos fija, cada uno con la misma cantidad de equipos
        //for (var i = 0; i < cantGrupos; i++) {
        //    var nuevoGrupo = {
        //        idGrupo: i + 1,
        //        idFase: numFase,
        //        idEdicion: widget.options.idEdicion,
        //        equipos: []
        //    };
        //    var grupo = [];
        //    for (var j = 0; j < cantidadEquiposxGrupo; j++) {
        //        nuevoGrupo.equipos.push(listaDeEquipos[indice]);
        //        indice++;
        //    }
        //    grupos.push(nuevoGrupo);
        //}

        ////ahora se distribuyen los equipos sobrantes uno en cada grupo
        //indice = listaDeEquipos.length - sobrantes;        
        //for (var i = 0; i < sobrantes; i++) {
        //    grupos[i].equipos.push(listaDeEquipos[indice]);
        //    indice++;
        //}        
      
        //return grupos;
        //}
        ///// <summary>
        ///// Genera equipos para una fase generica
        ///// </summary>
        //private List<Equipo> generarEquipos(int cantidadEquipos)
        //{
        //    List<Equipo> respuesta = new List<Equipo>();
        //    for (int i = 0; i < cantidadEquipos; i++)
        //    {
                
        //    }
        //    return respuesta;
        //}

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
