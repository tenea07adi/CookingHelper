using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class RecipeIngredient : BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public decimal Quantity { get; set; }
    }
}
