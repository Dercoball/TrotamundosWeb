using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TrotamundosNetCore.Models;
using System;
using System.Collections.Generic;
using TrotamundosNetCore.Clases;
using TrotamundosNetCore.Services;
using Glimpse.AspNet.Model;

namespace TrotamundosNetCore.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ILogger<VehiculosController> _logger;
        private readonly IConfiguration _config;
        private readonly IVehiculoService _vehiculoService;

        public VehiculosController(
            ILogger<VehiculosController> logger,
            IConfiguration config,
            IVehiculoService vehiculoService)
        {
            _logger = logger;
            _config = config;
            _vehiculoService = vehiculoService;
        }

        public IActionResult Index()
        {
            return View("ConsultaVehiculos");
        }

        [HttpPost]
        public IActionResult DownloadWordDocument([FromBody] TrotamundosNetCore.Clases.RequestModel model)
        {
            var placeholders = model.Placeholders; // Obtén las variables dinámicas.
            var imagesBase64 = model.ImagesBase64; // Obtén las imágenes en formato Base64.

            var wordBytes = WordGenerator.GenerateWordDocument(placeholders, imagesBase64);

            // Devuelve el archivo como un adjunto descargable.
            return File(wordBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedDocument.docx");
        }


        public IActionResult ConsultaVehiculos()
        {
            List<Vehiculos> vehiculos = new List<Vehiculos>();
            try
            {
                // Obtener lista de vehículos desde la base de datos
                string host = _config["DatabaseSettings:Host"];
                string user = _config["DatabaseSettings:User"];
                string pass = _config["DatabaseSettings:Password"];
                string proceso = "ConsultaVehiculos";

                Model vehiculoModel = new Model(host, user, pass, proceso);
                vehiculos = vehiculoModel.ObtenerVehiculos();

                // Procesar imágenes en Base64
                ProcesarImagenesBase64(vehiculos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar los vehículos");
                return View("Error"); // Retorna una vista de error si ocurre una excepción
            }

            return PartialView("_Vehiculos", vehiculos);
        }

        private void ProcesarImagenesBase64(List<Vehiculos> vehiculos)
        {
            foreach (var vehiculo in vehiculos)
            {
                if (!string.IsNullOrEmpty(vehiculo.Espejo_retrovisor_foto))
                {
                    vehiculo.Espejo_retrovisor_foto = "data:image/jpeg;base64," + vehiculo.Espejo_retrovisor_foto;
                }
                if (!string.IsNullOrEmpty(vehiculo.Espejo_izquierdo_foto))
                {
                    vehiculo.Espejo_izquierdo_foto = "data:image/jpeg;base64," + vehiculo.Espejo_izquierdo_foto;
                }
                // Puedes agregar más propiedades de imágenes si es necesario
            }
        }

        public JsonResult ConsultaVehiculo(int vehiculoId)
        {
            try
            {
                // Obtener el vehículo por su ID
                var vehiculo = _vehiculoService.ObtenerVehiculo(vehiculoId);
                if (vehiculo == null)
                {
                    return Json(new { success = false, message = "Vehículo no encontrado." });
                }

                // Retornar las fotos en Base64 en formato adecuado
                return Json(new
                {
                    success = true,
                    Espejo_retrovisor_foto = !string.IsNullOrEmpty(vehiculo.Espejo_retrovisor_foto)
                        ? "data:image/jpeg;base64," + vehiculo.Espejo_retrovisor_foto
                        : null,
                    Espejo_izquierdo_foto = !string.IsNullOrEmpty(vehiculo.Espejo_izquierdo_foto)
                        ? "data:image/jpeg;base64," + vehiculo.Espejo_izquierdo_foto
                        : null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar el vehículo");
                return Json(new { success = false, message = "Error al consultar el vehículo." });
            }
        }
    }
}
