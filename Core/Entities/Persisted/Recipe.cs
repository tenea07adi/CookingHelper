using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ExternalLink { get; set; }
        public int EstimatedDurationInMinutes { get; set; }
    }
}
