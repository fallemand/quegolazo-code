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
                            <div class="thumbnail">
                                <img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive center-block" alt="" style="max-height:31px;">
                            </div>
                        </div>
                        <div class="col-md-10">
                            <h4>Edición Que Golazo <small>Torneo Córdoba Gambeta</small></h4>
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
                                    <table>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
                                        <tr class="col-md-4">
                                            <td><img src="http://pngimg.com/upload/car_logo_PNG1667.png" class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;" /></td>
                                            <td>Boca Juniors</td>
                                        </tr>
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
                                    Registra Arbitros: <span class="text-success"><b><span class="glyphicon glyphicon-ok"></span></b></span><br />
                                    Registra Arbitros: <span class="text-danger"><b><span class="glyphicon glyphicon-remove"></span></b></span><br />
                                    Registra Arbitros: <span class="text-danger"><b><span class="glyphicon glyphicon-remove"></span></b></span><br />
                                    Registra Arbitros: <span class="text-success"><b><span class="glyphicon glyphicon-ok"></span></b></span><br />
                                </div>
                            </div>
                        </div>
                </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-group" id="fases">
                      <div class="panel panel-default">
                        <div class="panel-heading">
                          <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#fases" href="#collapseOne">
                              Fase 1 <small>Ver Más Detalles</small>
                            </a>
                          </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in">
                          <div class="panel-body">
                              <div class="col-md-6">
                                <b>Tipo Fixture:</b> Todos Contro Todos <br />
                                <b>Cantidad de Grupos:</b> 2 <br />
                                <b>Cantidad de Equipos:</b> 8 <br />
                                <b>Cantidad de Equipos por Grupo:</b> 4 <br />
                              </div>
                              <div class="col-md-6">
                                <b>Cantidad de Fechas:</b> 8 <br />
                                <b>Partidos por Fecha:</b> 4 <br />
                              </div>
                          </div>
                        </div>
                      </div>
                      <div class="panel panel-default">
                        <div class="panel-heading">
                          <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#fases" href="#collapseTwo">
                              Fase 2 <small>Ver Más Detalles</small>
                            </a>
                          </h4>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse">
                          <div class="panel-body">
                            <b>Tipo Fixture:</b> Todos Contro Todos <br />
                            <b>Cantidad de Grupos:</b> 2 <br />
                            <b>Cantidad de Equipos:</b> 8 <br />
                            <b>Cantidad de Equipos por Grupo:</b> 4 <br />
                          </div>
                        </div>
                      </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
            </asp:Panel>
        </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Configuración" CssClass="btn btn-success pull-right"/>
        </div>
    </div>
</asp:Content>
