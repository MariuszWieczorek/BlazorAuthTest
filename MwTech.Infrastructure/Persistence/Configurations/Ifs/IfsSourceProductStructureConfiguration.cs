
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsSourceProductStructureConfiguration : IEntityTypeConfiguration<IfsSourceProductStructure>
{
    public void Configure(EntityTypeBuilder<IfsSourceProductStructure> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsSourceProductStructures");

    }
}
