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

        [HttpGet("template-routines")]
        public async Task<IActionResult> GetTemplateRoutines()
        {
            var routines = await _routinesService.GetRoutineTemplatesAsync();
            return Ok(routines);
        }

        [HttpGet("template-routines/{routineTemplateId}/exercises")]
        public async Task<IActionResult> GetExercisesByRoutineTemplateId(int routineTemplateId)
        {
            var exercises = await _routinesService.GetExercisesByRoutineTemplateIdAsync(routineTemplateId);
            return Ok(exercises);
        }

        [HttpPost("create-user-routine")]
        public async Task<IActionResult> CreateUserRoutine([FromBody] CreateUserRoutineRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || request.RoutineTemplateId <= 0 || request.ExerciseTemplateId <= 0)
            {
                return BadRequest("Datos inválidos. Se requiere un userId válido, un routineTemplateId y un exerciseTemplateId mayores a 0.");
            }

            var result = await _routinesService.CreateUserRoutineAsync(request.UserId, request.RoutineTemplateId, request.ExerciseTemplateId);
            return Ok(result);
        }


        [HttpGet("user-routines")]
        public async Task<IActionResult> GetUserRoutines([FromQuery] string userId)
        {
            var routines = await _routinesService.GetUserRoutinesAsync(userId);
            return Ok(routines);
        }

        [HttpPut("user-routines")]
        public async Task<IActionResult> UpdateUserRoutine([FromBody] UserRoutine updatedRoutine)
        {
            var result = await _routinesService.UpdateUserRoutineAsync(updatedRoutine);
            return Ok(result);
        }

        [HttpDelete("user-routines/{userRoutineId}")]
        public async Task<IActionResult> DeleteUserRoutine(int userRoutineId)
        {
            var success = await _routinesService.DeleteUserRoutineAsync(userRoutineId);
            return success ? NoContent() : NotFound();
        }
    }

    // DTO para la solicitud de creación de rutina
    public class CreateUserRoutineRequest
    {
        public string? UserId { get; set; }
        public int RoutineTemplateId { get; set; }
        public int ExerciseTemplateId { get; set; } // Nuevo campo agregado
    }
}
