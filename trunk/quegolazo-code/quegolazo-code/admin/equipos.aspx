<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:UpdatePanel ID="upRegistrarEquipo" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Agregar un Equipo                                   
                            </div>
                            <div class="panel-body nopadding-bottom">
                                <fieldset class="vgEquipo">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" runat="server" id="txtNombreEquipo" placeholder="Nombre del Equipo" required="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Director</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtNombreDirector" runat="server" placeholder="Nombre del Director Técnico">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Color °1</label>
                                            <div class="col-lg-2">
                                                <div class="colorpick">
                                                    <input type="text" class="form-control" rel="txtTooltip" title="Color primario de la camiseta" id="txtColorPrimario" runat="server" value="#E1E1E1" required="true">
                                                </div>
                                            </div>
                                            <label for="text" class="col-lg-2 control-label">Color 2°</label>
                                            <div class="col-lg-2">
                                                <div class="colorpick">
                                                    <input type="text" class="form-control" rel="txtTooltip" title="Color secundario de la camiseta" id="txtColorSecundario" runat="server" value="#E1E1E1" required="true">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="subform-horizontal clearfix">
                                            <label for="select" class="col-lg-2 control-label">Delegados</label>
                                            <div class="col-lg-10">
                                                <p class="nomargin-bottom">
                                                    <span class="label label-default label-md">
                                                        <a href="" rel="txtTooltip" title="Agregar Delegado" onclick="showDelegados();return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Nuevo</a>
                                                    </span>
                                                    <asp:Repeater ID="rptDelegados" runat="server" OnItemCommand="rptDelegados_ItemCommand">
                                                        <ItemTemplate>
                                                            <span class="label label-default label-md"><%# Eval("nombre") %>
                                                                <asp:LinkButton ClientIDMode="AutoID" title="Eliminar" rel="txtTooltip" ID="lnkEliminar" runat="server" CommandName="eliminarDelegado" CommandArgument='<%# Eval("nombre") %>'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                                <asp:LinkButton ClientIDMode="AutoID" title="Modificar" rel="txtTooltip" ID="lnkModificar" runat="server" CommandName="modificarDelegado" CommandArgument='<%# Eval("nombre") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                                <div id="delegado" style="display: none;" class="col-md-9">
                                                    <fieldset class="vgDelegado">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-user"></i></span>
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtNombreDelegado" placeholder="Nombre del Delegado" runat="server" required="true" disabled>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-envelope"></i></span>
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtEmailDelegado" placeholder="Email del Delegado" runat="server" required="true" email="true" disabled>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-phone"></i></span>
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtTelefonoDelegado" placeholder="Teléfono del Delegado" runat="server" required="true" number="true" disabled>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-home"></i></span>
                                                                <input type="text" class="form-control margin-xs input-sm" id="txtDireccionDelegado" placeholder="Dirección del Delegado" runat="server" disabled>
                                                            </div>
                                                        </div>
                                                        <asp:Button class="btn btn-default btn-xs causesValidation vgDelegado" ID="btnAgregarDelegado" runat="server" Text="Agregar Delegado" OnClick="btnAgregarDelegado_Click" />
                                                        <asp:Button class="btn btn-default btn-xs causesValidation vgDelegado" ID="btnModificarDelegado" runat="server" Text="Modificar Delegado" OnClick="btnModificarDelegado_Click" Visible="false" />
                                                        <asp:Button class="btn btn-default btn-xs" ID="btnCancelarDelegado" runat="server" Text="Cancelar" OnClick="btnCancelarDelegado_Click" />
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                            <div class="col-lg-10">
                                                <div class="row">
                                                <div class="col-md-5">
                                                    <div class="fileinput">
                                                        <div class="thumbnail fileinput-preview">
                                                            <img id="imagenpreview" runat="server" />
                                                        </div>
                                                        <div class="fileUpload">
                                                            <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span></span>
                                                            <asp:FileUpload ID="imagenUpload" runat="server" CssClass="upload" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-7">
                                                    <img src="../resources/img/theme/load2.gif" id="cargandoImagen" style="display: none;" alt="load" />
                                                    <span id="imagenCorrecta" class="label alert-success label-md" style="display: none;">Imagen Correcta <span class="glyphicon glyphicon-ok"></span></span>
                                                    <span id="imagenIncorrecta" class="label alert-danger label-md" style="display: none;"><span id="mensajeErrorImagen"></span></span>
                                                    <p class="help-block">
                                                        <strong>Formato admitido</strong><br />
                                                        PNG, JPEG, JPG, GIF<br />
                                                        <strong>Tamaño Máximo</strong><br />
                                                        1 Mb
                                                    </p>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <asp:Panel ID="panelExito" runat="server" CssClass="alert alert-success" Visible="False">
                                    <asp:Literal ID="litExito" runat="server"></asp:Literal>
                                </asp:Panel>
                                <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionEquipo" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionEquipo_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgEquipo" ID="btnModificarEquipo" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarEquipo_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgEquipo" ID="btnRegistrarEquipo" runat="server" Text="Registrar" OnClick="btnRegistrarEquipo_Click" />
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
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-search"></span>
                                Equipos Existentes
                            </div>
                            <div class="col-md-4">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar por Nombre"/>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaEquipos" runat="server" class="listado">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarEquipo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarEquipo" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-1"></th>
                                            <th class="col-md-9">Nombre</th>
                                            <th class="col-md-2"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptEquipos" runat="server" OnItemCommand="rptEquipos_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                    <td><%# Eval("nombre") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarEquipo" title="Editar Equipo" runat="server" CommandName="editarEquipo" CommandArgument='<%#Eval("idEquipo")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarEquipo" title="Eliminar Equipo" runat="server" CommandName="eliminarEquipo" CommandArgument='<%#Eval("idEquipo")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar"></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinequipos" runat="server" visible="false">
                                            <td colspan="2">No hay equipos registrados</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="panelFracasoListaEquipos" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoListaEquipos" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="eliminarEquipo" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Equipo</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarEquipo" runat="server">
                        <ContentTemplate>
                            <strong>Equipo: </strong>
                            <asp:Literal ID="litNombreEquipo" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Esta seguro de eliminar el equipo?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminar_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            loadColorPicker();
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
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                loadColorPicker();
            }
        });
        function showDelegados() {
            $('#delegado').toggle("slow", function fildsetActivator() {
                if ($('#delegado').is(":visible"))
                    $('#delegado').find('input').prop('disabled', false);
                else
                    $('#delegado').find('input').attr('disabled', 'disabled');
            });
        };
        function loadColorPicker() {
            $('#ContentAdmin_ContentAdminTorneo_txtColorPrimario').colorPicker();
            $('#ContentAdmin_ContentAdminTorneo_txtColorSecundario').colorPicker();
        };
    </script>
</asp:Content>
