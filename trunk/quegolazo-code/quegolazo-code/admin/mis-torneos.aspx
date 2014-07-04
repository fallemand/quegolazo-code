<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="mis-torneos.aspx.cs" Inherits="quegolazo_code.admin.mis_torneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container padding-top">
        <div class="row">
            <div class="container">
                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#registrarTorneo">Crear un Nuevo Torneo</button>
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
                                                <img src="/imagenes/torneos/<%# Eval("idTorneo") %>.png" />
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <h3><%# Eval("nombre") %></h3>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="pull-right botones">
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Ver Sitio Web del Torneo"><span class="glyphicon glyphicon-globe"></span></a>
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Editar Torneo"><span class="glyphicon glyphicon-pencil"></span></a>
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
                                                    <th class="col-md-1">Número</th>
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
                                                            <td><%# Eval("idEdicion") %></td>
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
                        <h4 class="modal-title" id="myModalLabel"><i class="flaticon-trophy5"></i>Registrar Nuevo Torneo</h4>
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
                                        <input type="text" class="form-control" id="txtNombreTorneo" runat="server" name="nombreTorneo" minlength="3" maxlength="60" required="true" placeholder="Nombre del Nuevo Torneo">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">URL</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" name="urlTorneo" id="txtUrlTorneo" runat="server" nospace="true" minlength="3" maxlength="60" required="true" placeholder="url-del-torneo">
                                        <span class="help-block">Nombre de la url del torneo. No podrá cambiarlo. www.quegolazo.com/<b>url-del-torneo</b></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Descripción</label>
                                    <div class="col-lg-10">
                                        <textarea class="form-control" id="txtDescripcion" runat="server" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-preview thumbnail" data-trigger="fileinput"></div>
                                                <div>
                                                    <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span><span class="fileinput-exists">Cambiar</span><asp:FileUpload ID="imagenUpload" runat="server" /></span>
                                                    <a href="#" class="btn btn-default btn-xs fileinput-exists" data-dismiss="fileinput">Eliminar</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <p class="help-block" style="margin-top: 15px;">
                                                <strong>Formato admitido</strong><br />
                                                PNG, JPEG, JPG, GIF<br />
                                                <strong>Tamaño Máximo</strong><br />
                                                512kb<br />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="panFracasoTorneo" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoTorneo" runat="server"></asp:Literal>
                                </asp:Panel>
                                </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnResgitrarTorneo" runat="server" CssClass="btn btn-success causesValidation" data-toggle="modal" data-target="#registrarTorneo" Text="Registrar" OnClick="btnResgitrar_Click" />
                            <asp:UpdateProgress runat="server" class="updateProgressInline" ID="PageUpdateProgress">
                                <ProgressTemplate>
                                    <img src="/resources/img/theme/load3.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
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
                                                <input type="text" class="form-control" id="txtPuntosPorGanar" runat="server" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3">
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <span class="input-group-addon">=</span>
                                                <input type="text" class="form-control" id="txtPuntosPorEmpatar" runat="server" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1">
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                                <input type="text" class="form-control" id="txtPuntosPorPerder" runat="server" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnRegistrarEdicion" runat="server" Text="Registrar" class="btn btn-success causesValidation" OnClick="btnRegistrarEdicion_Click" />
                        <asp:UpdateProgress runat="server" class="updateProgressInline" ID="UpdateProgress1">
                                <ProgressTemplate>
                                    <img src="/resources/img/theme/load3.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
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
        function closeModalTorneo() {
            $('#registrarTorneo').modal('hide');
            $('#form1').resetForm();
        }
        function closeModalEdicion() {
            $('#agregarEdicion2').modal('hide');
            $('#form1').resetForm();
        }
        function openModalEdicion() {
            $('#agregarEdicion2').modal('show');
        }
    </script>
</asp:Content>
