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

        // Obtener todas las rutinas predefinidas
        public async Task<List<Routine>> GetRoutineTemplatesAsync()
        {
            return await _repository.GetRoutineTemplatesAsync();
        }

        // Obtener ejercicios de una rutina predefinida
        public async Task<List<Exercise>> GetExercisesByRoutineTemplateIdAsync(int routineTemplateId)
        {
            return await _repository.GetExercisesByRoutineTemplateIdAsync(routineTemplateId);
        }

        // Crear rutina del usuario
        public async Task<UserRoutine> CreateUserRoutineAsync(string userId, int routineTemplateId)
        {
            return await _repository.CreateUserRoutineAsync(userId, routineTemplateId);
        }
    }
}
