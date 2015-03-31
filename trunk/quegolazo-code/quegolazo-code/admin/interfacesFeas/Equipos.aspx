<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="quegolazo_code.admin.interfacesFeas.Equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row clearfix">
                        Equipos
                    </div>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-1"></th>
                                <th class="col-md-9">Nombre</th>
                                <th class="col-md-2"></th>
                            </tr>
                        </thead>
                        <tbody class="tablaFiltro">
                            <asp:Repeater ID="rptEquipos" runat="server" OnItemCommand="rptEquipos_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                        <td><strong><%# Eval("nombre") %></strong></td>
                                        <td>
                                            <asp:LinkButton ClientIDMode="AutoID" ID="lnkElegirEquipo" title="Elegir Equipo" runat="server" CommandName="elegirEquipo" CommandArgument='<%#Eval("idEquipo")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr id="sinequipos" runat="server" visible="false">
                                <td colspan="3">No hay equipos registrados</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblNombreEquipo" runat="server" Text=""></asp:Label>
                    <div class="thumbnail fileinput-preview">
                        <img id="imagenpreview" runat="server" />
                    </div>
                </div>
                <div class="panel-body nopadding-bottom">
                    <fieldset class="vgEquipo">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <br />
                                Director Tecnico:
                                <asp:Label ID="lblDirectorTecnico" runat="server" Text=""></asp:Label>
                                <br />
                                Delegado 1:
                                <asp:Label ID="lblDelegado1" runat="server" Text=""></asp:Label>
                                <br />
                                Delegado 2:
                                <asp:Label ID="lblDelegado2" runat="server" Text=""></asp:Label>
                                <br />
                                Puntos:
                                <asp:Label ID="lblPuntos" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row clearfix">
                        Jugadores<span class="hidden-xs"> del Equipo</span>
                    </div>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-1"></th>
                                <th class="col-md-2">Nombre</th>
                                <th class="col-md-1 hidden-xs">#</th>
                                <th class="col-md-2 hidden-xs">Fecha Nac</th>
                            </tr>
                        </thead>
                        <tbody class="tablaFiltro">
                            <asp:Repeater ID="rptJugadores" runat="server" OnItemCommand="rptJugadores_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <img src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                        <td><strong><%# Eval("nombre") %></strong></td>
                                        <td class="hidden-xs"><%# Eval("numeroCamiseta") %></td>
                                        <td class="hidden-xs"><%# Eval("fechaNacimiento") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr id="sinJugadores" runat="server" visible="false">
                                <td colspan="6">No hay Jugadores registrados para este equipo</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="row">
            <br />
            <asp:Label ID="lblPartidosJugados" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGanados" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblEmpates" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblPerdidos" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesFavor" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesContra" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblAmarillas" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblRojas" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <br />
            Goleador del Equipo
            <br />
            <img id="imgGoleador" runat="server" />
               <br />
            <asp:Label ID="lblNombreGoleador" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesGoleador" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblPartidosGoleador" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <br />
            Ultimos Partidos
                <div class="panel panel-default">
                    <div class="panel-body small-padding">
                        <table id="tabla-fechas" class="table nomargin-bottom">
                            <thead style="display: none;">
                                <tr>
                                    <th class="col-md-4">Equipo Local</th>
                                    <th class="col-md-2">Resultado</th>
                                    <th class="col-md-4">Equipo Visitante</th>
                                </tr>
                            </thead>
                            <tbody class="tablaFiltro">
                                <asp:Repeater ID="rptPartidos" runat="server">
                                    <ItemTemplate>
                                        <asp:Panel ID="panelPartidoNormal" runat="server" Visible="false">
                                            <tr>
                                                <td><%# Eval("Equipo Local") %></td>
                                                <td class="col-xs-4"><%# Eval("Goles Local") %>
                                                       - <%# Eval("Goles Visitante") %>
                                                </td>
                                                <td> <%# Eval("Equipo Visitante") %></td>  
                                            </tr>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
        </div>
    </div>
</asp:Content>
