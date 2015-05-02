$(document).ready(function ($) {
    $("#graficosFases").show();
    $("#menu-fases li").on("click", function () {        
        var index = ($(this).text().length > 1) ? 0 : parseInt($(this).text());
        if (datosFases[index] != null) {
            $("#graficosFases").show();
            $("#sinGoleadoresFase").hide();
            $("#numFaseGrafico").text($(this).text());
            var ctx = $("#graficosFases").get(0).getContext("2d");
            myPieChart.destroy();
            myPieChart = new Chart(ctx).Bar(datosFases[index]);
        } else {
            $("#sinGoleadoresFase").show();
            $("#graficosFases").hide();
        }
    });
   
    if (datosFases[0] != null) {
        var ctx = $("#graficosFases").get(0).getContext("2d");
        var myPieChart = new Chart(ctx).Bar(datosFases[0]);
    } else {
        //mostrar cartel de grafico no disponible
        $("#graficosFases").hide();
    }

    if (golesDeEquipo != null) {          
            var canvasEquipos = $("#graficoGolesEquipos").get(0).getContext("2d");
            var graficoTiposGoles = new Chart(canvasEquipos).Doughnut(golesDeEquipo, {
                animateScale: true,
                tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
            });
         

        }
    
    if (tiposDeGol != null) {
            
            var canvasTiposGoles = $("#graficoTiposDeGol").get(0).getContext("2d");
            var graficoTiposGol = new Chart(canvasTiposGoles).Doughnut(tiposDeGol, {
                animateScale: true,
                tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
            });
            $("#graficoTipos").removeClass("active in");
        }
        $("#liGraficoTiposGoles").on("click", function () {
            $("#graficoTipos").addClass("active in");
            graficoTiposGol.destroy();
            graficoTiposGol = new Chart(canvasTiposGoles).Doughnut(tiposDeGol, {
                animateScale: true,
                tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
            });
           
        });
        $("#liGraficoGolesEquipos").on("click", function () {
            $("#graficoEquipos").addClass("active in");
            graficoTiposGoles.destroy();
            graficoTiposGoles = new Chart(canvasEquipos).Doughnut(golesDeEquipo, {
                animateScale: true,
                tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
            });
           
            
        });

    
});


