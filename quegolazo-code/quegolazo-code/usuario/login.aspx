﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="quegolazo_code.admin.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <fieldset id="login" class="form-singin validationGroup">
          <h2>Login de Usuarios</h2>
          <p>Por favor ingrese sus datos</p>
          <div class="margin-top"></div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="text" id="txtEmail" class="form-control" placeholder="Email" email="true" rangelength="5, 100" required="true" runat="server" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" id="txtContrasenia" class="form-control" placeholder="Contraseña" rangelength="6, 16" required="true" runat="server" />
            </div>
          </div>
          <div class="clearfix">
            <div class="col-xs-7">
              <label class="checkbox">
                <input type="checkbox" id="noCerrarSesion" runat="server" /> No cerrar sesión
              </label>
            </div>
            <div class="col-xs-5">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnLoguearse" runat="server" Text="Loguearse" CssClass="btn btn-success pull-right causesValidation" OnClick="btnLoguearse_Click" />
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
           </div>
            <asp:UpdatePanel ID="pnlLoguear" runat="server">
                 <Triggers>
                    <asp:AsyncPostBackTrigger controlid="btnLoguearse" eventname="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:UpdateProgress runat="server" id="PageUpdateProgress">
                        <ProgressTemplate>
                            <div class="col-xs-12">
                                <img src="/resources/img/theme/load.gif" class="img-responsive center-block"/>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success margin-top panelMensaje" Visible="False"><asp:Literal ID="litMensaje" runat="server"></asp:Literal> <br />Revise su casilla de correo para activar su cuenta</asp:Panel>
                    <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger margin-top panelMensaje" Visible="False"><asp:Literal ID="litError" runat="server"></asp:Literal></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <div class="center-block margin-top sub-login">
          <a href="<%=Logica.GestorUrl.uRECUPERAR %>" >No recuerdo la contraseña</a> - <a href="<%=Logica.GestorUrl.uREGISTRO %>" >Registrarme</a>
        </div>
      </div>
</asp:Content>
