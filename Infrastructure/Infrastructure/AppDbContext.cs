using Domain;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        // Constructor que acepta las opciones de configuración del contexto
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definición de tablas en la base de datos
        public DbSet<Milk> Milks { get; set; }
        public DbSet<Cow> Cows { get; set; }
        public DbSet<UserMetrics> UserMetrics { get; set; }
    }
}
