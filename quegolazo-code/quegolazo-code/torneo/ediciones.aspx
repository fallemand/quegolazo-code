﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ediciones.aspx.cs" Inherits="quegolazo_code.torneo.ediciones" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- Basic -->
    <meta charset="utf-8">
    <title><%= torneo.nombre %> | Ediciones</title>
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
    <asp:Literal ID="litFavicon" runat="server" />  

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
<body class="fixed">
    <form id="form1" runat="server">
        <!-- layout-->
        <div id="layout-small">

            <!-- Section Area - Content Central -->
            <section class="content-info error">

                <!-- Crumbs --->
                <div class="crumbs">
                    <div class="">
                        <ul>
                            <li><a href="#"><%= torneo.nombre%></a></li>
                            <li>/</li>
                            <li><a href="#">Ediciones</a></li>
                        </ul>
                    </div>
                </div>
                <!-- END Crumbs --->

                <!-- Content Central -->
                <div class="">
                    <div class="row">
                        <div class="page-error">
                            <div class="col-xs-3 col-sm-3">
                                <img src="<%=torneo.obtenerImagenMediana()%>" alt="<%= torneo.nombre%>" class="img-responsive center-block"/>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <h1 class="titulo-torneo"><%= torneo.nombre%></h1>
                            </div>
                            <div class="col-xs-12">
                                <hr class="tall" />
                            </div>
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
                                    <h3>Seleccione una edición: <small><asp:Literal ID="litSinEdiciones" runat="server" Visible="false">No existen ediciones registradas</asp:Literal></small></h3>
                                </div>
                                <ul class="ediciones-slide noborder slider-multiple tooltip-hover">
                                    <asp:Repeater ID="rptEdiciones" runat="server">
                                        <ItemTemplate>
                                            <li class="li-item">
                                                <a href="<%# Logica.GestorUrl.urlEdicion(torneo.nick, ((Entidades.Edicion)Container.DataItem).idEdicion )%>" class="torneo-default full-width avatar-bg-<%#((Entidades.Edicion)Container.DataItem).lastNumber() %>">
                                                    <h1><%# ((Entidades.Edicion)Container.DataItem).nombre %></h1>
                                                    <p data-toggle="tooltip" data-placement="bottom" title="<%# ((Entidades.Edicion)Container.DataItem).estado.descripcion %>"><%# ((Entidades.Edicion)Container.DataItem).estado.nombre %></p>
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
