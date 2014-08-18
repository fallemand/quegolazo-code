﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="torneos.aspx.cs" Inherits="quegolazo_code.admin.mis_torneos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <!-- Pantalla Principal -->
    <div class="container padding-top">
        <div class="row">
            <div class="container">
                <asp:UpdatePanel ID="upTorneos" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-xs-12">
                                 <asp:Button ID="btnRegistrarNuevoTorneo" runat="server" Text="Crear un Nuevo Torneo" CssClass="btn btn-success" OnClick="btnRegistrarNuevoTorneo_Click" />
                            </div>
                        </div>
                        <asp:Panel ID="panelSinTorneos" runat="server" CssClass="col-md-5 margin-top " Enabled="True" Visible="False">
                            <div class="alert alert-success">
                                <span class="glyphicon glyphicon-chevron-up"></span> Haz clic en el botón para crear tu primer torneo!
                            </div>
                        </asp:Panel>
                        <asp:Repeater ID="rptTorneos" runat="server" OnItemDataBound="rptTorneosItemDataBound" OnItemCommand="rptTorneos_ItemCommand">
                            <ItemTemplate>
                                <div class="panel panel-default lista-torneos shadow-sm">
                                    <div class="panel-heading header clearfix">
                                        <div class="col-md-1">
                                            <div class="thumbnail nomargin-bottom">
                                                <img id="img<%# Eval("idTorneo") %>" src="<%# ((Entidades.Torneo)Container.DataItem).obtenerImagenChicha() %>" />
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <h3><%# Eval("nombre") %></h3>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="pull-right botones">
                                                <asp:LinkButton ClientIDMode="AutoID" ID="lnkAdministrarTorneo" title="Admnistrar Torneo" CssClass="btn btn-panel-important shadow-xs" runat="server" CommandName="administrarTorneo" CommandArgument='<%#Eval("idTorneo")%>' rel="txtTooltip"><span class="glyphicon glyphicon-flag"></span></asp:LinkButton>
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Ver Sitio Web del Torneo"><span class="glyphicon glyphicon-globe"></span></a>
                                                <asp:LinkButton ClientIDMode="AutoID" ID="lnkModificarCampeonato" title="Editar Torneo" CssClass="btn btn-panel shadow-xs" runat="server" CommandName="editarTorneo" CommandArgument='<%#Eval("idTorneo")%>' rel="txtTooltip"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Eliminar Torneo"><span class="glyphicon glyphicon-remove"></span></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="pull-left">
                                            <h4>Ediciones</h4>
                                        </div>
                                        <div class="pull-right">
                                            <asp:Button ID="btnAgregarEdicion" runat="server" Text="Agregar Edición" CommandName="agregarEdicion" CommandArgument='<%# Eval("idTorneo") %>' data-target="#modalEdicion" CssClass="btn btn-success btn-xs" />
                                        </div>
                                        <table class="table">
                                            <thead>
                                                <tr>
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
                                                <asp:Panel ID="panelSinEdiciones" runat="server">
                                                    <tr><td colspan="5">No hay ediciones registradas para el torneo</td></tr>
                                                </asp:Panel>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
    </div>
    <!-- Pantalla Principal -->

    <!-- Modal Registrar Torneo -->
    <div class="modal fade" id="modalTorneo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upModalTorneo" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnRegistrarTorneo" EventName="Click"/>
                            <asp:AsyncPostBackTrigger ControlID="btnModificarTorneo" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="modalTorneoLabel"><i class="flaticon-trophy5"></i>
                                    <asp:Label ID="lblTituloModalTorneo" runat="server" Text="Registrar Torneo"></asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <fieldset class="form-horizontal vgModalTorneo">
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">Nombre</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" id="txtNombreTorneo" runat="server" name="nombreTorneo" minlength="3" maxlength="50" required="required" placeholder="Nombre del Nuevo Torneo" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="text" class="col-lg-2 control-label">URL</label>
                                    <div class="col-lg-10">
                                        <input type="text" class="form-control" name="urlTorneo" id="txtUrlTorneo" runat="server" nospace="true" minlength="3" maxlength="100" required="required" placeholder="url-del-torneo" />
                                        <span class="help-block">Nombre de la url del torneo. No podrá cambiarlo. www.quegolazo.com/<b>url-del-torneo</b></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Descripción</label>
                                    <div class="col-lg-10">
                                        <textarea class="form-control" id="txtDescripcion" runat="server" maxlength="400" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
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
                                        <div class="col-md-8">
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
                                <asp:Panel ID="panFracasoTorneo" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoTorneo" runat="server"></asp:Literal>
                                </asp:Panel>
                                    </fieldset>
                            </div>
                            <div class="modal-footer">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                    <asp:Button ID="btnRegistrarTorneo" runat="server" CssClass="btn btn-success causesValidation vgModalTorneo" Text="Registrar" OnClick="btnResgitrarTorneo_Click"/>
                                    <asp:Button ID="btnModificarTorneo" runat="server" CssClass="btn btn-success causesValidation vgModalTorneo" Text="Modificar" OnClick="btnModificarTorneo_Click" Visible="false"/>
                                </div>
                                <div class="col-xs-1">
                                    <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo" AssociatedUpdatePanelID="upModalTorneo">
                                        <ProgressTemplate>
                                            <img src="/resources/img/theme/load3.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
        </div>
    </div>
    <!-- Modal Registrar Torneo -->

    <!-- Modal Agregar Edicion -->
    <div class="modal fade" id="modalEdicion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upModalEdicion" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H1"><i class="flaticon-trophy5"></i>Agregar Nueva Edición</h4>
                        </div>
                        <div class="modal-body">
                           <fieldset class="form-horizontal vgDatosEdicion">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Torneo</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTorneoAsociado" runat="server" name="nombreTorneoEdicion" placeholder="Nombre del Torneo" disabled>
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
                                            <label for="text" class="col-lg-2 control-label">Puntos</label>
                                            <div class="col-lg-10">
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-up"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorGanar" runat="server" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">=</span>
                                                        <input type="number" class="form-control" id="txtPuntosPorEmpatar" runat="server" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorPerder" runat="server" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0" required="required">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                             <asp:Panel ID="panFracasoEdicion" runat="server" CssClass="alert alert-danger" Visible="False">
                               <asp:Literal ID="litFracasoEdicion" runat="server"></asp:Literal>
                             </asp:Panel>                      
                        </div>
                        <div class="modal-footer">
                            <div class="col-xs-5 col-xs-offset-6">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnSiguienteEdicion" runat="server" Text="Guardar" CssClass="btn btn-success causesValidation vgDatosEdicion" OnClick="btnSiguienteEdicion_Click" />
                           </div>
                            <div class="col-xs-1">
                                <asp:UpdateProgress runat="server" ID="UpdateProgressModalEdicion" AssociatedUpdatePanelID="upModalEdicion">
                                    <ProgressTemplate>
                                        <img src="/resources/img/theme/load3.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Agregar Edicion -->

    <!-- Script -->
    <script>
        $(document).ready(function () {
            $('body').on('change', '#ContentAdmin_imagenUpload', function () {
                previewImage(this, 'ContentAdmin_imagenpreview');
                ajaxFileUpload('ContentAdmin_imagenUpload');
            });
            $('#modalEdicion').on('hidden.bs.modal', function () {
                limpiarModalEdicion();
            });
        });
    </script>
    <!-- Script -->
   </asp:Content>
