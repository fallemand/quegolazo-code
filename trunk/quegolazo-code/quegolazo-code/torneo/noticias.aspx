<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="noticias.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web15" %>
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
                     <asp:Repeater ID="rptUltimasNoticias" runat="server">
                       <ItemTemplate>
                       <div class="col-md-6">
                        <!-- Recent Post -->
                        <div class="panel panel-default panel-noticias">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-sm-9 col-xs-8">
                                        <h3 class="panel-title">
                                          <a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion, ((Entidades.Noticia)Container.DataItem).idNoticia)%>"><%# Eval("titulo") %></a>
                                         </h3>
                                    </div>
                                    <div class="col-sm-3 col-xs-4">
                                        <div class="label label-md theme-bg-color">
                                           <%# ((Entidades.Noticia)Container.DataItem).categoria.nombre %>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="panel-body">                               
                                <!-- Post Item -->                               
                                <div class="post-item">
                                    <div class="row box-noticias">
                                        <div class="imagen-noticia imagen-noticia-thumb">
                                            <div class="img-hover">
                                                <img src="<%# ((Entidades.Noticia)Container.DataItem).obtenerImagenMediana() %>" alt="" class="img-responsive">
                                                <div class="overlay"><a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion, ((Entidades.Noticia)Container.DataItem).idNoticia)%>">+</a></div>
                                            </div>
                                        </div>                                                                                  
                                            <p class="data-info"><%# DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Day.ToString()+" de "+Utils.GestorExtra.nombreMes(DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Month)+" , "+DateTime.Parse(((Entidades.Noticia)Container.DataItem).fecha.ToString()).Year.ToString() %></p><!-- <i class="fa fa-comments"></i><a href="#">0</a> --> 
                                            <p><%# Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Substring(0,Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length >= 110 ? 110 : Utils.HtmlRemoval.StripTagsRegexCompiled(Eval("descripcion").ToString()).Length)  %>... <a href="<%# Logica.GestorUrl.urlNoticia(nickTorneo, idEdicion, ((Entidades.Noticia)Container.DataItem).idNoticia)%>">Leer Más [+]</a></p>                                        
                                    </div>
                                </div>                               
                                <!-- End Post Item -->                                 
                            </div>
                         </div>
                        <!-- End Recent Post -->
                       </div>
                        </ItemTemplate>
                      </asp:Repeater>
                     <div id="msjNoticias" runat="server" class="panel panel-default" visible="false">
                        <div class="panel-body">
                            <span>Aún no existen noticias registradas para esta edicion...</span>
                        </div>
                     </div>
                     
                   </div>
                    <!-- End content Left -->

                </div>
            </div>
            <!-- End Content Central -->

 

      </section>

</asp:Content>
