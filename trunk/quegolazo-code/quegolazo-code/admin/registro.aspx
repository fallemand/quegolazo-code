<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="quegolazo_code.admin.registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <form id="registrar" class="form-singin">
          <h2>Registro de Usuarios</h2>
          <p>Por favor ingrese sus datos</p>
          <div class="margin-top"></div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input type="text" class="form-control" name="nombre" placeholder="Nombre" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input type="text" class="form-control" name="apellido" placeholder="Apellido" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="text" class="form-control" name="email" placeholder="Email" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="text" class="form-control" id="clave" name="clave" placeholder="Contraseña" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="text" class="form-control" name="repClave" placeholder="Repita Contraseña" />
            </div>
          </div>
          <div class="row margin-top">
            <div class="col-md-6 col-md-offset-1">
              <div class="form-group">
              <label class="checkbox nomargin-top">
                <input type="checkbox" id="cbTerminos" name="cbTerminos" value="remember-me" />
                    Acuerdo con los <a href="#">Términos y Condiciones</a>
              </label>
                </div>
            </div>
            <div class="col-md-5">
              <button class="btn btn-success pull-right">Registrar</button>
            </div>
          </div>
        </form>
        <div class="center-block margin-top sub-login">
          ¿Ya tenes una cuenta? - <a href="login.html" >Ingresar Aquí</a>
        </div>
      </div>
</asp:Content>
