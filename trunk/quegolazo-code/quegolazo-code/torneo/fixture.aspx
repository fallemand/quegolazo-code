<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fixture.aspx.cs" Inherits="quegolazo_code.torneo.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <script src="/torneo/js/llaves/jquery-ui.min.js"></script>
    <script src="/torneo/js/llaves/jquery.bracket.min.js"></script>
    <link href="/torneo/css/jquery.bracket.min.css" rel="stylesheet" />
    <script src="/torneo/js/llaves/widgetLlaves.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">

    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1>      Fixture</h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo)%>"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion)%>"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlFixture(nickTorneo,idEdicion)%>">Fixture</a></li>
                </ul>
            </div>
        </div>
        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>
        <!-- Content Central -->
        <div class="container padding-top">
            <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound">
                <HeaderTemplate>
                    <div class="panel-group" id="fases">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="panel panel-default tooltip-hover">
                        <div class="panel-heading">
                            <div class="row" id="masterContainer">
                                <div class="col-md-7 col-xs-4">
                                    <a data-toggle="collapse" data-parent="#fases" href="#fase-<%# Eval("idFase") %>">
                                        <h4>
                                            <span class="glyphicon glyphicon-plus"></span>
                                            Fase <%# Eval("idFase") %>
                                        </h4>
                                    </a>
                                </div>
                                <div class="col-md-3 col-xs-5">
                                    <input type="text" id="filtro" class="form-control filtroFixture" placeholder="Filtrar por Equipo" />
                                </div>
                                <div class="col-md-2 col-xs-3" style="padding-top:10px;">
                                    <asp:Panel ID="panelEstadoFase" runat="server">
                                        <span class="label label-lg fase-<%# ((Entidades.Fase)Container.DataItem).estado.nombre %>" data-toggle="tooltip" title="<%# ((Entidades.Fase)Container.DataItem).estado.descripcion %>" data-placement="left"><%# ((Entidades.Fase)Container.DataItem).estado.nombre %></span>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="panelTCT" runat="server">
                            <div id='fase-<%# Eval("idFase") %>' class="panel-collapse collapse <%# (Container.ItemIndex==0) ? "in" : ""%>">
                                <div class="panel-body">

                                    <asp:Repeater ID="rptFechas" runat="server" OnItemDataBound="rptFechas_ItemDataBound">
                                        <HeaderTemplate>
                                            <div class="row" id="fechas">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="col-md-6">
                                                <div class="panel panel-default panel-fecha">
                                                    <div class="panel-heading">
                                                        <div class="row">
                                                            <div class="col-sm-9 col-xs-8">
                                                                <a href="<%# Logica.GestorUrl.urlFechas(nickTorneo,idEdicion,((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).idFase,((Entidades.Fecha)Container.DataItem).idFecha) %>">
                                                                    <h3 class="panel-title">
                                                                        <span class="glyphicon glyphicon-zoom-in"></span>
                                                                        <%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).tipoFixture.idTipoFixture=="ELIM" ?  Eval("nombre") + " " : "Fecha "+ Eval("idFecha")%>
                                                                    </h3>
                                                                </a>
                                                            </div>
                                                            <div class="col-sm-3 col-xs-4">
                                                                <div class="label label-md fecha-<%# ((Entidades.Fecha)Container.DataItem).estado.nombre%>" data-toggle="tooltip" title="<%# ((Entidades.Fecha)Container.DataItem).estado.descripcion %>">
                                                                    <%# ((Entidades.Fecha)Container.DataItem).estado.nombre  %>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel-body nopadding panel-maxheight">                                                    
                                                        <!-- Fecha -->
                                                            <table class="table table-condensed nomargin-bottom table-hover table-fecha">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="col-md-5 text-center" colspan="2">Local</th>
                                                                        <th class="col-md-2 text-center"><span class="glyphicon glyphicon-search"></span></th>
                                                                        <th class="col-md-5 text-center" colspan="2">Visitante</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody class="tablaFiltro">
                                                                    <asp:Repeater ID="rptPartidos" runat="server" OnItemDataBound="rptPartidos_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="panelPartidoNormal" runat="server" Visible="false">
                                                                                <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                    <td>
                                                                                        <%# ((Entidades.Partido)Container.DataItem).local!=null ?  ((Entidades.Partido)Container.DataItem).local.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") : "<img src='"+new Entidades.Equipo().obtenerImagenMediana()+"'/>"%>
                                                                                    </td>
                                                                                    <td class="text-left">
                                                                                        <asp:Label Font-Size="17px" ID="lblPrimerPuesto" class="flaticon-football81" runat="server" Visible="false" title="Final" rel="txtTooltip" data-placement="left"></asp:Label><asp:Label Font-Size="16px" ID="lblTercerPuesto" class="flaticon-football78" runat="server" Visible="false" title="Tercer Puesto" rel="txtTooltip" data-placement="left"></asp:Label>

                                                                                        <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre : "" %></td>
                                                                                    <td class="text-center partido-<%# ((Entidades.Partido)Container.DataItem).estado.nombre %>">
                                                                                        <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,gestorEdicion.edicion.idEdicion,Eval("idPartido").ToString()) %>">
                                                                                            <span>
                                                                                                <%# ((Entidades.Partido)Container.DataItem).golesLocal%>
                                                                                                <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesLocal.ToString()+")" : "" %>
                                                                                                - <%# ((Entidades.Partido)Container.DataItem).golesVisitante%>
                                                                                                <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesVisitante.ToString()+")" : "" %>
                                                                                            </span>
                                                                                        </a>
                                                                                    </td>
                                                                                    <td class="text-right"><%# ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.nombre : "" %></td>
                                                                                    <td>
                                                                                          <%# ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.obtenerImagen(Utils.GestorImagen.CHICA,"avatar-xs") : "<img src='"+new Entidades.Equipo().obtenerImagenMediana()+"'/>" %>
                                                                                    </td>
                                                                                </tr>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelPartidoLibre" runat="server" Visible="false">
                                                                                <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                    <td colspan="5">
                                                                                        <asp:Literal ID="litLibre"  runat="server" Text=""></asp:Literal></td>
                                                                                </tr>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Panel ID="panelSinPartidos" runat="server">
                                                                        <tr>
                                                                            <td colspan="12">No hay partidos registrados para la fecha</td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </tbody>
                                                            </table>
                                                        <!-- End Fecha -->
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="panelSinFechas" runat="server">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <span>No hay fechas registradas para la fase</span>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelLlaves" runat="server">
                            <div id="divLlaves<%# Eval("idFase") %>">
                            </div>
                        </asp:Panel>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Panel ID="panelSinFases" runat="server">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <span>No hay fases registradas para la edición</span>
                    </div>
                </div>
            </asp:Panel>
            <div class="row mobile-margin-top">
            </div>
        </div>
        <!-- End Content Central -->
    </section>
    <!-- END contentPages-->

    <script type="text/javascript">
        $('body').on('keyup', '#filtro', function () {
            if ($(this).val().length > 0) {
                $('.panel-collapse').collapse('show');
                $('.panel-title').attr('data-toggle', '');
            }
            else {
                $('.panel-collapse').collapse('hide');
                $('.panel-title').attr('data-toggle', 'collapse');
            }
            var rex = new RegExp($(this).val(), 'i');
            $('.tablaFiltro tr').hide();
            $('.tablaFiltro tr').filter(function () {
                return rex.test($(this).text());
            }).show();
        });
    </script>
</asp:Content>