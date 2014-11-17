<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="confirmar.aspx.cs" Inherits="quegolazo_code.admin.edicion.confirmar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderEdicion" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentEdicion" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading panel-heading-master">
            <span class="glyphicon glyphicon-cog"></span>
            Confirmar Configuración Edición
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="well well-sm clearfix">
                        <div class="col-md-2">
                            <div class="thumbnail"><img src="<%= Logica.Sesion.getTorneo().obtenerImagenChicha() %>" class="img-responsive center-block" alt="" style="max-height:31px; " />
                            </div>
                        </div>
                        <div class="col-md-10">
                            <h4><asp:Literal ID="LitEdicion" runat="server"></asp:Literal><small> <%= Logica.Sesion.getTorneo().nombre %></small></h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                    <div class="col-md-8">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="flaticon-soccer18" style="font-size: 18px;line-height: 12px;"></span>
                                    Equipos Participantes
                                </div>
                                <div class="panel-body small-padding">
                                    <table style="width:100%;">
                                         <asp:Repeater ID="rptEquipos" runat="server">
                                                        
                                             <ItemTemplate>
                                                         <tr class="col-md-4">
                                                            <td><img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>"  class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;"  /></td>
                                                            <td><%# Eval("nombre") %></td> 
                                                        </tr>
                                           </ItemTemplate>
                                                    </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                    </div>
                    <div class="col-md-4">
                        <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="glyphicon glyphicon-cog"></span>
                                    Opciones
                                </div>
                                <div class="panel-body">
                                    Registra Jugadores: <span class="text-success"><b><span class="glyphicon glyphicon-ok" runat="server" visible="false" id="rJugadoresSi"></span></b></span>
                                                        <span class="text-danger"><b><span class="glyphicon glyphicon-remove" runat="server" visible="false" id="rJugadoresNo"></span></b></span><br />
                                    Registra Arbitros:  <span class="text-success"><b><span class="glyphicon glyphicon-ok" runat="server" visible="false" id="rArbitrosSi"></span></b></span>
                                                        <span class="text-danger"><b><span  class="glyphicon glyphicon-remove" runat="server" visible="false" id="rArbitrosNo" ></span></b></span><br />
                                    Registra Sanciones: <span class="text-success"><b><span  class="glyphicon glyphicon-ok" runat="server" visible="false" id="rSancionesSi"></span></b></span>
                                                        <span class="text-danger"><b><span  class="glyphicon glyphicon-remove" runat="server" visible="false" id="rSancionesNo"></span></b></span><br />
                                    Registra Canchas:   <span class="text-success"><b><span  class="glyphicon glyphicon-ok" runat="server" visible="false" id="rCanchasSi"></span></b></span>
                                                        <span class="text-danger"><b><span  class="glyphicon glyphicon-remove" runat="server" visible="false" id="rCanchasNo"></span></b></span><br />
                                </div>
                            </div>
                        </div>
                </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-group" id="fases">
                        <asp:Repeater  ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound">
                            <ItemTemplate>
                                 <div class="panel panel-default">
                        <div class="panel-heading">
                          <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#fases" href="#collapseOne">
                              Fase  <%# Eval("idFase") %> <small>Ver Más Detalles</small>
                            </a>
                          </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in">
                          <div class="panel-body">
                              <div class="col-md-6">
                                <b>Tipo Fixture:</b> <%# Eval("TipoFixture.nombre") %><br />
                                <b>Cantidad de Grupos:</b><asp:Literal ID="LitCantidadGrupos" runat="server"></asp:Literal><br />
                                <b>Cantidad de Equipos:</b> <asp:Literal ID="litCantidadEquipos" runat="server"></asp:Literal><br />
                                <b>Cantidad de Equipos por Grupo:</b> <asp:Literal ID="litCantidadEquiposGrupo" runat="server" ></asp:Literal> <br />
                              </div>
                              <div class="col-md-6">
                               <b>Cantidad de Fechas:</b><asp:Literal ID="LitCantidadFechas" runat="server"></asp:Literal> <br />
                               <%--<b>Partidos por Fecha:</b> <asp:Literal ID="LitPartidosPorFecha" runat="server"></asp:Literal><br />--%>
                              </div>
                          </div>
                        </div>
                      </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="pnlConfigurar" runat="server">
                 <Triggers>
                    <asp:AsyncPostBackTrigger controlid="btnRegistrar" eventname="Click" />
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
            <asp:Button ID="btnAtras" runat="server" Text="Atrás" CssClass="btn btn-success pull-left" OnClick="btnAtras_Click"/>
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Configuración" OnClick="btnRegistrar_Click" CssClass="btn btn-success pull-right"/>
        </div>
              </ContentTemplate>
             </asp:UpdatePanel>
    </div>
</asp:Content>
