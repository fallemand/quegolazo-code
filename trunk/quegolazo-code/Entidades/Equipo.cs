using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entidades
{
    public class Equipo : ICloneable    
    {
        public int idEquipo { get; set; }
        public string nombre { get; set; }
        public string nombreCorto { get; set; }
        public string colorCamisetaPrimario { get; set; }
        public string colorCamisetaSecundario { get; set; }
        public string directorTecnico { get; set; }
        public List<Jugador> jugadores { get; set; } 
        public Delegado delegadoPrincipal { get; set; }
        public Delegado delegadoOpcional { get; set; }

        public string obtenerImagen(string tamanioImagen, string clases)
        {
            String html = "";
            if (tieneImagen(tamanioImagen))
            {
                html = "<img src='" + GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, tamanioImagen) + "' class='"+ clases +"'>";
            }
            else
            {
                html = @"
                    <div class='camiseta-equipo'>
                        <div>
                            <i class='flaticon-football114' style='color:" + colorCamisetaPrimario + @"'></i>
                        </div><!--
                      --><div class='segunda-mitad'>
                            <i class='flaticon-football114' style='color:" + colorCamisetaSecundario + @"'></i>
                        </div>
                    </div>";
            }
            return html;
        }
        public string obtenerImagenChicha()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.CHICA);
        }
        public string obtenerImagenMediana()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.MEDIANA);
        }
        public string obtenerImagenGrande()
        {
            return GestorImagen.obtenerImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.GRANDE);
        }
        public bool tieneImagen()
        {
            return GestorImagen.tieneImagen(idEquipo, GestorImagen.EQUIPO, GestorImagen.GRANDE);
        }
        public bool tieneImagen(string tamanioImagen)
        {
            return GestorImagen.tieneImagen(idEquipo, GestorImagen.EQUIPO, tamanioImagen);
        }
        public object Clone()
        {
            return (Equipo)this.MemberwiseClone();
        }
    }
}
