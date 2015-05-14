<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="equipo.aspx.cs" Inherits="quegolazo_code.torneo.equipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!-- Charts.js-->
    <script type="text/javascript" src="/torneo/js/charts/Chart.min.js"></script>
    <script type="text/javascript" src="/torneo/js/charts/charts-equipo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1><%=gestorEquipo.equipo.nombre %> </h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>" ><%=gestorTorneo.torneo.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion) %>" ><%=gestorEdicion.edicion.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipos(nickTorneo,idEdicion) %>">Equipos</a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,idEquipo) %>" "><%=gestorEquipo.equipo.nombre %></a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

        <!-- Content Central -->
        <div class="container padding-top">
            <div class="row mobile-margin-top">

                <!-- Otros Partidos de la Fecha -->
                <div class="col-sm-12">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">
                            <p class="slider-multiple-title">Otros Equipos de la Edición</p>
                            <ul class="otros-equipos slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptOtroseEquiposDeEdicion" runat="server">
                                    <ItemTemplate>
                                        <li class="li-item" data-toggle="tooltip" title="<%# Eval("nombre") %>">
                                            <a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                                <%# ((Entidades.Equipo)Container.DataItem).obtenerImagen(Utils.GestorImagen.MEDIANA, "") %>
                                            </a>   
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>                                
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros partidos de la fecha -->

                <!-- Datos del Equipo -->
                <div class="col-sm-4">
                    <div class="panel-box bg-dark score theme-border principal">
                        <%= gestorEquipo.equipo.obtenerImagen(Utils.GestorImagen.GRANDE, "") %>
                        <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%=gestorEquipo.equipo.nombre %></a></h3>
                        <div class="row text-center">
                            <div class="col-xs-12">
                            <ul class="list-group">
                                <%--<li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span> <span class="hidden-xs">Sabado </span>27/10/2015</li>--%>
                                <li class="list-group-item"><span class="flaticon-football95" aria-hidden="true"></span> DT: <%=gestorEquipo.equipo.directorTecnico %></li>
                                <li class="list-group-item"><span class="flaticon-football50" aria-hidden="true"></span> Delegado: <%=gestorEquipo.equipo.delegadoPrincipal.nombre %></li>
                                <%--<li class="list-group-item"><span class="label label-success">Partido Jugado</span></li>--%>
                            </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Datos del Equipo -->
                
                <!-- Jugadores del Equipo -->
                <div class="col-sm-8">
                    <div class="row">
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1>
                                        <span class="flaticon-football31" aria-hidden="true"></span><asp:Literal ID="ltPuntos" runat="server"/>
                                    </h1>
                                    <span>Puntos</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md text-success">
                                    <h1 class="text-success">
                                        <span class="flaticon-football28" aria-hidden="true"></span><asp:Literal ID="ltGolesAFavor" runat="server"/>
                                    </h1>
                                    <span class="text-success">Goles a Favor</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-danger">
                                        <span class="flaticon-football28" aria-hidden="true"></span><asp:Literal ID="ltGolesEnContra" runat="server"/>
                                    </h1>
                                    <span class="text-danger">Goles en Contra</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1>
                                        <span class="flaticon-football68" aria-hidden="true"></span><asp:Literal ID="ltPartidosJugados" runat="server"/>
                                    </h1>
                                    <span>Partidos Jugados</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                  <asp:Repeater ID="rptJugadoresDelEquipo" runat="server">
                                    <ItemTemplate>
                                        <!-- Jugador -->
                                        <div class="col-md-2 col-xs-3">
                                            <a id='jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>' class="popover-jugador <%#(((Entidades.Jugador)Container.DataItem).tieneImagen()==false) ? "avatar-jugador avatar-lg avatar-slider avatar-bg-" + ((Entidades.Jugador)Container.DataItem).lastNumber() : "" %>" href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,idEquipo,int.Parse(Eval("idJugador").ToString())) %>" >
                                                <img runat="server" src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenGrande() %>" class="img-circle img-responsive" alt="imagen" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()%>" />
                                                <h1 runat="server" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()==false%>"><%# ((Entidades.Jugador)Container.DataItem).iniciales() %></h1>
                                                <p runat="server" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()==false%>" class="text-thin"><%# ((Entidades.Jugador)Container.DataItem).nombre %></p>
                                            </a>
                                            <!-- Popover del Jugador -->
                                            <div id="popover-jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>" style="display:none">
                                                <div class="row">
                                                    <div class="col-md-7">
                                                        <div class="row">
                                                          <div class="col-xs-6 widget widget-xs">
                                                            <h1><span class="flaticon-football68"></span><%#((Entidades.Jugador)Container.DataItem).PJ %></h1>
                                                            <span>Partidos</span>
                                                          </div>
                                                          <div class="col-xs-6 widget widget-xs">
                                                            <h1><span class="flaticon-football28"></span><%#((Entidades.Jugador)Container.DataItem).cantidadGoles %></h1>
                                                            <span>Goles</span>
                                                          </div>
                                                          <div class="col-xs-6 widget widget-xs">
                                                            <h1><span style="color: #c41a1a;" class="flaticon-football103"></span><%#((Entidades.Jugador)Container.DataItem).cantidadRojas %></h1>
                                                            <span>Rojas</span>
                                                          </div>
                                                          <div class="col-xs-6 widget widget-xs">
                                                            <h1><span style="color: #e5e520;" class="flaticon-football103"></span><%#((Entidades.Jugador)Container.DataItem).cantidadAmarillas %></h1>
                                                            <span>Amarillas</span>
                                                          </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5 text-center">
                                                        <div class="camiseta-back">
                                                            <span class="numero"><%#((Entidades.Jugador)Container.DataItem).numeroCamiseta %></span>
                                                            <span class="apellido"><%#((Entidades.Jugador)Container.DataItem).nombre %></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="popover-title-jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString()%>" style="display:none">
                                               <%#((Entidades.Jugador)Container.DataItem).nombre %>
                                                   <a href="#" class="icon pull-right facebook" ><i data-toggle="tooltip" title="Perfil de Facebook" class="fa fa-facebook"></i></a>
                                                   <a href="#" class="icon pull-right mail" ><i  data-toggle="tooltip" title="Copiar Mail" class="glyphicon glyphicon-envelope"></i></a>
                                            </div>
                                            <!-- END Popover del Jugador -->
                                        </div>
                                        <!-- END Jugador -->
                                        <!-- Jugador -->
                                    </ItemTemplate>
                                </asp:Repeater>   
                                <asp:Literal ID="sinJugadores" runat="server" Visible="false" Text="No registra información de Jugadores"></asp:Literal>                                      
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Jugadores del Equipo -->

                <!-- Resumen del Partido -->
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <nav class="navbar navbar-default navbar-nav-small nomargin-bottom">
                                <div class="container-fluid">
                                    <!-- Brand and toggle get grouped for better mobile display -->
                                    <div class="navbar-header">
                                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                                            <span class="sr-only">Menú</span>
                                            <span class="icon-bar"></span>
                                            <span class="icon-bar"></span>
                                            <span class="icon-bar"></span>
                                        </button>
                                        <span class="navbar-brand visible-xs" href="#">Estadísticas</span>
                                    </div>
                                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                                        <ul class="nav navbar-nav  nav-justified">
                                            <li class="active"><a href="#resumen-equipo" data-toggle="tab"><span class="flaticon-football128"></span>Resumen</a></li>
                                            <li><a href="#historial-partidos" data-toggle="tab"><span class="flaticon-football118"></span>Historial de Partidos</a></li>
                                            <li><a href="#goleadores" data-toggle="tab"><span class="flaticon-football28"></span> Goleadores</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </nav>

                            <!-- Tabs -->
                            <div class="tab-content highlight">
                                
                                <!-- Tab Resumen -->
                                <div class="tab-pane fade in active" id="resumen-equipo">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Partidos</th>
                                                        <th class="text-center no-strong"><small>Ganados</small></th>
                                                        <th class="text-center no-strong"><small>Empatados</small></th>
                                                        <th class="text-center no-strong"><small>Perdidos</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><asp:Literal ID="ltResumenPartidosJugados" runat="server"/></td>
                                                        <td class="success"><asp:Literal ID="ltResumenPartidosGanados" runat="server"/></td>
                                                        <td class="warning"><asp:Literal ID="ltResumenPartidosEmpatados" runat="server"/></td>
                                                        <td class="danger"><asp:Literal ID="ltResumenPartidosPerdidos" runat="server"/></td>
                                                    </tr>
                                                    </tbody>
                                            </table>
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Dif. Goles</th>
                                                        <th class="text-center no-strong"><small>A Favor</small></th>
                                                        <th class="text-center no-strong"><small>En Contra</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><asp:Literal ID="ltResumenGoles" runat="server"/></td>
                                                        <td class="success"><asp:Literal ID="ltResumenGolesConvertidos" runat="server"/></td>
                                                        <td class="danger"><asp:Literal ID="ltResumenGolesEnContra" runat="server"/></td>
                                                    </tr>
                                                    </tbody>
                                            </table>
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Tarjetas</th>
                                                        <th class="text-center no-strong"><small>Amarillas</small></th>
                                                        <th class="text-center no-strong"><small>Rojas</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><asp:Literal ID="ltResumenTarjetas" runat="server"/></td>
                                                        <td class="warning"><asp:Literal ID="ltResumenTarjetasAmarillas" runat="server"/></td>
                                                        <td class="danger"><asp:Literal ID="ltResumenTarjetasRojas" runat="server"/></td>
                                                    </tr>
                                                    </tbody>
                                            </table>               
                                        </div>
                                        <div class="col-sm-4">
                                            <h5 class="col-title">Gráfico de Goles</h5>
                                            <canvas id="graficoGoles" class="canvas-md"></canvas>
                                        </div>
                                        <div class="col-sm-4">
                                            <h5 class="col-title">Gráficos de Partidos</h5>
                                            <canvas id="graficoPartidos" class="canvas-md"></canvas>
                                        </div>
                                    </div>
                                    
                                </div>

                                <!-- Tab Historial de Partidos -->
                                <div class="tab-pane active fade in" id="historial-partidos">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped table-hover table-partidos">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="5">VS</th>
                                                        <th class="text-center">Resultado</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptHistorialPartidos" runat="server" OnItemDataBound="rptHistorialPartidos_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%# ((Entidades.Partido)Container.DataItem).local.obtenerImagen(Utils.GestorImagen.CHICA, "avatar-xs") %>
                                                                </td>
                                                                <td><%# Eval("local.nombre") %></td>
                                                                <td><%# Eval("golesLocal") %><small><asp:Literal ID="ltPenalesLocal" runat="server" Visible="false"/></small> - <%# Eval("golesVisitante") %><small><asp:Literal ID="ltPenalesVisitante" runat="server" Visible="false"/></small></td>
                                                                <td><%# Eval("visitante.nombre") %></td>
                                                                <td>
                                                                    <%# ((Entidades.Partido)Container.DataItem).visitante.obtenerImagen(Utils.GestorImagen.CHICA, "avatar-xs") %>
                                                                </td>
                                                                <td><span class="label partido-<%# Eval("resultadoParaUnEquipo") %>"><%# Eval("resultadoParaUnEquipo") %></span></td>
                                                                <td class="hidden"><a href="<%# Logica.GestorUrl.urlPartido(nickTorneo, idEdicion, ((Entidades.Partido)Container.DataItem).idPartido.ToString()) %>"></a></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr id="sinHistorialDePartido" runat="server" visible="false">
                                                        <td colspan="6">No hay información de partidos anteriores jugados</td>
                                                    </tr>    
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <h5 class="col-title">Evolución de Puntos</h5>
                                        <canvas id="graficoPuntos" class="canvas-lg"></canvas>
                                    </div>
                                </div>
                                <!-- END Tab Historial de Partidos  -->

                                <!-- Tab de Goleadores -->
                                <div class="tab-pane fade active in " id="goleadores">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped table-hover table-jugadores">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="2">Jugador</th>
                                                        <th class="text-center" colspan="2">Goles</th>
                                                    </tr>
                                                </thead>
                                                <tbody>                                                
                                                    <asp:Repeater ID="rptGoleadores" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-1">
                                                                    <a id='jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>' class="<%#(((Entidades.Jugador)Container.DataItem).tieneImagen()==false) ? "avatar-jugador avatar-bg-" + ((Entidades.Jugador)Container.DataItem).lastNumber() : "" %>" href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,idEquipo,int.Parse(Eval("idJugador").ToString())) %>" >
                                                                        <img runat="server" src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenChicha() %>" class="avatar-xs img-responsive" alt="imagen" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()%>" />
                                                                        <h1 runat="server" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()==false%>"><%# ((Entidades.Jugador)Container.DataItem).iniciales() %></h1>
                                                                    </a>
                                                                </td>
                                                                <td class="col-xs-9"><%# Eval("nombre") %></td>
                                                                <td class="col-xs-2"><%# Eval("cantidadGoles") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater> 
                                                    <tr id="sinGoleadores" runat="server" visible="false">
                                                        <td colspan="3">No hay información de goleadores</td>
                                                    </tr>  
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 col-xs-12">
                                        <h5 class="col-title">Goleadores</h5>
                                        <canvas id="graficoGoleadores" class="canvas-lg"></canvas>
                                    </div>
                                </div>
                                <!-- END Tab de Goleadores -->
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Resumen del Partido -->
            </div>
        </div>
        <!-- End Content Central -->
      </section>
      <!-- contentPages-->
</asp:Content>
