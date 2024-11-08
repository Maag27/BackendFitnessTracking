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
            return await _context.UserMetrics
                .Where(um => um.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserMetrics> AddUserMetricsAsync(UserMetrics userMetrics)
        {
            _context.UserMetrics.Add(userMetrics);
            await _context.SaveChangesAsync();
            return userMetrics;
        }
    }
}
