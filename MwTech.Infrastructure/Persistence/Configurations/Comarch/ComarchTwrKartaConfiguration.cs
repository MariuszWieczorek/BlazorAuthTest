
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Comarch;

namespace MwTech.Infrastructure.Persistence.Configurations.Comarch;

public class ComarchTwrKartaConfiguration : IEntityTypeConfiguration<ComarchTwrKarta>
{
    public void Configure(EntityTypeBuilder<ComarchTwrKarta> builder)
    {

        builder
            .HasNoKey()
            .ToView("ComarchTwrKarty");

    }
}
