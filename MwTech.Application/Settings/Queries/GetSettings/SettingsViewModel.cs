using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.Settings.Queries.GetSettings;

public class SettingsViewModel
{
    public List<Setting> Settings { get; set; }
    public IEnumerable<SettingCategory> SettingCategories { get; set; }
    public IEnumerable<MachineCategory> MachineCategories { get; set; }
    public ApplicationUser CurrentUser { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public SettingFilter SettingFilter { get; set; }
    public int NumberOfRecords { get; set; }

}
