using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutinesController : ControllerBase
    {
        private readonly RoutinesService _routinesService;

        public RoutinesController(RoutinesService routinesService)
        {
            _routinesService = routinesService;
        }

        // Obtener todas las rutinas predefinidas
        [HttpGet("template-routines")]
        public async Task<IActionResult> GetTemplateRoutines()
        {
            try
            {
                var routines = await _routinesService.GetRoutineTemplatesAsync();
                return Ok(routines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetTemplateRoutines: {ex.Message}");
                return StatusCode(500, "Error interno al obtener las rutinas predefinidas.");
            }
        }

        // Obtener ejercicios de una rutina predefinida específica
        [HttpGet("template-routines/{routineTemplateId}/exercises")]
        public async Task<IActionResult> GetExercisesByRoutineTemplateId(int routineTemplateId)
        {
            try
            {
                var exercises = await _routinesService.GetExercisesByRoutineTemplateIdAsync(routineTemplateId);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetExercisesByRoutineTemplateId: {ex.Message}");
                return StatusCode(500, "Error interno al obtener los ejercicios de la rutina.");
            }
        }

        // Crear rutina personalizada para el usuario
        [HttpPost("create-user-routine")]
        public async Task<IActionResult> CreateUserRoutine([FromQuery] string userId, [FromQuery] int routineTemplateId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("El UserId no puede ser nulo o vacío.");
                }

                var result = await _routinesService.CreateUserRoutineAsync(userId, routineTemplateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateUserRoutine: {ex.Message}");
                return StatusCode(500, "Error interno al crear la rutina de usuario.");
            }
        }
    }
}
