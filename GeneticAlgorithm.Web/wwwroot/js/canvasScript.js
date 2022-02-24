/*** functions ***/


// ajax call to get chart data from back end
GetChartData = function () {

    $.ajax({
        url: '/Home/GetGenerationFitness',
        //data: JSON.stringify(jsonObject),
        type: "post",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var canvasElement = document.getElementById("scatterChart");
            var ctx = canvasElement.getContext('2d');
            BindChartDataToCanvasElement(response, ctx);
        }
    })
}

BindChartDataToCanvasElement = function (graphData, ctx) {
    var graphData = JSON.parse(graphData);
    console.log(graphData);
    var myChart = new Chart(ctx, {
        type: 'line',
        height: "100%",
        width: "300px",
        responsive: true,
        animation: true,
        stacked: true,
        legend: { position: 'bottom' },

        data: graphData
        , options: {
            events: ['click'],
            scaleShowValues: true,
            scales: {
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Generation'
                    },
                    ticks: {
                        beginAtZero: true,
                    }
                }],
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Fitness'
                    },
                    ticks: {
                        beginAtZero: true,
                        min: 0,
                    },
                    type: 'linear',
                    position: 'bottom'
                }]

            }
        }
    });
}