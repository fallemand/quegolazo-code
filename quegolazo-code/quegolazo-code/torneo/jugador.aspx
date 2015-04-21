<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="jugador.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!-- Charts.js-->
    <script type="text/javascript" src="/torneo/js/charts/Chart.min.js"></script>
    <script type="text/javascript" src="/torneo/js/charts/charts-jugador.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Román Riquelme</h1>
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
                    <li><a href="index-2.html">Equipos</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Boca Juniors</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Roman Riquelme</a></li>
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
                            <p class="slider-multiple-title">Otros Judadores del Equipo</p>
                            <ul class="otros-jugadores slider-multiple tooltip-hover">
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-1" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-2" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-3" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-4" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-5" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-6" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-7" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li><li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-8" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-9" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item">
                                    <a href="#verJugador" class="avatar-jugador avatar-slider avatar-bg-0" data-toggle="tooltip" title="Román Riquelme">
                                        <h1>RR</h1>
                                    </a>
                                </li>
                                <li class="li-item" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#verJugador">
                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-responsive img-circle center-block">
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros partidos de la fecha -->

                <!-- Datos del Equipo -->
                <div class="col-sm-4">
                    <div class="panel-box bg-dark score theme-border">
                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-principal img-responsive" alt="imagen"></img>
                        <!-- 
                        <a href="#verJugador" class="avatar-jugador avatar-principal avatar-bg-7" data-toggle="tooltip" title="Román Riquelme">
                            <h1>RR</h1>
                        </a>
                        -->
                        
                        <h3 class="text-center text-thin"><a href="#">Román Riquelme</a></h3>
                        <div class="row text-center">
                          <div class="col-xs-12">
                            <ul class="list-group tooltip-hover">
                                <li class="list-group-item" ><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""><a href=# data-toggle="tooltip" title="Ver Equipo"> Boca Juniors</a></li>
                              <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span> <span class="hidden-xs"></span> Nac: 22/14/1991</li>
                              <li class="list-group-item center-block">
                                  <a href="#" class="icon mail" data-toggle="tooltip" title="Copiar Mail"><i class="glyphicon glyphicon-envelope"></i></a>
                                  <a href="#" class="icon facebook" data-toggle="tooltip" title="Perfil de Facebook"><i class="fa fa-facebook"></i></a>
                              </li>
                            </ul>
                          </div>
                        </div>
                    </div>
                </div>
                <!-- END Datos del Equipo -->
                
                <!-- Jugadores del Equipo -->
                <div class="col-sm-8">
                    <div class="row">
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1>
                                        <span class="flaticon-football68" aria-hidden="true"></span>12
                                    </h1>
                                    <span>Partidos Jugados</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md text-success">
                                    <h1 class="text-success">
                                        <span class="flaticon-football28" aria-hidden="true"></span>15
                                    </h1>
                                    <span class="text-success">Goles Convertidos</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-danger">
                                        <span class="flaticon-football103" aria-hidden="true"></span>34
                                    </h1>
                                    <span class="text-danger">Tarjetas Rojas</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-yellow">
                                        <span class="flaticon-football103" aria-hidden="true"></span>34
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
                                                    <li><a href="#goleadores" data-toggle="tab"><span class="flaticon-football28"></span> Goles</a></li>
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
                                                                <td>22</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Goles Convertidos: </td>
                                                                <td>22</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Tarjetas Rojas: </td>
                                                                <td>22</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Tarjetas Amarillas: </td>
                                                                <td>22</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-sm-3">
                                                    <h5 class="col-title">Número Camiseta</h5>
                                                    <div class="camiseta-back center-block">
                                                        <span class="numero">10</span>
                                                        <span class="apellido">Riquelme</span>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <h5 class="col-title">Edad</h5>
                                                    <div class="widget widget-lg">
                                                      <h1>22</h1>
                                                      <span>Años</span>
                                                  </div>
                                                </div>
                                            </div>
                                        </div>      

                                        <!-- Tab Historial de Partidos -->
                                        <div class="tab-pane fade in" id="historial-partidos">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-center" colspan="2">VS</th>
                                                                <th>Fase</th>
                                                                <th>Fecha</th>
                                                                <th class="text-center" colspan="2">Resultado</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                                <td>Fase 1</td>
                                                                <td>Fecha 2</td>
                                                                <td>River Plate</td>
                                                                <td>5<small>(5)</small> - 5<small>(2)</small></td>
                                                                <td><span class="label label-success">Ganado</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                                <td>Fase 1</td>
                                                                <td>Fecha 2</td>
                                                                <td>River Plate</td>
                                                                <td>5<small>(5)</small> - 5<small>(2)</small></td>
                                                                <td><span class="label label-success">Ganado</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                                <td>Fase 1</td>
                                                                <td>Fecha 2</td>
                                                                <td>River Plate</td>
                                                                <td>5<small>(5)</small> - 5<small>(2)</small></td>
                                                                <td><span class="label label-warning">Empatado</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                                <td>Fase 1</td>
                                                                <td>Fecha 2</td>
                                                                <td>River Plate</td>
                                                                <td>5<small>(5)</small> - 5<small>(2)</small></td>
                                                                <td><span class="label label-danger">Perdido</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                                <td>Fase 1</td>
                                                                <td>Fecha 2</td>
                                                                <td>River Plate</td>
                                                                <td>5<small>(5)</small> - 5<small>(2)</small></td>
                                                                <td><span class="label label-warning">Empatado</span></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- END Tab Historial de Partidos  -->

                                        <!-- Tab de Goleadores -->
                                        <div class="tab-pane fade active in" id="goleadores">
                                            <div class="col-sm-7">
                                                <table class="table table-striped">
                                                      <thead>
                                                          <tr>
                                                              <th class="text-center" colspan="2">VS</th>
                                                              <th>Fecha</th>
                                                              <th>Tipo</th>
                                                          </tr>
                                                      </thead>
                                                      <tbody>
                                                          <tr>
                                                              <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                              <td>River Plate</td>
                                                              <td>Fecha 2</td>
                                                              <td>De Cabeza</td>
                                                          </tr>
                                                          <tr>
                                                              <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                              <td>River Plate</td>
                                                              <td>Fecha 2</td>
                                                              <td>De Cabeza</td>
                                                          </tr>
                                                          <tr>
                                                              <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                              <td>River Plate</td>
                                                              <td>Fecha 2</td>
                                                              <td>De Cabeza</td>
                                                          </tr>
                                                          <tr>
                                                              <td><img src="/torneo/img/img-theme/equipo.png" class="img-circle avatar-xs" alt=""></td>
                                                              <td>River Plate</td>
                                                              <td>Fecha 2</td>
                                                              <td>De Cabeza</td>
                                                          </tr>
                                                      </tbody>
                                                  </table>
                                            </div>
                                            <div class="col-sm-5">
                                                <h5 class="col-title">Tipos de Goles</h5>
                                                <canvas id="graficoGoles" class="canvas-md"></canvas>
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
