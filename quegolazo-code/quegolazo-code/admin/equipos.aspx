<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.equipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="col-md-6">
            <fieldset class="validationGroup">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-plus"></span>
                        Agregar un Equipo
                    </div>
                    <div class="panel-body nopadding-bottom">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Nombre</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtNombreEquipo" placeholder="Nombre del Equipo" required>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Director</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtNombreDirector" placeholder="Nombre del Director Técnico">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Color °1</label>
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color primario de la camiseta" id="txtColorPrimario" value="#E1E1E1">
                                </div>
                                <label for="text" class="col-lg-2 control-label">Color 2°</label>
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color secundario de la camiseta" id="txtColorSecundario" value="#E1E1E1">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Delegados</label>
                                <div class="col-lg-10">
                                    <span class="label label-default label-md">
                                        <a href="" rel="txtTooltip" title="Eliminar" onclick="showDelegados();return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Nuevo</a>
                                    </span>
                                    <span class="label label-default label-md">Juan Martín
                     <a href="" rel="txtTooltip" title="Eliminar"><span class="glyphicon glyphicon-remove"></span></a>
                                        <a href="" rel="txtTooltip" title="Modificar"><span class="glyphicon glyphicon-pencil"></span></a>
                                    </span>
                                    <span class="label label-default label-md">Juan Martín
                     <a href="" rel="txtTooltip" title="Eliminar"><span class="glyphicon glyphicon-remove"></span></a>
                                        <a href="" rel="txtTooltip" title="Modifical"><span class="glyphicon glyphicon-pencil"></span></a>
                                    </span>
                                    <div class="row">
                                        <div id="delegado" class="col-md-9">
                                            <div class="input-group">
                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-user"></i></span>
                                                <input type="text" class="form-control margin-xs input-sm" id="txtNombreDelegado" placeholder="Nombre del Delegedo">
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-envelope"></i></span>
                                                <input type="text" class="form-control margin-xs input-sm" id="txtEmailDelegado" placeholder="Email del Delegedo">
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-phone"></i></span>
                                                <input type="text" class="form-control margin-xs input-sm" id="txtTelefonoDelegado" placeholder="Teléfono del Delegedo">
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-home"></i></span>
                                                <input type="text" class="form-control margin-xs input-sm" id="txtDireccionDelegado" placeholder="Dirección del Delegedo">
                                            </div>
                                            <button class="btn btn-default btn-xs pull-right">Agregar Delegado</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                <div class="col-lg-10">
                                    <div class="col-md-5">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="fileinput-new thumbnail">
                                                <img src="../resources/img/theme/logo-default.png" alt="...">
                                            </div>
                                            <div class="fileinput-preview fileinput-exists thumbnail"></div>
                                            <div>
                                                <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span><span class="fileinput-exists">Cambiar</span><input type="file" name="..."></span>
                                                <a href="#" class="btn btn-default btn-xs fileinput-exists" data-dismiss="fileinput">Eliminar</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-7">
                                        <p class="help-block" style="margin-top: 15px;">
                                            <strong>Formato admitido</strong><br />
                                            PNG, JPEG, JPG, GIF<br />
                                            <strong>Tamaño Máximo</strong><br />
                                            512kb<br />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer clearfix">
                        <button class="btn btn-success pull-right causesValidation">Registrar</button>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-search"></span>
                    Equipos Existentes
                </div>
                <div class="panel-body">
                    Panel content
                </div>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            $('#txtColorPrimario').colorPicker();
            $('#delegado').hide();
            $('#txtColorSecundario').colorPicker();
        });
        function showDelegados() {
            $('#delegado').toggle("slow");
        }
    </script>
</asp:Content>
