using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserMetricsRepository
    {
        private readonly AppDbContext _context;

        public UserMetricsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserMetrics>> GetUserMetricsByUserIdAsync(string userId)
        {
            Console.WriteLine($"Consultando UserMetrics para userId: {userId}");
            var result = await _context.UserMetrics
                .Where(um => um.UserId == userId)
                .ToListAsync();

            if (!result.Any())
            {
                Console.WriteLine("No se encontraron métricas en la base de datos para el userId especificado.");
            }

            return result;
        }

        public async Task<UserMetrics> AddUserMetricsAsync(UserMetrics userMetrics)
        {
            try
            {
                Console.WriteLine($"Agregando UserMetrics para userId: {userMetrics.UserId}");
                _context.UserMetrics.Add(userMetrics);
                await _context.SaveChangesAsync();
                Console.WriteLine("Métricas del usuario guardadas exitosamente en la base de datos.");
                return userMetrics;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar métricas: {ex.Message}");
                throw;
            }
        }
    }
}
