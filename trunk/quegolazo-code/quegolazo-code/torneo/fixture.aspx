<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="fixture.aspx.cs" Inherits="quegolazo_code.torneo.fechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
    <script src="/torneo/js/jquery-ui.min.js"></script>   
    <script src="/torneo/js/jquery.bracket.min.js"></script>
    <link href="/torneo/css/jquery.bracket.min.css" rel="stylesheet" />
    <script src="/torneo/js/widgetLlaves.js"></script>
    <script src="/torneo/js/jquery.stickytableheaders.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
    
    <!-- contentPages-->
    <!-- Titulo Sección -->
    <section class="section-title img-about">
        <div class="overlay-bg"></div>
        <div class="container">
            <h1> <%= gestorEdicion.edicion.nombre %> - Fixture</h1>
        </div>
    </section>
    <!-- End Titulo Sección -->

    <!-- Section Area - Content Central -->
    <section class="content-info">
        <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="/<%=nickTorneo%>"><%= gestorTorneo.torneo.nombre %></a></li>
                    <li>/</li>
                    <li><a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>"><%= gestorEdicion.edicion.nombre %></a></li>
                    <li>/</li>
                    <li><a href="/<%=nickTorneo%>/edicion-<%=idEdicion%>/fixture">Fixture</a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>
                
        <!-- Content Central -->
        <div class="container padding-top">

              <asp:Repeater ID="rptFases" runat="server" OnItemDataBound="rptFases_ItemDataBound">
                            <HeaderTemplate>
                        <div class="panel-group" id="fases">
                            </HeaderTemplate>
                            <ItemTemplate>                                
                                <div class="panel panel-default">                                    
                                    <div class="panel-heading panel-heading-master"> 
                                          <div class="row clearfix" id="masterContainer">    
                                            <a data-toggle="collapse" data-parent="#fases" href="#fase-<%# Eval("idFase") %>">                                        
                                            <div class="col-md-7 col-xs-4">    
                                              <h4>                                                     
                                                   <span class="glyphicon glyphicon-plus"></span>                                                      
                                                    Fase <%# Eval("idFase") %>
                                               </h4>                                                                                            
                                            </div>
                                            </a>
                                            <div class="col-md-3 nopadding-left mobile-nopadding-right col-xs-2">
                                                <input type="text" id="filtro" class="pull-right form-control input-xs filtroFixture" placeholder="Filtrar Fechas" />
                                            </div>                                               
                                            <div class="col-md-1 col-xs-4">
                                                <asp:Panel ID="panelEstadoFase" runat="server">
                                                    <span class="label label-big fase-<%# ((Entidades.Fase)Container.DataItem).estado.nombre %>" rel="txtTooltip" title="<%# ((Entidades.Fase)Container.DataItem).estado.descripcion %>" data-placement="left"><%# ((Entidades.Fase)Container.DataItem).estado.nombre %></span>
                                                </asp:Panel>
                                            </div>                                               
                                        </div>                                             
                                    </div>                                    
                                    <asp:Panel ID="panelTCT" runat="server">
                                    <div id='fase-<%# Eval("idFase") %>' class="panel-collapse collapse">
                                        <div class="panel-body">
                                            
                                            <asp:Repeater ID="rptFechas" runat="server" OnItemDataBound="rptFechas_ItemDataBound">
                                                <HeaderTemplate>
                                                <div class="row" id="fechas">
                                                </HeaderTemplate>
                                                <ItemTemplate> 
                                                   <div class="col-md-4">                                                       
                                                        <div class="panel-box panel-fechas">                            
                                                                        <div class="titles row">
                                                                            <div class="col-md-6" >                                                                                
                                                                                <h4>
                                                                                    <a href="#">
                                                                                    <span class="glyphicon glyphicon-zoom-in"></span>
                                                                                    </a>
                                                                                    <%# ((Entidades.Fase)((RepeaterItem)Container.Parent.Parent.Parent).DataItem).tipoFixture.idTipoFixture=="ELIM" ?  Eval("nombre") + " " : "Fecha "+ Eval("idFecha")%>
                                                                                </h4>
                                                                            </div>
                                                                            <div class="col-md-6" >
                                                                                <span class="label estadoFecha fecha-<%# ((Entidades.Fecha)Container.DataItem).estado.nombre%>" rel="txtTooltip" title="<%# ((Entidades.Fecha)Container.DataItem).estado.descripcion %>" data-placement="left">
                                                                                 <%# ((Entidades.Fecha)Container.DataItem).estado.nombre  %>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <!-- Fecha -->
                                                                        <div class="post-item">
                                                                            <div class="row">
                                                                                 <table  class="table .table-condensed tabla-fechas" >
                                                                    <thead>
                                                                        <tr >
                                                                            <th class="col-md-5 text-center">Local</th>
                                                                            <th class="col-md-2 text-center"><span class="glyphicon glyphicon-search"></span></th>
                                                                            <th class="col-md-5 text-center">Visitante</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="tablaFiltro">
                                                                        <asp:Repeater ID="rptPartidos" runat="server" OnItemCommand="rptPartidos_ItemCommand" OnItemDataBound="rptPartidos_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <asp:Panel ID="panelPartidoNormal" runat="server" Visible="false">
                                                                                    <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                        <td class="text-left">
                                                                                            <asp:Label Font-Size="17px" ID="lblPrimerPuesto" class="flaticon-football81" runat="server" Visible="false" title="Final" rel="txtTooltip" data-placement="left"></asp:Label><asp:Label Font-Size="16px" ID="lblTercerPuesto" class="flaticon-football78" runat="server" Visible="false" title="Tercer Puesto" rel="txtTooltip" data-placement="left"></asp:Label>
                                                                                            
                                                                                            <%# ((Entidades.Partido)Container.DataItem).local!=null ? ((Entidades.Partido)Container.DataItem).local.nombre : "" %></td>
                                                                                           <td class="text-center">
                                                                                           <a href="<%# Logica.GestorUrl.urlPartido(nickTorneo,gestorEdicion.edicion.idEdicion,Eval("idPartido").ToString()) %>">
                                                                                               <p class="partido-<%# ((Entidades.Partido)Container.DataItem).estado.nombre %>">   
                                                                                               <%# ((Entidades.Partido)Container.DataItem).golesLocal%>
                                                                                               <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesLocal.ToString()+")" : "" %>
                                                                                             - <%# ((Entidades.Partido)Container.DataItem).golesVisitante%>
                                                                                               <%# (((Entidades.Partido)Container.DataItem).huboPenales==true) ? "("+((Entidades.Partido)Container.DataItem).penalesVisitante.ToString()+")" : "" %>
                                                                                             </p>
                                                                                           </a>                                                                                           
                                                                                        </td>
                                                                                        <td class="text-right"><%# ((Entidades.Partido)Container.DataItem).visitante!=null ? ((Entidades.Partido)Container.DataItem).visitante.nombre : "" %></td>                                                                                       
                                                                                    </tr>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="panelPartidoLibre" runat="server" Visible="false">
                                                                                     <tr class="grupo-<%# Eval("idGrupo") %>">
                                                                                        <td colspan="4">
                                                                                            <asp:Literal ID="litLibre" runat="server" Text=""></asp:Literal></td>                                                                                        
                                                                                    </tr>
                                                                                </asp:Panel>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Panel ID="panelSinPartidos" runat="server">
                                                                            <tr>
                                                                                <td colspan="12">No hay partidos registrados para la fecha</td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                    </tbody>
                                                                </table>
                                                                           </div>
                                                                        </div>
                                                                         <!-- End Fecha -->
                                                        </div>                                             
                                                   </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:Repeater>                                            
                                            <asp:Panel ID="panelSinFechas" runat="server">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <span>No hay fechas registradas para la fase</span>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                  </asp:Panel>
                                    <asp:Panel ID="panelLlaves" runat="server">
                                        <div id="divLlaves<%# Eval("idFase") %>">

                                        </div>
                                    </asp:Panel>
                                </div>                                
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
              <asp:Panel ID="panelSinFases" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <span>No hay fases registradas para la edición</span>
                                </div>
                            </div>
                        </asp:Panel>
            
            <div class="row mobile-margin-top">        
            

            </div>
        </div>
        <!-- End Content Central -->
    
    </section>
    <!-- END contentPages-->

          <script type="text/javascript">              
              $(document).ready(function ($) {
                  
                  //Deja visible el header de todas las tablas
                  var tablas = $(".tabla-fechas");
                  for (var i = 0; i < tablas.length; i++) {
                      $(tablas[i]).stickyTableHeaders({ scrollableArea: $(tablas[i]).parent().parent(), "fixedOffset": "offset"});
                  }
                  //hace que el scroll en las tablas detenga el scroll en la pagina
                  $('.panel-fechas .post-item').bind('mousewheel DOMMouseScroll', function (e) {
                      var e0 = e.originalEvent,
                          delta = e0.wheelDelta || -e0.detail;
                      this.scrollTop += (delta < 0 ? 1 : -1) * 30;
                      e.preventDefault();
                  });
              });
              $('body').on('keyup', '#filtro', function () {
                  if ($(this).val().length > 0) {
                      $('.panel-collapse').collapse('show');
                      $('.panel-title').attr('data-toggle', '');
                  }
                  else {
                      $('.panel-collapse').collapse('hide');
                      $('.panel-title').attr('data-toggle', 'collapse');
                  }
                  var rex = new RegExp($(this).val(), 'i');
                  $('.tablaFiltro tr').hide();
                  $('.tablaFiltro tr').filter(function () {
                      return rex.test($(this).text());
                  }).show();
              });

              function filtrarPosiciones(idGrupo) {
                  $('#tabla-posiciones tbody tr').hide();
                  $('#tabla-posiciones tbody tr').filter(function () {
                      return $(this).find('td:last-child').text() == idGrupo;
                  }).show('fast');
              };

              function filtrarFechasPorGrupo(Grupo) {
                  $('#tabla-fechas tbody tr').hide();
                  $('#tabla-fechas tbody tr').filter(function () {
                      return $(this).find('td:last-child').text() == Grupo;
                  }).show('fast');
              };
              function agrandarLlaves() {
                  $("#divFechas").hide('slow');
                  $("#divLlaves").removeClass('col-md-6', 'slow').addClass('col-md-12');
                  $("#divLlaves").hide();
                  $("#divLlaves").show('slow');
                  $("#btnAgrandar").hide();
                  $("#btnAchicar").show();
              };
              function achicarLlaves() {
                  $("#divFechas").show('slow');
                  $("#divLlaves").removeClass('col-md-12', 'slow').addClass('col-md-6', 800, 'easeOutBounce');
                  $("#divLlaves").hide();
                  $("#divLlaves").show('slow');
                  $("#btnAgrandar").show();
                  $("#btnAchicar").hide();
              };

              
    </script>
        
</asp:Content>
