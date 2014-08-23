<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="jugadores.aspx.cs" Inherits="quegolazo_code.admin.edicion.jugadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
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
                                                <input type="text" class="form-control" runat="server" id="txtNombreJugador" placeholder="Nombre del Jugador" required="true">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">DNI</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtDni" runat="server" placeholder="DNI" required="true">
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Fecha de Nacimiento</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFechaNacimiento" runat="server" placeholder="Fecha de Nacimiento" date="true">
                                            </div>
                                        </div> 
                                         <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Teléfono:</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTelefono" runat="server" placeholder="Telefono" date="true">
                                            </div>
                                        </div>   
                                         <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">E-mail</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="E-mail">
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Facebook</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFacebook" runat="server" placeholder="Facebook">
                                            </div>
                                        </div>
                                        <label class="control-label col-md-3" for="radios">Sexo:</label>
                                            <div class="col-md-3">
                                            <label class="radio-inline">
                                                <input type="radio" name="rbgSexo" id="rdSexoFemenino" runat="server">
                                                Femenino
                                            </label>
                                            &nbsp;<label class="radio-inline"><input type="radio" name="rbgSexo" id="rdSexoMasculino" runat="server">
                                                Masculino
                                            </label>
                                            &nbsp;
                                        </div>
                                        &nbsp;
                                        &nbsp;
                                        <label class="control-label col-md-3" for="radios">¿Tiene Ficha Médica?</label>
                                            <div class="col-md-3">
                                            <label class="radio-inline">
                                                <input type="radio" name="rbgTieneFichaMedica" id="rdTieneFichaMedicaSi" runat="server">
                                                Si
                                            </label>
                                            &nbsp;<label class="radio-inline"><input type="radio" name="rbgTieneFichaMedica" id="rdTieneFichaMedicaNo" runat="server">
                                                No
                                            </label>
                                            &nbsp;
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
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionJugador" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionJugador_Click"/>
                                    <asp:Button class="btn btn-success causesValidation vgJugador" ID="btnModificarJugador" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarJugador_Click"/>
                                    <asp:Button class="btn btn-success causesValidation vgJugador" ID="btnRegistrarJugador" runat="server" Text="Registrar" OnClick="btnRegistrarJugador_Click" />
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
            </div>
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-search"></span>
                        Jugadores Registrados
                    </div>
                    <div class="panel-body">        
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-2">Nombre</th>
                                            <th class="col-md-2">Telefono</th>
                                            <th class="col-md-2">E-mail</th>
                                            <th class="col-md-2">Facebook</th>
                                            <th class="col-md-1"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptJugadores" runat="server" OnItemCommand="rptJugadores_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("nombre") %></td>
                                                    <td><%# Eval("telefono") %></td>
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
                                            <td colspan="5">No hay Jugadores registrados</td>
                                        </tr>
                                    </tbody>
                                </table>
                        <asp:Panel ID="panelFracasoListaJugadores" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracasoListaJugadores" runat="server"></asp:Literal>
                        </asp:Panel>
                    </div>
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
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                loadColorPicker();
            }
        });       
    </script>
</asp:Content>

