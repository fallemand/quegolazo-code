 $(document).ready(function($) {    
     var themes = [
         {
             bodyClass: "none fixed",
             colorDeFondo: "rgb(95, 165, 78)",
             colorDestacado: "css/skins/cocoa.css",
             colorHeader: "rgb(90, 71, 57)",
             estiloPagina: "layout-boxed-margin",
             patronDeFondo: "url(/torneo/img/bg-theme/c3.png)",
             patronHeader: "url(/torneo/img/bg-theme/8.png)",
             theme: "/torneo/css/bootstrap/darkly.css"
         },
         {
             bodyClass: "none",
             colorDeFondo: "rgb(40, 38, 41)",
             colorDestacado: "css/skins/blue.css",
             colorHeader: "rgb(0, 126, 168)",
             estiloPagina: "layout-boxed-margin",
             patronDeFondo: "url(/torneo/img/bg-theme/18.png)",
             patronHeader: "url(/torneo/img/bg-theme/8.png)",
             theme: "/torneo/css/bootstrap/cyborg.css"
         }, {
             bodyClass: "none",
             colorDeFondo: "rgb(40, 38, 41)",
             colorDestacado: "css/skins/orange.css",
             colorHeader: "rgb(40, 38, 41)",
             estiloPagina: "layout-wide",
             patronDeFondo: "url(/torneo/img/bg-theme/18.png)",
             patronHeader: "url(/torneo/img/bg-theme/3.png)",
             theme: "/torneo/css/bootstrap/superhero.css"
         }, {
             bodyClass: "none",
             colorDeFondo: "rgb(219, 219, 219)",
             colorDestacado: "css/skins/green.css",
             colorHeader: "rgb(112, 113, 117)",
             estiloPagina: "layout-wide",
             patronDeFondo: "url(/torneo/img/bg-theme/a1.png)",
             patronHeader: "url(/torneo/img/bg-theme/3.png)",
             theme: "/torneo/css/bootstrap/slate.css"
         }, {
             bodyClass: "none",
             colorDeFondo: "rgb(112, 113, 117)",
             colorDestacado: "css/skins/pink.css",
             colorHeader: "rgb(219, 219, 219)",
             estiloPagina: "layout-boxed-margin",
             patronDeFondo: "url(/torneo/img/bg-theme/1.png)",
             patronHeader: "url(/torneo/img/bg-theme/14.png)",
             theme: "/torneo/css/bootstrap/bootstrap.css"
         }
     ];
     function interface() {
    // Skin value
    var skin = "green"; // green (default), red ,yellow,purple,blue, orange, purple, pink, cocoa, custom 
    // Boxed value
    var layout = "layout-boxed-margin"; // layout-semiboxed(default), layout-boxed, layout-boxed-margin ,layout-wide
    //Only in boxed version 
    var bg = "none";  // none (default), bg1, bg2, bg3, bg4, bg5, bg6, bg7, bg8, bg9, bg10, bg11 
    $(".skin").attr("href", "css/skins/"+ skin + ".css");
    $("#layout").addClass(layout);	
    $("body").addClass(bg);   
    return false;    
	}
     //cargo el colorpicker	
	$(".cp-background").colorPicker();
	function cargarEstilosVisuales(estilos) {
	    $('#layout').removeClass().addClass(estilos.estiloPagina);
	    $('body').css('background-image', estilos.patronDeFondo);
	    $('body').css('background-color', estilos.colorDeFondo);
	    $('#colorFondo .colorPicker-picker').css('background-color', estilos.colorDeFondo);
	    $('.headerbox').css('background-image', estilos.patronHeader);
	    $('#colorHeader .colorPicker-picker').css('background-color', estilos.colorHeader);
	    $('.headerbox').css('background-color', estilos.colorHeader);
	    $('#theme-options ul.backgrounds li').css('background-color', estilos.colorDeFondo);
	    $('#theme-options ul.backgrounds-h li').css('background-color', estilos.colorHeader);
	    $(".skin").attr("href", estilos.colorDestacado);
	    $('.wide').removeClass('active');
	    $('.boxed').removeClass('active');
	    $('.boxed-margin').removeClass('active');
	    $('.semiboxed').removeClass('active');
	    $('.' + estilos.estiloPagina.replace("layout-", "")).addClass('active');
	    $("#theme").attr("href", estilos.theme);
	    $('body').attr('class', estilos.bodyClass);
	}
	interface();
    //si tiene estilos guardados, los cargamos
    try {
        if (configuracion == null)
            setDefaultTheme();
    } catch (ReferenceError) {
        setDefaultTheme();
    }
    cargarEstilosVisuales(configuracion);

    function setDefaultTheme() {
        configuracion = {
            bodyClass: "none fixed",
            colorDeFondo: "rgb(95, 165, 78)",
            colorDestacado: "css/skins/green.css",
            colorHeader: "rgb(255, 255, 255)",
            estiloPagina: "layout-boxed-margin",
            patronDeFondo: "url(/torneo/img/bg-theme/c11.png)",
            patronHeader: "url(/torneo/img/bg-theme/19.png)",
            theme: "/torneo/css/bootstrap/sandstone.css"
        };
    }
	

	//=================================== Theme Options ====================================//

	$('.wide').click(function() {
		$('.boxed').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'none');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-wide');
		configuracion.estiloPagina = "layout-wide";
	});
	$('.semiboxed').click(function() {
		$('.wide').removeClass('active');
		$('.boxed').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-wide').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-semiboxed');
		configuracion.estiloPagina = "layout-semiboxed";
	});
	$('.boxed').click(function() {
		$('.wide').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed-margin').removeClass('layout-wide').addClass('layout-boxed');
		configuracion.estiloPagina = "layout-boxed";
	});
	$('.boxed-margin').click(function() {
		$('.boxed').removeClass('active');
		$('.wide').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-wide').removeClass('layout-boxed').addClass('layout-boxed-margin');
		configuracion.estiloPagina = "layout-boxed-margin";
	});

  

     //=================================== Skins Changer ====================================//

	    //Theme Selector
	$("#themeSelector .bootstrap").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/bootstrap.css");
	    return false;
	});
	$("#themeSelector .cyborg").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/cyborg.css");
	    return false;
	});
	$("#themeSelector .darkly").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/darkly.css");
	    return false;
	});
	$("#themeSelector .flatly").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/flatly.css");
	    return false;
	});
	$("#themeSelector .sandstone").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/sandstone.css");
	    return false;
	});
	$("#themeSelector .slate").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/slate.css");
	    return false;
	});
	$("#themeSelector .hero").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/superhero.css");
	    return false;
	});
	$("#themeSelector .yeti").click(function () {
	    $("#theme").attr("href", "/torneo/css/bootstrap/yeti.css");
	    return false;
	});

    // Color changer
	$("#colorchanger .red").click(function () {
	    $(".skin").attr("href", "css/skins/red.css");	    
	    return false;
	});
    $("#colorchanger .red-dark").click(function () {
	    $(".skin").attr("href", "css/skins/red-dark.css");	    
	    return false;
	});
	$("#colorchanger .blue").click(function () {
	    $(".skin").attr("href", "css/skins/blue.css");	    
	    return false;
	});
    $("#colorchanger .blue-dark").click(function () {
	    $(".skin").attr("href", "css/skins/blue-dark.css");	    
	    return false;
	});
	$("#colorchanger .yellow").click(function () {
	    $(".skin").attr("href", "css/skins/yellow.css");	    
	    return false;
	});
	$("#colorchanger .green").click(function () {
	    $(".skin").attr("href", "css/skins/green.css");	    
	    return false;
	});
    $("#colorchanger .green-dark").click(function () {
	    $(".skin").attr("href", "css/skins/green-dark.css");	    
	    return false;
	});
	$("#colorchanger .orange").click(function () {
	    $(".skin").attr("href", "css/skins/orange.css");	    
    	return false;
	});
    $("#colorchanger .orange-dark").click(function () {
	    $(".skin").attr("href", "css/skins/orange-dark.css");	    
    	return false;
	});
	$("#colorchanger .purple").click(function () {
	    $(".skin").attr("href", "css/skins/purple.css");	    
	    return false;
	});
	$("#colorchanger .pink").click(function () {
	    $(".skin").attr("href", "css/skins/pink.css");	    
	    return false;
	});
	$("#colorchanger .cocoa").click(function () {
	    $(".skin").attr("href", "css/skins/cocoa.css");	    
        return false;
	});
    $("#colorchanger .grey").click(function () {
	    $(".skin").attr("href", "css/skins/grey.css");	    
        return false;
	});
    $("#colorchanger .lynch").click(function () {
	    $(".skin").attr("href", "css/skins/lynch.css");	    
        return false;
	});
    $("#colorchanger .black").click(function () {
	    $(".skin").attr("href", "css/skins/black.css");	    
        return false;
	});


	//=================================== Background Options ====================================//
	
	$('#theme-options ul.backgrounds li').click(function(){
	    var $bgSrc = $(this).css('background-image');
		if ($(this).attr('class') == 'bgnone')
		    $bgSrc = "none";
		if ($(this).hasClass('fixed')) {
            $bgSrc = $bgSrc.replace('-sm', '');
		    $('body').addClass('fixed');
        }
		else 
		    $('body').removeClass('fixed');
		$('body').css('background-image', $bgSrc);
		$.cookie('background', $bgSrc);
		$.cookie('backgroundclass', $(this).attr('class').replace(' active',''));
		$(this).addClass('active').siblings().removeClass('active');
		configuracion.patronDeFondo = $bgSrc;
	});
	$('#theme-options ul.backgrounds-h li').click(function () {
	    var $bgSrc = $(this).css('background-image');
	    if ($(this).attr('class') == 'bgnone')
	        $bgSrc = "none";
	    $('.headerbox').css('background-image', $bgSrc);
	    $.cookie('background-header', $bgSrc);
	    $.cookie('backgroundclass-header', $(this).attr('class').replace(' active', ''));
	    $(this).addClass('active').siblings().removeClass('active');
	    configuracion.patronHeader = $bgSrc;
	});
	//=================================== Panel Options ====================================//
	$('.openclose').click(function(){		
	    cerrarPanel();
	});
	function cerrarPanel() {
	    if ($('#theme-options').css('left') == "-220px") {
	        $left = "0px";
	        $.cookie('displayoptions', "0");
	    } else {
	        $left = "-220px";
	        $.cookie('displayoptions', "1");
	    }
	    $('#theme-options').animate({
	        left: $left
	    }, {
	        duration: 50
	    });
	    $("#msjeAjax").hide();
	}
	$('#cerrarConfig').click(function () {
	    cerrarPanel();
	});    
	$("#colorPicker_palette-0 div").click(function () {
	    var $bgSrc = $(this).css('background-color');
	    if ($(this).attr('class') == 'bgnone')
	        $bgSrc = "none";
	    $('body').css('background-color', $bgSrc);
	    $.cookie('background-color', $bgSrc);
	    $('#theme-options ul.backgrounds li').css('background-color', $bgSrc);
	    configuracion.colorDeFondo = $bgSrc;
	});
	$("#colorPicker_palette-1 div").click(function () {
	    var $bgSrc = $(this).css('background-color');
	    if ($(this).attr('class') == 'bgnone')
	        $bgSrc = "none";
	    $('.headerbox').css('background-color', $bgSrc);
	    $.cookie('background-color', $bgSrc);
	    $('#theme-options ul.backgrounds-h li').css('background-color', $bgSrc);
	    configuracion.colorHeader = $bgSrc;
	});
	$(function(){
		$('#theme-options').fadeIn();
		$bgSrc = $.cookie('background');
		$('body').css('background-image',$bgSrc);
		if ($.cookie('displayoptions') == "1")
		{
			$('#theme-options').css('left','-220px');
		} else if ($.cookie('displayoptions') == "0") {
			$('#theme-options').css('left','0');
		} else {
			$('#theme-options').delay(800).animate({
				left: "-220px"
			},{
				duration: 500				
			});
			$.cookie('displayoptions', "1");
		}
		$('#theme-options ul.backgrounds').find('li.' + $.cookie('backgroundclass')).addClass('active');
		$('#theme-options ul.backgrounds-h').find('li.' + $.cookie('backgroundclass-header')).addClass('active');
	});

  });

 function guardarConfiguracion() {
     configuracion.patronDeFondo = $('body').css('background-image').replace('http://' + window.location.hostname, '');
     configuracion.colorHeader = $('.headerbox').css('background-color');
     configuracion.patronHeader = $('.headerbox').css('background-image').replace('http://' + window.location.hostname, '');
     configuracion.colorDeFondo = $('body').css('background-color');
     configuracion.colorDestacado = $(".skin").attr("href").replace('http://' + window.location.hostname, '');
     configuracion.estiloPagina = $('#layout').attr("class");
     configuracion.theme = $("#theme").attr("href").replace('http://' + window.location.hostname, '');
     configuracion.bodyClass = $('body').attr('class');
      $.ajax({
          type: "POST",
          url: "configurarSitio.aspx/guardarConfiguracion",
          contentType: "application/json",
          dataType: "json",
          async: false,
          global: false,
          data: "{configuracion :" + JSON.stringify(configuracion) + " }",
          success: function (response) {
              if (response.d.indexOf("MAS TARDE") != 0)
                  $("#msjeAjax").text(response.d).addClass("alert").addClass("alert-success").show('slow');
                  else
                  $("#msjeAjax").text(response.d).addClass("alert").addClass("alert-danger").show('slow');
          },
          error: function (response) {
              console.log(response);
          }
      });
  } 
