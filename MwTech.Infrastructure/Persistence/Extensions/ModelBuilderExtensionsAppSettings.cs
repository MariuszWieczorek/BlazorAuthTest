using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensionsAppSettings
{
    public static void SeedAppSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppSetting>().HasData(
            new AppSetting
            {
                Id = 1,
                Description = "E-mail",
                Order = 2
            },
            new AppSetting
            {
                Id = 2,
                Description = "Ogólne",
                Order = 1
            });
    }
}