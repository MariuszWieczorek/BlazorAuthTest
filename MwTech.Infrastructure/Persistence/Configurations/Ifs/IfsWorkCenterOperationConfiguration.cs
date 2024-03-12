
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsWorkCenterOperationConfiguration : IEntityTypeConfiguration<IfsWorkCenterOperation>
{
    public void Configure(EntityTypeBuilder<IfsWorkCenterOperation> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsWorkCenterOperations");

    }
}
