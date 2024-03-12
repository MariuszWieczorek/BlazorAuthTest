using MwTech.Domain.Enums;

namespace MwTech.Domain.Entities;
public class AppSettingPosition
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public AppSettingType Type { get; set; }
    public int Order { get; set; }
    public int AppSettingId { get; set; }
    public AppSetting AppSettings { get; set; }
}
