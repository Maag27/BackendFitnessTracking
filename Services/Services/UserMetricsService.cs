using Domain.Models;
using Infrastructure.Repositories;

namespace Services
{
    public class UserMetricsService
    {
        private readonly UserMetricsRepository _repository;

        public UserMetricsService(UserMetricsRepository repository)
        {
            _repository = repository;
        }

        // Método para crear o actualizar las métricas del usuario
        public async Task<UserMetrics> AddOrUpdateUserMetricsAsync(UserMetrics userMetrics)
        {
            // Validamos que el userId no sea nulo
            if (string.IsNullOrEmpty(userMetrics.UserId))
            {
                throw new ArgumentException("El userId no puede ser nulo o vacío.");
            }

            // Obtener las métricas actuales para el userId
            var existingMetrics = await _repository.GetUserMetricsByUserIdAsync(userMetrics.UserId);

            // Si no existen métricas previas, crear nuevas
            if (existingMetrics == null || existingMetrics.Count == 0)
            {
                return await _repository.AddUserMetricsAsync(userMetrics);
            }

            // Si existen métricas, actualizar la existente (suponemos una sola métrica por UserId)
            var metricToUpdate = existingMetrics.First();

            // Actualizar los campos existentes (Ejemplo: Edad, Peso y Altura)
            metricToUpdate.Nombre = userMetrics.Nombre;
            metricToUpdate.Apellido = userMetrics.Apellido;
            metricToUpdate.Edad = userMetrics.Edad;
            metricToUpdate.Peso = userMetrics.Peso;
            metricToUpdate.Altura = userMetrics.Altura;

            return await _repository.UpdateUserMetricsAsync(metricToUpdate);
        }

        // Método para agregar métricas (sin cambios)
        public async Task<UserMetrics> AddUserMetricsAsync(UserMetrics userMetrics)
        {
            return await _repository.AddUserMetricsAsync(userMetrics);
        }

        // Obtener métricas por UserId con validación
        public async Task<List<UserMetrics>> GetUserMetricsByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("El userId no puede ser nulo o vacío.");
            }
            return await _repository.GetUserMetricsByUserIdAsync(userId);
        }
    }
}
