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
function cbPenalesClick(cbId) {
    if ($('#' + cbId).prop('checked')) {
        $('#ContentAdmin_ContentAdminTorneo_txtPenalesLocal').show('slow');
        $('#ContentAdmin_ContentAdminTorneo_txtPenalesVisitante').show('slow');
    } else {
        $('#ContentAdmin_ContentAdminTorneo_txtPenalesLocal').hide('slow');
        $('#ContentAdmin_ContentAdminTorneo_txtPenalesVisitante').hide('slow');
    }
};
function togglePanel(panelId, idRadioSi, idRadioNo) {
    if ($('#' + idRadioSi).is(':checked')) {
        $('#' + idRadioSi).removeAttr('checked');
        $('#' + idRadioNo).prop('checked', true);
        $('#' + panelId).collapse('hide');
    }
    else {
        $('#' + idRadioNo).removeAttr('checked');
        $('#' + idRadioSi).prop('checked', true);
        $('#' + panelId).collapse('show');
    }
};
function showPanel(panelId) {
    $('#' + panelId).collapse('show');
};

function activaTab(nombreGrupo, nombreTab) {
    $('#' + nombreGrupo + ' a[href="#' + nombreTab + '"]').tab('show');
};
function addClass(id, clase) {
    $('#' + id).addClass(clase);
};
function removeClass(id, clase) {
    $('#' + id).removeClass(clase);
};

function setActiveMenu() {
    $("#menuTorneo li").each(function (index) {
        var href = $("a", this).attr('href');
        var pathname = window.location.pathname;
        if (href == pathname) {
            $(this).addClass('active');
        }
    });
}
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
function mostrarMensajeEnModal(titulo, mensaje) {
       
   var html = " <div class='modal fade bs-example-modal-sm' id='dialogoGenerado' tabindex='-1' role='dialog' aria-labelledby='mySmallModalLabel' aria-hidden='true'>" +
        "<div class='modal-dialog modal-sm'>"+
            "<div class='modal-content'>"+
               " <div class='modal-header'>"+
                    "<button type='button' class='close' data-dismiss='modal'><span aria-hidden='true'>&times;</span><span class='sr-only'>Cerrar</span></button>" +
                    "<h4 class='modal-title' id='myModalLabel'>"+titulo+"</h4>"+
                "</div>"+
                "<div class='modal-body'>"+                    
                    mensaje+
                "</div>"+
                "<div class='modal-footer'>"+
                    "<button type='button' class='btn btn-default' onclick=$('#dialogoGenerado').modal('toggle')>Aceptar</button>" +
              "</div>"+
        "</div>"+
      "</div>";  
   $("body").append(html);
   $("#dialogoGenerado").modal('show');   
   $('#dialogoGenerado').remove();

}