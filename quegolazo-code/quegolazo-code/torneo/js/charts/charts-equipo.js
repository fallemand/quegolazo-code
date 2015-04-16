// THEME OPTIONS.JS
//--------------------------------------------------------------------------------------------------------------------------------
//This is JS file that contains principal fuctions of theme */
// -------------------------------------------------------------------------------------------------------------------------------
// Template Name: Sports Cup- Responsive HTML5  soccer and sports Template.
// Author: Iwthemes.
// Name File: main.js
// Version 1.0 - Created on 20 May 2014
// Website: http://www.iwthemes.com 
// Email: support@iwthemes.com
// Copyright: (C) 2014

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
    
    //=================================== Bar Chart ===================================//
    // Get context with jQuery - using jQuery's .get() method.
    //Chart.defaults.global.responsive = true;
    var ctx = $('#graficoGoles').get(0).getContext("2d");
    var data = {
        labels: ["Convertidos", "En Contra"],
        datasets: [
            {
                label: "Goles",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,0.75)",
                highlightStroke: "rgba(151,187,205,1)",
                data: [12,4]
            }
        ]
    };
    
    // This will get the first returned node in the jQuery collection.
    var myBarChart = new Chart(ctx).Bar(data);
    myBarChart.datasets[0].bars[0].fillColor = colors('green','fillColor'); //bar 1
    myBarChart.datasets[0].bars[0].strokeColor = colors('green','strokeColor'); //bar 1
    myBarChart.datasets[0].bars[0].highlightFill = colors('green','highlightFill'); //bar 1
    myBarChart.datasets[0].bars[0].highlightStroke = colors('green','highlightStroke'); //bar 1
    myBarChart.datasets[0].bars[1].fillColor = colors('red','fillColor'); //bar 2
    myBarChart.datasets[0].bars[1].strokeColor = colors('red','strokeColor'); //bar 2
    myBarChart.datasets[0].bars[1].highlightFill = colors('red','highlightFill'); //bar 2
    myBarChart.datasets[0].bars[1].highlightStroke = colors('red','highlightStroke'); //bar 2
    myBarChart.update();   
    
    //=================================== Pie Chart ===================================//
    var ctx = $("#graficoPartidos").get(0).getContext("2d");
    var data = [
        {
            value: 70,
            color: colors('green','color'),
            highlight: colors('green','highlight'),
            label: "Partidos Ganados"
        },
        {
            value: 12,
            color: colors('yellow','color'),
            highlight: colors('yellow','highlight'),
            label: "Partidos Empatados"
        },
        {
            value: 18,
            color: colors('red','color'),
            highlight: colors('red','highlight'),
            label: "Partidos Perdidos"
        }
    ];
    
    var myPieChart = new Chart(ctx).Doughnut(data, {
        animateScale: true,
        tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
    });
    
    //=================================== Line Chart ===================================//
    var ctx = $("#graficoPuntos").get(0).getContext("2d");
    var data = {
        labels: ["Fecha 1", "Fecha 2", "Fecha 3", "Fecha 4", "Fecha 5", "Fecha 6", "Fecha 7", "Fecha 8", "Fecha 9", "Fecha 10"],
        datasets: [
            {
                label: "My Second dataset",
                fillColor: "rgba(151,187,205,0.2)",
                strokeColor: "rgba(151,187,205,1)",
                pointColor: "rgba(151,187,205,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(151,187,205,1)",
                data: [3, 4, 7, 7, 7, 10, 11, 14, 17, 20]
            }
        ]
    };
    
    var myLineChart = new Chart(ctx).Line(data, {
        bezierCurve: false,
        tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+' Puntos' %>",
    });

    //=================================== Bar Chart ===================================//
    // Get context with jQuery - using jQuery's .get() method.
    //Chart.defaults.global.responsive = true;
    var ctx = $('#graficoGoleadores').get(0).getContext("2d");
    var data = {
        labels: ["Fernando Gago", "Fernando Gago", "Fernando Gago", "Fernando Gago", "Fernando Gago", "Fernando Gago"],
        datasets: [
            {
                label: "Goles",
                fillColor: colors('blue', 'fillColor'),
                strokeColor: colors('blue', 'strokeColor'),
                highlightFill: colors('blue', 'highlightFill'),
                highlightStroke: colors('blue', 'highlightStroke'),
                data: [8, 5, 4, 4, 3, 1]
            }
        ]
    };

    // This will get the first returned node in the jQuery collection.
    var myBarChart = new Chart(ctx).Bar(data);
});	