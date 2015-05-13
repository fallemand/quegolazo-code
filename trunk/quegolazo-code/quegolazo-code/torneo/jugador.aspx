<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="jugador.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!-- Charts.js-->
    <script type="text/javascript" src="/torneo/js/charts/Chart.min.js"></script>
    <script type="text/javascript" src="/torneo/js/charts/charts-jugador.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1><%= gestorJugador.jugador.nombre %></h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%=Logica.GestorUrl.urlTorneo(nickTorneo)%>"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion)%>"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipos(nickTorneo,idEdicion)%>">Equipos</a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,idEquipo)%>"><%= gestorEquipo.equipo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,idEquipo,idJugador)%>"><%= gestorJugador.jugador.nombre%></a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

        <!-- Content Central -->
        <div class="container padding-top">
            <div class="row mobile-margin-top">

                <!-- Otros jugadores -->
                <div class="col-sm-12">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">
                            <p class="slider-multiple-title">Jugadores del Equipo</p>
                            <ul class="otros-jugadores slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptOtroseJugadores" runat="server">
                                    <ItemTemplate>
                                        <li class="li-item" data-toggle="tooltip" title="<%# Eval("nombre")%>">
                                            <a id='jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>' class="popover-jugador <%#(((Entidades.Jugador)Container.DataItem).tieneImagen()==false) ? "avatar-jugador avatar-slider avatar-bg-" + ((Entidades.Jugador)Container.DataItem).lastNumber() : "" %>" href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,idEquipo,int.Parse(Eval("idJugador").ToString())) %>" >
                                                <img runat="server" src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenGrande() %>" class="img-circle center-block img-responsive" alt="imagen" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()%>" />
                                                <h1 runat="server" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()==false%>"><%# ((Entidades.Jugador)Container.DataItem).iniciales() %></h1>
                                                <p runat="server" visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen()==false%>" class="text-thin"><%# ((Entidades.Jugador)Container.DataItem).nombre %></p>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros jugadores -->

                <!-- Datos del Jugador -->
                <div class="col-sm-4">
                    <div class="panel-box bg-dark score theme-border">
                        <%if(gestorJugador.jugador.tieneImagen()) {%>
                        <img src="<%= gestorJugador.jugador.obtenerImagenMediana() %>" class="img-circle img-principal img-responsive" alt="imagen" />
                        <%} else {%>
                        <a href="#verJugador" class="avatar-jugador avatar-principal avatar-bg-<%= gestorJugador.jugador.lastNumber() %>" data-toggle="tooltip" title="<%= gestorJugador.jugador.nombre %>">
                            <h1><%= gestorJugador.jugador.iniciales() %></h1>
                        </a>
                        <%} %>
                        <h3 class="text-center text-thin"><a href="#"><%= gestorJugador.jugador.nombre %></a></h3>
                        <div class="row text-center">
                            <div class="col-xs-12">
                                <ul class="list-group tooltip-hover">
                                    <li class="list-group-item">
                                        <%if(gestorEquipo.equipo.tieneImagen()) {%>
                                            <img src="<%= gestorEquipo.equipo.obtenerImagenChicha() %>" class="img-circle avatar-xs" alt="">
                                        <%} else {%>
                                            <div class="camiseta-equipo camiseta-equipo-xs">
                                                <div>
                                                    <i class="flaticon-football114" style="color: <%= gestorEquipo.equipo.colorCamisetaPrimario %>"></i>
                                                </div><!--
                                                --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: <%= gestorEquipo.equipo.colorCamisetaSecundario %>"></i>
                                                </div>
                                            </div>
                                        <%}%>
                                        <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorEquipo.equipo.idEquipo) %>" data-toggle="tooltip" title="Ver Equipo"> <%= gestorEquipo.equipo.nombre %></a>
                                    </li>
                                    <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><span class="hidden-xs"></span>Nac: <%= ((DateTime)gestorJugador.jugador.fechaNacimiento).ToString("dd/MM/yyyy") %></li>
                                    <li class="list-group-item center-block">
                                        <a href="<%= gestorJugador.jugador.email %>" class="icon mail" data-toggle="tooltip" title="Copiar Mail"><i class="glyphicon glyphicon-envelope"></i></a>
                                        <a href="https://www.facebook.com/search/results/?q=<%= gestorJugador.jugador.facebook %>" target="_blank" class="icon facebook" data-toggle="tooltip" title="Perfil de Facebook"><i class="fa fa-facebook"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Datos del Jugador -->

                <!-- Principales Datos Jugadores  -->
                <div class="col-sm-8">
                    <div class="row">
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1>
                                        <span class="flaticon-football68" aria-hidden="true"></span>
                                        <asp:Literal ID="litPartidoJugados" runat="server"></asp:Literal>
                                    </h1>
                                    <span>Partidos Jugados</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md text-success">
                                    <h1 class="text-success">
                                        <span class="flaticon-football28" aria-hidden="true"></span>
                                        <asp:Literal ID="litGolesConvertidos" runat="server"></asp:Literal>
                                    </h1>
                                    <span class="text-success">Goles Convertidos</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-danger">
                                        <span class="flaticon-football103" aria-hidden="true"></span>
                                        <asp:Literal ID="litRojas" runat="server"></asp:Literal>
                                    </h1>
                                    <span class="text-danger">Tarjetas Rojas</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-yellow">
                                        <span class="flaticon-football103" aria-hidden="true"></span>
                                        <asp:Literal ID="litAmarillas" runat="server"></asp:Literal>
                                    </h1>
                                    <span class="text-yellow">Tarjetas Amarillas</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <!-- Resumen del Partido -->
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
                                                <ul class="nav navbar-nav nav-justified">
                                                    <li class="active"><a href="#resumen-equipo" data-toggle="tab"><span class="flaticon-football128"></span>Resumen</a></li>
                                                    <li><a href="#historial-partidos" data-toggle="tab"><span class="flaticon-football118"></span>Historial de Partidos</a></li>
                                                    <li><a href="#goleadores" data-toggle="tab"><span class="flaticon-football28"></span>Goles</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </nav>

                                    <!-- Tabs -->
                                    <div class="tab-content highlight">

                                        <!-- Tab Resumen -->
                                        <div class="tab-pane fade in active" id="resumen-equipo">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" colspan="2">Datos del Jugador</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Partidos Jugados: </td>
                                                                <td>
                                                                    <asp:Literal ID="litResumenPJ" runat="server"></asp:Literal></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Goles Convertidos: </td>
                                                                <td>
                                                                    <asp:Literal ID="litResumenGC" runat="server"></asp:Literal></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Tarjetas Rojas: </td>
                                                                <td>
                                                                    <asp:Literal ID="litResumenTR" runat="server"></asp:Literal></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Tarjetas Amarillas: </td>
                                                                <td>
                                                                    <asp:Literal ID="litResumenTA" runat="server"></asp:Literal></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-sm-3">
                                                    <h5 class="col-title">Número Camiseta</h5>
                                                    <div class="camiseta-back center-block">
                                                        <span class="numero">
                                                            <asp:Literal ID="litResumenNroCamiseta" runat="server"></asp:Literal></span>
                                                        <span class="apellido">
                                                            <asp:Literal ID="litResumenApellido" runat="server"></asp:Literal></span>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <h5 class="col-title">Edad</h5>
                                                    <div class="widget widget-lg">
                                                        <h1>
                                                            <asp:Literal ID="litResumenEdad" runat="server"></asp:Literal></h1>
                                                        <span>Años</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Tab Historial de Partidos -->
                                        <div class="tab-pane fade in" id="historial-partidos">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel-maxheight maxheight-md">
                                                        <table class="table table-striped table-hover table-partidos">
                                                            <thead>
                                                                <tr>
                                                                    <th class="text-center" colspan="2">Local</th>
                                                                    <th>Resultado</th>
                                                                    <th class="text-center" colspan="2">Visitante</th>
                                                                    <th>Partido</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rptHistorialPartidos" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <img src="<%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("idEquipoLocal").ToString())}).obtenerImagenChicha()%>" class="img-circle avatar-xs" alt="">
                                                                            </td>
                                                                            <td><%# Eval("Equipo Local") %></td>
                                                                            <td><%# Eval("Goles Local") %> - <%# Eval("Goles Visitante") %></td>
                                                                            <td><%# Eval("Equipo Visitante") %></td>
                                                                            <td>
                                                                                <img src="<%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("idEquipoVisitante").ToString())}).obtenerImagenChicha()%>" class="img-circle avatar-xs" alt=""></td>
                                                                            <td><span class="label partido-<%# Eval("Resultado") %>"><%# Eval("Resultado") %></span></td>
                                                                            <td class="hidden"><a href="<%# Logica.GestorUrl.urlPartido(nickTorneo, idEdicion, Eval("idPartido").ToString()) %>"></a></td>
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
                                            </div>
                                        </div>
                                        <!-- END Tab Historial de Partidos  -->

                                        <!-- Tab de Goleadores -->
                                        <div class="tab-pane fade active in" id="goleadores">
                                            <div class="col-sm-7">
                                                <div class="panel-maxheight maxheight-md">
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" colspan="2">VS</th>
                                                                <th>Fecha</th>
                                                                <th>Tipo</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptGolesJugador" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <img src="<%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("Id Otro equipo").ToString())}).obtenerImagenChicha()%>" class="img-circle avatar-xs" alt=""></td>
                                                                        <td><%# Eval("Otro Equipo") %></td>
                                                                        <td>Fecha <%# Eval("Fecha") %></td>
                                                                        <td><%# Eval("Tipo Gol") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                             <tr id="sinGolesJugador" runat="server" visible="false">
                                                                    <td colspan="6">No hay información de goles convertidos</td>
                                                                </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <h5 class="col-title">Tipos de Goles</h5>
                                                 <div id="graphics" runat="server">
                                                <canvas id="graficoGoles" class="canvas-md"></canvas></div>
                                                <div id="noGraphics" runat="server" visible="false" ><span>No hay información para mostrar</span></div>
                                            </div>
                                        </div>
                                        <!-- END Tab de Goleadores -->
                                    </div>
                                    <!-- END Resumen del Partido -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Jugadores del Equipo -->
            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- END contentPages-->
</asp:Content>
