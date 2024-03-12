
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsWorkCenterMaterialConfiguration : IEntityTypeConfiguration<IfsWorkCenterMaterial>
{
    public void Configure(EntityTypeBuilder<IfsWorkCenterMaterial> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsWorkCenterMaterials");

        /*
        builder
                .Property(x => x.WorkCenterNo)
                .HasColumnName("WORK_CENTER_NO");

        builder
                .Property(x => x.OrderNo)
                .HasColumnName("ORDER_NO");

        
        builder
               .Property(x => x.SourceLocation)
               .HasColumnName("CF$_C_SOURCE_LOKC");

        */

    }
}
