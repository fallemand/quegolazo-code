<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web13" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <asp:UpdatePanel ID="upListadoFases" runat="server">
        <ContentTemplate>
            <!-- contentPages-->
            <!-- Titulo Sección -->
            <section class="section-title overlay-bg">
                <div class="container">
                    <h1>Fase
                <asp:Literal ID="litFase" runat="server"></asp:Literal>
                        <small>|</small> Fecha
                <asp:Literal ID="litFecha" runat="server"></asp:Literal>
                    </h1>
                </div>
            </section>
            <!-- End Titulo Sección -->

            <!-- Section Area - Content Central -->
            <section class="content-info">
                <div class="crumbs">
                    <div class="container">
                        <ul>
                            <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>"><%= nombreTorneo %></a></li>
                            <li>/</li>
                            <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion) %>"><%= gestorEdicion.edicion.nombre %></a></li>
                            <li>/</li>
                            <li><a href="<%= Logica.GestorUrl.urlFechasFase(nickTorneo,idEdicion,idFase) %>">Fase
                        <asp:Literal ID="litLnkFase" runat="server"></asp:Literal></a></li>
                            <li>/</li>
                            <li><a href="<%= Logica.GestorUrl.urlFechas(nickTorneo,idEdicion,idFase, idFecha) %>">Fecha
                        <asp:Literal ID="litLnkFecha" runat="server"></asp:Literal></a></li>
                        </ul>
                    </div>
                </div>

                <div class="semiboxshadow text-center">
                    <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
                </div>

                <!-- Content Central -->
                <div class="container padding-top">
                    <div class="row mobile-margin-top">

                        <!-- Seleccionar la Fase -->
                        <div class="col-sm-3">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body">
                                    <p class="slider-multiple-title">Seleccione la Fase</p>
                                    <ul class="fases slider-multiple theme-bg-color-2 tooltip-hover">
                                        <asp:Repeater ID="rptFases" runat="server" OnItemCommand="rptFases_ItemCommand1">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFase" runat="server" ClientIDMode="AutoID" CommandName="SeleccionarFase" CommandArgument='<%# Eval("idFase") %>'>
                                                <li class="li-item fase-<%# Eval("estado.nombre") %>" data-toggle="tooltip" title="Seleccionar Fase">
                                                    <div class="widget widget-md">
                                                        <h1><%# Eval("idFase") %></h1>
                                                        <span><%# Eval("estado.nombre") %></span>
                                                    </div>
                                                </li>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                      <div id="sinFases" runat="server"  class="alert alert-info col-md-10 col-md-offset-1 mobile-margin-top"  visible="false">
                                            <small> No existen Fases Registradas </small>
                                         </div> 
                                </div>
                            </div>
                        </div>
                        <!-- END Seleccionar la Fase -->

                        <!-- Seleccionar la Fecha -->
                        <div class="col-sm-9">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body">
                                    <p class="slider-multiple-title">Seleccione la Fecha</p>
                                    <ul class="fechas theme-bg-color-2 <%= (gestorEdicion.faseActual != null && gestorEdicion.faseActual.tipoFixture.idTipoFixture=="ELIM")? "eliminatoria" : "" %> slider-multiple tooltip-hover">
                                        <asp:Repeater ID="rptFechas" runat="server" OnItemCommand="rptFechas_ItemCommand">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFecha" CommandName="SeleccionarFecha" ClientIDMode="AutoID" runat="server" CommandArgument='<%# Eval("idFecha") %>'>
                                                <li class="li-item fecha-<%# Eval("estado.nombre") %>" data-toggle="tooltip" title="Seleccionar Fecha">
                                                    <div class="widget widget-md">
                                                        <h1><%# gestorEdicion.faseActual.tipoFixture.idTipoFixture=="ELIM" ?  Eval("nombre") + " " : Eval("idFecha")%> </h1>
                                                        <span><%# Eval("estado.nombre") %></span>
                                                    </div>
                                                </li>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                   <div id="sinFechas" runat="server"  class="alert alert-info col-md-10 col-md-offset-1 mobile-margin-top"  visible="false">
                                           <small>No existen Fechas Registradas</small>  
                                    </div> 
                                </div>
                            </div>
                        </div>
                        <!-- END Seleccionar la Fecha -->

                        <!-- Listado de Partidos -->
                        <div class="col-sm-12">
                            <asp:Repeater ID="rptGrupos" runat="server" OnItemDataBound="rptGrupos_ItemDataBound">
                                <ItemTemplate>
                                    <h5 class="page-title">Grupo <%# Eval("idGrupo") %></h5>
                                    <div id="grupo-1" class="panel score bg-dark theme-border panel-default">
                                        <div class="panel-body">
                                            <div class="row tooltip-hover">
                                                <asp:Repeater ID="rptPartidos" runat="server">
                                                    <ItemTemplate>
                                                        <!-- Widget Partido -->
                                                        <div class="col-md-4 col-sm-6" runat="server" id="divPartido" visible="<%# !((Entidades.Partido)Container.DataItem).esLibre()%>">
                                                            <div class="panel panel-default">
                                                                <div class="panel-body nopadding">
                                                                    <div class="widget-partido" >
                                                                        <div class="col-xs-4">
                                                                            <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.obtenerImagen(Utils.GestorImagen.MEDIANA, "") : "<img src='"+new Entidades.Equipo().obtenerImagenMediana()+"'/>" %>
                                                                            <h5><a href="<%# ((Entidades.Partido)Container.DataItem).local!=null ? Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("local.idEquipo").ToString())) :"" %>" data-toggle="tooltip" title="Ver Equipo"><%#((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre : "-" %> </a></h5>
                                                                        </div>
                                                                        <div class="col-xs-4 resultado">
                                                                            <div class="thumbnail">
                                                                                <h2><%# (((Entidades.Partido)Container.DataItem).golesLocal!=null) ? Eval("golesLocal") : "-"%><small><small><%# Eval("penalesLocal")%></small></small></h2>
                                                                            </div>
                                                                            <div class="thumbnail">
                                                                                <h2><%# (((Entidades.Partido)Container.DataItem).golesVisitante!=null) ? Eval("golesVisitante"): "-"%><small><small><%# Eval("penalesVisitante")%></small></small></h2>
                                                                            </div>
                                                                            <i class="flaticon-football85" data-toggle="tooltip" title="Árbitro: <%# Eval("arbitro.nombre")%>"></i>
                                                                            <span class="glyphicon glyphicon-time" data-toggle="tooltip" title="<%# Eval("fecha")%>"></span>
                                                                            <i class="flaticon-football96" data-toggle="tooltip" title="<%# Eval("cancha.nombre")%>"></i>
                                                                            <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion,Eval("idPartido").ToString()) %>" class="btn btn-primary btn-xs">+ Info</a>
                                                                        </div>
                                                                        <div class="col-xs-4">
                                                                            <%#  ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.obtenerImagen(Utils.GestorImagen.MEDIANA, "") : "<img src='"+new Entidades.Equipo().obtenerImagenMediana()+"' />"%>
                                                                            <h5><a href="<%# ((Entidades.Partido)Container.DataItem).visitante!=null ? Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("visitante.idEquipo").ToString())) :"" %>" data-toggle="tooltip" title="Ver Equipo"><%#((Entidades.Partido)Container.DataItem).visitante!=null?((Entidades.Partido)Container.DataItem).visitante.nombre:"-"%></a></h5>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- End Widget Partido -->
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                 <div id="sinPartidos" runat="server"  class="alert alert-info col-md-10 col-md-offset-1 mobile-margin-top"  style="display:none;">
                                                     No se registra información de partidos
                                                 </div> 
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                           
                            <!-- END Listado de Partidos -->
                        </div>
                    </div>
                    <!-- End Content Central -->
            </section>
            <!-- contentPages-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
