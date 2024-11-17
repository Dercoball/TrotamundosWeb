using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Diagnostics;
using System.Numerics;
using TrotamundosNetCore.Clases.Util;
using TrotamundosNetCore.Clases.Util.Dynatrace;
using TrotamundosNetCore.Models;

namespace TrotamundosNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigurationRoot _config;
        private readonly ICompositeViewEngine _viewEngine;

        //Se debe replicar las variables privadas y el constructor en los otros controladores que tenga el sitio
        /// <summary>
        /// Constructor de controlador con middleware
        /// </summary>
        /// <param name="logger">Middleware para generar log</param>
        /// <param name="config">Archivo de configuracion appsettings.config</param>
        /// <param name="viewEngine">Motor para generar vistas</param>
        public HomeController(ILogger<HomeController> logger, IConfiguration config, ICompositeViewEngine viewEngine)
        {
            _logger = logger;
            _config = (IConfigurationRoot)config;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contenido()
        {
            return PartialView("_Contenido");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}