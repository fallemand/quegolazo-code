<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="noticias.aspx.cs" Inherits="quegolazo_code.admin.noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <fieldset class="vgSeleccionarEdicion">
                        <div class="col-md-5 col-xs-9 mobile-nopadding-left">
                            <div id="selectEdiciones">
                                <aspNew:NewDropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true" ViewStateMode="Enabled"></aspNew:NewDropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-3 mobile-nopadding-left">
                            <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
                        </div>
                    </fieldset>
                </div>
                <div class="row">
                    <asp:Panel ID="panelSinEdiciones" runat="server" CssClass="col-md-7" Enabled="True" Visible="False">
                        <div class="alert alert-info">
                            Este torneo no cuenta con ediciones. Puede crear una ingresando <a href="<%=Logica.GestorUrl.aEDICIONES %>"><b>Aquí</b></a>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
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
                                                <label for="text" class="col-md-2 control-label">Título</label>
                                                <div class="col-md-10">
                                                    <input type="text" class="form-control" runat="server" id="txtTituloNoticia" placeholder="Título de la Noticia" required="true" rangelength="3, 100">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="select" class="col-md-2 control-label">Cuerpo</label>
                                                <div class="col-md-10">
                                                    <asp:TextBox class="form-control" ID="txtDescripcionNoticia" runat="server" Height="110px" TextMode="MultiLine" Width="341px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                            <label for="textArea" class="col-lg-2 control-label">Imagen</label>
                                            <div class="col-lg-10">
                                                <div class="row">
                                                    <div class="col-md-5 col-xs-6">
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
                                                    <div class="col-md-7 col-xs-6">
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
                                    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                                <div class="panel-footer clearfix text-right">
                                    <div class="mobile-nopadding-left col-xs-10 col-md-8 col-md-offset-3">
                                        <asp:Button class="btn btn-default" ID="btnCancelarModificacionNoticia" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionNoticia_Click"/>
                                        <asp:Button class="btn btn-success causesValidation vgNoticia" ID="btnModificarNoticia" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarNoticia_Click"/>
                                        <asp:Button class="btn btn-succes
                                            s causesValidation vgNoticia" ID="btnRegistrarNoticia" runat="server" Text="Registrar" OnClick="btnRegistrarNoticia_Click"/>
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
                                           <%-- <th class="col-md-1"></th>--%>
                                            <th class="col-md-1 hidden-xs">Fecha</th>
                                            <th class="col-md-5">Título</th>
                                            <th class="col-md-1"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptNoticias" runat="server" OnItemCommand="rptNoticias_ItemCommand">
                                            <ItemTemplate>
                                                <tr>                  
                                                    <%--<td><img src="<%# ((Entidades.Noticia)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>--%>                            
                                                    <td class="hidden-xs"><%# Eval("fecha") %></td>
                                                    <td><%# Eval("titulo") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarNoticia" title="Editar Noticia" runat="server" CommandName="editarNoticia" CommandArgument='<%#Eval("idNoticia")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarNoticia" title="Eliminar Noticia" runat="server" CommandName="eliminarNoticia" CommandArgument='<%#Eval("idNoticia")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinNoticias" runat="server" visible="false">
                                            <td colspan="5">No hay noticias registradas</td>
                                        </tr>
                                    </tbody>
                                </table>
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
