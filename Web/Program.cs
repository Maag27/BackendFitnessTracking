using ApiSampleFinal.Automapper;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.IO;
using System.Net;
using System.Net.Security;

namespace ApiSampleFinal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cargar configuración de appsettings.json y variables de entorno
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Configuration;

            // Validar y obtener la cadena de conexión
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada en appsettings.json");
            }

            // Especificar el puerto de Render (8080) para asegurarnos de que Render redirija correctamente las solicitudes
            builder.WebHost.UseUrls("http://*:8080");

            // Añadir servicios al contenedor
            builder.Services.AddControllers();

            // Repositorios y servicios
            builder.Services.AddScoped<IMilkRepository, MilkRepository>();
            builder.Services.AddScoped<UserMetricsService>();
            builder.Services.AddScoped<UserMetricsRepository>();
            builder.Services.AddScoped<MilkRepository>();

            // Configuración de DbContext con PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Configuración de Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuración de AutoMapper
            var mappingConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Configuración de CORS para permitir el frontend desde localhost:4200
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                        policy.WithOrigins("http://localhost:4200", "https://backendfitnesstracking.onrender.com") // Permitir origen local y en Render
                          .AllowAnyMethod()                      // Permitir cualquier método HTTP (GET, POST, etc.)
                          .AllowAnyHeader();                     // Permitir cualquier encabezado
                });
            });

            var app = builder.Build();

            // Configuración SSL para desarrollo
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // Aceptar certificado SSL solo en desarrollo
                return app.Environment.IsDevelopment() ? true : errors == SslPolicyErrors.None;
            };

            // Pipeline de Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Aplicar política de CORS
            app.UseCors("AllowAngularApp");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
