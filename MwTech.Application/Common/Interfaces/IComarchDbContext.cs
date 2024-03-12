using Microsoft.EntityFrameworkCore;
using MwTech.Domain.Entities.Comarch;

namespace MwTech.Application.Common.Interfaces;
public interface IComarchDbContext
{
    public DbSet<ComarchBom> ComarchBoms { get; set; }
    public DbSet<ComarchTwrKarta> ComarchTwrKarty { get; set; }
    public DbSet<ComarchTwrCost> ComarchTwrCost { get; set; }

    int SaveChanges();
}
