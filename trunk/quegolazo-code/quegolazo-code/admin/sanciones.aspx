<%@ Page Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="sanciones.aspx.cs" Inherits="quegolazo_code.admin.sanciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <asp:UpdatePanel ID="upRegistrarNuevaSancion" runat="server">
                    <ContentTemplate>
                        <div class="well">
                        <div class="row">
                            <fieldset class="vgSeleccionarEdicion">
                                <div class="col-md-8">
                                    <div id="selectEdiciones">
                                        <asp:DropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar Edición" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click"/>
                                </div>
                            </fieldset>
                        </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-plus"></span>
                                Agregar una Sanción                                  
                            </div>
                            <div class="panel-body nopadding-bottom">   
                            <div class="clearfix">
                            <div class="col-md-8 first">
                                <p class="">
                                   ¿A quién le va aplicar la Sanción?                                   
                                </p>
                            </div>
                            <div class="col-md-4 nopadding-left nopadding-right switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="cambioJugadores();">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdJugadores" name="rbgSanciones" disabled >
                                        <label for="rdJugadores" onclick="">Jugadores</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdEquipos" name="rbgSanciones" disabled> 
                                        <label for="rdEquipos" onclick="">Equipos</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
                            </div>
                            </div>
                            </div>
                            <div class="panel-body nopadding-bottom">   
                            <div class="clearfix">
                            <div class="col-md-8 first">
                                <p class="">
                                   ¿Se la va aplicar a un Partido?                                  
                                </p>
                            </div>
                            <div class="col-md-4 nopadding-left nopadding-right switch">
                                <div class="switch-toggle well nomargin-bottom" onclick="cambioPartido()">
                                    <input type="radio"  ClientIDMode="Static" runat="server" id="rdPartido" name="rbgPartidoSinDefinir" disabled>
                                        <label for="rdPartido" onclick="">Partido</label>
                                    <input type="radio" ClientIDMode="Static" runat="server" id="rdSinDefinir" name="rbgPartidoSinDefinir" disabled> 
                                        <label for="rdSinDefinir" onclick="">Sin definir</label>
                                    <a class="btn btn-success" onclick=""></a>
                                </div>
                                &nbsp;
                            </div>
                            </div>
                            </div>
                            <asp:Panel ID="panelCombos" runat="server">  
                            <div class="panel-body nopadding-bottom">                                   
                                <fieldset class="vgSancion">
                                    <div class="form-horizontal">
                                        <div class="form-group" id="idDivFecha">
                                            <label for="text" class="col-lg-2 control-label">Fecha</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlFecha" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFecha_SelectedIndexChanged" ></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="idDivPartido">
                                            <label for="text" class="col-lg-2 control-label">Partido</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlPartido" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPartido_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="idDivEquipo">
                                            <label for="text" class="col-lg-2 control-label">Equipo</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlEquipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEquipo_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="idDivEquipo2">
                                            <label for="text" class="col-lg-2 control-label">Equipo</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlEquipoSinPartido" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEquipoSinPartido_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="idDivJugador">
                                            <label for="select" class="col-lg-2 control-label">Jugador</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlJugador" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="idDivFechaDate">
                                            <label for="select" class="col-lg-2 control-label">Fecha</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtFecha" runat="server" placeholder="EJ: 20/11/2014">
                                            </div>
                                        </div> 
                                        <div class="form-group" id="idDivMotivo">
                                            <label for="select" class="col-lg-2 control-label">Motivo</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div> 
                                        <div class="form-group" id="idDivObservacion">
                                            <label for="select" class="col-lg-2 control-label">Observación</label>
                                            <div class="col-lg-10">
                                                <textarea class="form-control" id="txtObservacion" runat="server" maxlength="200" rows="3"></textarea>
                                            </div>
                                        </div>     
                                        <div class="form-group" id="idDivPuntosAQuitar">
                                            <label for="select" class="col-lg-6 control-label">Puntos a Quitar</label>
                                            <div class="col-lg-6">
                                                <input type="text" class="form-control text-center" runat="server" data-container="body" id="txtPuntosAQuitar" digits="true" rel="txtTooltip" title="Puntos a Quitar" rangelength="0, 2">
                                            </div>
                                        </div> 
                                        <div class="form-group" id="idDivCantidadFechasASuspender">
                                            <label for="select" class="col-lg-6 control-label">Cantidad Fechas a Suspender</label>
                                            <div class="col-lg-6">
                                                <input type="text" class="form-control text-center" runat="server" data-container="body" id="txtCantidadFechasSuspendidas" digits="true" rel="txtTooltip" title="Cantidad Fechas a Suspender" rangelength="0, 2">
                                            </div>
                                        </div>                                          
                                    </div>
                                </fieldset>
                                </asp:Panel>
                                <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer clearfix text-right">
                                <div class="col-xs-8 col-xs-offset-3">
                                    <asp:Button class="btn btn-default" ID="btnCancelarModificacionSancion" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelarModificacionSancion_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgSancion" ID="btnModificarSancion" runat="server" Text="Modificar" Visible="false" OnClick="btnModificarSancion_Click" />
                                    <asp:Button class="btn btn-success causesValidation vgSancion" ID="btnRegistrarSancion" runat="server" Text="Registrar" OnClick="btnRegistrarSancion_Click" Enabled="false"/>
                                </div>
                                <div class="col-xs-1">
                                    <asp:UpdateProgress runat="server" ID="UpdateProgressModalTorneo">
                                        <ProgressTemplate>
                                            <img src="/resources/img/theme/load4.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-7">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-search"></span>
                                Sanciones Existentes
                            </div>
                            <div class="col-md-4">
                                <input type="text" id="filtro" class="pull-right form-control input-xs" placeholder="Filtrar Sanciones"/>
                            </div>
                        </div>                        
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upListaSanciones" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRegistrarSancion" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnModificarSancion" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-1">Fecha</th>
                                            <th class="col-md-2">Equipo</th>
                                            <th class="col-md-2">Jugador</th>
                                            <th class="col-md-2">Motivo</th>
                                            <th class="col-md-2">Puntos A Quitar</th>
                                            <th class="col-md-1">Fechas Suspendidas</th>
                                            <th class="col-md-1">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptSanciones" runat="server" OnItemCommand="rptSanciones_ItemCommand">
                                            <ItemTemplate>
                                                <tr>                                   
                                                    <td><%# Eval("Fecha") %></td>
                                                    <td><%# Eval("NombreEquipo") %></td>
                                                    <td><%# Eval("NombreJugador") %></td>
                                                    <td><%# Eval("MotivoSancion") %></td>
                                                    <td><%# Eval("PtosAQuitar") %></td>
                                                    <td><%# Eval("CantFechas") %></td>
                                                    <td>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEditarSancion" title="Editar Sanción" runat="server" CommandName="editarSancion" CommandArgument='<%#Eval("idSancion")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarSancion" title="Eliminar Sanción" runat="server" CommandName="eliminarSancion" CommandArgument='<%#Eval("idSancion")%>' rel="txtTooltip" data-toggle="tooltip" data-placement="top"><span class="glyphicon glyphicon-remove eliminar""></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinSanciones" runat="server" visible="false">
                                            <td colspan="7">No hay sanciones registradas</td>
                                        </tr>
                                        <tr id="sinEdicion" runat="server">
                                            <td colspan="7">Seleccione una edición</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="panelFracasoListaSanciones" runat="server" CssClass="alert alert-danger" Visible="False">
                                    <asp:Literal ID="litFracasoListaSanciones" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-sm" id="eliminarSancion" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Eliminar Sanción</h4>
                </div>
                <div class="modal-body">
                    ¿Está seguro de eliminar la sanción?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminar_Click"/>
                </div>
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            $('#ContentAdmin_ContentAdminTorneo_txtColorPrimario').colorPicker();
            $('#ContentAdmin_ContentAdminTorneo_txtColorSecundario').colorPicker();
            $('body').on('change', '#ContentAdmin_ContentAdminTorneo_imagenUpload', function () {
                previewImage(this, 'ContentAdmin_ContentAdminTorneo_imagenpreview');
                ajaxFileUpload('ContentAdmin_ContentAdminTorneo_imagenUpload');
            });
            $('body').on('keyup', '#filtro', function () {
                var rex = new RegExp($(this).val(), 'i');
                $('.tablaFiltro tr').hide();
                $('.tablaFiltro tr').filter(function () {
                    return rex.test($(this).text());
                }).show();
            });
            deshabilitarPanel();
        });

        //Cambio Selección JUGADOR - EQUIPO
        function cambioJugadores() {
            deshabilitarPanel();
            if (!$('#rdEquipos').is(':disabled') && !$('#rdJugadores').is(':disabled')) {
                if ($('#rdEquipos').is(':checked')) {
                    $('#idDivFechaDate').show();
                    $('#idDivMotivo').show();
                    $('#idDivObservacion').show();
                    $('#idDivPuntosAQuitar').show();
                    $('#idDivCantidadFechasASuspender').show();
                    if ($('#rdPartido').is(':checked')) {
                        $('#idDivFecha').show();
                        $('#idDivPartido').show();
                        $('#idDivEquipo').show();
                    }
                    else {
                        $('#idDivEquipo2').show();
                    }
                }
                else {
                    $('#idDivFechaDate').show();
                    $('#idDivMotivo').show();
                    $('#idDivObservacion').show();
                    $('#idDivCantidadFechasASuspender').show();
                    $('#idDivJugador').show();
                    if ($('#rdPartido').is(':checked')) {
                        $('#idDivFecha').show();
                        $('#idDivPartido').show();
                        $('#idDivEquipo').show();
                    }
                    else {
                        $('#idDivEquipo2').show();
                    }
                }
            }
        }            

        function cambioPartido() {
            if (!$('#rdPartido').is(':disabled') && !$('#rdSinDefinir').is(':disabled'))
            {
                if ($('#rdPartido').is(':checked')) {
                    $('#idDivFecha').show();
                    $('#idDivPartido').show();
                    $('#idDivEquipo').show();
                    $('#idDivEquipo2').hide();
                }
                else {
                    $('#idDivFecha').hide();
                    $('#idDivPartido').hide();
                    $('#idDivEquipo2').show();
                    $('#idDivEquipo').hide();

                }
                if ($('#rdJugadores').is(':checked')) {
                    $('#idDivJugador').show();
                }
                else
                    $('#idDivJugador').hide();
            }            
        }

        function deshabilitarPanel() {
            $('#idDivFecha').hide();
            $('#idDivPartido').hide();
            $('#idDivEquipo').hide();
            $('#idDivEquipo2').hide();
            $('#idDivJugador').hide();
            $('#idDivFechaDate').hide();
            $('#idDivMotivo').hide();
            $('#idDivObservacion').hide();
            $('#idDivPuntosAQuitar').hide();
            $('#idDivCantidadFechasASuspender').hide();
        }

        function limpiarCombos()
        {
            $('#idDivPartido').find('option').remove();
            $('#idDivEquipo').find('option').remove();
            $('#idDivJugador').find('option').remove();
        }

        //CASO EQUIPO - SIN DEFINIR
        function equipoYSinDefinir() {
            deshabilitarPanel();
            $('#idDivEquipo2').show();
            $('#idDivFechaDate').show();
            $('#idDivMotivo').show();
            $('#idDivObservacion').show();
            $('#idDivPuntosAQuitar').show();
            $('#idDivCantidadFechasASuspender').show();

            $('#idDivFecha').hide();
            $('#idDivPartido').hide();
            $('#idDivEquipo').hide();
            $('#idDivJugador').hide();
        }
        //CASO EQUIPO - PARTIDO
        function equipoYPartido() {
            deshabilitarPanel();
            $('#idDivFecha').show();
            $('#idDivPartido').show();
            $('#idDivEquipo').show();
            $('#idDivFechaDate').show();
            $('#idDivMotivo').show();
            $('#idDivObservacion').show();
            $('#idDivPuntosAQuitar').show();
            $('#idDivCantidadFechasASuspender').show();

            $('#idDivEquipo2').hide();
            $('#idDivJugador').hide();
        }

        //CASO JUGADOR Y SIN DEFINIR
        function jugadorYSinDefinir() {
            deshabilitarPanel();
            $('#idDivEquipo2').show();
            $('#idDivJugador').show();
            $('#idDivFechaDate').show();
            $('#idDivMotivo').show();
            $('#idDivObservacion').show();
            $('#idDivCantidadFechasASuspender').show();

            $('#idDivFecha').hide();
            $('#idDivPartido').hide();
            $('#idDivEquipo').hide();
            $('#idDivPuntosAQuitar').hide();
        }

        //CASO JUGADOR Y PARTIDO
        function jugadorYPartido() {
            $('#idDivFecha').show();
            $('#idDivPartido').show();
            $('#idDivEquipo').show();
            $('#idDivJugador').show();
            $('#idDivFechaDate').show();
            $('#idDivMotivo').show();
            $('#idDivObservacion').show();
            $('#idDivPuntosAQuitar').show();
            $('#idDivCantidadFechasASuspender').show();

            $('#idDivEquipo2').hide();           
        }
        
    </script>
</asp:Content>
