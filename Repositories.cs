using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TrotamundosNetCore.Clases;
using TrotamundosNetCore.Data;
using TrotamundosNetCore.Services;

public class RepositorioVehiculo : IRepositorioVehiculo
{
    private readonly ApplicationDbContext _context;

    // Constructor
    public RepositorioVehiculo(ApplicationDbContext context)
    {
        _context = context;
    }

    // Implementación de ObtenerPorId
    public Vehiculos ObtenerPorId(int vehiculoId)
    {
        // Modificado para obtener un solo resultado
        string query = $"exec [trotamundosdb].[dbo].[ObtenerVehiculoWeb] @IdVehiculo = {vehiculoId}";
        return EjecutaQuerySql<Vehiculos>(query).FirstOrDefault(); // Devuelve el primer vehículo o null si no hay coincidencia
    }

    // Método genérico para ejecutar la consulta SQL
    private List<T> EjecutaQuerySql<T>(string query) where T : class
    {
        return _context.Set<T>()
                       .FromSqlRaw(query)
                       .ToList();
    }
}
