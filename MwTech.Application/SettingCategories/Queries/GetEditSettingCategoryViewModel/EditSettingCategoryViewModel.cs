using MwTech.Application.SettingCategories.Commands.EditSettingCategory;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetEditSettingCategoryViewModel;

public class EditSettingCategoryViewModel
{
    public EditSettingCategoryCommand EditSettingCategoryCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }

}
