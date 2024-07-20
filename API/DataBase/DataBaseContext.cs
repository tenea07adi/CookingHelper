using API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace API.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<IngredientDBM> Ingredients { get; set; }
        public DbSet<RecipeDBM> Recipes { get; set; }
        public DbSet<RecipeIngredientDBM> RecipeIngredients { get; set; }
        public DbSet<UserDBM> Users { get; set; }

    }
}
