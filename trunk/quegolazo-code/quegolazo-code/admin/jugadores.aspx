<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="jugadores.aspx.cs" Inherits="quegolazo_code.admin.edicion.jugadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upRegistrarJugador" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Agregar un Jugador                                 
                            </div>
                            <div class="panel-body nopadding-bottom">
                                <fieldset class="vgJugador">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" runat="server" id="txtNombreJugador" placeholder="Nombre del Jugador" required="true" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">DNI</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtDni" runat="server" placeholder="DNI" required="true" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Número de Camiseta</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" runat="server" id="txtNumeroCamiseta" digits="true" placeholder="Número de Camiseta" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Fecha Nacimiento</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFechaNacimiento" runat="server" placeholder="Fecha de Nacimiento" fecha="true" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Teléfono:</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTelefono" runat="server" placeholder="Telefono" digits="true" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">E-mail</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="E-mail" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Facebook</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFacebook" runat="server" placeholder="Facebook" disabled="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-2 control-label" for="radios">Sexo</label>
                                            <div class="col-lg-10">
                                                <label class="radio-inline">
                                                    <input type="radio" name="rbgSexo" id="rdSexoMasculino" runat="server" disabled="true" checked="true">
                                                    Masculino
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" name="rbgSexo" id="rdSexoFemenino" runat="server" disabled="true">
                                                    Femenino
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-2 control-label" for="radios">Ficha Médica</label>
                                            <div class="col-lg-10">
                                                <label class="radio-inline">
                                                    <input type="radio" name="rbgTieneFichaMedica" id="rdTieneFichaMedicaSi" runat="server" disabled="true">
                                                    Si
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" name="rbgTieneFichaMedica" id="rdTieneFichaMedicaNo" runat="server" disabled="true" checked="true">
                                                    No
                                                </label>
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
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionJugador" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionJugador_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgJugador" ID="btnModificarJugador" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarJugador_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgJugador" ID="btnRegistrarJugador" runat="server" Text="Registrar" OnClick="btnRegistrarJugador_Click" Enabled="False" />
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
                <div class="well">
                    <div class="row">
                        <fieldset class="vgSeleccionarEquipo">
                            <div class="col-md-8">
                                <div id="selectEquipos">
                                    <asp:DropDownList ID="ddlEquipos" runat="server" CssClass="form-control searchableSelect" required="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnSeleccionarEquipo" runat="server" Text="Seleccionar Equipo" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEquipo" OnClick="btnSeleccionarEquipo_Click" />
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-search"></span>
                                Jugadores del Equipo
                            </div>
                            <div class="col-md-4">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Jugadores" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaEquipos" runat="server" class="listado">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarJugador" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarJugador" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-1"></th>
                                            <th class="col-md-2">Nombre</th>
                                            <th class="col-md-1">#</th>
                                            <th class="col-md-2">E-mail</th>
                                            <th class="col-md-2">Facebook</th>
                                            <th class="col-md-1">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptJugadores" runat="server" OnItemCommand="rptJugadores_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><img src="<%# ((Entidades.Jugador)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                    <td><strong><%# Eval("nombre") %></strong></td>
                                                    <td><%# Eval("numeroCamiseta") %></td>
                                                    <td><%# Eval("email") %></td>
                                                    <td><%# Eval("facebook") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarJugador" title="Editar Jugador" runat="server" CommandName="editarJugador" CommandArgument='<%#Eval("idJugador")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarJugador" title="Eliminar Jugador" runat="server" CommandName="eliminarJugador" CommandArgument='<%#Eval("idJugador")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar"></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinJugadores" runat="server" visible="false">
                                            <td colspan="6">No hay Jugadores registrados para este equipo</td>
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
     <div class="modal fade bs-example-modal-sm" id="eliminarJugador" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Jugador</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarJugador" runat="server">
                        <ContentTemplate>
                            <strong>Jugador: </strong>
                            <asp:Literal ID="litNombreJugador" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Está seguro de eliminar el Jugador?
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
            $('body').on('change', '#ContentAdmin_ContentAdminTorneo_imagenUpload', function () {
                previewImage(this, 'ContentAdmin_ContentAdminTorneo_imagenpreview');
                ajaxFileUpload('ContentAdmin_ContentAdminTorneo_imagenUpload');
            });
        });
        $('body').on('keyup', '#filtro', function () {
            var rex = new RegExp($(this).val(), 'i');
            $('.tablaFiltro tr').hide();
            $('.tablaFiltro tr').filter(function () {
                return rex.test($(this).text());
            }).show();
        });
    </script>
</asp:Content>

