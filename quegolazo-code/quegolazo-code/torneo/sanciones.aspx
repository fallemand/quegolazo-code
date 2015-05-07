<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="sanciones.aspx.cs" Inherits="quegolazo_code.torneo.sanciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1>Sanciones</h1>
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
                    <li><a href="<%= Logica.GestorUrl.urlSanciones(nickTorneo,idEdicion) %>">Sanciones</a></li>                  
                </ul>
            </div>
        </div>
        <div class="container padding-top">

        <div class="row mobile-margin-top"> 
           <div class="col-md-6">
                 <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8 col-xs-5">                                
                                <h4>Tarjetas</h4>
                                
                            </div>
                            <div class="col-md-4 col-xs-7">
                                <input type="text" id="filtroTarjetas" class="pull-right form-control input-xs filtroFixture" placeholder="Filtrar Tarjetas" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th class="col-md-1"></th>
                                                <th class="col-md-3">Equipo</th>
                                                <th class="col-md-4">Jugador</th>
                                                <th class="col-md-2"><span class="flaticon-football103" aria-hidden="true">Amarillas</span></th>
                                                <th class="col-md-2"><span class="flaticon-football103" aria-hidden="true">Rojas</span></th>
                                            </tr>
                                        </thead>
                                        <tbody class="tablaFiltroTarjetas">
                                            <asp:Repeater ID="rptTarjetas" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                         <input hidden="hidden" <%# gestorEquipo.equipo = gestorEquipo.obtenerEquipoPorId(int.Parse(Eval("IDEQUIPO").ToString())) %> />
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
                                                        <td><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,gestorEquipo.equipo.idEquipo) %>"><%# Eval("EQUIPO") %></a></td>
                                                        <td><a href="<%# Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,gestorEquipo.equipo.idEquipo, int.Parse(Eval("IDJUGADOR").ToString())) %>"><%# Eval("JUGADOR") %></a></td>
                                                        <td><%# Eval("AMARILLAS") %></td>
                                                        <td><%# Eval("ROJAS") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinTarjetas" runat="server" visible="false">
                                                <td colspan="7">No hay tarjetas registradas</td>
                                            </tr>
                                        </tbody>
                                    </table>
                    </div>
                </div>

            </div>
                
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row clearfix">
                            <div class="col-md-8 col-xs-5">
                                <h4>Sanciones</h4>                                 
                            </div>
                            <div class="col-md-4 col-xs-7">
                                <input type="text" id="filtroSanciones" class="pull-right form-control input-xs filtroFixture" placeholder="Filtrar Sanciones" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th class="col-md-1">Fecha</th>
                                                <th class="col-md-2">Equipo</th>
                                                <th class="col-md-3">Jugador</th>
                                                <th class="col-md-3">Motivo</th>
                                                <th class="col-md-1"><abbr title="Puntos A Quitar">PAQ</abbr></th>
                                                <th class="col-md-1"><abbr title="Fechas Suspendidas">FS</abbr></th>                                                
                                            </tr>
                                        </thead>
                                        <tbody class="tablaFiltroSanciones">
                                            <asp:Repeater ID="rptSanciones" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("Fecha") %></td>
                                                        <td><a href="<%# Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>"><%# Eval("NombreEquipo") %></a> </td>
                                                        <td><a href="<%# (Eval("idJugador").Equals(System.DBNull.Value)) ? "#" :Logica.GestorUrl.urlJugador(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString()), int.Parse(Eval("idJugador").ToString())) %>"><%# Eval("NombreJugador") %></a></td>
                                                        <td><%# Eval("MotivoSancion") %></td>
                                                        <td><%# Eval("PtosAQuitar") %></td>
                                                        <td><%# Eval("CantFechas") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="sinSanciones" runat="server" visible="false">
                                                <td colspan="7">No hay sanciones registradas</td>
                                            </tr>
                                        </tbody>
                                    </table>
                    </div>
                </div>
            </div>
        </div>
            </div>
     </section>
    <script>
        $('body').on('keyup', '#filtroSanciones', function () {
            var rex = new RegExp($(this).val(), 'i');
            $('.tablaFiltroSanciones tr').hide();
            $('.tablaFiltroSanciones tr').filter(function () {
                return rex.test($(this).text());
            }).show();
        });
        $('body').on('keyup', '#filtroTarjetas', function () {
            var rex = new RegExp($(this).val(), 'i');
            $('.tablaFiltroTarjetas tr').hide();
            $('.tablaFiltroTarjetas tr').filter(function () {
                return rex.test($(this).text());
            }).show();
        });
    </script>
</asp:Content>
