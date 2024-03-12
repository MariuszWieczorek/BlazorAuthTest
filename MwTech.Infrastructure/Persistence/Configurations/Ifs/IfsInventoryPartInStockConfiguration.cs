
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsInventoryPartInStockConfiguration : IEntityTypeConfiguration<IfsInventoryPartInStock>
{
    public void Configure(EntityTypeBuilder<IfsInventoryPartInStock> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsInventoryPartsInStock");

    }
}
