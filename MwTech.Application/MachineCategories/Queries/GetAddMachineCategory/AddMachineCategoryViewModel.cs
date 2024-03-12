using MwTech.Application.MachineCategories.Commands.AddMachineCategory;
using MwTech.Domain.Entities;

namespace MwTech.Application.MachineCategories.Queries.GetAddMachineCategoryViewModel;

public class AddMachineCategoryViewModel
{
    public AddMachineCategoryCommand AddMachineCategoryCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
