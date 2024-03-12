using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensionsUsers
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {   
                Id = "2d1c0b97-5e74-49a1-9252-58788873dde5",
                FirstName = "Mariusz",
                LastName = "Wieczorek",
                RegisterDateTime = DateTime.UtcNow,
                IsDeleted = false,
                UserName = "m.wieczorek1972@gmail.com",
                NormalizedUserName = "M.WIECZOREK1972@GMAIL.COM",
                Email = "m.wieczorek1972@gmail.com",
                NormalizedEmail = "M.WIECZOREK1972@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEHA2HWq0uJVCUIzYmHM+yamfiVIrNSnuinbmP43J+KJQJPFhjNVR1O46ElzpL2++wQ==",
                SecurityStamp = "ZZNB2SYLM3POSL4P4JSV2MLP7N6KVTV4",
                ConcurrencyStamp = "3958de97-2995-4fa4-af37-eab993b16fb2",
                PhoneNumber = "11111111",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0

            }
          );
    }
}
