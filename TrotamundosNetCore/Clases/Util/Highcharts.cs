using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Newtonsoft.Json;

namespace TrotamundosNetCore.Clases.Util
{
    public class Highcharts
    {
        public string ContainerId { get; set; }

        public List<HighchartsTilemapData> data { get; set; }

        public int maxX { get; set; }
        public int maxY { get; set; }

        public Highcharts()
        {

        }

        public Highcharts(string containerId, List<HighchartsTilemapData> data, int maxX, int maxY)
        {
            ContainerId = containerId;
            this.data = data;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public string dataJson => JsonConvert.SerializeObject(data);
    }

    public class HighchartsTilemapData
    {
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public double value { get; set; }

        public HighchartsTilemapData()
        {

        }

        public HighchartsTilemapData(string name, int x, int y, double value)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }

    public class GraficaHighcharts
    {
        public Highsoft.Web.Mvc.Charts.Highcharts highcharts { get; set; }

        private HighchartsRenderer renderer { get; set; }

        public GraficaHighcharts(XAxis XAxis, YAxis YAxis, PlotOptions PlotOptions, List<Series> Series, string id)
        {
            highcharts = new Highsoft.Web.Mvc.Charts.Highcharts()
            {
                XAxis = new List<XAxis> { XAxis },
                YAxis = new List<YAxis> { YAxis },
                PlotOptions = PlotOptions,
                Series = Series,
                ID = id
            };

            renderer = new HighchartsRenderer(highcharts);
        }
        public string ObtenerGrafica()
        {
            return renderer.RenderHtml();
        }
    }


}
