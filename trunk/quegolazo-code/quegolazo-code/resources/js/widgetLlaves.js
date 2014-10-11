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
            container.append(data.nombre).attr("data-idEquipo", data.idEquipo)
        }
    },

    _create: function () { 
        this.renderizarLlaves();
        //workaround para eliminar los controles de customizacion
        $(".tools").remove(); 
    },

    armarLlaves: function () {
        var widget = this;
        var llaves = [];
        var partido = [];
        if (widget.options.mezclar)//sort(function () { return 0.5 - Math.random() }) sirve para randomizar la lista.
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
    reiniciar: function () {
        this._create();
    },
    _destroy: function () {
        $(".jQBracket").remove();
    }
});