using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInvite> UserInvites { get; set; }
        public DbSet<PreparationStep> PreparationSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applyes all configurations assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
