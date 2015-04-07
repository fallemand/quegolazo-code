<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="partido.aspx.cs" Inherits="quegolazo_code.torneo.partido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <!--Theme Options-->
    <script type='text/javascript' src="/torneo/js/theme-options/theme-options.js"></script>
    <link href="css/colorPicker.css" rel="stylesheet" />
    <script src="../resources/js/jquery.colorPicker.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Partido</h1>
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
                    <li>/</li>
                    <li><a href="index-2.html">Boca Juniors vs Boca Juniors</a></li>
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
                            <p class="proximos-partidos-title">Otros Partidos de la Fecha</p>
                            <ul class="proximos-partidos">
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado jugado theme-bg-color-2">
                                                2 (5) - 2 (3)
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-clock-o"></i>
                                                Programado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido" style="width: 120px">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-clock-o"></i>
                                                Programado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                                <li class="li-partido">
                                    <span class="fecha theme-bg-color">September 26, 2015</span>
                                    <div class="text">
                                        <div class="equipos">
                                            <a href="#">
                                                <span>BOC</span>
                                                vs
                                                    <span>ARG</span>
                                            </a>
                                            <p class="estado theme-bg-color-2">
                                                <i class="fa fa-ban"></i>
                                                Cancelado
                                            </p>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END Otros partidos de la fecha -->

                <!-- Tablero de Resultados -->
                <div class="col-sm-12">
                    <div class="panel-box score bg-dark theme-border">
                        <div class="row">
                            <div class="col-md-4 col-xs-3 nopadding-right padding-top">
                                <!--<div class="camiseta-equipo text-center">
                                        <span class="primera-mitad glyphicon glyphicon-triangle-left" style="color:#2966B8;"></span>
                                        <span class="glyphicon glyphicon-triangle-right" style="color:#DFE32D;"></span>
                                      </div>-->
                                <div class="row">
                                    <div class="camiseta-equipo">
                                        <div>
                                            <i class="flaticon-football114" style="color: #005A96"></i>
                                        </div><!--
                                     --><div class="segunda-mitad">
                                            <i class="flaticon-football114" style="color: #FAD201"></i>
                                        </div>
                                    </div>
                                </div>
                                <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h3>
                            </div>
                            <div class="col-xs-6 col-md-4">
                                <div class="row text-center resultado">
                                    <div class="col-xs-5 nopadding-left">
                                        <h1>9</h1>
                                    </div>
                                    <div class="col-xs-2 nopadding-right nopadding-left">
                                        <h1>-</h1>
                                    </div>
                                    <div class="col-xs-5 nopadding-right">
                                        <h1>9</h1>
                                    </div>
                                </div>
                                <div class="row text-center">
                                    <div class="col-xs-12">
                                        <ul class="list-group">
                                            <li class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span><span class="hidden-xs">Sabado </span>27/10/2015</li>
                                            <li class="list-group-item hidden-xs"><span class="glyphicon glyphicon-home" aria-hidden="true"></span>Complejo San Andres</li>
                                            <li class="list-group-item hidden-xs"><span class="label label-success">Partido Jugado</span></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3 col-md-4 padding-top">
                                <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block" style="max-height: 150px;">
                                <h3 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h3>
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
                                                    <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                    Boca Juniors
                                                </th>
                                                <th class="col-xs-8 col-md-4 text-center">VS</th>
                                                <th class="col-xs-2 col-md-4 text-center">
                                                    <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                    Boca Juniors
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody class="text-center">
                                            <tr>
                                                <td>
                                                    <span class="flaticon-football28"></span>
                                                    <span class="flaticon-football28"></span>
                                                    <span class="flaticon-football28"></span>
                                                </td>
                                                <td>Goles</td>
                                                <td>
                                                    <span class="flaticon-football28"></span>
                                                    <span class="flaticon-football28"></span>
                                                    <span class="flaticon-football28"></span>
                                                    <span class="flaticon-football28"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="color: red;" class="flaticon-football103"></span>
                                                    <span style="color: red;" class="flaticon-football103"></span>
                                                </td>
                                                <td class="text-center">Tarjetas Rojas</td>
                                                <td>
                                                    <span style="color: red;" class="flaticon-football103"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="color: yellow;" class="flaticon-football103"></span>
                                                    <span style="color: yellow;" class="flaticon-football103"></span>
                                                </td>
                                                <td class="text-center">Tarjetas Amarillas</td>
                                                <td>-                      
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                    <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                </td>
                                                <td class="text-center">Cambios</td>
                                                <td>
                                                    <img class="img-xs" src="/torneo/img/img-theme/cambio.png" alt="Cambio">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>-
                                                </td>
                                                <td class="text-center">Sanciones</td>
                                                <td>-
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                                <!-- Tab de Jugadores -->
                                <div class="tab-pane fade" id="titulares">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-9">Fernando Gago</td>
                                                    <td class="col-xs-2">5</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Goles -->
                                <div class="tab-pane fade" id="goles">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">18'</span></td>
                                                    <td class="col-xs-7">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-3">Tiro Libre</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-7">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-3">Tiro Libre</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">18'</span></td>
                                                    <td class="col-xs-7">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-3">Tiro Libre</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-7">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-3">Tiro Libre</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Cambios -->
                                <div class="tab-pane fade" id="cambios">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-1"><span class="text-success text-lg glyphicon glyphicon-arrow-up" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-1"><span class="text-danger text-lg glyphicon glyphicon-arrow-down" aria-hidden="true"></span></td>
                                                    <td class="col-xs-4">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Tarjetas -->
                                <div class="tab-pane fade" id="tarjetas">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-8">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-2">
                                                        <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-8">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-2">
                                                        <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-sm" alt=""></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped text-middle">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-8">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-2">
                                                        <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-sm" alt=""></td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-2"><span class="text-lg">34'</span></td>
                                                    <td class="col-xs-8">
                                                        <img src="/torneo/img/img-theme/jugador-mediano.jpg" class="img-circle avatar-sm" alt="">
                                                        Fernando Gago</td>
                                                    <td class="col-xs-2">
                                                        <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-sm" alt=""></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <!-- Tab de Sanciones -->
                                <div class="tab-pane fade" id="sanciones">
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-4">Fernando Gago</td>
                                                    <td class="col-xs-7">Sancionado por 5 Fechas</td>
                                                </tr>
                                                <tr>
                                                    <td class="col-xs-1">
                                                        <img src="/torneo/img/img-theme/jugador.jpg" class="img-responsive avatar-xs" alt="" style="height: 22px; max-width: 30px;"></td>
                                                    <td class="col-xs-4">Fernando Gago</td>
                                                    <td class="col-xs-7">Sancionado hasta el 15/12</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" colspan="3">
                                                        <img src="/torneo/img/img-theme/equipo1.png" style="max-height: 20px;">
                                                        Boca Juniors
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>No Registra Sanciones</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Resumen del Partido -->

                <!-- Widget Partidos Anteriores Equipo Local -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Ultimos Partidos: Boca Juniors</h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4">
                                            <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="nopadding-left col-xs-4 resultado nopadding-right">
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2>0</h2>
                                            </div>
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2>0</h2>
                                            </div>
                                            <div class="col-xs-12 text-center">
                                                Fecha 14
                                            </div>
                                        </div>

                                        <div class="col-xs-4 nopadding-right">
                                            <div class="camiseta-equipo">
                                                <div>
                                                    <i class="flaticon-football114" style="color: #005A96"></i>
                                                </div><!--
                                             --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                </div>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Partidos Anteriores Equipo Local -->

                <!-- Widget Versus -->
                <div class="col-md-4 col-sm-6" style="margin-bottom: 10px;">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Versus</h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <!-- Versus: Puntos -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>39</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <span class="flaticon-football31" aria-hidden="true"></span>
                                            <div class="col-xs-12 text-center">
                                                Puntos
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>21</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Puntos -->

                                <!-- Versus: Partidos Ganados -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>6</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/ganador.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Partidos Ganados
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>4</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Partidos Ganados -->

                                <!-- Versus: Tarjetas Rojas -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>8</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-roja.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Tarjetas Rojas
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>33</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Rojas -->

                                <!-- Versus: Tarjetas Amarillas -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>39</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <img src="/torneo/img/img-theme/tarjeta-amarilla.png" class="img-circle avatar-md" alt="">
                                            <div class="col-xs-12 text-center">
                                                Tarjetas Amarillas
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>21</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Tarjetas Amarillas -->

                                <!-- Versus: Goles -->
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>39</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="col-xs-4 text-center resultado">
                                            <span class="flaticon-flaming" aria-hidden="true" style="color: #E00C0C;"></span>
                                            <div class="col-xs-12 text-center">
                                                Goles a Favor
                                            </div>
                                        </div>
                                        <div class="col-xs-4 resultado">
                                            <div class="thumbnail text-center col-xs-12">
                                                <h2>21</h2>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
                                <!-- END Versus: Goles -->
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Widget Versus -->

                <!-- Widget Partidos Anteriores Equipo Visitante -->
                <div class="col-md-4 col-sm-6">
                    <div class="panel nopadding panel-default small-arrows">
                        <div class="panel-heading">
                            <h3 class="panel-title">Ultimos Partidos: Boca Juniors</h3>
                        </div>
                        <div class="panel-body">
                            <ul class="single-carousel">
                                <li>
                                    <div class="widget-partido">
                                        <div class="col-xs-4">
                                            <img src="/torneo/img/img-theme/equipo1.png" class="img-responsive center-block" style="max-height: 120px;">
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                        <div class="nopadding-left col-xs-4 resultado nopadding-right">
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2>0</h2>
                                            </div>
                                            <div class="thumbnail text-center col-xs-6">
                                                <h2>0</h2>
                                            </div>
                                            <div class="col-xs-12 text-center">
                                                Fecha 14
                                            </div>
                                        </div>

                                        <div class="col-xs-4 nopadding-right">
                                            <div class="camiseta-equipo">
                                                <div>
                                                    <i class="flaticon-football114" style="color: #005A96"></i>
                                                </div><!--
                                                --><div class="segunda-mitad">
                                                    <i class="flaticon-football114" style="color: #FAD201"></i>
                                                </div>
                                            </div>
                                            <h5 class="text-center"><a href="#" data-toggle="tooltip" data-placement="bottom" title="Ver Equipo">Boca Juniors</a></h5>
                                        </div>
                                    </div>
                                </li>
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
    <!-- Theme-options -->
    <div id="theme-options" style="z-index:9999">
            <div class="openclose"></div>
            <div class="title">
               <span>OPCIONES DEL TEMA</span>
               </div>
        <span>Tema</span>
            <ul id="themeSelector" class="text-center">      
                <li class="cyborg" title="Cyborg"><span>1</span></li>
                <li class="darkly" title="Darkly"><span>2</span></li>                    
                <li class="flatly"  title="Flatly"><span>3</span></li>
                <li class="sandstone" title="Sandstone"><span>4</span></li>
                <li class="slate" title="Slate"><span>5</span></li>
                <li class="hero" title="Super Heroe"><span>6</span></li>
                <li class="yeti"  title="Yeti"><span>7</span></li>      
                <li class="bootstrap"  title="Bootstrap"><span>8</span></li>            
            </ul> 
            <span>Color destacado</span>
            <ul id="colorchanger">      
                <li><a class="colorbox red" href="about0e99.html?theme=red" title="Red Skin"></a></li>
                <li><a class="colorbox blue" href="aboutca00.html?theme=blue" title="Blue Skin"></a></li>                    
                <li><a class="colorbox yellow" href="about2746.html?theme=yellow" title="Yellow Skin"></a></li>
                <li><a class="colorbox green" href="aboutaf91.html?theme=green" title="Green Skin"></a></li>
                <li><a class="colorbox orange" href="aboutceb0.html?theme=orange" title="Orange Skin"></a></li>
                <li><a class="colorbox purple" href="about938c.html?theme=purple" title="Purple Skin"></a></li>
                <li><a class="colorbox pink" href="abouta820.html?theme=pink" title="Pink Skin"></a></li>
                <li><a class="colorbox cocoa" href="about6788.html?theme=cocoa" title="Cocoa Skin"></a></li>
            </ul>           
            <span>Patron del header</span>
            <ul class="backgrounds-h">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg1"></li>
                    <li class="bg2"></li>
                    <li class="bg3"></li>
                    <li class="bg4 "></li>
                    <li class="bg5"></li> 
                    <li class="bg6"></li>
                    <li class="bg7"></li>                                  
                </ul>  
            <span>Colores:</span>
           <ul class="layout-style text-center">    
                <li>
                    Fondo
                    <div id="colorFondo" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>    
                <li >
                    Header
                    <div id="colorHeader" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>                                 
            </ul> 
            <span>ESTILO DE PAGINA</span>
            <ul class="layout-style">      
                <li class="wide">ANCHO</li>
                <li class="semiboxed active">SEMI CAJA</li> 
                <li class="boxed">CAJA</li> 
                <li class="boxed-margin">CAJA C/MARGEN</li>               
            </ul>               
            <div class="patterns">               
            <span>PATRON DEL FONDO</span>
            <ul class="backgrounds">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg1"></li>
                    <li class="bg2"></li>
                    <li class="bg3"></li>
                    <li class="bg4 "></li>
                    <li class="bg5"></li> 
                    <li class="bg6"></li>
                    <li class="bg7"></li>
                    <li class="bg8"></li>
                    <li class="bg9 "></li>
                    <li class="bg10"></li> 
                    <li class="bg11"></li> 
                    <li class="bg12"></li> 
                    <li class="bg13"></li> 
                    <li class="bg14"></li> 
                    <li class="bg15"></li> 
                    <li class="bg16"></li> 
                    <li class="bg17"></li> 
                    <li class="bg18"></li> 
                    <li class="bg19"></li> 
                    <li class="bg20"></li> 
                    <li class="bg21"></li> 
                    <li class="bg22"></li> 
                    <li class="bg23"></li> 
                    <li class="a1"></li>   
                    <li class="a2"></li> 
                    <li class="a3"></li>   
                    <li class="a4"></li>      
                    <li class="a5"></li>     
                    <li class="c1 fixed"></li>   
                    <li class="c2 fixed"></li> 
                    <li class="c3 fixed"></li>    
                </ul>  
            </div>
            <ul class="layout-style">    
                <li class="btn btn-danger" id="cerrarConfig">Cerrar</li>    
                <li onclick="guardarConfiguracion()" class="btn btn-primary">Guardar</li>                                 
            </ul> 
            <div class="col-xs-12">
            <span id="msjeAjax" class="text-center"></span>  
            </div>                       
        </div>
    <!-- End Theme-options -->      

</asp:Content>
