using MwTech.Application.Dictionaries;
using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MwTech.Domain.Enums;

namespace MwTech.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensionsAppSettingsPosition
{
    public static void SeedAppSettingPositions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppSettingPosition>().HasData(
            new AppSettingPosition
            {
                Id = 1,
                Key = SettingsDict.HostSmtp,
                Value = "smtp.gmail.com",
                Description = "Host",
                Type = AppSettingType.Text,
                AppSettingId = 1,
                Order = 1
            },
            new AppSettingPosition
            {
                Id = 2,
                Key = SettingsDict.Port,
                Value = "587",
                Description = "Port",
                Type = AppSettingType.Integer,
                AppSettingId = 1,
                Order = 2
            },
            new AppSettingPosition
            {
                Id = 3,
                Key = SettingsDict.SenderEmail,
                Value = "mariusz.wieczorek.testy@gmail.com",
                Description = "Adres e-mail nadawcy",
                Type = AppSettingType.Text,
                AppSettingId = 1,
                Order = 3
            },
            new AppSettingPosition
            {
                Id = 4,
                Key = SettingsDict.SenderEmailPassword,
                Value = "rmhfvaurzyxnuztn",
                Description = "Hasło",
                Type = AppSettingType.Password,
                AppSettingId = 1,
                Order = 4
            },
            new AppSettingPosition
            {
                Id = 5,
                Key = SettingsDict.SenderName,
                Value = "Mariusz Wieczorek",
                Description = "Nazwa nadawcy",
                Type = AppSettingType.Text,
                AppSettingId = 1,
                Order = 5
            },
            new AppSettingPosition
            {
                Id = 6,
                Key = SettingsDict.SenderLogin,
                Value = "",
                Description = "Login nadawcy",
                Type = AppSettingType.Text,
                AppSettingId = 1,
                Order = 6
            },
            new AppSettingPosition
            {
                Id = 7,
                Key = SettingsDict.BannerVisible,
                Value = "True",
                Description = "Czy wyświetlać banner na stronie głównej?",
                Type = AppSettingType.Boolean,
                AppSettingId = 2,
                Order = 1
            },
            new AppSettingPosition
            {
                Id = 8,
                Key = SettingsDict.FooterColor,
                Value = "#dc3545",
                Description = "Folor footera strona głównej",
                Type = AppSettingType.Color,
                AppSettingId = 2,
                Order = 2
            },
            new AppSettingPosition
            {
                Id = 9,
                Key = SettingsDict.AdminEmail,
                Value = "mariusz.wieczorek@kabat.pl",
                Description = "Główny adres e-mail administratora",
                Type = AppSettingType.Text,
                AppSettingId = 2,
                Order = 3
            });
    }
}