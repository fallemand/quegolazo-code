<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
      <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title overlay-bg">
        <div class="container">
            <h1>Equipos</h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>" ><%=torneo.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion) %>" ><%=gestorEdicion.edicion.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipos(nickTorneo,idEdicion) %>">Equipos</a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

        <!-- Content Central -->

        <div class="container padding-top">
            <div class="row mobile-margin-top">
                <asp:Repeater ID="rptEquipos" runat="server" >
                    <ItemTemplate>
                        <!-- Jugador -->
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <!-- Datos del Equipo -->
                            <div class="panel-box bg-dark score principal theme-border box-equipos">
                                <a id='equipo-<%#Eval("idEquipo")%>' class="popover-equipo" href="<%#Logica.GestorUrl.urlEquipo(nickTorneo,idEdicion,int.Parse(Eval("idEquipo").ToString())) %>">
                                    <%# new Logica.GestorEquipo().obtenerEquipoPorId(int.Parse(Eval("idEquipo").ToString())).obtenerImagen(Utils.GestorImagen.GRANDE,"img-responsive center-block") %>
                                    <h3 class="text-center"><%#Eval("equipo") %></h3>
                                </a>
                                <div class="row text-center">
                                    <div class="col-xs-12">
                                        <ul class="list-group">
                                            <li class="list-group-item nopadding-vertical"><span class="flaticon-football95" aria-hidden="true"></span> DT: <%# Eval("directorTecnico")%></li>
                                            <li class="list-group-item nopadding-vertical"><span class="flaticon-football50" aria-hidden="true"></span> Delegado: <%# Eval("delegadoPrincipal")%></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <!-- Popover del Equipo --><%--style="display: none"--%>
                            <div id="popover-equipo-<%#Eval("idEquipo")%>" style="display: none">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-xs-4 widget widget-xs">
                                                <h1><span class="flaticon-football31"></span><%# Eval("Puntos") %></h1>
                                                <span>Puntos</span>
                                            </div>
                                            <div class="col-xs-4 widget widget-xs text-success">
                                                <h1><span class="flaticon-football28"></span><%# Eval("GF") %></h1>
                                                <span>GF</span>
                                            </div>
                                            <div class="col-xs-4 widget widget-xs text-danger">
                                                <h1><span class="flaticon-football28"></span><%# Eval("GC") %></h1>
                                                <span>GC</span>
                                            </div>
                                            <div class="col-xs-4 widget widget-xs">
                                                <h1><span class="flaticon-football86"></span><%# Eval("PG") %></h1>
                                                <span>Ganados</span>
                                            </div>
                                            <div class="col-xs-4 widget widget-xs">
                                                <h1><span class="flaticon-up23"></span><%# Eval("PE") %></h1>
                                                <span>Empatados</span>
                                            </div>
                                            <div class="col-xs-4 widget widget-xs">
                                                <h1><span class="flaticon-football53"></span><%# Eval("PP") %></h1>
                                                <span>Perdidos</spa>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="popover-title-equipo-<%# Eval("idEquipo")%>" style="display: none">
                                <%#Eval("equipo") %>
                                <a href="<%#Logica.GestorUrl.urlEquipo(nickTorneo, idEdicion, int.Parse(Eval("idEquipo").ToString())) %>" class="icon pull-right mail"><i data-toggle="tooltip" title="Ver Equipo!" class="glyphicon glyphicon-eye-open"></i></a>
                            </div>
                            <!-- END Popover del Equipo -->
                        </div>
                        <!-- END Jugador -->
                        <!-- Jugador -->
                    </ItemTemplate>
                </asp:Repeater>
                <div id="sinEquipos" runat="server"  class="alert alert-info col-md-10 col-md-offset-1 mobile-margin-top"  visible="false">
                    <small>No existen equipos registrados para esta edición</small>  
                 </div>
            </div>
        </div>
        <!-- End Content Central -->
      </section>
      <!-- contentPages-->
</asp:Content>
