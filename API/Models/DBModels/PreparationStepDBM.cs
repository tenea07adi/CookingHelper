using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    public class PreparationStepDBM : BaseDBM
    {
        public int RecipeId { get; set; }

        public string Instructions { get; set; }
        public int OrderNumber { get; set; }
    }
}
