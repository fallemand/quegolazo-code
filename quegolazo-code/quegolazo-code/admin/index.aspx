<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="quegolazo_code.admin.index" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
    <script type="text/javascript" src="<%=Logica.GestorUrl.rJS %>/jquery.percentageloader-0.1.js"></script>
        <script src="/resources/js/jquery.ui/jquery-ui.min.js"></script>
    <script src="/resources/js/quegolazo.js"></script>
    <script src="/resources/js/jquery.bracket.min.js"></script>
    <link href="/resources/css/jquery.bracket.min.css" rel="stylesheet" />
    <script src="/resources/js/widgetLlaves.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <!-- Contenido -->
    <div class="container">
    <asp:Literal ID="LitEdicion" runat="server"></asp:Literal>
    <div class="row">
    <div class="col-md-12">
            <div class="well">
                <fieldset class="vgSeleccionarEdicion">
                    <div class="col-md-5 col-xs-9 mobile-nopadding-left">
                        <div id="selectEdiciones">
                            <aspNew:NewDropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true" ViewStateMode="Enabled"></aspNew:NewDropDownList>
                        </div>
                    </div>
                    <div class="col-md-2 col-xs-3 mobile-nopadding-left">
                        <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
                    </div>
                </fieldset>
         </div>
        <div class="row">
            <asp:Panel ID="panelEdicionRegistrada" runat="server" CssClass="col-md-7" Enabled="True" Visible="False">
                <div class="alert alert-info">
                    Debe configurar la edición para poder ver las estadísticas.
                </div>
            </asp:Panel>
            <asp:Panel ID="panelSinEdiciones" runat="server" CssClass="col-md-7" Enabled="True" Visible="False">
                <div class="alert alert-info">
                    Este torneo no cuenta con ediciones. Puede crear una ingresando <a href="<%=Logica.GestorUrl.aEDICIONES %>"><b>Aquí</b></a>
                </div>
            </asp:Panel>
        </div>
      </div>        
    </div>
    <asp:Panel ID="panelEstadisticas" runat="server">
    <div class="row">
        <div class="col-md-6">
            <div id="panelPosiciones" runat="server" class="panel panel-default">
                <div class="panel-heading">
                    <span class="flaticon-sports24"></span>
                    Tabla de Posiciones
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            Grupos:
                            <div class="btn-group btn-group-sm" role="group" aria-label="...">
                                <button type="button" style="margin-right: 5px;" class="btn btn-success btn-sm" onclick="$('#tabla-posiciones tr').show('fast');">Todos</button>
                                <asp:Repeater ID="rptGrupos" runat="server">
                                    <ItemTemplate>
                                        <button type="button" class="btn btn-success" onclick="filtrarPosiciones('<%# Eval("idGrupo")%>')"><span class="hidden-xs">Grupo</span> <%# Eval("idGrupo")%></button>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 listado listado-sm">
                            <table id="tabla-posiciones" class="table table-condensed table-hover">
                                <thead>
                                    <tr>
                                        <th class="col-md-1"></th>
                                        <th class="col-md-4">Equipo</th>
                                        <th class="col-md-1">PJ</th>
                                        <th class="col-md-1 hidden-xs">PG</th>
                                        <th class="col-md-1 hidden-xs">PE</th>
                                        <th class="col-md-1 hidden-xs">PP</th>
                                        <th class="col-md-1 hidden-xs">GF</th>
                                        <th class="col-md-1 hidden-xs">GC</th>
                                        <th class="col-md-1">PTS</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptPosiciones" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                                <td><%# Eval("Equipo") %></td>
                                                <td><%# Eval("PJ") %></td>
                                                <td class="hidden-xs"><%# Eval("PG") %></td>
                                                <td class="hidden-xs"><%# Eval("PE") %></td>
                                                <td class="hidden-xs"><%# Eval("PP") %></td>
                                                <td class="hidden-xs"><%# Eval("GF") %></td>
                                                <td class="hidden-xs"><%# Eval("GC") %></td>
                                                <td><%# Eval("Puntos") %></td>
                                                <td class="idEquipo" style="display:none;"><%# Eval("idEquipo") %></td>
                                                <td style="display:none;"><%# Eval("idGrupo") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr id="sinequipos" runat="server" visible="false">
                                        <td colspan="12">Todavia no hay partidos registrados.</td>
                                    </tr>                                    
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
            <div id="panelLlaves" runat="server" class="panel panel-default">
                <div class="panel-heading">
                    <span class="flaticon-sports24"></span>
                    Tabla de Posiciones
                </div>
                <div class="panel-body" id="containerLlaves">

                </div>

            </div>
        </div>
        <div class="col-md-6 ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Fecha 
                    <asp:Literal ID="ltFecha" runat="server" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="btn-group btn-group-sm" role="group" style="margin-right: 5px;" aria-label="...">
                                <button type="button" class="btn btn-success btn-sm" onclick="$('#tabla-fechas tr').show('fast');">Todos</button>
                            </div>
                            <div class="btn-group btn-group-sm" role="group" aria-label="...">
                                <asp:Repeater ID="rptGruposFecha" runat="server">
                                    <ItemTemplate>
                                        <button type="button" class="btn btn-success" onclick="filtrarFechasPorGrupo('<%# Eval("idGrupo")%>')">Grupo <%# Eval("idGrupo")%></button>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 listado listado-sm">
                            <table id="tabla-fechas" class="table nomargin-bottom">
                            <thead>
                                <tr>
                                    <th class="col-md-2">Local</th>
                                    <th class="col-md-1">VS</th>
                                    <th class="col-md-2">Visitante</th>
                                    <th class="col-md-1 hidden-xs">Fecha</th>
                                    <th class="col-md-1">Estado</th>
                                </tr>
                            </thead>
                            <tbody class="tablaFiltro">
                                <asp:Repeater ID="rptFecha" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Local") %></td>
                                            <td><%# Eval("GolesLocal") %> - <%# Eval("GolesVisitante") %></td>
                                            <td><%# Eval("Visitante") %></td>                                        
                                            <td class="hidden-xs"><%# Eval("FechaPartido") %></td>
                                            <td>
                                                <span class="label partido-<%# Eval("estado") %> visible-xs" title="<%# Eval("estado") %>" rel="txtTooltip" data-placement="left"><%# Eval("estado").ToString().PadRight(1).Substring(0,1).TrimEnd() %></span>
                                                <span class="label partido-<%# Eval("estado") %> hidden-xs"><%# Eval("estado") %></span>
                                            </td>
                                            <td style="display:none;"><%# Eval("Grupo") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="noFixture" runat="server" visible="false">
                                    <td colspan="4">Todavia no hay un fixture generado.</td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvFixture" runat="server" CssClass="table"></asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Avance de la Edición
                </div>
                <div class="panel-body">
                    <div id="avanceTorneo"></div>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Avance de la Fecha 
                </div>
                <div class="panel-body">
                    <div id="avanceFecha"></div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="flaticon-football116"></span>
                    Goleadores de la Edición
                </div>
                <div class="panel-body">
                    <div class="listado listado-xs">
                        <table class="table nomargin-bottom">
                            <thead>
                                <tr>
                                    <th class="col-md-1"></th>
                                    <th class="col-md-3">Jugador</th>
                                    <th class="col-md-1 hidden-xs"></th>
                                    <th class="col-md-3">Equipo</th>
                                    <th class="col-md-2">Goles</th>
                                </tr>
                            </thead>
                            <tbody class="tablaFiltro">
                                <asp:Repeater ID="rptGoleadores" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("IDJUGADOR").ToString()), Utils.GestorImagen.JUGADOR, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                            <td><%# Eval("JUGADOR") %></td>
                                            <td class="hidden-xs"><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("IDEQUIPO").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                            <td>
                                                <span class="hidden-xs"><%# Eval("EQUIPO") %></span>
                                                <img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" title="<%# Eval("EQUIPO") %>" rel="txtTooltip" data-placement="left" />
                                            </td>                                       
                                            <td><%# Eval("GOLES") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="sinpartidosGoleadores" runat="server" visible="false">
                                    <td colspan="5"><asp:Literal ID="litSinGoleadores" runat="server"></asp:Literal></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
    </div>
    <!-- Contenido -->
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

         function filtrarPosiciones(idGrupo) {
             $('#tabla-posiciones tbody tr').hide();
             $('#tabla-posiciones tbody tr').filter(function () {
                 return $(this).find('td:last-child').text() == idGrupo;
             }).show('fast');
         };

         function filtrarFechasPorGrupo(Grupo) {
             $('#tabla-fechas tbody tr').hide();
             $('#tabla-fechas tbody tr').filter(function () {
                 return $(this).find('td:last-child').text() == Grupo;
             }).show('fast');
         };
         
    </script>
</asp:Content>
