$.widget("quegolazo.generadorDeLlaves", {
    options: {
        container: null,
        equipos: null,
        equiposGenericos : null,
        mezclar: false,
        numFase: null,
        generica: false,
        noSwap: false,
        scroll: true,
        resultados: [],
        llaves: [],
        datosEquipos: {teams:[], results:[]},
        //cuando se hace click en el label de un equipo.
        alEditar : undefined
        //cuando se renderiza en la UI el label de los equipos.
        
    },

    _create: function () { 
        this.renderizarLlaves();
        //workaround para eliminar los controles de customizacion
        $(".tools").remove();
        //$(".team").unbind();
        if(!this.options.generica && !this.options.noSwap)
            this.habilitarSwap();
        if (scroll) {
            this.element.css("overflow", "scroll");
            
        }

    },    
    armarLlaves: function () {
        var widget = this;
        var llaves = [];
        var partido = [];
        //si mezclar viene en false, no habia fase precargada
        if (widget.options.mezclar || widget.options.generica)//sort(function () { return 0.5 - Math.random() }) sirve para randomizar la lista.
        {
            var equiposAReordenar = (this.options.equiposGenericos != null) ? this.options.equiposGenericos : widget.options.equipos;
            equiposAReordenar.sort(function () { return 0.5 - Math.random() });
            for (var i = 0; i < equiposAReordenar.length; i++) {
                var equipo = { nombre: equiposAReordenar[i].nombre, idEquipo: equiposAReordenar[i].idEquipo };
                partido.push(equipo);
                //cada 2 equipos forman un partido
                if ((i + 1) % 2 == 0 && i != 0) {
                    llaves.push(partido);
                    partido = [];
                }
            }
            return llaves;
        } else {
            var equipos = [];
            for (var i = 0; i < widget.options.equipos.length; i++) {
                var partidos = [];
                partidos.push(widget.options.equipos[i].local);
                partidos.push(widget.options.equipos[i].visitante);
                equipos.push(partidos);
            }
            return equipos;
        }
            
          
    },
    renderizarLlaves: function (equipos) {
        var widget = this;
        widget.options.datosEquipos.teams = widget.armarLlaves();
        widget.options.datosEquipos.results = widget.options.resultados;
        this.element.bracket({
            init: widget.options.datosEquipos, /* data to initialize the bracket with */            
            decorator: {
                edit: function () {},
                render: function render_fn(container, data, score) {
                        if (!data.idEquipo || !data.nombre)
                        return
                    //container es el label, el parent es el div que lo contiene
                    container.append('<img src="/resources/img/equipos/' + data.idEquipo + '-sm.png" class="avatar-xs" /> ');
                    container.append(data.nombre);
                    if (!widget.options.generica) //si no es generica le asigno el idEquipo al div.
                        container.parent().attr("data-idequipo", data.idEquipo);

                }
            }
        });
    
    },
    habilitarSwap: function ()  {
        var droppableParent;
        var widget = this;
        $('[data-idequipo]').wrap("<div class='target'><div/>");	
        $('[data-idequipo]').draggable({
            revert: 'invalid',
            revertDuration: 200,
            start: function () {
                droppableParent = $(this).parent();		
                $(this).addClass('arrastrando');
                
            },
            stop: function () {
                $(this).removeClass('arrastrando');
            }
        });
	
        $('.target').droppable({
            hoverClass: 'drop-hover',
            drop: function (event, ui) {
                var draggable = $(ui.draggable[0]),
                    draggableOffset = draggable.offset(),
                    container = $(event.target),
                    containerOffset = container.offset();
			
                $('[data-idequipo]', event.target).appendTo(droppableParent).css({ opacity: 0 }).animate({ opacity: 1 }, 500);
			
                draggable.appendTo(container).css({left: draggableOffset.left - containerOffset.left, top: draggableOffset.top - containerOffset.top}).animate({left: 0, top: 0}, 200);
            }
        });
    },
    reiniciar: function () {
        this._create();
    },
    _destroy: function () {
        $("#cuerpoFase" + widget.options.numFase).remove();
    }
});