using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        // Configuración de mapeo de columnas en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración específica para la entidad Milk
            modelBuilder.Entity<Milk>()
                .Property(m => m.Id)
                .HasColumnType("uuid") // Especificamos el tipo uuid para PostgreSQL
                .HasDefaultValueSql("uuid_generate_v4()"); // Genera un UUID por defecto

            base.OnModelCreating(modelBuilder);
        }
    }
}
