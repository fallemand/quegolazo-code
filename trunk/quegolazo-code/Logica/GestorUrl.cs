using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class GestorUrl
    {
        //Forms en carpeta Admin
        public const string aARBITROS = "/admin/arbitros.aspx";
        public const string aCANCHAS = "/admin/canchas.aspx";
        public const string aEQUIPOS = "/admin/equipos.aspx";
        public const string aJUGADORES = "/admin/jugadores.aspx";
        public const string aINDEX = "/admin/index.aspx";
        public const string aTORNEOS = "/admin/torneos.aspx";
        public const string aNOTICIAS = "/admin/noticias.aspx";
        public const string aFECHAS = "/admin/fechas.aspx";
        public const string aEDICIONES = "/admin/ediciones.aspx";
        public const string aSANCIONES = "/admin/sanciones.aspx";

        //Forms en carpeta Edición
        public const string eCONFIGURAR = "/admin/edicion/configurar.aspx";
        public const string eEQUIPOS = "/admin/edicion/equipos.aspx";
        public const string eFASES = "/admin/edicion/fases.aspx";
        public const string eCONFIRMAR = "/admin/edicion/confirmar.aspx";
        public const string eJUGADORES = "/admin/edicion/jugadores.aspx";

        //Forms en carpeta Usuario
        public const string uACTIVAR = "/usuario/activar.aspx";
        public const string uLOGIN = "/usuario/login.aspx";
        public const string uRECUPERARCONTRASENIA = "/usuario/recuperar-contrasenia.aspx";
        public const string uRECUPERAR = "/usuario/recuperar.aspx";
        public const string uREGISTRO = "/usuario/registro.aspx";
        public const string uMODIFICAR = "/usuario/modificar.aspx";

        //Forms en el root 
        public const string rINDEX = "/index.aspx";
    }
}
