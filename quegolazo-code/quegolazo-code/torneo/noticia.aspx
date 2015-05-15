<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="noticia.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
        <!-- Section Area - Content Central -->
     <section class="section-title overlay-bg">
        <div class="container">
            <h1>Noticias</h1>
        </div>
    </section>  
      <section class="content-info">
             <div class="crumbs">
            <div class="container">
                <ul>
                    <li><a href="<%= Logica.GestorUrl.urlTorneo(nickTorneo) %>" ><%=torneo.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEdicion(nickTorneo,idEdicion) %>" ><%=edicion.nombre%></a></li>
                    <li>/</li>
                    <li><a href="<%= Logica.GestorUrl.urlEquipos(nickTorneo,idEdicion) %>">Noticias</a></li>
                    <li>/</li>
                    <li><a href="#" "><%=noticia.titulo %></a></li>
                </ul>
            </div>
        </div>

        <div class="semiboxshadow text-center">
            <img src="/torneo/img/img-theme/shp.png" class="img-responsive" alt="">
        </div>

            <!-- Content Central -->
            <div class="container padding-top">
                <div class="row mobile-margin-top">
                    <!-- content Column Left -->
                    <div class="col-md-12 col-sm-12">
                        <!-- Recent Post -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                 <div class="row">
                                    <div class="col-sm-9 col-xs-8">
                                       <h3 class="panel-title"> <%= DateTime.Parse((noticia.fecha.ToString())).Day.ToString()+" de "+Utils.GestorExtra.nombreMes(DateTime.Parse((noticia.fecha.ToString())).Month)+" , "+DateTime.Parse((noticia.fecha.ToString())).Year.ToString() %></h3>
                                    </div>
                                    <div class="col-sm-3 col-xs-4">
                                        <div class="label label-md theme-bg-color">
                                           <%= noticia.categoria.nombre %>
                                        </div>
                                    </div>
                                </div>
                                  </div>
                            <div class="panel-body">
                              
                                <!-- Post Item -->
                                <div class="post-item">
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12">
                                            <div class="imagen-noticia">
                                                <img src="<%= noticia.obtenerImagenGrande() %>" alt="" class="img-responsive">                                                
                                            </div>
                                            <h2><%= noticia.titulo %></h2>
                                            <%= noticia.descripcion %>
                                        </div>
                                       
                                    </div>
                                </div>
                             
                            </div>
                        </div>
                        <!-- End Recent Post -->
                    </div>
                    <!-- End content Left -->

                </div>
            </div>
            <!-- End Content Central -->

 

      </section>

</asp:Content>
