$(document).ready(function($) {
    
    //function colors(color,typeColor) {
    //    //blue, red, yellow, green, orange, purple, grey
    //    var colors = {
    //        blue:   'rgba(52, 73, 94,',
    //        red:    'rgba(192, 57, 43,',
    //        yellow: 'rgba(241, 196, 15,', 
    //        green:  'rgba(39, 174, 96,', 
    //        orange: 'rgba(211, 84, 0,', 
    //        purple: 'rgba(142, 68, 173,',
    //        grey:   'rgba(127, 140, 141,'
    //    };
    //    var transparencies = {
    //        fillColor:      '0.7',
    //        strokeColor:    '0.8',
    //        highlightFill:  '0.8',
    //        highlightStroke:'1.0',
    //        color:'0.8',
    //        highlight: '0.9'
    //    }
    //    var end=')';
    //    return colors[color] + transparencies[typeColor] + end;
    //}
    
    ////=================================== Pie Chart ===================================//
    
    //var data = [
    //    {
    //        value: 30,
    //        color: colors('blue','color'),
    //        highlight: colors('blue','highlight'),
    //        label: "De Cabeza"
    //    },
    //    {
    //        value: 20,
    //        color: colors('yellow','color'),
    //        highlight: colors('yellow','highlight'),
    //        label: "De Jugada"
    //    },
    //    {
    //        value: 50,
    //        color: colors('orange','color'),
    //        highlight: colors('orange','highlight'),
    //        label: "Tiro Libre"
    //    }
    //];
    function cargarGrafico(tiposDeGoles) {
        var ctx = $("#graficoGoles").get(0).getContext("2d");
        var myPieChart = new Chart(ctx).Doughnut(tiposDeGoles, {
            animateScale: true,
            tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
        });
        $('#goleadores').removeClass("active");
    }
});
