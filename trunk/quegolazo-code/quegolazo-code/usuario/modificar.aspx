<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="modificar.aspx.cs" Inherits="quegolazo_code.usuario.modificar_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="upModificarDatos" runat="server">
            <ContentTemplate>
                <fieldset id="modificar" class="form-singin vgModificar">
                    <h2>Mis Datos</h2>
                    <p>Modifique sus datos</p>
                    <div class="margin-top"></div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <input type="text" runat="server" class="form-control" id="txtNombre" rangelength="3, 50" required="true" placeholder="Nombre" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <input type="text" runat="server" class="form-control" id="txtApellido" rangelength="3, 50" required="true" placeholder="Apellido" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                            <input type="text" runat="server" class="form-control" id="txtEmailModif" email="true" rangelength="5, 100" required="true" placeholder="Email" />
                        </div>
                    </div>

                    <p>Confirmar modificación</p>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <input type="password" runat="server" class="form-control" id="txtClaveValidadora" rangelength="6, 16" required="true" placeholder="Contraseña" />
                        </div>
                    </div>
                    <p>
                        <asp:LinkButton ID="Modificar_contraseña" runat="server" OnClick="Modificar_contraseña_Click">Modificar Contraseña</asp:LinkButton></p>

                    <div id="div_modifCave" runat="server" visible="false">
                        <p>Escriba una Nueva Contraseña</p>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <input type="password" runat="server" class="form-control" id="nuevaClave" rangelength="6, 16" maxlength="16" required="true" placeholder="Nueva Contraseña" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <input id="nuevaClave2" type="password" runat="server" class="form-control" rangelength="6, 16" maxlength="16" required="true" equalto="#ContentAdmin_nuevaClave" placeholder="Repita Nueva Contraseña" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success margin-top panelMensaje" Visible="False">
                        <asp:Literal ID="litMensaje" runat="server"></asp:Literal></asp:Panel>
                    <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger margin-top panelMensaje" Visible="False"><strong>
                        <asp:Literal ID="litError" runat="server"></asp:Literal></strong></asp:Panel>
                    <div class="clearfix">
                        <div class="col-xs-10 col-md-9 col-md-offset-1 mobile-nopadding-left mobile-nopadding-right">
                            <asp:Button class="btn btn-default" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-success causesValidation vgModificar" />
                        </div>
                        <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                        <ProgressTemplate>
                            <div class="col-xs-2 nopadding-right">
                                <img src="/resources/img/theme/load3.gif" class="img-responsive center-block" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
