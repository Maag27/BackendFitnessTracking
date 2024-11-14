using Domain.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class RoutinesService
    {
        private readonly RoutinesRepository _repository;

        public RoutinesService(RoutinesRepository repository)
        {
            _repository = repository;
        }

        // Crear rutina del usuario
        public async Task<UserRoutine> CreateUserRoutineAsync(string userId, int routineTemplateId)
        {
            return await _repository.CreateUserRoutineAsync(userId, routineTemplateId);
        }

        // Obtener rutinas del usuario
        public async Task<List<UserRoutine>> GetUserRoutinesByUserIdAsync(string userId)
        {
            return await _repository.GetUserRoutinesByUserIdAsync(userId);
        }
    }
}
