using Core.Entities.Persisted;
using Core.Entities.Transfer;
using Core.Ports.Driving;

namespace Core.Services
{
    public class RecipesServices : GenericEntityService<Recipe>, IRecipesServices
    {
        private readonly IGenericRepo<Recipe> _recipesRepo;
        private readonly IGenericRepo<Ingredient> _ingredientsRepo;
        private readonly IGenericRepo<RecipeIngredient> _recipeIngredientRepo;
        private readonly IGenericRepo<PreparationStep> _preparationStepsRepo;

        public RecipesServices(
            IGenericRepo<Recipe> recipesRepo,
            IGenericRepo<Ingredient> ingredientsRepo,
            IGenericRepo<RecipeIngredient> recipeIngredientRepo,
            IGenericRepo<PreparationStep> preparationStepRepo) : base(recipesRepo)
        {
            _recipesRepo = recipesRepo;
            _ingredientsRepo = ingredientsRepo;
            _recipeIngredientRepo = recipeIngredientRepo;
            _preparationStepsRepo = preparationStepRepo;

            onDeleteAction = RemoveLinkedEntitiesOnDelete;
        }

        public RecipeIngredient AddIngredient(RecipeIngredient recipeIngredient)
        {
            if (!_recipesRepo.Exists(recipeIngredient.RecipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            if (!_ingredientsRepo.Exists(recipeIngredient.IngredientId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Ingredient).Name));
            }

            _recipeIngredientRepo.Add(recipeIngredient);

            return recipeIngredient;
        }

        public void RemoveIngredient(int recipeId, int ingredientId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            if (!_ingredientsRepo.Exists(ingredientId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Ingredient).Name));
            }

            _recipeIngredientRepo.Delete(c => c.RecipeId == recipeId && c.IngredientId == ingredientId);
        }

        public List<RecipeIngredient> GetIngredients(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            var resultList = _recipeIngredientRepo.Get(c => c.RecipeId == recipeId);

            return resultList;
        }

        public List<UsedIngredient> GetIngredientsForManyRecipes(List<int> recipesIds)
        {
            var resultList = new List<UsedIngredient>();

            foreach (var recipeId in recipesIds)
            {
                if (!_recipesRepo.Exists(recipeId))
                {
                    throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
                }

                foreach (var recipeIngredient in _recipeIngredientRepo.Get(c => c.RecipeId == recipeId))
                {
                    var i = RecipeIngredientToDTO(recipeIngredient);

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

            return resultList;
        }

        public RecipeIngredient GetIngredient(int recipeId, int ingredientId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            if (!_ingredientsRepo.Exists(ingredientId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Ingredient).Name));
            }

            var recipeIngredient = _recipeIngredientRepo.Get(c => c.RecipeId == recipeId && c.IngredientId == ingredientId).FirstOrDefault();

            return recipeIngredient;
        }

        public List<Ingredient> GetAvailableIngredients(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            var usedIngredients = _recipeIngredientRepo.Get(c => c.RecipeId == recipeId);

            var availableIngredients = _ingredientsRepo.Get(c => usedIngredients.Where(x => x.IngredientId == c.Id).Count() <= 0, c => c.Name);

            return availableIngredients;
        }

        public List<PreparationStep> GetPreparationSteps(int recipeId)
        {
            if (!_recipesRepo.Exists(recipeId))
            {
                throw new Exception(string.Format(Constants.Exceptions.SpecificEntityNotFound, typeof(Recipe).Name));
            }

            var resultList = _preparationStepsRepo.Get(c => c.RecipeId == recipeId).OrderBy(c => c.OrderNumber).ToList();

            return resultList;
        }

        private UsedIngredient RecipeIngredientToDTO(RecipeIngredient recipeIngredientDBM)
        {
            var ingredient = _ingredientsRepo.Get(recipeIngredientDBM.IngredientId);

            if (ingredient == null)
            {
                _recipeIngredientRepo.Delete(recipeIngredientDBM.Id);
                return null;
            }

            return new UsedIngredient()
            {
                IngredientId = recipeIngredientDBM.IngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description,
                MeasureUnit = recipeIngredientDBM.MeasureUnit,
                MeasureUnitName = Enum.GetName(typeof(MeasureUnit), recipeIngredientDBM.MeasureUnit),
                Quantity = recipeIngredientDBM.Quantity
            };
        }

        private void RemoveLinkedEntitiesOnDelete(Recipe entity)
        {
            _recipeIngredientRepo.Delete(c => c.RecipeId == entity.Id);
        }
    }
}
