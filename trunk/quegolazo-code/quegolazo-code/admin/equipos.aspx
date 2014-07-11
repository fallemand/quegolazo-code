﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.equipos" %>

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
                                    <input type="text" class="form-control" runat="server" id="txtNombreEquipo" placeholder="Nombre del Equipo" required ="true">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Director</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtNombreDirector" runat="server" placeholder="Nombre del Director Técnico">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Color °1</label>
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color primario de la camiseta" id="txtColorPrimario" runat="server" value="#E1E1E1">
                                </div>
                                <label for="text" class="col-lg-2 control-label">Color 2°</label>
                                <div class="col-lg-2 colorpick">
                                    <input type="text" class="form-control" rel="txtTooltip" title="Color secundario de la camiseta" id="txtColorSecundario" runat="server" value="#E1E1E1">
                                </div>
                            </div>
                            <div class="subform-horizontal clearfix">
                                <label for="select" class="col-lg-2 control-label">Delegados</label>
                                <div class="col-lg-10">
                                    <span class="label label-default label-md">
                                        <a href="" rel="txtTooltip" title="Eliminar" onclick="showDelegados();return false;"><span class="glyphicon glyphicon-plus"></span>Agregar Nuevo</a>
                                    </span>
                                    <asp:Repeater ID="rptDelegados" runat="server">
                                        <ItemTemplate>
                                            <span class="label label-default label-md"><%# Eval("nombre") %>
                                             <a href="" rel="txtTooltip" title="Eliminar"><span class="glyphicon glyphicon-remove"></span></a>
                                             <a href="" rel="txtTooltip" title="Modificar"><span class="glyphicon glyphicon-pencil"></span></a>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        <div id="delegado" class="col-md-9">
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-user"></i></span>
                                                    <input type="text" class="form-control margin-xs input-sm" id="txtNombreDelegado" placeholder="Nombre del Delegedo" runat="server">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-envelope"></i></span>
                                                    <input type="text" class="form-control margin-xs input-sm" id="txtEmailDelegado" placeholder="Email del Delegedo" runat="server">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-phone"></i></span>
                                                    <input type="text" class="form-control margin-xs input-sm" id="txtTelefonoDelegado" placeholder="Teléfono del Delegedo" runat="server">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon input-sm"><i class="glyphicon glyphicon-home"></i></span>
                                                    <input type="text" class="form-control margin-xs input-sm" id="txtDireccionDelegado" placeholder="Dirección del Delegedo" runat="server">
                                                </div>
                                            </div>
                                            <asp:Button class="btn btn-default btn-xs pull-right" ID="btnAgregarDelegado" runat="server" Text="Agregar Delegado" OnClick="btnAgregarDelegado_Click" />
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
                                                <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span><span class="fileinput-exists">Cambiar</span>
                                                    <asp:FileUpload ID="fuLog" runat="server" />
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
                        <asp:Button class="btn btn-success pull-right causesValidation" ID="btnRegistrarEquipo" runat="server" Text="Registrar" OnClick="btnRegistrarEquipo_Click" />
                    </div>
                </div>
            </fieldset>
            <asp:Panel ID="panFracasoTorneo" runat="server" CssClass="alert alert-danger" Visible="False">
            <asp:Literal ID="litFracasoTorneo" runat="server"></asp:Literal>
            </asp:Panel>
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
            $('#ContentAdmin_ContentAdminTorneo_txtColorPrimario').colorPicker();
            $('#delegado').hide();
            $('#ContentAdmin_ContentAdminTorneo_txtColorSecundario').colorPicker();
        });
        function showDelegados() {
            $('#delegado').toggle("slow");
        }
    </script>
</asp:Content>
