using Microsoft.AspNetCore.Mvc;
namespace ForumAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHealth")]
    public string Get()
    {
       return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
