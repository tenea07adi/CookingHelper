﻿using API.Controllers.ActionFilters;
using API.Controllers.Generics;
using API.Models.DBModels;
using API.Models.DTOs;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilterAttribute]
    public class RecipesController : GenericController<RecipeDBM>
    {
        private readonly IGenericRepo<RecipeDBM> _recipesRepo;
        private readonly IGenericRepo<IngredientDBM> _ingredientsRepo;
        private readonly IGenericRepo<RecipeIngredientDBM> _recipeIngredientRepo;
        private readonly IGenericRepo<PreparationStepDBM> _preparationStepsRepo;

        public RecipesController(
            IGenericRepo<RecipeDBM> recipesRepo, 
            IGenericRepo<IngredientDBM> ingredientsRepo, 
            IGenericRepo<RecipeIngredientDBM> recipeIngredientRepo,
            IGenericRepo<PreparationStepDBM> preparationStepRepo) : base(recipesRepo, "Name")
        {
            _recipesRepo = recipesRepo;
            _ingredientsRepo = ingredientsRepo;
            _recipeIngredientRepo = recipeIngredientRepo;
            _preparationStepsRepo = preparationStepRepo;

            onDeleteAction = RemoveLinkedEntitiesOnDelete;
        }

        [HttpPost("{recipeId}/ingredient/{ingredientId}")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
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
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
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

        [HttpGet("ingredients")]
        public IActionResult GetIngredientsForManyRecipes([FromQuery(Name = "recipesIds")]List<int> recipesIds)
        {
            var resultList = new List<RecipeIngredientDTO>();
            foreach (var recipeId in recipesIds)
            {
                if (!_recipesRepo.Exists(recipeId))
                {
                    return BadRequest("Not valid recipe id!");
                }

                foreach (var recipeIngredient in _recipeIngredientRepo.Get(c => c.RecipeId == recipeId))
                {
                    var i = RecipeIngredientToDTO(recipeIngredient);
                    i.RecipeId = -1;

                    var foundIngredient = resultList.FirstOrDefault(c => c.IngredientId == i.IngredientId && c.MeasureUnit == i.MeasureUnit);

                    if (foundIngredient != null)
                    {
                        foundIngredient.Quantity += i.Quantity;
                    }
                    else
                    {
                        resultList.Add(i);
                    }
                }
            }

            resultList = resultList.OrderBy(c => c.Name).ToList();

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

        [HttpGet("{recipeId}/AvailableIngredients")]
        public IActionResult GetAvailableIngredients(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            var resultList = new List<RecipeIngredientDTO>();

            var usedIngredients = _recipeIngredientRepo.Get(c => c.RecipeId == recipeId);

            var availableIngredients = _ingredientsRepo.Get(c => usedIngredients.Where(x => x.IngredientId == c.Id).Count() <= 0, c => c.Name);

            return Ok(availableIngredients);
        }

        [HttpGet("{recipeId}/PreparationSteps")]
        public IActionResult GetPreparationSteps(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                return BadRequest("Not valid recipe id!");
            }

            var resultList = _preparationStepsRepo.Get(c => c.RecipeId == recipeId).OrderBy(c => c.OrderNumber).ToList();

            return Ok(resultList);
        }

        private RecipeIngredientDTO RecipeIngredientToDTO(RecipeIngredientDBM recipeIngredientDBM)
        {
            var ingredient = _ingredientsRepo.Get(recipeIngredientDBM.IngredientId);

            if(ingredient == null)
            {
                _recipeIngredientRepo.Delete(recipeIngredientDBM.Id);
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

        private void RemoveLinkedEntitiesOnDelete(RecipeDBM entity)
        {
            _recipeIngredientRepo.Delete(c => c.RecipeId == entity.Id);
        }
    }
}
