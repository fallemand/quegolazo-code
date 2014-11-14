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
            //switch (ambito.idAmbito)
            //{
            //    case 1:
            //        //Estados Torneo
            //        const int REGISTRADO = 1;
            //        break;
            //    case 2:
            //        //Estados Edicion
            //        const int REGISTRADA = 2;
            //        break;
            //    case 3:
            //        //Estados Fecha
            //        const int DIAGRAMADA = 5;
            //        break;
            //    case 4:
            //        //Estados Partido
            //        const int DIAGRAMADO = 6;
            //        break;
            //    case 5:
            //        //Estados Fase
            //        const int CREADA = 4;
            //        break;
            //}
        }

        //Estados Torneo: 1
        public const int torneoREGISTRADO = 1;

        //Estados Edicion: 2
        public const int edicionREGISTRADA = 2;
        public const int edicionCONFIGURADA = 14;

        //Estados Fecha: 3
        public const int fechaDIAGRAMADA = 7;
        public const int fechaCOMPLETA = 8;
        public const int fechaINCOMPLETA = 9;

        //Estados Partido: 4
        public const int partidoDIAGRAMADO = 10;
        public const int partidoCANCELADO = 12;
        public const int partidoJUGADO = 13;
        public const int partidoPROGRAMADO = 11;
        
        //Estados Fase: 5
        public const int faseCERRADA = 6;
        public const int faseDIAGRAMADA = 4;
        public const int faseENJUEGO = 5;
        public const int faseREGISTRADA = 3;                    

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
