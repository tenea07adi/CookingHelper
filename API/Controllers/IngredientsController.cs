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
        private readonly IGenericRepo<RecipeIngredientDBM> _recipeIngredientRepo;

        public IngredientsController(IGenericRepo<IngredientDBM> ingredientRepo, IGenericRepo<RecipeIngredientDBM> recipeIngredientRepo) : base(ingredientRepo, "Name")
        {
            _recipeIngredientRepo = recipeIngredientRepo;
            onDeleteAction = RemoveLinkedEntitiesOnDelete;
        }

        [HttpGet("MeasureUnits")]
        public IActionResult GetMeasureUnits()
        {
            return Ok(EnumHelper.GetEnumValues<MeasureUnit>());
        }

        private void RemoveLinkedEntitiesOnDelete(IngredientDBM entity)
        {
            _recipeIngredientRepo.Delete(c => c.IngredientId == entity.Id);
        }
    }
}
