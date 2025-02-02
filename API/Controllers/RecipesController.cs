using API.Controllers.ActionFilters;
using API.Controllers.Generics;
using API.DTOs;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public class RecipesController : GenericController<Recipe>
    {
        private readonly IRecipesServices _recipesServices;
        private readonly IIngredientsService _ingredientsService;

        public RecipesController(IRecipesServices recipesServices, IIngredientsService ingredientsService) : base(recipesServices)
        {
            _recipesServices = recipesServices;
            _ingredientsService = ingredientsService;
        }

        [HttpPost("{recipeId}/ingredient/{ingredientId}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult AddIngredient(int recipeId, int ingredientId, RecipeIngredientQuantityDTO quantity)
        {
            var newRecipeIngredient = new RecipeIngredient()
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                MeasureUnit = quantity.MeasureUnit,
                Quantity = quantity.Quantity,
            };

            var result = _recipesServices.AddIngredient(newRecipeIngredient);

            return Ok(RecipeIngredientToDTO(result));
        }

        [HttpDelete("{recipeId}/ingredient/{ingredientId}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult RemoveIngredient(int recipeId, int ingredientId)
        {
            _recipesServices.RemoveIngredient(recipeId, ingredientId);

            return Ok();
        }

        [HttpGet("{recipeId}/ingredients")]
        public IActionResult GetIngredients(int recipeId)
        {
            var ingredinets = _recipesServices.GetIngredients(recipeId);

            var resultList = new List<RecipeIngredientDTO>();

            foreach(var recipeIngredient in ingredinets)
            {
                resultList.Add(RecipeIngredientToDTO(recipeIngredient));
            }

            return Ok(resultList);
        }

        [HttpGet("ingredients")]
        public IActionResult GetIngredientsForManyRecipes([FromQuery(Name = "recipesIds")]List<int> recipesIds)
        {
            var resultList = _recipesServices.GetIngredientsForManyRecipes(recipesIds);

            return Ok(resultList);
        }

        [HttpGet("{recipeId}/ingredient/{ingredientId}")]
        public IActionResult GetIngredient(int recipeId, int ingredientId)
        {
            var recipeIngredient = _recipesServices.GetIngredient(recipeId, ingredientId);

            return Ok(RecipeIngredientToDTO(recipeIngredient));
        }

        [HttpGet("{recipeId}/AvailableIngredients")]
        public IActionResult GetAvailableIngredients(int recipeId)
        {
            var availableIngredients = _recipesServices.GetAvailableIngredients(recipeId);

            return Ok(availableIngredients);
        }

        [HttpGet("{recipeId}/PreparationSteps")]
        public IActionResult GetPreparationSteps(int recipeId)
        {
            var resultList = _recipesServices.GetPreparationSteps(recipeId);

            return Ok(resultList);
        }

        private RecipeIngredientDTO RecipeIngredientToDTO(RecipeIngredient recipeIngredientDBM)
        {
            var ingredient = _ingredientsService.Get(recipeIngredientDBM.IngredientId);

            if(ingredient == null)
            {
                _ingredientsService.Delete(recipeIngredientDBM.Id);
                return null;
            }

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
