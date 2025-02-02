using API.Controllers.Generics;
using API.Interfaces;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : GenericController<Ingredient>
    {
        private readonly IEnumValueDtoFactory _enumValueDtoFactory;

        public IngredientsController(IIngredientsService ingredientsService, IEnumValueDtoFactory enumValueDtoFactory) : base(ingredientsService)
        {
            _enumValueDtoFactory = enumValueDtoFactory;
        }

        [HttpGet("MeasureUnits")]
        public IActionResult GetMeasureUnits()
        {
            return Ok(_enumValueDtoFactory.Create<MeasureUnit>());
        }
    }
}
