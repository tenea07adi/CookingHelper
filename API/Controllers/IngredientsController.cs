using API.Controllers.Generics;
using API.Helpers;
using API.Models.DBModels;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : GenericController<IngredientDBM>
    {
        public IngredientsController(IGenericRepo<IngredientDBM> ingredientRepo) : base(ingredientRepo)
        {
        }

        [HttpGet("MeasureUnits")]
        public IActionResult GetMeasureUnits()
        {
            return Ok(EnumHelper.GetEnumValues<MeasureUnit>());
        }
    }
}
