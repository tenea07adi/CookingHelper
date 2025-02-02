using Core.Entities.Persisted;

namespace API.DTOs
{
    public class RecipeIngredientQuantityDTO
    {
        public MeasureUnit MeasureUnit { get; set; }
        public decimal Quantity { get; set; }
    }
}
