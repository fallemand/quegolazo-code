using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;

namespace Logica
{
    public class GestorEquipo
    {

        public int registrarEquipo(Equipo nuevoEquipo, Torneo torneo, Delegado delegadoPrincipal, Delegado delegadoOpcional)
        {
            try
            {
                DAOEquipo daoEquipo = new DAOEquipo();
                int idNuevoEquipo = daoEquipo.registrarEquipo(nuevoEquipo, torneo, delegadoPrincipal, delegadoOpcional);
                return idNuevoEquipo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
    }
}
