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
                <div class="col-sm-12" runat="server" id="divOtrosPartidosDeFecha" visible="true">
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
                                        <li ><a href="#ultimos-eventos" data-toggle="tab">EVENTOS</a></li>
                                        <li class="active"><a href="#estadisticas" data-toggle="tab">ESTADÍSTICAS</a></li>
                                    </ul>
                                    <!-- End Nav Tabs -->

                                    <!-- Content Tabs -->
                                    <div class="panel">
                                        <div class="panel-body nopadding-horizontal bg-dark">
                                            <div class="tab-content home">
                                                <!-- Tab One - Feature News -->
                                                <div class="tab-pane" id="ultimos-eventos">
                                                    <!-- blog post-->
                                                    <ul id="events-carousel" class="events-carousel padding-top">
                                                        <!-- Item blog post -->
                                                          <asp:Repeater ID="rptEventos" runat="server">
                                                              <ItemTemplate>
                                                        <li>
                                                            <div class="header-post">
                                                               <div class="date">
                                                                    <span><%#((DateTime)((Entidades.Noticia)Container.DataItem).fecha).ToString("dd/MM")%></span>
                                                                     <%#((DateTime)Eval("fecha")).Year %>  
                                                                </div>
                                                                <a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion,((Entidades.Noticia)Container.DataItem).idNoticia)%>"><img src="<%# ((Entidades.Noticia)Container.DataItem).obtenerImagenMediana() %>" alt=""></a>                                                            
                                                            </div>
                                                            <div class="info-post descripcion-evento">
                                                                <h4><a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion,((Entidades.Noticia)Container.DataItem).idNoticia)%>"><%# Eval("titulo").ToString().Substring(0, Eval("titulo").ToString().Length > 22 ? 22 : Eval("titulo").ToString().Length)%> <%#Eval("titulo").ToString().Length > 22 ? "..." : "" %> </a></h4>
                                                                <p><%# Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Substring(0, Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length > 57  ? 57 : Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length)  %>...</p>
                                                            </div>
                                                        </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                        <!-- End Item blog post -->
                                                    </ul>
                                                    <!-- End blog post-->
                                                    <div id="sinEventos" runat="server"  class="alert alert-info col-md-10 col-md-offset-1 mobile-margin-top"  visible="false">
                                                        <small>No hay eventos para mostrar</small>  
                                                     </div> 
                                                </div>
                                                <!-- Tab One - Feature News -->

                                                <!-- Tab Two - Players Staff -->
                                                <div class="tab-pane active" id="estadisticas">
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
                                                                                        <td><%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("Id Equipo").ToString())}).obtenerImagen(Utils.GestorImagen.CHICA, "avatar-xs")%></td>
                                                                                        <td><%# (new Entidades.Jugador() { idJugador=int.Parse(Eval("Id Jugador").ToString())}).obtenerImagen(Utils.GestorImagen.CHICA, "img-circle avatar-xs", "", false)  %>
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
                                                                                        <td><%# (new Entidades.Equipo() { idEquipo=int.Parse(Eval("Id Equipo").ToString())}).obtenerImagen(Utils.GestorImagen.CHICA, "avatar-xs")%></td>
                                                                                        <td>
                                                                                            <%# (new Entidades.Jugador() { idJugador=int.Parse(Eval("Id Jugador").ToString())}).obtenerImagen(Utils.GestorImagen.CHICA, "img-circle avatar-xs", "", false)  %>
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
                                <div class="col-md-3" runat="server" id="divProximoPartido" visible = "false">
                                    <aside>
                                        <div class="title-color text-center">
                                            <h4>Próximo Partido</h4>
                                        </div>

                                        <div class="content-counter content-counter-home">
                                            <asp:Panel ID="pnlConProgramacion" runat="server" Visible="false">
                                                <p class="text-center">
                                                    <i class="fa fa-clock-o"></i>
                                                    Cuenta Regresiva
                                                </p>
                                                <div id="counter-proximo-partido" class="counter"></div>
                                                <ul class="post-options">
                                                    <li><i class="fa fa-calendar"></i><asp:Literal ID="ltDiaDePartido" runat="server"/> <asp:Literal ID="ltFechaPartido" runat="server"/></li>
                                                    <li><i class="fa fa-clock-o"></i><asp:Literal ID="ltHoraPartido" runat="server"/> hs</li>
                                                </ul>
                                            </asp:Panel>
                                            <div class="widget-partido">
                                                <div class="col-xs-6">
                                                    <% if(gestorEdicion.edicion.estado.idEstado == Entidades.Estado.edicionCONFIGURADA || gestorEdicion.edicion.estado.idEstado == Entidades.Estado.edicionINICIADA) {%>
                                                        <%= (new Entidades.Equipo() { idEquipo= proximoPartido.local.idEquipo}).obtenerImagen(Utils.GestorImagen.MEDIANA, "") %>   
                                                    <% } %>                                                 
                                                    <h5><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, proximoPartido.local.idEquipo) %>" data-toggle="tooltip" title="Ver Equipo"><asp:Literal ID="ltEquipoLocal" runat="server"/></a></h5>
                                                </div>
                                                <div class="col-xs-6">
                                                    <% if(gestorEdicion.edicion.estado.idEstado == Entidades.Estado.edicionCONFIGURADA || gestorEdicion.edicion.estado.idEstado == Entidades.Estado.edicionINICIADA) {%>
                                                    <%= (new Entidades.Equipo() { idEquipo= proximoPartido.visitante.idEquipo}).obtenerImagen(Utils.GestorImagen.MEDIANA, "")%> 
                                                    <% } %>                                                   
                                                    <h5><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, proximoPartido.visitante.idEquipo) %>" data-toggle="tooltip" title="Ver Equipo"><asp:Literal ID="ltEquipoVisitante" runat="server"/></a></h5>
                                                </div>
                                            </div>
                                            <a class="btn btn-primary" href="<%= Logica.GestorUrl.urlPartido(nickTorneo, idEdicion, proximoPartido.idPartido.ToString()) %>">
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
                                <h3 class="panel-title">Últimas Noticias</h3>
                            </div>
                            <div class="panel-body">
                               <asp:Repeater ID="rptUltimasNoticias" runat="server">
                                   <ItemTemplate>
                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row box-noticias">
                                        <div class="imagen-noticia imagen-noticia-thumb">
                                            <div class="img-hover">
                                                <img src="<%#((Entidades.Noticia)Container.DataItem).obtenerImagenMediana()%>" alt="" class="img-responsive">
                                                <div class="overlay"><a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion,((Entidades.Noticia)Container.DataItem).idNoticia)%>">+</a></div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h4><a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion,((Entidades.Noticia)Container.DataItem).idNoticia)%>"><%# Eval("titulo") %></a></h4>
                                            <p class="data-info"><%# nombreMes(DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Month)+" "+DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Day.ToString()+", "+DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Year.ToString() %></p><!-- <i class="fa fa-comments"></i><a href="#">0</a> --> 
                                            <p><%# Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Substring(0,Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length >= 150 ? 150 : Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length)  %>... <a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion, ((Entidades.Noticia)Container.DataItem).idNoticia)%>">Leer Más [+]</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Post Item -->
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div id="sinNoticias" runat="server" visible="false">
                                   <span>No se han cargado noticias</span>
                                </div>
                            </div>
                        </div>
                        <!-- End Recent Post -->
                    </div>
                    <!-- End content Left -->

                    <!-- Side Bar -->
                    <div class="col-md-4 col-sm-6">
                        <!-- Widget Partidos Anteriores -->
                        <div id="divUtimosPartidos" class="panel panel-default small-arrows" runat="server" visible="true">
                            <div class="panel-heading">
                                <h3 class="panel-title">Últimos Partidos</h3>
                            </div>
                            <div class="panel-body nopadding tooltip-hover">
                                <ul class="single-carousel">
                                    <asp:Repeater ID="rptUltimosPartidos" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <div class="widget-partido">
                                                    <div class="col-xs-4">
                                                        <%# (new Entidades.Equipo() { idEquipo = int.Parse(Eval("local.idEquipo").ToString())}).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                                        <h5><a href="#" data-toggle="tooltip" title="Ver Equipo"><%# Eval("local.nombre")%></a></h5>
                                                    </div>
                                                    <div class="col-xs-4 resultado">
                                                        <div class="thumbnail">
                                                            <h2><%# Eval("golesLocal")%><small><small><%# Eval("penalesLocal")%></small></small></h2>
                                                        </div>
                                                        <div class="thumbnail">
                                                            <h2><%# Eval("golesVisitante")%><small><small><%# Eval("penalesVisitante")%></small></small></h2>
                                                        </div>
                                                        <i class="flaticon-football85" data-toggle="tooltip" title=""<%# Eval("arbitro.nombre")%>></i>
                                                        <span class="glyphicon glyphicon-time" data-toggle="tooltip" title=<%# Eval("fecha")%>></span>
                                                        <i class="flaticon-football96" data-toggle="tooltip" title=<%# Eval("cancha.nombre")%>></i>
                                                        <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion, Eval("idPartido").ToString()) %>" class="btn btn-primary btn-xs">+ Info</a>
                                                    </div>

                                                    <div class="col-xs-4">
                                                         <%# (new Entidades.Equipo() { idEquipo = int.Parse(Eval("visitante.idEquipo").ToString())}).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                                        <h5><a href="#" data-toggle="tooltip" title="Ver Equipo"><%# Eval("visitante.nombre")%></a></h5>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <!-- END Widget Partidos Anteriores -->

                        <!-- Widget Podio -->
                        <asp:Panel ID="pnlPodio" runat="server" Visible ="false" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Podio</h3>
                            </div>
                            <div class="panel-body nopadding">
                                <div class="podio podio-md tooltip-hover">
                                    <div class="segundo">
                                        <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, podio[1].idEquipo)%>" data-toggle="tooltip" title="2do Puesto: <%= podio[1].nombre %>">
                                            <%= (new Entidades.Equipo() { idEquipo = podio[1].idEquipo }).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                        </a>
                                    </div>
                                    <div class="primero">
                                        <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, podio[0].idEquipo)%>"" data-toggle="tooltip" title="1er Puesto: <%= podio[0].nombre %>">
                                            <%= (new Entidades.Equipo() { idEquipo = podio[0].idEquipo }).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                        </a>
                                    </div>
                                    <div class="tercero">
                                        <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, podio[2].idEquipo)%>" data-toggle="tooltip" title="3er Puesto: <%= podio[2].nombre %>">
                                            <%= (new Entidades.Equipo() { idEquipo = podio[2].idEquipo }).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                       </asp:Panel>
                        <!-- END Widget Podio -->

                        <!-- Widget Tabla de Posiciones -->
                        <div class="panel panel-default" id="divTablaPosiciones" runat="server" visible="true">
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
                                        <asp:Repeater ID="rptTablaPosiciones" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# (new Entidades.Equipo() { idEquipo = int.Parse(Eval("idEquipo").ToString())}).obtenerImagen(Utils.GestorImagen.CHICA, "img-circle avatar-xs")%></td>
                                                    <td><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, int.Parse(Eval("idEquipo").ToString()))%>" data-toggle="tab"><%# Eval("Equipo") %></a></td>
                                                    <td><b><%# Eval("Puntos")%></b></td>
                                                    <td><%# Eval("PJ")%></td>
                                                    <td><%# Eval("PG")%></td>
                                                    <td><%# Eval("PE")%></td>
                                                    <td><%# Eval("PP")%></td>
                                                </tr>
                                             </ItemTemplate>
                                        </asp:Repeater>                                        
                                    </tbody>
                                </table>
                                <a class="center-block text-center" href="<%= Logica.GestorUrl.urlPosiciones(nickTorneo,idEdicion) %>">
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
            <div class="section-wide panel panel-default" id="divEquiposParticipantes" runat="server" visible="true">
                <div class="container panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <h2><span class="text-resalt"><asp:Literal ID="ltCantidadEquipo" runat="server"/></span> equipos participantes <small>Conocelos</small></h2>
                            </div>
                            <ul class="equipos-home tooltip-hover">
                                <asp:Repeater ID="rptEquiposParticipantes" runat="server">
                                    <ItemTemplate>
                                        <li class="li-item" data-toggle="tooltip" title="<%# Eval("nombre")%>">
                                            <a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, int.Parse(Eval("idEquipo").ToString()))%>">
                                                <%# (new Entidades.Equipo() { idEquipo = int.Parse(Eval("idEquipo").ToString())}).obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                            </a>
                                        </li>
                                     </ItemTemplate>
                                </asp:Repeater>
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
