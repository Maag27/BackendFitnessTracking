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

        // Obtener rutinas personalizadas de un usuario
        [HttpGet("{userId}/user-routines")]
        public async Task<IActionResult> GetUserRoutinesByUserId(string userId)
        {
            try
            {
                var routines = await _routinesService.GetUserRoutinesByUserIdAsync(userId);
                return Ok(routines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserRoutinesByUserId: {ex.Message}");
                return StatusCode(500, "Error interno al obtener las rutinas del usuario.");
            }
        }
    }
}
