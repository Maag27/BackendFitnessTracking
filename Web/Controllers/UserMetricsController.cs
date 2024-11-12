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
        public async Task<IActionResult> AddOrUpdateUserMetrics([FromBody] UserMetrics userMetrics)
        {
            try
            {
                Console.WriteLine($"Intentando agregar o actualizar métricas del usuario: {userMetrics.UserId}");
                var result = await _userMetricsService.AddOrUpdateUserMetricsAsync(userMetrics);
                Console.WriteLine("Métricas del usuario procesadas con éxito.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddOrUpdateUserMetrics: {ex.Message} - StackTrace: {ex.StackTrace}");
                return StatusCode(500, "Error interno del servidor al procesar métricas de usuario.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserMetricsByUserId(string userId)
        {
            try
            {
                Console.WriteLine($"Intentando obtener métricas para userId: {userId}");
                var result = await _userMetricsService.GetUserMetricsByUserIdAsync(userId);

                if (result == null)
                {
                    Console.WriteLine("No se encontraron métricas para el usuario especificado.");
                    return NotFound("No se encontraron métricas para el usuario.");
                }

                Console.WriteLine("Métricas del usuario obtenidas con éxito.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserMetricsByUserId: {ex.Message} - StackTrace: {ex.StackTrace}");
                return StatusCode(500, "Error interno del servidor al obtener métricas de usuario.");
            }
        }
    }
}
