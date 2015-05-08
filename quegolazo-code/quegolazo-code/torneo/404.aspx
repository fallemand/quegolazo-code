<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="quegolazo_code.torneo.ediciones2" %>

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
    <link rel="shortcut icon" href="/torneo/img/icons/favicon.ico">

    <!-- Head Libs -->
    <script src="/torneo/js/modernizr.js"></script>
    <!-- jQuery local-->
    <script src="/torneo/js/jquery.js"></script>
    <!-- Boostrap-->
    <script src="/torneo/js/bootstrap/bootstrap.js"></script>
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
<body class="fixed" style="background-image: url(/torneo/img/bg-theme/c11.png); margin-top:70px;">
    <form id="form1" runat="server">
        <!-- layout-->
        <div id="layout">

            <!-- Section Area - Content Central -->
            <section class="content-info home">

                <!-- Crumbs --->
                <div class="crumbs">
                    <div class="container">
                        <ul>
                            <li><a href="#">QueGolazo</a></li>
                            <li>/</li>
                            <li><a href="#">404</a></li>
                        </ul>
                    </div>
                </div>
                <!-- END Crumbs --->

                <!-- Content Central -->
                <div class="container">
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
                    <div class="container panel-body">
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

                <div class="semiboxshadow text-center">
                    <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="" />
                </div>

            </section>
            <!-- End Section Area - Content Central -->
        </div>
        <!-- End layout-->

        <script>
            $(document).ready(function ($) {
                $(".torneos-slide").owlCarousel({
                    autoPlay: 3200,
                    items: 8,
                    navigation: false,
                    itemsDesktop: [1199, 5],
                    itemsDesktopSmall: [1024, 4],
                    itemsTablet: [768, 3],
                    itemsMobile: [500, 2],
                    pagination: true,
                    rewindNav: false,
                });
                //=============================  tooltip demo ===========================================//
                $('.tooltip-hover').tooltip({
                    selector: "[data-toggle=tooltip]",
                    container: "body"
                });
            });
        </script>
    </form>
</body>
</html>
