<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="partido.aspx.cs" Inherits="quegolazo_code.torneo.proximoPartido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1><%=gestorPartido.partido.nombreCompleto %> </h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>"><%=gestorTorneo.torneo.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo, idEdicion) %>"><%=gestorEdicion.edicion.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlFechasFase(nickTorneo, idEdicion, gestorPartido.partido.faseAsociada.idFase) %>">Fase <%=gestorPartido.partido.faseAsociada.idFase%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlFechas(nickTorneo, idEdicion, gestorPartido.partido.faseAsociada.idFase, gestorPartido.partido.idFecha) %>">Fecha <%=gestorPartido.partido.idFecha %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlPartido(nickTorneo, idEdicion, gestorPartido.partido.idPartido.ToString()) %>"><%=gestorPartido.partido.nombreCompleto %></a></li>
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
                                <asp:Repeater ID="rptOtrosPartidosDeLaFecha" runat="server">
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

                <!-- Tablero de Resultados -->
                <div class="col-sm-12">
                    <div class="panel-box score bg-dark principal theme-border">
                        <div class="row">
                            <div class="col-md-4 col-xs-3 nopadding-right padding-top">
                                <%=  gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                <h3 class="text-center"><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre  %></a></h3>
                            </div>
                            <div class="col-xs-6 col-md-4">
                                <div class="row text-center resultado">                                    
                                    <div class="col-xs-5 nopadding-left">
                                        <% if(gestorPartido.partido.golesLocal != null) {%> 
                                            <h1><%=gestorPartido.partido.golesLocal %> 
                                            <% if(gestorPartido.partido.huboPenales == true) { %>
                                                <small>(<%=gestorPartido.partido.penalesLocal %>)</small>
                                            <% } %>
                                            </h1>
                                        <% } %>
                                    </div>
                                    <div class="col-xs-2 nopadding-right nopadding-left">
                                        <h1>-</h1>
                                    </div>                                    
                                    <div class="col-xs-5 nopadding-right">
                                        <% if(gestorPartido.partido.golesVisitante != null) {%> 
                                            <h1><%=gestorPartido.partido.golesVisitante %> 
                                            <% if(gestorPartido.partido.huboPenales == true) { %>
                                                <small>(<%=gestorPartido.partido.penalesVisitante %>)</small>
                                            <% } %>
                                            </h1>
                                        <% } %>
                                    </div>
                                </div>
                                <div class="row text-center">
                                    <div class="col-xs-12">
                                        <ul class="list-group">
                                            <% if(gestorPartido.partido.fecha != null) { %>
                                            <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><span class="hidden-xs"> <%= nombreDia(DateTime.Parse(gestorPartido.partido.fecha.ToString())) %> </span><%= gestorPartido.partido.fecha %></li>
                                            <% } %>
                                            <% if(gestorPartido.partido.cancha != null) { %>
                                            <li class="list-group-item hidden-xs"><span class="glyphicon glyphicon-home" aria-hidden="true"></span> <%= gestorPartido.partido.cancha.nombre %></li>
                                             <% } %>
                                            <% if(gestorPartido.partido.arbitro != null) { %>
                                            <li class="list-group-item hidden-xs"><span class="flaticon-black188" aria-hidden="true"></span> <%= gestorPartido.partido.arbitro.nombre %></li>
                                             <% } %>
                                            <li class="list-group-item hidden-xs"><span class="label label-success"> Partido <%= gestorPartido.partido.estado.nombre %></span></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3 col-md-4 padding-top">
                                 <%=  gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.GRANDE, "")%>
                                <h3 class="text-center"><a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h3>
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
                                                    <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                    <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a>
                                                </th>
                                                <th class="col-xs-8 col-md-4 text-center">VS</th>
                                                <th class="col-xs-2 col-md-4 text-center">
                                                    <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                    <a href="<%= Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a>
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
                                                    <span id="sinGolesLocal" runat="server" visible="false">-</span>
                                                </td>
                                                <td>Goles</td>
                                                <td>
                                                    <asp:Repeater ID="rptGolesVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span class="flaticon-football28"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                    <span id="sinGolesVisitante" runat="server" visible="false">-</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasRojasLocal" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: red;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                    <span id="sinTarjetasRojasLocal" runat="server" visible="false">-</span>
                                                </td>
                                                <td class="text-center">Tarjetas Rojas</td>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasRojasVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: red;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                    <span id="sinTarjetasRojasVisitante" runat="server" visible="false">-</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasAmarillasLocal" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: yellow;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater>
                                                    <span id="sinTarjetasAmarillasLocal" runat="server" visible="false">-</span>
                                                </td>
                                                <td class="text-center">Tarjetas Amarillas</td>
                                                <td>
                                                    <asp:Repeater ID="rptTarjetasAmarillasVisitante" runat="server">
                                                            <ItemTemplate>
                                                                <span style="color: yellow;" class="flaticon-football103"></span>
                                                            </ItemTemplate>
                                                     </asp:Repeater> 
                                                    <span id="sinTarjetasAmarillasVisitante" runat="server" visible="false">-</span>                     
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptCambiosLocal" runat="server">
                                                            <ItemTemplate>
                                                               <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                            </ItemTemplate>
                                                     </asp:Repeater>  
                                                    <span id="sinCambiosLocal" runat="server" visible="false">-</span> 
                                                </td>
                                                <td class="text-center">Cambios</td>
                                                <td>
                                                    <asp:Repeater ID="rptCambiosVisitante" runat="server">
                                                            <ItemTemplate>
                                                               <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                            </ItemTemplate>
                                                     </asp:Repeater> 
                                                    <span id="sinCambiosVisitante" runat="server" visible="false">-</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                                <!-- Tab de Jugadores -->
                                <div class="tab-pane fade" id="titulares">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped table-hover table-jugadores">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTitularesLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <a id='jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>'  href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,gestorPartido.partido.local.idEquipo,int.Parse(Eval("idJugador").ToString())) %>" >
                                                                   <%# ((Entidades.Jugador)Container.DataItem).obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs","",false) %>
                                                                </a>
                                                            </td>
                                                            <td class="col-xs-9"><%# Eval("nombre") %></td>
                                                            <td class="col-xs-2"><%# Eval("numeroCamiseta") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>   
                                                <tr id="sinTitularesLocal" runat="server" visible="false">
                                                    <td colspan="3">No hay jugadores titulares registrados</td>
                                                </tr>                                              
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped table-hover table-jugadores">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                        <%= gestorPartido.partido.visitante.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTitularesVisitante" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-1">
                                                                <a id='jugador-<%# ((Entidades.Jugador)Container.DataItem).idJugador.ToString() %>' href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,gestorPartido.partido.visitante.idEquipo,int.Parse(Eval("idJugador").ToString())) %>" >
                                                                    <%# ((Entidades.Jugador)Container.DataItem).obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs","",false) %>
                                                                </a>
                                                            </td>
                                                            <td class="col-xs-9"><%# Eval("nombre") %></td>
                                                            <td class="col-xs-2"><%# Eval("numeroCamiseta") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>    
                                                <tr id="sinTitularesVisitante" runat="server" visible="false">
                                                    <td colspan="3">No hay jugadores titulares registrados</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tab de Goles -->
                                <div class="tab-pane fade" id="goles">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                        <%= gestorPartido.partido.local.nombre %>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTabGolesLocal" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Gol)Container.DataItem).minuto != null ? ((Entidades.Gol)Container.DataItem).minuto+"'": "-" %></span></td>
                                                            <td class="col-xs-7">
                                                                <%# ((Entidades.Gol)Container.DataItem).jugador != null ? ((Entidades.Gol)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) :  new Entidades.Jugador().obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false)   %>
                                                                <%# ((Entidades.Gol)Container.DataItem).jugador != null ? ((Entidades.Gol)Container.DataItem).jugador.nombre : "" %></td>
                                                            <td class="col-xs-3"><%# ((Entidades.Gol)Container.DataItem).tipoGol != null ? ((Entidades.Gol)Container.DataItem).tipoGol.nombre : "-"%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater> 
                                                <tr id="sinGolesTabLocal" runat="server" visible="false">
                                                    <td colspan="3">No hay información de goles registrada</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.visitante.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptTabGolesVisitante" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Gol)Container.DataItem).minuto != null ? ((Entidades.Gol)Container.DataItem).minuto+"'": "-" %></span></td>
                                                                <td class="col-xs-7">
                                                                    <%# ((Entidades.Gol)Container.DataItem).jugador != null ? ((Entidades.Gol)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false): new Entidades.Jugador().obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false)  %>
                                                                    <%# ((Entidades.Gol)Container.DataItem).jugador != null ? ((Entidades.Gol)Container.DataItem).jugador.nombre : "" %></td>
                                                                <td class="col-xs-3"><%# ((Entidades.Gol)Container.DataItem).tipoGol != null ? ((Entidades.Gol)Container.DataItem).tipoGol.nombre : "-"%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr id="sinGolesTabVisitante" runat="server" visible="false">
                                                        <td colspan="3">No hay información de goles registrada</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tab de Cambios -->
                                <div class="tab-pane fade" id="cambios">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.local.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptTabCambiosLocal" runat="server">
                                                        <ItemTemplate>
                                                           <tr>
                                                            <td class="col-xs-1"><span class="text-lg"><%# ((Entidades.Cambio)Container.DataItem).minuto != null ? ((Entidades.Cambio)Container.DataItem).minuto+"'" : "-" %></span></td>
                                                            <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                            <td class="col-xs-4">
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %></td>
                                                            <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                            <td class="col-xs-4">
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %></td>
                                                           </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>  
                                                    <tr id="sinCambiosTabLocal" runat="server" visible="false">
                                                        <td colspan="5">No hay información de cambios registrada</td>
                                                    </tr>                                             
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.visitante.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptTabCambiosVisitante" runat="server">
                                                        <ItemTemplate>
                                                           <tr>
                                                            <td class="col-xs-1"><span class="text-lg"><%# ((Entidades.Cambio)Container.DataItem).minuto != null ? ((Entidades.Cambio)Container.DataItem).minuto+"'" : "-" %></span></td>
                                                            <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                            <td class="col-xs-4">
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %></td>
                                                            <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                            <td class="col-xs-4">
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %></td>
                                                           </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr id="sinCambiosTabVisitante" runat="server" visible="false">
                                                        <td colspan="5">No hay información de cambios registrada</td>
                                                    </tr>   
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tab de Tarjetas -->
                                <div class="tab-pane fade" id="tarjetas">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.local.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptTabTarjetasAmarillasLocal" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto != null ? ((Entidades.Tarjeta)Container.DataItem).minuto+"'" : "-" %></span></td>
                                                                <td class="col-xs-8">
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                                <td class="colrptTabTarjetasAmarillasLocal-xs-2">
                                                                    <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater> 
                                                    <asp:Repeater ID="rptTabTarjetasRojasLocal" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-2"><span class="text-lg"><%# ((Entidades.Tarjeta)Container.DataItem).minuto != null ? ((Entidades.Tarjeta)Container.DataItem).minuto+"'" : "-" %></span></td>
                                                                <td class="col-xs-8">
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                                <td class="col-xs-2">
                                                                    <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater> 
                                                    <tr id="sinTarjetasTabLocal" runat="server" visible="false">
                                                        <td colspan="3">No hay información de tarjetas registrada</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped text-middle">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
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
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
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
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.MEDIANA,"img-circle avatar-sm","",false) %>
                                                                    <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %></td>
                                                                <td class="col-xs-2">
                                                                    <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>                                                           
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr id="sinTarjetasTabVisitante" runat="server" visible="false">
                                                        <td colspan="3">No hay información de tarjetas registrada</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <!-- Tab de Sanciones -->
                                <div class="tab-pane fade" id="sanciones">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.local.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptSancionesLocal" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-1">
                                                                      <%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs","",false) : ""  %>
                                                                    <%--<img src="<%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagenChicha() : "/torneo/img/img-theme/jugador-mediano.jpg"  %>" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>--%>
                                                                <td class="col-xs-4"><%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.nombre : "-" %></td>
                                                                <td class="col-xs-7">Sancionado por <%# ((Entidades.Sancion)Container.DataItem).cantidadFechasSuspendidas != null ? ((Entidades.Sancion)Container.DataItem).cantidadFechasSuspendidas.ToString() : "-" %> Fechas</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr id="sinSancionesLocal" runat="server" visible="false">
                                                        <td colspan="3">No hay sanciones registradas</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="panel-maxheight maxheight-md">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center" colspan="3">
                                                            <%= gestorPartido.partido.visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %>
                                                            <%= gestorPartido.partido.visitante.nombre %>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptSancionesVisitante" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="col-xs-1">
                                                                    <%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs","",false) : "" %>
                                                                        <%--<img src="<%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.obtenerImagenChicha() : "/torneo/img/img-theme/jugador-mediano.jpg"  %>" class="img-responsive avatar-xs" alt="" style="/*height: 22px; max-width: 30px;*/"></td>--%>
                                                                <td class="col-xs-4"><%# ((Entidades.Sancion)Container.DataItem).jugador != null ? ((Entidades.Sancion)Container.DataItem).jugador.nombre : "-" %></td>
                                                                <td class="col-xs-7"><%# ((Entidades.Sancion)Container.DataItem).cantidadFechasSuspendidas != null ? "Sancionado por "+((Entidades.Sancion)Container.DataItem).cantidadFechasSuspendidas.ToString()+" Fechas" : "-" %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr id="sinSancionesVisitante" runat="server" visible="false">
                                                        <td colspan="3">No hay sanciones registradas</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Resumen del Partido -->

               
                <!-- Widget Partidos Anteriores Equipo Local -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title"><asp:Literal ID="ltUltimosOProximosEL" runat="server" Text="Últimos"></asp:Literal> Partidos: <%= gestorPartido.partido.local.nombre %></h3>
                        </div>                        
                        <div class="panel-body nopadding">
                            <ul class="single-carousel">
                                <asp:Repeater ID="rptUltimosPartidosEquipoLocal" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="widget-partido">
                                                <div class="col-xs-4">
                                                        <%# ((Entidades.Partido)Container.DataItem).local.obtenerImagen(Utils.GestorImagen.MEDIANA,"") %>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, ((Entidades.Partido)Container.DataItem).local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%# Eval("local.nombre") %></a></h5>
                                                </div>
                                                <div class="col-xs-4 resultado">
                                                    <div class="thumbnail">
                                                        <%--<h2><%# Eval("golesLocal") %></h2>--%>
                                                         <h2><%# (((Entidades.Partido)Container.DataItem).golesLocal != null) ? ((Entidades.Partido)Container.DataItem).golesLocal.ToString() :  "-" %></h2>
                                                    </div>
                                                    <div class="thumbnail">
                                                        <%--<h2><%# Eval("golesVisitante") %></h2>--%>
                                                        <h2><%# (((Entidades.Partido)Container.DataItem).golesVisitante != null) ? ((Entidades.Partido)Container.DataItem).golesVisitante.ToString() :  "-" %></h2>
                                                    </div>
                                                    Fecha <%# Eval("idFecha") %>
                                                </div>
                                                <div class="col-xs-4">
                                                     <%# ((Entidades.Partido)Container.DataItem).visitante.obtenerImagen(Utils.GestorImagen.MEDIANA,"") %>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, ((Entidades.Partido)Container.DataItem).visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%# Eval("visitante.nombre") %></a></h5>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>                                                                                             
                            </ul>
                        </div>                     
                    </div>
                </div>
                <!-- End Widget Partidos Anteriores Equipo Local -->

                <!-- Widget Versus -->
                <div class="col-md-4 col-sm-6" style="margin-bottom: 10px;">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Versus</h3>
                        </div>
                        <div class="panel-body onlypadding-top">
                            <ul class="single-carousel">
                                <!-- Versus: Puntos -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPuntosEL" runat="server"/></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <span class="flaticon-football31" aria-hidden="true"></span><br />
                                            Puntos
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPuntosEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Puntos -->
                                <!-- Versus: Partidos Ganados -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartGanadosEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <img src="/torneo/img/img-theme/ganador.png" class="img-circle avatar-md" alt=""><br />
                                            Partidos Ganados
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartGanadosEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Partidos Ganados -->
                                <!-- Versus: Partidos Empatados -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartEmpatadosEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <img src="/torneo/img/img-theme/ganador.png" class="img-circle avatar-md" alt=""><br />
                                            Partidos Empatados
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartEmpatadosEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Partidos Empatados -->
                                <!-- Versus: Partidos Perdidos -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartPerdidosEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <img src="/torneo/img/img-theme/ganador.png" class="img-circle avatar-md" alt=""><br />
                                            Partidos Perdidos
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltPartPerdidosEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Partidos Perdidos -->

                                <!-- Versus: Tarjetas Rojas -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoTarjRojasEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-md" alt=""><br />
                                            Tarjetas Rojas
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoTarjRojasEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Rojas -->
                                <!-- Versus: Tarjetas Amarillas -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoTarjAmarillasEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-md" alt=""><br />
                                            Tarjetas Amarillas
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoTarjAmarillasEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Amarillas -->
                                <!-- Versus: Goles a Favor -->
                                <li>
                                    <div class="widget-versus">
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoGolesEL" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.local.nombre %></a></h5>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <span class="flaticon-flaming" aria-hidden="true" style="color: #E00C0C;"></span><br />
                                            Goles a Favor
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="thumbnail resultado">
                                                <h2><asp:Literal ID="ltComparativoGolesEV" runat="server" /></h2>
                                            </div>
                                            <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, gestorPartido.partido.visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%= gestorPartido.partido.visitante.nombre %></a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Goles a Favor -->
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Versus -->

                <!-- Widget Partidos Anteriores Equipo Visitante -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title"><asp:Literal ID="ltUltimosOProximosEV" runat="server" Text="Últimos"></asp:Literal> Partidos: <%= gestorPartido.partido.visitante.nombre %></h3>
                        </div>
                        <div class="panel-body nopadding">
                            <ul class="single-carousel">
                                <asp:Repeater ID="rptUltimosPartidosEquipoVisitante" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="widget-partido">                                                
                                                <div class="col-xs-4">
                                                    <%# ((Entidades.Partido)Container.DataItem).local.obtenerImagen(Utils.GestorImagen.MEDIANA,"") %>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, ((Entidades.Partido)Container.DataItem).local.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%# Eval("local.nombre") %></a></h5>
                                                </div>
                                                <div class="col-xs-4 resultado">
                                                    <div class="thumbnail">
                                                        <%--<h2><%# Eval("golesLocal") %></h2>--%>
                                                        <h2><%# (((Entidades.Partido)Container.DataItem).golesLocal != null) ? ((Entidades.Partido)Container.DataItem).golesLocal.ToString() :  "-" %></h2>
                                                    </div>
                                                    <div class="thumbnail">
                                                        <%--<h2><%# Eval("golesVisitante") %></h2>--%>
                                                         <h2><%# (((Entidades.Partido)Container.DataItem).golesVisitante != null) ? ((Entidades.Partido)Container.DataItem).golesVisitante.ToString() :  "-" %></h2>
                                                    </div>
                                                    Fecha <%# Eval("idFecha") %>
                                                </div>
                                                <div class="col-xs-4">
                                                    <%# ((Entidades.Partido)Container.DataItem).visitante.obtenerImagen(Utils.GestorImagen.MEDIANA,"") %>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, ((Entidades.Partido)Container.DataItem).visitante.idEquipo) %>" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo"><%# Eval("visitante.nombre") %></a></h5>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater> 
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
</asp:Content>
