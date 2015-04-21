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
    
    //=================================== Pie Chart ===================================//
    var ctx = $("#graficoGoles").get(0).getContext("2d");
    var data = [
        {
            value: 30,
            color: colors('blue','color'),
            highlight: colors('blue','highlight'),
            label: "De Cabeza"
        },
        {
            value: 20,
            color: colors('yellow','color'),
            highlight: colors('yellow','highlight'),
            label: "De Jugada"
        },
        {
            value: 50,
            color: colors('orange','color'),
            highlight: colors('orange','highlight'),
            label: "Tiro Libre"
        }
    ];
    
    var myPieChart = new Chart(ctx).Doughnut(data, {
        animateScale: true,
        tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
    });
    
    $('#goleadores').removeClass("active");
});	