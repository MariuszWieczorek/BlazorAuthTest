using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Unit = MwTech.Domain.Entities.Unit;

namespace MwTech.Application.Common.Interfaces;

public interface IScadaIfsDbContext : IDisposable 
{
    public DbSet<ScadaReport> SCADA_REPORT { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
