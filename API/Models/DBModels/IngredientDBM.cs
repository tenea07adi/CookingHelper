using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class IngredientDBM : BaseDBM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public enum MeasureUnit
    {
        Unit = 0,
        Grams = 1,
        Liters = 2,
        TSP = 3,
        TBS = 4,
        Coups = 5
    }
}
