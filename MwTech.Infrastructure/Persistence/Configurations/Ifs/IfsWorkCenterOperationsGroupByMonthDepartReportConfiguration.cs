
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsWorkCenterOperationsGroupByMonthDepartReportConfiguration : IEntityTypeConfiguration<IfsWorkCenterOperationsByMonthDepart>
{
    public void Configure(EntityTypeBuilder<IfsWorkCenterOperationsByMonthDepart> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsWorkCenterOperationsByMonthDepartReports");

    }
}
