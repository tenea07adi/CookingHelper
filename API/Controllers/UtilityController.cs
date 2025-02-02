using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IInfrastructureUtilityService _infrastructureUtilityService;

        public UtilityController(IConfiguration configuration, IInfrastructureUtilityService infrastructureUtilityService)
        {
            _configuration= configuration;
            _infrastructureUtilityService = infrastructureUtilityService;
        }

        [HttpGet("Init")]
        public IActionResult Init()
        {
            string defaultUserEmail = _configuration["AppConfigurations:DefaultUserEmail"]!;
            string defaultUserName = _configuration["AppConfigurations:DefaultUserDisplayName"]!;

            string response = _infrastructureUtilityService.Init(defaultUserEmail!, defaultUserName!);

            return Ok(response);
        }

        [HttpGet("UpdateDatabase")]
        public IActionResult RunDatabaseUpdate()
        {
            _infrastructureUtilityService.RunDatabaseUpdate();

            return NoContent();
        }
    }
}
