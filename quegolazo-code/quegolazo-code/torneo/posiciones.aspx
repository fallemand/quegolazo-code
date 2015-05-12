<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="posiciones.aspx.cs" Inherits="quegolazo_code.torneo.posiciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <script src="js/fechas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
 
        <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1> <span class="flaticon-football31" aria-hidden="true"></span> Posiciones </h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>" ><%=gestorTorneo.torneo.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion) %>" ><%=gestorEdicion.edicion.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlPosiciones(nickTorneo,idEdicion) %>">Posiciones</a></li>                  
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

        <!-- Content Central -->
        <div class="container padding-top">
            <div class="row mobile-margin-top">

                 <div class="col-sm-12">    
                 <div class="panel panel-default">
                       <%-- <div class="modal-header">
                            <h4 class="modal-title" id="H1"><i class="flaticon-football106"></i><span id="tituloModal">Tabla de Posiciones</span> 
                            </h4>
                        </div>--%>
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
                                                            <th class="col-md-4 text-center" colspan="2">Equipo</th>
                                                            <th class="col-md-1 text-center">PTS</th>
                                                            <th class="col-md-1 text-center">PJ</th>
                                                            <th class="col-md-1 text-center">PG</th>
                                                            <th class="col-md-1 text-center">PE</th>
                                                            <th class="col-md-1 text-center">PP</th>
                                                            <th class="col-md-1 text-center">GF</th>
                                                            <th class="col-md-1 text-center">GC</th>
                                                            <th style="display:none;"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tablaFiltro">
                                                        <asp:Repeater ID="rptEquipos" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <input hidden="hidden" <%# gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(int.Parse(Eval("idEquipo").ToString())) %> />
                                                                        <img id="img" src="<%# gestorEquipo.equipo.obtenerImagenChicha() %>" class="img-responsive center-block avatar-xs" runat="server" visible="<%# gestorEquipo.equipo.tieneImagen()%>">
                                                                        <div id="divCamistea" class="camiseta-equipo" runat="server" visible="<%# gestorEquipo.equipo.tieneImagen()==false%>">
                                                                                            <div>
                                                                                                <i class="flaticon-football114" style="color: <%# gestorEquipo.equipo.colorCamisetaPrimario %>"></i>
                                                                                            </div><!--
                                                                                            --><div class="segunda-mitad">
                                                                                                <i class="flaticon-football114" style="color: <%#gestorEquipo.equipo.colorCamisetaSecundario%>"></i>
                                                                                            </div>
                                                                       </div>
                                                                    </td>
                                                                    <td><a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>"><%# Eval("Equipo") %></a></td>
                                                                    <td class="active text-center" style="font-size:16px;"><b><%# Eval("Puntos") %></b></td>
                                                                    <td class="text-center"><%# Eval("PJ") %></td>
                                                                    <td class="text-center"><%# Eval("PG") %></td>
                                                                    <td class="text-center"><%# Eval("PE") %></td>
                                                                    <td class="text-center"><%# Eval("PP") %></td>
                                                                    <td class="text-center"><%# Eval("GF") %></td>
                                                                    <td class="text-center"><%# Eval("GC") %></td>
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
                 </div>  
            
                            <!-- Equipos de la edicion -->
                <div class="col-sm-12">
                    <div class="panel nopadding panel-default">
                        <div class="panel-body">                           
                            <ul class="otros-equipos slider-multiple tooltip-hover">
                                <asp:Repeater ID="rptListaEquipos" runat="server">
                                    <ItemTemplate>
                                        <li class="li-item" data-toggle="tooltip" title="<%# Eval("nombre") %>">
                                            <a href="#ver equipo">
                                                <asp:Panel ID="panelLogoEquipo" runat="server" Visible="<%# ((Entidades.Equipo)Container.DataItem).tieneImagen() ? true : false %>">
                                                    <a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                                        <img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenMediana() %>" class="img-responsive center-block">
                                                    </a>  
                                                </asp:Panel>  
                                                <asp:Panel ID="panelCamisetaEquipo" runat="server" Visible="<%# ((Entidades.Equipo)Container.DataItem).tieneImagen() ? false : true %>">
                                                    <a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                                    <div id="Div1" class="camiseta-equipo" runat="server">
                                                      <div>
                                                        <i class="flaticon-football114" style="color:<%# Eval("colorCamisetaPrimario") %>"></i>
                                                      </div><!--
                                                   --><div class="segunda-mitad">
                                                        <i class="flaticon-football114" style="color:<%# Eval("colorCamisetaSecundario") %>"></i>
                                                      </div>
                                                    </div>
                                                        </a>
                                                </asp:Panel>     
                                                
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>                                
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- END equipos de la edicion -->
            
            </div>
        </div>
        <!-- End Content Central -->
      </section>
      <!-- contentPages-->
</asp:Content>
