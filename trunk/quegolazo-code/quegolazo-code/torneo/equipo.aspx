<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="equipo.aspx.cs" Inherits="quegolazo_code.torneo.equipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!-- Charts.js-->
    <script type="text/javascript" src="/torneo/js/charts/Chart.min.js"></script>
    <script type="text/javascript" src="/torneo/js/charts/charts-equipo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Boca Juniors</h1>
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
                            <p class="slider-multiple-title">Otros Equipos de la Edición</p>
                            <ul class="otros-equipos slider-multiple tooltip-hover">
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
                                    </a>
                                </li>
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
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
                                <li class="li-equipo" data-toggle="tooltip" title="Boca Juniors">
                                    <a href="#ver equipo">
                                        <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block">
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
                        <div class="camiseta-equipo">
                              <div>
                                <i class="flaticon-football114" style="color:#005A96"></i>
                              </div><!--
                           --><div class="segunda-mitad">
                                <i class="flaticon-football114" style="color:#FAD201"></i>
                              </div>
                        </div>
                            <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h3>
                            <div class="row text-center">
                              <div class="col-xs-12">
                                <ul class="list-group">
                                  <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span> <span class="hidden-xs">Sabado </span>27/10/2015</li>
                                  <li class="list-group-item"><span class="flaticon-football95" aria-hidden="true"></span> DT: Marcelo Bielsa</li>
                                  <li class="list-group-item"><span class="flaticon-football119" aria-hidden="true"></span> Delegado: Facundo Allemand</li>
                                  <li class="list-group-item"><span class="label label-success">Partido Jugado</span></li>
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
                                        <span class="flaticon-football31" aria-hidden="true"></span>58
                                    </h1>
                                    <span>Puntos</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md text-success">
                                    <h1 class="text-success">
                                        <span class="flaticon-football28" aria-hidden="true"></span>15
                                    </h1>
                                    <span class="text-success">Goles a Favor</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div class="panel nopadding panel-default">
                                <div class="panel-body widget widget-md">
                                    <h1 class="text-danger">
                                        <span class="flaticon-football28" aria-hidden="true"></span>8
                                    </h1>
                                    <span class="text-danger">Goles en Contra</span>
                                </div>
                            </div>
                        </div>
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
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                  
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='jugador-2' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                        <!-- Popover del Jugador -->
                                        <div id="popover-jugador-2" style="display:none">
                                            <div class="row">
                                                <div class="col-md-7">
                                                    <div class="row">
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span class="flaticon-football68"></span>28</h1>
                                                        <span>Partidos</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span class="flaticon-football28"></span>28</h1>
                                                        <span>Goles</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span style="color: #c41a1a;" class="flaticon-football103"></span>9</h1>
                                                        <span>Rojas</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span style="color: #e5e520;" class="flaticon-football103"></span>10</h1>
                                                        <span>Amarillas</span>
                                                      </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5 text-center">
                                                    <div class="camiseta-back">
                                                        <span class="numero">18</span>
                                                        <span class="apellido">Allemand</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="popover-title-jugador-2" style="display:none">
                                            Facundo Allemand
                                               <a href="#" class="icon pull-right facebook" ><i data-toggle="tooltip" title="Perfil de Facebook" class="fa fa-facebook"></i></a>
                                               <a href="#" class="icon pull-right mail" ><i  data-toggle="tooltip" title="Copiar Mail" class="glyphicon glyphicon-envelope"></i></a>
                                        </div>
                                        <!-- END Popover del Jugador -->
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='jugador-3' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                        <!-- Popover del Jugador -->
                                        <div id="popover-jugador-3" style="display:none">
                                            <div class="row">
                                                <div class="col-md-7">
                                                    <div class="row">
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span class="flaticon-football68"></span>28</h1>
                                                        <span>Partidos</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span class="flaticon-football28"></span>28</h1>
                                                        <span>Goles</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span style="color: #c41a1a;" class="flaticon-football103"></span>9</h1>
                                                        <span>Rojas</span>
                                                      </div>
                                                      <div class="col-xs-6 widget widget-xs">
                                                        <h1><span style="color: #e5e520;" class="flaticon-football103"></span>10</h1>
                                                        <span>Amarillas</span>
                                                      </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5 text-center">
                                                    <div class="camiseta-back">
                                                        <span class="numero">14</span>
                                                        <span class="apellido">Allemand</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="popover-title-jugador-3" style="display:none">
                                            Facundo Allemand
                                               <a href="#" class="icon pull-right facebook" ><i data-toggle="tooltip" title="Perfil de Facebook" class="fa fa-facebook"></i></a>
                                               <a href="#" class="icon pull-right mail" ><i  data-toggle="tooltip" title="Copiar Mail" class="glyphicon glyphicon-envelope"></i></a>
                                        </div>
                                        <!-- END Popover del Jugador -->
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='jugador-1' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A1' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A2' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A3' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A4' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A5' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A6' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A7' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A8' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                    <!-- Jugador -->
                                    <div class="col-md-2 col-xs-3">
                                        <a id='A9' class="popover-jugador" href="#" >
                                          <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle img-responsive" alt="imagen"></img>
                                        </a>
                                    </div>
                                    <!-- END Jugador -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Jugadores del Equipo -->

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
                                            <li class="active"><a href="#resumen-equipo" data-toggle="tab"><span class="flaticon-football128"></span>Resumen</a></li>
                                            <li><a href="#historial-partidos" data-toggle="tab"><span class="flaticon-football118"></span>Historial de Partidos</a></li>
                                            <li><a href="#goleadores" data-toggle="tab"><span class="flaticon-football28"></span> Goleadores</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </nav>

                            <!-- Tabs -->
                            <div class="tab-content highlight">
                                
                                <!-- Tab Resumen -->
                                <div class="tab-pane fade in active" id="resumen-equipo">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Partidos</th>
                                                        <th class="text-center no-strong"><small>Ganados</small></th>
                                                        <th class="text-center no-strong"><small>Empatados</small></th>
                                                        <th class="text-center no-strong"><small>Perdidos</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>22</td>
                                                        <td class="success">17</td>
                                                        <td class="warning">4</td>
                                                        <td class="danger">3</td>
                                                    </tr>
                                                    </tbody>
                                            </table>
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Goles</th>
                                                        <th class="text-center no-strong"><small>Convertidos</small></th>
                                                        <th class="text-center no-strong"><small>En Contra</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>22</td>
                                                        <td class="success">20</td>
                                                        <td class="danger">3</td>
                                                    </tr>
                                                    </tbody>
                                            </table>
                                            <table class="table text-center table-striped nomargin-bottom">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center col-xs-3">Tarjetas</th>
                                                        <th class="text-center no-strong"><small>Amarillas</small></th>
                                                        <th class="text-center no-strong"><small>Rojas</small></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>18</td>
                                                        <td class="warning">13</td>
                                                        <td class="danger">6</td>
                                                    </tr>
                                                    </tbody>
                                            </table>               
                                        </div>
                                        <div class="col-sm-4">
                                            <h5 class="col-title">Gráfico de Goles</h5>
                                            <canvas id="graficoGoles" class="canvas-md"></canvas>
                                        </div>
                                        <div class="col-sm-4">
                                            <h5 class="col-title">Gráficos de Partidos</h5>
                                            <canvas id="graficoPartidos" class="canvas-md"></canvas>
                                        </div>
                                    </div>
                                    
                                </div>

                                <!-- Tab Historial de Partidos -->
                                <div class="tab-pane fade" id="historial-partidos">
                                    <div class="col-sm-5 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="2">VS</th>
                                                    <th class="text-center" colspan="2">Resultado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-success">Ganado</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-success">Ganado</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-warning">Empatado</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-danger">Perdido</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-danger">Perdido</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-success">Ganado</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-warning">Empatado</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2 col-md-1"><img src="/torneo/img/img-theme/equipo1.png" class="img-circle avatar-xs" alt=""></td>
                                                    <td class="col-xs-6 col-md-5">River Plate</td>
                                                    <td class="col-xs-6 col-md-3">5<small>(5)</small> - 5<small>(2)</small></td>
                                                    <td class="col-xs-3 col-md-2"><span class="label label-danger">Perdido</span></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-7 col-xs-12">
                                        <h5 class="col-title">Evolución de Puntos</h5>
                                        <canvas id="graficoPuntos" class="canvas-lg"></canvas>
                                    </div>
                                </div>
                                <!-- END Tab Historial de Partidos  -->

                                <!-- Tab de Goleadores -->
                                <div class="tab-pane fade" id="goleadores">
                                    <div class="col-sm-5 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="2">Jugador</th>
                                                    <th class="text-center" colspan="2">Goles</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">8</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">4</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">3</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">3</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">1</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="">
                                                    </td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">1</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-7 col-xs-12">
                                        <h5 class="col-title">Goleadores</h5>
                                        <canvas id="graficoGoleadores" class="canvas-lg"></canvas>
                                    </div>
                                </div>
                                <!-- END Tab de Goleadores -->
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Resumen del Partido -->
            </div>
        </div>
        <!-- End Content Central -->
      </section>
      <!-- contentPages-->
</asp:Content>
