<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="arbitros.aspx.cs" Inherits="quegolazo_code.admin.arbitros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">

        <div id="alertmsg1"  class="alert alert-danger alert-dismissible flyover flyover-bottom" role="alert">
          <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
          <strong>Warning!</strong> Better check yourself, you're not looking too good.
        </div>
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upRegistrarNuevoArbitro" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Agregar un Árbitro                                   
                            </div>
                            <div class="panel-body nopadding-bottom">
                                <fieldset class="vgArbitro">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" runat="server" id="txtNombreArbitro" placeholder="Nombre del Árbitro" required="true" rangelength="3, 100">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Celular</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtCelular" runat="server" placeholder="Celular" maxlength="20">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">E-mail</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="Email" email="true" maxlength="100">
                                            </div>
                                        </div> 
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Matrícula</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtMatricula" runat="server" placeholder="Matrícula" maxlength="20">
                                            </div>
                                        </div>                         
                                         <div class="form-group">
                                            <label for="textArea" class="col-lg-2 control-label">Imagen</label>
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
                                <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionArbitro" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionArbitro_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgArbitro" ID="btnModificarArbitro" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarArbitro_Click"/>
                                    <asp:Button class="btn btn-success causesValidation vgArbitro" ID="btnRegistrarArbitro" runat="server" Text="Registrar" OnClick="btnRegistrarArbitro_Click" />
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
                                Árbitros Existentes
                            </div>
                            <div class="col-md-4">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Arbitros"/>
                            </div>
                        </div>                        
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaArbitros" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarArbitro" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarArbitro" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-1"></th>
                                            <th class="col-md-2">Nombre</th>
                                            <th class="col-md-1">Celular</th>
                                            <th class="col-md-1">E-mail</th>
                                            <th class="col-md-1">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptArbitros" runat="server" OnItemCommand="rptArbitros_ItemCommand" >
                                            <ItemTemplate>
                                                <tr>
                                                    <td><img src="<%# ((Entidades.Arbitro)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                    <td><strong><%# Eval("nombre") %></strong></td>
                                                    <td><%# Eval("celular") %></td>
                                                    <td><%# Eval("email") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarArbitro" title="Editar Arbitro" runat="server" CommandName="editarArbitro" CommandArgument='<%#Eval("idArbitro")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarArbitro" title="Eliminar Arbitro" runat="server" CommandName="eliminarArbitro" CommandArgument='<%#Eval("idArbitro")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinArbitros" runat="server" visible="false">
                                            <td colspan="4">No hay arbitros registrados</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="panelFracasoListaArbitros" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoListaArbitros" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="eliminarArbitro" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Árbitro</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarArbitro" runat="server">
                        <ContentTemplate>
                            <strong>Árbitro: </strong>
                            <asp:Literal ID="litNombreArbitro" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Está seguro de eliminar el árbitro?
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
            $('#alertmsg1').toggleClass('in');
        });
    </script>
</asp:Content>
