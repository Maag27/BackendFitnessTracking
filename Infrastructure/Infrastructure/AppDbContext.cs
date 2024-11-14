using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definición de tablas en la base de datos
        public DbSet<Milk> Milks { get; set; } = null!;
        public DbSet<Cow> Cows { get; set; } = null!;
        public DbSet<UserMetrics> UserMetrics { get; set; } = null!;

        // Plantillas de rutinas y ejercicios predefinidos
        public DbSet<Routine> RoutineTemplates { get; set; } = null!;
        public DbSet<Exercise> ExerciseTemplates { get; set; } = null!;
        public DbSet<ExerciseDetail> ExerciseDetailTemplates { get; set; } = null!;

        // Tablas para rutinas y ejercicios específicos del usuario
        public DbSet<UserRoutine> UserRoutines { get; set; } = null!;
        public DbSet<UserExercise> UserExercises { get; set; } = null!;
        public DbSet<UserExerciseDetail> UserExerciseDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de claves primarias
            modelBuilder.Entity<Exercise>()
                .HasKey(e => e.ExerciseId);  // Clave primaria para Exercise

            modelBuilder.Entity<ExerciseDetail>()
                .HasKey(ed => ed.ExerciseDetailId);  // Clave primaria para ExerciseDetail

            modelBuilder.Entity<Routine>()
                .HasKey(r => r.RoutineTemplateId);  // Clave primaria para Routine

            modelBuilder.Entity<UserExercise>()
                .HasKey(ue => ue.UserExerciseId);  // Clave primaria para UserExercise

            modelBuilder.Entity<UserExerciseDetail>()
                .HasKey(ued => ued.UserDetailId);  // Clave primaria para UserExerciseDetail

            modelBuilder.Entity<UserRoutine>()
                .HasKey(ur => ur.UserRoutineId);  // Clave primaria para UserRoutine

            // Configuración de relaciones
            modelBuilder.Entity<Routine>()
                .HasMany(r => r.Exercises)
                .WithOne()
                .HasForeignKey(e => e.RoutineTemplateId);

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.ExerciseDetails)
                .WithOne()
                .HasForeignKey(ed => ed.ExerciseTemplateId);

            modelBuilder.Entity<UserRoutine>()
                .HasMany(ur => ur.UserExercises)
                .WithOne()
                .HasForeignKey(ue => ue.UserRoutineId);

            modelBuilder.Entity<UserExercise>()
                .HasMany(ue => ue.UserExerciseDetails)
                .WithOne()
                .HasForeignKey(ued => ued.UserExerciseId);

            // Configuración adicional si es necesario
            // Aquí podrías agregar configuraciones adicionales si existen entidades sin clave (HasNoKey).

            base.OnModelCreating(modelBuilder);
        }

    }
}
