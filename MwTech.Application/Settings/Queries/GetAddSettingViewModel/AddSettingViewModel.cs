using MwTech.Application.Settings.Commands.AddSetting;
using MwTech.Domain.Entities;

namespace MwTech.Application.Settings.Queries.GetAddSettingViewModel;

public class AddSettingViewModel
{
    public AddSettingCommand AddSettingCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }
    public List<SettingCategory> SettingCategories { get; set; }
    public List<Unit> Units { get; set; }

}
