<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="quegolazo_code.admin.index" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
    <script src="../resources/js/jquery.percentageloader-0.1.js"></script>
     <script src="../../resources/js/jquery.ui/jquery-ui.min.js"></script>
    <script src="../../resources/js/quegolazo.js"></script>
    <script src="../../resources/js/jquery.bracket.min.js"></script>
    <link href="../../resources/css/jquery.bracket.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <!-- Contenido -->
    <asp:Literal ID="LitEdicion" runat="server"></asp:Literal>
    <div class="container padding-top">
    <div class="col-md-12">
            <div class="well">
                <fieldset class="vgSeleccionarEdicion">
                    <div class="col-md-5">
                        <div id="selectEdiciones">
                            <asp:DropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar Edición" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
                    </div>
                </fieldset>
         </div>
        <div class="row">
            <asp:Panel ID="panelEdicionRegistrada" runat="server" CssClass="col-md-7" Enabled="True" Visible="False">
                <div class="alert alert-info">
                    La edición seleccionada se encuentra registrada. Por favor, primero configurela para ver las estadísticas.
                </div>
            </asp:Panel>
        </div>
      </div>        
    </div>
    <asp:Panel ID="panelEstadisticas" runat="server">
    <div class="container padding-top">
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="icon-size flaticon-football31"></span>
                    Tabla de Posiciones
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="btn-group btn-group-sm" role="group" style="margin-right: 5px;" aria-label="...">
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
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table id="tabla-posiciones" class="table table-condensed table-hover">
                                <thead>
                                    <tr>
                                        <th class="col-md-1"></th>
                                        <th class="col-md-4">EQUIPO</th>
                                        <th class="col-md-1">PJ</th>
                                        <th class="col-md-1">PG</th>
                                        <th class="col-md-1">PE</th>
                                        <th class="col-md-1">PP</th>
                                        <th class="col-md-1">GF</th>
                                        <th class="col-md-1">GC</th>
                                        <th class="col-md-1">PTS</th>
                                    </tr>
                                </thead>
                                <tbody class="tablaFiltro">
                                    <asp:Repeater ID="rptPosiciones" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height: 22px; max-width: 30px;" /></td>
                                                <td><%# Eval("Equipo") %></td>
                                                <td><%# Eval("PJ") %></td>
                                                <td><%# Eval("PG") %></td>
                                                <td><%# Eval("PE") %></td>
                                                <td><%# Eval("PP") %></td>
                                                <td><%# Eval("GF") %></td>
                                                <td><%# Eval("GC") %></td>
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
    </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="icon-size flaticon-flaming"></span>
                    Goleadores de la Edición
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-6">JUGADOR</th>
                                <th class="col-md-5">EQUIPO</th>
                                <th class="col-md-1">GOLES</th>
                            </tr>
                        </thead>
                        <tbody class="tablaFiltro">
                            <asp:Repeater ID="rptGoleadores" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("JUGADOR") %></td>
                                        <td><%# Eval("EQUIPO") %></td>
                                        <%--<td><img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /> <%# Eval("EQUIPO") %></td>--%>
                                        <td><%# Eval("GOLES") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr id="sinpartidosGoleadores" runat="server" visible="false">
                                <td colspan="4">Todavia no hay partidos registrados.</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
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
        <div class="col-md-6 ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Fecha 
                    <asp:Literal ID="ltFecha" runat="server" />
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-3">LOCAL</th>
                                <th class="col-md-1">VS</th>
                                <th class="col-md-3">VISITANTE</th>
                                <th class="col-md-2">ARBITRO</th>
                                <th class="col-md-1">ESTADO</th>
                            </tr>
                        </thead>
                        <tbody class="tablaFiltro">
                            <asp:Repeater ID="rptFecha" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("local") %></td>
                                        <td>vs</td>
                                        <td><%# Eval("visitante") %></td>
                                        <td><%# Eval("arbitro") %></td>
                                        <td><%# Eval("estado") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr id="noFixture" runat="server" visible="false">
                                <td colspan="4">Todavia no hay un fixture generado.</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <asp:GridView ID="gvFixture" runat="server" CssClass="table"></asp:GridView>
            </div>
        </div>
    </div>
    </asp:Panel>
    <!-- Contenido -->
    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
    </asp:Panel>
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

    </script>
</asp:Content>
