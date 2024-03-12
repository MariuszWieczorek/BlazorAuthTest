using MwTech.Application.SettingCategories.Commands.AddSettingCategory;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetAddSettingCategoryViewModel;

public class AddSettingCategoryViewModel
{
    public AddSettingCategoryCommand AddSettingCategoryCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }

}
