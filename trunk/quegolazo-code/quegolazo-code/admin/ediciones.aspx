﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ediciones.aspx.cs" Inherits="quegolazo_code.admin.Ediciones" MasterPageFile="~/admin/admin.torneo.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <!-- Pantalla Principal -->
    <div class="container padding-top">
        <asp:UpdatePanel ID="upTorneos" runat="server">
            <ContentTemplate>             
                <div class="row">
                    <div class="col-md-12">
                        <asp:Repeater ID="rptEdiciones" runat="server"  OnItemCommand="rptEdiciones_ItemCommand" OnItemDataBound="rptEdiciones_ItemDataBound">
                            <ItemTemplate>
                                <div class="panel panel-default lista-torneos shadow-sm">
                                    <div class="panel-heading header clearfix">
                                        <div class="col-md-1">
                                           <div class="thumbnail nomargin-bottom">
                                                <img id='img<%# Logica.Sesion.getTorneo().idTorneo %>' src="<%# Logica.Sesion.getTorneo().obtenerImagenChicha() %>" />
                                            </div>
                                        </div>
                                        <div class="col-md-5"> 
                                            <h3><%# Eval("nombre") %></h3>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="pull-right botones">
                                                <asp:LinkButton ClientIDMode="AutoID" ID="lnkConfigurarEdicion" title="Ir al Panel de Configuración" CssClass="btn btn-panel-important shadow-xs" runat="server" CommandName="configurarEdicion" CommandArgument='<%#Eval("idEdicion")%>' rel="txtTooltip">Configurar Edición</asp:LinkButton>
                                                <asp:LinkButton ClientIDMode="AutoID" ID="lnkModificarEdicion" title="Editar Edición" CssClass="btn btn-panel shadow-xs" runat="server" CommandName="editarEdicion" CommandArgument='<%#Eval("idEdicion")%>' rel="txtTooltip"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                                <asp:LinkButton ClientIDMode="AutoID" ID="lnkEliminarEdicin" title="Eliminar Edición" CssClass="btn btn-panel shadow-xs" runat="server" CommandName="eliminarEdicion" CommandArgument='<%#Eval("idEdicion")%>' rel="txtTooltip"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    
                                 <div class="panel-body">
                                     <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="flaticon-soccer18" style="font-size: 18px;line-height: 12px;"></span>
                                    Equipos Participantes
                                </div>
                                <div class="panel-body small-padding">
                                    <table>
                                         <asp:Repeater ID="rptEquipos" runat="server">
                                                        
                                             <ItemTemplate>
                                                         <tr class="col-md-4">
                                                            <td><img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>"  class="img-responsive" alt="" style="height:22px; max-width:30px; margin-right:4px;"  /></td>
                                                            <td><%# Eval("nombre") %></td> 
                                                        </tr>
                                           </ItemTemplate>
                                                    </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                    </div>
                                 <div class="col-md-6">
                                <b>Tamaño: </b> <%# Eval("tamanioCancha.nombre") %><br />
                                <b>Tipo de Superficie:</b> <%# Eval("tipoSuperficie.nombre") %> <br />
                                <b>Género: </b> <%# Eval("generoEdicion.nombre") %><br />
                                <b>Estado:</b> <%# Eval("estado.nombre")%><br />
                              </div>
                                      <div class="col-md-4">
                                <b>Fase Actual: Fase 1 </b><br />
                                <b>Próxima Fecha: </b>Fecha 11  <br />
                     
                              </div>
                                 </div>
                                 
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="panFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pantalla Principal -->

    <!-- Modal Agregar Edicion -->
    <div class="modal fade" id="modalEdicion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upModalEdicion" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>                            
                            <h4 class="modal-title" id="H1"><i class="flaticon-trophy5"></i>
                                <asp:Label ID="lblTituloModalEdicion" runat="server" Text="Agregar Nueva Edición"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                           <fieldset class="form-horizontal vgDatosEdicion">
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Torneo</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtTorneoAsociado" runat="server" name="nombreTorneoEdicion" placeholder="Nombre del Torneo" disabled>
                                                <span class="help-block">Torneo para el cual esta creando una nueva Edición</span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Nombre</label>
                                            <div class="col-lg-10">
                                                <input type="text" class="form-control" id="txtNombreEdicion" runat="server" rangelength="3, 50" required="true" name="nombreEdicion" placeholder="Nombre de la Edición">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Tamaño</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlTamañoCancha" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Superficie</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlTipoSuperficie" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="select" class="col-lg-2 control-label">Género</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>                                        
                                 
                                        <div class="form-group">
                                            <label for="text" class="col-lg-2 control-label">Puntos</label>
                                            <div class="col-lg-10">
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-up"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorGanar" runat="server" rel="txtTooltip" title="Puntos por Ganar" name="ptosGanar" value="3" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">=</span>
                                                        <input type="number" class="form-control" id="txtPuntosPorEmpatar" runat="server" rel="txtTooltip" title="Puntos por Empatar" name="ptosEmpatar" value="1" required="required">
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-chevron-down"></span></span>
                                                        <input type="number" class="form-control" id="txtPuntosPorPerder" runat="server" rel="txtTooltip" title="Puntos por Perder" name="ptosPerder" value="0" required="required">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                             <asp:Panel ID="panFracasoEdicion" runat="server" CssClass="alert alert-danger" Visible="False">
                               <asp:Literal ID="litFracasoEdicion" runat="server"></asp:Literal>
                             </asp:Panel>                      
                        </div>
                        <div class="modal-footer">
                            <div class="col-xs-5 col-xs-offset-6">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                <asp:Button ID="btnSiguienteEdicion" runat="server" Text="Guardar" CssClass="btn btn-success causesValidation vgDatosEdicion" OnClick="btnSiguienteEdicion_Click" />
                                <asp:Button ID="btnModificarEdicion" runat="server" Text="Modificar" Visible="false" CssClass="btn btn-success causesValidation vgDatosEdicion" OnClick="btnModificarEdicion_Click"/>
                           </div>
                            <div class="col-xs-1">
                                <asp:UpdateProgress runat="server" ID="UpdateProgressModalEdicion" AssociatedUpdatePanelID="upModalEdicion">
                                    <ProgressTemplate>
                                        <img src="/resources/img/theme/load3.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Agregar Edicion -->
   
    <!-- Modal Eliminar Edición -->
      <div class="modal fade bs-example-modal-sm" id="eliminarEdicion" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="H2">Eliminar Edición</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEliminarEdicion" runat="server">
                        <ContentTemplate>
                            <strong>Edición: </strong>
                            <asp:Literal ID="litNombreEdicion" runat="server"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    ¿Está seguro de eliminar la Edición?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarEdicion" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminarEdicion_Click"/>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Eliminar Edición -->
    <!-- Script -->
    <script>
        $(document).ready(function () {
            $('body').on('change', '#ContentAdmin_imagenUpload', function () {
                previewImage(this, 'ContentAdmin_imagenpreview');
                ajaxFileUpload('ContentAdmin_imagenUpload');
            });
            $('#modalEdicion').on('hidden.bs.modal', function () {
                limpiarModalEdicion();
            });
        });
    </script>
    <!-- Script -->
   </asp:Content>