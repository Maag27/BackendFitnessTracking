using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMetricsController : ControllerBase
    {
        private readonly UserMetricsService _userMetricsService;

        public UserMetricsController(UserMetricsService userMetricsService)
        {
            _userMetricsService = userMetricsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserMetrics([FromBody] UserMetrics userMetrics)
        {
             try
            {
                var result = await _userMetricsService.AddUserMetricsAsync(userMetrics);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Registra el error para ver detalles en la consola del servidor
                Console.WriteLine("Error en AddUserMetrics: " + ex.Message);
                return StatusCode(500, "Error interno del servidor al agregar métricas de usuario.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserMetricsByUserId(string userId)
        {
            try
            {
                var result = await _userMetricsService.GetUserMetricsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddUserMetrics: {ex.Message} - Detalles: {ex.InnerException?.Message}");
                return StatusCode(500, "Error interno del servidor al agregar métricas de usuario.");
            }

        }
    }
}
