using Domain.Models;
using Infrastructure.Repositories;

namespace Services
{
    public class RoutinesService
    {
        private readonly RoutinesRepository _repository;

        public RoutinesService(RoutinesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Routine> AddRoutineAsync(Routine routine)
        {
            return await _repository.AddRoutineAsync(routine);
        }

        public async Task<List<Routine>> GetRoutinesByUserIdAsync(string userId)
        {
            return await _repository.GetRoutinesByUserIdAsync(userId);
        }
    }
}
