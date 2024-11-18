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

        public async Task<List<Routine>> GetRoutineTemplatesAsync()
        {
            return await _repository.GetRoutineTemplatesAsync();
        }

        public async Task<List<Exercise>> GetExercisesByRoutineTemplateIdAsync(int routineTemplateId)
        {
            return await _repository.GetExercisesByRoutineTemplateIdAsync(routineTemplateId);
        }

        public async Task<UserRoutine> CreateUserRoutineAsync(string userId, int routineTemplateId)
        {
            return await _repository.CreateUserRoutineAsync(userId, routineTemplateId);
        }

        public async Task<List<UserRoutine>> GetUserRoutinesAsync(string userId)
        {
            return await _repository.GetUserRoutinesAsync(userId);
        }

        public async Task<UserRoutine> UpdateUserRoutineAsync(UserRoutine updatedRoutine)
        {
            return await _repository.UpdateUserRoutineAsync(updatedRoutine);
        }

        public async Task<bool> DeleteUserRoutineAsync(int userRoutineId)
        {
            return await _repository.DeleteUserRoutineAsync(userRoutineId);
        }
    }
}
