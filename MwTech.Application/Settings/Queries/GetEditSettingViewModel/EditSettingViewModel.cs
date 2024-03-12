using MwTech.Application.Settings.Commands.EditSetting;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Settings.Queries.GetEditSettingViewModel;

public class EditSettingViewModel
{
    public EditSettingCommand EditSettingCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }
    public List<SettingCategory> SettingCategories { get; set; }
    public List<Unit> Units { get; set; }

}
