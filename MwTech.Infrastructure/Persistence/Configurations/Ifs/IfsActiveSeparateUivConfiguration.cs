
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsActiveSeparateUivConfiguration : IEntityTypeConfiguration<IfsActiveSeparateUiv>
{
    public void Configure(EntityTypeBuilder<IfsActiveSeparateUiv> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsActiveSeparateUivs");

    }
}
