using TrotamundosNetCore;
using Microsoft.EntityFrameworkCore;
using TrotamundosNetCore.Models;
using TrotamundosNetCore.Clases;  // Asegúrate de que esta clase contiene las entidades correctas

namespace TrotamundosNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definir las entidades de tu modelo
        public DbSet<Vehiculos> Vehiculos { get; set; }

        // Puedes agregar más DbSet para otras entidades si es necesario

        // Configuración adicional si es necesario
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Si tu tabla tiene un esquema diferente, especifícalo aquí
            // Ejemplo: Si la tabla es Vehiculos y está en el esquema "dbo"
            modelBuilder.Entity<Vehiculos>().ToTable("Vehiculos", "dbo");

            // Si tienes configuraciones específicas para las propiedades de las entidades, añádelas aquí
            modelBuilder.Entity<Vehiculos>()
                .Property(v => v.ID)
                .IsRequired();

            // Aquí puedes agregar más configuraciones como relaciones, validaciones, etc.
        }
    }
}
