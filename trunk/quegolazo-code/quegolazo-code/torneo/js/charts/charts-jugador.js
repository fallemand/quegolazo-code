$(document).ready(function($) {
    if (tiposDeGoles != null) {
        var ctx = $("#graficoGoles").get(0).getContext("2d");
        var myPieChart = new Chart(ctx).Doughnut(tiposDeGoles, {
            animateScale: true,
            tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value+'%' %>",
        });     
    }
    $('#goleadores').removeClass("active");
});

