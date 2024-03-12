
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class ComparedRouteIfsVsMwTechConfiguration : IEntityTypeConfiguration<ComparedRouteIfsVsMwTech>
{
    public void Configure(EntityTypeBuilder<ComparedRouteIfsVsMwTech> builder)
    {
        builder
            .HasNoKey().ToView("ComparedRoutesIfsVsMwTech");

        

    }
}
