 <%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="quegolazo_code.admin.index" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeaderAdminTorneo" runat="server">
   
    <script src="../resources/js/jquery.percentageloader-0.1.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <!-- Contenido -->
    <asp:Literal ID="LitEdicion" runat="server"></asp:Literal>
    <div class="container padding-top">
        <div class="col-md-12">
            <div class="well">
                <fieldset class="vgSeleccionarEdicion">
                    <div class="col-md-5">
                        <div id="selectEdiciones">
                            <asp:DropDownList ID="ddlEdiciones" runat="server" CssClass="form-control" required="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSeleccionarEdicion" runat="server" Text="Seleccionar Edición" CssClass="btn btn-success btn-sm CausesValidation vgSeleccionarEdicion" OnClick="btnSeleccionarEdicion_Click" />
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="icon-size flaticon-football31"></span>
                    Tabla de Posiciones
                   
                </div>
                <div class="panel-body">
                   
                  <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-5">EQUIPO</th>
                                            <th class="col-md-1">PJ</th>
                                            <th class="col-md-1">PG</th>
                                            <th class="col-md-1">PE</th>
                                            <th class="col-md-1">PP</th>
                                            <th class="col-md-1">GF</th>
                                            <th class="col-md-1">GC</th>
                                            <th class="col-md-1">PTS</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptPosiciones" runat="server">
                                            <ItemTemplate>
                                                <tr>      
                                                    <td><%--<img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " />--%><%# Eval("Equipo") %></td>                                              
                                                    <td><%# Eval("PJ") %></td>  
                                                    <td><%# Eval("PG") %></td>  
                                                    <td><%# Eval("PE") %></td>  
                                                    <td><%# Eval("PP") %></td>  
                                                    <td><%# Eval("GF") %></td>  
                                                    <td><%# Eval("GC") %></td>  
                                                    <td><%# Eval("Puntos") %></td>  
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinequipos" runat="server" visible="false">
                                            <td colspan="12">Todavia no hay partidos registrados.</td>
                                        </tr>
                                    </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="icon-size flaticon-flaming"></span>
                    Goleadores de la Edición
                    <asp:Literal ID="Literal1" Text="" runat="server"></asp:Literal>
                </div>
                <div class="panel-body">
                   
                    <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="col-md-5">JUGADOR</th>
                                            <th class="col-md-1">EQUIPO</th>
                                            <th class="col-md-1">GOLES</th>                                       
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptGoleadores" runat="server">
                                            <ItemTemplate>
                                                <tr>                                                       
                                                    <td><%# Eval("JUGADOR") %></td>
                                                    <td><%# Eval("EQUIPO") %></td>                                               
                                                    <%--<td><img src="<%# ((Entidades.Equipo)Container.DataItem).obtenerImagenChicha() %>" class="img-responsive" alt="" style="height:22px; max-width:30px; " /> <%# Eval("EQUIPO") %></td>--%>  
                                                    <td><%# Eval("GOLES") %></td>                                                   
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="sinpartidosGoleadores" runat="server" visible="false">
                                            <td colspan="4">Todavia no hay partidos registrados.</td>
                                        </tr>
                                    </tbody>
                    </table>
                    
                      
                </div>
            </div>
        </div>
    </div>
    <div class="container">
     <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Avance de la Edición
                </div>
                <div class="panel-body" >
                    <div id="avanceTorneo"></div>
                </div>
            </div>
        </div>
  
      <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Avance de la Fecha 
                </div>
                <div class="panel-body" >
                    <div id="avanceFecha"></div>
                </div>
            </div>
        </div>


        <div class="col-md-6 ">
             <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-stats"></span>
                    Última fecha  : Fecha <asp:Literal  ID="ltFecha" runat="server" />
                </div>
                <div class="panel-body" >
                     <table class="table">
                                    <thead>
                                        <tr>                                            
                                            <th class="col-md-3">LOCAL</th>
                                            <th class="col-md-1">VS</th>
                                            <th class="col-md-3">VISITANTE</th>
                                            <th class="col-md-2">ARBITRO</th>
                                            <th class="col-md-1">ESTADO</th> 
                                        </tr>
                                    </thead>
                                    <tbody class="tablaFiltro">
                                        <asp:Repeater ID="rptFecha" runat="server">
                                            <ItemTemplate>
                                                <tr>              
                                                    <td><%# Eval("local") %></td>  
                                                    <td>vs</td>  
                                                    <td><%# Eval("visitante") %></td>  
                                                    <td><%# Eval("arbitro") %></td>  
                                                    <td><%# Eval("estado") %></td>                                                      
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="noFixture" runat="server" visible="false">
                                            <td colspan="4">Todavia no hay un fixture generado.</td>
                                        </tr>
                                    </tbody>
                    </table>
                </div>
                <asp:GridView ID="gvFixture" runat="server" CssClass="table"></asp:GridView>
            </div>
          
        </div>


        </div>
    <!-- Contenido -->
    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
      <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
    </asp:Panel>
</asp:Content>
