using Newtonsoft.Json;

namespace TrotamundosNetCore.Clases.Util
{
    public class Chartjs
    {
        public string ContainerId { get; set; }
        public string CanvasId { get; set; }

        public ChartjsConfig Chart { get; set; }

        public string BackgroundColor { get; set; } = "rgba(1,1,1,0)";

        public Chartjs(string containerId, string canvasId, ChartjsConfig chart)
        {
            ContainerId = containerId;
            CanvasId = canvasId;
            Chart = chart;
        }

        public Chartjs(string containerId, string canvasId, ChartjsConfig chart, string backgroundColor)
        {
            ContainerId = containerId;
            CanvasId = canvasId;
            Chart = chart;
            BackgroundColor = backgroundColor;
        }

        public void AgregaDataset(ChartDataSet dataset)
        {
            if (dataset != null)
            {
                Chart.data.datasets.Add(dataset);
            }
        }

        public string Json => JsonConvert.SerializeObject(Chart);

    }

    /// <summary>
    /// https://www.chartjs.org/docs/latest/
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsConfig
    {
        public string type { get; set; } = "line";
        public ChartjsData data { get; set; }
        public ChartjsOptions options { get; set; }
        [JsonProperty(ItemConverterType = typeof(JSConverter))]
        public List<string> plugins { get; set; } = new List<string>();

        public ChartjsConfig()
        {

        }

    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsData
    {
        public List<ChartDataSet> datasets { get; set; }

        public List<string> labels { get; set; }

        public ChartjsData()
        {

        }

        public ChartjsData(List<ChartDataSet> datasets)
        {
            this.datasets = datasets;
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsOptions
    {
        public bool responsive { get; set; } = true;
        public bool maintainAspectRatio { get; set; }
        public object elements { get; set; }
        public ChartjsAnimation animation { get; set; }
        public ChartjsTooltips tooltips { get; set; }
        public object scales { get; set; }
        public string indexAxis { get; set; }
        public object plugins { get; set; }

        public ChartjsOptions()
        {

        }

        public static ChartjsOptions Default(string fontColor, string gridLineColor, string unidadTiempo = "hour", string formatoTiempo = "HH:mm") => new ChartjsOptions()
        {
            responsive = true,
            maintainAspectRatio = false,
            animation = new ChartjsAnimation(),
            scales = new
            {
                xAxis = new
                {
                    type = "time",
                    time = new
                    {
                        unit = unidadTiempo,
                        displayFormats = new Dictionary<string, string>()
                        {
                            {unidadTiempo, formatoTiempo }
                        },
                        tooltipFormat = formatoTiempo
                    },
                    ticks = new
                    {
                        autoSkip = true,
                        autoSkipPadding = 25,
                        minRotation = 0,
                        maxRotation = 0,
                        color = fontColor,
                        source = "data"
                    }
                },
                yAxis = new
                {
                    grid = new
                    {
                        display = true,
                        color = gridLineColor
                    },
                    ticks = new
                    {
                        color = fontColor,
                        beginAtZero = true
                    }
                }
            }
        };

        public static ChartjsOptions HorizontalBar(string fontColor, string gridLineColor) => new ChartjsOptions()
        {
            responsive = true,
            maintainAspectRatio = false,
            animation = new ChartjsAnimation(),
            indexAxis = "y",
            plugins = new
            {
                datalabels = new JSDataLabelsFormatter()
                {
                    formatter = "DataLabelFormatHorizontal",
                    align = "end",
                    anchor = "end",
                    color = fontColor
                },
                legend = new ChartjsLegend()
                {
                    display = true,
                    labels = new ChartjsLegendLabel()
                    {
                        color = fontColor
                    }
                }
            },
            scales = new
            {
                x = new
                {

                    ticks = new
                    {
                        color = fontColor
                    },
                    grid = new
                    {
                        display = true,
                        color = gridLineColor
                    }
                },
                y = new
                {
                    ticks = new
                    {
                        color = fontColor
                    }
                }
            }
        };

        public static ChartjsOptions Doughnut(string fontColor) => new ChartjsOptions()
        {
            responsive = true,
            maintainAspectRatio = false,
            animation = new ChartjsAnimation(),
            scales = null,
            indexAxis = null
        };
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsElements
    {
        public object point { get; set; }
        public object line { get; set; }
        public object bar { get; set; }
        public object arc { get; set; }

        public ChartjsElements()
        {

        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsAnimation
    {
        public long duration { get; set; } = 0;

        public ChartjsAnimation()
        {

        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsLegend
    {
        public bool display { get; set; }
        public string position { get; set; }
        public ChartjsLegendLabel labels { get; set; }

        public ChartjsLegend()
        {

        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsLegendLabel
    {
        public string color { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartjsTooltips
    {
        public bool enabled { get; set; }
        public string backgroundColor { get; set; }
        public string titleColor { get; set; }
        public string titleFont { get; set; }
        public string bodyColor { get; set; }
        public string bodyFont { get; set; }
        public object padding { get; set; }


        public ChartjsTooltips()
        {

        }
    }

    public class ChartDataSet
    {

    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartDataSetValues : ChartDataSet
    {
        public List<object> data { get; set; } = new List<object>();
        public List<string> backgroundColor { get; set; } = new List<string>();

        public ChartDataSetValues()
        {

        }
    }


    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChartDataSetPoint : ChartDataSet
    {
        public string label { get; set; }
        public string backgroundColor { get; set; } = "rgba(0,0,0,0)";
        public string borderColor { get; set; }
        public double borderWidth { get; set; }
        public bool fill { get; set; }
        public string stack { get; set; }
        public double pointRadius { get; set; }
        public string pointBackgroundColor { get; set; }

        public List<PuntoChart> data { get; set; } = new List<PuntoChart>();

        public double tension { get; set; }

        public ChartDataSetPoint(string label, string backgroundColor, string borderColor, double borderWidth, bool fill, string stack, List<PuntoChart> data, double tension, double pointRadius, string pointBackgroundColor)
        {
            this.label = label;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
            this.borderWidth = borderWidth;
            this.fill = fill;
            this.stack = stack;
            this.data = data;
            this.tension = tension;
            this.pointRadius = pointRadius;
            this.pointBackgroundColor = pointBackgroundColor;
        }
    }


    public class JSDataLabelsFormatter
    {
        public string align { get; set; }
        public string anchor { get; set; }

        [JsonConverter(typeof(JSConverter))]
        public string formatter { get; set; }
        public string color { get; set; }

        public JSDataLabelsFormatter()
        {

        }
    }

    public class PuntoChart
    {

        public PuntoChart()
        {

        }
    }

    public class PuntoChartValorLong : PuntoChart
    {
        public string x { get; set; }
        public long y { get; set; }

        public PuntoChartValorLong(string x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class PuntoChartValorDouble : PuntoChart
    {
        public string x { get; set; }
        public double y { get; set; }

        public PuntoChartValorDouble(string x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class PuntoChartTiempo : PuntoChart
    {
        public string x { get; set; }

        public PuntoChartTiempo()
        {

        }

        protected PuntoChartTiempo(string t)
        {
            //this.t = $"new moment('{t}')";
            this.x = t;
        }
    }

    public class PuntoChartLong : PuntoChartTiempo
    {
        public long? y { get; set; }

        public PuntoChartLong()
        {

        }

        public PuntoChartLong(string t, long? y) : base(t)
        {
            this.y = y;
        }
    }

    public class PuntoChartDouble : PuntoChartTiempo
    {
        public double? y { get; set; }

        public PuntoChartDouble()
        {

        }

        public PuntoChartDouble(string t, double? y) : base(t)
        {
            this.y = y;
        }
    }

    public class PuntoChartValorDoubleHorizontal : PuntoChart
    {
        public double? x { get; set; }
        public string y { get; set; }

        public PuntoChartValorDoubleHorizontal(double? x, string y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class InfoGrafica
    {
        public string id { get; set; }
        public string json { get; set; }
        public string titulo { get; set; }
        public bool mismaFecha { get; set; }
        public string fechaMin { get; set; }

        public InfoGrafica(string id, string json, string titulo, bool mismaFecha, string fechaMin)
        {
            this.id = id;
            this.json = json;
            this.titulo = titulo;
            this.mismaFecha = mismaFecha;
            this.fechaMin = fechaMin;
        }
    }

    public class JSConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<string>) || objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<string>)
            {
                var lista = (IEnumerable<string>)value;

                writer.WriteStartArray();

                for (int i = 0; i < lista.Count(); i++)
                {
                    writer.WriteRawValue(lista.ElementAt(i));

                    if (i < lista.Count() - 1)
                    {
                        writer.WriteRawValue(",");
                    }
                }

                writer.WriteEndArray();
            }
            else if (value is string)
            {
                writer.WriteRawValue(value.ToString());
            }



        }
    }

}
