using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;
public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.MachineNumber)
            .IsRequired();


        // relacja z kategorią maszyny
        builder.HasOne<MachineCategory>(p => p.MachineCategory)
        .WithMany(u => u.Machines)
        .HasForeignKey(p => p.MachineCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
