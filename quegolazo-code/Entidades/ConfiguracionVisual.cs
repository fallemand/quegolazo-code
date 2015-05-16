using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConfiguracionVisual
    {
        public string colorDeFondo { get; set; }
        public string patronDeFondo { get; set; }
        public string colorDestacado { get; set; }
        public string estiloPagina { get; set; }
        public string colorHeader { get; set; }
        public string patronHeader { get; set; }
        public string theme { get; set; }
        public string bodyClass { get; set; }
        
        public ConfiguracionVisual() {         
        }
        public ConfiguracionVisual(bool obtenerDefault)
        {
            this.bodyClass = "none fixed";
            this.colorDeFondo = "rgb(95, 165, 78)";
            this.patronDeFondo = "url(:12434/torneo/img/bg-theme/c10.png)";
            this.colorDestacado = "/torneo/css/skins/green.css";
            this.estiloPagina = "layout-boxed-margin";
            this.colorHeader = "rgb(255, 255, 255)";
            this.theme = "/torneo/css/bootstrap/sandstone.css";
            this.patronHeader = "none";            
        }
        
    }
   
}
