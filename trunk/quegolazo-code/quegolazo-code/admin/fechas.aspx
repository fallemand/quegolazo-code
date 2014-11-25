<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.admin.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upListadoFases" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="well">
                            <div class="row">
                                <fieldset class="vgSeleccionarEdicion">
                                    <div class="col-md-8">
                                        <div id="selectEdiciones">
                                            <asp:DropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar Edición" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound" OnItemCommand="rptFases_ItemCommand">
                            <HeaderTemplate>
                                <div class="panel-group" id="fases">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading panel-heading-master">
                                        <div class="row clearfix" id="masterContainer">
                                            <div class="col-md-5">
                                                <a data-toggle="collapse" data-parent="#fases" href="#fase-<%# Eval("idFase") %>" class="text-muted" style="font-size: 15px;">
                                                    <span class="glyphicon glyphicon-plus"></span>
                                                    Fase <%# Eval("idFase") %>
                                                </a>
                                            </div>
                                            <div class="col-md-4 nopadding-left">
                                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Fechas" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:LinkButton title="Finalizar Fecha" ClientIDMode="AutoID" rel="txtTooltip" ID="lnkFinalizarFase" data-placement="left" runat="server" CommandName="finalizarFase" CommandArgument='<%# Eval("idFase") %>'>
                                                    <span class="label label-green label-big">Finalizar</span></asp:LinkButton>
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
                                                                <a data-toggle="collapse" data-parent="#fechas" href="#fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>" style="font-size: 15px;">Fecha <%# Eval("idFecha") %> <small>Ver Más Detalles</small>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id='fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>' class="panel-collapse collapse">
                                                            <div class="panel-body small-padding">
                                                                <table class="table nomargin-bottom">
                                                                    <thead style="display: none;">
                                                                        <tr>
                                                                            <th class="col-md-4">Equipo Local</th>
                                                                            <th class="col-md-2">Resultado</th>
                                                                            <th class="col-md-4">Equipo Visitante</th>
                                                                            <th class="col-md-2"></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="tablaFiltro">
                                                                        <asp:Repeater ID="rptPartidos" runat="server" OnItemCommand="rptPartidos_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# ((Entidades.Equipo)DataBinder.Eval(Container.DataItem, "local")).nombre %></td>
                                                                                    <td><%# ((Entidades.Partido)Container.DataItem).golesLocal %>
                                                                                        <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesLocal.ToString()+")" : "" %>
                                                                                        - <%# ((Entidades.Partido)Container.DataItem).golesVisitante%>
                                                                                        <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesVisitante.ToString()+")" : "" %>
                                                                                    </td>
                                                                                    <td><%# ((Entidades.Equipo)DataBinder.Eval(Container.DataItem, "visitante")).nombre %></td>
                                                                                    <td>
                                                                                        <asp:LinkButton title="Administrar Partido" ClientIDMode="AutoID" rel="txtTooltip" ID="lnkAdministrarPartido" runat="server" CommandName="administrarPartido" CommandArgument='<%# Eval("idPartido") + ";fase" + ((Entidades.Fase)((((RepeaterItem)Container.Parent.Parent.Parent.Parent)).DataItem)).idFase +"-fecha"+((Entidades.Fecha)((RepeaterItem)Container.Parent.Parent).DataItem).idFecha %>'><span class="glyphicon glyphicon-cog"></span></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-7">
                <asp:UpdatePanel ID="upAdministrarFecha" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Administrar Partido                                
                            </div>
                            <div class="panel-body nopadding-bottom">
                                <fieldset class="vgPartido">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Equipos</label>
                                            <div class="col-md-4 nopadding-right">
                                                <input type="text" class="form-control" runat="server" id="txtEquipoLocal" required="true" disabled="true">
                                            </div>
                                            <div class="col-md-1" style="padding-left: 20px; line-height: 33px;">
                                                VS
                                            </div>
                                            <div class="col-md-5">
                                                <input type="text" class="form-control" runat="server" id="txtEquipoVisitante" required="true" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Resultado</label>
                                            <div class="col-md-2 nopadding-right">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtGolesLocal" rel="txtTooltip" title="Goles Equipo Local" maxlength="2" min="0" digits="true" placeholder="EJ: 2">
                                            </div>
                                            <div class="col-md-1 nopadding-right nopadding-left" style="padding-left:5px">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtPenalesLocal"  rel="txtTooltip" title="Goles en penales" maxlength="2" min="0" digits="true" style="display:none">
                                            </div>
                                            <div class="col-md-2 nopadding-right text-center"style="padding-left:40px;">
                                                <label>
                                                    <input type="checkbox" id="cbPenales" onclick="cbPenalesClick('ContentAdmin_ContentAdminTorneo_cbPenales');"  data-container="body" rel="txtTooltip" title="¿Se definió por penales?" runat="server"> ¿Penales?
                                                </label>
                                            </div>
                                            <div class="col-md-2 nopadding-right">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtGolesVisitante" rel="txtTooltip" title="Goles Equipo Visitante" maxlength="2" digits="true" min="0" placeholder="EJ: 0">
                                            </div>
                                            <div class="col-md-1 nopadding-right nopadding-left" style="padding-left:5px">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtPenalesVisitante" rel="txtTooltip" title="Goles en penales" maxlength="2" min="0" digits="true" style="display:none">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Fecha</label>
                                            <div id="divFechaPartido" class="col-lg-10 input-append date">
                                                <div class="input-group input-group-md">
                                                    <input type="text" data-format="dd/MM/yyyy hh:mm" class="form-control" runat="server" id="txtFecha" placeholder="Sin Asignar" fechahora="true">
                                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                                </div>
                                            </div>
                                        </div>
                                        <% if(gestorEdicion.edicion.preferencias.arbitrosAsignaXPartido) { %>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Árbitro</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlArbitros" runat="server" CssClass="form-control selectpicker" data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <% } 
                                           if(gestorEdicion.edicion.preferencias.canchaJueganEnComplejo) { %>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Cancha</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlCanchas" runat="server" CssClass="form-control searchableSelect">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <% } 
                                           if(gestorEdicion.edicion.preferencias.jugadoresXPartido) { %>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Titulares Local</label>
                                            <div class="col-lg-10">
                                                <asp:CheckBoxList ID="cblJugadoresEquipoLocal" runat="server" RepeatLayout="Table" RepeatColumns="3" Width="100%"></asp:CheckBoxList>
                                                <asp:Panel ID="panelSinJugadoresLocal" runat="server" style="padding-top: 10px;">
                                                    <small>No hay jugadores registrados para el equipo</small>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Titulares Visitante</label>
                                            <div class="col-lg-10">
                                                <asp:CheckBoxList ID="cblJugadoresEquipoVisitante" runat="server" RepeatLayout="Table" RepeatColumns="3" Width="100%"></asp:CheckBoxList>
                                                <asp:Panel ID="panelSinJugadoresVisitante" runat="server" style="padding-top: 10px;" >
                                                    <small>No hay jugadores registrados para el equipo</small>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <% } %>
                                        <div class="form-group" style="background-color: #F8F8F8">
                                            <label for="text" class="col-lg-2 control-label">
                                                <span class="flaticon-football37 flaticon-form"></span>
                                                Goles
                                            </label>
                                            <div class="col-lg-10">
                                                <p class="nomargin-bottom">
                                                    <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Gol" onclick="showSubform('goles');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Gol</a>
                                                    <asp:Repeater ID="rptGoles" runat="server" OnItemCommand="rptGoles_ItemCommand">
                                                        <ItemTemplate>
                                                            <span class="label label-default label-md">
                                                                <%# ((((Entidades.Gol)Container.DataItem).minuto!=null))? Eval("minuto") + "'": "" %>
                                                                <%# (((Entidades.Gol)Container.DataItem).jugador!=null) ? ((Entidades.Gol)Container.DataItem).jugador.nombre : ((Entidades.Gol)Container.DataItem).equipo.nombre%>
                                                                <asp:LinkButton ClientIDMode="AutoID" title="Eliminar" rel="txtTooltip" ID="lnkEliminarGol" runat="server" CommandName="eliminarGol" CommandArgument='<%# Eval("idGol") %>'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                                <div id="goles" style="display: none;" class="col-md-11 well well-sm alert-success">
                                                    <fieldset class="vgGoles">
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Equipo</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlGolesEquipos" CssClass="form-control margin-xs input-sm" runat="server" required="true" AutoPostBack="true" OnSelectedIndexChanged="ddlGolesEquipos_SelectedIndexChanged" disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <% if(gestorEdicion.edicion.preferencias.jugadoresGoles) { %>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Jugador</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlGolesJugadores" runat="server" CssClass="form-control margin-xs input-sm selectpicker" data-live-search="true" disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <% } %>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Tipo</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlGolesTipos" runat="server" CssClass="form-control margin-xs input-sm" disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Minuto</label>
                                                            <div class="col-md-10">
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtGolesMinuto" placeholder="Minuto" runat="server" maxlength="3" digits="true" disabled="true">
                                                            </div>
                                                        </div>
                                                        <asp:Button class="btn btn-default btn-xs causesValidation vgGoles pull-right" ID="btnGolAgregar" runat="server" Text="Agregar Gol" OnClick="btnGolAgregar_Click" />
                                                        <button class="btn btn-default btn-xs pull-right" onclick="hideSubform('goles');return false;">Cancelar</button>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                        <% if(gestorEdicion.edicion.preferencias.jugadoresCambios) { %>
                                        <div class="form-group" style="background-color: #F8F8F8">
                                            <label for="text" class="col-lg-2 control-label">
                                                <span class="flaticon-up23 flaticon-form"></span>
                                                Cambios
                                            </label>
                                            <div class="col-lg-10">
                                                <p class="nomargin-bottom">
                                                    <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Cambio" onclick="showSubform('cambios');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Cambio</a>
                                                    <asp:Repeater ID="rptCambios" runat="server" OnItemCommand="rptCambios_ItemCommand">
                                                        <ItemTemplate>
                                                            <span class="label label-default label-md">
                                                                <%# ((((Entidades.Cambio)Container.DataItem).minuto!=null))? Eval("minuto") + "'": "" %>
                                                        <span class="glyphicon glyphicon-arrow-up" style="color: green"></span><%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %>
                                                                <span class="glyphicon glyphicon-arrow-down" style="color: red"></span><%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %>
                                                                <asp:LinkButton ClientIDMode="AutoID" title="Eliminar" rel="txtTooltip" ID="lnkEliminarCambio" runat="server" CommandName="eliminarCambio" CommandArgument='<%# Eval("idCambio") %>'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                                <div id="cambios" style="display: none;" class="col-md-11 well well-sm alert-success">
                                                    <fieldset class="vgCambios">
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Equipo</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlCambiosEquipos" CssClass="form-control margin-xs input-sm" runat="server" OnSelectedIndexChanged="ddlCambiosEquipos_SelectedIndexChanged" AutoPostBack="True" disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Entra</label>
                                                            <div class="col-md-10 select-sm">
                                                                <asp:DropDownList ID="ddlCambiosJugadoresEntra" runat="server" CssClass="form-control margin-xs input-sm searchableSelect" required disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Sale</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlCambiosJugadoresSale" runat="server" CssClass="form-control margin-xs input-sm" required disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Minuto</label>
                                                            <div class="col-md-10">
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtCambiosMinuto" placeholder="Minuto" runat="server" maxlength="3" digits="true" disabled="true"/>
                                                            </div>
                                                        </div>
                                                        <asp:Button class="btn btn-default btn-xs causesValidation vgCambios pull-right" ID="btnCambiosAgregar" runat="server" Text="Agregar Cambio" OnClick="btnCambiosAgregar_Click" />
                                                        <button class="btn btn-default btn-xs pull-right" onclick="hideSubform('cambios');return false;">Cancelar</button>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                        <% } if(gestorEdicion.edicion.preferencias.jugadoresTarjetas) {%>
                                        <div class="form-group" style="background-color: #F8F8F8">
                                            <label class="col-lg-2 control-label">
                                                <span class="flaticon-football108 flaticon-form"></span>
                                                Tarjetas
                                            </label>
                                            <div class="col-lg-10">
                                                <p class="nomargin-bottom">
                                                    <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Tarjeta" onclick="showSubform('tarjetas');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Tarjeta</a>
                                                    <asp:Repeater ID="rptTarjetas" runat="server" OnItemCommand="rptTarjetas_ItemCommand" OnItemDataBound="rptTarjetas_ItemDataBound">
                                                        <ItemTemplate>
                                                            <span class="label label-default label-md">
                                                                <%# ((((Entidades.Tarjeta)Container.DataItem).minuto!=null))? Eval("minuto") + "'": "" %>
                                                                <%# ((Entidades.Tarjeta)Container.DataItem).jugador.nombre %>
                                                                <asp:Label ID="panelTarjetaRoja" CssClass="glyphicon glyphicon-stop" runat="server" Style="color: red;" Visible="false"></asp:Label>
                                                                <asp:Label ID="panelTarjetaAmarilla" CssClass="glyphicon glyphicon-stop" runat="server" Style="color: yellow;" Visible="false"></asp:Label>
                                                                <asp:LinkButton ClientIDMode="AutoID" title="Eliminar" rel="txtTooltip" ID="lnkEliminarTarjeta" runat="server" CommandName="eliminarTarjeta" CommandArgument='<%# Eval("idTarjeta") %>'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                                <div id="tarjetas" style="display: none;" class="col-md-11 well well-sm alert-success">
                                                    <fieldset class="vgTarjetas">
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Equipo</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlTarjetasEquipos" CssClass="form-control margin-xs input-sm" OnSelectedIndexChanged="ddlTarjetasEquipos_SelectedIndexChanged" runat="server" disabled="true" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Jugador</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlTarjetasJugadores" CssClass="form-control margin-xs input-sm" runat="server" required disabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Tipo</label>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="ddlTarjetasTipo" CssClass="form-control margin-xs input-sm" runat="server" required disabled="true">
                                                                    <asp:ListItem Text="Amarilla" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="Roja" Value="R"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group nomargin-bottom">
                                                            <label for="text" class="col-md-2 control-label">Minuto</label>
                                                            <div class="col-md-10">
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtTarjetasMinuto" placeholder="Minuto" runat="server" maxlength="3" digits="true" disabled="true">
                                                            </div>
                                                        </div>
                                                        <asp:Button class="btn btn-default btn-xs causesValidation vgTarjetas pull-right" ID="btnTarjetaAgregar" runat="server" OnClick="btnTarjetaAgregar_Click" Text="Agregar Tarjeta" />
                                                        <button class="btn btn-default btn-xs pull-right" onclick="hideSubform('tarjetas');return false;">Cancelar</button>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                        <% } %>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-success causesValidation vgPartido" ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                                </div>
                                <div class="col-xs-1">
                                    <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo">
                                        <ProgressTemplate>
                                            <img src="/resources/img/theme/load4.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                            <asp:Panel ID="panelFracaso" ClientIDMode="Static" runat="server" CssClass="alert alert-danger alert-dismissible flyover flyover-bottom" role="alert">
                                <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                <b>Error:</b> <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                            </asp:Panel>
                            <asp:Panel ID="panelExito" ClientIDMode="Static" runat="server" CssClass="alert alert-success alert-dismissible flyover flyover-bottom" role="alert">
                                <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                <b>Exito:</b> <asp:Literal ID="litExito" runat="server"></asp:Literal>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="modalConfirmarFinalizarFase" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Finalizar Fase</h4>
                </div>
                <div class="modal-body">
                    <div id="panelFaseNoCompleta" class="alert alert-danger" style="display:none;">
                        <b>Atención:</b> Esta fase posee fechas que no han sido completadas.
                    </div>  
                    ¿Esta seguro que desea finalizar la fase?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-success" Text="Finalizar" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Agregar Edicion -->
    <div class="modal fade" id="modalFinalizarFase" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="upModalEdicion" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>                            
                            <h4 class="modal-title" id="H1"><i class="flaticon-trophy5"></i>
                                <asp:Label ID="lblTituloModalEdicion" runat="server" Text="Agregar Nueva Edición"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                           <fieldset class="form-horizontal vgDatosEdicion">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Torneo</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTorneoAsociado" runat="server" name="nombreTorneoEdicion" placeholder="Nombre del Torneo" disabled>
                                                <span class="help-block">Torneo para el cual esta creando una nueva Edición</span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtNombreEdicion" runat="server" rangelength="3, 50" required="true" name="nombreEdicion" placeholder="Nombre de la Edición">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Tamaño</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlTamañoCancha" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Superficie</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlTipoSuperficie" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Género</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>                                        
                                 
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Puntos</label>
                                            <div class="col-lg-10">
                                                <div class="row">
                                                    <div class="col-xs-4">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-up"></span></span>
                                                            <input type="number" class="form-control" digits="true" id="txtPuntosPorGanar" runat="server" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3" required="required">
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <div class="input-group">
                                                            <span class="input-group-addon">=</span>
                                                            <input type="number" class="form-control" digits="true" id="txtPuntosPorEmpatar" runat="server" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1" required="required">
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                                            <input type="number" class="form-control" digits="true" id="txtPuntosPorPerder" runat="server" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0" required="required">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                             <asp:Panel ID="panFracasoEdicion" runat="server" CssClass="alert alert-danger" Visible="False">
                               <asp:Literal ID="litFracasoEdicion" runat="server"></asp:Literal>
                             </asp:Panel>                      
                        </div>
                        <div class="modal-footer">
                            <div class="col-md-5 col-md-offset-6 col-xs-10 col-xs-offset-1">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnSiguienteEdicion" runat="server" Text="Guardar" CssClass="btn btn-success causesValidation vgDatosEdicion" />
                                <asp:Button ID="btnModificarEdicion" runat="server" Text="Modificar" Visible="false" CssClass="btn btn-success causesValidation vgDatosEdicion" />
                           </div>
                            <div class="col-xs-1">
                                <asp:UpdateProgress runat="server" ID="UpdateProgressModalEdicion" AssociatedUpdatePanelID="upModalEdicion">
                                    <ProgressTemplate>
                                        <img src="/resources/img/theme/load3.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Agregar Edicion -->

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
        $('#divFechaPartido').datetimepicker({
            language: 'es'
        });
        function EndRequestHandler(sender, args) {
            $('#divFechaPartido').datetimepicker({
                language: 'es'
            });
            cbPenalesClick('ContentAdmin_ContentAdminTorneo_cbPenales');
        };
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>
</asp:Content>
