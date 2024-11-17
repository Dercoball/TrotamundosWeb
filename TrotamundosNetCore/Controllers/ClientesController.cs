using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using TrotamundosNetCore.Clases;
using TrotamundosNetCore.Models;

namespace TrotamundosNetCore.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IConfigurationRoot _config;
        private readonly ICompositeViewEngine _viewEngine;

        // Constructor de controlador con middleware
        public ClientesController(ILogger<ClientesController> logger, IConfiguration config, ICompositeViewEngine viewEngine)
        {
            _logger = logger;
            _config = (IConfigurationRoot)config;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            return View("ConsultaClientes");
        }

        // Acción para obtener los clientes desde el modelo
        public IActionResult ConsultaClientes()
        {
            List<Clientes> clientes = new List<Clientes>();

            try
            {
                // Obtener parámetros de conexión desde el archivo de configuración
                string host = _config["DatabaseSettings:Host"];
                string user = _config["DatabaseSettings:User"];
                string pass = _config["DatabaseSettings:Password"];
                string proceso = "ConsultaClientes"; // Nombre de proceso o aplicación

                // Instanciar el modelo con la cadena de conexión
                Model clienteModel = new Model(host, user, pass, proceso);

                // Obtener la lista de clientes
                clientes = clienteModel.ObtenerClientes();
            }
            catch (Exception ex)
            {
                // Manejar errores
                _logger.LogError(ex, "Error al consultar los clientes");
            }

            // Devolver la vista parcial con la lista de clientes
            return PartialView("_Clientes", clientes);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
