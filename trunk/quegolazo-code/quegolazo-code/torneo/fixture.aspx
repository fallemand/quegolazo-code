﻿<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fixture.aspx.cs" Inherits="quegolazo_code.torneo.fixture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
             <div class="row">
                        <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound" OnItemCommand="rptFases_ItemCommand">
                            <HeaderTemplate>
                                <div class="panel-group" id="fases">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading panel-heading-master">
                                        <div class="row clearfix" id="masterContainer">
                                            <div class="col-md-5 col-xs-4">
                                                <a data-toggle="collapse" data-parent="#fases" href="#fase-<%# Eval("idFase") %>" class="text-muted" style="font-size: 15px;">
                                                    <span class="glyphicon glyphicon-plus"></span>
                                                    Fase <%# Eval("idFase") %>
                                                </a>
                                            </div>
                                            <div class="col-md-4 nopadding-left mobile-nopadding-right col-xs-4">
                                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Fechas" />
                                            </div>
                                            <div class="col-md-3 col-xs-4">
                                                <asp:Panel ID="panelEstadoFase" Visible="false" runat="server">
                                                    <span class="label label-big fase-<%# ((Entidades.Fase)Container.DataItem).estado.nombre %>" rel="txtTooltip" title="<%# ((Entidades.Fase)Container.DataItem).estado.descripcion %>" data-placement="left"><%# ((Entidades.Fase)Container.DataItem).estado.nombre %></span>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div id='fase-<%# Eval("idFase") %>' class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:Repeater ID="rptFechas" runat="server" OnItemDataBound="rptFechas_ItemDataBound">
                                                <HeaderTemplate>
                                                    <div class="panel-group" id="fechas">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#fechas" href="#fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>" style="font-size: 15px;"><%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).tipoFixture.idTipoFixture=="ELIM" ?  Eval("nombre") + " " : "Fecha "+ Eval("idFecha")%>  <small>Ver Más Detalles</small></a>
                                                                <small><span class="label pull-right fecha-<%# ((Entidades.Fecha)Container.DataItem).estado.nombre%>" rel="txtTooltip" title="<%# ((Entidades.Fecha)Container.DataItem).estado.descripcion %>" data-placement="left"><%# ((Entidades.Fecha)Container.DataItem).estado.nombre  %></span></small>
                                                            </h4>
                                                        </div>
                                                        <div id='fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>' class="panel-collapse collapse">
                                                            <div class="panel-body small-padding">
                                                                <table id="tabla-fechas" class="table nomargin-bottom">
                                                                    <thead style="display: none;">
                                                                        <tr>
                                                                            <th class="col-md-4">Equipo Local</th>
                                                                            <th class="col-md-2">Resultado</th>
                                                                            <th class="col-md-4">Equipo Visitante</th>
                                                                            <th class="col-md-2"></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="tablaFiltro">
                                                                        <asp:Repeater ID="rptPartidos" runat="server" OnItemCommand="rptPartidos_ItemCommand" OnItemDataBound="rptPartidos_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <asp:Panel ID="panelPartidoNormal" runat="server" Visible="false">
                                                                                    <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                        <td>
                                                                                            <asp:Label Font-Size="17px" ID="lblPrimerPuesto" class="flaticon-football81" runat="server" Visible="false" title="Final" rel="txtTooltip" data-placement="left"></asp:Label><asp:Label Font-Size="16px" ID="lblTercerPuesto" class="flaticon-football78" runat="server" Visible="false" title="Tercer Puesto" rel="txtTooltip" data-placement="left"></asp:Label>
                                                                                            <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre : "" %></td>
                                                                                        <td class="col-xs-4"><%# ((Entidades.Partido)Container.DataItem).golesLocal %>
                                                                                            <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesLocal.ToString()+")" : "" %>
                                                                                            - <%# ((Entidades.Partido)Container.DataItem).golesVisitante%>
                                                                                            <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesVisitante.ToString()+")" : "" %>
                                                                                        </td>
                                                                                        <td><%# ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.nombre : "" %></td>
                                                                                        <td>
                                                                                            <asp:LinkButton title="Administrar Partido" ClientIDMode="AutoID" rel="txtTooltip" ID="lnkAdministrarPartido" runat="server" CommandName="administrarPartido" CommandArgument='<%# Eval("idPartido") + ";fase" + ((Entidades.Fase)((((RepeaterItem)Container.Parent.Parent.Parent.Parent)).DataItem)).idFase +"-fecha"+((Entidades.Fecha)((RepeaterItem)Container.Parent.Parent).DataItem).idFecha %>'><span class="glyphicon glyphicon-cog"></span></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="panelPartidoLibre" runat="server" Visible="false">
                                                                                     <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                        <td colspan="4">
                                                                                            <asp:Literal ID="litLibre" runat="server" Text=""></asp:Literal></td>
                                                                                        <%--<td colspan="4">Libre: <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre :( ((Entidades.Partido)Container.DataItem).visitante.nombre!=null) ? ((Entidades.Partido)Container.DataItem).visitante.nombre : ""  %> </td>--%>
                                                                                    </tr>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="panelPartidoEliminatorioIncompleto" runat="server" Visible="false">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Literal ID="litEquipo1" runat="server" Text=""></asp:Literal></td>
                                                                                        <%--<td><%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre :( ((Entidades.Partido)Container.DataItem).visitante.nombre!=null) ? ((Entidades.Partido)Container.DataItem).visitante.nombre : "" %> </td>--%>
                                                                                        <td>-</td>
                                                                                        <td>Equipo 2</td>
                                                                                        <td>
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
            </div>
</asp:Content>
