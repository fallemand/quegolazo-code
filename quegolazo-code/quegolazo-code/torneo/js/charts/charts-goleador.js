$(document).ready(function ($) {
    $("#menu-fases li").on("click", function () {
        //$("#graficosFases").remove();        
        //$('<canvas/>', {
        //    id: 'graficosFases'+$(this).text(),
        //    class : 'canvas-lg'            
        //}).appendTo($('#containerCanvas'));
        var index = ($(this).text().length > 1) ? 0 : parseInt($(this).text());
        $("#numFaseGrafico").text($(this).text());
        var ctx = $("#graficosFases").get(0).getContext("2d");
        myPieChart.destroy();
        myPieChart = new Chart(ctx).Bar(datosFases[index]);
    });
        var ctx = $("#graficosFases").get(0).getContext("2d");
        var data = {
            labels: ["Antonio Herrera", "Paulita Pedrosa", "Apache Tevez", "Pipa HIguain", "Amadeo Sabattini", "No me acuerdo", "Tampoco me acuerdo"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,0.8)",
                    highlightFill: "rgba(220,220,220,0.75)",
                    highlightStroke: "rgba(220,220,220,1)",
                    data: [20, 2, 6, 15, 18, 12, 10]
                }
            ]
        };
        var data2 = {
            labels: ["Antonio Alzogaray", "Paulita Pedrosa", "Apache Tevez", "Pipa HIguain", "Amadeo Sabattini", "No me acuerdo"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,0.8)",
                    highlightFill: "rgba(220,220,220,0.75)",
                    highlightStroke: "rgba(220,220,220,1)",
                    data: [4, 1, 8, 3, 5, 4]
                }
            ]
        };
        var data3 = {
            labels: ["Flor", "Rompe", "Huevos!", "y la", "paula", "tambien!"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,0.8)",
                    highlightFill: "rgba(220,220,220,0.75)",
                    highlightStroke: "rgba(220,220,220,1)",
                    data: [2, 1, 6, 3, 5, 4]
                }
            ]
        };
   
        var data4 = {
            labels: ["Antonio Herrera", "Paulita Pedrosa", "Apache Tevez", "Pipa HIguain", "Amadeo Sabattini", "No me acuerdo"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,0.8)",
                    highlightFill: "rgba(220,220,220,0.75)",
                    highlightStroke: "rgba(220,220,220,1)",
                    data: [2, 1, 6, 3, 5, 4]
                }
            ]
        };
        var datosFases = [];
        datosFases.push(data);
        datosFases.push(data2);
        datosFases.push(data3);
        datosFases.push(data4);
        var myPieChart = new Chart(ctx).Bar(data);     
    
});

