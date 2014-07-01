<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="mis-torneos.aspx.cs" Inherits="quegolazo_code.admin.mis_torneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container padding-top">
        <div class="row">
            <div class="container">
                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#registrarTorneo">Crear un Nuevo Torneo</button>
                <asp:Label ID="lblMensajeTorneos" runat="server" Text=""></asp:Label>
                <asp:Repeater ID="rptTorneos" runat="server" OnItemDataBound="rptTorneosItemDataBound">
                    <ItemTemplate>
                        <div class="panel panel-default lista-torneos shadow-sm">
                    <div class="panel-heading header clearfix">
                        <div class="col-md-1">
                            <div class="thumbnail nomargin-bottom">
                                <img src="http://www.micampeonato.com/images/campeonatos/8341614052275_244_logo_torneol.jpg" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <h3><%# Eval("nombre") %></h3>
                        </div>
                        <div class="col-md-6">
                            <div class="pull-right botones">
                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Ver Sitio Web del Torneo"><span class="glyphicon glyphicon-globe"></span></a>
                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Editar Torneo"><span class="glyphicon glyphicon-pencil"></span></a>
                                <a href="#" class="btn btn-panel shadow-xs" rel="txtTooltip" data-placement="top" title="Eliminar Torneo"><span class="glyphicon glyphicon-remove"></span></a>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">

                        <div class="pull-left">
                            <h4>Ediciones</h4>
                        </div>
                        <div class="pull-right">
                            <a href="#" class="btn btn-success btn-xs" data-toggle="modal" data-target="#agregarEdicion2"><span class="glyphicon glyphicon-plus-sign"></span>Agregar Edición</a>
                        </div>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th class="col-md-1">Número</th>
                                    <th class="col-md-4">Nombre</th>
                                    <th class="col-md-2">Tamaño</th>
                                    <th class="col-md-2">Superficie</th>
                                    <th class="col-md-2">Estado</th>
                                    <th class="col-md-1"></th>
                                </tr>
                            </thead>
                            <tbody>                               
                                <asp:Repeater ID="rptEdiciones" runat="server" >
                                  <ItemTemplate>
                                      <tr>
                                        <td><%# Eval("idEdicion") %></td>
                                        <td><%# Eval("nombre") %></td>
                                        <td><%# Eval("tamanioCancha.nombre") %></td>
                                        <td><%# Eval("tipoSuperficie.nombre") %></td>
                                        <td><%# Eval("estado.nombre") %></td>

                                        <td>
                                            <a href="#" class="editar"><span class="glyphicon glyphicon-pencil" rel="txtTooltip" title="Editar Edición" data-toggle="tooltip" data-placement="top"></span></a>
                                            <a href="#" class="editar"><span class="glyphicon glyphicon-remove eliminar" rel="txtTooltip" title="Eliminar Edición" data-toggle="tooltip" data-placement="top"></span></a>
                                        </td>
                                    
                                    </tr>
                                                                        
                                  </ItemTemplate>
                              
                                </asp:Repeater>
                             
                            </tbody>
                        </table>
                       <asp:Label ID="lblMensajeEdiciones" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                </ItemTemplate>
              </asp:Repeater>
                
            </div>
        </div>
    </div>
    <!-- Modal Registrar Torneo -->
    <div class="modal fade" id="registrarTorneo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"><i class="flaticon-trophy5"></i>Registrar Nuevo Torneo</h4>
                </div>
                <div class="modal-body">
                    <fieldset class="form-horizontal validationGroup">
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Nombre</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" name="nombreTorneo" placeholder="Nombre del Nuevo Torneo">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">URL</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" name="urlTorneo"  minlength="3" maxlength="60" required="true" placeholder="url-del-torneo">
                                    <span class="help-block">Nombre de la url del torneo. No podrá cambiarlo. www.quegolazo.com/<b>url-del-torneo</b></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="textArea" class="col-lg-2 control-label">Descripción</label>
                                <div class="col-lg-10">
                                    <textarea class="form-control" rows="3" id="textArea"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="textArea" class="col-lg-2 control-label">Logo</label>
                                <div class="col-lg-10">
                                    <div class="col-md-4">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="fileinput-preview thumbnail" data-trigger="fileinput"></div>
                                            <div>
                                                <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span><span class="fileinput-exists">Cambiar</span><input type="file" name="..."></span>
                                                <a href="#" class="btn btn-default btn-xs fileinput-exists" data-dismiss="fileinput">Eliminar</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <p class="help-block" style="margin-top: 15px;">
                                            <strong>Formato admitido</strong><br />
                                            PNG, JPEG, JPG, GIF<br />
                                            <strong>Tamaño Máximo</strong><br />
                                            512kb<br />
                                        </p>
                                    </div>
                                </div>
                            </div>
                    </fieldset>
                    <div id="panFracaso" class="alert alert-danger"><strong>Error</strong> Ya existe un torneo con esa url!</div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-success causesValidation">Registrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registrar Torneo -->

    <!-- Modal Agregar Edicion -->
    <div class="modal fade" id="agregarEdicion2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H1"><i class="flaticon-trophy5"></i>Agregar Nueva Edición</h4>
                </div>
                <div class="modal-body">
                    <fieldset class="validationGroup form-horizontal">
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Torneo</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" name="nombreTorneoEdicion"  placeholder="Torneo Cuna Potrero" disabled>
                                    <span class="help-block">Torneo para el cual esta creando una nueva edición.</span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Nombre</label>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" name="nombreEdicion" minlength="3" maxlength="60" required="true" placeholder="Nombre de la Edición">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Tamaño</label>
                                <div class="col-lg-10">
                                    <select class="form-control" name="tamanioEquipos">
                                        <option disabled selected>Seleccione el tamaño de los equipos</option>
                                        <option>5 vs 5</option>
                                        <option>7 vs 7</option>
                                        <option>9 vs 9</option>
                                        <option>11 vs 11</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Superficie</label>
                                <div class="col-lg-10">
                                    <select class="form-control" name="tipoSuperficie">
                                        <option disabled selected>Seleccione el tipo de superficie</option>
                                        <option>Cesped Sintético</option>
                                        <option>Césped Natural</option>
                                        <option>Tierra</option>
                                        <option>Salón</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="select" class="col-lg-2 control-label">Complejos</label>
                                <div class="col-lg-10">
                                    <div class="input-group">
                                        <select class="form-control" name="establecimientos">
                                            <option disabled selected>Seleccione el establecimiento</option>
                                            <option>Complejo Quilmes</option>
                                            <option>Complejo Tres Aguas</option>
                                            <option>Complejo Match5</option>
                                        </select>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">Agregar</button>
                                        </span>
                                    </div>
                                    <span class="help-block">Complejo Grandes Arcos <a href="" rel="txtTooltip" title="Eliminar"><span class="glyphicon glyphicon-remove"></span></a></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="text" class="col-lg-2 control-label">Puntos</label>
                                <div class="col-lg-10">
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-up"></span></span>
                                            <input type="text" class="form-control" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3">
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">=</span>
                                            <input type="text" class="form-control" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1">
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                            <input type="text" class="form-control" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0">
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </fieldset>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-success causesValidation">Registrar</button>
                </div>
            </div>
        </div>
     </div>
    <!-- Modal Agregar Edicion -->
</asp:Content>
