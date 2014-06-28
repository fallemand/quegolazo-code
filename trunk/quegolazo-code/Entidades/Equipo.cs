﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Equipo
    {
        public int idEquipo { get; set; }
        public string nombre { get; set; }
        public string colorCamisetaPrimario { get; set; }
        public string colorCamisetaSecundario { get; set; }
        public string directorTecnico { get; set; }
        public List<Delegado> delegados { get; set; }

    }
}
