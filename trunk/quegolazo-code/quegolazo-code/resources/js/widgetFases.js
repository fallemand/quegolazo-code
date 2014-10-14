﻿$.widget("quegolazo.generadorDeFases", {
    options: {
        idEdicion: null,
        idTorneo: null,
        equiposDeLaEdicion: [],
        fases: [],
        error : false,
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
        }]      
    },

    _create: function () {
        var widget = this;
        widget.cargarEstructuraDeFases();
    },

    //crea la estrucctura incial para presentar todas las fases del campeonato
    agregarNuevaFase: function () {
        var widget = this;
        if (widget.options.fases.length > 0) {
            alert("por ahora solo una fase..");
            return;
        }
        widget.agregarFase();
        widget.crearHtmlFaseNueva(widget.options.fases.length);
        widget.ocultarOMostrarBotones($("#ddlTipoFixtureFase" + widget.options.fases.length));
    },
    crearHtmlFaseNueva: function (numFase) {
        var widget = this;
        var panelFases = $("#accordionFases");
        var contenedorGeneral = $("<div/>", { class: 'panel panel-default' });
        var headerContenedor = $("<div/>", { class: 'panel-heading' });
        var contenedorTitulo = $("<h4/>", { class: 'panel-title' });
        var linkTitulo = $("<a/>").attr("data-toggle", "collapse").attr("data-parent", "#accordionFases").attr("href", "#collapse" + numFase).text("Fase N° " + numFase);
        var contenedorDeLaFase = $("<div/>", { class: 'panel-collapse collapse in' }).attr("id", "collapse" + numFase);
        var cuerpoDeLaFase = $("<div/>", { class: 'panel-body' }).attr("id", "panelFase" + numFase);
        var contenedorControles = widget.crearControlesDeFase(numFase);
        contenedorControles.appendTo(cuerpoDeLaFase);
        cuerpoDeLaFase.appendTo(contenedorDeLaFase);
        linkTitulo.appendTo(contenedorTitulo);
        contenedorTitulo.appendTo(headerContenedor);
        headerContenedor.appendTo(contenedorGeneral);
        contenedorDeLaFase.appendTo(contenedorGeneral);
        contenedorGeneral.appendTo(panelFases);
        $("<div/>").attr("id", "cuerpoFase" + numFase).appendTo(cuerpoDeLaFase);
        //agrego el tooltip al boton
        $("#btnMostrarFase" + numFase).tooltip({ title: "Presiona nuevamente este botón para generar una nueva configuracion de la fase." });
    },
    //crea la estructura html para fases precargadas desde la sesión
    cargarEstructuraDeFases: function () {
        var widget = this;
        //creo todos los elementos html para cada fase
        for (var i = 0; i < widget.options.fases.length; i++) {
            var numFase = i + 1;
            widget.crearHtmlFaseNueva(numFase);
            widget.presentarFase(null, widget.options.fases[i]);
        }
    },
    //crea los controles para setear las propiedades de una fase, el tipo de fixture, y la cantidad de equipos o grupos participantes.
    crearControlesDeFase: function (numFase) {
        var widget = this;
        //primero creo una row para agrupar los controles
        var row = $("<div/>", { class: 'row' }).attr("id", "controlesFase" + numFase).css("margin-bottom","20px");
        //creo un col-4 para cada control de la fase
        var divIzquierda = $("<div/>", { class: 'col-md-5', id: 'divIzquierda' + numFase });
        var labelFixture = $("<label/>").text("Tipo de Fixture:");
        var comboTipoFixture = createDropDownList("ddlTipoFixtureFase" + numFase, this.options.tiposDeFixture);
        comboTipoFixture.on("change", function () { widget.ocultarOMostrarBotones($(this)); })
        labelFixture.appendTo(divIzquierda);
        comboTipoFixture.appendTo(divIzquierda);
        divIzquierda.appendTo(row);
        var divCentro = $("<div/>", { class: 'col-md-4', id: 'divCentroFase' + numFase });
        var labelCantidad = $("<label/>").attr("id", "lbl-cantidad-Fase" + numFase).text("Cantidad de equipos:");        
        labelCantidad.appendTo(divCentro);
        var comboCant = createDropDownList("ddlCantidad-Fase" + numFase, widget.obtenerGruposPosibles(widget.options.fases[numFase - 1].cantidadDeEquipos));
        comboCant.appendTo(divCentro);
        divCentro.appendTo(row);
        var divDerecha = $("<div/>", { class: 'col-md-3' });
        var botonMostrar = $('<input/>').attr({ type: 'button', id: 'btnMostrarFase' + numFase, value: 'Mostrar Fase', class: 'btn btn-success' }).css("margin-top", "23px").on("click", function () { widget.presentarFase(numFase); });
        botonMostrar.appendTo(divDerecha);        
        divDerecha.appendTo(row);
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
        $("#panelFracaso").hide(); //se esconde el panel de fracaso.
        var numFase = combo.attr("id").substr(combo.attr("id").length - 1);
        //elimino los grupos que se crearon anteriormente
        $("#cuerpoFase" + numFase).remove();
        //elimino el popup en caso de error
        $("#btnMostrarFase" + numFase).popover("destroy");
        widget.options.error = false;
        widget.actualizarTipoDeFixture(numFase);        
        if (combo.val().indexOf("TCT") >= 0) {
            $("#divCentroFase" + numFase).show();
            $("#lbl-cantidad-Fase" + numFase).text("Cantidad de Grupos: ");           
        } else {
            $("#divCentroFase" + numFase).hide();
            widget.validarCantidadDeEquiposEliminatorio(numFase);
        }        
       
    },    
    //agrega un objeto fase a las opciones del widget
    agregarFase: function () {
        var widget = this;        
        var numFase = widget.options.fases.length + 1;       
        var faseNueva = {
            "idFase": numFase,
            "idEdicion": widget.options.idEdicion,
            "idTorneo": widget.options.idTorneo,
            "equipos": (numFase === 1) ? widget.options.equiposDeLaEdicion : [],
            "grupos": [],
            "tipoFixture": null,
            "cantidadDeEquipos": (numFase === 1) ? widget.options.equiposDeLaEdicion.length : 0
        };
        widget.options.fases.push(faseNueva);
    },
    //devuelve un objeto TipoFxture con los valores seleccionados en el combo
    obtenerTipoFixtureDeUnaFase: function (numFase) {
        var widget = this;
        var tipoDeFixture = {
            "idTipoFixture": $("#ddlTipoFixtureFase" + numFase).val(),
            "nombre": $("#ddlTipoFixtureFase" + numFase +" option:selected").text()
        };
        return tipoDeFixture;
    },
    //setea el tipo de fixture segun el valor seleccionado por el usuario para una fase determinada
    actualizarTipoDeFixture: function (numFase) {
        var widget = this;
        widget.options.fases[numFase - 1].tipoFixture = widget.obtenerTipoFixtureDeUnaFase(numFase);
    },
    //valida que la cantidad de qeuipos para una fase eliminatoria sea 2,4,8,16,..., si no lo es muestra un popup indicando el error
    validarCantidadDeEquiposEliminatorio: function (numFase) {
        var widget = this;
        var esValido = ($.inArray(widget.options.fases[numFase - 1].equipos.length, [2, 4, 6, 8, 16, 32, 64, 128]) > -1);
        if (!esValido) {
            $("#btnMostrarFase" + numFase).popover({
                container: '#controlesFase'+numFase,
                title: 'Cantidad de equipos inválida.',
                trigger: 'manual',
                content: 'Para fixtures tipo "Eliminatorio" solo se admiten 2,4,8,16,32,64 o 128 equipos.'
            });
            $("#btnMostrarFase" + numFase).popover("show");            
            widget.options.error = true;
        }
    },    
    //crea la estructura html para los grupos de una fase, si el atributo fase es distinto de null, crea toda una estructura nueva, sino crea lo que tenga la fase que se pasa por parametro
    mostrarEquiposEngrupo: function (numFase, cantidadGrupos, fase) {        
        var widget = this;
        //var row;
        numFase = (fase != null) ? fase.idFase : numFase;
        var cantidadEquiposPorGrupo = (cantidadGrupos != null) ? parseInt(widget.options.equiposDeLaEdicion.length / cantidadGrupos) : null;                
        var grupos = (fase!=null) ? fase.grupos : widget.armarGruposParaPresentar(widget.options.equiposDeLaEdicion.sort(function() { return 0.5 - Math.random() }), cantidadGrupos, (fase != null) ? fase.idFase : numFase);
        //recorremos todos los grupos, previamente borramos los grupos que se han agregado antes.
        widget.options.fases[numFase - 1].grupos = grupos;
            for (var i = 0; i < grupos.length; i++) {                
                var tablaGrupo = widget.crearTablaParaGrupo(i + 1);
                $("#cuerpoFase" + numFase).append(tablaGrupo);
                var listaNueva = $("<ul/>", { class: "connectedSortable ui-sortable gruposFase" }).attr("data-id-fase", numFase).attr("data-idGrupo", grupos[i].idGrupo);
                for (var j = 0 ; j < grupos[i].equipos.length; j++) {
                    var unEquipo = $("<li/>").attr("data-id-equipo", grupos[i].equipos[j].idEquipo).text(grupos[i].equipos[j].nombre);
                    unEquipo.appendTo(listaNueva);
                }
                //cada dos grupos hago una fila nueva para que se acomoden los grupos 2 por cada fila.
                if ((i + 1) % 2 != 0) {
                   var row = $("<div/>", { class: "row filaGrupo" });                    
                }
                tablaGrupo = $("#tablaGrupo" + (i + 1));
                listaNueva.appendTo(tablaGrupo.find("td"));
                tablaGrupo.appendTo(row);
                row.appendTo($("#cuerpoFase" + numFase));
            }
            $(".connectedSortable").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();        
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
        var msj=widget.validarFasesCorrectas();
        if (msj !== "OK") {
            widget.mostrarMensajeDeError("ATENCION! "+msj);
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
            return "Debe crear al menos una fase para continuar.";
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            if (fase.grupos.length < 1)
                return "Debe crear al menos un grupo para continuar." ;
            for (var j = 0; j < fase.grupos.length; j++) {               
                for (var k = 0; k < fase.grupos.length; k++) {
                    // si la diferencia entre cantidades de equipos es mayor a uno, o bien la cantidad de equipos de algun grupo es 1
                    if (Math.abs(fase.grupos[j].equipos.length - fase.grupos[k].equipos.length) > 1 || fase.grupos[j].length < 2)
                        return "La diferencia de equipos entre los grupos no debe ser mayor a uno.";
                    }
            }
        }
        return "OK";
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
            for (var j = 0; j < widget.options.fases[numFase-1].grupos.length; j++) {
                var equipo = widget.buscarEquipo(widget.options.fases[numFase - 1].grupos[j].equipos, $(idEquipos[i]).attr("data-id-equipo"));
                if (equipo != null) {
                    equipos.push(equipo);
                    break;
                }
                }
            
        }
        return equipos;
    }, 
    //divide una lista de equipos en cierta cantidad de grupos, teniendo en cuenta que como maximo puede haber una diferencia de un equipo entre cada grupo
    armarGruposParaPresentar: function (listaDeEquipos, cantGrupos, numFase) {
        var widget = this;
        //esta variable es la cantidad de equipos por grupo sin tener en cuenta los sobrantes        
        var cantidadEquiposxGrupo = parseInt(listaDeEquipos.length / cantGrupos);
        var sobrantes = listaDeEquipos.length - cantidadEquiposxGrupo*cantGrupos ;
        var limite = listaDeEquipos.length - sobrantes;
        var grupos = [];
        var indice = 0;
        //se divide en una cantidad de grupos fija, cada uno con la misma cantidad de equipos
        for (var i = 0; i < cantGrupos; i++) {
            var nuevoGrupo = {
                idGrupo: i + 1,
                idFase: numFase,
                idEdicion: widget.options.idEdicion,
                equipos: []
            };
            var grupo = [];
            for (var j = 0; j < cantidadEquiposxGrupo; j++) {
                nuevoGrupo.equipos.push(listaDeEquipos[indice]);
                indice++;
            }
            grupos.push(nuevoGrupo);
        }

        //ahora se distribuyen los equipos sobrantes uno en cada grupo
        indice = listaDeEquipos.length - sobrantes;        
        for (var i = 0; i < sobrantes; i++) {
            grupos[i].equipos.push(listaDeEquipos[indice]);
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
    //renderiza una fase en la pantalla, si el atributo fase es un objeto, muestra esos datos, sino genera una nueva basandose en el numero de fase que se pasa como parametro
    presentarFase: function (numFase, fase) {
        $("#cuerpoFase" + numFase).remove();
        var widget = this;
        var fasePreCargada = fase != null || fase != undefined;
        numFase = (fasePreCargada) ? fase.idFase : numFase;
        if (widget.options.error)
            return;
        var tipoFixture = (fasePreCargada) ? fase.tipoFixture.idTipoFixture : $("#ddlTipoFixtureFase" + numFase).val();
        var cantidades = (fasePreCargada) ? fase.grupos.length : $("#ddlCantidad-Fase" + numFase).val();
        $("<div/>").attr("id", "cuerpoFase" + numFase).appendTo($("#panelFase"+ numFase));
        $("#panelFracaso").hide();         
        if (tipoFixture.indexOf("TCT") >= 0) {
            if(fasePreCargada)
            $('#ddlCantidad-Fase'+ fase.idFase + ' option:contains("'+fase.grupos.length+'")').prop('selected', true);
            widget.mostrarEquiposEngrupo(numFase, cantidades, fase);
        } else {
            $("#cuerpoFase" + numFase).css("overflow-x", "scroll");
            $("#cuerpoFase" + numFase).generadorDeLlaves({                
                equipos: (fase != null) ? fase.equipos : widget.options.fases[numFase -1].equipos,
                mezclar: !fasePreCargada
            });
        }
    },
    //muestra el mensaje que se pasa por parametro, haciendo el efecto de luz ;)
    mostrarMensajeDeError: function (mensaje) {
        $("#msjFracaso").text(mensaje);
        $("#panelFracaso").show();
        $("#panelFracaso").effect("highlight", 500);
    }

});