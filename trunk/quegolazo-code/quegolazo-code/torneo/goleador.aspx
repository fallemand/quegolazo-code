<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="goleador.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
        <!-- Charts.js-->
    <script type="text/javascript" src="/torneo/js/charts/Chart.min.js"></script>
    <script type="text/javascript" src="/torneo/js/charts/charts-goleador.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
        <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Goleadores - <%= gestorEdicion.edicion.nombre %></h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="/<%=nickTorneo%>"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>/goleadores">Goleadores</a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>
                
        <!-- Content Central -->
        <div class="container padding-top">

             
            <div class="row mobile-margin-top">

              <!-- Goleadores del torneo -->
                <div class="col-sm-12">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">
                            <p class="slider-multiple-title">Goleadores del Torneo</p>
                            <ul class="otros-jugadores slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptGoleadores" runat="server" OnItemDataBound="rptGoleadores_ItemDataBound" >
                                    <ItemTemplate>
                                        <li class="li-item" data-toggle="tooltip" title="<%# Eval("nombre")%>">
                                            <a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>/equipo-<%=idEquipo%>/jugador-<%# Eval("idJugador")%>">
                                                <asp:Panel ID="panelFotoJugador" runat="server" Visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen() ? true : false %>">
                                                    <img src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenMediana() %>" class="img-responsive img-circle center-block">
                                                </asp:Panel>
                                                <asp:Panel ID="panelJugadorSinFoto" runat="server" Visible="<%# ((Entidades.Jugador)Container.DataItem).tieneImagen() ? false : true %>">
                                                    <a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>/equipo-<%=idEquipo%>/jugador-<%# Eval("idJugador")%>" class="avatar-jugador avatar-slider avatar-bg-<%# Eval("idJugador").ToString().Substring(Eval("idJugador").ToString().Length -1 , 1) %>">
                                                        <h1><asp:Literal ID="litIniciales" runat="server" Text=""></asp:Literal></h1>
                                                    </a>
                                                </asp:Panel>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros Goleadores del torneo -->

               <div class="col-sm-6">
                    <div class="panel panel-default">
                         <div class="panel-heading">
                         <h3 class="panel-title text-center ">Fases de la Edicion: <%= gestorEdicion.edicion.nombre %> </h3>
                         </div>
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
                                        <span class="navbar-brand visible-xs" href="#">Goleadores</span>
                                    </div>
                                    <div class="collapse navbar-collapse" id="menu-fases">
                                        <ul class="nav navbar-nav  nav-justified">
                                            <li class="active"><a href="#todas" data-toggle="tab">Todas</a></li>
                                            <asp:Repeater ID="rptFasesEdicion" runat="server" Visible="false">
                                                <ItemTemplate>
                                                    <li class=""><a href="#fase<%# Eval("idFase") %>" data-toggle="tab"><%# Eval("idFase") %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>                                        
                                        </ul>
                                    </div>
                                </div>
                            </nav>


                            <!-- Tabs -->
                            <div class="tab-content highlight">

                                <!-- Tab Resumen -->
                                <div class="tab-pane fade active in" id="todas">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th class="col-xs-1 col-md-1 text-center">#</th>
                                                <th class="col-xs-8 col-md-4 text-center">Jugador</th>
                                                <th class="col-xs-2 col-md-4 text-center">Equipo</th>
                                                <th class="col-xs-2 col-md-4 text-center">Goles</th>
                                            </tr>
                                        </thead>
                                        <tbody class="text-center">
                                            <asp:Repeater ID="rptGoleadoresTodasLasFases" runat="server" OnItemDataBound="rptGoleadoresTodasLasFases_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><asp:Literal ID="litPosicionJugador" runat="server" Text=""></asp:Literal></td>
                                                        <td><%# Eval("JUGADOR") %></td>
                                                        <td><%# Eval("EQUIPO") %></td>
                                                        <td><%# Eval("GOLES") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinGoleadoresTodas" runat="server" visible="false">
                                                <td colspan="4">No hay información de goleadores registrada</td>
                                            </tr>                                            
                                        </tbody>
                                    </table>
                                </div>
                                <asp:Repeater ID="rptFasesIndividuales" runat="server" Visible="false" OnItemDataBound="rptFasesIndividuales_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="tab-pane fade" id="fase<%# Eval("idFase") %>">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="col-xs-1 col-md-1 text-center">#</th>
                                                        <th class="col-xs-8 col-md-4 text-center">Jugador</th>
                                                        <th class="col-xs-2 col-md-4 text-center">Equipo</th>
                                                        <th class="col-xs-2 col-md-4 text-center">Goles</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="text-center">
                                                    <asp:Repeater ID="rptFaseHija" runat="server" OnItemDataBound="rptFaseHija_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><asp:Literal ID="litPosicionJugador" runat="server" Text=""></asp:Literal></td>
                                                                <td><%# Eval("JUGADOR") %></td>
                                                                <td><%# Eval("EQUIPO") %></td>
                                                                <td><%# Eval("GOLES") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Panel ID="pnlSinGoleadoresFaseIndividual" runat="server" visible="false">
                                                        <tr id="sinGoleadoresFaseIndividual" runat="server" >
                                                            <td colspan="4">No hay información de goleadores registrada</td>
                                                        </tr>
                                                    </asp:Panel>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                       <%--</div>--%>
                                    </FooterTemplate>
                                </asp:Repeater>                                
                            </div>
                        </div>
                    </div>
               </div>

                <div class="col-sm-6">
                    <div class="panel panel-default">
                         <div class="panel-heading">
                         <h3 class="panel-title text-center ">Gráfico de goleadores para la fase: <span id="numFaseGrafico">Todas</span> </h3>
                         </div>
                        <div class="panel-body" id="containerCanvas">
                          <canvas id="graficosFases" class="canvas-lg"></canvas>   
                        </div>
                    </div>
               </div>                
            </div>
            <div class="row mobile-margin-top">        
            <div class="col-sm-6">
                    <div class="panel panel-default">
                         <div class="panel-heading">
                         <h3 class="panel-title text-center ">Equipos que convirtieron </h3>
                         </div>
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
                                        <span class="navbar-brand visible-xs" href="#">Goleadores</span>
                                    </div>
                                    <div class="collapse navbar-collapse">
                                        <ul class="nav navbar-nav  nav-justified">
                                            <li class="active"><a href="#tabla" data-toggle="tab">Tabla</a></li>
                                            <li class=""><a href="#graficoEquipos" data-toggle="tab">Gráfico</a></li>                                          
                                        </ul>
                                    </div>
                                </div>
                            </nav>
                                <!-- Tabs -->
                            <div class="tab-content highlight">

                                <!-- Tab Resumen -->
                                <div class="tab-pane fade active in" id="tabla">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th class="col-xs-1 col-md-1 text-center"></th>
                                                <th class="col-xs-1 col-md-1 text-center">Equipo</th>
                                                <th class="col-xs-8 col-md-4 text-center">Cantidad</th>
                                            </tr>
                                        </thead>
                                        <tbody class="text-center">
                                            <asp:Repeater ID="rptEquiposQueConvirtieron" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("Id equipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                                        <td><%# Eval("Equipo") %></td>
                                                        <td><%# Eval("Goles") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinEquipos" runat="server" >
                                               <td colspan="3">No hay información de equipos registrada</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                 <div class="tab-pane fade" id="graficoEquipos">
                                  <canvas id="canvasTiposGoles" class="canvas-md" ></canvas>
                                </div>  
                            </div>
                        </div>
                    </div>
               </div>
            <div class="col-sm-6">
                    <div class="panel panel-default">
                         <div class="panel-heading">
                         <h3 class="panel-title text-center ">Tipos de goles de la Edición </h3>
                         </div>
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
                                        <span class="navbar-brand visible-xs" href="#">Goleadores</span>
                                    </div>
                                    <div class="collapse navbar-collapse">
                                        <ul class="nav navbar-nav  nav-justified">
                                            <li class=""><a href="#tablaTipos" data-toggle="tab">Tabla</a></li>
                                            <li class="active"><a href="#graficoTipos" data-toggle="tab">Gráfico</a></li>                                          
                                        </ul>
                                    </div>
                                </div>
                            </nav>
                                <!-- Tabs -->
                            <div class="tab-content highlight">

                                <!-- Tab Resumen -->
                                <div class="tab-pane fade" id="tablaTipos">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th class="col-xs-1 col-md-1 text-center"></th>
                                                <th class="col-xs-1 col-md-1 text-center">Tipo de Gol</th>
                                                <th class="col-xs-8 col-md-4 text-center">Cantidad</th>
                                            </tr>
                                        </thead>
                                        <tbody class="text-center">                                            
                                            <asp:Repeater ID="rptGolesPorTipoGol" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><span class="flaticon-<%# Eval("tipo") %>"></span></td>
                                                        <td><%# Eval("Tipo Gol") %></td>
                                                        <td><%# Eval("Goles") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater> 
                                            <tr id="sinTiposDeGoles" runat="server" >
                                                <td colspan="3">No hay información de tipos de goles registrada</td>
                                            </tr>                                           
                                        </tbody>
                                    </table>
                                </div>
                                 <div class="tab-pane fade active in" id="graficoTipos">
                                  <canvas id="graficoTiposDeGol" class="canvas-md" ></canvas>
                                </div> 
                            </div>
                        </div>
                    </div>
               </div>
            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- END contentPages-->
</asp:Content>
