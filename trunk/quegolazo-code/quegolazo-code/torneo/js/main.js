// THEME OPTIONS.JS
//--------------------------------------------------------------------------------------------------------------------------------
//This is JS file that contains principal fuctions of theme */
// -------------------------------------------------------------------------------------------------------------------------------
// Template Name: Sports Cup- Responsive HTML5  soccer and sports Template.
// Author: Iwthemes.
// Name File: main.js
// Version 1.0 - Created on 20 May 2014
// Website: http://www.iwthemes.com 
// Email: support@iwthemes.com
// Copyright: (C) 2014

$(document).ready(function($) {

	'use strict';

  //=================================== Sticky nav ===================================//

  $(".mainmenu").sticky({topSpacing:0});

  //=================================== Counter  ==============================//

   $('#event-one').countdown('2014/06/12', function(event) {
      var $this = $(this).html(event.strftime(''
      + '<span>%D <br> <small>days</small></span>  '
      + '<span>%H <br> <small>hr</small> </span>  '
      + '<span>%M <br> <small>min</small> </span>  '
      + '<span>%S <br> <small>sec</small></span> '));
   });

   $('#event-two').countdown('2014/09/10', function(event) {
      var $this = $(this).html(event.strftime(''
      + '<span>%D <br> <small>days</small></span>  '
      + '<span>%H <br> <small>hr</small> </span>  '
      + '<span>%M <br> <small>min</small> </span>  '
      + '<span>%S <br> <small>sec</small></span> '));
   });
   $('#event-three').countdown('2014/12/24', function(event) {
      var $this = $(this).html(event.strftime(''
      + '<span>%D <br> <small>days</small></span>  '
      + '<span>%H <br> <small>hr</small> </span>  '
      + '<span>%M <br> <small>min</small> </span>  '
      + '<span>%S <br> <small>sec</small></span> '));
   });
 	
	//=================================== Slide Services  ==============================//
	 
	$(".single-carousel").owlCarousel({
		  items : 1,
		  autoPlay: true,  
    	navigation : true,
    	autoHeight : true,
    	slideSpeed : 200,
    	singleItem: true,
    	pagination : false
	});
  
  //=================================== Slide Proximos Partidos  ==============================//
	 
	$(".proximos-partidos").owlCarousel({
        items : 8,
		autoPlay: true,  
    	navigation : true,
    	autoHeight : true,
    	slideSpeed : 200,
    	pagination : false,
        itemsCustom: [[0, 1],[350, 2], [500, 3],[600, 4], [800, 5], [1000, 6], [1100, 7], [1200, 8]]
	});
    
    //=================================== Slide Otros Equipos  ==============================//
	 
	$(".otros-equipos").owlCarousel({
        items : 10,
		autoPlay: true,  
    	navigation : true,
    	autoHeight : true,
    	slideSpeed : 200,
    	pagination : false,
        itemsCustom: [[0, 1],[350, 3], [500, 5],[600, 6], [800, 7], [1000, 9], [1100, 10], [1200, 11]]
	});

  //=================================== Carousel Blog  ==================================//

  $("#events-carousel").owlCarousel({
       autoPlay: 3200,      
       items : 3,
       navigation: false,
       itemsDesktop : [1199,3],
       itemsDesktopSmall : [1024,3],
       itemsTablet: [1000,2],
       itemsMobile : [480,1],
       pagination: true
   });

  //=================================== Carousel Players  ==================================//

   $("#players-carousel").owlCarousel({
       autoPlay: 3200,      
       items : 4,
       navigation: false,
       itemsDesktopSmall : [1024,3],
       itemsTablet : [768,3],
       itemsMobile : [600,2],
       pagination: true
   });

  //=================================== Carousel Clubs  ==================================//

   $("#clubs-carousel").owlCarousel({
       autoPlay: 3200,      
       items : 1,
       navigation: false,
       singleItem: true,
       pagination: true
   });

   //=================================== Carousel Sponsor  ==================================//

   $("#sponsors").owlCarousel({
       autoPlay: 3200,      
       items : 6,
       navigation: false,
       itemsDesktop : [1199,5],
       itemsDesktopSmall : [1024,4],
       itemsTablet : [768,3],
       itemsMobile : [500,2],
       pagination: true
   });

   //=================================== Carousel Testimonials  ============================//

  $("#testimonials").owlCarousel({
       autoPlay: 3200,      
       items : 3,
       navigation: false,
       itemsDesktop : [1199,3],
       itemsDesktopSmall : [1024,3],
       itemsTablet : [1000,2],
       itemsMobile : [600,1],
       pagination: true
  });

	//=================================== Carousel Twitter  ===============================//
	 
	$(".tweet_list").owlCarousel({
		  items : 1,
		  autoPlay: 3200,  
    	navigation : false,
    	autoHeight : true,
    	slideSpeed : 400,
    	singleItem: true,
    	pagination : true
	});

	//=================================== Subtmit Form  ===================================//

	$('.form-theme').submit(function(event) {  
	     event.preventDefault();  
	     var url = $(this).attr('action');  
	     var datos = $(this).serialize();  
	     	$.get(url, datos, function(resultado) {  
	     	$('.result').html(resultado);  
		});  
 	});

  //=================================== Form Newslleter  =================================//

  $('#newsletterForm').submit(function(event) {  
       event.preventDefault();  
       var url = $(this).attr('action');  
       var datos = $(this).serialize();  
        $.get(url, datos, function(resultado) {  
        $('#result-newsletter').html(resultado);  
    });  
  });  

  //=================================== Ligbox  ===========================================//	

  $(".fancybox").fancybox({
      openEffect  : 'elastic',
      closeEffect : 'elastic',

      helpers : {
        title : {
          type : 'inside'
        }
      }
  });
	
	//=============================  tooltip demo ===========================================//

  $('.tooltip-hover').tooltip({
      selector: "[data-toggle=tooltip]",
      container: "body"
   });

//=================================== Totop  ============================================//

  $().UItoTop({
		scrollSpeed:500,
		easingType:'linear'
	});	

//=================================== Portfolio Filters  ==============================//

  $(window).load(function(){
      var $container = $('.portfolioContainer');
      $container.isotope({
      filter: '*',
          animationOptions: {
          duration: 750,
          easing: 'linear',
          queue: false
  	}
  });
	 
  $('.portfolioFilter a').click(function(){
      $('.portfolioFilter .current').removeClass('current');
      $(this).addClass('current');
       var selector = $(this).attr('data-filter');
       $container.isotope({
        filter: selector,
              animationOptions: {
              duration: 750,
              easing: 'linear',
              queue: false
            }
        });
       return false;
      }); 
   });
});	