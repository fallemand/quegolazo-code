<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="partido.aspx.cs" Inherits="quegolazo_code.torneo.partido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!--Theme Options-->
    <script type='text/javascript' src="/torneo/js/theme-options/theme-options.js"></script>
    <link href="css/colorPicker.css" rel="stylesheet" />
    <script src="../resources/js/jquery.colorPicker.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Partido</h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="index-2.html">Torneo La Rivera</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Edición 2014</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Fase 1</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Fecha 2</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Boca Juniors vs Boca Juniors</a></li>
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
                            <p class="slider-multiple-title">Otros Partidos de la Fecha</p>
                            <ul class="proximos-partidos slider-multiple tooltip-hover">
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-clock-o"></i>
                                                Programado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-clock-o"></i>
                                                Programado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros partidos de la fecha -->

                <!-- Tablero de Resultados -->
                <div class="col-sm-12">
                    <div class="panel-box score bg-dark theme-border">
                        <div class="row">
                            <div class="col-md-4 col-xs-3 nopadding-right padding-top">
                                <!--<div class="camiseta-equipo text-center">
                                        <span class="primera-mitad glyphicon glyphicon-triangle-left" style="color:#2966B8;"></span>
                                        <span class="glyphicon glyphicon-triangle-right" style="color:#DFE32D;"></span>
                                      </div>-->
                                <% if(gestorPartido.partido.local.tieneImagen()) { %>
                                <img src="<%= gestorPartido.partido.local.obtenerImagenGrande() %>" class="img-responsive center-block" style="max-height: 150px;">
                                <%} else { %>
                                <div class="row">
                                    <div class="camiseta-equipo">
                                        <div>
                                            <i class="flaticon-football114" style="color: <%= gestorPartido.partido.local.colorCamisetaPrimario%>"></i>
                                        </div><!--
                                     --><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color: <%= gestorPartido.partido.local.colorCamisetaSecundario%>"></i>
                                        </div>
                                    </div>
                                </div>
                                <% } %>
                                <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre  %></a></h3>
                            </div>
                            <div class="col-xs-6 col-md-4">
                                <div class="row text-center resultado">                                    
                                    <div class="col-xs-5 nopadding-left">
                                        <% if(gestorPartido.partido.golesLocal != null) {%> <h1><%=gestorPartido.partido.golesLocal %> </h1><% } %>
                                    </div>
                                    <% if(gestorPartido.partido.empate == true) { %>
                                    <div class="col-xs-5 nopadding-left">
                                        <h1>(<%=gestorPartido.partido.penalesLocal %>)</h1>
                                    </div>
                                    <% } %>
                                    <div class="col-xs-2 nopadding-right nopadding-left">
                                        <h1>-</h1>
                                    </div>                                    
                                    <div class="col-xs-5 nopadding-right">
                                         <% if(gestorPartido.partido.golesVisitante != null) {%> <h1><%=gestorPartido.partido.golesVisitante %> </h1><% } %>
                                    </div>
                                    <% if(gestorPartido.partido.empate == true) { %>
                                    <div class="col-xs-5 nopadding-left">
                                        <h1>(<%=gestorPartido.partido.penalesVisitante %>)</h1>
                                    </div>
                                    <% } %>
                                </div>
                                <div class="row text-center">
                                    <div class="col-xs-12">
                                        <ul class="list-group">
                                            <% if(gestorPartido.partido.fecha != null) { %>
                                            <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><span class="hidden-xs">Sábado </span><%= gestorPartido.partido.fecha %></li>
                                            <% } %>
                                            <% if(gestorPartido.partido.cancha != null) { %>
                                            <li class="list-group-item hidden-xs"><span class="glyphicon glyphicon-home" aria-hidden="true"></span><%= gestorPartido.partido.cancha.nombre %></li>
                                             <% } %>
                                            <li class="list-group-item hidden-xs"><span class="label label-success"><%= gestorPartido.partido.estado.nombre %></span></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3 col-md-4 padding-top">
                                <% if(gestorPartido.partido.visitante.tieneImagen()) { %>
                                <img src="<%= gestorPartido.partido.visitante.obtenerImagenGrande() %>" class="img-responsive center-block" style="max-height: 150px;">
                                <%} else { %>
                                <div class="row">
                                    <div class="camiseta-equipo">
                                        <div>
                                            <i class="flaticon-football114" style="color: <%= gestorPartido.partido.visitante.colorCamisetaPrimario%>"></i>
                                        </div><!--
                                     --><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color: <%= gestorPartido.partido.visitante.colorCamisetaSecundario%>"></i>
                                        </div>
                                    </div>
                                </div>
                                <% } %>
                                <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h3>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Tablero de Resultado -->

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
                                            <li class="active"><a href="#resumen" data-toggle="tab"><span class="flaticon-football128"></span>Resumen</a></li>
                                            <li><a href="#titulares" data-toggle="tab"><span class="flaticon-football118"></span>Titulares</a></li>
                                            <li><a href="#goles" data-toggle="tab"><span class="flaticon-football28"></span>Goles</a></li>
                                            <li><a href="#cambios" data-toggle="tab"><span class="flaticon-up23"></span>Cambios</a></li>
                                            <li><a href="#tarjetas" data-toggle="tab"><span class="flaticon-football79"></span>Tarjetas</a></li>
                                            <li><a href="#sanciones" data-toggle="tab"><span class="flaticon-black188"></span>Sanciones</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </nav>


                            <!-- Tabs -->
                            <div class="tab-content highlight">

                                <!-- Tab Resumen -->
                                <div class="tab-pane fade in active" id="resumen">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th class="col-xs-2 col-md-4 text-center">
                                                    <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                    <%= gestorPartido.partido.local.nombre %>
                                                </th>
                                                <th class="col-xs-8 col-md-4 text-center">VS</th>
                                                <th class="col-xs-2 col-md-4 text-center">
                                                    <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                    <%= gestorPartido.partido.visitante.nombre %>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody class="text-center">
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptGolesLocal" runat="server">
                                                            <ItemTemplate>
                                                                <span class="flaticon-football28"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                </td>
                                                <td>Goles</td>
                                                <td>
                                                    <asp:Repeater ID="rptGolesVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span class="flaticon-football28"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasRojasLocal" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: red;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                </td>
                                                <td class="text-center">Tarjetas Rojas</td>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasRojasVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: red;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasAmarillasLocal" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: yellow;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                </td>
                                                <td class="text-center">Tarjetas Amarillas</td>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasAmarillasVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: yellow;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>                      
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptCambiosLocal" runat="server">
                                                            <ItemTemplate>
                                                               <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                            </ItemTemplate>
                                                     </asp:Repeater>   
                                                </td>
                                                <td class="text-center">Cambios</td>
                                                <td>
                                                    <asp:Repeater ID="rptCambiosVisitante" runat="server">
                                                            <ItemTemplate>
                                                               <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                            </ItemTemplate>
                                                     </asp:Repeater> 
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                                <!-- Tab de Jugadores -->
                                <div class="tab-pane fade" id="titulares">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTitularesLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <img src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenMediana() %>" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                            <td class="col-xs-9"><%# Eval("nombre") %></td>
                                                            <td class="col-xs-2"><%# Eval("numeroCamiseta") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>                                                 
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTitularesVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <img src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenMediana() %>" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                            <td class="col-xs-9"><%# Eval("nombre") %></td>
                                                            <td class="col-xs-2"><%# Eval("numeroCamiseta") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>    
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Goles -->
                                <div class="tab-pane fade" id="goles">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabGolesLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Gol)Container.DataItem).minuto %>'</span></td>
                                                            <td class="col-xs-7">
                                                                <img src="<%# ((Entidades.Gol)Container.DataItem).jugador.obtenerImagenMediana() %>" class="img-circle avatar-sm" alt="">
                                                                <%# ((Entidades.Gol)Container.DataItem).jugador.nombre %></td>
                                                            <td class="col-xs-3"><%# ((Entidades.Gol)Container.DataItem).tipoGol.nombre %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater> 
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabGolesVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                        <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Gol)Container.DataItem).minuto %>'</span></td>
                                                        <td class="col-xs-7">
                                                            <img src="<%# ((Entidades.Gol)Container.DataItem).jugador.obtenerImagenMediana() %>" class="img-circle avatar-sm" alt="">
                                                            <%# ((Entidades.Gol)Container.DataItem).jugador.nombre %></td>
                                                        <td class="col-xs-3"><%# ((Entidades.Gol)Container.DataItem).tipoGol.nombre %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Cambios -->
                                <div class="tab-pane fade" id="cambios">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabCambiosLocal" runat="server">
                                                    <ItemTemplate>
                                                       <tr>
                                                        <td class="col-xs-1"><span class="text-lg"><%# ((Entidades.Cambio)Container.DataItem).minuto %>'</span></td>
                                                        <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                        <td class="col-xs-4">
                                                            <img src="<%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.obtenerImagenMediana() %>" class="img-circle avatar-sm" alt="">
                                                            <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %></td>
                                                        <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                        <td class="col-xs-4">
                                                            <img src="<%# ((Entidades.Cambio)Container.DataItem).jugadorSale.obtenerImagenMediana() %>"  class="img-circle avatar-sm" alt="">
                                                            <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %></td>
                                                       </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>                                               
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabCambiosVisitante" runat="server">
                                                    <ItemTemplate>
                                                       <tr>
                                                        <td class="col-xs-1"><span class="text-lg"><%# ((Entidades.Cambio)Container.DataItem).minuto %>'</span></td>
                                                        <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                        <td class="col-xs-4">
                                                            <img src="<%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.obtenerImagenMediana() %>" class="img-circle avatar-sm" alt="">
                                                            <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %></td>
                                                        <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                        <td class="col-xs-4">
                                                            <img src="<%# ((Entidades.Cambio)Container.DataItem).jugadorSale.obtenerImagenMediana() %>"  class="img-circle avatar-sm" alt="">
                                                            <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %></td>
                                                       </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>  
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Tarjetas -->
                                <div class="tab-pane fade" id="tarjetas">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabTarjetasAmarillasLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto %>'</span></td>
                                                            <td class="col-xs-8">
                                                                <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                                <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                            <td class="colrptTabTarjetasAmarillasLocal-xs-2">
                                                                <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater> 
                                                <asp:Repeater ID="rptTabTarjetasRojasLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto %>'</span></td>
                                                            <td class="col-xs-8">
                                                                <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                                <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                            <td class="col-xs-2">
                                                                <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater> 
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabTarjetasAmarillasVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto %>'</span></td>
                                                            <td class="col-xs-8">
                                                                <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                                <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                            <td class="colrptTabTarjetasAmarillasLocal-xs-2">
                                                                <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater> 
                                                <asp:Repeater ID="rptTabTarjetasRojasVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto %>'</span></td>
                                                            <td class="col-xs-8">
                                                                <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                                <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                            <td class="col-xs-2">
                                                                <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Sanciones -->
                                <div class="tab-pane fade" id="sanciones">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.local.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptSancionesLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <img src="<%# ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagenChicha() %>" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                            <td class="col-xs-4"><%# Eval("jugador.nombre") %></td>
                                                            <td class="col-xs-7">Sancionado por <%# Eval("cantidadFechasSuspendidas") %> Fechas</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="<%= gestorPartido.partido.visitante.obtenerImagenChicha() %>" style="max-height: 20px;">
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptSancionesVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <img src="<%# ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagenChicha() %>" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                            <td class="col-xs-4"><%# Eval("jugador.nombre") %></td>
                                                            <td class="col-xs-7">Sancionado por <%# Eval("cantidadFechasSuspendidas") %> Fechas</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                 </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Resumen del Partido -->

                <!-- Widget Partidos Anteriores Equipo Local -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Últimos Partido: <%= gestorPartido.partido.local.nombre %></h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4">
                                            <img src=<%= gestorEquipo.obtenerEquipoPorId(cargarUltimoPartidoEL()[0]).obtenerImagenMediana() %> class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="ltUltimoPartidoEqLocal" runat="server" /></a></h5>
                                        </div>
                                        <div class="nopadding-left col-xs-4 resultado nopadding-right">
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2><asp:Literal ID="ltUltimoPartidoGolesLocalEL" runat="server" /></h2>
                                            </div>
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2><asp:Literal ID="ltUltimoPartidoGolesVisitanteEL" runat="server" /></h2>
                                            </div>
                                            <div class="col-xs-12 text-center">
                                                Fecha <asp:Literal ID="litUltimoPartidoFechaEL" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <img src="<%= gestorEquipo.obtenerEquipoPorId(cargarUltimoPartidoEL()[1]).obtenerImagenMediana() %>" class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="ltUltimoPartidoEqVisitante" runat="server" /></a></h5>
                                        </div>
                                        <%--<div class="col-xs-4 nopadding-right">
                                            <div class="camiseta-equipo">
                                                <div>
                                                    <i class="flaticon-football114" style="color: #005A96"></i>
                                                </div><!--
                                             --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                </div>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="" runat="server" /></a></h5>
                                        </div>--%>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Partidos Anteriores Equipo Local -->

                <!-- Widget Versus -->
                <div class="col-md-4 col-sm-6" style="margin-bottom: 10px;">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Versus</h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <!-- Versus: Puntos -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2><asp:Literal ID="ltPuntosEL" runat="server"/></h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <span class="flaticon-football31" aria-hidden="true"></span>
                                            <div class="col-xs-12 text-center">
                                                Puntos
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2><asp:Literal ID="ltPuntosEV" runat="server" /></h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Puntos -->

                                <!-- Versus: Partidos Ganados -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>6</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/ganador.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Partidos Ganados
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>4</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Partidos Ganados -->

                                <!-- Versus: Tarjetas Rojas -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>8</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Tarjetas Rojas
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>33</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Rojas -->

                                <!-- Versus: Tarjetas Amarillas -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>39</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Tarjetas Amarillas
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>21</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Amarillas -->

                                <!-- Versus: Goles -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>39</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <span class="flaticon-flaming" aria-hidden="true" style="color: #E00C0C;"></span>
                                            <div class="col-xs-12 text-center">
                                                Goles a Favor
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>21</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Goles -->
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Versus -->

                <!-- Widget Partidos Anteriores Equipo Visitante -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Último Partidos: <%= gestorPartido.partido.visitante.nombre %></h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4">
                                            <img src="<%= gestorEquipo.obtenerEquipoPorId(cargarUltimoPartidoEV()[0]).obtenerImagenMediana() %>" class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="ltPartidoPrevioEquipoLocalEV" runat="server" /></a></h5>
                                        </div>
                                        <div class="nopadding-left col-xs-4 resultado nopadding-right">
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2><asp:Literal ID="ltPartidoPrevioGolesLocalEV" runat="server" /></h2>
                                            </div>
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2><asp:Literal ID="ltPartidoPrevioGolesVisitanteEV" runat="server" /></h2>
                                            </div>
                                            <div class="col-xs-12 text-center">
                                                Fecha <asp:Literal ID="litPartidoPrevioFechaEV" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <img src="<%= gestorEquipo.obtenerEquipoPorId(cargarUltimoPartidoEV()[1]).obtenerImagenMediana() %>" class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="ltPartidoPrevioEquipoVisitanteEV" runat="server" /></a></h5>
                                        </div>
                                        <%--<div class="col-xs-4 nopadding-right">
                                            <div class="camiseta-equipo">
                                                <div>
                                                    <i class="flaticon-football114" style="color: #005A96"></i>
                                                </div><!--
                                                --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                </div>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><asp:Literal ID="ltPartidoPrevioEquipoVisitanteEV" runat="server" /></a></h5>
                                        </div>--%>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Partidos -->

                <!-- Galeria -->
                <div class="col-sm-6 col-md-12">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Fotos del Partido</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- Items Gallery filters-->
                                    <div class="portfolioContainer">
                                        <div class="row">
                                            <!-- Item Gallery-->
                                            <div class="col-xs-6 col-sm-6 col-md-3 soccer">
                                                <div class="img-hover">
                                                    <img src="/torneo/img/gallery/1.jpg" alt="" class="img-responsive">
                                                    <div class="overlay"><a href="/torneo/img/gallery/1.jpg" class="fancybox">+</a></div>
                                                </div>
                                            </div>
                                            <!-- End Item Gallery-->
                                            <!-- Item Gallery-->
                                            <div class="col-xs-6 col-sm-6 col-md-3 soccer">
                                                <div class="img-hover">
                                                    <img src="/torneo/img/gallery/2.jpg" alt="" class="img-responsive">
                                                    <div class="overlay"><a href="/torneo/img/gallery/1.jpg" class="fancybox">+</a></div>
                                                </div>
                                            </div>
                                            <!-- End Item Gallery-->
                                            <!-- Item Gallery-->
                                            <div class="col-xs-6 col-sm-6 col-md-3 soccer">
                                                <div class="img-hover">
                                                    <img src="/torneo/img/gallery/3.jpg" alt="" class="img-responsive">
                                                    <div class="overlay"><a href="/torneo/img/gallery/1.jpg" class="fancybox">+</a></div>
                                                </div>
                                            </div>
                                            <!-- End Item Gallery-->
                                            <!-- Item Gallery-->
                                            <div class="col-xs-6 col-sm-6 col-md-3 soccer">
                                                <div class="img-hover">
                                                    <img src="/torneo/img/gallery/5.jpg" alt="" class="img-responsive">
                                                    <div class="overlay"><a href="/torneo/img/gallery/1.jpg" class="fancybox">+</a></div>
                                                </div>
                                            </div>
                                            <!-- End Item Gallery-->
                                        </div>
                                        <!-- End Items Gallery filters-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Galeria de Fotos-->

            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- Theme-options -->
    <div id="theme-options" style="z-index:9999">
            <div class="openclose"></div>
            <div class="title">
               <span>OPCIONES DEL TEMA</span>
               </div>
        <span>Tema</span>
            <ul id="themeSelector" class="text-center">      
                <li class="cyborg" title="Cyborg"><span>1</span></li>
                <li class="darkly" title="Darkly"><span>2</span></li>                    
                <li class="flatly"  title="Flatly"><span>3</span></li>
                <li class="sandstone" title="Sandstone"><span>4</span></li>
                <li class="slate" title="Slate"><span>5</span></li>
                <li class="hero" title="Super Heroe"><span>6</span></li>
                <li class="yeti"  title="Yeti"><span>7</span></li>      
                <li class="bootstrap"  title="Bootstrap"><span>8</span></li>            
            </ul> 
            <span>Color destacado</span>
            <ul id="colorchanger">      
                <li><a class="colorbox red" href="#" title="Red Skin"></a></li>
                <li><a class="colorbox blue" href="#" title="Blue Skin"></a></li>                    
                <li><a class="colorbox yellow" href="#" title="Yellow Skin"></a></li>
                <li><a class="colorbox green" href="#" title="Green Skin"></a></li>
                <li><a class="colorbox orange" href="#" title="Orange Skin"></a></li>
                <li><a class="colorbox purple" href="#" title="Purple Skin"></a></li>
                <li><a class="colorbox pink" href="#" title="Pink Skin"></a></li>
                <li><a class="colorbox cocoa" href="#" title="Cocoa Skin"></a></li>
            </ul> 
            <span>ESTILO DE PAGINA</span>
            <ul class="layout-style">      
                <li class="wide">ANCHO</li>
                <li class="semiboxed active">SEMI CAJA</li> 
                <li class="boxed">CAJA</li> 
                <li class="boxed-margin">C/MARGEN</li>               
            </ul>           
            <span>Patron del header</span>
            <ul class="backgrounds-h">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg3"></li>
                    <li class="bg4"></li>
                    <li class="bg8"></li>
                    <li class="bg9 "></li>
                    <li class="bg12"></li> 
                    <li class="bg14"></li>
                    <li class="bg19"></li>                                  
                </ul>  
            <span>Colores:</span>
           <ul class="layout-style text-center">    
                <li>
                    Fondo
                    <div id="colorFondo" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>    
                <li >
                    Header
                    <div id="colorHeader" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>                                 
            </ul>           
            <div class="patterns">               
            <span>PATRON DEL FONDO</span>
            <ul class="backgrounds">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg1"></li>
                    <li class="bg2"></li>
                    <li class="bg3"></li>
                    <li class="bg4 "></li>
                    <li class="bg5"></li> 
                    <li class="bg6"></li>
                    <li class="bg7"></li>
                    <li class="bg8"></li>
                    <li class="bg9 "></li>
                    <li class="bg10"></li> 
                    <li class="bg11"></li> 
                    <li class="bg12"></li> 
                    <li class="bg13"></li> 
                    <li class="bg14"></li> 
                    <li class="bg15"></li> 
                    <li class="bg16"></li> 
                    <li class="bg17"></li> 
                    <li class="bg18"></li> 
                    <li class="bg19"></li> 
                    <li class="bg20"></li> 
                    <li class="bg21"></li> 
                    <li class="bg22"></li> 
                    <li class="bg23"></li> 
                    <li class="a1"></li>   
                    <li class="a2"></li> 
                    <li class="a3"></li>   
                    <li class="a4"></li>      
                    <li class="a5"></li>     
                    <li class="c1 fixed"></li>   
                    <li class="c2 fixed"></li> 
                    <li class="c3 fixed"></li>    
                </ul>  
            </div>
            <ul class="layout-style">    
                <li class="btn btn-danger" id="cerrarConfig">Cerrar</li>    
                <li onclick="guardarConfiguracion()" class="btn btn-primary">Guardar</li>                                 
            </ul> 
            <div class="col-xs-12">
            <span id="msjeAjax" class="text-center nomargin-bottom"></span>  
            </div>                       
        </div>
    <!-- End Theme-options -->      

</asp:Content>
