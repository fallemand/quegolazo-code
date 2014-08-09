<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="recuperar.aspx.cs" Inherits="quegolazo_code.olvide_contrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <fieldset class="form-singin vgRecuperar">
            <h3>Recuperar Contraseña</h3>
            <p>Ingresá tu cuenta de correo y recibirás un email con instrucciones para restablecer tu clave.</p>
            <div class="row">
                <div class="col-xs-9 col-xs-offset-1">
                    <div class="form-group">
                        <div class="input-group">
                            <input type="text" id="email" class="form-control" runat="server" name="email" minlength="5" maxlength="60" required="true" email="true" placeholder="Email" />
                            <span class="input-group-btn">
                                <asp:Button ID="btnEnviarMail" runat="server" Text="Enviar" OnClick="btnEnviarMail_Click" CssClass="btn btn-default causesValidation vgRecuperar" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-xs-2">
                    <asp:UpdateProgress runat="server" id="PageUpdateProgress">
                        <ProgressTemplate>
                                <img src="/resources/img/theme/load3.gif" class="img-responsive center-block"/>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>
            <asp:UpdatePanel id="upRecuperar" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEnviarMail" EventName="Click" />
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
