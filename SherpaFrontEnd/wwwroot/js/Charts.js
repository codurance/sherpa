let chart;
window.generateColumnsChart = function (elementId, series, categories, yaxis) {
    const targetElement = document.getElementById(elementId);

    if (!targetElement || targetElement.innerHTML !== "") {
        return;
    }

    const options = {
        series,
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
            categories
        },
        yaxis: {
            max: 1,
            ...yaxis
        },
        fill: {
            type: 'image',
            image: {
                src: ["./img/charts/tortoise-shell.svg", "./img/charts/bermuda-traingle.png", "./img/charts/wavey-fingerprint.png"],
            },
        },
        legend: {
            position: "top",
            fontSize: '20px',
            markers: {
                fillColors: ["transparent", "transparent", "transparent"],
                offsetX: -5,
                offsetY: -5,
                customHTML: [
                    function() {
                        return `<div onclick="handleLegendClick()" style="display: block; width: 20px; height: 20px; padding: 2px; border: 1px solid green; border-radius: 5px"><span style="display: block; width: 100%; height: 100%; object-fit: cover; background-image: url('./img/charts/tortoise-shell.svg'); border-radius: 3px" /></div>`
                    }, 
                    function() {
                        return `<div onclick="handleLegendClick()" style="display: block; width: 20px; height: 20px; padding: 2px; border: 1px solid blue; border-radius: 5px"><span style="display: block; width: 100%; height: 100%; object-fit: cover; background-image: url('./img/charts/bermuda-traingle.png'); border-radius: 3px" /></div>`
                    }
                ],
            },
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

}

window.generateHorizontalColumnsChart = function (elementId, series, categories) {
    const targetElement = document.getElementById(elementId);

    if (!targetElement || targetElement.innerHTML !== "") {
        return;
    }

    const options = {
        series,
        chart: {
            type: 'bar',
            height: 350,
            stacked: true,
            stackType: '100%'
        },
        plotOptions: {
            bar: {
                horizontal: true,
            },
        },
        stroke: {
            width: 1,
            colors: ['#fff']
        },
        title: {
            text: '100% Stacked Bar'
        },
        xaxis: {
            categories,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return val + "K"
                }
            }
        },
        fill: {
            opacity: 1

        },
        legend: {
            position: 'top',
            horizontalAlign: 'left',
            offsetX: 40
        },
        annotations: {
            xaxis: [{
                x: 35,
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
                x: 75,
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

}

window.generateScatterPlotChart = function (elementId, series, categories) {
    const targetElement = document.getElementById(elementId);

    if (!targetElement || targetElement.innerHTML !== "") {
        return;
    }

    const options = {
        series,
        chart: {
            height: 350,
            type: 'scatter',
            animations: {
                enabled: false,
            },
            zoom: {
                enabled: false,
            },
            toolbar: {
                show: false
            }
        },
        colors: ['#056BF6', '#D2376A'],
        xaxis: {
            tickAmount: 10,
            min: 0,
            max: 40
        },
        yaxis: {
            tickAmount: 7
        },
        markers: {
            size: 20
        },
        fill: {
            type: 'image',
            opacity: 1,
            image: {
                src: ['./img/charts/icons/cross.svg', './img/charts/icons/square.svg', './img/charts/icons/triangle.svg'],
                width: 40,
                height: 40
            }
        },
        legend: {
            labels: {
                useSeriesColors: true
            },
            markers: {
            fillColors: ['transparent', 'transparent', 'transparent'],
                customHTML: [
                    function() {
                        return '<img src="./img/charts/icons/cross.svg"/>'
                    }, function() {
                        return '<img src="./img/charts/icons/square.svg"/>'
                    }, function() {
                        return '<img src="./img/charts/icons/triangle.svg"/>'
                    }
                ]
            }
        }
    };

    chart = new ApexCharts(targetElement, options);
    chart.render();
}

window.updateScatterPlotChartSeries = function (chartId, series){
    console.log(chartId);
    console.log(series);
    if(!chart) return;
    chart.updateSeries(series, true);
}