using API.Models.DBModels;

namespace API.Models.DTOs
{
    public class RecipeIngredientQuantityDTO
    {
        public MeasureUnit MeasureUnit { get; set; }
        public decimal Quantity { get; set; }
    }
}
