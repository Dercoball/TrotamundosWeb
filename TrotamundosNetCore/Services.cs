using TrotamundosNetCore.Clases;
using TrotamundosNetCore.Models;

namespace TrotamundosNetCore.Services
{
    // Interfaz IVehiculoService
    public interface IVehiculoService
    {
        Vehiculos ObtenerVehiculo(int vehiculoId);
    }

    // Interfaz IRepositorioVehiculo
    public interface IRepositorioVehiculo
    {
        Vehiculos ObtenerPorId(int vehiculoId);
    }

    // Implementación de VehiculoService
    public class VehiculoService : IVehiculoService
    {
        private readonly IRepositorioVehiculo _repositorioVehiculo;

        public VehiculoService(IRepositorioVehiculo repositorioVehiculo)
        {
            _repositorioVehiculo = repositorioVehiculo;
        }

        public Vehiculos ObtenerVehiculo(int vehiculoId)
        {
            // Lógica para obtener el vehículo
            return _repositorioVehiculo.ObtenerPorId(vehiculoId);
        }
    }
}
