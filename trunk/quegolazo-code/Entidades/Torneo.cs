﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Torneo
    {
        public int idTorneo { get; set; }
        public string nombre { get; set; }
        public string nick { get; set; }
        public List<Equipo> equipos { get; set; }
        public List<Edicion> ediciones { get; set; }
        public Usuario usuario { get; set; }
        public string descripcion { get; set; }
        public string rutaImagen { get; set; }
        
        public Torneo()
        {
            usuario = new Usuario();
            equipos = new List<Equipo>();
            ediciones = new List<Edicion>();
        }
    }
}
