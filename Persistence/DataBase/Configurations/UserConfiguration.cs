using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(r => r.Email)
                .IsUnique();
        }
    }
}
