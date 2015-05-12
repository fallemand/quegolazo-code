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

$(document).ready(function ($) {
    $(".torneos-slide").owlCarousel({
        autoPlay: 3200,
        items: 7,
        navigation: false,
        itemsDesktop: [1199, 5],
        itemsDesktopSmall: [1024, 4],
        itemsTablet: [768, 3],
        itemsMobile: [500, 2],
        pagination: true,
        rewindNav: false,
    });

    //=============================  tooltip demo ===========================================//
    $('.tooltip-hover').tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    });
});