using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        // Obtener todas las rutinas predefinidas
        public async Task<List<Routine>> GetRoutineTemplatesAsync()
        {
            try
            {
                return await _context.RoutineTemplates.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetRoutineTemplatesAsync: {ex.Message}");
                throw new Exception("Error al obtener las rutinas predefinidas desde la base de datos.");
            }
        }

        // Obtener ejercicios asociados a una rutina predefinida
        public async Task<List<Exercise>> GetExercisesByRoutineTemplateIdAsync(int routineTemplateId)
        {
            try
            {
                return await _context.ExerciseTemplates
                    .Where(e => e.RoutineTemplateId == routineTemplateId)
                    .Include(e => e.ExerciseDetails)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetExercisesByRoutineTemplateIdAsync: {ex.Message}");
                throw new Exception("Error al obtener los ejercicios de la rutina predefinida.");
            }
        }

        // Crear una nueva rutina de usuario basada en un RoutineTemplate
        public async Task<UserRoutine> CreateUserRoutineAsync(string userId, int routineTemplateId)
        {
            try
            {
                var routineTemplate = await _context.RoutineTemplates
                    .Include(rt => rt.Exercises!)
                    .ThenInclude(e => e.ExerciseDetails!)
                    .FirstOrDefaultAsync(rt => rt.RoutineTemplateId == routineTemplateId);

                if (routineTemplate == null)
                    throw new Exception("Routine Template no encontrado");

                var exercises = routineTemplate.Exercises ?? new List<Exercise>();

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateUserRoutineAsync: {ex.Message}");
                throw new Exception("Error al crear la rutina de usuario.");
            }
        }
    }
}
