$.widget("quegolazo.generadorDeFases", {
    options: {
        idEdicion: null,
        idTorneo: null,
        equiposDeLaEdicion: [],
        fases: [],
        tiposDeFixture: [{
            "value": "TCT",
            "text": "Todos contra todos"
        }, {
            "value": "TCT-IV",
            "text": "Todos contra todos, ida y vuelta"
        }, {
            "value": "ELIM",
            "text": "Eliminatorio"
        }, {
            "value": "ELIM-IV",
            "text": "Eliminatorio, ida y vuelta"
        }],
        cantEquipos: [{
            "value": "4",
            "text": "4"
        }, {
            "value": "8",
            "text": "8"
        }, {
            "value": "16",
            "text": "16"
        }, {
            "value": "32",
            "text": "32"
        }],
        fases: []
    },

    _create: function () {
        
    },
    crearFase: function () {
        var widget = this;
        var numeroDeFase = widget.options.fases.length + 1;

        //creo todos los elementos html
        var panelFases = $("#accordionFases");
        var contenedorGeneral = $("<div/>", { class: 'panel panel-default' });
        var headerContenedor = $("<div/>", { class: 'panel-heading' });
        var contenedorTitulo = $("<h4/>", { class: 'panel-title' });
        var linkTitulo = $("<a/>").attr("data-togle", "collapse").attr("data-parent", "#accordionFases").attr("href", "#collapse" + numeroDeFase).text("Fase N° " + numeroDeFase);
        var contenedorDeLaFase = $("<div/>", { class: 'panel-collapse collapse in' }).attr("id", "collapse" + numeroDeFase);
        var cuerpoDeLaFase = $("<div/>", { class: 'panel-body' }).attr("id", "panelFase" + numeroDeFase);
        var contenedorControles = $("<div/>", { class: 'form-group' });
        var labelFixture = $("<label/>").text("Tipo de Fixture:");
        var labelCantEquipos = $("<label/>").attr("id", "lbl-cantEquipos-Fase" + numeroDeFase).text("Cantidad de equipos:");
        var comboTipoFixture = createDropDownList("ddlTipoFixtureFase" + numeroDeFase, this.options.tiposDeFixture).change(function () { widget.ocultarOMostrarBotones($(this)); });;
        var botonAregarGrupos = $("<button/>").attr("id", "btn-agregarGrupo-Fase" + numeroDeFase).attr("type", "button").text("Agregar grupo").click(function () { widget.agregarGrupo(numeroDeFase); });
        var comboCantEquipos = createDropDownList("ddlCantEquipos-Fase" + numeroDeFase, this.options.cantEquipos);

        labelFixture.appendTo(contenedorControles);
        comboTipoFixture.appendTo(contenedorControles);
        botonAregarGrupos.appendTo(contenedorControles);
        labelCantEquipos.appendTo(contenedorControles);
        comboCantEquipos.appendTo(contenedorControles);
        contenedorControles.appendTo(cuerpoDeLaFase);
        cuerpoDeLaFase.appendTo(contenedorDeLaFase);
        linkTitulo.appendTo(contenedorTitulo);
        contenedorTitulo.appendTo(headerContenedor);
        headerContenedor.appendTo(contenedorGeneral);
        contenedorDeLaFase.appendTo(contenedorGeneral);
        contenedorGeneral.appendTo(panelFases);
        widget.ocultarOMostrarBotones($(comboTipoFixture));
        widget.agregarFase();
    },
    agregarGrupo: function (numFase) {
        var widget = this;
        var nuevoGrupo = {
            idGrupo: widget.options.fases[numFase - 1].grupos.length + 1,
            idFase: numFase,
            idEdicion: widget.options.idEdicion,
            equipos: []
        };
        widget.options.fases[numFase - 1].grupos.push(nuevoGrupo);
        widget.mostrarEquiposEngrupo(numFase);
    },
    ocultarOMostrarBotones: function (combo) {
        var widget = this;
        var numFase = combo.attr("id").substr(combo.attr("id").length - 1);
        if (combo.val().indexOf("TCT") >= 0) {
            $("#lbl-cantEquipos-Fase" + numFase).css("display", "none");
            $("#ddlCantEquipos-Fase" + numFase).css("display", "none");
            $("#btn-agregarGrupo-Fase" + numFase).css("display", "inline");
        } else {
            $("#lbl-cantEquipos-Fase" + numFase).css("display", "inline");
            $("#ddlCantEquipos-Fase" + numFase).css("display", "inline");
            $("#btn-agregarGrupo-Fase" + numFase).css("display", "none");
        }
    },
    agregarFase: function () {
        var widget = this;
        var numFase = widget.options.fases.length + 1;
        var faseNueva = {
            "idFase": numFase,
            "idEdicion": widget.options.idEdicion,
            "idTorneo": widget.options.idTorneo,
            "equipos": (numFase === 1) ? widget.options.equiposDeLaEdicion : [],
            "grupos": widget.obtenerGruposDeUnaFase(numFase),
            "tipoFixture": widget.obtenerTipoFixtureDeUnaFase(numFase),
            "cantidadDeEquipos": widget.obtenerCantidadDeEquiposDeUnaFase(numFase)
        };
        widget.options.fases.push(faseNueva);
    },
    obtenerGruposDeUnaFase: function (numFase) {
        var grupos = $("#panelFase" + numFase).find(".connectedSortable");
        for (var i = 0; i < grupos.length; i++) {
            var grupo = grupos[i];
        }
        return [];
    },
    obtenerTipoFixtureDeUnaFase: function (numFase) {
        var widget = this;
        var tipoDeFixture = {
            "idTipoFixture": $("#ddlTipoFixtureFase" + numFase).val(),
            "nombre": $("#ddlTipoFixtureFase" + numFase).text()
        };
        return tipoDeFixture;
    },
    obtenerCantidadDeEquiposDeUnaFase: function (numFase) {
        var widget = this;
        if ($("#ddlCantEquipos-Fase" + numFase).css("display") === "inline") {
            return $("#ddlCantEquipos-Fase" + numFase).val();
        }
        else
            return null;
    },
    //crea la estructura html para un grupo
    mostrarEquiposEngrupo: function (numFase) {
        var widget = this;
        var cantGrupos = widget.options.fases[numFase - 1].grupos.length;
        var cantidadEquiposPorGrupo = parseInt(widget.options.equiposDeLaEdicion.length / cantGrupos);
        if (numFase === 1) {
            if (cantGrupos === 1) {
                for (var i = 0; i < cantGrupos; i++) {
                    var listaNueva = $("<ul/>", { class: "connectedSortable ui-sortable" }).css("width", (100 / cantGrupos) + "%").attr("data-id-fase", numFase).attr("data-idGrupo", widget.options.fases[numFase - 1].grupos[i].idGrupo);
                    for (var j = 0 ; j < widget.options.equiposDeLaEdicion.length; j++) {
                        var equipoNuevo = { "idEquipo": widget.options.equiposDeLaEdicion[j].idEquipo, "nombre": widget.options.equiposDeLaEdicion[j].nombre };
                        if (j < widget.options.equiposDeLaEdicion.length / cantGrupos) {
                            var unEquipo = $("<li/>").attr("data-id-equipo", equipoNuevo.idEquipo).text(equipoNuevo.nombre);
                            unEquipo.appendTo(listaNueva);
                        }
                    }
                    listaNueva.appendTo($("#panelFase" + numFase));
                }
            } else {
                alert("Por ahora, solo admitimos un solo grupo..")
                //codigo para los grupos cuando no se que mierda va en los equipos
            }
            $(".connectedSortable").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();
        }

    },
    guardarFasesEnSesion: function () {
        var widget = this;
        widget.armarGrupos();
        $.ajax({
            type: "POST",
            url: "fases.aspx/guardarFases",
            contentType: "application/json",
            dataType: "json",
            async: false,
            data: "{JSONFases :" + JSON.stringify(widget.options.fases) + " }",
            success: function (response) {
                alert("Se guardaron las fases en sesion!" + response);
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    armarGrupos: function () {
        var widget = this;
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            for (var j = 0; j < fase.grupos.length; j++) {
                var grupo = fase.grupos[j];
                grupo.equipos = widget.obtenerEquiposdeUnGrupo(fase.idFase, grupo.idGrupo);
            }
        }
    },
    obtenerEquiposdeUnGrupo: function (numFase, numGrupo) {
        var widget = this;
        var equipos = [];
        //este selector identifica los equipos que pertenecen a un grupo y a una fase determinada.
        var idEquipos = $("ul[data-id-fase][data-idgrupo][data-id-fase='" + numFase + "'][data-idGrupo='" + numFase + "'] li");
        //recorremos todos los id de equipos que pertenecen a un grupo
        for (var i = 0; i < idEquipos.length; i++) {
         //buscamos el equipo con el id determinado y lo guardamos en la lista de equipos
          var equipo =  widget.buscarEquipo(widget.options.fases[numFase - 1].equipos, $(idEquipos[i]).attr("data-id-equipo"));
          equipos.push(equipo);
        }
        return equipos;
    },
    //devuelve un equipo pasando un id como parametro, si no lo encuentra devuelve null
    buscarEquipo: function(listaDeEquipos, idBuscado){
        for (var j = 0; j < listaDeEquipos.length; j++) {
            var equipo = listaDeEquipos[j];
            if (idBuscado == equipo.idEquipo) {
                return equipo;
            }
        }
        return null;
}
});