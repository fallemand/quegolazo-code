<%@ Page Title="" Language="C#" MasterPageFile="~/torneo/torneoMaster.Master" AutoEventWireup="true" CodeBehind="principal.aspx.cs" Inherits="quegolazo_code.torneo.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headTorneoMasterContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMasterTorneo" runat="server">
      <!-- Theme-options -->
       <div id="themeOptions" runat="server">
        <div id="theme-options" style="z-index:9999">
            <div class="openclose"></div>
            <div class="title">
            <span>OPCIONES DEL TEMA</span>
            </div>
            <span>Color destacado</span>
            <ul id="colorchanger">      
                <li><a class="colorbox red" href="about0e99.html?theme=red" title="Red Skin"></a></li>
                <li><a class="colorbox blue" href="aboutca00.html?theme=blue" title="Blue Skin"></a></li>                    
                <li><a class="colorbox yellow" href="about2746.html?theme=yellow" title="Yellow Skin"></a></li>
                <li><a class="colorbox green" href="aboutaf91.html?theme=green" title="Green Skin"></a></li>
                <li><a class="colorbox orange" href="aboutceb0.html?theme=orange" title="Orange Skin"></a></li>
                <li><a class="colorbox purple" href="about938c.html?theme=purple" title="Purple Skin"></a></li>
                <li><a class="colorbox pink" href="abouta820.html?theme=pink" title="Pink Skin"></a></li>
                <li><a class="colorbox cocoa" href="about6788.html?theme=cocoa" title="Cocoa Skin"></a></li>
            </ul>           
            <span>Patron del header</span>
            <ul class="backgrounds-h">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg1"></li>
                    <li class="bg2"></li>
                    <li class="bg3"></li>
                    <li class="bg4 "></li>
                    <li class="bg5"></li> 
                    <li class="bg6"></li>
                    <li class="bg7"></li>                                  
                </ul>  
            <span>Colores:</span>
            <ul class="layout-style text-center">    
                <li>
                    Fondo
                    <div id="colorFondo" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>    
                <li >
                    Header
                    <div id="colorHeader" class="conBorde">
                    <div class="cp-background"></div>
                    </div>
                </li>                                 
            </ul> 
            <span>ESTILO DE PAGINA</span>
            <ul class="layout-style">      
                <li class="wide">ANCHO</li>
                <li class="semiboxed active">SEMI CAJA</li> 
                <li class="boxed">CAJA</li> 
                <li class="boxed-margin">CAJA C/MARGEN</li>               
            </ul>               
            <div class="patterns">               
            <span>PATRON DEL FONDO</span>
            <ul class="backgrounds">
                    <li class="bgnone" title="None - Default"></li>
                    <li class="bg1"></li>
                    <li class="bg2"></li>
                    <li class="bg3"></li>
                    <li class="bg4 "></li>
                    <li class="bg5"></li> 
                    <li class="bg6"></li>
                    <li class="bg7"></li>
                    <li class="bg8"></li>
                    <li class="bg9 "></li>
                    <li class="bg10"></li> 
                    <li class="bg11"></li> 
                    <li class="bg12"></li> 
                    <li class="bg13"></li> 
                    <li class="bg14"></li> 
                    <li class="bg15"></li> 
                    <li class="bg16"></li> 
                    <li class="bg17"></li> 
                    <li class="bg18"></li> 
                    <li class="bg19"></li> 
                    <li class="bg20"></li> 
                    <li class="bg21"></li> 
                    <li class="bg22"></li> 
                    <li class="bg23"></li>              
                </ul>  
            </div>
            <ul class="layout-style">    
                <li class="btn btn-danger" id="cerrarConfig">Cerrar</li>    
                <li onclick="guardarConfiguracion()" class="btn btn-primary">Guardar</li>                                 
            </ul> 
            <div class="col-xs-12">
            <span id="msjeAjax" class="text-center"></span>  
            </div>                       
        </div>
        <!-- End Theme-options --> 
       </div> 
     <!-- Section Title -->           
            <section class="section-title img-about">
                <div class="overlay-bg"></div>
                <div class="container">
                    <h1>About Us</h1>
                </div>
            </section>
            <!-- End Section Title --> 

            <!-- Section Area - Content Central -->
            <section class="content-info">

                <div class="crumbs">
                    <div class="container">
                        <ul>
                            <li><a href="index-2.html">Home</a></li>
                            <li>/</li>
                            <li>About Us</li>                                       
                        </ul>
                    </div>        
                </div>

                <div class="semiboxshadow text-center">
                    <img src="../resources/torneo/img/img-theme/shp.png" class="../resources/torneo/img-responsive" alt="">
                </div>

                <!-- Content Central -->
                <div class="container padding-top">
                    <div class="row">
                        
                        <!-- content Players --> 
                        <div class="col-md-12">                     
                            <!-- Panel Box -->
                            <div class="panel-box">
                                <div class="titles">
                                    <h4><i class="fa fa-group"></i>Our Team</h4>
                                </div>
                                <!-- Players --> 
                                <div class="row">
                                    <div class="col-md-12">
                                        <!-- Item Players-->  
                                        <ul id="players-carousel" class="players">
                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/1.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->

                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/2.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->

                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/3.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->

                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/4.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->

                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/5.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->

                                            <!-- Item Player -->  
                                            <li class="item-player">
                                                <img src="../resources/torneo/img/players/6.jpg" alt="" class="../resources/torneo/img-responsive">
                                                <div class="info-player">
                                                    <h4><a href="#">Robert</a></h4>
                                                    <h5><span>Bmx Sport</span></h5>
                                                    <div class="overlay-player">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmodn culpa qui officia deserunt mollit anim id est laborum.</p>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- End Player post -->
                                        </ul>
                                        <!-- End Item Players-->  
                                    </div>
                                </div>
                                <!-- End Players -->                                
                            </div>  
                            <!-- End Panel Box --> 
                        </div>
                        <!-- End Players -->

                        <div class="col-md-6">
                            <!-- Info -->
                            <div class="panel-box">
                                <div class="titles">
                                    <h4><i class="fa fa-cogs"></i>Skins</h4>
                                </div>
                                <!-- Skins --> 
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="pro_bar">
                                            <h5 class="nocaps">Futbol Soccer</h5>
                                            <div id="progress_bar" class="ui-progress-bar ui-container">
                                                <div class="ui-progress">
                                                    <span class="ui-label"><b class="value">90%</b></span>
                                                </div>
                                            </div><!-- end section -->
                                            
                                            <br>
                                            
                                            <h5 class="nocaps">Bmx Sport</h5>
                                            <div id="progress_bar2" class="ui-progress-bar ui-container">
                                                <div class="ui-progress two">
                                                    <span class="ui-label"><b class="value">72%</b></span>
                                                </div>
                                            </div><!-- end section -->
                                            
                                            <br>
                                            
                                            <h5 class="nocaps">Motocross</h5>
                                            <div id="progress_bar3" class="ui-progress-bar ui-container">
                                                <div class="ui-progress three">
                                                    <span class="ui-label"><b class="value">80%</b></span>
                                                </div>
                                            </div><!-- end section -->
                                            
                                            <br>
                                            
                                            <h5 class="nocaps">Golfd</h5>
                                            <div id="progress_bar4" class="ui-progress-bar ui-container">
                                                <div class="ui-progress four">
                                                    <span class="ui-label"><b class="value">94%</b></span>
                                                </div>
                                            </div><!-- end section -->
                                        </div>
                                    </div>
                                </div>
                                <!-- End Skins --> 
                            </div>  
                            <!-- End Info-->
                        </div>

                        <!-- Accordion -->
                        <div class="col-md-6">
                            <!-- Info -->
                            <div class="panel-box">
                                <div class="titles">
                                    <h4><i class="fa fa-coffee"></i>Who we are</h4>
                                </div>
                                <!-- Skins --> 
                                <div class="row">
                                    <div class="accrodation">
                                        <!-- section 1 -->
                                        <span class="acc-trigger"><a href="#">Mision</a></span>
                                        <div class="acc-container" style="display: none;">
                                            <div class="content">
                                                Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                                            </div>
                                        </div>
                                      
                                        <!-- section 2 -->
                                        <span class="acc-trigger"><a href="#">Vision</a></span>
                                        <div class="acc-container" style="display: none;">
                                            <div class="content">
                                                Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                                            </div>
                                        </div>
                                   
                                        <!-- section 3 -->
                                        <span class="acc-trigger"><a href="#">What we offer?</a></span>
                                        <div class="acc-container" style="display: none;">
                                            <div class="content">
                                                Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                                            </div>
                                        </div>
                                        
                                        <!-- section 4 -->
                                        <span class="acc-trigger active"><a href="#">Our services</a></span>
                                        <div class="acc-container" style="display: block;">
                                            <div class="content">
                                                Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End Skins --> 
                            </div>  
                            <!-- End Info-->
                        </div>
                        <!-- End Accordion -->

                        <!-- About Template-->
                        <div class="col-md-12">
                            <!-- Info -->
                            <div class="panel-box">
                                <div class="titles">
                                    <h4><i class="fa fa-rocket"></i>About Template</h4>
                                </div>
                                <!-- Info ABout --> 
                                <div class="row">
                                    <div class="col-md-4">
                                        <!-- Mini SLide --> 
                                        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                                          <ol class="carousel-indicators">
                                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="1" class=""></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="2" class=""></li>
                                          </ol>
                                          <div class="carousel-inner">
                                            <div class="item active">
                                              <img src="../resources/torneo/img/blog/1.jpg">
                                            </div>
                                            <div class="item">
                                              <img src="../resources/torneo/img/blog/2.jpg">
                                            </div>
                                            <div class="item">
                                              <img src="../resources/torneo/img/blog/3.jpg">
                                            </div>
                                          </div>
                                          <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                                            <span class="glyphicon glyphicon-chevron-left"></span>
                                          </a>
                                          <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                                            <span class="glyphicon glyphicon-chevron-right"></span>
                                          </a>
                                        </div>
                                        <!-- End Mini SLide --> 
                                    </div>
                                    <div class="col-md-8">
                                        <h4>Pellentesque habitantu vulputate</h4>
                                        <p>Pellentesque habitantu vulputate magna eros eu erat. Aliquam erat volutpat. Nam dui mi, tincidunt quis, accumsan porttitor, facilisis luctus, metus Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante.</p>

                                        <p> Donec Mauris placerat eleifend leo. Quisque sit amet est et sapien ullamcorper pharetra. Vestibulum erat wisi, condimentum sed, commodo vitae, ornare sit amet, wisi. Aenean fermentum, elit eget tincidunt condimentum, eros ipsum rutrum orci, sagittis tempus lacus enim ac dui. Donec non enim in turpis pulvinar facilisis. </p>
                                    </div>
                                </div>
                                <!-- End Info ABout --> 
                            </div>  
                            <!-- End Info-->
                        </div>
                        <!-- End About Template-->
                        
                    </div>                     
                </div>  
                <!-- End Content Central -->

                <!-- Sponsors -->
                <div class="section-wide">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <h2>We have earned the trust of <span class="text-resalt">25,869</span> partners.</h2>
                                    <p>Duis non lorem porta,  eros sit amet, tempor sem. semper a tempus et.</p>
                                </div>
                                <ul id="sponsors" class="tooltip-hover">
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/1.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/2.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/3.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/4.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/5.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/3.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/4.png" alt="Image"></a></li>
                                    <li data-toggle="tooltip" title data-original-title="Name Sponsor"> <a href="#"><img src="../resources/torneo/img/sponsors/3.png" alt="Image"></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>  
                <!-- End Sponsors -->  
            </section>
            <!-- End Section Area - Content Central -->
      
        
</asp:Content>
