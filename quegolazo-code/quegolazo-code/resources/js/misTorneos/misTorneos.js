function modificarTorneo(idTorneo, url) {    
        $('#modalTorneoLabel').text("Modifcar Torneo");     
        $('#ContentAdmin_txtUrlTorneo').text(url);
        $('#ContentAdmin_txtUrlTorneo').prop("disabled", true);        
        $('#ContentAdmin_btnResgitrarTorneo').hide();
        $('#logoTorneoPreview').css('background-image', 'url(' + $('#img'+idTorneo).attr('src') + ')');
        $('#logoTorneoPreview').addClass('fileinput-preview thumbnail');

    }
