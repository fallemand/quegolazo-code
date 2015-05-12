<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- Section Area - Content Central -->
        <section class="content-info">

            <!-- Content Central -->
            <div class="container padding-top">
                <div class="row mobile-margin-top">

<!-- Otros Partidos de la Fecha -->
                <div class="col-sm-12">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">
                            <p class="slider-multiple-title">Otros Partidos de la Fecha</p>
                            <ul class="proximos-partidos slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptFechaActual" runat="server">
                                    <ItemTemplate>
                                        <li class="li-partido" style="width: 120px">
                                            <span class="fecha theme-bg-color"> <%#((Entidades.Partido)Container.DataItem).fecha != null ? nombreMes(DateTime.Parse(((Entidades.Partido)Container.DataItem).fecha.ToString()).Month)+" "+DateTime.Parse(((Entidades.Partido)Container.DataItem).fecha.ToString()).Day.ToString()+", "+DateTime.Parse(((Entidades.Partido)Container.DataItem).fecha.ToString()).Year.ToString() : "Sin fecha asignada" %></span>
                                            <div class="text">
                                                <div class="equipos">
                                                   <%-- <a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>/partido-<%# Eval("idPartido") %>">--%>
                                                    <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion, Eval("idPartido").ToString()) %>">
                                                        <span data-toggle="tooltip" title="<%# Eval("local.nombre") %>"><%# Eval("local.nombreCorto") %></span>
                                                        vs
                                                        <span data-toggle="tooltip" title="<%# Eval("visitante.nombre") %>"><%# Eval("visitante.nombreCorto") %></span>
                                                    </a>
                                                    <p class="estado <%# Eval("estado.nombre") %> theme-bg-color-2">
                                                       <%# (((Entidades.Partido)Container.DataItem).golesLocal != null) ? ((Entidades.Partido)Container.DataItem).golesLocal+"-"+((Entidades.Partido)Container.DataItem).golesVisitante : ((Entidades.Partido)Container.DataItem).estado.nombre %>
                                                    </p>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros partidos de la fecha -->

                    <!-- Alerta Edicion Finalizada -->
                    <!--<div class="col-sm-12">
                        <div class="alert alert-success alert-dismissible" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <strong>Atención!</strong> Esta edición se encuentra <strong>Finalizada</strong>.
                        </div>
                    </div>-->
                    <!-- END Alerta Edicion Finalizada -->

                    <!-- Dark Home -->
                    <div class="col-sm-12">
                            <div class="row">
                                <!-- Left Content - Tabs and Carousel -->
                                <div class="col-md-9">
                                    <!-- Nav Tabs -->
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active"><a href="#ultimos-partidos" data-toggle="tab">EVENTOS</a></li>
                                        <li><a href="#estadisticas" data-toggle="tab">ESTADÍSTICAS</a></li>
                                    </ul>
                                    <!-- End Nav Tabs -->

                                    <!-- Content Tabs -->
                                    <div class="panel">
                                        <div class="panel-body nopadding-horizontal bg-dark">
                                            <div class="tab-content home">
                                                <!-- Tab One - Feature News -->
                                                <div class="tab-pane active" id="ultimos-partidos">
                                                    <!-- blog post-->
                                                    <ul id="events-carousel" class="events-carousel padding-top">
                                                        <!-- Item blog post -->
                                                          <asp:Repeater ID="rptEventos" runat="server">
                                                              <ItemTemplate>
                                                        <li>
                                                            <div class="header-post">
                                                                <div class="date">
                                                                    <span><%# ((DateTime)((Entidades.Noticia)Container.DataItem).fecha).ToString("dd/MM/yyyy") %></span>
                                                                   <%#  ((DateTime)Eval("fecha")).Year %> 
                                                                </div>
                                                                <a href="single-news.html"><img src="<%# ((Entidades.Noticia)Container.DataItem).obtenerImagenMediana() %>" alt=""></a>
                                                                <div class="meta-tag">
                                                                    <ul>
                                                                        <%--<li><i class="fa fa-user"></i><a href="#">Admin</a></li>--%>
                                                                        <%--<li><i class="fa fa-folder-open"></i><a href="#">Design</a></li>--%>
                                                                        <%--<li class="text-right"><i class="fa fa-comment"></i><a href="#">10</a></li>--%>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="info-post">
                                                                <h4><a href="single-news.html"><%# Eval("titulo") %></a></h4>
                                                                <p><%# Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Substring(0,60)  %>...</p>
                                                            </div>
                                                        </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                        <!-- End Item blog post -->

                                                        <!-- Item blog post -->
                                                       <%-- <li>
                                                            <div class="header-post">
                                                                <div class="date">
                                                                    <span>08/jan</span>
                                                                    2014
                                                                </div>
                                                                <a href="single-news.html"><img src="/torneo/img/blog/2.jpg" alt=""></a>
                                                                <div class="meta-tag">
                                                                    <ul>
                                                                        <li><i class="fa fa-user"></i><a href="#">Admin</a></li>
                                                                        <li><i class="fa fa-folder-open"></i><a href="#">Design</a></li>
                                                                        <li class="text-right"><i class="fa fa-comment"></i><a href="#">10</a></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="info-post">
                                                                <h4><a href="single-news.html">Championship Final</a></h4>
                                                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                                            </div>
                                                        </li>--%>
                                                        <!-- End Item blog post -->

                                                        <!-- Item blog post -->
                                                        <%--<li>
                                                            <div class="header-post">
                                                                <div class="date">
                                                                    <span>08/jan</span>
                                                                    2014
                                                                </div>
                                                                <a href="single-news.html"><img src="/torneo/img/blog/3.jpg" alt=""></a>
                                                                <div class="meta-tag">
                                                                    <ul>
                                                                        <li><i class="fa fa-user"></i><a href="#">Admin</a></li>
                                                                        <li><i class="fa fa-folder-open"></i><a href="#">Design</a></li>
                                                                        <li class="text-right"><i class="fa fa-comment"></i><a href="#">10</a></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="info-post">
                                                                <h4><a href="single-news.html">Confidence indicators</a></h4>
                                                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                                            </div>
                                                        </li>--%>
                                                        <!-- End Item blog post -->

                                                        <!-- Item blog post -->
                                                      <%--  <li>
                                                            <div class="header-post">
                                                                <div class="date">
                                                                    <span>08/jan</span>
                                                                    2014
                                                                </div>
                                                                <a href="single-news.html"><img src="/torneo/img/blog/4.jpg" alt=""></a>
                                                                <div class="meta-tag">
                                                                    <ul>
                                                                        <li><i class="fa fa-user"></i><a href="#">Admin</a></li>
                                                                        <li><i class="fa fa-folder-open"></i><a href="#">Design</a></li>
                                                                        <li class="text-right"><i class="fa fa-comment"></i><a href="#">10</a></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="info-post">
                                                                <h4><a href="single-news.html">Championship Final.</a></h4>
                                                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                                            </div>
                                                        </li>--%>
                                                        <!-- End Item blog post -->

                                                        <!-- Item blog post -->
                                                       <%-- <li>
                                                            <div class="header-post">
                                                                <div class="date">
                                                                    <span>08/jan</span>
                                                                    2014
                                                                </div>
                                                                <a href="single-news.html"><img src="/torneo/img/blog/2.jpg" alt=""></a>
                                                                <div class="meta-tag">
                                                                    <ul>
                                                                        <li><i class="fa fa-user"></i><a href="#">Admin</a></li>
                                                                        <li><i class="fa fa-folder-open"></i><a href="#">Design</a></li>
                                                                        <li class="text-right"><i class="fa fa-comment"></i><a href="#">10</a></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="info-post">
                                                                <h4><a href="single-news.html">Great Prospects.</a></h4>
                                                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                                            </div>
                                                        </li>--%>
                                                        <!-- End Item blog post -->
                                                    </ul>
                                                    <!-- End blog post-->
                                                </div>
                                                <!-- Tab One - Feature News -->

                                                <!-- Tab Two - Players Staff -->
                                                <div class="tab-pane" id="estadisticas">
                                                    <h3>Estadísticas de la Edición</h3>
                                                    <div class="row">
                                                        <div class="col-md-3 col-xs-6">
                                                            <div class="panel nopadding panel-default">
                                                                <div class="panel-body widget widget-md">
                                                                    <h1>
                                                                        <span class="flaticon-football68" aria-hidden="true"></span><asp:Literal ID="ltPJ" runat="server"/>
                                                                    </h1>
                                                                    <span>Partidos Jugados</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-xs-6">
                                                            <div class="panel nopadding panel-default">
                                                                <div class="panel-body widget widget-md text-success">
                                                                    <h1 class="text-success">
                                                                        <span class="flaticon-football28" aria-hidden="true"></span><asp:Literal ID="ltGolesConvertidos" runat="server"/>
                                                                    </h1>
                                                                    <span class="text-success">Goles Convertidos</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-xs-6">
                                                            <div class="panel nopadding panel-default">
                                                                <div class="panel-body widget widget-md">
                                                                    <h1 class="text-danger">
                                                                        <span class="flaticon-football103" aria-hidden="true"></span><asp:Literal ID="ltTR" runat="server"/>
                                                                    </h1>
                                                                    <span class="text-danger">Tarjetas Rojas</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-xs-6">
                                                            <div class="panel nopadding panel-default">
                                                                <div class="panel-body widget widget-md">
                                                                    <h1 class="text-yellow">
                                                                        <span class="flaticon-football103" aria-hidden="true"></span><asp:Literal ID="ltTA" runat="server"/>
                                                                    </h1>
                                                                    <span class="text-yellow">Tarjetas Amarillas</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="panel">
                                                                <div class="panel-body nopadding">
                                                                    <table class="table table-condensed table-striped nomargin-bottom text-middle">
                                                                        <thead>
                                                                            <tr>
                                                                                <th class="text-center" colspan="3">
                                                                                    <span class="flaticon-football28"></span>
                                                                                    Últimos Goles
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rptUltimosGoles" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td><img src="<%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("Id Equipo").ToString())}).obtenerImagenChicha()%>" class=" avatar-xs"></td>
                                                                                        <td>
                                                                                            <img src="<%# (new Entidades.Jugador() { idJugador=int.Parse(Eval("Id Jugador").ToString())}).obtenerImagenChicha()%>" class="img-circle avatar-xs" alt="">
                                                                                            <%# Eval("Jugador") %></td>
                                                                                        <td><%# Eval("Tipo Gol") %></td>
                                                                                        <td><a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion, Eval("Id Partido").ToString()) %>">Ver Partido</a></td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>                                                                            
                                                                            <tr id="sinUltimosGoles" runat="server" visible="false">
                                                                                <td colspan="4">No hay información registrada de últimos goles</td>
                                                                            </tr>                                                                            
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="panel">
                                                                <div class="panel-body nopadding">
                                                                    <table class="table table-condensed table-striped nomargin-bottom text-middle">
                                                                        <thead>
                                                                            <tr>
                                                                                <th class="text-center" colspan="3">
                                                                                    <img src="/torneo/img/img-theme/tarjetas.png" class="tarjetas-icon">
                                                                                     Últimas Tarjetas
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rptUltimasTarjetas" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td><img src="<%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("Id Equipo").ToString())}).obtenerImagenChicha()%>" class=" avatar-xs"></td>
                                                                                        <td>
                                                                                            <img src="<%# (new Entidades.Jugador() { idJugador=int.Parse(Eval("Id Jugador").ToString())}).obtenerImagenChicha()%>" class="img-circle avatar-xs" alt="">
                                                                                            <%# Eval("Jugador") %></td>
                                                                                        <td><img src="/torneo/img/img-theme/tarjeta-<%# Eval("Tipo Tarjeta") %>.png" class="img-circle avatar-xs" alt=""></td>
                                                                                        <td><a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion, Eval("Id Partido").ToString()) %>">Ver Partido</a></td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <tr id="sinUltimasTarjetas" runat="server" visible="false">
                                                                                <td colspan="4">No hay información registrada de últimas tarjetas</td>
                                                                            </tr>   
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Tab Two - Players Staff -->
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Content Tabs -->
                                </div>
                                <!-- Left Content - Tabs and Carousel -->

                                <!-- Right Content - Content Counter -->
                                <div class="col-md-3">
                                    <aside>
                                        <div class="title-color text-center">
                                            <h4>Próximo Partido</h4>
                                        </div>

                                        <div class="content-counter content-counter-home">
                                            <p class="text-center">
                                                <i class="fa fa-clock-o"></i>
                                                Cuenta Regresiva
                                            </p>
                                            <div id="counter-proximo-partido" class="counter"></div>
                                            <ul class="post-options">
                                                <li><i class="fa fa-calendar"></i><asp:Literal ID="ltDiaDePartido" runat="server"/> <asp:Literal ID="ltFechaPartido" runat="server"/></li>
                                                <li><i class="fa fa-clock-o"></i><asp:Literal ID="ltHoraPartido" runat="server"/> hs</li>
                                            </ul>
                                            <div class="widget-partido">
                                                <div class="col-xs-6">
                                                    <asp:Panel ID="tieneFotoLocal" runat="server">
                                                    <img src="<%# (new Entidades.Equipo() { idEquipo= proximoPartido.local.idEquipo}).obtenerImagenMediana()%>" class="img-responsive center-block"></asp:Panel>
                                                    <asp:Panel ID="noTieneFotoLocal" runat="server">
                                                        <div class="camiseta-equipo">
                                                        <div>
                                                            <i class="flaticon-football114" style="color: #005A96"></i>
                                                        </div><!--
--><div class="segunda-mitad">
                                                        <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                        </div>
                                                        </asp:Panel>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo"><asp:Literal ID="ltEquipoLocal" runat="server"/></a></h5>
                                                </div>

                                                <div class="col-xs-6">
                                                    <asp:Panel ID="tieneFotoVisitante" runat="server">
                                                    <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block"></asp:Panel>
                                                    <asp:Panel ID="noTieneFotoVisitante" runat="server">
                                                        <div class="camiseta-equipo">
                                                        <div>
                                                            <i class="flaticon-football114" style="color: #005A96"></i>
                                                        </div><!--
--><div class="segunda-mitad">
                                                        <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                        </div>
                                                        </asp:Panel>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo"><asp:Literal ID="ltEquipoVisitante" runat="server"/></a></h5>
                                                </div>
                                            </div>
                                            <a class="btn btn-primary" href="#">
                                                VER FICHA DEL PARTIDO
                                                <i class="fa fa-trophy"></i>
                                            </a>
                                        </div>
                                    </aside>
                                    <!-- Content Counter -->
                                </div>
                                <!-- End Right Content - Content Counter -->
                            </div>
                    </div>
                    <!-- Dark Home -->

                    <!-- content Column Left -->
                    <div class="col-md-8 col-sm-12">
                        <!-- Recent Post -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Ultimas Noticias</h3>
                            </div>
                            <div class="panel-body">
                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="img-hover">
                                                <img src="/torneo/img/blog/1.jpg" alt="" class="img-responsive">
                                                <div class="overlay"><a href="single-news.html">+</a></div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h4><a href="single-news.html">Porto Alegre and Cuiabá to welcome Valcke</a></h4>
                                            <p class="data-info">January 3, 2014  / <i class="fa fa-comments"></i><a href="#">0</a></p>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus rutrum, libero id imperdiet elementum, nunc... <a href="single-news.html">Read More [+]</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Post Item -->

                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="img-hover">
                                                <img src="/torneo/img/blog/3.jpg" alt="" class="img-responsive">
                                                <div class="overlay"><a href="single-news.html">+</a></div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h4><a href="single-news.html">Porto Alegre and Cuiabá to welcome Valcke</a></h4>
                                            <p class="data-info">January  4, 2014  / <i class="fa fa-comments"></i><a href="#">3</a></p>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus rutrum, libero id imperdiet elementum, nunc... <a href="single-news.html">Read More [+]</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Post Item -->

                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="img-hover">
                                                <img src="/torneo/img/blog/3.jpg" alt="" class="img-responsive">
                                                <div class="overlay"><a href="single-news.html">+</a></div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h4><a href="single-news.html">Porto Alegre and Cuiabá to welcome Valcke</a></h4>
                                            <p class="data-info">January  4, 2014  / <i class="fa fa-comments"></i><a href="#">3</a></p>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus rutrum, libero id imperdiet elementum, nunc... <a href="single-news.html">Read More [+]</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Post Item -->

                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="img-hover">
                                                <img src="/torneo/img/blog/4.jpg" alt="" class="img-responsive">
                                                <div class="overlay"><a href="single-news.html">+</a></div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h4><a href="single-news.html">Porto Alegre and Cuiabá to welcome Valcke</a></h4>
                                            <p class="data-info">January 8, 2014  / <i class="fa fa-comments"></i><a href="#">2</a></p>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus rutrum, libero id imperdiet elementum, nunc... <a href="single-news.html">Read More [+]</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Post Item -->
                            </div>
                        </div>
                        <!-- End Recent Post -->
                    </div>
                    <!-- End content Left -->

                    <!-- Side Bar -->
                    <div class="col-md-4 col-sm-6">
                        <!-- Widget Partidos Anteriores -->
                        <div class="panel panel-default small-arrows">
                            <div class="panel-heading">
                                <h3 class="panel-title">Últimos Partidos</h3>
                            </div>
                            <div class="panel-body nopadding tooltip-hover">
                                <ul class="single-carousel">
                                    <li>
                                        <div class="widget-partido">
                                            <div class="col-xs-4">
                                                <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                                <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                            </div>
                                            <div class="col-xs-4 resultado">
                                                <div class="thumbnail">
                                                    <h2>2<small><small>3</small></small></h2>
                                                </div>
                                                <div class="thumbnail">
                                                    <h2>2<small><small>5</small></small></h2>
                                                </div>
                                                <i class="flaticon-football85" data-toggle="tooltip" title="Árbitro: Gabriel Faballe"></i>
                                                <span class="glyphicon glyphicon-time" data-toggle="tooltip" title="25 Marzo 2015 22:00"></span>
                                                <i class="flaticon-football96" data-toggle="tooltip" title="Complejo Tiruyaki"></i>
                                                <a href="#ruta-al-partido" class="btn btn-primary btn-xs">+ Info</a>
                                            </div>

                                            <div class="col-xs-4">
                                                <div class="camiseta-equipo">
                                                    <div>
                                                        <i class="flaticon-football114" style="color: #005A96"></i>
                                                    </div><!--
        --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                    </div>
                                                </div>
                                                <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="widget-partido">
                                            <div class="col-xs-4">
                                                <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                                <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                            </div>
                                            <div class="col-xs-4 resultado">
                                                <div class="thumbnail">
                                                    <h2>2<small><small>3</small></small></h2>
                                                </div>
                                                <div class="thumbnail">
                                                    <h2>2<small><small>5</small></small></h2>
                                                </div>
                                                <i class="flaticon-football85" data-toggle="tooltip" title="Árbitro: Gabriel Faballe"></i>
                                                <span class="glyphicon glyphicon-time" data-toggle="tooltip" title="25 Marzo 2015 22:00"></span>
                                                <i class="flaticon-football96" data-toggle="tooltip" title="Complejo Tiruyaki"></i>
                                                <a href="#ruta-al-partido" class="btn btn-primary btn-xs">+ Info</a>
                                            </div>

                                            <div class="col-xs-4">
                                                <div class="camiseta-equipo">
                                                    <div>
                                                        <i class="flaticon-football114" style="color: #005A96"></i>
                                                    </div><!--
--><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                    </div>
                                                </div>
                                                <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <!-- END Widget Partidos Anteriores -->

                        <!-- Widget Podio -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Podio</h3>
                            </div>
                            <div class="panel-body nopadding">
                                <div class="podio podio-md tooltip-hover">
                                    <div class="segundo">
                                        <a href="#link-equipo" data-toggle="tooltip" title="2do Puesto: Boca Juniors">
                                            <img src="/torneo/img/img-theme/equipo.png" alt="" />
                                        </a>
                                    </div>
                                    <div class="primero">
                                        <a href="#link-equipo" data-toggle="tooltip" title="1er Puesto: Boca Juniors">
                                            <div class="camiseta-equipo">
                                                <div>
                                                    <i class="flaticon-football114" style="color: #005A96"></i>
                                                </div><!---->
                                                <div class="segunda-mitad">
                                                <i class="flaticon-football114" style="color: #FAD201"></i>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="tercero">
                                        <a href="#link-equipo" data-toggle="tooltip" title="3er Puesto: Boca Juniors">
                                            <img src="/torneo/img/img-theme/equipo.png" alt="" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END Widget Podio -->

                        <!-- Widget Tabla de Posiciones -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Tabla de Posiciones</h3>
                            </div>
                            <div class="panel-body nopadding">
                                <table class="table table-striped text-center nomargin-bottom">
                                    <thead>
                                        <tr>
                                            <th class="text-center" colspan="2">Equipo</th>
                                            <th>PTS</th>
                                            <th>PJ</th>
                                            <th>PG</th>
                                            <th>PE</th>
                                            <th>PP</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                        </tr>
                                        <tr>
                                            <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                            <td>River Plate</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>5</td>
                                            <td>3</td>
                                            <td>3</td>
                                        </tr>

                                    </tbody>
                                </table>
                                <a class="center-block text-center" href="#">
                                    + MAS INFO
                                </a>
                            </div>
                        </div>
                        <!-- END Tabla de Posiciones -->

                    </div>
                    <!-- End Side Bar -->
                </div>
            </div>
            <!-- End Content Central -->

            <!-- Sponsors -->
            <div class="section-wide panel panel-default">
                <div class="container panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <h2><span class="text-resalt">32</span> equipos participantes <small>Conocelos</small></h2>
                            </div>
                            <ul class="equipos-home tooltip-hover">
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!----><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!---->
                                            <div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!---->
                                            <div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!---->
                                            <div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!--
--><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!--
--><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!--
--><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <div class="camiseta-equipo">
                                            <div>
                                                <i class="flaticon-football114" style="color:#005A96"></i>
                                            </div><!--
--><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color:#FAD201"></i>
                                            </div>
                                        </div>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo.png" class="img-responsive center-block">
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Sponsors -->

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

      </section>

</asp:Content>
