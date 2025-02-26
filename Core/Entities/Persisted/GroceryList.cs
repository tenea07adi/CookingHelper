using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class GroceryList : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPinned { get; set; }
    }
}
