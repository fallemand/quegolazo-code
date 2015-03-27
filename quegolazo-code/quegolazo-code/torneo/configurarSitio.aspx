<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configurarSitio.aspx.cs" Inherits="quegolazo_code.torneo.configurarSitio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
        <!-- Basic -->
        <meta charset="utf-8">
        <title>SportsCup - Responsive, Bootstrap 3, HTML5 Soccer And Sports Template</title>
        <meta name="keywords" content="HTML5 Template" />
        <meta name="description" content="World Cup - Responsive HTML5 Template soccer and sports">
        <meta name="author" content="iwthemes.com">  

        <!-- Mobile Metas -->
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <!-- Theme CSS -->
        <link href="../resources/torneo/css/style.css" rel="stylesheet" media="screen">
        <!-- Responsive CSS -->
        <link href="../resources/torneo/css/theme-responsive.css" rel="stylesheet" media="screen">
        <!-- Skins Theme -->
        <link href="#" rel="stylesheet" media="screen" class="skin">
        <link href="../resources/css/colorPicker.css" rel="stylesheet" />
        <!-- Favicons -->
        <link rel="shortcut icon" href="../resources/torneo/img/icons/favicon.ico">
        <link rel="apple-touch-icon" href="../resources/torneo/img/icons/apple-touch-icon.png">
        <link rel="apple-touch-icon" sizes="72x72" href="../resources/torneo/img/icons/apple-touch-icon-72x72.png">
        <link rel="apple-touch-icon" sizes="114x114" href="../resources/torneo/img/icons/apple-touch-icon-114x114.png">  

        <!-- Head Libs -->
        <script src="../resources/torneo/js/modernizr.js"></script>    
        <!-- jQuery local--> 
        <script src="../resources/torneo/js/jquery.js"></script>                
        <script src="../resources/js/jquery.colorPicker.min.js"></script>
         
        <!--[if IE]>
            <link rel="stylesheet" href="../resources/torneo/css/ie/ie.css">
        <![endif]-->

        <!--[if lte IE 8]>
            <script src="../resources/torneo/js/responsive/html5shiv.js"></script>
            <script src="../resources/torneo/js/responsive/respond.js"></script>
        <![endif]-->

        <!-- Skins Changer-->
        <script type="text/javascript" src="http://www.google.com/jsapi"></script>
     <script type="text/javascript" src="../resources/torneo/js/theme-options/theme-options.js"></script>
    </head>

    <body>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" />
        </form>
       
       <!-- Theme-options -->
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

        <!-- layout-->
        <div id="layout">
            <!-- Header-->
            <header>
                <!-- End headerbox-->
                <div class="headerbox">
                    <div class="container">
                        <div class="row">
                            <!-- Logo-->
                            <div class="col-md-3 logo">
                                <a href="index-2.html" title="Return Home">                            
                                    <img src="../resources/torneo/img/logo.png" alt="Logo" class="logo_img">
                                </a>
                            </div>
                            <!-- End Logo-->

                            <!-- Adds Header-->
                            <div class="col-md-9 adds">
                                <a href="http://themeforest.net/user/iwthemes/portfolio?ref=iwthemes" target="_blank">
                                    <img src="../resources/torneo/img/adds/banner.jpg" alt="" class="../resources/torneo/img-responsive">
                                </a>
                            </div>
                            <!-- End Adds Header-->
                        </div>
                    </div>
                </div>
                <!-- End headerbox-->  

                <!-- mainmenu-->
                <nav class="mainmenu">
                    <div class="container">
                        <!-- Menu-->
                        <ul class="sf-menu" id="menu">
                            <li class="selected">
                                <a href="index-2.html">Home</a>
                            </li>                                
                            <li class="current">
                                <a href="about.html">About</a>
                                <ul class="sub-current">
                                    <li>
                                        <a href="about.html">About Us</a>
                                    </li>
                                    <li>
                                        <a href="events.html">Events</a>
                                    </li>
                                    <li>
                                        <a href="club-teams.html">Club Teams</a>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a href="sports.html">Sports</a>
                                <div class="sf-mega">
                                    <div class="col-md-3">
                                        <h4>Sports Navigation</h4>
                                        <ul>                                                
                                            <li><a href="sports.html">Football Soccer</a></li>
                                            <li><a href="sports.html">Motocross</a></li>
                                            <li><a href="sports.html">Bmx</a></li>
                                            <li><a href="sports.html">Skater</a></li>
                                        </ul>
                                    </div>

                                    <div class="col-md-3">
                                      <h4>Club Teams</h4>
                                      <div class="../resources/torneo/img-hover">
                                         <img src="../resources/torneo/img/blog/1.jpg" alt="" class="../resources/torneo/img-responsive">
                                         <div class="overlay"><a href="sports.html">+</a></div>
                                      </div>
                                    </div>

                                    <div class="col-md-3">
                                      <h4>Players And Staff</h4>
                                      <div class="../resources/torneo/img-hover">
                                         <img src="../resources/torneo/img/blog/2.jpg" alt="" class="../resources/torneo/img-responsive">
                                         <div class="overlay"><a href="sports.html">+</a></div>
                                      </div>
                                    </div>

                                    <div class="col-md-3">
                                      <h4>Locations</h4>
                                      <div class="../resources/torneo/img-hover">
                                         <img src="../resources/torneo/img/blog/3.jpg" alt="" class="../resources/torneo/img-responsive">
                                         <div class="overlay"><a href="sports.html">+</a></div>
                                      </div>
                                    </div>
                                </div>
                            </li>
                            <li class="current">
                                 <a href="gallery-4-columns.html">Gallery</a>
                                <ul class="sub-current">
                                    <li>
                                        <a href="gallery-2-columns.html">Gallery 2 Columns</a>
                                    </li>
                                    <li>
                                        <a href="gallery-3-columns.html">Gallery 3 Columns</a>
                                    </li>
                                    <li>
                                        <a href="gallery-4-columns.html">Gallery 4 Columns</a>
                                    </li>
                                </ul>
                            </li>
                            <li class="current">
                                <a href="#">Features</a>
                                <ul class="sub-current"> 
                                    <li class="current">
                                        <a href="#">Pages</a>
                                        <ul class="sub-current">                            
                                            <li><a href="page-full-width.html">Full Width</a></li>
                                            <li><a href="page-left-sidebar.html">Left Sidebar</a></li>
                                            <li><a href="page-right-sidebar.html">Right Sidebar</a></li>
                                            <li><a href="page-404.html">404 Page</a></li>
                                            <li><a href="page-faq.html">FAQ</a></li>
                                            <li><a href="sitemap.html">Sitemap</a></li>
                                         </ul>
                                    </li> 
                                    <li class="current">
                                        <a href="#">Headers</a>
                                        <ul class="sub-current">
                                            <li><a href="feature-header-1.html">Header Version 1</a></li>
                                            <li><a href="feature-header-2.html">Header Version 2</a></li>
                                            <li><a href="feature-header-3.html">Header Version 3</a></li>
                                        </ul>
                                    </li> 
                                    <li class="current">
                                        <a href="#">Footers</a>
                                        <ul class="sub-current">
                                            <li><a href="feature-footer-1.html#footer">Footer Version 1</a></li>
                                            <li><a href="feature-footer-2.html#footer">Footer Version 2</a></li>
                                            <li><a href="feature-footer-3.html#footer">Footer Version 3</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="feature-grid-system.html">Grind System</a></li>
                                    <li><a href="feature-typograpy.html">Tipograpy</a></li>
                                    <li><a href="feature-icons.html">Icons</a></li>
                                    <li><a href="feature-shortcodes.html">Shortcodes</a></li>
                                    <li class="current">
                                        <a href="#">Third Level</a>
                                        <ul class="sub-current">
                                            <li><a href="#">menu item</a></li>
                                            <li><a href="#">menu item</a></li>
                                            <li><a href="#">menu item</a></li>
                                        </ul>
                                    </li>
                                 </ul>
                            </li> 
                            <li class="current">
                                 <a href="single-news.html">News</a>
                                <ul class="sub-current">
                                    <li>
                                        <a href="news-left-sidebar.html">News Lef Sidebar</a>
                                    </li>
                                    <li>
                                        <a href="news-right-sidebar.html">News Right Sidebar</a>
                                    </li>
                                    <li>
                                        <a href="news-no-sidebar.html">News No Sidebar</a>
                                    </li>
                                    <li>
                                        <a href="single-news.html">Single News</a>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a href="contact.html">Contact</a>
                            </li> 
                        </ul>
                        <!-- End Menu-->
                    </div>
                </nav>
                <!-- End mainmenu-->
            </header>
            <!-- End Header-->
            
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
      
            <!-- footer-->
            <footer id="footer">
                <div class="container">

                    <!-- Wiguets Footer-->
                    <div class="row">

                        <!-- Twitter Wiguet-->
                        <div class="col-xs-12 col-sm-7 col-md-3 col-lg-4">
                            <div class="divisor-footer">
                                <i class="fa fa-twitter twit-list"></i>
                                <h4>Latest Tweet</h4>                     
                                <div id="twitter"></div>   
                            </div>
                        </div>
                        <!-- End Twitter Wiguet-->

                        <!-- Tags Wiguet-->
                        <div class="col-xs-12 col-sm-5 col-md-3 col-lg-3">
                            <div class="tags">                              
                                <h4>Tags</h4>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> corporate </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> theme </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> css3 </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> premium </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> html5 </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> business </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> all purpose </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> Js </a>
                                <a href="#" class="#" title="Tags"><i class="fa fa-tag"></i> muse </a>
                            </div>
                        </div>
                        <!-- End Tags Wiguet-->
                        
                        <!-- Links Wiguet-->
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2">                            
                            <h4>Recent Links</h4>
                            <ul class="links">
                                <li><i class="fa fa-check"></i> <a href="#">World Cup</a></li>
                                <li><i class="fa fa-check"></i> <a href="#">Teams Members</a></li>
                                <li><i class="fa fa-check"></i> <a href="#">Soccer Champion</a></li>
                                <li><i class="fa fa-check"></i> <a href="#">Champions</a></li>
                            </ul>                            
                        </div>
                        <!-- End Links Wiguet-->

                        <!-- flickr Wiguet-->
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">                            
                            <h4>Recent flickr</h4>
                            <ul id="flickr" class="thumbs"></ul>                            
                        </div>
                        <!-- End flickr Wiguet-->
                    </div>
                    <!-- End Wiguets Footer-->

                    <!-- Social Icons-->
                    <div class="row">
                        <ul class="social">
                                <li>
                                    <div>
                                        <a href="#" class="facebook">
                                            <i class="fa fa-facebook"></i>
                                        </a>
                                    </div>
                                </li>
                                <li>
                                    <div>
                                        <a href="#" class="twitter-icon">
                                            <i class="fa fa-twitter"></i>
                                        </a>
                                    </div>
                                </li>
                                <li>
                                    <div>
                                        <a href="#" class="vimeo">
                                            <i class="fa fa-vimeo-square"></i>
                                        </a>
                                    </div>
                                </li>
                                <li>
                                    <div>
                                        <a href="#" class="google-plus">
                                            <i class="fa fa-google-plus"></i>
                                        </a>
                                    </div>
                                </li> 
                                <li>
                                    <div>
                                        <a href="#" class="youtube">
                                            <i class="fa fa-youtube"></i>
                                        </a>
                                    </div>
                                </li> 
                        </ul>
                    </div> 
                    <!-- End Social Icons-->

                </div>
            </footer>      
            <!-- End footer-->

            <!-- footer Down-->
            <div class="footer-down">
                <div class="container">
                    <div class="row">
                        <div class="col-md-5">
                            <p>&copy; 2014 WordCup . All Rights Reserved.  1995 - 2014</p>
                        </div>
                        <div class="col-md-7">
                            <!-- Nav Footer-->
                            <ul class="nav-footer">
                                <li><a href="#">HOME</a> </li>
                                <li><a href="#">EVENTS</a></li>
                                <li><a href="#">TEAM</a></li> 
                                <li><a href="#">GALLERY</a></li> 
                                <li><a href="#">SPORT</a></li>                
                                <li><a href="#">CONTACT</a></li>
                            </ul>
                            <!-- End Nav Footer-->
                        </div>
                    </div>
                </div>
            </div>
            <!-- footer Down-->

        </div>
        <!-- End layout-->

        <!-- ======================= JQuery libs =========================== -->
       
        <script type="text/javascript" src="../resources/torneo/js/nav/tinynav.js"></script> 
        <script type="text/javascript" src="../resources/torneo/js/nav/hoverIntent.js"></script>   
        <script type="text/javascript" src="../resources/torneo/js/nav/superfish.js"></script> 
        <script src="../resources/torneo/js/nav/jquery.sticky.js" type="text/javascript"></script>    
        <!--Totop-->
        <script type="text/javascript" src="../resources/torneo/js/totop/jquery.ui.totop.js" ></script>  
        <!--Accorodion-->
        <script type="text/javascript" src="../resources/torneo/js/accordion/accordion.js" ></script>  
        <!--Slide-->
        <script type="text/javascript" src="../resources/torneo/js/slide/camera.js" ></script>      
        <script type='text/javascript' src='js/slide/jquery.easing.1.3.min.js'></script> 
        <!-- Maps -->
        <script src="../resources/torneo/js/maps/gmap3.js"></script>              
        <!--Ligbox--> 
        <script type="text/javascript" src="../resources/torneo/js/fancybox/jquery.fancybox.js"></script> 
        <!-- carousel.js-->
        <script src="../resources/torneo/js/carousel/carousel.js"></script>
        <!-- Filter -->
        <script src="../resources/torneo/js/filters/jquery.isotope.js" type="text/javascript"></script>
        <!-- Twitter Feed-->
        <script src="../resources/torneo/js/twitter/jquery.tweet.js"></script> 
        <!-- flickr Feed-->
        <script src="../resources/torneo/js/flickr/jflickrfeed.min.js"></script> 
        <!-- Counter -->
        <script src="../resources/torneo/js/counter/jquery.countdown.js"></script>      
        <!--Theme Options-->        
        <script type="text/javascript" src="../resources/torneo/js/theme-options/jquery.cookies.js"></script>         
        <!-- Bootstrap.js-->
        <script type="text/javascript" src="../resources/torneo/js/bootstrap/bootstrap.js"></script> 
        <!--MAIN FUNCTIONS-->
        <script type="text/javascript" src="../resources/torneo/js/main.js"></script>     
        <!-- ======================= End JQuery libs =========================== -->
    </body>
</html>
