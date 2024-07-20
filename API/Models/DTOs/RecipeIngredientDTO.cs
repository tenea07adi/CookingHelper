using API.Models.DBModels;

namespace API.Models.DTOs
{
    public class RecipeIngredientDTO
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public int Quantity { get; set; }
    }
}
