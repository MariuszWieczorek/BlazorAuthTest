using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
//using MwTech.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Unit = MwTech.Domain.Entities.Unit;
using MwTech.Infrastructure.Persistence.Extensions;

namespace MwTech.Infrastructure.Persistence;
public class ScadaIfsDbContext : IdentityDbContext<ApplicationUser>, IScadaIfsDbContext
{

    public ScadaIfsDbContext(DbContextOptions<ScadaIfsDbContext> options) : base(options)
    {

    }
    public DbSet<ScadaReport> SCADA_REPORT { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
