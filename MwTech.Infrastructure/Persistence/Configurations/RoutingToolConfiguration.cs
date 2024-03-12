
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class RoutingToolConfiguration : IEntityTypeConfiguration<RoutingTool>
{
    public void Configure(EntityTypeBuilder<RoutingTool> builder)
    {
        builder
            .Property(x => x.ToolNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder
                .Property(x => x.Name)
                .HasMaxLength(100);

    }
}
