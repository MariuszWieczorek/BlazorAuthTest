
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsScadaOperationConfiguration : IEntityTypeConfiguration<IfsScadaOperation>
{
    public void Configure(EntityTypeBuilder<IfsScadaOperation> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsScadaOperations");

    }
}
