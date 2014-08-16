<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="canchas.aspx.cs" Inherits="quegolazo_code.admin.canchas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="col-md-6">
            <fieldset class="vgCancha">
            <asp:UpdatePanel ID="upRegistrarNuevaCancha" runat="server">
                <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading">                        
                    <span class="glyphicon glyphicon-plus"></span>
                        Agregar una Cancha                                   
                    </div>
                    <div class="panel-body nopadding-bottom">
                        <div class="form-horizontal">
                            <div class="form-group">                               
                                <label for="text" class="col-lg-2 control-label">Nombre</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" runat="server" id="txtNombreCancha" placeholder="Nombre de la Cancha" required="true">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Domicilio</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtDomicilio" runat="server" placeholder="Domicilio">
                                </div>
                            </div>     
                              <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Telefono</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtTelefono" runat="server" placeholder="Telefono">
                                </div>
                            </div>                         
                            <div class="form-group">
                                <label for="textArea" class="col-lg-2 control-label">Imagen</label>
                                <div class="col-lg-10">
                                    <div class="col-lg-10">
                                       <div class="col-md-5">
                                            <div class="fileinput">
                                                <div class="thumbnail fileinput-preview">
                                                    <img id="imagenpreview" src="../resources/img/theme/logo-default.png" runat="server" />
                                                </div>
                                                <div class="fileUpload">
                                                    <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span></span>
                                                    <asp:FileUpload ID="imagenUpload" runat="server" CssClass="upload" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-7">
                                            <p class="help-block">
                                                <img src="../resources/img/theme/load2.gif" id="loading" style="display:none;" alt="load" />
                                                <span id="resultadoImagen" style="display:none;"><span id="error"></span></span><br />
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
                        <asp:Panel ID="panelExito" runat="server" CssClass="alert alert-success" Visible="False">
                            <asp:Literal ID="litExito" runat="server"></asp:Literal>
                        </asp:Panel>
                        <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
                    </div>                    
                    <div class="panel-footer clearfix text-right">
                        <div class="col-xs-8 col-xs-offset-3">
                            <asp:Button class="btn btn-default" ID="btnCancelarModificacionCancha" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionCancha_Click" />
                            <asp:Button class="btn btn-success causesValidation vgCancha" ID="btnModificarCancha" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarCancha_Click" />
                            <asp:Button class="btn btn-success causesValidation vgCancha" ID="btnRegistrarCancha" runat="server" Text="Registrar" OnClick="btnRegistrarCancha_Click" />
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
            </fieldset>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-search"></span>
                    Canchas Existentes
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="upListaCanchas" runat="server">
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnRegistrarCancha" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnModificarCancha" EventName="Click" />
                        </Triggers> 
                        <ContentTemplate>
                        <table class="table">
                        <thead>
                            <tr>
                                <th class="col-md-2">Nombre</th>
                                <th class="col-md-3">Domicilio</th>
                                <th class="col-md-2">Telefono</th>
                                <th class="col-md-1"></th>
                            </tr>
                        </thead>
                    <tbody>
                        <asp:Repeater ID="rptCanchas" runat="server" OnItemCommand="rptCanchas_ItemCommand" OnItemDataBound="rptCanchas_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("domicilio") %></td>
                                    <td><%# Eval("telefono") %></td>
                                    <td>
                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarCancha" title="Editar Cancha" runat="server" CommandName="editarCancha" CommandArgument='<%#Eval("idCancha")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarCancha" title="Eliminar Cancha" runat="server" CommandName="eliminarCancha" CommandArgument='<%#Eval("idCancha")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr id="sinCanchas" runat="server" visible="false">
                            <td colspan="2">No hay canchas registradas</td>
                        </tr>
                    </tbody>
                    </table>
                    <asp:Panel ID="panelFracasoListaCanchas" runat="server" CssClass="alert alert-danger" Visible="False">
                        <asp:Literal ID="litFracasoListaCanchas" runat="server"></asp:Literal>
                    </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
        });
    </script>
</asp:Content>
