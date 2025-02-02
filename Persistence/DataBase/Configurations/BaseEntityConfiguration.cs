using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(c => c.Id);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
