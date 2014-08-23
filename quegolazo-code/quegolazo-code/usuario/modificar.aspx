<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="modificar.aspx.cs" Inherits="quegolazo_code.usuario.modificar_usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        
          <h3>Mis Datos</h3>
      <fieldset id="registrar" class="form-singin vgModificar">
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
              <input type="text" runat="server" class="form-control" id="txtApellido"  rangelength="3, 50" required="true" placeholder="Apellido" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="text" runat="server" class="form-control" id="txtEmailModif" email="true"  rangelength="5, 100" required="true" placeholder="Email" />
            </div>
          </div>
          
          <p>Contraseña Actual</p>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" runat="server" class="form-control" id="txtClaveValidadora" rangelength="6, 16" required="true" placeholder="Contraseña" />
            </div>
          </div>
          <p><asp:LinkButton ID="Modificar_contraseña" runat="server" OnClick="Modificar_contraseña_Click">Modificar Contraseña</asp:LinkButton></p>
          
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <Triggers>
                    <asp:AsyncPostBackTrigger controlid="btnModificar" eventname="Click" />
                </Triggers>
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
         
          <div class="clearfix">

            <div class="col-xs-5">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-success pull-right causesValidation vgModificar" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
          </div>
          <asp:UpdatePanel ID="pnlRegistrar" runat="server">
                 <Triggers>
                    <asp:AsyncPostBackTrigger controlid="btnModificar" eventname="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:UpdateProgress runat="server" id="PageUpdateProgress">
                        <ProgressTemplate>
                            <div class="col-xs-12">
                                    <img src="/resources/img/theme/load.gif" class="img-responsive center-block"/>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Panel ID="panExito" runat="server" CssClass="alert alert-success" Visible="False"><asp:Literal ID="litMensaje" runat="server"></asp:Literal></asp:Panel>
                    <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False"><strong><asp:Literal ID="litError" runat="server"></asp:Literal></strong></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
  
</div>
         
  
</asp:Content>
