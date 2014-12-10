$.widget("quegolazo.generadorDeFases", {
    options: {
        idFase: null,
        idEdicion: null,
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
        $("#agregarFase").tooltip({title:"Presiona este botón para generar nuevas fases.", placement:"right"});
        widget.cargarEstructuraDeFases();
    },

    //crea la estrucctura incial para presentar todas las fases del campeonato
    agregarNuevaFase: function () {
        var widget = this;
        if (widget.options.fases.length > 0 && $("#cuerpoFase" + widget.options.fases.length).length == 0) {
            widget.mostrarMensajeDeError("Antes de crear una nueva fase, debe configurar la fase actual.");
            return false;
        }
        widget.agregarFase();
        widget.crearHtmlFaseNueva(widget.options.fases.length);
        widget.cambioEnTipoDeFixture($("#ddlTipoFixtureFase" + widget.options.fases.length));
    },
    crearHtmlFaseNueva: function (numFase) {
        var widget = this;
        var panelFases = $("#accordionFases");
        var contenedorGeneral = $("<div/>", { class: 'panel panel-default' }).attr("id", "panelContenedorFase"+numFase);
        var headerContenedor = $("<div/>", { class: 'panel-heading' });
        var btnEliminar = $("<span/>", { class: 'glyphicon glyphicon-remove eliminar pull-right', id:"btnEliminarFase"+numFase }).on("click",
            function () {
                $("#panelContenedorFase" + numFase).remove();
                widget.options.fases.pop();
                if(numFase>1)
                $("#btnEliminarFase" + (numFase - 1)).show("slow");
            }).tooltip({ title: "Eliminar Fase" });
        btnEliminar.appendTo(headerContenedor);           
        var contenedorTitulo = $("<h4/>", { class: 'panel-title text-center' }).text("Fase N° " + numFase).css("font-weight", "700");
        var linkTitulo = $("<a/>").attr("data-toggle", "collapse").attr("data-parent", "#accordionFases").attr("id", "collapsableFase" + numFase).attr("href", "#collapseFase" + numFase);
        var contenedorDeLaFase = $("<div/>", { class: 'panel-collapse collapse in' }).attr("id", "collapseFase" + numFase);
        var cuerpoDeLaFase = $("<div/>", { class: 'panel-body' }).attr("id", "panelFase" + numFase);
        var contenedorControles = widget.crearControlesDeFase(numFase);
        contenedorControles.appendTo(cuerpoDeLaFase);
        cuerpoDeLaFase.appendTo(contenedorDeLaFase);
        contenedorTitulo.appendTo(linkTitulo);       
        linkTitulo.appendTo(headerContenedor);
        headerContenedor.appendTo(contenedorGeneral);
        contenedorDeLaFase.appendTo(contenedorGeneral);
        contenedorGeneral.appendTo(panelFases);
        $("<div/>").attr("id", "cuerpoFase" + numFase).appendTo(cuerpoDeLaFase);
        //si la fase es mayor a uno, cargo los equipos genericos para el primer caso.
        if (numFase > 1) {
            widget.generarListaDeEquiposParaFase(numFase,(widget.options.fases[numFase-1].cantidadDeEquipos > 0) ? widget.options.fases[numFase-1].cantidadDeEquipos :  $("#ddlCantidadParticipantesFase" + numFase).val());
            $("#ddlCantidadParticipantesFase" + numFase).tooltip({ placemenent:'bottom',title: "Indica la cantidad de equipos que clasifican de la fase anterior"});
            //elimino el boton eliminar de una fase que no sea la ultima.  
            $("#btnEliminarFase" + (numFase - 1)).hide();
            //cierro el panel de la fase anterior.
            $("#collapseFase" + (numFase - 1)).addClass("collapse").removeClass("in");
        } else
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
        comboTipoFixture.on("change", function () { widget.cambioEnTipoDeFixture($(this)); })
        labelFixture.appendTo(divIzquierda);
        comboTipoFixture.appendTo(divIzquierda);
        divIzquierda.appendTo(row);

        var divCentro = $("<div/>", { class: 'col-md-4', id: 'divCentroFase' + numFase });
        var labelCantidad = $("<label/>").attr("id", "lblCantidadFase" + numFase).text("Cantidad de grupos:");
        //var cantidadParticipantesEnLaFase = (numFase > 1) ? (widget.options.fases[numFase - 1].equipos.length > 0) ? widget.options.fases[numFase - 1].equipos.length - 2 :  : ;
        var comboCant = null;
        

        // si la fase es mayor a uno, armos dos divs para que la cantidad de participantes y de grupos se alineen en un solo div central
        if (numFase > 1) {
            var divCantidadParticipantes = $("<div/>", { class: 'col-md-6 faseGenerica', id: 'divCantidadParticipantesFase' + numFase });
            var labelCantidadParticpantes = $("<label/>").attr("id", "lblCantidadParticipantesFase" + numFase).text("Clasifican:");
            labelCantidadParticpantes.appendTo(divCantidadParticipantes);
            var comboCantParticipantes = createDropDownList("ddlCantidadParticipantesFase" + numFase, widget.obtenerArrayDeParticipantes(widget.options.fases[numFase - 2].equipos.length));
            comboCantParticipantes.appendTo(divCantidadParticipantes);
            $(comboCantParticipantes).prop('selectedIndex', (widget.options.fases[numFase - 1].equipos.length > 0) ? widget.options.fases[numFase - 1].equipos.length-2 : $("#ddlCantidadParticipantesFase" + numFase).length);
            comboCantParticipantes.on("change", function () {
                createDropDownList("ddlCantidadFase" + numFase, widget.obtenerGruposPosibles($("#ddlCantidadParticipantesFase" + numFase).val()));
                widget.generarListaDeEquiposParaFase(numFase, $(this).val());
                $("#cuerpoFase" + numFase).remove();
                widget.eliminarFasesSiguientes(numFase);
                $("#btnEliminarFase" + numFase).show("slow");
                if ($("#ddlTipoFixtureFase" + numFase).val().indexOf("ELIM") >= 0) {
                    widget.validarCantidadDeEquiposEliminatorio(numFase);
                }
            });
            comboCant = createDropDownList("ddlCantidadFase" + numFase, widget.obtenerGruposPosibles($(comboCantParticipantes).val()));
            var divCantidadGrupos = $("<div/>", { class: 'col-md-6', id: 'divCantidadGruposFase' + numFase });
            labelCantidad.text("Grupos:");
            labelCantidad.appendTo(divCantidadGrupos);
            comboCant.appendTo(divCantidadGrupos);
            divCantidadParticipantes.appendTo(divCentro);
            divCantidadGrupos.appendTo(divCentro);
            } else {
            comboCant = createDropDownList("ddlCantidadFase" + numFase, widget.obtenerGruposPosibles(widget.options.fases[numFase - 1].equipos.length));
            labelCantidad.appendTo(divCentro);
            comboCant.appendTo(divCentro);          
        }
        divCentro.appendTo(row);
        comboCant.on("change", function () {
            $("#cuerpoFase" + numFase).remove();
            widget.options.fases[numFase - 1].cantidadDeGrupos = $(this).val();
        })
        var divDerecha = $("<div/>", { class: 'col-md-3' });
        var botonMostrar = $('<input/>').attr({ type: 'button', id: 'btnMostrarFase' + numFase, value: 'Mostrar Fase', class: 'btn btn-success' }).css("margin-top", "23px").on("click", function () { widget.presentarFase(numFase); });
        botonMostrar.appendTo(divDerecha);        
        divDerecha.appendTo(row);
        return row;
    },
    //genera una lista de equipos generica para una fase distinta a la primera, pasando como parametro la cantidad de equipos y el numero de fase.
    generarListaDeEquiposParaFase: function (numFase, cantidadEquipos) {
        var widget = this;
        var equipo = { nombre: "", idEquipo: 0, };
        var equipos = [];
        for (var i = 0; i < cantidadEquipos; i++) {
            equipo.nombre = "Equipo " + (i + 1);
            equipo.idEquipo = i + 1;
            var nuevo = $.extend({}, equipo);
            equipos.push(nuevo);
        }
        widget.options.fases[numFase - 1].equipos = equipos;
        widget.options.fases[numFase - 1].cantidadDeEquipos = equipos.length;
        widget.options.fases[numFase - 1].esGenerica = true;
    },
    //elimina todas las fases genericas creadas tras la modificacion de los equipos participantes.
    eliminarFasesSiguientes : function (numFase) {
        var widget = this;
        for (var i = widget.options.fases.length; i > numFase; i--) {
            $("#panelContenedorFase" + i).remove();
            widget.options.fases.pop();    
        }
    
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
    //devuelve un array con todos los numeros que pueden ser elegidos como cantidad de equipos participantes, como parametro se pasa el maximo numero
    obtenerArrayDeParticipantes: function (max) {
        var respuesta = [];        
        for (var i = 0; i < max; i++) {
            var opcion = { value: 0, text: "" };
            if (i > 0) {
                opcion.value = i + 1;
                opcion.text = i + 1;
                respuesta.push(opcion);
            }
        }
        return respuesta;
    },
    //prepara los controles de la fase ségun el tipo de fixture que se eligió, ademas setea la cantidad de grupos en 0.
    cambioEnTipoDeFixture: function (combo) {
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
            $("#divCantidadGruposFase" + numFase).show();
        } else {
            //si es la primera fase oculto todo el div central.
            if (numFase == 1)
                $("#divCentroFase" + numFase).hide();
            else {//sino oculto solo la cantidad de grupos.
                $("#divCantidadGruposFase" + numFase).hide();
            }
            widget.validarCantidadDeEquiposEliminatorio(numFase);
        }
        widget.options.fases[numFase - 1].grupos = [];
       
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
            "esGenerica": false,
            "cantidadDeEquipos": (numFase === 1) ? widget.options.equiposDeLaEdicion.length : 0,
            "cantidadDeGrupos":0
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
        var esValido = ($.inArray((numFase == 1) ? widget.options.fases[numFase - 1].equipos.length : parseInt($("#ddlCantidadParticipantesFase" + numFase).val()), [2, 4, 8, 16, 32, 64, 128]) > -1);
        if (!esValido) {
            $((numFase > 1) ? "#ddlCantidadParticipantesFase" + numFase : "#btnMostrarFase" + numFase).popover({
                container: '#controlesFase' + numFase,
                placement: (numFase > 1) ? 'bottom' : 'right',
                title: 'Cantidad de equipos inválida.',
                trigger: 'manual',
                content: 'Para fixtures tipo "Eliminatorio" solo se admiten 2,4,8,16,32,64 o 128 equipos.'
            });
            $((numFase > 1) ? "#ddlCantidadParticipantesFase" + numFase : "#btnMostrarFase" + numFase).popover("show");
            widget.options.error = true;
        } else {
            $((numFase > 1) ? "#ddlCantidadParticipantesFase" + numFase : "#btnMostrarFase" + numFase).popover("destroy");
            widget.options.error = false;
        }
    },    
    //crea la estructura html para los grupos de una fase, si el atributo fase es distinto de null, crea toda una estructura nueva, sino crea lo que tenga la fase que se pasa por parametro
    mostrarEquiposEngrupo: function (numFase, cantidadGrupos, fase) {        
        var widget = this;
        var fasePreCargada = fase != null || fase != undefined;
        numFase = (fase != null) ? fase.idFase : numFase;
        var cantidadEquiposPorGrupo = (cantidadGrupos != null) ? parseInt((numFase == 1) ? widget.options.equiposDeLaEdicion.length : $("#ddlCantidadParticipantesFase" + numFase).val() / cantidadGrupos) : null;
        var grupos = widget.armarGruposParaPresentar(fasePreCargada ? fase.equipos : widget.options.fases[numFase-1].equipos, cantidadGrupos, fasePreCargada ? fase.idFase : numFase);
        //recorremos todos los grupos, previamente borramos los grupos que se han agregado antes.
        widget.options.fases[numFase - 1].grupos = grupos;        
            for (var i = 0; i < grupos.length; i++) {                
                var tablaGrupo = widget.crearTablaParaGrupo(i + 1, numFase);
                $("#cuerpoFase" + numFase).append(tablaGrupo);
                var listaNueva = $("<ul/>", { class: "connectedSortable ui-sortable gruposFase"+numFase }).attr("data-id-fase", numFase).attr("data-idGrupo", grupos[i].idGrupo);
                for (var j = 0 ; j < grupos[i].equipos.length; j++) {
                    var unEquipo = $("<li/>").attr("data-id-equipo", grupos[i].equipos[j].idEquipo).text(grupos[i].equipos[j].nombre);
                    unEquipo.appendTo(listaNueva);
                }
                //cada dos grupos hago una fila nueva para que se acomoden los grupos 2 por cada fila.
                if ((i + 1) % 2 != 0) {
                   var row = $("<div/>", { class: "row filaGrupo" });                    
                }
                tablaGrupo = $("#tablaGrupo" + (i + 1) + "Fase" +numFase);
                listaNueva.appendTo(tablaGrupo.find("td"));
                tablaGrupo.appendTo(row);
                row.appendTo($("#cuerpoFase" + numFase));
            }
        if(numFase ==1) // si no es la primera fase, 
        $(".gruposFase"+numFase).sortable({
            connectWith: ".gruposFase" + numFase
            }).disableSelection();        
    },
    //crea una tabla para presentar el grupo
    crearTablaParaGrupo: function (idGrupo, numFase) {
        return "<div id='tablaGrupo" + idGrupo + "Fase"+numFase + "' class='col-md-6 divGrupos'> <table  class='table'><thead><tr><th>Grupo " + idGrupo + "</th></tr></thead><tbody><tr><td></td></tr></tbody></table></div>"
     },
    // realiza la llamada final al servidor, mandando como parametro una lista de fases con sus atributos, y guardando las fases en sesión.
    guardarFasesEnSesion: function () {
        var widget = this;
        var respuesta = false;
        try {
            widget.armarFases();
            widget.validarFasesCorrectas();
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
                        widget.mostrarMensajeDeError(response.responseJSON.Message);
                    }
                });
                return respuesta;
            
        } catch (e) {
            widget.mostrarMensajeDeError(e.message);
            return respuesta;
        }
        
    },
    //valida que las fases estén generadas por el usuario y que la diferencia de equipos entre los grupos no sea mayor a uno
    validarFasesCorrectas: function () {
        var widget = this;
        if (widget.options.fases.length < 1)
            throw new Error("Debe crear al menos una fase para continuar.");
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            if ( fase.tipoFixture.idTipoFixture.indexOf("TCT") >=0 && fase.grupos.length < 1)
                throw new Error("Debe crear al menos un grupo para continuar.") ;
            for (var j = 0; j < fase.grupos.length; j++) {               
                for (var k = 0; k < fase.grupos.length; k++) {
                    // si la diferencia entre cantidades de equipos es mayor a uno, o bien la cantidad de equipos de algun grupo es 1
                    if (Math.abs(fase.grupos[j].equipos.length - fase.grupos[k].equipos.length) > 1 || fase.grupos[j].length < 2)
                        throw new Error("La diferencia de equipos entre los grupos generados en la Fase 1 no debe ser mayor a uno.");
                    }
            }
        }
       
    },
    //setea los grupos en la opcion del widget, guardando los grupos en cada fase, basandose en lo que generó el usuario en la interfaz
    armarFases: function () {
        var widget = this;
        //guardo los grupos segun lo que arm el usuario
        for (var i = 0; i < widget.options.fases.length; i++) {
            var fase = widget.options.fases[i];
            if (!fase.esGenerica) {//armo para cuando la fase NO es generica
                if (fase.tipoFixture.idTipoFixture.indexOf("TCT") >= 0) {//si es todos contra todos
                    for (var j = 0; j < fase.grupos.length; j++) {
                        var grupo = fase.grupos[j];
                        grupo.equipos = widget.obtenerEquiposdeUnGrupo(fase.idFase, grupo.idGrupo);
                    }
                } else {
                    widget.armarLlaves(fase.idFase);
                }
            } else {//armo para cuando la fase es generica
                //        fase.cantidadDeGrupos = $("#ddlCantidadFase" + fase.idFase).val();
                return;//no me interesan las demas fases solo la primera que es personalizable

            }
                
            
            }
                       
        },
    //arma un grupo con una fecha, que corresponde a todas las llaves de la primera ronda, guarda en objetos tipo PartidoEliminatorio
    armarLlaves: function (numFase) {
        var widget = this;
        var idEquipos = $("[data-idequipo]");
        if (idEquipos.length == 0)
            throw new Error("ATENCION : Debe configurar los partidos de la eliminaoria para continuar");
        var partido = { local: null, visitante: null };
        var fecha = { idFecha: 0, partidos: [], nombre: "" };
        var equiposDelGrupo = [];
        for (var i = 0; i < idEquipos.length; i++) {
           var equipo = widget.buscarEquipo(widget.options.fases[numFase-1].equipos, $(idEquipos[i]).attr("data-idequipo"));
            //cada 2 equipos viene el visitante
           equiposDelGrupo.push(equipo);
            if ((i + 1) % 2 == 0 && i != 0) {
                partido.visitante = equipo;               
                fecha.partidos.push( $.extend({}, partido));
            } else {
                //sino es el local
                partido.local = equipo;
            }          
        }
        //le asigno a la fase indicada el grupo con la fecha que corresponde a la primera fase
        var grupo = { idGrupo: 1, idFase: numFase + 1, fechas: [fecha], equipos: equiposDelGrupo };
        if (widget.options.fases[numFase - 1].grupos.length === 0)
            widget.options.fases[numFase - 1].grupos.push(grupo);
        else
            widget.options.fases[numFase - 1].grupos[0] = grupo;


        
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
            var equipo = widget.buscarEquipo(widget.options.fases[numFase - 1].equipos, $(idEquipos[i]).attr("data-id-equipo"));
            if (equipo != null) {
                equipos.push(equipo);               
            } else {
                throw new Error("Ha ocurrido un error inesperado. Por favor actualice la pagina e intente nuevamente");
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
        var cantidadGrupos = (fasePreCargada) ? (fase.esGenerica) ? fase.cantidadDeGrupos : fase.grupos.length : $("#ddlCantidadFase" + numFase).val();
        $("<div/>").attr("id", "cuerpoFase" + numFase).appendTo($("#panelFase"+ numFase));
        $("#panelFracaso").hide();         
        if (tipoFixture.indexOf("TCT") >= 0) {
            if (fasePreCargada) {
                $("#ddlCantidadParticipantesFase" + fase.idFase + ' option:contains("' + fase.equipos.length + '")').prop('selected', true);
                createDropDownList("ddlCantidadFase" + numFase, widget.obtenerGruposPosibles(fase.equipos.length));
                $("#ddlCantidadFase" + fase.idFase + ' option:contains("' + cantidadGrupos + '")').prop('selected', true);
            }          
            widget.mostrarEquiposEngrupo(numFase, cantidadGrupos, fase);
        } else {
            if (fasePreCargada) {
                $('#ddlTipoFixtureFase' + fase.idFase).val(fase.tipoFixture.idTipoFixture);
                $("#divCentroFase" + fase.idFase).hide();
                widget.validarCantidadDeEquiposEliminatorio(fase.idFase);
            }
            $("#cuerpoFase" + numFase).css("overflow-x", "scroll");
            $("#cuerpoFase" + numFase).generadorDeLlaves({                
                equipos: (fasePreCargada && !(numFase > 1)) ? fase.grupos[0].fechas[0].partidos : widget.options.fases[numFase - 1].equipos,
                mezclar: !fasePreCargada,
                generica : (numFase>1)
            });
        }
        widget.armarFases();
    },
    //muestra el mensaje que se pasa por parametro, haciendo el efecto de luz ;)
    mostrarMensajeDeError: function (mensaje) {
        //var msj = "<div id='alertmsg1'  class='alert alert-danger alert-dismissible flyover flyover-bottom' role='alert'>" +
        //"<button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>"+
        //"<strong>Atención :</strong> <span id='msjError'></span></div>"       
        //$('body').append(msj);
        //$("#msjError").text(mensaje);
        //$("#alertmsg1").addClass("in");
        $("#msjFracaso").text(mensaje);
        $("#panelFracaso").show();
    }

});