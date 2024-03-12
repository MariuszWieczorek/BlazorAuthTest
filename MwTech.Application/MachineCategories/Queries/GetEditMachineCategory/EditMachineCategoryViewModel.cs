using MwTech.Application.MachineCategories.Commands.EditMachineCategory;
using MwTech.Domain.Entities;

namespace MwTech.Application.MachineCategories.Queries.GetEditMachineCategoryViewModel;

public class EditMachineCategoryViewModel
{
    public EditMachineCategoryCommand EditMachineCategoryCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
