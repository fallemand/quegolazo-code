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

              <!-- Otros jugadores -->
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
                                                        <h1><asp:Literal ID="litIniciales" runat="server" Text="">AB</asp:Literal></h1>
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
                <!-- END Otros jugadores -->

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
                                            <li class=""><a href="#fase1" data-toggle="tab">1</a></li>
                                            <li class=""><a href="#fase2" data-toggle="tab">2</a></li>
                                            <li class=""><a href="#fase3" data-toggle="tab">3</a></li>                                           
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
                                            <tr>
                                                <td>1</td>
                                                <td>Antonio Herrera</td>
                                                <td>Que Golazo</td>
                                                <td>20</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Gustavo Alzogaray</td>
                                                <td>Otra tesis</td>
                                                <td>18</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>El Apache Tevez</td>
                                                <td>Juventus</td>
                                                <td>18</td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Pipita Higuain</td>
                                                <td>Palermo</td>
                                                <td>14</td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Gonzalo Alberti</td>
                                                <td>Yupanqui</td>
                                                <td>10</td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Amadeo Sabattini</td>
                                                <td>Historiadores</td>
                                                <td>8</td>
                                            </tr>
                                              <tr>
                                                    <td>7</td>
                                                    <td>Paula Pedrosa</td>
                                                    <td>Las rompe huevos</td>
                                                    <td>6</td>
                                                </tr>

                                        </tbody>
                                    </table>
                                </div>
                                 <div class="tab-pane fade" id="fase1">
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
                                            <tr>
                                                <td>1</td>
                                                <td>Antonio Herrera</td>
                                                <td>Que Golazo</td>
                                                <td>5</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Gustavo Alzogaray</td>
                                                <td>Otra tesis</td>
                                                <td>8</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>El Apache Tevez</td>
                                                <td>Juventus</td>
                                                <td>3</td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Pipita Higuain</td>
                                                <td>Palermo</td>
                                                <td>5</td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Gonzalo Alberti</td>
                                                <td>Yupanqui</td>
                                                <td>1</td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Amadeo Sabattini</td>
                                                <td>Historiadores</td>
                                                <td>6</td>
                                            </tr>
                                            <tr>
                                                <td>7</td>
                                                <td>Paula Pedrosa</td>
                                                <td>Las rompe huevos</td>
                                                <td>1</td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                                 <div class="tab-pane fade" id="fase2">
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
                                            <tr>
                                                <td>1</td>
                                                <td>Antonio Herrera</td>
                                                <td>Que Golazo</td>
                                                <td>4</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Gustavo Alzogaray</td>
                                                <td>Otra tesis</td>
                                                <td>8</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>El Apache Tevez</td>
                                                <td>Juventus</td>
                                                <td>3</td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Pipita Higuain</td>
                                                <td>Palermo</td>
                                                <td>4</td>
                                            </tr>
                                            

                                        </tbody>
                                    </table>
                                </div>
                                 <div class="tab-pane fade" id="fase3">
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
                                            <tr>
                                                <td>1</td>
                                                <td>Antonio Herrera</td>
                                                <td>Que Golazo</td>
                                                <td>10</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Gustavo Alzogaray</td>
                                                <td>Otra tesis</td>
                                                <td>8</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>El Apache Tevez</td>
                                                <td>Juventus</td>
                                                <td>8</td>
                                            </tr>
                                        
                                            <tr>
                                                <td>4</td>
                                                <td>Gonzalo Alberti</td>
                                                <td>Yupanqui</td>
                                                <td>10</td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Amadeo Sabattini</td>
                                                <td>Historiadores</td>
                                                <td>2</td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Paula Pedrosa</td>
                                                <td>Las rompe huevos</td>
                                                <td>1</td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>

                               

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
                <!-- END Datos del Jugador -->

              
            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- END contentPages-->
</asp:Content>
