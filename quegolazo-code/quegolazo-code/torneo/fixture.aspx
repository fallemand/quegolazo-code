<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fixture.aspx.cs" Inherits="quegolazo_code.torneo.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <script src="/torneo/js/jquery-ui.min.js"></script>   
    <script src="/torneo/js/jquery.bracket.min.js"></script>
    <link href="/torneo/css/jquery.bracket.min.css" rel="stylesheet" />
    <script src="/torneo/js/widgetLlaves.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <div class="container">
        <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound">
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
                                    <asp:Panel ID="panelTCT" runat="server">
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
                                                                <a data-toggle="collapse" data-parent="#fechas" href="#fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>" style="font-size: 15px;"><%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).tipoFixture.idTipoFixture=="ELIM" ?  Eval("nombre") + " " : "Fecha "+ Eval("idFecha")%>  <small>Ver Más Detalles</small></a>
                                                                <small><span class="label pull-right fecha-<%# ((Entidades.Fecha)Container.DataItem).estado.nombre%>" rel="txtTooltip" title="<%# ((Entidades.Fecha)Container.DataItem).estado.descripcion %>" data-placement="left"><%# ((Entidades.Fecha)Container.DataItem).estado.nombre  %></span></small>
                                                            </h4>
                                                        </div>
                                                        <div id='fase<%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).idFase %>-fecha<%# Eval("idFecha") %>' class="panel-collapse collapse">
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
                                                                                            <%--<asp:LinkButton title="Administrar Partido" ClientIDMode="AutoID" rel="txtTooltip" ID="lnkAdministrarPartido" runat="server" CommandName="administrarPartido" CommandArgument='<%# Eval("idPartido") + ";fase" + ((Entidades.Fase)((((RepeaterItem)Container.Parent.Parent.Parent.Parent)).DataItem)).idFase +"-fecha"+((Entidades.Fecha)((RepeaterItem)Container.Parent.Parent).DataItem).idFecha %>'><span class="glyphicon glyphicon-cog"></span></asp:LinkButton>--%>
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
                                  </asp:Panel>
                                    <asp:Panel ID="panelLlaves" runat="server">
                                        <div id="divLlaves">

                                        </div>
                                    </asp:Panel>
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
           
     <%--<script type="text/javascript">

         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>--%>
          <script type="text/javascript">
              $(document).ready(function ($) {
                  $("#divLlaves").generadorDeLlaves(datosLlaves);
              });
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
              function agrandarLlaves() {
                  $("#divFechas").hide('slow');
                  $("#divLlaves").removeClass('col-md-6', 'slow').addClass('col-md-12');
                  $("#divLlaves").hide();
                  $("#divLlaves").show('slow');
                  $("#btnAgrandar").hide();
                  $("#btnAchicar").show();
              };
              function achicarLlaves() {
                  $("#divFechas").show('slow');
                  $("#divLlaves").removeClass('col-md-12', 'slow').addClass('col-md-6', 800, 'easeOutBounce');
                  $("#divLlaves").hide();
                  $("#divLlaves").show('slow');
                  $("#btnAgrandar").show();
                  $("#btnAchicar").hide();
              };

              
    </script>
        
</asp:Content>
