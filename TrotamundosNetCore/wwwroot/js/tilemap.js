var Tilemaps = [];

function EliminaTilemaps() {
    if (Tilemaps) {
        for (var i = 0; i < Charts.length; i++) {
            Tilemaps[i].chart.destroy(); //Se elimina grafica de memoria
        }
        Tilemaps = null; //Se eliminan las referencias
        Tilemaps = [];
    }
}

function EliminaTilemap(id) {
    if (Tilemaps) {
        let r = [];
        for (var i = 0; i < Charts.length; i++) {
            if (Tilemaps[i].id == id) {
                Tilemaps[i].chart.destroy(); //Se elimina grafica de memoria
                r.push(i);
            }
        }

        for (var i = 0; i < r.length; i++) {
            Tilemaps.splice(r[i], 1); //Se quita de arreglo
        }
    }
}



function CreaTilemap(id, data, maxX, maxY) {

    Highcharts.theme = {

        chart: {
            type: 'tilemap',
            inverted: true,
            height: (100) + '%',

            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 0 },
                stops: [
                    [0, '#0C0C1A'],
                    [1, '#0C0C1A']
                ]
            }
        },
        title: {
            enabled: false,
            text: ''
        },
        subtitle: {
            enabled: false
        },

        colorAxis: {
            dataClasses: [{
                from: 1.99,
                to: 100,
                color: '#d5100a',
                name: 'Error'
            },
            {
                from: 0.99,
                to: 1.99,
                color: '#fcd800',
                name: 'Warning'
            },
            {
                from: 0,
                to: 0.99,
                color: '#00AA23',
                name: 'OK'
            },
            {
                from: -1,
                to: -0.01,
                color: '#aa979797',
                name: 'Inactivo'
            }
            ]
        },

        xAxis: {
            max: maxX,
            min: -1,
            visible: false
        },

        yAxis: {
            max: maxY,
            min: -1,
            visible: false
        },


        tooltip: {
            headerFormat: '',
            pointFormat: '{point.name}'
        },

        plotOptions: {
            series: {
                dataLabels: {
                    enabled: false,
                    format: '',
                    color: '#000000',
                    style: {
                        textOutline: false
                    }
                }
            }
        },
        legend: {
            enabled: false
        }
    };

    // Apply the theme
    Highcharts.setOptions(Highcharts.theme);

    //Grafica
    Tilemaps.push({
        id,
        chart: Highcharts.chart(id, {
            series: [
                {
                    name: '',
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '{point.name}'
                    },
                    data: data
                }
            ]
        }
        )
    });
}