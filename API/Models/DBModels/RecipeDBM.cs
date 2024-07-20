using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class RecipeDBM : BaseDBM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int EstimatedDurationInMinutes { get; set; }
    }
}
