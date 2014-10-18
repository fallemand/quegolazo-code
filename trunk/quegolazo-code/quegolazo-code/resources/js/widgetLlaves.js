$.widget("quegolazo.generadorDeLlaves", {
    options: {
        container: null,
        equipos: null,
        mezclar: false,
        llaves: [],
        datosEquipos: {teams:[], results:[]},
        //cuando se hace click en el label de un equipo.
        alEditar : undefined,
        //cuando se renderiza en la UI el label de los equipos.
        alRenderizar :  function render_fn(container, data, score) {
            if (!data.idEquipo || !data.nombre)
                return
            //container es el label, el parent es el div que lo contiene
            container.append(data.nombre);
            container.parent().attr("data-idequipo", data.idEquipo);
            
        }
    },

    _create: function () { 
        this.renderizarLlaves();
        //workaround para eliminar los controles de customizacion
        $(".tools").remove();
        //$(".team").unbind();
        this.habilitarSwap();    

    },

    armarLlaves: function () {
        var widget = this;
        var llaves = [];
        var partido = [];
        //si mezclar viene en false, no habia fase precargada
        if (widget.options.mezclar)//sort(function () { return 0.5 - Math.random() }) sirve para randomizar la lista.
        {
            widget.options.equipos.sort(function () { return 0.5 - Math.random() });
            for (var i = 0; i < widget.options.equipos.length; i++) {
                var equipo = { nombre: widget.options.equipos[i].nombre, idEquipo: widget.options.equipos[i].idEquipo };
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
        widget.element.bracket({
            init: widget.options.datosEquipos, /* data to initialize the bracket with */            
            decorator: {
                edit: function () {},
                render: widget.options.alRenderizar}
        });
    
    },
    habilitarSwap: function ()  {
        var droppableParent;
       
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
        $(".jQBracket").remove();
    }
});