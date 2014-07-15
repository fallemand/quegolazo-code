function modificarTorneo(idTorneo, url, nombre, descripcion) {    
        $('#modalTorneoLabel').text("Modificar Torneo");
        $('#ContentAdmin_txtUrlTorneo').val(url);
        $('#ContentAdmin_txtDescripcion').val(descripcion);
        $('#ContentAdmin_txtUrlTorneo').prop("disabled", true);
        $('#ContentAdmin_txtNombreTorneo').val(nombre);
        $('#ContentAdmin_btnResgitrarTorneo').hide();
        $('#ContentAdmin_btnModificarTorneo').show();
        $('#imagen-preview').attr('src', $('#img' + idTorneo).attr('src')); 
}
function crearTorneo() {
    $('#modalTorneoLabel').text("Registrar Nuevo Torneo");
    $('#ContentAdmin_txtUrlTorneo').val("");
    $('#ContentAdmin_txtDescripcion').val("");
    $('#ContentAdmin_txtUrlTorneo').prop("disabled", false);
    $('#ContentAdmin_txtNombreTorneo').val("");
    $('#ContentAdmin_btnResgitrarTorneo').show();
    $('#ContentAdmin_btnModificarTorneo').hide();
    $('#imagen-preview').attr('src', "../resources/img/theme/logo-default.png");
}


