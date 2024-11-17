namespace TrotamundosNetCore.Clases.Util.Dynatrace
{
    public class Metrics
    {
        public int totalCount { get; set; }
        public string nextPageKey { get; set; }
        public string resolution { get; set; }

        public List<MetricsResult> result { get; set; }

        public Metrics()
        {

        }
    }

    public class MetricsResult
    {
        public string metricId { get; set; }
        public string dataPointCountRatio { get; set; }
        public string dimensionCountRatio { get; set; }

        public List<MetricsData> data { get; set; }

        public MetricsResult()
        {

        }
    }

    public class MetricsData
    {
        public List<string> dimensions { get; set; }
        public Dictionary<string, string> dimensionMap { get; set; }

        public List<long> timestamps { get; set; }

        public List<double?> values { get; set; }
        public MetricsData()
        {

        }

        public List<PuntoChart> RegresaPuntos()
        {
            List<PuntoChart> result = new List<PuntoChart>();
            for (int i = 0; i < timestamps.Count && i < values.Count; i++)
            {
                //Millisegundos a fecha y hora local
                DateTime Fecha = DateTimeOffset.FromUnixTimeMilliseconds(timestamps[i]).LocalDateTime;
                double? N = values[i];

                result.Add(new PuntoChartDouble(Fecha.ToString("yyyy-MM-dd HH:mm:ss"), N));
            }

            return result;

        }

        public List<PuntoChart> RegresaPuntosEnY(Entity entidad)
        {
            List<PuntoChart> result = new List<PuntoChart>();
            for (int i = 0; i < timestamps.Count && i < values.Count; i++)
            {
                //Millisegundos a fecha y hora local
                DateTime Fecha = DateTimeOffset.FromUnixTimeMilliseconds(timestamps[i]).LocalDateTime;

                //string Etiqueta = dimensions[i];

                double? N = values[i];

                result.Add(new PuntoChartValorDoubleHorizontal(N, entidad.displayName));
            }

            return result;

        }

        public List<PuntoChart> RegresaPuntosEnY(string label)
        {
            List<PuntoChart> result = new List<PuntoChart>();
            for (int i = 0; i < timestamps.Count && i < values.Count; i++)
            {
                //Millisegundos a fecha y hora local
                DateTime Fecha = DateTimeOffset.FromUnixTimeMilliseconds(timestamps[i]).LocalDateTime;

                //string Etiqueta = dimensions[i];

                double? N = values[i];

                result.Add(new PuntoChartValorDoubleHorizontal(N, label));
            }

            return result;

        }

        public double Promedio()
        {
            double? promedio = values.Where(x => x.HasValue).Average();
            if (promedio.HasValue)
            {
                return promedio.Value;
            }
            return 0;
        }
        public double Suma()
        {
            double? suma = values.Where(x => x.HasValue).Sum();
            if (suma.HasValue)
            {
                return suma.Value;
            }
            return 0;
        }
    }
}
