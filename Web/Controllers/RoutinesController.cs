using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

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

        [HttpPost("create")]
        public async Task<IActionResult> AddRoutine([FromBody] Routine routine)
        {
            try
            {
                if (string.IsNullOrEmpty(routine.UserId))
                {
                    return BadRequest("El UserId no puede ser nulo o vacío.");
                }

                Console.WriteLine($"Creando rutina para el usuario: {routine.UserId}");
                var result = await _routinesService.AddRoutineAsync(routine);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddRoutine: {ex.Message}");
                return StatusCode(500, "Error interno al agregar la rutina.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRoutinesByUserId(string userId)
        {
            try
            {
                var routines = await _routinesService.GetRoutinesByUserIdAsync(userId);
                return Ok(routines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetRoutinesByUserId: {ex.Message}");
                return StatusCode(500, "Error interno al obtener las rutinas.");
            }
        }
    }
}
