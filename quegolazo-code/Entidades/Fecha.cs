﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fecha
    {
        public int idFecha { get; set; }
        public List<Partido> partidos { get; set; }
        public string nombre { get; set; }
        public Estado estado { get; set; }
        public string nombreCompleto { get; set; }

        public Fecha()
        {
            partidos = new List<Partido>();
            estado = new Estado();
        }
    }
}
