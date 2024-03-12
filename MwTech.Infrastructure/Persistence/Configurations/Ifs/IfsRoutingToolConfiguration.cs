
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsRoutingToolConfiguration : IEntityTypeConfiguration<IfsRoutingTool>
{
    public void Configure(EntityTypeBuilder<IfsRoutingTool> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsRoutingTools");



    }
}
