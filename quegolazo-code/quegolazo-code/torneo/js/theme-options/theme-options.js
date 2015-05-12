 $(document).ready(function($) {

    //cargo el colorpicker
    $(".cp-background").colorPicker();

    //=================================== Theme Options ====================================//

    $('.wide').click(function() {
        $('.boxed').removeClass('active');
        $('.boxed-margin').removeClass('active');
        $('.semiboxed').removeClass('active');
        $(this).addClass('active');
        $('.patterns').css('display' , 'none');
        $('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-wide');
    });
    $('.semiboxed').click(function() {
        $('.wide').removeClass('active');
        $('.boxed').removeClass('active');
        $('.boxed-margin').removeClass('active');
        $(this).addClass('active');
        $('.patterns').css('display' , 'block');
        $('#layout').removeClass('layout-wide').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-semiboxed');
    });
    $('.boxed').click(function() {
        $('.wide').removeClass('active');
        $('.boxed-margin').removeClass('active');
        $('.semiboxed').removeClass('active');
        $(this).addClass('active');
        $('.patterns').css('display' , 'block');
        $('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed-margin').removeClass('layout-wide').addClass('layout-boxed');
    });
    $('.boxed-margin').click(function() {
        $('.boxed').removeClass('active');
        $('.wide').removeClass('active');
        $('.semiboxed').removeClass('active');
        $(this).addClass('active');
        $('.patterns').css('display' , 'block');
        $('#layout').removeClass('layout-semiboxed').removeClass('layout-wide').removeClass('layout-boxed').addClass('layout-boxed-margin');
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
        $(".skin").attr("href", "/torneo/css/skins/red.css");
        return false;
    });
    $("#colorchanger .red-dark").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/red-dark.css");
        return false;
    });
    $("#colorchanger .blue").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/blue.css");
        return false;
    });
    $("#colorchanger .blue-dark").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/blue-dark.css");
        return false;
    });
    $("#colorchanger .yellow").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/yellow.css");
        return false;
    });
    $("#colorchanger .green").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/green.css");
        return false;
    });
    $("#colorchanger .green-dark").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/green-dark.css");
        return false;
    });
    $("#colorchanger .orange").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/orange.css");
        return false;
    });
    $("#colorchanger .orange-dark").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/orange-dark.css");
        return false;
    });
    $("#colorchanger .purple").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/purple.css");
        return false;
    });
    $("#colorchanger .pink").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/pink.css");
        return false;
    });
    $("#colorchanger .cocoa").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/cocoa.css");
        return false;
    });
    $("#colorchanger .grey").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/grey.css");
        return false;
    });
    $("#colorchanger .lynch").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/lynch.css");
        return false;
    });
    $("#colorchanger .black").click(function () {
        $(".skin").attr("href", "/torneo/css/skins/black.css");
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
        $(this).addClass('active').siblings().removeClass('active');
    });
    $('#theme-options ul.backgrounds-h li').click(function () {
        var $bgSrc = $(this).css('background-image');
        if ($(this).attr('class') == 'bgnone')
            $bgSrc = "none";
        $('.headerbox').css('background-image', $bgSrc);
        $(this).addClass('active').siblings().removeClass('active');
    });
    //=================================== Panel Options ====================================//
    $('.openclose').click(function(){
        togglePanel();
    });
    function togglePanel() {
        if ($('#theme-options').css('left') == "-220px") {
            $left = "0px";
            $('#theme-options ul.backgrounds li').css('background-color', $('body').css('background-color'));
            $('#theme-options ul.backgrounds-h li').css('background-color', $('body').css('background-color'));
            $('#colorFondo .colorPicker-picker').css('background-color', $('body').css('background-color'));
            $('#colorHeader .colorPicker-picker').css('background-color', $('.headerbox').css('background-color'));
        } else {
            $left = "-220px";
        }
        $('#theme-options').animate({
            left: $left
        }, {
            duration: 50
        });
        $("#msjeAjax").hide();
    }
    $('#cerrarConfig').click(function () {
        togglePanel();
    });
    $("#colorPicker_palette-0 div").click(function () {
        var $bgSrc = $(this).css('background-color');
        if ($(this).attr('class') == 'bgnone')
            $bgSrc = "none";
        $('body').css('background-color', $bgSrc);
        $('#theme-options ul.backgrounds li').css('background-color', $bgSrc);
    });
    $("#colorPicker_palette-1 div").click(function () {
        var $bgSrc = $(this).css('background-color');
        if ($(this).attr('class') == 'bgnone')
            $bgSrc = "none";
        $('.headerbox').css('background-color', $bgSrc);
        $('#theme-options ul.backgrounds-h li').css('background-color', $bgSrc);
    });

  });

 function guardarConfiguracion() {
     var configuracion = {
         patronDeFondo : $('body').css('background-image').replace('http://' + window.location.hostname, ''),
         colorHeader : $('.headerbox').css('background-color'),
         patronHeader : $('.headerbox').css('background-image').replace('http://' + window.location.hostname, ''),
         colorDeFondo : $('body').css('background-color'),
         colorDestacado : $(".skin").attr("href").replace('http://' + window.location.hostname, ''),
         estiloPagina : $('#layout').attr("class"),
         theme : $("#theme").attr("href").replace('http://' + window.location.hostname, ''),
         bodyClass : $('body').attr('class'),
     }
      $.ajax({
          type: "POST",
          url: "/torneo/index.aspx/guardarConfiguracion",
          contentType: "application/json",
          dataType: "json",
          async: false,
          global: false,
          data: "{configuracion :" + JSON.stringify(configuracion) + " }",
          success: function (response) {
              if (response.d.indexOf("MAS TARDE") != 0)
                  $("#msjeAjax").text(response.d).addClass("alert").addClass("alert-success").slideDown('slow');
                  else
                  $("#msjeAjax").text(response.d).addClass("alert").addClass("alert-danger").slideDown('slow');
          },
          error: function (response) {
              console.log(response);
          }
      });
  }
