<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="quegolazo_code.admin.registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <fieldset id="registrar" class="form-singin validationGroup">
          <h2>Registro de Usuarios</h2>
          <p>Por favor ingrese sus datos</p>
          <div class="margin-top"></div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input type="text" runat="server" class="form-control" id="txtNombre"  minlength="3" maxlength="60" required="true" placeholder="Nombre" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input type="text" runat="server" class="form-control" id="txtApellido"  minlength="3" maxlength="60" required="true" placeholder="Apellido" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="text" runat="server" class="form-control" id="txtEmail"  minlength="5" mail="true" maxlength="60" required="true" placeholder="Email" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" runat="server" class="form-control" id="txtClave"  minlength="4" maxlength="20" required="true" placeholder="Contraseña" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                <input type="password" runat="server" class="form-control"  minlength="4" maxlength="20" required="true" equalTo="#ContentAdmin_txtClave" placeholder="Repita Contraseña" />
            </div>
          </div>
          <div class="row margin-top">
            <div class="col-md-6 col-md-offset-1">
              <div class="form-group">
              <label class="checkbox nomargin-top">
                <input type="checkbox" id="cbTerminos" name="cbTerminos" required="true" value="remember-me" />
                    Acuerdo con los <a href="#">Términos y Condiciones</a>
              </label>
                </div>
            </div>
            <div class="col-md-5">
                 <asp:ScriptManager ID="MainScriptManager" runat="server" />
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" CssClass="btn btn-success pull-right causesValidation" />
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
          </div>
          <asp:UpdatePanel ID="pnlRegistrar" runat="server">
                 <Triggers>
                    <asp:AsyncPostBackTrigger controlid="btnRegistrar" eventname="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:UpdateProgress runat="server" id="PageUpdateProgress">
                        <ProgressTemplate>
                            <img src="/resources/img/theme/load.gif" class="img-responsive center-block"/>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success" Visible="False"><strong><asp:Literal ID="litMensaje" runat="server"></asp:Literal></strong> <br />Revise su casilla de correo para activar su cuenta</asp:Panel>
                    <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False"><strong><asp:Literal ID="litError" runat="server"></asp:Literal></strong></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <div class="center-block margin-top sub-login">
          ¿Ya tenes una cuenta? - <a href="login.html" >Ingresar Aquí</a>
        </div>
      </div>
</asp:Content>
