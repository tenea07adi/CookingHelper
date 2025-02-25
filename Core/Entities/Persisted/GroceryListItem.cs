using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class GroceryListItem : BaseEntity
    {
        public int GroceryListId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public MeasureUnit MeasureUnit { get; set; }
        public decimal Quantity { get; set; }

        public bool IsCompleted { get; set; }
    }
}
