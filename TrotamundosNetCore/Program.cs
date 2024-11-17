using Microsoft.EntityFrameworkCore;
using TrotamundosNetCore.Data;
using TrotamundosNetCore.Services;

namespace TrotamundosNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registrar el ApplicationDbContext con la cadena de conexión
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registrar los servicios y repositorios
            builder.Services.AddScoped<IVehiculoService, VehiculoService>();
            builder.Services.AddScoped<IRepositorioVehiculo, RepositorioVehiculo>();

            // Agregar los controladores y vistas
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configuración de la solicitud HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Iniciar la aplicación
            app.Run();
        }
    }
}
