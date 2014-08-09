<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperar-contrasenia.aspx.cs" MasterPageFile="~/admin/admin.Master" Inherits="quegolazo_code.admin.recuperar_contrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <fieldset class="form-singin vgRecuperar">
            <h3>Recuperar Contraseña</h3>
            <p>Escriba una nueva contraseña</p>
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                    <input type="password" runat="server" class="form-control" id="txtClave" rangelength="6, 16" maxlength="16" required="true" placeholder="Contraseña" />
                </div>
            </div>
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                    <input id="Password1" type="password" runat="server" class="form-control" rangelength="6, 16" maxlength="16" required="true" equalto="#ContentAdmin_txtClave" placeholder="Repita Contraseña" />
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-10">
                        <asp:Button ID="btnGuardarClave" runat="server" Text="Guardar" OnClick="btnGuardarClave_Click" CssClass="btn btn-default causesValidation vgRecuperar pull-right" />
                    </div>
                    <div class="col-xs-2">
                        <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                            <ProgressTemplate>
                                <img src="/resources/img/theme/load3.gif" class="img-responsive center-block" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="upRecuperar" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarClave" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success" Visible="False">
                        <asp:Literal ID="LitExito" runat="server"></asp:Literal>
                    </asp:Panel>
                    <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                        <asp:Literal ID="LitError" runat="server"></asp:Literal>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>

