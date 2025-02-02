using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class UserInviteConfiguration : BaseEntityConfiguration<UserInvite>
    {
        public override void Configure(EntityTypeBuilder<UserInvite> builder)
        {
            base.Configure(builder);

            builder.HasIndex(r => r.Email)
                .IsUnique();
        }
    }
}
