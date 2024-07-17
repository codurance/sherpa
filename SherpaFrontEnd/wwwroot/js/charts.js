window.generateColumnsChart = function (elementId, chartData, generalMetrics) {
    const targetElement = document.getElementById(elementId);

    if (!targetElement || targetElement.innerHTML !== "") {
        return;
    }
    
    if (!chartData) {
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
                columnWidth: '60%',
                borderRadius: 0,
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
            max: chartData.config.maxValue,
            stepSize: chartData.config.stepSize,
            decimalsInFloat: chartData.config.decimalsInFloat,
        },
        legend: {
            position: "top",
            horizontalAlign: "left",
            offsetX: -6
        },
        annotations: {
            yaxis: [{
                y: generalMetrics.average,
                strokeDashArray: 5,
                borderColor: "#e44f00",
                width: '100%',
                label: {
                    borderColor: "#e44f00",
                    style: {
                        color: '#fff',
                        background: '#e44f00',
                    },
                    text: 'Average'
                }
            }, {
                y: generalMetrics.aspirational,
                strokeDashArray: 5,
                borderColor: "#008d73",
                width: '100%',
                label: {
                    borderColor: "#008d73",
                    style: {
                        color: '#fff',
                        background: '#008d73',
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