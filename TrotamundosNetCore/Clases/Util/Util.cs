using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Reflection;
using TrotamundosNetCore.Clases.Util.Dynatrace;
using TrotamundosNetCore.Models;

namespace TrotamundosNetCore.Clases.Util
{
    public static class Util
    {
        /// <summary>
        /// Metodo para regresar el html de una vista parcial en un string, utilizado comunmente para pasar una o varias vista en un json
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewEngine"></param>
        /// <param name="viewName">Vista parcial a renderizar</param>
        /// <param name="model">Modelo a pasar a la vista</param>
        /// <returns></returns>
        public static async Task<string> RenderPartialViewToString(this Controller controller, ICompositeViewEngine viewEngine, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Genera el modelo base con la cadena de conexión de la base de datos a utilizar
        /// </summary>
        /// <param name="config">Archivo de configuracion</param>
        /// <param name="id">Identificador de la base de datos en el archivo de configuracion</param>
        /// <returns></returns>
        public static Model GeneraModelo(IConfigurationRoot config, string id)
        {
            return new Model(config[$"{id}:Host"], config[$"{id}:User"], config[$"{id}:Pass"], config["Proceso:Nombre"]);
        }

        /// <summary>
        /// Genera el modelo especifico con la cadena de conexión de la base de datos a utilizar
        /// </summary>
        /// <param name="config">Archivo de configuracion</param>
        /// <param name="id">Identificador de la base de datos en el archivo de configuracion</param>
        /// <returns></returns>
        public static T GeneraModelo<T>(IConfigurationRoot config, string id) where T : Model
        {
            return (T)Activator.CreateInstance(typeof(T), config[$"{id}:Host"], config[$"{id}:User"], config[$"{id}:Pass"], config["Proceso:Nombre"]);
        }

        /// <summary>
        /// Genera un cliente Dynatrace con la configuracion en appsettings
        /// </summary>
        /// <param name="config">Archivo de configuracion</param>
        /// <returns>Cliente para realizar consultas a Dynatrace</returns>
        public static ClienteDynatrace GeneraClienteDynatrace(IConfigurationRoot config)
        {
            return new ClienteDynatrace(config["Apis:Dynatrace:Endpoint"], config["Apis:Dynatrace:Environment"], Encrypt.Desencriptar(config["Apis:Dynatrace:Token"]));
        }

        /// <summary>
        /// Genera un cliente Elastic con la configuracion en appsettings
        /// </summary>
        /// <param name="config">Archivo de configuracion</param>
        /// <returns>Cliente para realizar consultas a Elastic</returns>
        public static ClienteElastic GeneraClienteElastic(IConfigurationRoot config)
        {
            return new ClienteElastic(config["Apis:Elastic:Endpoint"], Encrypt.Desencriptar(config["Apis:Elastic:User"]), Encrypt.Desencriptar(config["Apis:Elastic:Pass"]));
        }

        /// <summary>
        /// Genera un dataset para una grafica en chartjs
        /// </summary>
        /// <param name="data">Datos a graficas</param>
        /// <param name="label">Etiqueta para dataset</param>
        /// <param name="borderColor">Color de borde</param>
        /// <param name="borderWidth">Tamaño de borde</param>
        /// <param name="backgroundColor">Color de fondo</param>
        /// <param name="fill">Bandera para habilitar relleno/color de fondo</param>
        /// <param name="stack"></param>
        /// <param name="tension"></param>
        /// <param name="pointRadius"></param>
        /// <param name="pointBackgroundColor"></param>
        /// <returns></returns>
        public static ChartDataSet GeneraDataset(List<PuntoChart> data, string label, string borderColor, double borderWidth, string backgroundColor = "rgba(0,0,0,0)", bool fill = false, string stack = "line", double tension = 0, double pointRadius = 0, string pointBackgroundColor = null)
        {
            return new ChartDataSetPoint(label, backgroundColor, borderColor, borderWidth, fill, stack, data, tension, pointRadius, pointBackgroundColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id del div que contiene a la grafica</param>
        /// <param name="idCanvas">Id del canvas de la grafica</param>
        /// <param name="data"></param>
        /// <param name="tipoGrafica">Tipo de grafica </param>
        /// <param name="fontColor"></param>
        /// <param name="gridLinesColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="opciones"></param>
        /// <param name="plugins"></param>
        /// <returns></returns>
        public static Chartjs GeneraGrafica(string id, string idCanvas, ChartjsData data, string tipoGrafica, string fontColor = "#000000", string gridLinesColor = "rgba(0,0,0,0.3)", string backgroundColor = null, ChartjsOptions opciones = null, List<string> plugins = null)
        {
            ChartjsOptions options = ChartjsOptions.Default(fontColor, gridLinesColor);
            if (opciones != null)
                options = opciones;

            List<string> pluginsList = new List<string>();
            if (plugins != null)
                pluginsList = plugins;

            return new Chartjs(id, idCanvas, new ChartjsConfig()
            {
                data = data,
                type = tipoGrafica,
                options = options,
                plugins = pluginsList
            }, backgroundColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metrica"></param>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        /// <param name="cliente"></param>
        /// <param name="id"></param>
        /// <param name="idCanvas"></param>
        /// <param name="tipoGrafica"></param>
        /// <param name="colores"></param>
        /// <param name="fontColor"></param>
        /// <param name="gridLinesColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="opciones"></param>
        /// <param name="plugins"></param>
        /// <param name="stack"></param>
        /// <param name="fill"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Chartjs GeneraGraficaMetricaDynatrace(string metrica, DateTime inicio, DateTime fin, ClienteDynatrace cliente, string id, string idCanvas, string tipoGrafica, List<ColorGrafica> colores, string fontColor = "#000000", string gridLinesColor = "rgba(0,0,0,0.3)", string backgroundColor = null, ChartjsOptions opciones = null, List<string> plugins = null, string stack = "line", bool fill = false, string label = "")
        {
            Metrics resultados = cliente.ConsultaAPIMetricas(metrica, inicio, fin);

            List<ChartDataSet> datasets = new List<ChartDataSet>();

            int i = 0;

            foreach (MetricsData d in resultados.result.First().data)
            {
                if (d.dimensions.Count > 0)
                {
                    Entity entidad = cliente.ConsultaEntidad(d.dimensions.First());

                    ColorGrafica color = colores[i % colores.Count];
                    ColorGrafica relleno = color.Clone();
                    relleno.a = 0.6M;

                    ChartDataSetPoint dataset = new ChartDataSetPoint(entidad.displayName, relleno.ToString(), color.ToString(), 2, fill, stack, d.RegresaPuntos(), 0, 0, null);

                    datasets.Add(dataset);

                    i++;
                }
                else
                {
                    ColorGrafica color = colores[i % colores.Count];
                    ColorGrafica relleno = color.Clone();
                    relleno.a = 0.6M;

                    ChartDataSetPoint dataset = new ChartDataSetPoint(label, relleno.ToString(), color.ToString(), 2, fill, stack, d.RegresaPuntos(), 0, 0, null);

                    datasets.Add(dataset);

                    i++;
                }

            }

            ChartjsData data = new ChartjsData(datasets);

            return GeneraGrafica(id, idCanvas, data, tipoGrafica, opciones: opciones, backgroundColor: backgroundColor, fontColor: fontColor, plugins: plugins, gridLinesColor: gridLinesColor);

        }

        public static Chartjs GeneraGraficaHorizontalMetricaDynatrace(string metrica, DateTime inicio, DateTime fin, ClienteDynatrace cliente, string id, string idCanvas, string tipoGrafica, List<ColorGrafica> colores, string fontColor = "#000000", string gridLinesColor = "rgba(0,0,0,0.3)", string backgroundColor = null, ChartjsOptions opciones = null, List<string> plugins = null, string stack = "line", bool fill = false, string label = "")
        {
            Metrics resultados = cliente.ConsultaAPIMetricas(metrica, inicio, fin);

            List<ChartDataSet> datasets = new List<ChartDataSet>();

            int i = 0;

            foreach (MetricsData d in resultados.result.First().data)
            {
                if (d.dimensions.Count > 0)
                {
                    Entity entidad = cliente.ConsultaEntidad(d.dimensions.First());

                    ColorGrafica color = colores[i % colores.Count];
                    ColorGrafica relleno = color.Clone();
                    relleno.a = 0.6M;

                    ChartDataSetPoint dataset = new ChartDataSetPoint(entidad.displayName, relleno.ToString(), color.ToString(), 2, fill, stack, d.RegresaPuntosEnY(entidad), 0, 0, null);

                    datasets.Add(dataset);

                    i++;
                }
                else
                {
                    ColorGrafica color = colores[i % colores.Count];
                    ColorGrafica relleno = color.Clone();
                    relleno.a = 0.6M;

                    ChartDataSetPoint dataset = new ChartDataSetPoint(label, relleno.ToString(), color.ToString(), 2, fill, stack, d.RegresaPuntosEnY(label), 0, 0, null);

                    datasets.Add(dataset);

                    i++;
                }

            }

            ChartjsData data = new ChartjsData(datasets);

            return GeneraGrafica(id, idCanvas, data, tipoGrafica, opciones: opciones, backgroundColor: backgroundColor, fontColor: fontColor, plugins: plugins, gridLinesColor: gridLinesColor);

        }


        public static Chartjs GeneraGraficaMetricaDynatracePromedio(string metrica, DateTime inicio, DateTime fin, ClienteDynatrace cliente, string id, string idCanvas, string tipoGrafica, List<ColorGrafica> colores, string fontColor = "#000000", string gridLinesColor = "rgba(0,0,0,0.3)", string backgroundColor = null, ChartjsOptions opciones = null, List<string> plugins = null)
        {
            Metrics resultados = cliente.ConsultaAPIMetricas(metrica, inicio, fin);

            List<ChartDataSet> datasets = new List<ChartDataSet>();

            ChartDataSetValues dataset = new ChartDataSetValues();

            List<string> entidades = new List<string>();

            int i = 0;

            foreach (MetricsData d in resultados.result.First().data)
            {
                Entity entidad = cliente.ConsultaEntidad(d.dimensions.First());

                ColorGrafica color = colores[i % colores.Count];
                ColorGrafica relleno = color.Clone();
                relleno.a = 0.6M;


                dataset.data.Add(Math.Round(d.Promedio(), 2));
                dataset.backgroundColor.Add(color.ToString());

                entidades.Add(entidad.displayName);

                i++;
            }

            datasets.Add(dataset);

            ChartjsData data = new ChartjsData(datasets);

            data.labels = entidades;

            return GeneraGrafica(id, idCanvas, data, tipoGrafica, opciones: opciones, backgroundColor: backgroundColor, fontColor: fontColor, plugins: plugins, gridLinesColor: gridLinesColor);

        }

        public static Highcharts GeneraTilemapHostHealth(ClienteDynatrace cliente, string hostgroup, string id)
        {
            List<Entity> entidades = cliente.ConsultaEntidadesEnHostgroup(hostgroup);

            List<HighchartsTilemapData> data = new List<HighchartsTilemapData>();

            int x = 0;
            int y = 0;
            int x1 = 0;

            int yMax = (int)Math.Ceiling(Math.Sqrt(entidades.Count));

            foreach (Entity entidad in entidades)
            {
                data.Add(new HighchartsTilemapData(entidad.displayName, x, y, entidad.properties.state == "RUNNING" ? 0 : 2));


                y++;

                if (y >= yMax)
                {
                    x++;
                    y = x1;
                }
            }

            return new Highcharts(id, data, x, yMax);
        }

        //public static List<ColorGrafica> ObtenerListaColores(IConfigurationRoot config)
        //{
        //    Model modelo = GeneraModelo(config, "Conexiones:Monitoreo:BAZ_CDMX");
        //    return modelo.EjecutaQuery<ColorGrafica>("select * from Catalogos.dbo.ColoresGraficas with(nolock)");
        //}

    }
}
