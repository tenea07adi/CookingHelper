using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class RecipeConfiguration : BaseEntityConfiguration<Recipe>
    {
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            base.Configure(builder);

            builder.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}