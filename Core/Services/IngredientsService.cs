using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class IngredientsService : GenericEntityService<Ingredient>, IIngredientsService
    {
        protected readonly IGenericRepo<RecipeIngredient> _recipeIngredientRepo;
        public IngredientsService(IGenericRepo<Ingredient> ingredientRepo, IGenericRepo<RecipeIngredient> recipeIngredientRepo) : base(ingredientRepo)
        {
            _recipeIngredientRepo = recipeIngredientRepo;
            onDeleteAction = RemoveLinkedEntitiesOnDelete;

            _defaultOrderField = "Name";
        }

        private void RemoveLinkedEntitiesOnDelete(Ingredient entity)
        {
            _recipeIngredientRepo.Delete(c => c.IngredientId == entity.Id);
        }
    }
}
