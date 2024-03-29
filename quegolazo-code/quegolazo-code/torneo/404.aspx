﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="quegolazo_code.torneo.ediciones2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Basic -->
    <meta charset="utf-8">
    <title>Error 404 - Torneo no encontrado - QueGolazo!</title>
    <meta name="keywords" content="futbol, torneo, partido, campeonato, goles, gol, goleadores" />
    <meta name="description" content="Sitio web del torneo en donde encontrarás toda la información necesaria">
    <meta name="author" content="quegolazo.org">

    <!-- Mobile Metas -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Theme CSS -->
    <link id="theme" href="/torneo/css/bootstrap/bootstrap.css" rel="stylesheet" media="screen">
    <link href="/torneo/css/style.css" rel="stylesheet" media="screen">
    <link href="/torneo/css/quegolazo.css" rel="stylesheet">

    <!-- Responsive CSS -->
    <link href="/torneo/css/theme-responsive.css" rel="stylesheet" media="screen">
    <!-- Skins Theme -->
    <link href="/torneo/css/skins/green.css" rel="stylesheet" media="screen" class="skin">

    <!-- Favicons -->
    <link rel="shortcut icon" href="/torneo/img/img-theme/favicon.ico">

    <!-- jQuery local-->
    <script src="/torneo/js/jquery.min.js"></script>
    <!-- Boostrap-->
    <script src="/torneo/js/bootstrap/bootstrap.js"></script>
    <!-- Ediciones-->
    <script src="/torneo/js/ediciones.js"></script>
    <!-- carousel.js-->
    <script type='text/javascript' src="/torneo/js/carousel/carousel.js"></script>
    <!--[if IE]>
            <link rel="stylesheet" href="css/ie/ie.css">
        <![endif]-->
    <!--[if lte IE 8]>
            <script src="js/responsive/html5shiv.js"></script>
            <script src="js/responsive/respond.js"></script>
        <![endif]-->
</head>
<body class="fixed" style="background-image: url(/torneo/img/bg-theme/c11.png); ">
    <form id="form1" runat="server">
        <!-- layout-->
        <div id="layout-small">

            <!-- Section Area - Content Central -->
            <section class="content-info">

                <!-- Crumbs --->
                <div class="crumbs">
                    <div class=>
                        <ul>
                            <li><a href="#">QueGolazo</a></li>
                            <li>/</li>
                            <li><a href="#">404</a></li>
                        </ul>
                    </div>
                </div>
                <!-- END Crumbs --->

                <!-- Content Central -->
                <div class=>
                    <div class="row">
                        <div class="page-error">
                            <h1>404 <img src="/torneo/img/img-theme/logo-big.png" alt="Logo" /></h1>
                            <hr class="tall">
                            <p class="lead">Parece que no has seleccionado ningún torneo o la ruta es incorrecta.</p>
                            <!-- <a href="index-2.html" class="btn btn-lg btn-primary">Volver al Inicio</a> -->
                        </div>
                    </div>
                </div>
                <!-- End Content Central -->

                <!-- Sponsors -->
                <div class="section-wide panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <h2><span class="text-resalt"><asp:Literal ID="litCantTorneos" runat="server"></asp:Literal></span> torneos utilizan QueGolazo!</h2>
                                </div>
                                <ul class="torneos-slide tooltip-hover">
                                    <asp:Repeater ID="rptTorneos" runat="server">
                                        <ItemTemplate>
                                            <li class="li-item" data-toggle="tooltip" title="<%# ((Entidades.Torneo)Container.DataItem).nombre %>">
                                                <a href="/<%# ((Entidades.Torneo)Container.DataItem).nick %>" class="<%#(((Entidades.Torneo)Container.DataItem).tieneImagen()==false) ? "torneo-default avatar-bg-" + ((Entidades.Torneo)Container.DataItem).lastNumber() : "" %>">
                                                    <h1 runat="server" visible="<%# ((Entidades.Torneo)Container.DataItem).tieneImagen()==false%>"><%# ((Entidades.Torneo)Container.DataItem).iniciales() %></h1>
                                                    <p runat="server" visible="<%# ((Entidades.Torneo)Container.DataItem).tieneImagen()==false%>"><%# ((Entidades.Torneo)Container.DataItem).nombre %></p>
                                                    <img runat="server" visible="<%# ((Entidades.Torneo)Container.DataItem).tieneImagen()%>" src="<%# ((Entidades.Torneo)Container.DataItem).obtenerImagenMediana() %>" class="img-responsive center-block" />
                                                </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Sponsors -->
            </section>
            <!-- End Section Area - Content Central -->
        </div>
        <!-- End layout-->
    </form>
</body>
</html>
