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
            var result = await _userMetricsService.AddUserMetricsAsync(userMetrics);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserMetricsByUserId(string userId)
        {
            var result = await _userMetricsService.GetUserMetricsByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
