<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="fechas.aspx.cs" Inherits="quegolazo_code.admin.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
    <script type="text/javascript" src="../resources/js/moment.js"></script>
    <script type="text/javascript" src="../resources/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="../resources/js/bootstrap-datetimepicker.es.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
        <div class="container">
            <div class="row">
            <div class="col-md-5">
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
                <asp:UpdatePanel ID="upRegistrarNuevaCancha" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound">
                            <HeaderTemplate>
                                <div class="panel-group" id="fases">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="panel panel-default">
                                <div class="panel-heading panel-heading-master">
                                     <div class="row clearfix">
                                        <div class="col-md-8">
                                            <a data-toggle="collapse" data-parent="#fases" href="#fase-1" class="text-muted" style="font-size:15px;">
                                                <span class="glyphicon glyphicon-plus"></span>
                                                Fase <%# Eval("idFase") %>
                                            </a> 
                                        </div>
                                        <div class="col-md-4">
                                            <input type="text" id="Text1" class="pull-right form-control input-xs" placeholder="Filtrar Canchas"/>
                                        </div>
                                    </div>                                           
                                </div>
                                <div id="fase-1" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:Repeater ID="rptFechas" runat="server" OnItemDataBound="rptFechas_ItemDataBound">
                                            <HeaderTemplate>
                                                <div class="panel-group" id="fechas">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#fechas" href="#fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>" style="font-size:15px;">
                                                                Fecha <%# Eval("idFecha") %> <small>Ver Más Detalles</small>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id="fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>" class="panel-collapse collapse">
                                                        <div class="panel-body small-padding">
                                                            <table class="table nomargin-bottom">
                                                                <thead style="display:none;">
                                                                    <tr>
                                                                        <th class="col-md-4">Equipo Local</th>
                                                                        <th class="col-md-4">Equipo Visitante</th>
                                                                        <th class="col-md-2">Resultado</th>
                                                                        <th class="col-md-2"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                        <asp:Repeater ID="rptPartidos" runat="server" OnItemCommand="rptPartidos_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td><%# ((Entidades.Equipo)DataBinder.Eval(Container.DataItem, "local")).nombre %></td>
                                                                                    <td><%# ((Entidades.Equipo)DataBinder.Eval(Container.DataItem, "visitante")).nombre %></td>
                                                                                    <td>2-0</td>
                                                                                    <td>
                                                                                        <asp:LinkButton ClientIDMode="AutoID" title="Administrar Partido" rel="txtTooltip" ID="lnkAdministrarPartido" runat="server" CommandName="administrarPartido" CommandArgument='<%#Eval("idPartido")%>' ><span class="glyphicon glyphicon-cog"></span></asp:LinkButton></td>
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
                        <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-7">
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
                                    <div class="col-lg-4">
                                        <input type="text" class="form-control" runat="server" id="txtEquipoLocal" required="true" disabled="true">
                                    </div>
                                    <div class="col-lg-1">
                                        VS
                                    </div>
                                    <div class="col-lg-5">
                                        <input type="text" class="form-control" runat="server" id="txtEquipoVisitante" required="true" disabled="true">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Fecha</label>
                                    <div id="divFechaPartido" class="col-lg-10 input-append date">
                                        <div class="input-group input-group-md">
                                            <input type="text" data-format="dd/MM/yyyy hh:mm" class="form-control" runat="server" id="txtFecha" placeholder="Sin Asignar" required="true" fecha="true">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="select" class="col-lg-2 control-label">Árbitro</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlArbitros" runat="server" CssClass="form-control">

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Cancha</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlCanchas" runat="server" CssClass="form-control">

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Titulares Local</label>
                                    <div class="col-lg-10">
                                        <asp:Repeater ID="rptJugadoresEquipoLocal" runat="server">
                                            <ItemTemplate>
                                                <div class="checkbox col-md-4">
                                                    <label><input type="checkbox" runat="server" value="<%#((Entidades.Jugador)Container.DataItem).idJugador %>"><%#((Entidades.Jugador)Container.DataItem).nombre %></label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Titulares Visitante</label>
                                    <div class="col-lg-10">
                                        <asp:Repeater ID="rptJugadoresEquipoVisitante" runat="server">
                                            <ItemTemplate>
                                                <div class="checkbox col-md-4">
                                                    <label><input type="checkbox" runat="server" value="<%#((Entidades.Jugador)Container.DataItem).idJugador %>"><%#((Entidades.Jugador)Container.DataItem).nombre %></label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="form-group" style="background-color:#F8F8F8">
                                    <label for="text" class="col-lg-2 control-label">
                                        <span class="flaticon-football37 flaticon-form"></span>
                                        Goles
                                    </label>
                                    <div class="col-lg-10">
                                        <p class="nomargin-bottom">
                                            <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Gol" onclick="showSubform('goles');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Gol</a>
                                            <asp:Repeater ID="rptGoles" runat="server">
                                                <ItemTemplate>
                                                    <span class="label label-default label-md"><%# Eval("minuto")%>' <%# Eval("nombre")%>
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
                                                        <asp:DropDownList id="ddlGolesEquipos" CssClass="form-control margin-xs input-sm" runat="server" required="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Jugador</label>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlGolesJugadores"  runat="server" CssClass="form-control margin-xs input-sm"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Tipo</label>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlGolesTipos"  runat="server" CssClass="form-control margin-xs input-sm"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Minuto</label>
                                                    <div class="col-md-10">
                                                        <input type="text" class="form-control margin-xs input-sm" id="txtGolesMinuto" placeholder="Minuto" runat="server" maxlength="3">
                                                    </div>
                                                </div>
                                                <asp:Button class="btn btn-default btn-xs causesValidation vgGoles pull-right" ID="btnGolAgregar" runat="server" Text="Agregar Gol" />
                                                <button class="btn btn-default btn-xs pull-right" OnClick="hideSubform('goles');return false;">Cancelar</button>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" style="background-color:#F8F8F8">
                                    <label for="text" class="col-lg-2 control-label">
                                        <span class="flaticon-up23 flaticon-form"></span>
                                        Cambios
                                    </label>
                                    <div class="col-lg-10">
                                        <p class="nomargin-bottom">
                                            <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Cambio" onclick="showSubform('cambios');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Cambio</a>
                                            <asp:Repeater ID="rptCambios" runat="server">
                                                <ItemTemplate>
                                                    <span class="label label-default label-md"><%# Eval("minuto")%>' 
                                                        <span class="glyphicon glyphicon-arrow-up" style="color:green"></span> <%# ((Entidades.Cambio)Container.DataItem).jugadorEntra.nombre %>
                                                        <span class="glyphicon glyphicon-arrow-down" style="color:red"></span> <%# ((Entidades.Cambio)Container.DataItem).jugadorSale.nombre %>
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
                                                        <asp:DropDownList id="ddlCambiosEquipos" CssClass="form-control margin-xs input-sm" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Entra</label>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlCambiosJugadoresEntra" runat="server" CssClass="form-control margin-xs input-sm" required></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Sale</label>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlCambiosJugadoresSale" runat="server" CssClass="form-control margin-xs input-sm" required></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Minuto</label>
                                                    <div class="col-md-10">
                                                        <input type="text" class="form-control margin-xs input-sm" id="txtCambiosMinuto" placeholder="Minuto" runat="server" maxLenght="3" />
                                                    </div>
                                                </div>
                                                <asp:Button class="btn btn-default btn-xs causesValidation vgCambios pull-right" ID="btnCambiosAgregar" runat="server" Text="Agregar Cambio" />
                                                <button class="btn btn-default btn-xs pull-right" OnClick="hideSubform('cambios');return false;">Cancelar</button>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" style="background-color:#F8F8F8">
                                    <label class="col-lg-2 control-label">
                                        <span class="flaticon-football108 flaticon-form"></span>
                                        Tarjetas
                                    </label>
                                    <div class="col-lg-10">
                                           <p class="nomargin-bottom">
                                            <a class="btn btn-success btn-xs" rel="txtTooltip" title="Agregar Tarjeta" onclick="showSubform('tarjetas');return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Tarjeta</a>
                                            <asp:Repeater ID="rptTarjetas" runat="server">
                                                <ItemTemplate>
                                                    <span class="label label-default label-md"><%#Eval("minuto") %>' <%# Eval("nombre") %>
                                                        <asp:Panel ID="panelTarjetaAmarilla" runat="server" Visible="False">
                                                            <span class="glyphicon glyphicon-stop" style="color:yellow"></span>
                                                        </asp:Panel>
                                                        <asp:Panel ID="panel1" runat="server" Visible="False">
                                                            <span class="glyphicon glyphicon-stop" style="color:red"></span>
                                                        </asp:Panel>
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
                                                        <asp:DropDownList id="ddlTarjetasEquipos" CssClass="form-control margin-xs input-sm" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Jugador</label>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList id="ddlTarjetasJugador" CssClass="form-control margin-xs input-sm" runat="server" required></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Tipo</label>
                                                    <div class="col-md-10">
                                                        <select id="ddlTarjetasTipo" class="form-control margin-xs input-sm" required>
                                                            <option value="A">Amarilla</option>
                                                            <option value="R">Roja</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group nomargin-bottom">
                                                    <label for="text" class="col-md-2 control-label">Minuto</label>
                                                    <div class="col-md-10">
                                                        <input type="text" class="form-control margin-xs input-sm" id="txtTarjetasMinuto" placeholder="Minuto" runat="server" maxlength="3" >
                                                    </div>
                                                </div>
                                                <asp:Button class="btn btn-default btn-xs causesValidation vgTarjetas pull-right" ID="btnTarjetaAgregar" runat="server" Text="Agregar Tarjeta" />
                                                <button class="btn btn-default btn-xs pull-right" OnClick="hideSubform('tarjetas');return false;">Cancelar</button>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="panel2" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </asp:Panel>
                    </div>
                    <div class="panel-footer clearfix text-right">
                        <div class="col-xs-8 col-xs-offset-3">
                            <asp:Button class="btn btn-default" ID="btnCancelarModificacionJugador" runat="server" Text="Cancelar" Visible="false" />
                            <asp:Button class="btn btn-success causesValidation vgPartido" ID="btnModificarJugador" runat="server" Text="Modificar" Visible="false" />
                            <asp:Button class="btn btn-success causesValidation vgPartido" ID="btnRegistrarJugador" runat="server" Text="Registrar" Enabled="False" />
                        </div>
                        <div class="col-xs-1">
                            <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo">
                                <ProgressTemplate>
                                    <img src="/resources/img/theme/load4.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>
            </div>
        </div>
     </div>
    <script type="text/javascript">
        $(function () {
            $('#divFechaPartido').datetimepicker({
                language: 'es'
            });
        });
    </script>
</asp:Content>
