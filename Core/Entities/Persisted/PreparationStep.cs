using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class PreparationStep : BaseEntity
    {
        public int RecipeId { get; set; }

        public string Instructions { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
    }
}
