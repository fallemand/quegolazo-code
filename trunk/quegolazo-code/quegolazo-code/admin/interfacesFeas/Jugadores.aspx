<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="Jugadores.aspx.cs" Inherits="quegolazo_code.admin.interfacesFeas.Jugadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
         <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row clearfix">
                        Jugadores
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblNombreJugador" runat="server" Text=""></asp:Label>
                    <img id="imagenJugador" runat="server" />
                </div>
                <div class="panel-body nopadding-bottom">
                    <fieldset class="vgEquipo">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <br />
                                Fecha de Nacimiento:
                                <asp:Label ID="lblFechaNacimiento" runat="server" Text=""></asp:Label>
                                <br />
                                Número de Camiseta<asp:Label ID="lblNroCamiseta" runat="server" Text=""></asp:Label>
                                <br />
                                <img id="imgEquipo" runat="server" />
                                <br />
                                Equipo:
                                <asp:Label ID="lblEquipo" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
       
        <div class="row">
            <br />
            <asp:Label ID="lblGolesJugada" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesCabeza" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesPenal" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesTiroLibre" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesEnContra" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblGolesNoDefinido" runat="server" Text=""></asp:Label>
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
                                                <td>
                                                    <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre : "" %></td>
                                                <td class="col-xs-4"><%# ((Entidades.Partido)Container.DataItem).golesLocal %>
                                                    <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesLocal.ToString()+")" : "" %>
                                                       - <%# ((Entidades.Partido)Container.DataItem).golesVisitante%>
                                                    <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesVisitante.ToString()+")" : "" %>
                                                </td>
                                                <td><%# ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.nombre : "" %></td>  
                                            </tr>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
        </div>
        <div class="row">
            <br />
            Ediciones jugadas
           <table id="Table1" class="table nomargin-bottom">
                            <thead style="display: none;">
                                <tr>
                                    <th class="col-md-6">Edición</th>
                                    <th class="col-md-6">GolesConvertidos</th>
                                </tr>
                            </thead>
                            <tbody class="tablaFiltro">
                                <asp:Repeater ID="rptEdiciones" runat="server">
                                    <ItemTemplate>
                                        <asp:Panel ID="panelPartidoNormal" runat="server" Visible="false">
                                            <tr>
                                                <td> <%# Eval("edicion")%></td>
                                                <td><%# Eval("golesConvertidos")%></td>  
                                            </tr>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
        </div>
    </div>
</asp:Content>
