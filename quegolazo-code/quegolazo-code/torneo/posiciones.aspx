<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="posiciones.aspx.cs" Inherits="quegolazo_code.torneo.posiciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
     <script src="../../resources/js/fechas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
   <div class="panel panel-default">
                        <div class="modal-header">
                            <h4 class="modal-title" id="H1"><i class="flaticon-football106"></i><span id="tituloModal">Tabla de Posiciones</span> 
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="panelSeleccionarEquipos" ClientIDMode="Static"  runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="btn-group btn-group-sm" role="group" style="margin-right:5px;" aria-label="...">
                                                    <button type="button" class="btn btn-success btn-sm" onclick="$('#tabla-posiciones tr').show('fast');">Todos</button>
                                                </div>
                                                <div class="btn-group btn-group-sm" role="group" aria-label="...">
                                                    <asp:Repeater ID="rptGrupos" runat="server">
                                                        <ItemTemplate>
                                                                <button type="button" class="btn btn-success" onclick="filtrarPosiciones('<%# Eval("idGrupo")%>')">Grupo <%# Eval("idGrupo")%></button>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row margin-top" style="max-height: 350px !important;overflow: auto;">
                                            <div class="col-md-12">
                                                <table id="tabla-posiciones" class="table table-condensed table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th class="col-md-4" colspan="2">Equipo</th>
                                                            <th class="col-md-1">PTS</th>
                                                            <th class="col-md-1">PJ</th>
                                                            <th class="col-md-1">PG</th>
                                                            <th class="col-md-1">PE</th>
                                                            <th class="col-md-1">PP</th>
                                                            <th class="col-md-1">GF</th>
                                                            <th class="col-md-1">GC</th>
                                                            <th style="display:none;"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tablaFiltro">
                                                        <asp:Repeater ID="rptEquipos" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><img src="<%# Utils.GestorImagen.obtenerImagen(Utils.Validador.castInt(Eval("idEquipo").ToString()), Utils.GestorImagen.EQUIPO, Utils.GestorImagen.CHICA) %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /></td>
                                                                    <td><%# Eval("Equipo") %></td>
                                                                    <td class="active" style="font-size:16px;"><b><%# Eval("Puntos") %></b></td>
                                                                    <td><%# Eval("PJ") %></td>
                                                                    <td><%# Eval("PG") %></td>
                                                                    <td><%# Eval("PE") %></td>
                                                                    <td><%# Eval("PP") %></td>
                                                                    <td><%# Eval("GF") %></td>
                                                                    <td><%# Eval("GC") %></td>
                                                                    <td class="idEquipo" style="display:none;"><%# Eval("idEquipo") %></td>
                                                                    <td style="display:none;"><%# Eval("idGrupo") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </asp:Panel>
                        </div>
                 </div>
</asp:Content>
