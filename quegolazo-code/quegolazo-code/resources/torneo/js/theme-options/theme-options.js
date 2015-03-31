 $(document).ready(function($) {    
	function interface(){
    // Skin value
    var skin = "green"; // green (default), red ,yellow,purple,blue, orange, purple, pink, cocoa, custom 
    // Boxed value
    var layout = "layout-semiboxed"; // layout-semiboxed(default), layout-boxed, layout-boxed-margin ,layout-wide
    //Only in boxed version 
    var bg = "none";  // none (default), bg1, bg2, bg3, bg4, bg5, bg6, bg7, bg8, bg9, bg10, bg11 
    $(".skin").attr("href", "../resources/torneo/css/skins/"+ skin + "/" + skin + ".css");
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
	}
	interface();
    //si tiene estilos guardados, los cargamos
	if (configuracion != undefined)
	    cargarEstilosVisuales(configuracion);
	

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

	google.setOnLoadCallback(function(){

	'use strict';

    // Color changer
	$("#colorchanger .red").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/red/red.css");	    
	    return false;
	});
	$("#colorchanger .blue").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/blue/blue.css");	    
	    return false;
	});
	$("#colorchanger .yellow").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/yellow/yellow.css");	    
	    return false;
	});
	$("#colorchanger .green").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/green/green.css");	    
	    return false;
	});
	$("#colorchanger .orange").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/orange/orange.css");	    
    	return false;
	});
	$("#colorchanger .purple").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/purple/purple.css");	    
	    return false;
	});
	$("#colorchanger .pink").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/pink/pink.css");	    
	    return false;
	});
	$("#colorchanger .cocoa").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/cocoa/cocoa.css");	    
        return false;
   	});
 });

	//=================================== Background Options ====================================//
	
	$('#theme-options ul.backgrounds li').click(function(){
	var 	$bgSrc = $(this).css('background-image');
		if ($(this).attr('class') == 'bgnone')
			$bgSrc = "none";
		$('body').css('background-image',$bgSrc);
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
      configuracion.colorDestacado = $(".skin").attr("href");
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