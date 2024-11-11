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

            // Dependency Injection
            builder.Services.AddScoped<IMilkService, MilkService>();
            builder.Services.AddScoped<UserMetricsService>();

            // Repository Injection
            builder.Services.AddScoped<UserMetricsRepository>();
            builder.Services.AddScoped<MilkRepository>();

            // DbContext Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper Configuration
            var mappingConfiguration = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // SSL Configuration for Development
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                return app.Environment.IsDevelopment() ? true : errors == SslPolicyErrors.None;
            };

            // Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS Policy
            app.UseCors("AllowAngularApp");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
