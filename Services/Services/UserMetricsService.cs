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

        public async Task<UserMetrics> AddUserMetricsAsync(UserMetrics userMetrics)
        {
            return await _repository.AddUserMetricsAsync(userMetrics);
        }

        public async Task<List<UserMetrics>> GetUserMetricsByUserIdAsync(string userId)
        {
            return await _repository.GetUserMetricsByUserIdAsync(userId);
        }
    }
}
