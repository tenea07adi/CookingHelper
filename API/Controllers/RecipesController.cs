using API.Controllers.Generics;
using API.Models.DBModels;
using API.Models.DTOs;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : GenericController<RecipeDBM>
    {
        private readonly IGenericRepo<RecipeDBM> _recipesRepo;
        private readonly IGenericRepo<IngredientDBM> _ingredientsRepo;
        private readonly IGenericRepo<RecipeIngredientDBM> _recipeIngredientRepo;


        public RecipesController(
            IGenericRepo<RecipeDBM> recipesRepo, 
            IGenericRepo<IngredientDBM> ingredientsRepo, 
            IGenericRepo<RecipeIngredientDBM> recipeIngredientRepo) : base(recipesRepo)
        {
            _recipesRepo = recipesRepo;
            _ingredientsRepo = ingredientsRepo;
            _recipeIngredientRepo = recipeIngredientRepo;
        }

        [HttpPost("{recipeId}/ingredient/{ingredientId}")]
        public IActionResult AddIngredient(int recipeId, int ingredientId, RecipeIngredientQuantityDTO quantity)
        {
            if(!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            if (!_ingredientsRepo.Exists(ingredientId))
            {
                return BadRequest("Not valid ingredient id!");
            }

            var newRecipeIngredient = new RecipeIngredientDBM()
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                MeasureUnit = quantity.MeasureUnit,
                Quantity = quantity.Quantity,
            };

            _recipeIngredientRepo.Add(newRecipeIngredient);

            return Ok(RecipeIngredientToDTO(newRecipeIngredient));
        }

        [HttpDelete("{recipeId}/ingredient/{ingredientId}")]
        public IActionResult RemoveIngredient(int recipeId, int ingredientId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            if (!_ingredientsRepo.Exists(ingredientId))
            {
                return BadRequest("Not valid ingredient id!");
            }

            _recipeIngredientRepo.Delete(c => c.RecipeId == recipeId && c.IngredientId == ingredientId);

            return Ok();
        }

        [HttpGet("{recipeId}/ingredients")]
        public IActionResult GetIngredients(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            var resultList = new List<RecipeIngredientDTO>();

            foreach(var recipeIngredient in _recipeIngredientRepo.Get(c => c.RecipeId == recipeId))
            {
                resultList.Add(RecipeIngredientToDTO(recipeIngredient));
            }

            return Ok(resultList);
        }

        [HttpGet("{recipeId}/ingredient/{ingredientId}")]
        public IActionResult GetIngredient(int recipeId, int ingredientId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            if (!_ingredientsRepo.Exists(ingredientId))
            {
                return BadRequest("Not valid ingredient id!");
            }

            var recipeIngredient = _recipeIngredientRepo.Get(c => c.RecipeId == recipeId && c.IngredientId == ingredientId).FirstOrDefault();

            return Ok(RecipeIngredientToDTO(recipeIngredient));
        }

        private RecipeIngredientDTO RecipeIngredientToDTO(RecipeIngredientDBM recipeIngredientDBM)
        {
            var ingredient = _ingredientsRepo.Get(recipeIngredientDBM.IngredientId);

            return new RecipeIngredientDTO()
            {
                RecipeId = recipeIngredientDBM.RecipeId,
                IngredientId = recipeIngredientDBM.IngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description,
                MeasureUnit = recipeIngredientDBM.MeasureUnit,
                MeasureUnitName = Enum.GetName(typeof(MeasureUnit), recipeIngredientDBM.MeasureUnit),
                Quantity = recipeIngredientDBM.Quantity
            };
        }
    }
}
