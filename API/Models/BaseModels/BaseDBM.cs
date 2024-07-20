namespace API.Models.BaseModels
{
    public abstract class BaseDBM
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
