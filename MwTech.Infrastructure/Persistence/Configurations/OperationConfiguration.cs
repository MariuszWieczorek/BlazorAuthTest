
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {

            builder
                .Property(x => x.OperationNumber)
                .HasMaxLength(50);

            builder
           .HasIndex(u => u.OperationNumber)
           .IsClustered(false)
           .IsUnique();

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.UnitId)
                .HasDefaultValue(1);

            builder.Property(x => x.ProductCategoryId)
                .HasDefaultValue(1);

            // relacja z jednostką miary
            builder.HasOne<Unit>(p => p.Unit)
            .WithMany(u => u.Operations)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.Restrict);


            // relacja z kategorią produktu
            builder.HasOne<ProductCategory>(p => p.ProductCategory)
            .WithMany(u => u.Operations)
            .HasForeignKey(p => p.ProductCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
