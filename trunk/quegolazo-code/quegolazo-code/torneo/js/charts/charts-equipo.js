
$(document).ready(function($) {
    
    function colors(color,typeColor) {
        //blue, red, yellow, green, orange, purple, grey
        var colors = {
            blue:   'rgba(52, 73, 94,',
            red:    'rgba(192, 57, 43,',
            yellow: 'rgba(241, 196, 15,', 
            green:  'rgba(39, 174, 96,', 
            orange: 'rgba(211, 84, 0,', 
            purple: 'rgba(142, 68, 173,',
            grey:   'rgba(127, 140, 141,'
        };
        var transparencies = {
            fillColor:      '0.7',
            strokeColor:    '0.8',
            highlightFill:  '0.8',
            highlightStroke:'1.0',
            color:'0.8',
            highlight: '0.9'
        }
        var end=')';
        return colors[color] + transparencies[typeColor] + end;
    }
    
    //=================================== GRAFICO TARJETAS ===================================//
    // Get context with jQuery - using jQuery's .get() method.
    //Chart.defaults.global.responsive = true;
    var ctx = $('#graficoGoles').get(0).getContext("2d");
    
    // This will get the first returned node in the jQuery collection.
    var myBarChart = new Chart(ctx).Bar(datosGoles);
    myBarChart.datasets[0].bars[0].fillColor = colors('green','fillColor'); //bar 1
    myBarChart.datasets[0].bars[0].strokeColor = colors('green','strokeColor'); //bar 1
    myBarChart.datasets[0].bars[0].highlightFill = colors('green','highlightFill'); //bar 1
    myBarChart.datasets[0].bars[0].highlightStroke = colors('green','highlightStroke'); //bar 1
    myBarChart.datasets[0].bars[1].fillColor = colors('red','fillColor'); //bar 2
    myBarChart.datasets[0].bars[1].strokeColor = colors('red','strokeColor'); //bar 2
    myBarChart.datasets[0].bars[1].highlightFill = colors('red','highlightFill'); //bar 2
    myBarChart.datasets[0].bars[1].highlightStroke = colors('red','highlightStroke'); //bar 2
    myBarChart.update();   
    
    //=================================== GRAFICO PARTIDOS ===================================//
    var ctx = $("#graficoPartidos").get(0).getContext("2d");
    var myPieChart = new Chart(ctx).Doughnut(datosPartidos, {
        animateScale: true,
        tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
    });
    
    //=================================== EVOLUCION DE PUNTOS ===================================//
    var ctx = $("#graficoPuntos").get(0).getContext("2d");    
    var myLineChart = new Chart(ctx).Line(datosEvolucionPuntos, {
        bezierCurve: false,
        tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+' Puntos' %>",
    });

    //=================================== GRAFICO GOLEADORES ===================================//
    // Get context with jQuery - using jQuery's .get() method.
    //Chart.defaults.global.responsive = true;
    var ctx = $('#graficoGoleadores').get(0).getContext("2d");
    // This will get the first returned node in the jQuery collection.
    var myBarChart = new Chart(ctx).Bar(datosGoleadores);
    $('#goleadores').removeClass("active");
    $('#historial-partidos').removeClass("active");
});	