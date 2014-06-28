<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="activar-usuario.aspx.cs" Inherits="quegolazo_code.admin.activar_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container">
        <div class="form-singin">
            <h2>Activación de Usuarios</h2>
            <p>Activar el mail: facualle@hotmail.com</p>
            <div id="panExito" class="alert alert-success"><strong>Exito!</strong> será redirigido al login en unos segundos</div>
            <div id="panFracaso" class="alert alert-danger"><strong>Error</strong> Ya existe un torneo con esa url!</div>
        </div>

        <fieldset class="form-singin validationGroup">
            <h2>Reenviar Activación</h2>
            <p>Si luego de unos minutos no recibe el mail de activación, busque en la carpeta de <strong>correo no deseado</strong> o <strong>spam</strong></p>
            <div class="form-group">
                <div class="input-group margin-top">
                    <input type="text" class="form-control" name="email" minlength="5" maxlength="60" required="true" email="true" placeholder="Email">
                    <span class="input-group-btn">
                        <button class="btn btn-default causesValidation" type="button">Enviar</button>
                    </span>
                </div>
            </div>
            <div id="Div1" class="alert alert-success"><strong>Exito!</strong> se ha enviado el mail de activación</div>
            <div id="Div2" class="alert alert-danger"><strong>Error</strong> Ya existe un torneo con esa url!</div>
        </fieldset>
    </div>
</asp:Content>
