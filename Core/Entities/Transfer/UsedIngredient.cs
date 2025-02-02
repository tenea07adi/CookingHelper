
using Core.Entities.Persisted;

namespace Core.Entities.Transfer
{
    public class UsedIngredient
    {
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public string MeasureUnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }
}
