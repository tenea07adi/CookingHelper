using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class RecipeIngredientConfiguration : BaseEntityConfiguration<RecipeIngredient>
    {
        public override void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            base.Configure(builder);

            builder.HasIndex(r => new { r.RecipeId, r.IngredientId })
                .IsUnique();
        }
    }
}
