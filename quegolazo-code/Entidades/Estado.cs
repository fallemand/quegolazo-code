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
        public string descripcion { get; set; }

        public Estado()
        {
            ambito = new Ambito();
        }

        //Estados Torneo: 1
        public const int torneoREGISTRADO = 1;//REGISTRADO = Torneo creado  

        //Estados Edicion: 2
        public const int edicionREGISTRADA = 2;//REGISTRADA = Edición creada
        public const int edicionCONFIGURADA = 14;//CONFIGURADA = Edición con preferencias, equipos y fixture 
        public const int edicionINICIADA = 16;//INICIADA = Edición con algún partido jugado
        public const int edicionFINALIZADA = 17;//FINALIZADA = Edición finalizada
        public const int edicionCANCELADA = 18;//CANCELADA = Edición cancelada         

        //Estados Fecha: 3
        public const int fechaDIAGRAMADA = 7;//DIAGRAMA = Fecha creada
        public const int fechaINCOMPLETA = 9;//INCOMPLETA = Fecha con partidos pendientes de juego
        public const int fechaCOMPLETA = 8;//COMPLETA = Fecha con todos los partidos jugados        

        //Estados Partido: 4
        public const int partidoDIAGRAMADO = 10;//DIAGRAMADO = Partido creado
        public const int partidoPROGRAMADO = 11;//PROGRAMADO = Partido con fecha asignada
        public const int partidoJUGADO = 13;//JUGADO = Partido con resultado asignado
        public const int partidoCANCELADO = 12;//CANCELADO = Partido cancelado               
        
        //Estados Fase: 5        
        public const int faseREGISTRADA = 3;//REGISTRADA = Fase genérica creada
        public const int faseDIAGRAMADA = 4;//DIAGRAMADA = Fase creada
        public const int faseINICIADA = 5;//INICIADA = Fase con al menos un partido jugado
        public const int faseFINALIZADA = 6;//FINALIZADA = Fase finalizada

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
