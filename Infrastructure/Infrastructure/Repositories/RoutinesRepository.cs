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
                    .Include(rt => rt.Exercises!) // Forzar que Exercises no sea nulo con '!'
                    .ThenInclude(e => e.ExerciseDetails!) // Forzar que ExerciseDetails no sea nulo
                    .FirstOrDefaultAsync(rt => rt.RoutineTemplateId == routineTemplateId);

                if (routineTemplate == null)
                    throw new Exception("Routine Template no encontrado");

                // Manejo seguro de nulabilidad
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

        // Obtener rutinas del usuario por UserId
        public async Task<List<UserRoutine>> GetUserRoutinesAsync(string userId)
        {
            try
            {
                return await _context.UserRoutines
                    .Include(ur => ur.UserExercises!) // Forzar que UserExercises no sea nulo
                        .ThenInclude(ue => ue.UserExerciseDetails!) // Forzar que UserExerciseDetails no sea nulo
                    .Where(ur => ur.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserRoutinesAsync: {ex.Message}");
                throw new Exception("Error al obtener rutinas del usuario.");
            }
        }

        // Editar rutina de usuario
        public async Task<UserRoutine> UpdateUserRoutineAsync(UserRoutine updatedRoutine)
        {
            try
            {
                var existingRoutine = await _context.UserRoutines
                    .Include(ur => ur.UserExercises!) // Forzar que UserExercises no sea nulo
                        .ThenInclude(ue => ue.UserExerciseDetails!) // Forzar que UserExerciseDetails no sea nulo
                    .FirstOrDefaultAsync(ur => ur.UserRoutineId == updatedRoutine.UserRoutineId);

                if (existingRoutine == null)
                    throw new Exception("Rutina de usuario no encontrada.");

                existingRoutine.UserExercises = updatedRoutine.UserExercises;
                _context.UserRoutines.Update(existingRoutine);
                await _context.SaveChangesAsync();
                return existingRoutine;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateUserRoutineAsync: {ex.Message}");
                throw new Exception("Error al actualizar la rutina de usuario.");
            }
        }

        // Eliminar rutina de usuario
        public async Task<bool> DeleteUserRoutineAsync(int userRoutineId)
        {
            try
            {
                var userRoutine = await _context.UserRoutines
                    .FirstOrDefaultAsync(ur => ur.UserRoutineId == userRoutineId);

                if (userRoutine == null)
                    return false;

                _context.UserRoutines.Remove(userRoutine);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteUserRoutineAsync: {ex.Message}");
                throw new Exception("Error al eliminar la rutina de usuario.");
            }
        }
    }
}
