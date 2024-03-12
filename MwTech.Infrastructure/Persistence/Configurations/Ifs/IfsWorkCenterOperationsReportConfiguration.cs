
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsWorkCenterOperationsReportConfiguration : IEntityTypeConfiguration<IfsWorkCenterOperationsReport>
{
    public void Configure(EntityTypeBuilder<IfsWorkCenterOperationsReport> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsWorkCentersOperationsReports");

    }
}
