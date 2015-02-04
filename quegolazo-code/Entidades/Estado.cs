using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estado
    {
        public int idEstado { get; set; }
        public string nombre { get; set; }
        public Ambito ambito { get; set; }

        public Estado()
        {
            ambito = new Ambito();
        }

        //Estados Torneo: 1
        public const int torneoREGISTRADO = 1;//REGISTRADO = un Torneo se crea como Registrado   

        //Estados Edicion: 2
        public const int edicionREGISTRADA = 2;//REGISTRADA =
        public const int edicionCONFIGURADA = 14;//CONFIGURADA = 
        public const int edicionINICIADA = 16;//INICIADA =
        public const int edicionFINALIZADA = 17;//FINALIZADA =
        public const int edicionCANCELADA = 18;//CANCELADA =         

        //Estados Fecha: 3
        public const int fechaDIAGRAMADA = 7;//DIAGRAMA = 
        public const int fechaCOMPLETA = 8;//COMPLETA = 
        public const int fechaINCOMPLETA = 9;

        //Estados Partido: 4
        public const int partidoDIAGRAMADO = 10;
        public const int partidoPROGRAMADO = 11;
        public const int partidoJUGADO = 13; 
        public const int partidoCANCELADO = 12;               
        
        //Estados Fase: 5        
        public const int faseREGISTRADA = 3;
        public const int faseDIAGRAMADA = 4;
        public const int faseINICIADA = 5;
        public const int faseFINALIZADA = 6;

    }
    /// <summary>
    /// Struct que define los ambitos. Debe coincidir con la estructura de la Base de Datos
    /// autor: Facundo Allemand
    /// </summary>
    public class Ambito
    {
        public int idAmbito;
        public string nombre;

        //Ambitos
        public const int TORNEO = 1;
        public const int EDICION = 2;
        public const int FASE = 5;
        public const int FECHA = 3;
        public const int PARTIDO = 4;
    }
}
