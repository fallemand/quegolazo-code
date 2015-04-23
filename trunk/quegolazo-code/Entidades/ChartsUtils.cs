using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ChartsUtils
    {
        public static class colors {
            public static string blue { get { return "rgba(52, 73, 94,"; } } 
           public static string red   { get {return "rgba(192, 57, 43,";} }
           public static string yellow { get {return "rgba(241, 196, 15,";} } 
           public static string green  { get {return "rgba(39, 174, 96,";} } 
           public static string orange { get {return "rgba(211, 84, 0,";} } 
           public static string purple { get {return "rgba(142, 68, 173,";} }
           public static string grey   { get {return "rgba(127, 140, 141,";} }
        }
        public static class transparences { 
            public static string fillColor{get {return  "0.7)";}}
            public static string strokeColor{get {return "0.8)";}}
            public static string highlightFill{get {return "0.8)";}}
            public static string highlightStroke{get {return "1.0)";}}
            public static string color{get {return "0.8)";}}
            public static string highlight {get {return  "0.9)";}}            
        }

        public static string[] listaDeColores { get { return new string[] { colors.green, colors.blue, colors.red, colors.yellow, colors.orange, colors.purple, colors.grey }; } }
        
        public class barChartData
        {
            public List<string> labels { get; set; }
            public List<barChartDataSet> datasets { get; set; }
            public barChartData(List<string> labels, List<int> datos) {
                this.labels = labels;
                this.datasets= new List<barChartDataSet>() {(new barChartDataSet() { data = datos })};
            }
            public barChartData(List<string> labels, List<barChartDataSet> datasets)
            {
                this.labels = labels;
                this.datasets = datasets;
            }
        }

        public class barChartDataSet{
            public string label { get; set; }
            public string fillColor { get; set; }
            public string strokeColor { get; set; }
            public string highlightFill { get; set; }
            public string highlightStroke { get; set; }
            public List<int> data { get; set; }           
        }


        public class pieChartDataObject {
            public double value { get; set; }
            public string color { get; set; }
            public string highlight { get; set; }
            public string label { get; set; }
        }


        public class lineChartData {
            public List<string> labels { get; set; }
            public List<lineChartDataSet> datasets { get; set; }
            public lineChartData(List<string> labels, List<lineChartDataSet> datasets)
            {
                this.labels = labels;
                this.datasets = datasets;
            }
        }

        public class lineChartDataSet{
            public string label { get; set; }
            public string fillColor { get; set; }
            public string strokeColor { get; set; }
            public string pointColor { get; set; }
            public string pointStrokeColor { get; set; }
            public string pointHighlightFill { get; set; }
            public string pointHighlightStroke { get; set; }
            public List<int> data { get; set; }
        }
      
    
    }
}
