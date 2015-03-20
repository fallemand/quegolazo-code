// THEME OPTIONS.JS
//--------------------------------------------------------------------------------------------------------------------------------
//This is JS file that contains skin, layout Style and bg used in this template*/
// -------------------------------------------------------------------------------------------------------------------------------
// Template Name: Sports Cup- Responsive HTML5  soccer and sports Template.
// Author: Iwthemes.
// Name File: theme-options.js
// Version 1.0 - Created on 20 May 2014
// Website: http://www.iwthemes.com 
// Email: support@iwthemes.com
// Copyright: (C) 2014
// -------------------------------------------------------------------------------------------------------------------------------
/* Selec your skin and layout Style */
var configuracion = {
    colorDeFondo: "",
    patronDeFondo: "",
    colorDestacado: "",
    estiloPagina: "",
    colorHeader: "",
    patronHeader: ""
};
  $(document).ready(function($) {

  
   
	function interface(){

    // Skin value
    var skin = "green"; // green (default), red ,yellow,purple,blue, orange, purple, pink, cocoa, custom 

    // Boxed value
    var layout = "layout-semiboxed"; // layout-semiboxed(default), layout-boxed, layout-boxed-margin ,layout-wide

    //Only in boxed version 
    var bg = "none";  // none (default), bg1, bg2, bg3, bg4, bg5, bg6, bg7, bg8, bg9, bg10, bg11 

    // Theme Panel - disable panel options
    var themepanel = "1"; // 1 (default - enable), 0 ( disable)

    $(".skin").attr("href", "../resources/torneo/css/skins/"+ skin + "/" + skin + ".css");
    $("#layout").addClass(layout);	
    $("body").addClass(bg);   
    $("#theme-options").css('opacity' , themepanel);
    return false;
  }

 	interface();



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
	    configuracion.colorDestacado = "red.css";
	    return false;
	});
	$("#colorchanger .blue").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/blue/blue.css");
	    configuracion.colorDestacado = "blue.css";
	    return false;
	});
	$("#colorchanger .yellow").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/yellow/yellow.css");
	    configuracion.colorDestacado = "yellow.css";
	    return false;
	});
	$("#colorchanger .green").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/green/green.css");
	    configuracion.colorDestacado = "green.css";
	    return false;
	});
	$("#colorchanger .orange").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/orange/orange.css");
	    configuracion.colorDestacado = "orange.css";
    	return false;
	});
	$("#colorchanger .purple").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/purple/purple.css");
	    configuracion.colorDestacado = "purple.css";
	    return false;
	});
	$("#colorchanger .pink").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/pink/pink.css");
	    configuracion.colorDestacado = "pink.css";
	    return false;
	});
	$("#colorchanger .cocoa").click(function () {
	    $(".skin").attr("href", "../resources/torneo/css/skins/cocoa/cocoa.css");
	    configuracion.colorDestacado = "cocoa.css";
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
	    $.cookie('background', $bgSrc);
	    $.cookie('backgroundclass', $(this).attr('class').replace(' active', ''));
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
	}
	$('#cerrarConfig').click(function () {
	    cerrarPanel();
	});
      //colorpicker	
	$(".cp-background").colorPicker();
	$("#colorPicker_palette-0 div").click(function () {
	    var $bgSrc = $(this).css('background-color');
	    if ($(this).attr('class') == 'bgnone')
	        $bgSrc = "none";
	    $('body').css('background-color', $bgSrc);
	    $.cookie('background-color', $bgSrc);
	    configuracion.colorDeFondo = $bgSrc;
	});
	$("#colorPicker_palette-1 div").click(function () {
	    var $bgSrc = $(this).css('background-color');
	    if ($(this).attr('class') == 'bgnone')
	        $bgSrc = "none";
	    $('.headerbox').css('background-color', $bgSrc);
	    $.cookie('background-color', $bgSrc);
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

	});
	
  });

  function guardarConfiguracion() {
      $.ajax({
          type: "POST",
          url: "configurarSitio.aspx/guardarConfiguracion",
          contentType: "application/json",
          dataType: "json",
          async: false,
          global: false,
          data: configuracion,
          success: function (response) {
              $("#msjeAjax").text(response.d);
          },
          error: function (response) {
              console.log(response);
          }
      });
  }
