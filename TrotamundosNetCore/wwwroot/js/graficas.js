var Charts = [];

function EliminaGraficas() {
    if (Charts) {
        for (var i = 0; i < Charts.length; i++) {
            Charts[i].chart.destroy(); //Se elimina grafica de memoria
        }
        Charts = null; //Se eliminan las referencias
        Charts = [];
    }
}

function EliminaGrafica(id) {
    if (Charts) {
        let r = [];
        for (var i = 0; i < Charts.length; i++) {
            if (Charts[i].id == id) {
                Charts[i].chart.destroy(); //Se elimina grafica de memoria
                r.push(i);
            }
        }

        for (var i = 0; i < r.length; i++) {
            Charts.splice(r[i], 1); //Se quita de arreglo
        }
    }
}

function DataLabelFormatHorizontal(value, context) {
    return +value.x.toFixed(2);
}

function PluginBackgroundColor(idGrafica, color) {
    return {
        id: `${idGrafica}_plugin_background`,
        beforeDraw: (chart) => {
            const { ctx } = chart;
            ctx.save();
            ctx.globalCompositeOperation = 'destination-over';
            ctx.fillStyle = color;
            ctx.fillRect(0, 0, chart.width, chart.height);
            ctx.restore();
        }
    }
}

function CreaGrafica(id, chart, colorFondo) {
    let ctx = document.getElementById(id).getContext('2d');

    //Color de fondo
    chart.plugins.push(PluginBackgroundColor(id, colorFondo));

    //Grafica
    Charts.push({
        id,
        chart: new Chart(ctx, chart)
    });
}