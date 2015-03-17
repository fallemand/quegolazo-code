<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.admin.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
    <script type="text/javascript" src="/resources/js/jquery.multi-select.js"></script>
    <script src="/resources/js/jquery.ui/jquery-ui.min.js"></script>
    <script src="/resources/js/quegolazo.js"></script>
    <script src="/resources/js/widgetFases.js"></script>
    <script src="/resources/js/jquery.bracket.min.js"></script>
    <link href="/resources/css/jquery.bracket.min.css" rel="stylesheet" />
    <script src="/resources/js/widgetLlaves.js"></script>
    <script src="../../resources/js/fechas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-push-6">
                <asp:UpdatePanel ID="upAdministrarFecha" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#accordion" href="#administrarPartido" aria-expanded="true" aria-controls="administrarPartido">
                                    <span class="glyphicon glyphicon-plus"></span>
                                    Administrar Partido 
                                </a>                               
                            </div>
                            <div id="administrarPartido" class="panel-collapse collapse in mobile-collapse" role="tabpanel" aria-labelledby="headingOne"">
                                <div class="panel-body nopadding-bottom">
                                    <fieldset class="vgPartido">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="text" class="col-lg-2 control-label">Equipos</label>
                                                <div class="col-md-4 nopadding-right mobile-padding-right">
                                                    <input type="text" class="form-control" runat="server" id="txtEquipoLocal" required="true" disabled="true">
                                                </div>
                                                <div class="col-md-1 text-center" style="padding-left: 20px; line-height: 33px;">
                                                    VS
                                                </div>
                                                <div class="col-md-5">
                                                    <input type="text" class="form-control" runat="server" id="txtEquipoVisitante" required="true" disabled="true">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="text" class="col-lg-2 col-xs-12 control-label">Resultado</label>
                                                <div class="col-md-2 col-xs-8 nopadding-right ">
                                                    <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtGolesLocal" rel="txtTooltip" title="Goles Equipo Local" maxlength="2" min="0" digits="true" placeholder="EJ: 2">
                                                </div>
                                                <div class="col-md-1 col-xs-4 nopadding-right mobile-padding-right nopadding-left" style="padding-left:5px">
                                                    <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtPenalesLocal"  rel="txtTooltip" title="Goles en penales" maxlength="2" min="0" digits="true" style="display:none">
                                                </div>
                                               <div class="col-md-2 col-xs-12 nopadding-right text-center"style="padding-left:40px;">
                                                    <label>
                                                        <input type="checkbox" id="cbPenales" onclick="cbPenalesClick('ContentAdmin_ContentAdminTorneo_cbPenales');"  data-container="body" rel="txtTooltip" title="¿Se definió por penales?" runat="server"> ¿Penales?
                                                    </label>
                                                </div>
                                                <div class="col-md-2 col-xs-8 nopadding-right">
                                                    <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtGolesVisitante" rel="txtTooltip" title="Goles Equipo Visitante" maxlength="2" digits="true" min="0" placeholder="EJ: 0">
                                                </div>
                                                <div class="col-md-1 col-xs-4 nopadding-right mobile-padding-right nopadding-left" style="padding-left:5px">
                                                    <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtPenalesVisitante" rel="txtTooltip" title="Goles en penales" maxlength="2" min="0" digits="true" style="display:none">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="text" class="col-lg-2 control-label">Fecha</label>
                                                <div class="col-lg-10 input-append date">
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
                                                    <asp:CheckBoxList ID="cblJugadoresEquipoLocal" runat="server" RepeatLayout="Table" RepeatColumns="2" Width="100%"></asp:CheckBoxList>
                                                    <asp:Panel ID="panelSinJugadoresLocal" runat="server" style="padding-top: 10px;">
                                                        <small>No hay jugadores registrados para el equipo</small>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="text" class="col-lg-2 control-label">Titulares Visitante</label>
                                                <div class="col-lg-10">
                                                    <asp:CheckBoxList ID="cblJugadoresEquipoVisitante" runat="server" RepeatLayout="Table" RepeatColumns="2" Width="100%"></asp:CheckBoxList>
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
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Equipo</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlGolesEquipos" CssClass="form-control margin-xs input-sm" runat="server" required="true" AutoPostBack="true" OnSelectedIndexChanged="ddlGolesEquipos_SelectedIndexChanged" disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <% if(gestorEdicion.edicion.preferencias.jugadoresGoles) { %>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Jugador</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlGolesJugadores" runat="server" CssClass="form-control margin-xs input-sm selectpicker" data-live-search="true" disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <% } %>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Tipo</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlGolesTipos" runat="server" CssClass="form-control margin-xs input-sm" disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Minuto</label>
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
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Equipo</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlCambiosEquipos" CssClass="form-control margin-xs input-sm" runat="server" OnSelectedIndexChanged="ddlCambiosEquipos_SelectedIndexChanged" AutoPostBack="True" disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Entra</label>
                                                                <div class="col-md-10 select-sm">
                                                                    <asp:DropDownList ID="ddlCambiosJugadoresEntra" runat="server" CssClass="form-control margin-xs input-sm searchableSelect" required disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Sale</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlCambiosJugadoresSale" runat="server" CssClass="form-control margin-xs input-sm" required disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Minuto</label>
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
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Equipo</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlTarjetasEquipos" CssClass="form-control margin-xs input-sm" OnSelectedIndexChanged="ddlTarjetasEquipos_SelectedIndexChanged" runat="server" disabled="true" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Jugador</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlTarjetasJugadores" CssClass="form-control margin-xs input-sm" runat="server" required disabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Tipo</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="ddlTarjetasTipo" CssClass="form-control margin-xs input-sm" runat="server" required disabled="true">
                                                                        <asp:ListItem Text="Amarilla" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="Roja" Value="R"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group nomargin-bottom">
                                                                <label for="text" class="col-md-2 control-label mobile-nopadding-top">Minuto</label>
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
                                    <div class="mobile-nopadding-left col-xs-10 col-md-8 col-md-offset-3">
                                        <asp:Button class="btn btn-default" ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" />
                                        <asp:Button class="btn btn-success causesValidation vgPartido" ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                                    </div>
                                    <div class="col-xs-2 col-md-1">
                                        <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo">
                                            <ProgressTemplate>
                                                <img src="/resources/img/theme/load4.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6 col-md-pull-6">
                <asp:UpdatePanel ID="upListadoFases" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="well">
                            <div class="row">
                                <fieldset class="vgSeleccionarEdicion">
                                    <div class="col-md-8 col-xs-9 mobile-nopadding-left">
                                        <div id="selectEdiciones">
                                            <aspNew:NewDropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true"></aspNew:NewDropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-3 mobile-nopadding-left">
                                        <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
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
                                                <asp:LinkButton Visible="false" title="Finalizar Fase" ClientIDMode="AutoID" rel="txtTooltip" ID="lnkFinalizarFase" data-placement="left" runat="server" OnClientClick="" CommandName="finalizarFase" CommandArgument='<%# Eval("idFase") %>'>
                                                    <span class="label label-red label-big">Finalizar</span></asp:LinkButton>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal para la finalización de fases -->
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
                    ¿Está seguro que desea finalizar la fase?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                  
                    <asp:Button ID="btnFinalizar" runat="server" OnClick="btnFinalizar_Click" CssClass="btn btn-success" Text="Finalizar" />
                </div>
            </div>
        </div>
    </div>
    <!-- Fin para la finalización de fases -->

     <!-- Modal para la finalización de Edición -->
    <div class="modal fade" id="modalFinalizarEdicion" tabindex="-1"  role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="H2">Finalizar Edición</h4>
                </div>
                <div class="modal-body">
                    <div id="panelFaseNoCompleta2" class="alert alert-danger" style="display:none;">
                        <b>Atención:</b> Esta fase posee fechas que no han sido completadas.
                    </div> 
                    Ha finalizado la última fase de la edición ¿Qué desea hacer?
                </div>
                <div class="modal-footer" >
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnNuevaFase" runat="server" OnClick="btnFinalizar_Click" CssClass="btn btn-success " Text="Crear Nueva Fase" />
                    <asp:Button ID="btnFinalizarEdicion" runat="server" OnClick="btnFinalizarEdicion_Click" CssClass="btn btn-success" Text="Finalizar Edición" />
                </div>
            </div>
        </div>
    </div>
    <!-- Fin para la finalización de Edición -->


    <!-- Modal Seleccionar Equipos que pasan-->
    <div class="modal fade" id="modalFinalizarFase" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                  <asp:UpdatePanel ID="upModalEdicion" runat="server">
                    <ContentTemplate>
                   
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="reiniciarContador();">&times;</button>
                            <h4 class="modal-title" id="H1"><i class="flaticon-football106"></i><span id="tituloModal">Seleccionar los Equipos Clasificados</span> 
                                <%--<asp:Label ID="lblTituloModalEdicion" runat="server" Text="Agregar Nueva Edición"></asp:Label>--%>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="panelSeleccionarEquipos" ClientIDMode="Static"  runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <div class="btn-group btn-group-sm" role="group" style="margin-right:5px;" aria-label="...">
                                                    <button type="button" class="btn btn-success btn-sm" onclick="$('#tabla-posiciones tr').show('fast');">Todos</button>
                                                </div>
                                                <div class="btn-group btn-group-sm" role="group" aria-label="...">
                                                    <asp:Repeater ID="rptGrupos" runat="server">
                                                        <ItemTemplate>
                                                                <button type="button" class="btn btn-success" onclick="filtrarPosiciones('<%# Eval("idGrupo")%>')">Grupo <%# Eval("idGrupo")%></button>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                Equipos Seleccionados: <span id="spanSeleccionados" class="badge">0</span>
                                            </div>
                                        </div>
                                        <div class="row margin-top" style="max-height: 350px !important;overflow: auto;">
                                            <div class="col-md-12">
                                                <table id="tabla-posiciones" class="table table-condensed table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th class="col-md-1"></th>
                                                            <th class="col-md-4" colspan="2">Equipo</th>
                                                            <th class="col-md-1">PTS</th>
                                                            <th class="col-md-1">PJ</th>
                                                            <th class="col-md-1">PG</th>
                                                            <th class="col-md-1">PE</th>
                                                            <th class="col-md-1">PP</th>
                                                            <th class="col-md-1">GF</th>
                                                            <th class="col-md-1">GC</th>
                                                            <th style="display:none;"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tablaFiltro">
                                                        <asp:Repeater ID="rptEquipos" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="checkbox" value="<%# Eval("idEquipo") %>" /></td>
                                                                    <td><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                                    <td><%# Eval("Equipo") %></td>
                                                                    <td class="active" style="font-size:16px;"><b><%# Eval("Puntos") %></b></td>
                                                                    <td><%# Eval("PJ") %></td>
                                                                    <td><%# Eval("PG") %></td>
                                                                    <td><%# Eval("PE") %></td>
                                                                    <td><%# Eval("PP") %></td>
                                                                    <td><%# Eval("GF") %></td>
                                                                    <td><%# Eval("GC") %></td>
                                                                    <td class="idEquipo" style="display:none;"><%# Eval("idEquipo") %></td>
                                                                    <td style="display:none;"><%# Eval("idGrupo") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfEquiposSeleccionados" ClientIDMode="Static" runat="server" />
                            </asp:Panel>

                            <!-- Toni se te ve la tanga-->
                            <asp:Panel ID="panelConfigurarFase" ClientIDMode="Static" runat="server">
                                  <div class="row">
                                    <div class="col-md-12" id="contenedorFases">
                                         <p class="bs-component">    
                                    </div>
                                  </div>
                                  <div class="row">
                                       <div class="col-md-12">
                                          <div class="panel-group" id="accordionFases">   
              
                                          </div>
                                      </div>
                                  </div> 
                            </asp:Panel>
                            <div ID="divFracaso" Class="alert alert-danger margin-top" style="display:none" >                
                             <p id="txtFracaso" runat="server" ClientIDMode="Static"></p>
                            </div>
                        </div>
                        <div class="modal-footer">
                         <div class="col-md-5 col-md-offset-6 col-xs-10 col-xs-offset-1">
                         <Button ID="btnCerrar" data-dismiss="modal" onclick="clickBotonCerrar();" class="btn btn-default" > Cancelar</button>
                         <Button ID="btnConfigurarFase"  onclick="clickSiguiente();" type="button" class="btn btn-success"> Siguiente</button>  
                         <asp:Button ID="btnConfirmar" style="display:none" runat="server" Text="Confirmar Fase" OnClick="btnConfirmar_Click" OnClientClick="return $('#contenedorFases').generadorDeFases('guardarFasesEnSesion');closeModal('modalFinalizarFase');"   ClientIDMode="Static" CssClass="btn btn-success causesValidation vgDatosEdicion" />
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
    <!-- Modal Seleccionar Equipos que pasan -->

   <!-- Modal Seleccionar Ganadores - Finalizar Edicion-->
    <div class="modal fade" id="modalSeleccionarGanadores" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H3"><i class="flaticon-trophy5"></i>Tabla de Posiciones Final
                                <%--<asp:Label ID="lblTituloModalEdicion" runat="server" Text="Agregar Nueva Edición"></asp:Label>--%>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="panel1" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-8">
                                                Para modificar, arrastre los equipos a la posición correspondiente
                                            </div>
                                        </div>
                                        <div class="row margin-top" style="max-height: 350px !important;overflow: auto;">
                                            <div class="col-md-12">
                                                <table id="tabla-posiciones2" class="table table-condensed table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th class="col-md-1">Posición</th>
                                                            <th class="col-md-4" colspan="2">Equipo</th>
                                                            <th class="col-md-1">PTS</th>
                                                            <th class="col-md-1">PJ</th>
                                                            <th class="col-md-1">PG</th>
                                                            <th class="col-md-1">PE</th>
                                                            <th class="col-md-1">PP</th>
                                                            <th class="col-md-1">GF</th>
                                                            <th class="col-md-1">GC</th>
                                                            <th style="display:none;"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tablaFiltro">
                                                           <asp:Repeater ID="rptPosiciones" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="font-size:17px" ><strong> <%# Container.ItemIndex + 1 %>º </strong></td>
                                                                    <td><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                                    <td><%# Eval("Equipo") %></td>
                                                                    <td class="active" style="font-size:16px;"><b><%# Eval("Puntos") %></b></td>
                                                                    <td><%# Eval("PJ") %></td>
                                                                    <td><%# Eval("PG") %></td>
                                                                    <td><%# Eval("PE") %></td>
                                                                    <td><%# Eval("PP") %></td>
                                                                    <td><%# Eval("GF") %></td>
                                                                    <td><%# Eval("GC") %></td>
                                                                    <td class="idEquipo" style="display:none;"><%# Eval("idEquipo") %></td>
                                                                    <td style="display:none;"><%# Eval("idGrupo") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                     
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                        <div class="modal-footer">
                            <div class="col-md-5 col-md-offset-6 col-xs-10 col-xs-offset-1">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnConfirmarFinalizacion" runat="server" Text="Finalizar" OnClientClick="enviarPosicionesEquipos();" OnClick="btnConfirmarFinalizacion_Click" class="btn btn-success pull-right"/>
                           </div>  
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Confirmar cambios en cantidades -->
        <div class="modal fade bs-example-modal-sm" id="modalCambioEnCantidades" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="H4">Confirmar cambios</h4>
                </div>
                <div class="modal-body">
                    <div id="Div2" class="alert alert-danger">
                        <b>Atención:</b> (Acá va una explicacion que se entienda). Si continua, se eliminarán las fases siguientes.
                    </div> 
                    ¿Está seguro que desea finalizar la fase?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCambioEnCantidadEquipos" runat="server" OnClick="btnCambioEnCantidadEquipos_Click" CssClass="btn btn-success" Text="Continuar" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Confirmar cambios en cantidades -->

    <script type="text/javascript">
     
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>
 
    
</asp:Content>
