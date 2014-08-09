<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="activar-usuario.aspx.cs" Inherits="quegolazo_code.admin.activar_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <asp:Panel ID="panel_activacion" runat="server" Visible="False">
            <div class="form-singin">
                <h2>Activación de Usuarios</h2>
                <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success" Visible="False">
                    <p>
                        Tu cuenta:
                        <asp:Literal ID="LitEmail" runat="server"></asp:Literal>
                    </p>
                    <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                </asp:Panel>
                <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="panel_no_activacion" runat="server" Visible="False">
            <fieldset class="form-singin vgActivar">
                <h2>Reenviar Activación</h2>
                <p>Si luego de unos minutos no recibe el mail de activación, busque en la carpeta de <strong>correo no deseado</strong> o <strong>spam</strong></p>
                <div class="row">
                    <div class="col-xs-9 col-xs-offset-1">
                        <div class="form-group">
                            <div class="input-group margin-top">
                                <input type="text" id="email" class="form-control" runat="server" name="email" minlength="5" maxlength="60" required="true" email="true" placeholder="Email" />
                                <span class="input-group-btn">
                                    <asp:Button ID="btnEnviarMail" runat="server" Text="Enviar" OnClick="btnEnviarMail_Click" CssClass="btn btn-default causesValidation vgActivar" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                            <ProgressTemplate>
                                <img src="/resources/img/theme/load3.gif" class="img-responsive center-block" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <asp:UpdatePanel ID="upEnviarMail" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnEnviarMail" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="panExito1" runat="server" CssClass="alert alert-success" Visible="False">
                            <asp:Literal ID="LitExito1" runat="server"></asp:Literal>
                        </asp:Panel>
                        <asp:Panel ID="panFracaso1" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="LitError1" runat="server"></asp:Literal>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
