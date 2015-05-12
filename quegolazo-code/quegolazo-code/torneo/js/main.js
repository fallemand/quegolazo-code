// THEME OPTIONS.JS
//-------------------------------------------------------------------------------------
//This file contains the inicializations of the plugins, and some principal functions.
//-------------------------------------------------------------------------------------
// Author: QueGolazo.
// Name File: main.js
// Website: http://www.quegolazo.com.ar

//================================== Funciones Principales=================================//

//Mostrar Panel de Error
//-------------------------------------------------------------
function showPanelMessage(idPanel, idMensaje, mensaje) {
    setTimeout(function () {
        $('#' + idMensaje).text(mensaje);
        $('#' + idPanel).toggleClass('in');
    }, 1);
};

//Ocultar Panel de Error
//-------------------------------------------------------------
function hidePanelMessage(idPanel) {
    if ($('#' + idPanel).hasClass('in')) {
        setTimeout(function () {
            $('#' + idPanel).removeClass('in');
        }, 1);
        $('#' + idPanel).find(".panel-text").text('');
    }
};

//Cargar Estilos Visuales
//-------------------------------------------------------------
function cargarEstilosVisuales(estilos) {
    if(estilos==null) {
        //Estilo por Defecto
        var estilos = {
            bodyClass: "none fixed",
            colorDeFondo: "rgb(95, 165, 78)",
            colorDestacado: "/torneo/css/skins/red-dark.css",
            colorHeader: "rgb(185, 61, 72)",
            estiloPagina: "layout-boxed-margin",
            patronDeFondo: "url(/torneo/img/bg-theme/c6.png)",
            patronHeader: "url(/torneo/img/bg-theme/8.png)",
            theme: "/torneo/css/bootstrap/bootstrap.css"
        };
    }
    $('#layout').removeClass().addClass(estilos.estiloPagina);
    $('body').css('background-image', estilos.patronDeFondo);
    $('body').css('background-color', estilos.colorDeFondo);
    $('.headerbox').css('background-image', estilos.patronHeader);
    $('.headerbox').css('background-color', estilos.colorHeader);
    $(".skin").attr("href", estilos.colorDestacado);
    $('.wide').removeClass('active');
    $('.boxed').removeClass('active');
    $('.boxed-margin').removeClass('active');
    $('.semiboxed').removeClass('active');
    $('.' + estilos.estiloPagina.replace("layout-", "")).addClass('active');
    $("#theme").attr("href", estilos.theme);
    $('body').attr('class', estilos.bodyClass);
}


$(document).ready(function ($) {

    'use strict';

    //================================== Paneles de Error Close Button=================================//
    $('#panelFracaso .close').bind("click", function(e) {
        hidePanelMessage('panelFracaso');
    });
    $('#panelExito .close').bind("click", function(e) {
        hidePanelMessage('panelExito');
    });

    //================================== Tooltip y Link en Tablas=================================//
    $('.table-fecha tr:not(:has(th))').tooltip({
        title: 'Ver Partido',
        placement: 'right',
        container: 'body'
    }).click(function () {
        window.location = $(this).find('a').attr('href');
    }).hover(function () {
        $(this).toggleClass('hover');
    });

    $('.table-jugadores tr:not(:has(th))').tooltip({
        title: 'Ver Jugador',
        placement: 'right',
        container: 'body'
    }).click(function () {
        window.location = $(this).find('a').attr('href');
    }).hover(function () {
        $(this).toggleClass('hover');
    });

    $('.table-partidos tr:not(:has(th))').tooltip({
        title: 'Ver Partido',
        placement: 'right',
        container: 'body'
    }).click(function () {
        window.location = $(this).find('a').attr('href');
    }).hover(function () {
        $(this).toggleClass('hover');
    });

    //=================================== MaxHeight Tables ===================================//
    //Deja visible el header de todas las tablas
    var tablas = $(".panel-maxheight table");
    for (var i = 0; i < tablas.length; i++) {
        $(tablas[i]).stickyTableHeaders({ scrollableArea: $(tablas[i]).parent(), "fixedOffset": "offset" });
    }
    //hace que el scroll en las tablas detenga el scroll en la pagina
    $('.panel-maxheight').bind('mousewheel DOMMouseScroll', function (e) {
        var e0 = e.originalEvent,
            delta = e0.wheelDelta || -e0.detail;
        this.scrollTop += (delta < 0 ? 1 : -1) * 30;
        e.preventDefault();
    });

    //=================================== MaxHeight Tables ===================================//
    $('.panel-maxheight').slimscroll({
        color: '#999',
        size: '6px',
        width: '100%',
        height: '100%'
    });

    //=================================== Sticky nav ===================================//
    $(".mainmenu").sticky({
        topSpacing: 0
    });


    //=================================== Widget Partido  ==============================//
    $(".single-carousel").owlCarousel({
        items: 1,
        autoPlay: true,
        navigation: true,
        autoHeight: true,
        slideSpeed: 200,
        singleItem: true,
        pagination: false
    });

    //=================================== Slide Proximos Partidos  ==============================//
    $(".proximos-partidos").owlCarousel({
        items: 8,
        autoPlay: true,
        navigation: true,
        autoHeight: true,
        slideSpeed: 200,
        pagination: false,
        itemsCustom: [[0, 1], [350, 2], [500, 3], [600, 4], [800, 5], [1000, 6], [1100, 7], [1200, 8]]
    });

    //=================================== Slide Partidos Fecha Actual  ==============================//
    $(".partidos-fecha-actual").owlCarousel({
        items: 8,
        autoPlay: false,
        navigation: true,
        autoHeight: true,
        slideSpeed: 200,
        pagination: false,
        itemsCustom: [[0, 1], [350, 2], [500, 3], [600, 4], [800, 5], [1000, 6], [1100, 7], [1200, 8]]
    });

    //=================================== Slide Otros Equipos  ==============================//
    $(".otros-equipos").owlCarousel({
        items: 12,
        autoPlay: true,
        navigation: true,
        slideSpeed: 400,
        pagination: false,
        itemsCustom: [[0, 2], [300, 3], [400, 5], [500, 6], [600, 7], [800, 8], [1000, 10], [1100, 11], [1200, 13]]
    });

    //=================================== Slide Otros Judaores  ==============================//
    $(".otros-jugadores").owlCarousel({
        items: 13,
        autoPlay: true,
        navigation: true,
        slideSpeed: 400,
        autoWidth:true,
        pagination: false,
        itemsCustom: [[0, 3], [300, 4], [400, 6], [500, 8], [600, 9], [800, 9], [1000, 11], [1100, 12], [1200, 14]]
    });

    //=================================== Slide Otros Judaores  ==============================//
    $(".fases").owlCarousel({
        autoPlay: true,
        items: 3,
        responsive: true,
        navigation: true,
        slideSpeed: 400,
        autoPlay: false,
        pagination: false,
    });

    //=================================== Carousel Blog  ==================================//
    $("#events-carousel").owlCarousel({
        autoPlay: 3200,
        items: 3,
        navigation: false,
        itemsDesktop: [1199, 3],
        itemsDesktopSmall: [1024, 3],
        itemsTablet: [1000, 2],
        itemsMobile: [480, 1],
        pagination: true
    });

    //=================================== Slide Otros Judaores  ==============================//
    $(".fechas").owlCarousel({
        autoPlay: true,
        items: 8,
        responsive: true,
        navigation: true,
        autoPlay: false,
        slideSpeed: 400,
        pagination: false,
    });

    //=================================== Slide de Equipos eh Home  ==================================//
    $(".equipos-home").owlCarousel({
        autoPlay: 3200,
        items: 8,
        navigation: false,
        itemsDesktop: [1199, 5],
        itemsDesktopSmall: [1024, 4],
        itemsTablet: [768, 3],
        itemsMobile: [500, 2],
        pagination: true
    });

    //=================================== Torneos de QueGolazo 404  ==================================//
    $(".torneos-slide").owlCarousel({
        autoPlay: 3200,
        items: 8,
        navigation: false,
        itemsDesktop: [1199, 5],
        itemsDesktopSmall: [1024, 4],
        itemsTablet: [768, 3],
        itemsMobile: [500, 2],
        pagination: true,
        rewindNav: false,
    });

    //=================================== PopOvers ==================================//
    $(".popover-jugador").popover({
            trigger: "manual",
            html: true,
            animation: true,
            container: '.content-info',
            placement: 'left',
            content: function () {
                return $('#popover-' + $(this).attr('id')).html();
            },
            title: function () {
                return $('#popover-title-' + $(this).attr('id')).html();
            }
        })
        .on("mouseenter", function () {
            var _this = this;
            $(this).popover("show");
            $(".popover").on("mouseleave", function () {
                $(_this).popover('hide');
            });
        }).on("mouseleave", function () {
            var _this = this;
            setTimeout(function () {
                if (!$(".popover:hover").length) {
                    $(_this).popover("hide");
                }
            }, 50);
        });

    //=================================== Ligbox  ===========================================//
    $(".fancybox").fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        helpers: {
            title: {
                type: 'inside'
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
        scrollSpeed: 500,
        easingType: 'linear'
    });

    //=================================== Reload Jquery Plugins after PostBack  ==============================//
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        $(".fechas").owlCarousel({
            items: 8,
            responsive: true,
            navigation: true,
            autoPlay: false,
            slideSpeed: 400,
            pagination: false,
        });

        $(".fases").owlCarousel({
            items: 3,
            responsive: true,
            navigation: true,
            slideSpeed: 400,
            autoPlay: false,
            pagination: false,
        });

        $('.tooltip.in').removeClass('in');

        $('.tooltip-hover').tooltip({
            selector: "[data-toggle=tooltip]",
            container: "body"
        });
    };

    //=================================== Portfolio Filters  ==============================//

    $(window).load(function () {
        var $container = $('.portfolioContainer');
        $container.isotope({
            filter: '*',
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });

        $('.portfolioFilter a').click(function () {
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

//=================================== Counter  ==============================//
function cargarCounter(fechaPartido){
    $('#counter-proximo-partido').countdown(fechaPartido , function (event) {
        var $this = $(this).html(event.strftime('' +
            '<span>%D <br> <small>días</small></span>  ' +
            '<span>%H <br> <small>horas</small> </span>  ' +
            '<span>%M <br> <small>min</small> </span>  ' +
            '<span>%S <br> <small>seg</small></span> '));
});
}   