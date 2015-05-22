<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="miscelaneas.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <script src="/torneo/js/blur-card.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1>Misceláneas - <%= gestorEdicion.edicion.nombre %></h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%# Logica.GestorUrl.urlTorneo(nickTorneo)%>"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%# Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion)%>"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%# Logica.GestorUrl.urlMiscelanea(nickTorneo,idEdicion)%>">Misceláneas</a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

        <!-- Content Central -->
        <div class="container padding-top">
            <div class="row mobile-margin-top">

                <!-- Widget Datos Edicion -->
                <div class="col-md-4 col-sm-4" style="margin-bottom: 10px;">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"><span class="flaticon-trophy5"></span>&nbsp;&nbsp; <a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo, gestorEdicion.edicion.idEdicion)  %>"><%= gestorEdicion.edicion.nombre %></a> </h3>
                        </div>
                        <ul class="list-group">
                            <li class="list-group-item"><i class="flaticon-football65"></i>&nbsp;&nbsp;Cancha: <%= gestorEdicion.edicion.tamanioCancha.nombre %></li>
                            <li class="list-group-item"><i class="glyphicon glyphicon-picture"></i>&nbsp;&nbsp;Superficie: <%= gestorEdicion.edicion.tipoSuperficie.nombre %></li>
                            <li class="list-group-item"><i class="glyphicon glyphicon-info-sign"></i>&nbsp;&nbsp;Puntos: G: <%= gestorEdicion.edicion.puntosGanado %> - P: <%= gestorEdicion.edicion.puntosPerdido %> - E: <%= gestorEdicion.edicion.puntosEmpatado %>   </li>
                            <li class="list-group-item"><i class="flaticon-soccer18"></i>&nbsp;&nbsp;<a href="<%= Logica.GestorUrl.urlEquipos(nickTorneo, gestorEdicion.edicion.idEdicion)  %>"><%= gestorEdicion.edicion.equipos.Count %> equipos participantes.</a>  </li>
                            <li class="list-group-item"><i class="glyphicon glyphicon-info-sign"></i>&nbsp;&nbsp;Fútbol <%= gestorEdicion.edicion.generoEdicion.nombre  %> </li>
                        </ul>
                    </div>
                </div>
                <!-- End Widget Datos Edicion -->

                <!-- Widget Arbitros -->
                <div class="col-md-4 col-sm-4" style="margin-bottom: 10px;">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"><span class="flaticon-football85"></span>&nbsp;&nbsp; Árbitros</h3>
                        </div>
                        <div class="panel-body nopadding">
                            <ul class="single-carousel">
                                <asp:Repeater ID="rptAribitros" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="card theme-bg-color">
                                                <div class="header-bg"></div>
                                                <div class="avatar">
                                                    <img src="" alt="" />
                                                </div>
                                                <div class="content">
                                                    <p><span class="btn btn-default"><%# Eval("nombreArbitro") %></span></p>
                                                    <p>
                                                        <%#Eval("CantPartidosArbitradosEdicion") %> partidos en la Edición
                                                        <br>
                                                        <%#Eval("CantPartidosArbitradosTorneo") %> partidos en el Torneo
                                                    </p>
                                                </div>
                                                <img class="src-image" src="<%# new Logica.GestorArbitro().obtenerObjetoArbitroPorId(int.Parse(Eval("idArbitro").ToString())).obtenerImagenMediana() %>" />
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!-- Versus: Puntos -->
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Aribitros -->


                <!-- Widget Canchas -->
                <div class="col-md-4 col-sm-4" style="margin-bottom: 10px;">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"><span class="flaticon-football65"></span>&nbsp;&nbsp;Canchas</h3>
                        </div>
                        <div class="panel-body nopadding">
                            <ul class="single-carousel">
                                <asp:Repeater ID="rptCanchas" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="card theme-bg-color">
                                                <div class="header-bg"></div>
                                                <div class="avatar">
                                                    <img src="" alt="" />
                                                </div>
                                                <div class="content">
                                                    <p><span class="btn btn-default"><%#  ((Entidades.Cancha)Container.DataItem).nombre%></span></p>
                                                    <p>
                                                        <span class="glyphicon glyphicon-home"></span> <%#((Entidades.Cancha)Container.DataItem).domicilio %>
                                                        <br>
                                                        <span class="glyphicon glyphicon-earphone"></span> <%#((Entidades.Cancha)Container.DataItem).telefono%>
                                                    </p>
                                                </div>
                                                <img class="src-image" src="<%# ((Entidades.Cancha)Container.DataItem).obtenerImagenMediana() %>" />
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!-- Versus: Puntos -->
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Canchas -->
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"><span class="flaticon-football124"></span> Valla menos vencida</h3>
                        </div>
                        <div class="panel-body nopadding-horizontal panel-maxheight maxheight-md">
                            <table class="table table-condensed table-hover">
                                <thead>
                                    <tr>
                                        <th class="col-md-4 text-center" colspan="2">Equipo</th>
                                        <th class="col-md-2 text-center">PJ</th>
                                        <th class="col-md-3 text-center">Goles en contra</th>
                                        <th class="col-md-3 text-center">Promedio</th>
                                    </tr>
                                </thead>
                                <tbody class="tablaFiltro">
                                    <asp:Repeater ID="rptVallaMenosVencida" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                                        <%# new Entidades.Equipo(){idEquipo=int.Parse(Eval("idEquipo").ToString())}.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %></a>
                                                </td>
                                                <td><a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>"><%# Eval("equipo") %></a></td>
                                                <td class="text-center"><%# Eval("PJ") %></td>
                                                <td class="text-center"><%# Eval("GC") %></td>
                                                <td class="text-center"><%# Math.Round(double.Parse(Eval("promedio").ToString()), 2) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr id="msjValla" runat="server" visible="false">
                                        <td colspan="3">No hay información de goles</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default small-arrows ">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"><span class="glyphicon glyphicon-thumbs-up"></span> Ranking Fair Play</h3>
                        </div>
                        <div class="panel-body nopadding-horizontal panel-maxheight maxheight-md">
                            <table class="table table-condensed table-hover">
                                <thead>
                                    <tr>
                                        <th class="col-md-4 text-center" colspan="2">Equipo</th>
                                        <th class="col-md-2 text-center">Total Tarjetas</th>
                                        <th class="col-md-1 text-center">Rojas</th>
                                        <th class="col-md-1 text-center">Amarillas</th>
                                    </tr>
                                </thead>
                                <tbody class="tablaFiltro">
                                    <asp:Repeater ID="rptFairPlay" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                                        <%# new Entidades.Equipo(){idEquipo=int.Parse(Eval("idEquipo").ToString())}.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") %></a>
                                                </td>
                                                <td><a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>"><%# Eval("equipo") %></a></td>
                                                <td class="text-center"><%# Eval("cantidad") %></td>
                                                <td class="text-center"><%# Eval("TR") %></td>
                                                <td class="text-center"><%# Eval("TA") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr id="msjFairPLay" runat="server" visible="false">
                                        <td colspan="3">No hay información disponible</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- END contentPages-->
</asp:Content>
