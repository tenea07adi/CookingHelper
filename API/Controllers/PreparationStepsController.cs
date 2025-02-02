using API.Controllers.ActionFilters;
using API.Controllers.Generics;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public class PreparationStepsController : GenericController<PreparationStep>
    {
        private readonly IPreparationStepsService _preparationStepsService;
        public PreparationStepsController(IPreparationStepsService preparationStepsService) : base(preparationStepsService)
        {
            _preparationStepsService = preparationStepsService;
        }

        [HttpGet("{id}/MoveUp")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult MoveUp(int id)
        {
            _preparationStepsService.MoveUp(id);

            return Ok();
        }

        [HttpGet("{id}/MoveDown")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult MoveDown(int id)
        {
            _preparationStepsService.MoveDown(id);

            return Ok();
        }
    }
}
