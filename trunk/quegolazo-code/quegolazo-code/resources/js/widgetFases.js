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

    //crea la estrucctura incial para presentar una fase
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
        var contenedorControles = $("<div/>", { class: 'form-group' }).attr("id", "controlesFase" + numeroDeFase);
        var labelFixture = $("<label/>").text("Tipo de Fixture:");
        var labelCantidad = $("<label/>").attr("id", "lbl-cantidad-Fase" + numeroDeFase).text("Cantidad de equipos:");
        var comboTipoFixture = createDropDownList("ddlTipoFixtureFase" + numeroDeFase, this.options.tiposDeFixture).change(function () { widget.ocultarOMostrarBotones($(this)); });;
        var comboCantidades = createDropDownList("ddlCantidad-Fase" + numeroDeFase, this.options.cantEquipos);

        labelFixture.appendTo(contenedorControles);
        comboTipoFixture.appendTo(contenedorControles);        
        labelCantidad.appendTo(contenedorControles);
        comboCantidades.appendTo(contenedorControles);
        contenedorControles.appendTo(cuerpoDeLaFase);
        cuerpoDeLaFase.appendTo(contenedorDeLaFase);
        linkTitulo.appendTo(contenedorTitulo);
        contenedorTitulo.appendTo(headerContenedor);
        headerContenedor.appendTo(contenedorGeneral);
        contenedorDeLaFase.appendTo(contenedorGeneral);
        contenedorGeneral.appendTo(panelFases);
        widget.agregarFase();
        widget.ocultarOMostrarBotones($(comboTipoFixture));        
    },
    agregarGrupo: function (numFase, equipos) {
        var widget = this;
        var nuevoGrupo = {
            idGrupo: widget.options.fases[numFase - 1].grupos.length + 1,
            idFase: numFase,
            idEdicion: widget.options.idEdicion,
            equipos: equipos
        };
        widget.options.fases[numFase - 1].grupos.push(nuevoGrupo);
        
    },
    ocultarOMostrarBotones: function (combo) {
        var widget = this;        
        var numFase = combo.attr("id").substr(combo.attr("id").length - 1);
        $("#ddlCantidad-Fase" + numFase).remove();
        var comboCant ;
        if (combo.val().indexOf("TCT") >= 0) {
            $("#lbl-cantidad-Fase" + numFase).text("Cantidad de Grupos: ");
            comboCant = createDropDownList("ddlCantidad-Fase"+numFase, widget.obtenerGruposPosibles(widget.options.fases[numFase - 1].equipos.length));
        } else {
            $("#lbl-cantidad-Fase" + numFase).text("Cantidad de Equipos: ");
            comboCant = createDropDownList("ddlCantidad-Fase"+numFase, widget.options.cantEquipos);
        }
        comboCant.appendTo($("#controlesFase" + numFase));
        //agrego el evento de cambio al combo de cantidades de equipos
        comboCant.on("change", function () {            
            widget.presentarFase(combo.val(), comboCant.val(), numFase);
        });  

        //agrego el evento de cambio al combo de tipo de fixture
        combo.on("change", function () {           
            widget.presentarFase(combo.val(), comboCant.val(), numFase);
        });

        widget.presentarFase(combo.val(), comboCant.val(), numFase);

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
    mostrarEquiposEngrupo: function (numFase, cantidadGrupos) {
        
        var widget = this;        
        var grupos;
        var cantidadEquiposPorGrupo = parseInt(widget.options.equiposDeLaEdicion.length / cantidadGrupos);
        if (numFase == 1) {            
            grupos = widget.armarGruposParaPresentar(widget.options.equiposDeLaEdicion, cantidadGrupos);
            for (var i = 0; i < grupos.length; i++) {
                widget.agregarGrupo(numFase, grupos[i]);
                var panelGrupo = widget.crearPanel(i + 1);
                //$("#panelFase" + numFase).append(panelGrupo);
                var listaNueva = $("<ul/>", { class: "connectedSortable ui-sortable col-md-5 gruposFase" }).attr("data-id-fase", numFase).attr("data-idGrupo", widget.options.fases[numFase - 1].grupos[i].idGrupo);
                for (var j = 0 ; j < grupos[i].length; j++) {
                    var unEquipo = $("<li/>").attr("data-id-equipo", grupos[i][j].idEquipo).text(grupos[i][j].nombre);
                    unEquipo.appendTo(listaNueva);
                }                              
                listaNueva.appendTo($("#panelFase" + numFase));
            }
            $(".connectedSortable").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();
        } else {
            alert("loco aguanta, por ahora solo una fase ok?");
        }

    },
    crearPanel: function (idGrupo) {
        return "<div id='panelGrupo"+idGrupo+"' class='panel panel-default col-md-5 gruposFase'><div class='panel-heading'><h4 class='panel-title'>Grupo "+idGrupo+" </h4></div></div>"
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
    },
    armarGruposParaPresentar: function (listaDeEquipos, cantGrupos) {
        //esta variable es la cantidad de equipos por grupo sin tener en cuenta los sobrantes
        var cantidadEquiposxGrupo = parseInt(listaDeEquipos.length / cantGrupos);
        var sobrantes = listaDeEquipos.length - cantidadEquiposxGrupo*cantGrupos ;
        var limite = listaDeEquipos.length - sobrantes;
        var grupos = [];
        var indice = 0;
        //se divide en una cantidad de grupos fija, cada uno con la misma cantidad de equipos
        for (var i = 0; i < cantGrupos; i++) {
            var grupo = [];
            for (var j = 0; j < cantidadEquiposxGrupo; j++) {
                grupo.push(listaDeEquipos[indice]);
                indice++;
            }
            grupos.push(grupo);
        }

        //ahora se distribuyen los equipos sobrantes uno en cada grupo
        indice = listaDeEquipos.length - sobrantes;        
        for (var i = 0; i < sobrantes; i++) {
            grupos[i].push(listaDeEquipos[indice]);
            indice++;
        }        
      
        return grupos;
    },
    obtenerGruposPosibles: function (cantidadEquipos) {
        var i = 1;
        var cantidades = [];        
        while (i < (cantidadEquipos / 2) + 1) {
            var opcion = { value: 0, text: "" };
            if (cantidadEquipos / i >= 2) {
                opcion.value = i;
                opcion.text = i;
                cantidades.push(opcion);
            }
            i++;
        }
        return cantidades;
    },
    presentarFase: function (tipoFixture, cantidades, numeroFase) {
        $(".connectedSortable").remove();
        var widget = this;
        if (tipoFixture.indexOf("TCT") >= 0) {
            widget.mostrarEquiposEngrupo(numeroFase, cantidades);
        }
    }

    
});