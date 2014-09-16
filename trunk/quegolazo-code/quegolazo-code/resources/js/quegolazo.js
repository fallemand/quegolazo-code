function modificarTorneo(idTorneo, url, nombre, descripcion) {
    $('#modalTorneoLabel').text("Modificar Torneo");
    $('#ContentAdmin_txtUrlTorneo').val(url);
    $('#ContentAdmin_txtDescripcion').val(descripcion);
    $('#ContentAdmin_txtUrlTorneo').prop("disabled", true);
    $('#ContentAdmin_txtNombreTorneo').val(nombre);
    $('#ContentAdmin_btnResgitrarTorneo').hide();
    $('#ContentAdmin_btnModificarTorneo').show();
    $('#imagen-preview').attr('src', $('#img' + idTorneo).attr('src'));
};
function crearTorneo() {
    $('#modalTorneoLabel').text("Registrar Nuevo Torneo");
    $('#ContentAdmin_txtUrlTorneo').val("");
    $('#ContentAdmin_txtDescripcion').val("");
    $('#ContentAdmin_txtUrlTorneo').prop("disabled", false);
    $('#ContentAdmin_txtNombreTorneo').val("");
    $('#ContentAdmin_btnResgitrarTorneo').show();
    $('#ContentAdmin_btnModificarTorneo').hide();
    $('#imagen-preview').attr('src', "../resources/img/theme/logo-default.png");
};
function closeModal(idModal) {
    $('#' + idModal).modal('hide');
};
function openModal(idModal) {
    $('#' + idModal).modal('show');
};
function togglePanel(panelId) { $('#' + panelId + '').collapse('toggle'); };
function activaTab(nombreGrupo, nombreTab) {
    $('#' + nombreGrupo + ' a[href="#' + nombreTab + '"]').tab('show');
};
function addClass(id, clase) {
    $('#' + id).addClass(clase);
};
function removeClass(id, clase) {
    $('#' + id).removeClass(clase);
};
//function limpiarModalTorneo() {
//    $('.modal-body').find('input[type=text], input[type=password], input[type=number], input[type=email], textarea').val('');
//    $('.modal-body').find('div').removeClass('has-success has-error');
//    $('#ContentAdmin_txtUrlTorneo').prop('disabled', false);
//    $("#ContentAdmin_imagenpreview").attr("src", "../resources/img/theme/logo-default.png");
//    $("#ContentAdmin_btnModificarTorneo").hide();
//    $("#ContentAdmin_btnRegistrarTorneo").show();
//    $("#ContentAdmin_lblTituloModalTorneo").text("Registrar Torneo");
//    $("#error").text("");
//};
function limpiarModalEdicion() {
    $('.modal-body').find('input[type=text], input[type=password], input[type=number], input[type=email], textarea').val('');
    $('.modal-body').find('div').removeClass('has-success has-error');
};
function previewImage(input, idImagen) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#'+idImagen).attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
};
function previewTempImage(idImagen,path) {
    $('#' + idImagen).attr('src', path);
};
function ajaxFileUpload(input) {
    $(document).ajaxStart(function () {
        $("#cargandoImagen").show("slow");
        $("#imagenIncorrecta").hide();
        $("#imagenCorrecta").hide();
    });
    $(document).ajaxStop(function () {
        $("#cargandoImagen").hide();
    });
    $.ajaxFileUpload
    (
        {
            url: 'AjaxFileUploader.ashx',
            secureuri: false,
            fileElementId: input,
            dataType: 'json',
            data: { name: 'logan', id: 'id' },
            success: function (data, status) {
                if (typeof (data.error) != 'undefined') {
                    if (data.error != '') {
                        $("#imagenIncorrecta").show("slow");
                        $("#mensajeErrorImagen").text(data.error);
                    }
                }
                else {
                    $("#imagenCorrecta").show("slow");
                }
            },
            error: function (data, status, e) {
                alert(e);
            }
        }
    )
    return false;
};
//crea un dopdonwlist con un id i las opciones correspondientes
function createDropDownList(id, optionList) {
    var combo = $("<select></select>").attr("id", id).attr("class", "form-control");
    $.each(optionList, function (i, el) {
        combo.append("<option value='" + this.value + "'>" + this.text + "</option>");
    });

    return combo;

}

function shuffle(list) {
    var i, j, t;
    for (i = 1; i < list.length; i++) {
        j = Math.floor(Math.random() * (1 + i));  // choose j in [0..i]
        if (j != i) {
            t = list[i];                        // swap list[i] and list[j]
            list[i] = list[j];
            list[j] = t;
        }
    }
}

function showSubform(id) {
    $('#' + id).toggle("fast", function fildsetActivator() {
        if ($('#' + id).is(":visible"))
            $('#' + id).find('input,select').prop('disabled', false);
        else
            $('#' + id).find('input,select').attr('disabled', 'disabled');
    });
};

function hideSubform(id) {
    $('#' + id).toggle("fast", function fildsetDesactivator() {
        if ($('#' + id).is(":visible"))
            $('#' + id).find('input,select').prop('disabled', false);
        else
            $('#' + id).find('input,select').attr('disabled', 'disabled');
    });
};