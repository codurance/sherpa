window.generateColumnsChart = function (elementId, chartData) {
    const targetElement = document.getElementById(elementId);

    if (!targetElement || targetElement.innerHTML !== "") {
        return;
    }

    const options = {
        series: chartData.series,
        chart: {
            type: 'bar',
            height: 350,
            animations: {
                enabled: false,
            },
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '35%',
                borderRadius: 10,
                borderRadiusApplication: "end",
            },
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            show: true,
            width: 4,
            colors: ['transparent']
        },
        xaxis: {
            categories: chartData.categories,
        },
        yaxis: {
            max: chartData.maxValue,
        },
        legend: {
            position: "top",
        },
        annotations: {
            yaxis: [{
                y: 2,
                strokeDashArray: 5,
                borderColor: "#ff0000",
                width: '100%',
                label: {
                    borderColor: "#ff0000",
                    style: {
                        color: '#fff',
                        background: '#ff0000',
                    },
                    text: 'Average'
                }
            }, {
                y: 5,
                strokeDashArray: 5,
                borderColor: "#ff8c00",
                width: '100%',
                label: {
                    borderColor: "#ff8c00",
                    style: {
                        color: '#fff',
                        background: '#ff8c00',
                    },
                    text: 'Aspirational'
                }
            }
            ],
        }
    };

    const chart = new ApexCharts(targetElement, options);
    chart.render();
    if (chartData.series.length > 4) {
        const defaultDeselectedSeries = chartData.series.slice(0, -4);
        defaultDeselectedSeries.forEach(seriesElement => {
            chart.hideSeries(seriesElement.name)
        })
    }
}