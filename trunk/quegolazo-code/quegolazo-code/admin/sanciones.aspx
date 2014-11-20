<%@ Page Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="sanciones.aspx.cs" Inherits="quegolazo_code.admin.sanciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upRegistrarNuevaSancion" runat="server">
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
                                    <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar Edición" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click"/>
                                </div>
                            </fieldset>
                        </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Agregar una Sanción                                  
                            </div>
                            <div class="panel-body nopadding-bottom">                                
                                <fieldset class="vgSancion">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Fecha</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlFecha" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFecha_SelectedIndexChanged" ></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Partido</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlPartido" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPartido_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Equipo</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlEquipo" runat="server" CssClass="form-control" required="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEquipo_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Jugador</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlJugador" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Fecha</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFecha" runat="server" placeholder="EJ: 20/11/2014" required="true">
                                            </div>
                                        </div> 
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Motivo</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="form-control" required="true"></asp:DropDownList>
                                            </div>
                                        </div>     
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Observación</label>
                                            <div class="col-lg-10">
                                                <textarea class="form-control" id="txtObservacion" runat="server" maxlength="400" rows="3"></textarea>
                                            </div>
                                        </div>   
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Puntos a Quitar</label>
                                            <div class="col-lg-10">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtPuntosAQuitar" rel="txtTooltip" title="Puntos a Quitar" maxlength="2" min="0" digits="true">
                                            </div>
                                        </div> 
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Cantidad Fechas a Suspender</label>
                                            <div class="col-lg-10">
                                                <input type="number" class="form-control text-center" runat="server" data-container="body" id="txtCantidadFechasSuspendidas" rel="txtTooltip" title="Cantidad Fechas a Suspender" maxlength="2" min="0" digits="true">
                                            </div>
                                        </div>      
                                    </div>
                                </fieldset>
                                <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionArbitro" runat="server" Text="Cancelar" Visible="false" />
                                    <asp:Button class="btn btn-success causesValidation vgArbitro" ID="btnModificarArbitro" runat="server" Text="Modificar" Visible="false" />
                                    <asp:Button class="btn btn-success causesValidation vgArbitro" ID="btnRegistrarArbitro" runat="server" Text="Registrar" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-7">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-search"></span>
                                Sanciones Existentes
                            </div>
                            <div class="col-md-4">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Sanciones"/>
                            </div>
                        </div>                        
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaSanciones" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarArbitro" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarArbitro" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-2">Fecha</th>
                                            <th class="col-md-1">Equipo</th>
                                            <th class="col-md-1">Jugador</th>
                                            <th class="col-md-1">Motivo</th>
                                            <th class="col-md-1">Observación</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptSanciones" runat="server">
                                            <ItemTemplate>
                                                <tr>                                                    
                                                    <td><%# Eval("idFecha") %></td>
                                                    <td><%# Eval("nombreEquipo") %></td>
                                                    <td><%# Eval("nombreJugador") %></td>
                                                    <td><%# Eval("motivo") %></td>
                                                    <td><%# Eval("observacion") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarArbitro" title="Editar Arbitro" runat="server" rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarArbitro" title="Eliminar Arbitro" runat="server" rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinArbitros" runat="server" visible="false">
                                            <td colspan="4">No hay sanciones registradas</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="panelFracasoListaSanciones" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoListaSanciones" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="eliminarSancion" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Sanción</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarSancion" runat="server">
                        <ContentTemplate>
                            <strong>Sanción: </strong>
                            <asp:Literal ID="litSancion" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Está seguro de eliminar la sanción?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar"/>
                </div>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            $('#ContentAdmin_ContentAdminTorneo_txtColorPrimario').colorPicker();
            $('#ContentAdmin_ContentAdminTorneo_txtColorSecundario').colorPicker();
            $('body').on('change', '#ContentAdmin_ContentAdminTorneo_imagenUpload', function () {
                previewImage(this, 'ContentAdmin_ContentAdminTorneo_imagenpreview');
                ajaxFileUpload('ContentAdmin_ContentAdminTorneo_imagenUpload');
            });
            $('body').on('keyup', '#filtro', function () {
                var rex = new RegExp($(this).val(), 'i');
                $('.tablaFiltro tr').hide();
                $('.tablaFiltro tr').filter(function () {
                    return rex.test($(this).text());
                }).show();
            });
        });
    </script>
</asp:Content>
