<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- contentPages-->
    
        <!-- Titulo Sección -->
        <section class="section-title img-about">
            <div class="overlay-bg"></div>
            <div class="container">
                <h1>Fase 1 <small>|</small> Fecha 2 <small>|</small> Grupo B</h1>
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
                    <li><a href="index-2.html">Fase 1</a></li>
                    <li>/</li>
                    <li><a href="index-2.html">Fecha 2</a></li>
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
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fase-Finalizada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>1</h1>
                                            <span>Finalizada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fase-Iniciada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>2</h1>
                                            <span>Iniciada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fase-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>3</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fase-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>4</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fase-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>5</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
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
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Completa" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>1</h1>
                                            <span>Completa</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Completa" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>2</h1>
                                            <span>Completa</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Completa" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>3</h1>
                                            <span>Completa</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Completa" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>4</h1>
                                            <span>Completa</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Incompleta" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>5</h1>
                                            <span>Incompleta</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>6</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>7</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>8</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>9</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>10</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>11</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                                <a href="#cargar-esta-fecha">
                                    <li class="li-item fecha-Diagramada" data-toggle="tooltip" title="Seleccionar Fecha">
                                        <div class="widget widget-md">
                                            <h1>12</h1>
                                            <span>Diagramada</span>
                                        </div>
                                    </li>
                                </a>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Seleccionar la Fecha -->

                <!-- Listado de Partidos -->
                <div class="col-sm-12">
                    <h5 class="page-title">Grupo 1</h5>
                    <div id="grupo-1" class="panel score bg-dark theme-border panel-default">
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
                                                        </div><!--
                                                     --><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                    <h5 class="page-title">Grupo 2</h5>
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                                                        </div><!--
--><div class="segunda-mitad">
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
                </div>
                <!-- END Listado de Partidos -->
            </div>
        </div>
        <!-- End Content Central -->
      </section>
      <!-- contentPages-->
</asp:Content>
