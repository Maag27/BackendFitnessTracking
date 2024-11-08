using ApiSampleFinal.Automapper;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Net;
using System.Net.Security;

namespace ApiSampleFinal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container
            builder.Services.AddControllers();

            // Inyección de dependencias manual para evitar referencia circular
            builder.Services.AddScoped<IMilkService, MilkService>();
            builder.Services.AddScoped<UserMetricsService>();

            // Agregar los repositorios necesarios
            builder.Services.AddScoped<UserMetricsRepository>();
            builder.Services.AddScoped<MilkRepository>();

            // Repositorios y DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuración de AutoMapper
            var mappingConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Configuración CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORS_Policy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configuración SSL para desarrollo
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                return app.Environment.IsDevelopment() ? true : errors == SslPolicyErrors.None;
            };

            // Configurar pipeline de solicitud HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Aplicar política CORS
            app.UseCors("CORS_Policy");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
