using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataBase.Configurations
{
    public class PreparationStepConfiguration : BaseEntityConfiguration<PreparationStep>
    {
        public override void Configure(EntityTypeBuilder<PreparationStep> builder)
        {
            base.Configure(builder);
        }
    }
}
