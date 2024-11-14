using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoutinesRepository
    {
        private readonly AppDbContext _context;

        public RoutinesRepository(AppDbContext context)
        {
            _context = context;
        }

        // Crear una nueva rutina de usuario basada en un RoutineTemplate
        public async Task<UserRoutine> CreateUserRoutineAsync(string userId, int routineTemplateId)
        {
            // Cargar el RoutineTemplate con sus ejercicios y detalles
            var routineTemplate = await _context.RoutineTemplates
                .Include(rt => rt.Exercises!)
                .ThenInclude(e => e.ExerciseDetails!)
                .FirstOrDefaultAsync(rt => rt.RoutineTemplateId == routineTemplateId);

            if (routineTemplate == null)
                throw new Exception("Routine Template no encontrado");

            // Validar si la lista de ejercicios es nula antes de usarla
            var exercises = routineTemplate.Exercises ?? new List<Exercise>();

            // Crear UserRoutine con ejercicios personalizados
            var userRoutine = new UserRoutine
            {
                UserId = userId,
                RoutineTemplateId = routineTemplateId,
                UserExercises = exercises.Select(e => new UserExercise
                {
                    ExerciseTemplateId = e.ExerciseTemplateId,
                    UserExerciseDetails = (e.ExerciseDetails ?? new List<ExerciseDetail>()).Select(ed => new UserExerciseDetail
                    {
                        Series = ed.Series,
                        Repetitions = ed.Repetitions,
                        RestTime = ed.RestTime
                    }).ToList()
                }).ToList()
            };

            _context.UserRoutines.Add(userRoutine);
            await _context.SaveChangesAsync();
            return userRoutine;
        }

        // Obtener rutinas personalizadas de un usuario
        public async Task<List<UserRoutine>> GetUserRoutinesByUserIdAsync(string userId)
        {
            return await _context.UserRoutines!
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.UserExercises!)
                .ThenInclude(ue => ue.UserExerciseDetails!)
                .ToListAsync();
        }
    }
}
