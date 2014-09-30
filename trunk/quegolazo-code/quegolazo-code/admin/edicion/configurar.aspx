<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="configurar.aspx.cs" Inherits="quegolazo_code.admin.edicion.configurar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentEdicion" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading panel-heading-master">
            <span class="glyphicon glyphicon-cog"></span>
            Configuración de la edición
        </div>
        <div class="panel-body">
            <div class="panel-group opciones" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="clearfix">
                            <div class="col-md-9 first">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#panelJugadores">¿Registrará Jugadores?</a>
                                    <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará los jugadores que pertenecen a cada equipo?" data-placement="right"></span></small>
                                </h4>
                            </div>
                            <div class="col-md-3 switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="togglePanel('panelJugadores','rdJugadoresSi','rdJugadoresNo');return false;">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdJugadoresNo" name="rbgJugadores">
                                        <label for="rdJugadoresNo" onclick="">No</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdJugadoresSi" name="rbgJugadores"> 
                                        <label for="rdJugadoresSi" onclick="">Si</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <div id="panelJugadores" class="panel-collapse collapse">
                        <div class="panel-body">
                            <label class="control-label col-md-9" for="radios">¿Registrará que jugador juega cada partido?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgRegistraJugadores" id="rdJugadoresRegistroSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgRegistraJugadores" id="rdJugadoresRegistroNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                            <label class="control-label col-md-9" for="radios">¿Registrará los cambios que se hagan durante el partido?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgCambios" id="rdJugadoresCambiosSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgCambios" id="rdJugadoresCambiosNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                            <label class="control-label col-md-9" for="radios">¿Registrará que jugador hizo los goles?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgGoles" id="rdJugadoresGolesSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgGoles" id="rdJugadoresGolesNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                            <label class="control-label col-md-9" for="radios">¿Registrará las tarjetas que reciba cada jugador?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgTarjetas" id="rdJugadoresTarjetasSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgTarjetas" id="rdJugadoresTarjetasNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="clearfix">
                            <div class="col-md-9 first">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#panelSanciones">¿Registrará Sanciones?</a>
                                    <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará sanciones a los equipos o jugadores?" data-placement="right"></span></small>
                                </h4>
                            </div>
                            <div class="col-md-3 switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="togglePanel('panelSanciones','rdSancionesSi','rdSancionesNo');return false;">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdSancionesNo" name="rbgSanciones">
                                        <label for="rdSancionesNo" onclick="">No</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdSancionesSi" name="rbgSanciones">
                                        <label for="rdSancionesSi" onclick="">Si</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
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
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgSancionesEquipos" id="rdSancionesEquiposNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                            <label class="control-label col-md-9" for="radios">¿Registrará sanciones a los jugadores?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgSancionesJugadores" id="rdSancionesJugadoresSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgSancionesJugadores" id="rdSancionesJugadoresNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="clearfix">
                            <div class="col-md-9 first">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#panelArbitros">¿Registrará Árbitros?</a>
                                    <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará Árbitros para la edición?" data-placement="right"></span></small>
                                </h4>
                            </div>
                            <div class="col-md-3 switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="togglePanel('panelArbitros','rdArbitrosSi','rdArbitrosNo');return false;">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdArbitrosNo" name="rbgArbitros">
                                        <label for="rdArbitrosNo" onclick="">No</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdArbitrosSi" name="rbgArbitros">
                                        <label for="rdArbitrosSi" onclick="">Si</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <div id="panelArbitros" class="panel-collapse collapse">
                        <div class="panel-body">
                            <label class="control-label col-md-9" for="radios">¿Asignará árbitros en particular a cada partido?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgArbitrosPorPartido" id="rdArbitrosPorPartidoSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgArbitrosPorPartido" id="rdArbitrosPorPartidoNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                            <label class="control-label col-md-9" for="radios">¿Registrará el desempeño del árbitros por cada partido?</label>
                            <div class="col-md-3">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgArbitrosDesempenio" id="rdArbitroDesempenioSi" runat="server">
                                    Si
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgArbitrosDesempenio" id="rdArbitroDesempenioNo" runat="server">
                                    No
                                </label>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="clearfix">
                            <div class="col-md-9 first">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#panelCanchas">¿Registrará Complejos o Canchas?</a>
                                    <small><span class="glyphicon glyphicon-question-sign" rel="txtTooltip" title="¿Registrará Complejos o canchas donde se disputará la edición?" data-placement="right"></span></small>
                                </h4>
                            </div>
                            <div class="col-md-3 switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="togglePanel('panelCanchas','rdCanchasSi','rdCanchasNo');return false;" id="toggle12">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdCanchasNo" name="rbgCanchas">
                                        <label for="rdCanchasNo" onclick="">No</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdCanchasSi"  name="rbgCanchas">
                                        <label for="rdCanchasSi" onclick="">Si</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <div id="panelCanchas" class="panel-collapse collapse">
                        <div class="panel-body">
                            <label class="control-label col-md-5" for="radios">¿Donde se jugarán los partidos?</label>
                            <div class="col-md-7">
                                <label class="radio-inline">
                                    <input type="radio" name="rbgCanchasSubPanel" id="rdCanchasComplejos" runat="server">
                                    Complejo/s propios del torneo
                                </label>
                                &nbsp;<label class="radio-inline"><input type="radio" name="rbgCanchasSubPanel" id="rdCanchasEquipos" runat="server">
                                    Canchas de los equipos participantes
                                </label>
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="pnlConfigurar" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSiguiente" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="panel-footer clearfix ">
                    <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" OnClick="btnSiguiente_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
