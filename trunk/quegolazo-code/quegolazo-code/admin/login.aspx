<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="quegolazo_code.admin.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <form id="login" class="form-singin">
          <h2>Login de Usuarios</h2>
          <p>Por favor ingrese sus datos</p>
          <div class="margin-top"></div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="text" class="form-control" name="email" placeholder="Email" />
            </div>
          </div>
          <div class="form-group">
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="text" class="form-control" name="clave" placeholder="Contraseña" />
            </div>
          </div>
          <div class="row margin-top">
            <div class="col-md-6 col-md-offset-1">
              <label class="checkbox">
                <input type="checkbox" value="remember-me" /> No cerrar sesión
              </label>
            </div>
            <div class="col-md-5">
              <button class="btn btn-success pull-right">Loguearse</button>
            </div>
          </div>
        </form>
        <div class="center-block margin-top sub-login">
          <a href="#" >No recuerdo la contraseña</a> - <a href="registrar.html" >Registrarme</a>
        </div>
      </div>
    <!-- Validar Form -->
    <script>
        $('#login').validate({
            rules: {
                email: {
                    minlength: 5,
                    maxlength: 60,
                    required: true,
                    email: true
                },
                clave: {
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            },
            errorElement: 'span',
            errorClass: 'help-block',
            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
                } else {
                    error.insertAfter(element);
                }
            }
        });
    </script>
    <!-- Validar Form -->
</asp:Content>
