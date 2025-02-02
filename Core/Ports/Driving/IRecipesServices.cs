using Core.Entities.Persisted;
using Core.Entities.Transfer;

namespace Core.Ports.Driving
{
    public interface IRecipesServices : IGenericEntityService<Recipe>
    {
        public RecipeIngredient AddIngredient(RecipeIngredient recipeIngredient);
        public void RemoveIngredient(int recipeId, int ingredientId);
        public List<RecipeIngredient> GetIngredients(int recipeId);
        public List<UsedIngredient> GetIngredientsForManyRecipes(List<int> recipesIds);
        public RecipeIngredient GetIngredient(int recipeId, int ingredientId);
        public List<Ingredient> GetAvailableIngredients(int recipeId);
        public List<PreparationStep> GetPreparationSteps(int recipeId);
    }
}
