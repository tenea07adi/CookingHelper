using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
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
