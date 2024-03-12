
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsScadaMaterialConfiguration : IEntityTypeConfiguration<IfsScadaMaterial>
{
    public void Configure(EntityTypeBuilder<IfsScadaMaterial> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsScadaMaterials");

    }
}
