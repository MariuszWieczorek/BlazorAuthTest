using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Comarch;
using MwTech.Infrastructure.Persistence.Configurations;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MwTech.Infrastructure.Persistence;

public class ComarchDbContext : IdentityDbContext<ApplicationUser>, IComarchDbContext
{
    public ComarchDbContext(DbContextOptions<ComarchDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfiguration(new ComarchBomConfiguration());
        // modelBuilder.ApplyConfiguration(new ComarchTwrKartaConfiguration());
         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ComarchBom> ComarchBoms { get; set; }
    public DbSet<ComarchTwrKarta> ComarchTwrKarty { get; set; }
    public DbSet<ComarchTwrCost> ComarchTwrCost { get; set; }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

}
