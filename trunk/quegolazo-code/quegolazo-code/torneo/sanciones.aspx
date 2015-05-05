<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="sanciones.aspx.cs" Inherits="quegolazo_code.torneo.sanciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Sanciones</h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href=""><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href=""><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="">Goleadores</a></li>
                </ul>
            </div>
        </div>
        <div class="container padding-top">

        <div class="row mobile-margin-top">

            <div class="col-md-6">
                 <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-12 col-xs-5">
                                Tarjetas
                            </div>
                        </div>
                    </div>
                    <div class="panel-body table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th class="col-md-1"></th>
                                                <th class="col-md-2">Equipo</th>
                                                <th class="col-md-2">Jugador</th>
                                                <th class="col-md-2"><span class="flaticon-football103" aria-hidden="true">Amarillas</span></th>
                                                <th class="col-md-2"><span class="flaticon-football103" aria-hidden="true">Rojas</span></th>
                                            </tr>
                                        </thead>
                                        <tbody class="tablaFiltro">
                                            <asp:Repeater ID="rptTarjetas" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td></td>
                                                        <td><%# Eval("EQUIPO") %></td>
                                                        <td><%# Eval("JUGADOR") %></td>
                                                        <td><%# Eval("AMARILLAS") %></td>
                                                        <td><%# Eval("ROJAS") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinTarjetas" runat="server" visible="false">
                                                <td colspan="7">No hay tarjetas registradas</td>
                                            </tr>
                                        </tbody>
                                    </table>
                    </div>
                </div>

            </div>
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8 col-xs-5">
                                <span class="glyphicon glyphicon-search"></span>
                                Sanciones<span class="hidden-xs"> Existentes</span>
                            </div>
                            <div class="col-md-4 col-xs-7">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Sanciones" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th class="col-md-1">Fecha</th>
                                                <th class="col-md-2">Equipo</th>
                                                <th class="col-md-2">Jugador</th>
                                                <th class="col-md-2">Motivo</th>
                                                <th class="col-md-2"><span class="hidden-xs">Puntos A Quitar</span><abbr class="visible-xs" title="Puntos A Quitar">PAQ</abbr></th>
                                                <th class="col-md-1"><span class="hidden-xs">Fechas Suspendidas</span><abbr class="visible-xs" title="Fechas Suspendidas">FS</abbr></th>
                                                <th class="col-md-1">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tablaFiltro">
                                            <asp:Repeater ID="rptSanciones" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("Fecha") %></td>
                                                        <td><%# Eval("NombreEquipo") %></td>
                                                        <td><%# Eval("NombreJugador") %></td>
                                                        <td><%# Eval("MotivoSancion") %></td>
                                                        <td><%# Eval("PtosAQuitar") %></td>
                                                        <td><%# Eval("CantFechas") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinSanciones" runat="server" visible="false">
                                                <td colspan="7">No hay sanciones registradas</td>
                                            </tr>
                                        </tbody>
                                    </table>
                    </div>
                </div>
            </div>
        </div>
            </div>
     </section>

</asp:Content>
