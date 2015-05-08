using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoADatos;
using Utils;
using System.Data;
using System.Web.Script.Serialization;

namespace Logica
{
   public  class GestorEstadisticas
    {
       //DAOEstadisticas
       public Edicion edicion;
       DAOEstadisticas DaoEstadisticas = new DAOEstadisticas();

       public GestorEstadisticas(Edicion edicion)
       {
           edicion = Sesion.getGestorEdicion().edicion;
           if(edicion == null || edicion.idEdicion==0)
           edicion = buscarUltimaEdicionTorneo();
           Sesion.setEdicion(edicion);
       }

       public DataTable obtenerTablaPosiciones(int idFase, int idGrupo)
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion, idFase, idGrupo);
       }

       public DataTable obtenerTablaPosiciones(int idFase)
       {
           return DaoEstadisticas.obtenerTablaPosiciones(edicion.idEdicion, idFase);
       }

       public DataTable obtenerTablaGoleadores()
       {
           return DaoEstadisticas.obtenerTablaGoleadores(edicion.idEdicion);
       }
       public DataTable ultimoPartidoPrevioDeUnEquipo(int idEquipo, int idPartido)
       {
           return DaoEstadisticas.ultimoPartidoPrevioDeUnEquipo(idEquipo, edicion.idEdicion, idPartido);
       }

       public DataTable estadisticasDeUnEquipo(int idEquipo)
       {
           return DaoEstadisticas.estadisticasDeUnEquipo(idEquipo, edicion.idEdicion);
       }

       public DataTable goleadoresPorFaseDeEdicion(int idFase)
       {
           return DaoEstadisticas.goleadoresPorFaseDeEdicion(edicion.idEdicion, idFase);
       }

       public DataTable obtenerVersus(int idEquipoLocal, int idEquipoVisitante, int idTorneo)
       {
           return DaoEstadisticas.obtenerVersus(idEquipoLocal, idEquipoVisitante, edicion.idEdicion, idTorneo);
       }

       public DataTable obtenerTablaTarjetas()
       {
           return DaoEstadisticas.obtenerTablaTarjetas(edicion.idEdicion);
       }

       public DataTable obtenerFixtureFecha(int idFecha)
       {
           return DaoEstadisticas.obtenerFixtureFecha(edicion.idEdicion, idFecha);
       }

       public DataTable obtenerFixtureUltimaFecha(int idFase)
       {
           return DaoEstadisticas.obtenerFixtureUltimaFecha(edicion.idEdicion, idFase);
       }

       public DataTable obtenerAvanceFecha(int idFase)
       {    
           return DaoEstadisticas.obtenerAvanceFecha(edicion.idEdicion, idFase);
       }

       public DataTable obtenerAvanceEdicion()
       {
           return DaoEstadisticas.obtenerAvanceEdicion(edicion.idEdicion);
       }

       public DataTable obtenerAvanceEdicion(int idEdicion)
       {
           return DaoEstadisticas.obtenerAvanceEdicion(idEdicion);
       }

       public DataTable obtenerPartidosPorArbitro(int idEdicion)
       {
           return DaoEstadisticas.obtenerPartidosPorArbitro(idEdicion);
       }

       public DataTable cantidadDeGolesPorTipoGol(int idJugador)
       {
           return DaoEstadisticas.cantidadDeGolesPorTipoGol(idJugador, edicion.idEdicion);
       }

       public DataTable obtenerEstadisticasEquipo(int idEquipo)
       {
           return DaoEstadisticas.estadisticasDeUnEquipo(idEquipo, edicion.idEdicion);
       }

       public DataTable ultimosGolesDeUnJugador(int idJugador)
       {
           return DaoEstadisticas.ultimosGolesDeUnJugador(idJugador, edicion.idEdicion);
       }

       public DataTable estadisticasDeUnJugador(int idJugador)
       {
           return DaoEstadisticas.estadisticasDeUnJugador(idJugador, edicion.idEdicion);
       }

       public DataTable evolucionPuntosDeUnEquipo(int idEquipo, List<Fase> fases)
       {
           GestorEdicion gestorEdicion = new GestorEdicion();
           gestorEdicion.edicion.fases = fases;
           gestorEdicion.getFaseActual();
           return DaoEstadisticas.evolucionPuntosDeUnEquipo(edicion.idEdicion, gestorEdicion.faseActual.idFase, idEquipo); 
       }

       public DataTable obtenerUltimosPartidosEquipo(int idEquipo)
       {
           return DaoEstadisticas.ultimosPartidosDeUnEquipo(idEquipo, edicion.idEdicion);
       }

       public DataTable obtenerGoleadoresDeUnEquipo(int idEquipo)
       {
           return DaoEstadisticas.obtenerGoleadorEquipo(edicion.idEdicion, idEquipo);
       }

       public DataTable cantidadGolesPorEquipo(bool paraGrafico)
       {
           return DaoEstadisticas.cantidadGolesPorEquipo(edicion.idEdicion, paraGrafico); 
       }

       public DataTable cantidadGolesPorTipoGol(bool paraGrafico)
       {
           DataTable datosTiposGol = DaoEstadisticas.cantidadGolesPorTipoGol(edicion.idEdicion, paraGrafico);
           if (datosTiposGol.Rows[0]["Goles"].ToString().Equals("0") && datosTiposGol.Rows[1]["Goles"].ToString().Equals("0") &&
                  datosTiposGol.Rows[2]["Goles"].ToString().Equals("0") && datosTiposGol.Rows[3]["Goles"].ToString().Equals("0")
                   && datosTiposGol.Rows[4]["Goles"].ToString().Equals("0") && datosTiposGol.Rows[5]["Goles"].ToString().Equals("0"))
               datosTiposGol = null;
           return datosTiposGol;
       }

       public void guardarTablaPosicionesFinal(List<Int64> idEquipos, int idEdicion)
       {
           List<Equipo> equipos = new List<Equipo>();
           for (int i = 0; i < idEquipos.Count; i++)
           {
               Equipo equipo = new Equipo();
               equipo.idEquipo = int.Parse(idEquipos[i].ToString());
               equipos.Add(equipo);
           }
           DaoEstadisticas.guardarTablaPosiciones(equipos, idEdicion);
       }

       private Edicion buscarUltimaEdicionTorneo()
       {
           DAOEdicion daoEdicion = new DAOEdicion();
           return daoEdicion.obtenerUltimaEdicionTorneo(Sesion.getTorneo().idTorneo);
       }
       public DataTable obtenerUltimosPartidosJugador(int idJugador)
       {
           return DaoEstadisticas.ultimosPartidosDeJugadorComoTitular(idJugador, edicion.idEdicion);
       }

       public string generarDatosParaGraficoGoles(DataTable datosPrincipalesEquipo)
       {
            ChartsUtils.barChartData datosGoles = new ChartsUtils.barChartData(new List<String>() { "Convertidos", "En contra" },
                                                                                 new List<int> { int.Parse(datosPrincipalesEquipo.Rows[0]["GF"].ToString()), int.Parse(datosPrincipalesEquipo.Rows[0]["GC"].ToString()) });
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(datosGoles);
       }
       public string generarDatosParaGraficoPArtidos(DataTable datosPrincipalesEquipo)
       {
           double pj = double.Parse(datosPrincipalesEquipo.Rows[0]["PJ"].ToString());
           double pg = double.Parse(datosPrincipalesEquipo.Rows[0]["PG"].ToString());
           double pe = double.Parse(datosPrincipalesEquipo.Rows[0]["PE"].ToString());
           double pp = double.Parse(datosPrincipalesEquipo.Rows[0]["PP"].ToString());
           List<Entidades.ChartsUtils.pieChartDataObject> datosPartidos = new List<ChartsUtils.pieChartDataObject>();
           ChartsUtils.pieChartDataObject partidosGanados = new ChartsUtils.pieChartDataObject
           {
               color = ChartsUtils.colors.green + ChartsUtils.transparences.color,
               highlight = ChartsUtils.colors.green + ChartsUtils.transparences.highlight,
               label = "Partidos Ganados",
               value = Math.Round( pg / pj * 100, 2)
           };
           ChartsUtils.pieChartDataObject partidosEmpatados = new ChartsUtils.pieChartDataObject
           {
               color = ChartsUtils.colors.yellow + ChartsUtils.transparences.color,
               highlight = ChartsUtils.colors.yellow + ChartsUtils.transparences.highlight,
               label = "Partidos Empatados",
               value = Math.Round( pe / pj * 100,2)
           };
           ChartsUtils.pieChartDataObject partidosPerdidos = new ChartsUtils.pieChartDataObject
           {
               color = ChartsUtils.colors.red + ChartsUtils.transparences.color,
               highlight = ChartsUtils.colors.red + ChartsUtils.transparences.highlight,
               label = "Partidos Perdidos",
               value = Math.Round(pp / pj * 100,2)
           };
           datosPartidos.Add(partidosPerdidos);
           datosPartidos.Add(partidosEmpatados);
           datosPartidos.Add(partidosGanados);
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(datosPartidos);
       }
       public string generarDatosParaGraficoDeTorta(DataTable datosGrafico)
       {
           List<string> labels = new List<string>();
           List<double> datos = new List<double>();
           double totales = 0;
           foreach (DataRow fila in datosGrafico.Rows)
           {
               labels.Add(fila.ItemArray[0].ToString());
               datos.Add(double.Parse(fila.ItemArray[1].ToString()));
               totales += double.Parse(fila.ItemArray[1].ToString());
           }
           List<ChartsUtils.pieChartDataObject> datosGraficoJSON = new List<ChartsUtils.pieChartDataObject>();
           int indiceColores = 0;
           for (int i = 0; i < labels.Count; i++)
           {
               //si se acaban los colores, volvemos a poner el mismo color del principio.
               if (i >= ChartsUtils.listaDeColores.Length - 1)
                   indiceColores = 0;
               ChartsUtils.pieChartDataObject datoGrafico = new ChartsUtils.pieChartDataObject
               {
                   color = ChartsUtils.listaDeColores[indiceColores] + ChartsUtils.transparences.color,
                   highlight = ChartsUtils.listaDeColores[indiceColores] + ChartsUtils.transparences.highlight,
                   label = labels[i],
                   value = Math.Round(datos[i] / totales * 100, 2)
               };
               indiceColores++;
               datosGraficoJSON.Add(datoGrafico);

           }    
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(GestorColecciones.desordenarLista(datosGraficoJSON));
       }
       public string generarJsonParaGraficoDeBarra(DataTable datosGrafico)
       {         
           List<string> labels = new List<string>();
           List<int> datos = new List<int>();           
           foreach (DataRow fila in datosGrafico.Rows)
           {
               labels.Add(fila.ItemArray[0].ToString().Split(' ').Last());
               datos.Add(int.Parse(fila.ItemArray[1].ToString()));               
           }
           ChartsUtils.barChartDataSet dato = new ChartsUtils.barChartDataSet()
           {
               data = datos,
               fillColor = ChartsUtils.colors.green + ChartsUtils.transparences.fillColor,
               highlightFill = ChartsUtils.colors.green + ChartsUtils.transparences.highlightFill,
               highlightStroke = ChartsUtils.colors.green + ChartsUtils.transparences.highlightStroke,
               strokeColor = ChartsUtils.colors.green + ChartsUtils.transparences.strokeColor
           };
           ChartsUtils.barChartData datosGoleadores = new ChartsUtils.barChartData(labels, new List<ChartsUtils.barChartDataSet>() { dato });
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(datosGoleadores);
       }
       public string generarJsonParaGraficoBarraGoleadores() {
           List<DataTable> datos = new List<DataTable>();
           datos.Add(obtenerTablaGoleadores());                    
           foreach (Fase fase in this.edicion.fases)
           {
               datos.Add(goleadoresPorFaseDeEdicion(fase.idFase));
           }           
           List<ChartsUtils.barChartData> datosProcesados = new List<ChartsUtils.barChartData>();
           foreach (DataTable tabla in datos)
           {
               datosProcesados.Add((tabla.Rows.Count > 0) ? generarObjetoParaGraficoDeBarraGoleadores(tabla) : null);
           }
           return new JavaScriptSerializer().Serialize(datosProcesados);
       }
       public ChartsUtils.barChartData generarObjetoParaGraficoDeBarra(DataTable datosGrafico)
       {
           List<string> labels = new List<string>();
           List<int> datos = new List<int>();           
           foreach (DataRow fila in datosGrafico.Rows)
           {
               labels.Add(fila.ItemArray[0].ToString());
               datos.Add(int.Parse(fila.ItemArray[1].ToString()));
           }
           ChartsUtils.barChartDataSet dato = new ChartsUtils.barChartDataSet()
           {
               data = datos,
               fillColor = ChartsUtils.colors.green + ChartsUtils.transparences.fillColor,
               highlightFill = ChartsUtils.colors.green + ChartsUtils.transparences.highlightFill,
               highlightStroke = ChartsUtils.colors.green + ChartsUtils.transparences.highlightStroke,
               strokeColor = ChartsUtils.colors.green + ChartsUtils.transparences.strokeColor
           };
           ChartsUtils.barChartData datosGoleadores = new ChartsUtils.barChartData(labels, new List<ChartsUtils.barChartDataSet>() { dato });

           return datosGoleadores;
       }
       public ChartsUtils.barChartData generarObjetoParaGraficoDeBarraGoleadores(DataTable datosGrafico)
       {
           List<string> labels = new List<string>();
           List<int> datos = new List<int>();
           foreach (DataRow fila in datosGrafico.Rows)
           {
               labels.Add(fila.ItemArray[1].ToString().Split(' ').Last());
               datos.Add(int.Parse(fila.ItemArray[4].ToString()));
           }
           ChartsUtils.barChartDataSet dato = new ChartsUtils.barChartDataSet()
           {
               data = datos,
               fillColor = ChartsUtils.colors.green + ChartsUtils.transparences.fillColor,
               highlightFill = ChartsUtils.colors.green + ChartsUtils.transparences.highlightFill,
               highlightStroke = ChartsUtils.colors.green + ChartsUtils.transparences.highlightStroke,
               strokeColor = ChartsUtils.colors.green + ChartsUtils.transparences.strokeColor
           };
           ChartsUtils.barChartData datosGoleadores = new ChartsUtils.barChartData(labels, new List<ChartsUtils.barChartDataSet>() { dato });

           return datosGoleadores;
       }
       public string generarDatosParaGraficoEvolucionDePuntos(int idEquipo, List<Fase> fases)
       {
           DataTable evolucionDePuntos = evolucionPuntosDeUnEquipo(idEquipo, fases);
           List<string> labels = new List<string>();
           List<int> datos = new List<int>();
           int acumuladorDePuntos = 0;
           foreach (DataRow fila in evolucionDePuntos.Rows)
	       {
		    labels.Add("Fecha " + fila.ItemArray[0].ToString());
            datos.Add(acumuladorDePuntos += int.Parse(fila.ItemArray[1].ToString()));
	       }
           ChartsUtils.lineChartDataSet dato = new ChartsUtils.lineChartDataSet()
           {
               data = datos,
               fillColor = "rgba(151, 187, 205, 0.2)",
               pointColor = "rgba(151,187,205,1)",
               pointStrokeColor = "#fff",
               pointHighlightFill = "#fff",
               pointHighlightStroke = "rgba(151,187,205,1)",
               strokeColor = "rgba(151,187,205,1)"
           };
           ChartsUtils.lineChartData datosEvolucionPuntos = new ChartsUtils.lineChartData(labels, new List<ChartsUtils.lineChartDataSet>() { dato });
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(datosEvolucionPuntos);
           
       }
       public string generarDatosGoleadores(List<Jugador> jugadores)
       {           
           List<string> labels = new List<string>();
           List<int> datos = new List<int>();
           foreach (Jugador jug in jugadores)
           {
               labels.Add(jug.nombre.Split(' ').Last());
               datos.Add((int)jug.cantidadGoles);
           }
           ChartsUtils.barChartDataSet dato = new ChartsUtils.barChartDataSet()
           {
               data = datos,
               fillColor = ChartsUtils.colors.green + ChartsUtils.transparences.fillColor,
               highlightFill = ChartsUtils.colors.green + ChartsUtils.transparences.highlightFill,
               highlightStroke = ChartsUtils.colors.green + ChartsUtils.transparences.highlightStroke,
               strokeColor = ChartsUtils.colors.green + ChartsUtils.transparences.strokeColor
           };
           ChartsUtils.barChartData datosGoleadores = new ChartsUtils.barChartData(labels, new List<ChartsUtils.barChartDataSet>() { dato });
           JavaScriptSerializer s = new JavaScriptSerializer();
           return s.Serialize(datosGoleadores);

       }
    }
}
