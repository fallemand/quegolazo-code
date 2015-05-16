
$('body').on('keyup', '#filtro', function () {
    if ($(this).val().length > 0) {
        $('.panel-collapse').collapse('show');
        $('.panel-title').attr('data-toggle', '');
    }
    else {
        $('.panel-collapse').collapse('hide');
        $('.panel-title').attr('data-toggle', 'collapse');
    }
    var rex = new RegExp($(this).val(), 'i');
    $('.tablaFiltro tr').hide();
    $('.tablaFiltro tr').filter(function () {
        return rex.test($(this).text());
    }).show();
});

function filtrarPosiciones(idGrupo) {
    $('#tabla-posiciones tbody tr').hide();
    $('#tabla-posiciones tbody tr').filter(function () {
        return $(this).find('td:last-child').text() == idGrupo;
    }).show('fast');
};

function EndRequestHandler(sender, args) {
    cbPenalesClick('ContentAdmin_ContentAdminTorneo_cbPenales');
};
$(document).ready(function () {
    $(document).on("click", "#tabla-posiciones > tbody > tr", function () {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }       
    });
    $("#tabla-posiciones tbody tr input[type='checkbox']").change(function (e) {
        if ($(this).is(":checked")) {
            $(this).closest('tr').addClass("success");
            $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val() + $(this).val() + ',');
        } else {
            $(this).closest('tr').removeClass("success");
            $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val().replace($(this).val() + ',', ''));
        }
        actualizarCantidades();
    });    
});

function actualizarCantidades() {
    var arr = $("#hfEquiposSeleccionados").val().split(",");
    var valor = $.grep(arr, function (a) {
        return a != "";
    }).length;
    $("#spanSeleccionados").text(valor);
}

function ordenarTabla() {
    $('#tabla-posiciones2 tbody').sortable({
        update: function (event, ui) {
            reordenarPosicionesEquipos();
        }
    });
}

function reordenarPosicionesEquipos() {
    var columna = $('#tabla-posiciones2 tr td:nth-child(1)');
    for (var i = 0; i < columna.length; i++) {
        //$(columna[i]).text((i + 1) + 'º');
        $(columna[i]).html('<strong>' + (i + 1) + 'º' + '</strong>');
        $(columna[i]).css('font-size', '17px');
    }
}

function enviarPosicionesEquipos() {
    var vector = [];
    var ids = $('#tabla-posiciones2 tr td:nth-child(11)');
    for (var i = 0; i < ids.length; i++) {
        vector.push($(ids[i]).text());
    }
    $.ajax({
        type: "POST",
        url: "fechas.aspx/guardarPosicionesEquipos",
        contentType: "application/json",
        dataType: "json",
        async: false,
        data: "{idEquipos :" + JSON.stringify(vector) + " }",
        success: function (response) {
            //Si hubo un error, hago esto
            if (response.d.StatusCode != 200) {
                //widget.mostrarMensajeDeError(response.d.StatusDescription);
                //acá va cuando se produce un error
            } else {
                //closeModal(modalSeleccionarGanadores);
                //respuesta = true;
                //$("#panelFracaso").hide();
            }
        },
        error: function (response) {
            //widget.mostrarMensajeDeError(response.responseJSON.Message);
        }
    });
}


function clickBotonCerrar() {

}
function clickBotonConfirmar() {

}
function clickSiguiente() {
    $("#imgProgreso").show();
    $.ajax({
        type: "POST",
        url: "fechas.aspx/finalizarFase",
        contentType: "application/json",
        dataType: "json",
        async: false,
        global: false,
        data: "{idEquipos :" + JSON.stringify($("#hfEquiposSeleccionados").val()) + " }",
        success: function (response) {
            //Si hubo un error, hago esto
            if (response.d.indexOf("Error") >= 0) {
                if (response.d.indexOf("CantidadEquiposInvalida") >= 0)
                  openModal('modalCambioEnCantidades');
                else if (response.d.indexOf("MenosDeDosEquipos") >= 0) 
                    showPanelMessage('panelFracaso', 'mensajeFracaso', "Debe Seleccionar al menos 2 equipos para configurar la siguiente fase.");
                else
                    showPanelMessage('panelFracaso', 'mensajeFracaso', response.d);
            } else {
                var jsonFases = jQuery.parseJSON(response.d);
                $('#contenedorFases').generadorDeFases(jsonFases);
                $('#tituloModal').text("Configurar la nueva Fase");
                $('#btnConfirmar').show();
                $('#btnConfigurarFase').hide();
                $('#panelSeleccionarEquipos').remove();
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
    $("#imgProgreso").hide();

}

//function obtenerGruposDeEquipos() {

//};


function cambioEnEquipos() {
    $('#tituloModal').text("Configurar la nueva Fase");
    $('#btnConfirmar').show();
    $('#btnConfigurarFase').hide();
    $('#panelSeleccionarEquipos').remove();
    openModal("modalFinalizarFase");
}
  

function reiniciarContador() {
    $("#hfEquiposSeleccionados").val("");
    $('#contenedorFases').generadorDeFases('destroy');
    var valor = 0;
    $("#spanSeleccionados").text(valor);
};

function ocultarColumnas() {
    $("#thPTS").hide();
    $("#tdPE").hide();
};
