<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="noticias.aspx.cs" Inherits="quegolazo_code.admin.noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upRegistrarNuevoArbitro" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                               <a data-toggle="collapse" data-parent="#accordion" href="#agregarNoticia" aria-expanded="true" aria-controls="agragarNoticia">
                                    <span class="glyphicon glyphicon-plus"></span>
                                    Agregar una Noticia 
                                </a>                              
                            </div>
                            <div id="agregarNoticia" class="panel-collapse collapse in mobile-collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body nopadding-bottom">
                                    <fieldset class="vgNoticia">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="text" class="col-md-2 control-label">Edición</label>
                                                <div class="col-md-10">
                                                    <asp:DropDownList ID="ddlEdicion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <label for="text" class="col-md-2 control-label">Fecha</label>
                                                <div class="col-md-10">
                                                    <input type="text" class="form-control" runat="server" id="txtFecha" placeholder="Fecha" required="true" date="true">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="text" class="col-md-2 control-label">Título</label>
                                                <div class="col-md-10">
                                                    <input type="text" class="form-control" runat="server" id="txtTituloNoticia" placeholder="Título de la Noticia" required="true" rangelength="3, 100">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="select" class="col-md-2 control-label">Tipo Noticia</label>
                                                <div class="col-md-10">
                                                    <select id="ddlTipoNoticia" runat="server" class="form-control">
                                                        <option value="1">Tipo 1</option>
                                                        <option value="2">Tipo 2</option>
                                                        <option value="3">Tipo 3</option>
                                                        <option value="4">Tipo 4</option>
                                                    </select>                                           
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="select" class="col-md-2 control-label">Descripción</label>
                                                <div class="col-md-10">
                                                    <asp:TextBox class="form-control" ID="txtDescripcionNoticia" runat="server" Height="110px" TextMode="MultiLine" Width="341px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                                <div class="panel-footer clearfix text-right">
                                    <div class="mobile-nopadding-left col-xs-10 col-md-8 col-md-offset-3">
                                        <asp:Button class="btn btn-default" ID="btnCancelarModificacionNoticia" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionNoticia_Click"/>
                                        <asp:Button class="btn btn-success causesValidation vgNoticia" ID="btnModificarNoticia" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarNoticia_Click"/>
                                        <asp:Button class="btn btn-success causesValidation vgNoticia" ID="btnRegistrarNoticia" runat="server" Text="Registrar" OnClick="btnRegistrarNoticia_Click"/>
                                    </div>
                                    <div class="col-xs-2 col-ms-1">
                                        <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo">
                                            <ProgressTemplate>
                                                <img src="/resources/img/theme/load4.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
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
                            <div class="col-md-8 col-xs-5">
                                <span class="glyphicon glyphicon-search"></span>
                                Noticias<span class="hidden-xs"> Existentes</span>
                            </div>
                            <div class="col-md-4 col-xs-7">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Noticias"/>
                            </div>
                        </div>                        
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaNoticias" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarNoticia" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarNoticia" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-1 hidden-xs">Fecha</th>
                                            <th class="col-md-1">Título</th>
                                            <th class="col-md-1 hidden-xs">Tipo Noticia</th>
                                            <th class="col-md-2 hidden-xs">Edición</th>
                                            <th class="col-md-1"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptNoticias" runat="server" OnItemCommand="rptNoticias_ItemCommand">
                                            <ItemTemplate>
                                                <tr>                                                    
                                                    <td class="hidden-xs"><%# Eval("fecha") %></td>
                                                    <td><%# Eval("titulo") %></td>
                                                    <td class="hidden-xs"><%# Eval("tipoNoticia") %></td>
                                                    <td class="hidden-xs"><%# Eval("nombre") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarNoticia" title="Editar Noticia" runat="server" CommandName="editarNoticia" CommandArgument='<%#Eval("idNoticia")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarNoticia" title="Eliminar Noticia" runat="server" CommandName="eliminarNoticia" CommandArgument='<%#Eval("idNoticia")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinNoticias" runat="server" visible="false">
                                            <td colspan="5">No hay noticias cargadas</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="panelFracasoListaNoticias" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoListaNoticias" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="eliminarNoticia" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Noticia</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarNoticia" runat="server">
                        <ContentTemplate>
                            <strong>Noticia: </strong>
                            <asp:Literal ID="litTituloNoticia" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Está seguro de eliminar la Noticia?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminar_Click"/>
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
