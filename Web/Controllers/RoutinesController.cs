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
        public async Task<IActionResult> CreateUserRoutine([FromQuery] string userId, [FromQuery] int routineTemplateId)
        {
            var result = await _routinesService.CreateUserRoutineAsync(userId, routineTemplateId);
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
}
