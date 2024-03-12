
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class ComparedStructureIfsVsMwTechConfiguration : IEntityTypeConfiguration<ComparedStructureIfsVsMwTech>
{
    public void Configure(EntityTypeBuilder<ComparedStructureIfsVsMwTech> builder)
    {
        builder
            .HasNoKey().ToView("ComparedStructuresIfsVsMwTech");

        

    }
}
