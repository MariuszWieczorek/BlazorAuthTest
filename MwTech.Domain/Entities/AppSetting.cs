namespace MwTech.Domain.Entities;

// Nagłówek do ustawień SettingSet
public class AppSetting
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }

    public ICollection<AppSettingPosition> Positions { get; set; } = new HashSet<AppSettingPosition>();
}
