<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web13" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <asp:UpdatePanel ID="upListadoFases" runat="server">
                    <ContentTemplate>
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Fase <asp:Literal ID="litFase" runat="server"></asp:Literal><small>|</small> Fecha <asp:Literal ID="litFecha" runat="server"></asp:Literal> </h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="index-2.html"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="index-2.html"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Fase <asp:Literal ID="litLnkFase" runat="server"></asp:Literal></a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Fecha <asp:Literal ID="litLnkFecha" runat="server"></asp:Literal></a></li>
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
                            <ul class="fases slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptFases" runat="server" OnItemCommand="rptFases_ItemCommand1" OnItemDataBound="rptFases_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFase" runat="server" ClientIDMode="AutoID" CommandName="SeleccionarFase" CommandArgument='<%# Eval("idFase") %>'>
                                        <%--<a href="#cargar-esta-fase" >--%>
                                           <li class="li-item fase-<%# Eval("estado.nombre") %>" data-toggle="tooltip" title="Seleccionar Fase">
                                                <div class="widget widget-md">
                                                    <h1><%# Eval("idFase") %></h1>
                                                    <span><%# Eval("estado.nombre") %></span>
                                                </div>
                                           </li>
                                        <%--</a>--%>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Seleccionar la Fase -->

                <!-- Seleccionar la Fecha -->
                
                <div class="col-sm-9">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">
                            <p class="slider-multiple-title">Seleccione la Fecha</p>
                            <ul class="fechas slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptFechas" runat="server" OnItemCommand="rptFechas_ItemCommand">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFecha" CommandName="SeleccionarFecha" ClientIDMode="AutoID"  runat="server" CommandArgument='<%# Eval("idFecha") %>'>
                                       <%-- <a href="#cargar-esta-fecha">--%>
                                            <li class="li-item fecha-<%# Eval("estado.nombre") %>" data-toggle="tooltip" title="Seleccionar Fecha">
                                                <div class="widget widget-md">
                                                    <h1><%# Eval("idFecha") %></h1>
                                                    <span><%# Eval("estado.nombre") %></span>
                                                </div>
                                            </li>
                                        <%--</a>--%>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
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
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
                                            <div class="widget-partido">
                                                <div class="col-xs-4">
                                                     <asp:Panel ID="panelLogoEquipo" runat="server" Visible="<%# ((Entidades.Partido)Container.DataItem).local.tieneImagen() ? true : false %>">
                                                    <img src="<%# ((Entidades.Partido)Container.DataItem).local.obtenerImagenMediana( )%>" class="img-responsive center-block">
                                                         </asp:Panel>
                                                     <asp:Panel ID="panelCamisetaEquipo" runat="server" Visible="<%# ((Entidades.Partido)Container.DataItem).local.tieneImagen()? false : true %>">
                                                    <div id="Div1" class="camiseta-equipo" runat="server">
                                                      <div>
                                                        <i class="flaticon-football114" style="color:<%#  Eval("local.colorCamisetaPrimario") %>"></i>
                                                      </div><!--
                                                   --><div class="segunda-mitad">
                                                        <i class="flaticon-football114" style="color:<%# Eval("local.colorCamisetaSecundario") %>"></i>
                                                      </div>
                                                    </div>
                                                </asp:Panel>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("local.idEquipo").ToString())) %>" data-toggle="tooltip" title="Ver Equipo"><%#((Entidades.Partido)Container.DataItem).local.nombre%></a></h5>
                                                </div>
                                                <div class="col-xs-4 resultado">
                                                    <div class="thumbnail">
                                                        <h2><%# Eval("golesLocal")%><small><small><%# Eval("penalesLocal")%></small></small></h2>
                                                    </div>
                                                    <div class="thumbnail">
                                                        <h2><%# Eval("golesVisitante")%><small><small><%# Eval("penalesVisitante")%></small></small></h2>
                                                    </div>
                                                    <i class="flaticon-football85" data-toggle="tooltip" title="Árbitro: <%# Eval("arbitro.nombre")%>"></i>
                                                    <span class="glyphicon glyphicon-time" data-toggle="tooltip" title="<%# Eval("fecha")%>"></span>
                                                    <i class="flaticon-football96" data-toggle="tooltip" title="<%# Eval("cancha.nombre")%>"></i>
                                                    <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,idEdicion,Eval("idPartido").ToString()) %>" class="btn btn-primary btn-xs">+ Info</a>
                                                </div>
                                               <div class="col-xs-4">
                                                     <asp:Panel ID="panelLogoVisitante" runat="server" Visible="<%# ((Entidades.Partido)Container.DataItem).visitante.tieneImagen() ? true : false %>">
                                                    <img src="<%# ((Entidades.Partido)Container.DataItem).visitante.obtenerImagenMediana( )%>" class="img-responsive center-block">
                                                         </asp:Panel>
                                                     <asp:Panel ID="panelCamisetaVisitante" runat="server" Visible="<%# ((Entidades.Partido)Container.DataItem).visitante.tieneImagen()? false : true %>">
                                                    <div id="Div2" class="camiseta-equipo" runat="server">
                                                      <div>
                                                        <i class="flaticon-football114" style="color:<%#  Eval("visitante.colorCamisetaPrimario") %>"></i>
                                                      </div><!--
                                                   --><div class="segunda-mitad">
                                                        <i class="flaticon-football114" style="color:<%# Eval("visitante.colorCamisetaSecundario") %>"></i>
                                                      </div>
                                                    </div>
                                                </asp:Panel>
                                                    <h5><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("visitante.idEquipo").ToString())) %>" data-toggle="tooltip" title="Ver Equipo"><%#((Entidades.Partido)Container.DataItem).visitante.nombre%></a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                         </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                        </div>
                    </div>
                          </ItemTemplate>
                                    </asp:Repeater>
                  <%--  <h5 class="page-title">Grupo 2</h5>
                    <div id="grupo-2" class="panel score bg-dark theme-border panel-default">
                        <div class="panel-body">
                            <div class="row tooltip-hover">
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                                <!-- Widget Partido -->
                                <div class="col-md-4 col-sm-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body nopadding">
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
                                                        </div>
                                                        <!--
-->
                                                        <div class="segunda-mitad">
                                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                                        </div>
                                                    </div>
                                                    <h5><a href="#" data-toggle="tooltip" title="Ver Equipo">Boca Juniors</a></h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Widget Partido -->
                            </div>
                        </div>
                    </div>
                </div>--%>
                <!-- END Listado de Partidos -->
            </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
        </div>
        <!-- End Content Central -->
                        
    </section>
    <!-- contentPages-->
</asp:Content>
