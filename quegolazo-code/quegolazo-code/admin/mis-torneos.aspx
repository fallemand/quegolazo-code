﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="mis-torneos.aspx.cs" Inherits="quegolazo_code.admin.mis_torneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <script src="../resources/js/misTorneos/misTorneos.js"></script>
    <div class="container padding-top">
        <div class="row">
            <div class="container">
                <button type="button" class="btn btn-success" data-toggle="modal" onclick="crearTorneo();" data-target="#registrarTorneo">Crear un Nuevo Torneo</button>
                <asp:Label ID="lblMensajeTorneos" runat="server" Text=""></asp:Label>
                <asp:UpdatePanel ID="pnlLoguear" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnResgitrarTorneo" />
                        <asp:AsyncPostBackTrigger ControlID="btnRegistrarEdicion" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Repeater ID="rptTorneos" runat="server" OnItemDataBound="rptTorneosItemDataBound" OnItemCommand="rptTorneos_ItemCommand">
                            <ItemTemplate>
                                <div class="panel panel-default lista-torneos shadow-sm">
                                    <div class="panel-heading header clearfix">
                                        <div class="col-md-1">
                                            <div class="thumbnail nomargin-bottom">
                                                <img id="img<%# Eval("idTorneo") %>" src="<%# Eval("rutaImagen") %>" />
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <h3><%# Eval("nombre") %></h3>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="pull-right botones">
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Ver Sitio Web del Torneo"><span class="glyphicon glyphicon-globe"></span></a>
                                                <%--<a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-toggle="modal" data-target="#registrarTorneo" data-placement="top" onclick="modificarTorneo(<%#Eval("idTorneo")%>,'<%#Eval("nick")%>','<%#Eval("nombre")%>','<%#Eval("descripcion")%>' );" title="Editar Torneo"><span class="glyphicon glyphicon-pencil"></span></a>--%>
                                                <asp:LinkButton ID="lnkModificarCampeonato" title="Editar Torneo" CssClass="btn btn-panel shadow-xs" runat="server" CommandName="editarTorneo" CommandArgument='<%#Eval("idTorneo")%>' rel="txtTooltip"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Eliminar Torneo"><span class="glyphicon glyphicon-remove"></span></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="pull-left">
                                            <h4>Ediciones</h4>
                                        </div>
                                        <div class="pull-right">
                                            <asp:Button ID="btnAgregarEdicion" runat="server" Text="Agregar Edición" CommandName="agregarEdicion" CommandArgument='<%# Eval("idTorneo") %>' data-target="#agregarEdicion2" CssClass="btn btn-success btn-xs" />
                                        </div>
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-4">Nombre</th>
                                                    <th class="col-md-2">Tamaño</th>
                                                    <th class="col-md-2">Superficie</th>
                                                    <th class="col-md-2">Estado</th>
                                                    <th class="col-md-1"></th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:Repeater ID="rptEdiciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("nombre") %></td>
                                                            <td><%# Eval("tamanioCancha.nombre") %></td>
                                                            <td><%# Eval("tipoSuperficie.nombre") %></td>
                                                            <td><%# Eval("estado.nombre") %></td>
                                                            <td>
                                                                <a href="#" class="editar"><span class="glyphicon glyphicon-pencil" rel="txtTooltip" title="Editar Edición" data-toggle="tooltip" data-placement="top"></span></a>
                                                                <a href="#" class="editar"><span class="glyphicon glyphicon-remove eliminar" rel="txtTooltip" title="Eliminar Edición" data-toggle="tooltip" data-placement="top"></span></a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tbody>
                                        </table>
                                        <asp:Label ID="lblMensajeEdiciones" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Registrar Torneo -->
    <div class="modal fade" id="registrarTorneo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <fieldset class="form-horizontal validationGroup">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="modalTorneoLabel"><i class="flaticon-trophy5"></i>Registrar Nuevo Torneo</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnResgitrarTorneo" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Nombre</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" id="txtNombreTorneo" runat="server" name="nombreTorneo" minlength="3" maxlength="50" required="required" placeholder="Nombre del Nuevo Torneo">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">URL</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" name="urlTorneo" id="txtUrlTorneo" runat="server" nospace="true" minlength="3" maxlength="100" required="required" placeholder="url-del-torneo">
                                        <span class="help-block">Nombre de la url del torneo. No podrá cambiarlo. www.quegolazo.com/<b>url-del-torneo</b></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Descripción</label>
                                    <div class="col-lg-10">
                                        <textarea class="form-control" id="txtDescripcion" runat="server" maxlength="400" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-new thumbnail" data-trigger="fileinput">

                                                    <img id="imagen-preview" src="../resources/img/theme/logo-default.png" />
                                                </div>
                                                <div id="logoTorneoPreview" class="fileinput-preview fileinput-exists thumbnail" data-trigger="fileinput"></div>
                                                <div>
                                                    <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span><span class="fileinput-exists">Cambiar</span><asp:FileUpload ID="imagenUpload" runat="server" /></span>
                                                    <a href="#" class="btn btn-default btn-xs fileinput-exists" id="limpiaImagen" data-dismiss="fileinput">Eliminar</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <p class="help-block" style="margin-top: 15px;">
                                                <strong>Formato admitido</strong><br />
                                                PNG, JPEG, JPG, GIF<br />
                                                <strong>Tamaño Máximo</strong><br />
                                                1 Mb<br />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="panFracasoTorneo" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoTorneo" runat="server"></asp:Literal>
                                </asp:Panel>
                                </div>
                        <div class="modal-footer">
                            <div class="col-xs-8 col-xs-offset-5">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnResgitrarTorneo" runat="server" CssClass="btn btn-success causesValidation" data-toggle="modal" data-target="#registrarTorneo" Text="Registrar" OnClick="btnResgitrar_Click" />
                                <asp:Button ID="btnModificarTorneo" runat="server" CssClass="btn btn-success causesValidation" data-toggle="modal" data-target="#registrarTorneo" Text="Modificar" OnClick="btnModificarTorneo_Click" />
                            </div>
                            <div class="col-xs-1">
                                <asp:UpdateProgress runat="server" ID="UpdateProgressFooter">
                                    <ProgressTemplate>
                                        <img src="/resources/img/theme/load3.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
            </fieldset>
        </div>
    </div>
    <!-- Modal Registrar Torneo -->

    <!-- Modal Agregar Edicion -->
    <div class="modal fade" id="agregarEdicion2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <fieldset class="validationGroup form-horizontal">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="H1"><i class="flaticon-trophy5"></i>Agregar Nueva Edición</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#datosGenerales" data-toggle="tab">Datos Generales</a></li>
                                    <li class=""><a href="#personalizacion" data-toggle="tab">Opciones</a></li>
                                </ul>
                                <div id="myTabContent" class="tab-content" style="padding-top: 10px">
                                    <div class="tab-pane active in fade" id="datosGenerales">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Torneo</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTorneoAsociado" runat="server" name="nombreTorneoEdicion" placeholder="Nombre del Torneo" disabled>
                                                <input type="hidden" class="form-control" id="txtIdTorneo" runat="server">
                                                <span class="help-block">Torneo para el cual esta creando una nueva edición.</span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtNombreEdicion" runat="server" required="true" name="nombreEdicion" placeholder="Nombre de la Edición">
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
                                            <label for="select" class="col-lg-2 control-label">Complejos</label>
                                            <div class="col-lg-10">
                                                <div class="input-group">
                                                    <select class="form-control" name="establecimientos">
                                                        <option disabled selected>Seleccione el establecimiento</option>
                                                        <option>Complejo Quilmes</option>
                                                        <option>Complejo Tres Aguas</option>
                                                        <option>Complejo Match5</option>
                                                    </select>
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button">Agregar</button>
                                                    </span>
                                                </div>
                                                <span class="help-block">Complejo Grandes Arcos <a href="" rel="txtTooltip" title="Eliminar"><span class="glyphicon glyphicon-remove"></span></a></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Puntos</label>
                                            <div class="col-lg-10">
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-up"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorGanar" runat="server" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">=</span>
                                                        <input type="number" class="form-control" id="txtPuntosPorEmpatar" runat="server" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorPerder" runat="server" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0" required="required">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarEdicion" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Panel ID="panFracasoEdicion" runat="server" CssClass="alert alert-danger" Visible="False">
                                                    <asp:Literal ID="litFracasoEdicion" runat="server"></asp:Literal>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane fade" id="personalizacion">
                                        <div class="panel-group opciones" id="accordion">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="clearfix">
                                                        <div class="col-md-8 first">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#panelJugadores">¿Registrará Jugadores?</a>
                                                                <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará los jugadores que pertenecen a cada equipo?" data-placement="right"></span></small>
                                                            </h4>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label class="switch-light well nomargin-bottom" onclick="togglePanelJugadores();">
                                                                <input type="checkbox">
                                                                <span>
                                                                    <span>No</span>
                                                                    <span>Si</span>
                                                                </span>
                                                                <a class="btn btn-success" onclick=""></a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="panelJugadores" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <label class="control-label col-md-9" for="radios">¿Registrará que jugador juega cada partido?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgJugadores" id="rdJugadoresRegistroSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgJugadores" id="rdJugadoresRegistroNo">
                                                                No
                                                            </label>
                                                        </div>
                                                        <label class="control-label col-md-9" for="radios">¿Registrará los cambios que se hagan durante el partido?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgCambios" id="rdJugadoresCambiosSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgCambios" id="rdJugadoresCambiosNo">
                                                                No
                                                            </label>
                                                        </div>
                                                        <label class="control-label col-md-9" for="radios">¿Registrará que jugador hizo los goles?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgGoles" id="rdJugadoresGolesSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgGoles" id="rdJugadoresGolesNo">
                                                                No
                                                            </label>
                                                        </div>
                                                        <label class="control-label col-md-9" for="radios">¿Registrará las tarjetas que reciba cada jugador?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgTarjetas" id="rdJugadoresTarjetasSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgTarjetas" id="rdJugadoresTarjetasNo">
                                                                No
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="clearfix">
                                                        <div class="col-md-8 first">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#panelSanciones">¿Registrará Sanciones?</a>
                                                                <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará sanciones a los equipos o jugadores?" data-placement="right"></span></small>
                                                            </h4>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label class="switch-light well nomargin-bottom" onclick="togglePanelSanciones();">
                                                                <input type="checkbox">
                                                                <span>
                                                                    <span>No</span>
                                                                    <span>Si</span>
                                                                </span>
                                                                <a class="btn btn-success" onclick=""></a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="panelSanciones" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <label class="control-label col-md-9" for="radios">¿Registrará sanciones a los equipos?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgSancionesEquipos" id="rdSancionesEquiposSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgSancionesEquipos" id="rdSancionesEquiposNo">
                                                                No
                                                            </label>
                                                        </div>
                                                        <label class="control-label col-md-9" for="radios">¿Registrará sanciones a los jugadores?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgSancionesJugadores" id="rdSancionesJugadoresSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgSancionesJugadores" id="rdSancionesJugadoresNo">
                                                                No
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="clearfix">
                                                        <div class="col-md-8 first">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#panelArbitros">¿Registrará Árbitros?</a>
                                                                <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará Árbitros para la edición?" data-placement="right"></span></small>
                                                            </h4>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label class="switch-light well nomargin-bottom" onclick="togglePanelArbitros();">
                                                                <input type="checkbox" runat="server" id="rdArbitrosSi">
                                                                <span>
                                                                    <span>No</span>
                                                                    <span>Si</span>
                                                                </span>
                                                                <a class="btn btn-success" onclick=""></a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="panelArbitros" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <label class="control-label col-md-9" for="radios">¿Asignará árbitros en particular a cada partido?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgArbitros" id="rdArbitrosPorPartidoSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgArbitros" id="rdArbitrosPorPartidoNo">
                                                                No
                                                            </label>
                                                        </div>
                                                        <label class="control-label col-md-9" for="radios">¿Registrará el desempeño del árbitros por cada partido?</label>
                                                        <div class="col-md-3">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgArbitros" id="rdArbitroDesempenioSi" runat="server">
                                                                Si
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgArbitros" id="rdArbitroDesempenioNo">
                                                                No
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="clearfix">
                                                        <div class="col-md-8 first">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#panelCanchas">¿Registrará Complejos o Canchas?</a>
                                                                <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará Complejos o canchas donde se disputará la edición?" data-placement="bottom"></span></small>
                                                            </h4>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label class="switch-light well nomargin-bottom" onclick="togglePanelCanchas();">
                                                                <input type="checkbox">
                                                                <span>
                                                                    <span>No</span>
                                                                    <span>Si</span>
                                                                </span>
                                                                <a class="btn btn-success" onclick=""></a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="panelCanchas" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <label class="control-label col-md-5" for="radios">¿Donde se jugarán los partidos?</label>
                                                        <div class="col-md-7">
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgCanchas" id="rdCanchasComplejos" runat="server">
                                                                Complejo/s propios del torneo
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="rbgCanchas" id="rdCanchasEquipos">
                                                                Canchas de los equipos participantes
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnRegistrarOpcioens" runat="server" Text="Grabar" OnClick="btnRegistrarOpcioens_Click" CssClass="btn btn-success pull-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-footer">
                        <div class="col-xs-5 col-xs-offset-6">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnRegistrarEdicion" runat="server" Text="Registrar" class="btn btn-success causesValidation" OnClick="btnRegistrarEdicion_Click" />
                        </div>
                        <div class="col-xs-1">
                            <asp:UpdateProgress runat="server" ID="UpdateProgress1">
                                <ProgressTemplate>
                                    <img src="/resources/img/theme/load3.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <!-- Modal Agregar Edicion -->

    <script type="text/javascript">
        $('.fileinput').on('change.bs.fileinput', function () {
            $('.fileinput-preview').css('background-image', 'none');
        });
        $('#registrarTorneo').on('hidden.bs.modal', function () {
            $('.modal-body').find('input[type=text], input[type=password], input[type=number], input[type=email], textarea').val('');
            $('.modal-body').find('div').removeClass('has-success has-error');
        });
        $('#agregarEdicion2').on('hidden.bs.modal', function () {
            $('.modal-body').find('input[type=text], input[type=password], input[type=number], input[type=email], textarea').val('');
            $('.modal-body').find('div').removeClass('has-success has-error');
        });
        function closeModalTorneo() {
            $('#registrarTorneo').modal('hide');
        };
        function openModalTorneo() {
            $('#registrarTorneo').removeClass("fade");
            $('#registrarTorneo').modal('show');
        };
        function closeModalEdicion() {
            $('#agregarEdicion2').modal('hide');
        };
        function openModalEdicion() {
            $('#agregarEdicion2').modal('show');
        };
        function togglePanelJugadores() { $('#panelJugadores').collapse('toggle'); };
        function togglePanelSanciones() { $('#panelSanciones').collapse('toggle'); };
        function togglePanelArbitros() { $('#panelArbitros').collapse('toggle'); };
        function togglePanelCanchas() { $('#panelCanchas').collapse('toggle'); };
    </script>

</asp:Content>
