using API.Models.DBModels;

namespace API.Models.DTOs
{
    public class RecipeIngredientQuantityDTO
    {
        public MeasureUnit MeasureUnit { get; set; }
        public int Quantity { get; set; }
    }
}
