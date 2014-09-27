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
        this.options.fases = [];
    },

    //crea la estrucctura incial para presentar una fase
    crearFase: function () {
        var widget = this;
        var numeroDeFase = widget.options.fases.length + 1;
        if (numeroDeFase != 1) {
            alert("por ahora solo una fase..");
            return;
        }
        //creo todos los elementos html
        var panelFases = $("#accordionFases");
        var contenedorGeneral = $("<div/>", { class: 'panel panel-default' });
        var headerContenedor = $("<div/>", { class: 'panel-heading' });
        var contenedorTitulo = $("<h4/>", { class: 'panel-title' });
        var linkTitulo = $("<a/>").attr("data-toggle", "collapse").attr("data-parent", "#accordionFases").attr("href", "#collapse" + numeroDeFase).text("Fase N° " + numeroDeFase);
        var contenedorDeLaFase = $("<div/>", { class: 'panel-collapse collapse in' }).attr("id", "collapse" + numeroDeFase);
        var cuerpoDeLaFase = $("<div/>", { class: 'panel-body' }).attr("id", "panelFase" + numeroDeFase);
        var contenedorControles = widget.crearControlesDeFase(numeroDeFase);
        contenedorControles.appendTo(cuerpoDeLaFase);
        cuerpoDeLaFase.appendTo(contenedorDeLaFase);
        linkTitulo.appendTo(contenedorTitulo);
        contenedorTitulo.appendTo(headerContenedor);
        headerContenedor.appendTo(contenedorGeneral);
        contenedorDeLaFase.appendTo(contenedorGeneral);
        contenedorGeneral.appendTo(panelFases);
        widget.agregarFase();
        widget.ocultarOMostrarBotones($("#ddlTipoFixtureFase"+numeroDeFase));
    },
    //crea los controles para setear las propiedades de una fase, el tipo de fixture, y la cantidad de equipos o grupos participantes.
    crearControlesDeFase: function (numeroDeFase) {
        var widget = this;
        //primero creo una row para agrupar los controles
        var row = $("<div/>", { class: 'row' }).attr("id", "controlesFase" + numeroDeFase).css("margin-bottom","20px");
        //creo un col-4 para cada control de la fase
        var divTipoFixture = $("<div/>", { class: 'col-md-5', id: 'divTipoFixture' + numeroDeFase });
        var labelFixture = $("<label/>").text("Tipo de Fixture:");
        var comboTipoFixture = createDropDownList("ddlTipoFixtureFase" + numeroDeFase, this.options.tiposDeFixture);
        comboTipoFixture.on("change", function () { widget.ocultarOMostrarBotones($(this)); })
        labelFixture.appendTo(divTipoFixture);
        comboTipoFixture.appendTo(divTipoFixture);
        divTipoFixture.appendTo(row);

        var divCantidades = $("<div/>", { class: 'col-md-4', id: 'divCantidadesFase' + numeroDeFase });
        var labelCantidad = $("<label/>").attr("id", "lbl-cantidad-Fase" + numeroDeFase).text("Cantidad de equipos:");
        var comboCantidades = createDropDownList("ddlCantidad-Fase" + numeroDeFase, this.options.cantEquipos);
        labelCantidad.appendTo(divCantidades);
        comboCantidades.appendTo(divCantidades);
        divCantidades.appendTo(row);


        var divBoton = $("<div/>", { class: 'col-md-3' });
        var botonMostrar = $('<input/>').attr({ type: 'button', id: 'btnMostrarFase' + numeroDeFase, value: 'Mostrar Fase', class: 'btn btn-success' }).css("margin-top", "23px").on("click", function () { widget.presentarFase(numeroDeFase); });;
        botonMostrar.appendTo(divBoton);        
        divBoton.appendTo(row);

        return row;
    },
    //agrega un nuevo grupo a una fase determinada, con la lista de equipos que se pasa por parámetro
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
    //prepara los controles de la fase ségun el tipo de fixture que se eligió, ademas setea la cantidad de grupos en 0.
    ocultarOMostrarBotones: function (combo) {
        var widget = this;
        $(".filaGrupo").remove();
        $("#panelFracaso").hide();
        var numFase = combo.attr("id").substr(combo.attr("id").length - 1);
        widget.options.fases[numFase - 1].grupos = [];
        $("#ddlCantidad-Fase" + numFase).remove();
        var comboCant ;
        if (combo.val().indexOf("TCT") >= 0) {
            $("#lbl-cantidad-Fase" + numFase).text("Cantidad de Grupos: ");
            comboCant = createDropDownList("ddlCantidad-Fase"+numFase, widget.obtenerGruposPosibles(widget.options.fases[numFase - 1].equipos.length));
        } else {
            $("#lbl-cantidad-Fase" + numFase).text("Cantidad de Equipos: ");
            comboCant = createDropDownList("ddlCantidad-Fase"+numFase, widget.options.cantEquipos);
        }
        comboCant.appendTo($("#divCantidadesFase" + numFase));

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
            "nombre": $("#ddlTipoFixtureFase" + numFase +" option:selected").text()
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
        var row;
        var grupos;
        var cantidadEquiposPorGrupo = parseInt(widget.options.equiposDeLaEdicion.length / cantidadGrupos);
        if (numFase == 1) {            
            grupos = widget.armarGruposParaPresentar(widget.options.equiposDeLaEdicion.sort(function() { return 0.5 - Math.random() }), cantidadGrupos);
            //recorremos todos los grupos, previamente borramos los grupos que se han agregado antes.
            widget.options.fases[numFase - 1].grupos = [];
            for (var i = 0; i < grupos.length; i++) {
                widget.agregarGrupo(numFase, grupos[i]);
                var tablaGrupo = widget.crearTablaParaGrupo(i + 1);
                $("#panelFase" + numFase).append(tablaGrupo);
                var listaNueva = $("<ul/>", { class: "connectedSortable ui-sortable gruposFase" }).attr("data-id-fase", numFase).attr("data-idGrupo", widget.options.fases[numFase - 1].grupos[i].idGrupo);
                for (var j = 0 ; j < grupos[i].length; j++) {
                    var unEquipo = $("<li/>").attr("data-id-equipo", grupos[i][j].idEquipo).text(grupos[i][j].nombre);
                    unEquipo.appendTo(listaNueva);
                }
                //cada dos grupos hago una fila nueva para que se acomoden los grupos 2 por cada fila.
                if ((i + 1) % 2 != 0) {
                    row = $("<div/>", { class: "row filaGrupo" });                    
                }
                tablaGrupo = $("#tablaGrupo" + (i + 1));
                listaNueva.appendTo(tablaGrupo.find("td"));
                tablaGrupo.appendTo(row);
                row.appendTo($("#panelFase" + numFase));
            }
            $(".connectedSortable").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();
        } else {
            alert("loco aguanta, por ahora solo una fase ok?");
        }
    },
    //crea una tabla para presentar el grupo
    crearTablaParaGrupo: function (idGrupo) {
        return "<div id='tablaGrupo" + idGrupo + "' class='col-md-6 divGrupos'> <table  class='table'><thead><tr><th>Grupo " + idGrupo + "</th></tr></thead><tbody><tr><td></td></tr></tbody></table></div>"
     },
    // realiza la llamada final al servidor, mandando como parametro una lista de fases con sus atributos, y guardando las fases en sesión.
    guardarFasesEnSesion: function () {
        var widget = this;
        var respuesta = false;
        widget.armarGrupos();
        if (!widget.validarFasesCorrectas()) {
            widget.mostrarMensajeDeError("Configuración de fases incorrecta, recordá que la diferencia de equipos entre los grupos no debe ser mayor a uno, y que debe haber al menos un grupo.");
            return respuesta;
        }        
        else {
            $.ajax({
                type: "POST",
                url: "fases.aspx/guardarFases",
                contentType: "application/json",
                dataType: "json",
                async: false,
                data: "{JSONFases :" + JSON.stringify(widget.options.fases) + " }",
                success: function (response) {
                    //Si hubo un error, hago esto
                    if (response.d.StatusCode != 200) {
                        widget.mostrarMensajeDeError(response.d.StatusDescription);                       
                    } else {
                        respuesta = true;
                        $("#panelFracaso").hide();
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
            return respuesta;
        }
    },
    //valida que las fases estén generadas por el usuario y que la diferencia de equipos entre los grupos no sea mayor a uno
    validarFasesCorrectas: function () {
        var widget = this;
        if (widget.options.fases.length < 1)
            return false;
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            if (fase.grupos.length < 1)
                return false;
            for (var j = 0; j < fase.grupos.length; j++) {               
                for (var k = 0; k < fase.grupos.length; k++) {
                    // si la diferencia entre cantidades de equipos es mayor a uno, o bien la cantidad de equipos de algun grupo es 1
                    if (Math.abs(fase.grupos[j].equipos.length - fase.grupos[k].equipos.length) > 1 || fase.grupos[j].length < 2)
                        return false;
                    }
            }
        }
        return true;
    },
    //setea los grupos en la opcion del widget, guardando los grupos en cada fase, basandose en lo que generó el usuario en la interfaz
    armarGrupos: function () {
        var widget = this;
        //guardo los grupos segun lo uqe arm el usuario
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            for (var j = 0; j < fase.grupos.length; j++) {
                var grupo = fase.grupos[j];
                grupo.equipos = widget.obtenerEquiposdeUnGrupo(fase.idFase, grupo.idGrupo);
            }
        }        
    },
    //devuelve un equipo pasando un id como parametro, si no lo encuentra devuelve null
    buscarEquipo: function (listaDeEquipos, idBuscado) {
        for (var j = 0; j < listaDeEquipos.length; j++) {
            var equipo = listaDeEquipos[j];
            if (idBuscado == equipo.idEquipo) {
                return equipo;
            }
        }
        return null;
    },
    //obtiene los equipos de un grupo para una fase determinada, basandose en como los ordenó el usuario en la UI
    obtenerEquiposdeUnGrupo: function (numFase, numGrupo) {
        var widget = this;
        var equipos = [];
        //este selector identifica los equipos que pertenecen a un grupo y a una fase determinada.
        var idEquipos = $("ul[data-id-fase][data-idgrupo][data-id-fase='" + numFase + "'][data-idGrupo='" + numGrupo + "'] li");
        //recorremos todos los id de equipos que pertenecen a un grupo
        for (var i = 0; i < idEquipos.length; i++) {
            //buscamos el equipo con el id determinado y lo guardamos en la lista de equipos
            var equipo =  widget.buscarEquipo(widget.options.fases[numFase - 1].equipos, $(idEquipos[i]).attr("data-id-equipo"));
            equipos.push(equipo);
        }
        return equipos;
    }, 
    //divide una lista de equipos en cierta cantidad de grupos, teniendo en cuenta que como maximo puede haber una diferencia de un equipo entre cada grupo
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
    presentarFase: function (numeroFase) {
        var tipoFixture = $("#ddlTipoFixtureFase" + numeroFase).val();
        var cantidades = $("#ddlCantidad-Fase" + numeroFase).val();
        $(".connectedSortable").remove();
        $(".filaGrupo").remove();
        $("#panelFracaso").hide();
        var widget = this;
        if (tipoFixture.indexOf("TCT") >= 0) {
            widget.mostrarEquiposEngrupo(numeroFase, cantidades);
        }
    },
    //muestra el mensaje que se pasa por parametro, haciendo el efecto de luz ;)
    mostrarMensajeDeError: function (mensaje) {
        $("#msjFracaso").text(mensaje);
        $("#panelFracaso").show();
        $("#panelFracaso").effect("highlight", 500);
    }

});