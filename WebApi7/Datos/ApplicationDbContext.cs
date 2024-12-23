using Microsoft.EntityFrameworkCore;
using WebApi7.Models;

namespace WebApi7.Datos
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVilla { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Nombre = "Casa Byron",
                Detalle = "Casa donde se vive Byron",
                ImagenUrl = "",
                Ocupantes = 4,
                MetrosCuadrados = 400,
                Tarifa = 5,
                Amenidad = "",
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
            },
            new Villa()
            {
                Id = 2,
                Nombre = "Casa Jose",
                Detalle = "Joselandia",
                ImagenUrl = "",
                Ocupantes = 10,
                MetrosCuadrados = 800,
                Tarifa = 44,
                Amenidad = "ABC",
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
            }
            );

            
        }

    }
}
