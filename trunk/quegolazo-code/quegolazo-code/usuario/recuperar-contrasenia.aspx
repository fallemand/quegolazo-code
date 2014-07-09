<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperar-contrasenia.aspx.cs" MasterPageFile="~/admin/admin.Master" Inherits="quegolazo_code.admin.recuperar_contrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">

            <fieldset class="form-singin validationGroup">
                <h3>Recuperar Contraseña</h3>
                <p> Escriba una nueva contraseña</p>
                <br />
                      <div class="form-group">
                          <%--<div class="input-group margin-top">--%>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" runat="server" class="form-control" id="txtClave" rangelength="6, 16" maxlength="16" required="true" placeholder="Contraseña" />
            </div>

            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                <input id="Password1" type="password" runat="server" class="form-control"  rangelength="6, 16" maxlength="16" required="true" equalTo="#ContentAdmin_txtClave" placeholder="Repita Contraseña" />
            </div>
            <br />
            <span class="input-group-btn">
                <asp:Button ID="btnGuardarClave" runat="server" Text="Guardar" OnClick="btnGuardarClave_Click" CssClass="btn btn-default causesValidation" />
            </span>
               
             </div>
                <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success" Visible="False">
                <asp:Literal ID="LitExito" runat="server"></asp:Literal></asp:Panel>
            <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                <asp:Literal ID="LitError" runat="server"></asp:Literal></asp:Panel>
      
            </fieldset>
       
    </div>
</asp:Content>

