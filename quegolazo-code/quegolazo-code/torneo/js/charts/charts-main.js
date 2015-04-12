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
    
    //=================================== Bar Chart ===================================//
    // Get context with jQuery - using jQuery's .get() method.
    //Chart.defaults.global.responsive = true;
    var ctx = $('#barChart').get(0).getContext("2d");
    var data = {
        labels: ["P. Ganados", "P. Empatados", "P. Perdidos"],
        datasets: [
            {
                label: "Partidos Ganados",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,0.75)",
                highlightStroke: "rgba(151,187,205,1)",
                data: [12,4,3]
            }
        ]
    };
    
    // This will get the first returned node in the jQuery collection.
    var myBarChart = new Chart(ctx).Bar(data);
    myBarChart.datasets[0].bars[0].fillColor = "rgb(175, 215, 175)"; //bar 1
    myBarChart.datasets[0].bars[0].strokeColor = "rgb(65, 147, 65)"; //bar 1
    myBarChart.datasets[0].bars[1].fillColor = "rgb(255, 238, 152)"; //bar 2
    myBarChart.datasets[0].bars[1].strokeColor = "rgb(231, 179, 84)"; //bar 2
    myBarChart.datasets[0].bars[2].fillColor = "rgb(231, 184, 184)"; //bar 3
    myBarChart.datasets[0].bars[2].strokeColor = "rgb(199, 45, 45)"; //bar 3
    myBarChart.update();
    
    
    //=================================== Pie Chart ===================================//
    var ctx = $("#pieChart").get(0).getContext("2d");
    var data = [
        {
            value: 300,
            color:"#F7464A",
            highlight: "#FF5A5E",
            label: "Red"
        },
        {
            value: 50,
            color: "#46BFBD",
            highlight: "#5AD3D1",
            label: "Green"
        },
        {
            value: 100,
            color: "#FDB45C",
            highlight: "#FFC870",
            label: "Yellow"
        }
    ];
    
    var myPieChart = new Chart(ctx).Doughnut(data, {
        animateScale: true
    });
    
    //=================================== Line Chart ===================================//
    var ctx = $("#lineChart").get(0).getContext("2d");
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
        bezierCurve: false
    });
});	