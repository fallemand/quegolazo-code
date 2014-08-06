<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.equipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="col-md-6">
            <fieldset class="vgEquipo">
                <div class="panel panel-default">
                    <div class="panel-heading">                        
                        <asp:LinkButton ID="lnkNuevoEquipo" Text="Agregar un Equipo" title="Nuevo Equipo" runat="server" OnClick="lnkNuevoEquipo_Click"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>     
                        Agregar un Equipo                                   
                    </div>
                    <div class="panel-body nopadding-bottom">
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
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color primario de la camiseta" id="txtColorPrimario" runat="server" value="#E1E1E1" required="true">
                                </div>
                                <label for="text" class="col-lg-2 control-label">Color 2°</label>
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color secundario de la camiseta" id="txtColorSecundario" runat="server" value="#E1E1E1" required="true">
                                </div>
                            </div>
                            <div class="subform-horizontal clearfix">
                                <label for="select" class="col-lg-2 control-label">Delegados</label>
                                <div class="col-lg-10">
                                    <p class="nomargin-bottom"> 
                                        <span class="label label-default label-md">
                                            <a href="" rel="txtTooltip" title="Eliminar" onclick="showDelegados();return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Nuevo</a>
                                        </span>
                                    <asp:Repeater ID="rptDelegados" runat="server" OnItemCommand="rptDelegados_ItemCommand">
                                        <ItemTemplate>
                                            <span class="label label-default label-md"><%# Eval("nombre") %>
                                                <asp:LinkButton rel="txtTooltip" ID="lnkEliminar" runat="server" CommandName="eliminarDelegado" CommandArgument='<%# Eval("nombre") %>'><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                <asp:LinkButton rel="txtTooltip" ID="lnkModificar" runat="server" CommandName="modificarDelegado" CommandArgument='<%# Eval("nombre") %>'><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        </p>
                                        <div id="delegado" style="display:none;" class="col-md-9">
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
                                                <asp:Button class="btn btn-default btn-xs causesValidation vgDelegado" ID="btnAgregarDelegado" runat="server" Text="Agregar Delegado" OnClick="btnAgregarDelegado_Click"/>
                                                <asp:Button class="btn btn-default btn-xs causesValidation vgDelegado" ID="btnModificarDelegado" runat="server" Text="Modificar Delegado" OnClick="btnModificarDelegado_Click" Visible="false"/>
                                                <asp:Button class="btn btn-default btn-xs" ID="btnCancelarDelegado" runat="server" Text="Cancelar" OnClick="btnCancelarDelegado_Click" Visible="false"/>
                                            </fieldset>
                                        </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                <div class="col-lg-10">
                                    <div class="col-md-5">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="fileinput-new thumbnail">
                                                <img id="imagenpreview" src="../resources/img/theme/logo-default.png" alt="..." runat ="server">
                                            </div>
                                            <div class="fileinput-preview fileinput-exists thumbnail"></div>
                                            <div>
                                                <span class="btn btn-default btn-xs btn-file">
                                                    <span class="fileinput-new">Seleccionar Imagen</span>
                                                    <span class="fileinput-exists">Cambiar</span>
                                                    <asp:FileUpload ID="fuLog" runat="server" />
                                                </span>
                                                <a href="#" class="btn btn-default btn-xs fileinput-exists" data-dismiss="fileinput">Eliminar</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-7">
                                        <p class="help-block" style="margin-top: 15px;">
                                            <strong>Formato admitido</strong><br />
                                            PNG, JPEG, JPG, GIF<br />
                                            <strong>Tamaño Máximo</strong><br />
                                            512kb<br />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="panelExito" runat="server" CssClass="alert alert-success" Visible="False">
                            <asp:Literal ID="litExito" runat="server"></asp:Literal>
                        </asp:Panel>
                        <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
                    </div>
                    <div class="panel-footer clearfix">
                        <asp:Button class="btn btn-success pull-right causesValidation vgEquipo" ID="btnRegistrarEquipo" runat="server" Text="Registrar" OnClick="btnRegistrarEquipo_Click" />
                        <asp:Button class="btn btn-success" ID="btnCancelarModificacionEquipo" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionEquipo_Click"/>
                        <asp:Button class="btn btn-success causesValidation vgEquipo" ID="btnModificarEquipo" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarEquipo_Click"/>                        
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-search"></span>
                    Equipos Existentes
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-4">Nombre</th>
                            </tr>
                        </thead>
                    <tbody>
                        <asp:Repeater ID="rptEquipos" runat="server" OnItemCommand="rptEquipos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nombre") %></td>
                                    <td>
                                        <asp:LinkButton ID="lnkEditarEquipo" title="Editar Equipo" runat="server" CommandName="editarEquipo" CommandArgument='<%#Eval("idEquipo")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                        <a href="#" class="editar"><span class="glyphicon glyphicon-remove eliminar" rel="txtTooltip" title="Eliminar Equipo" data-toggle="tooltip" data-placement="top"></span></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    </table>
                    <asp:Label ID="lblMensajeEquipos" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            $('#ContentAdmin_ContentAdminTorneo_txtColorPrimario').colorPicker();
            $('#ContentAdmin_ContentAdminTorneo_txtColorSecundario').colorPicker();
        });
        function showDelegados() {
            $('#delegado').toggle("slow", function fildsetActivator() {
                if ($('#delegado').is(":visible"))
                    $('#delegado').find('input').prop('disabled', false);
                else 
                    $('#delegado').find('input').attr('disabled', 'disabled');
            });
        };
    </script>
</asp:Content>
