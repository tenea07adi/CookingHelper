using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(RecipeId), nameof(IngredientId), IsUnique = true)]
    public class RecipeIngredientDBM : BaseDBM
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public decimal Quantity { get; set; }
    }
}
