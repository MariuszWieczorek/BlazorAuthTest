using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.MachineCategories.Queries.GetMachineCategories;

public class MachineCategoriesViewModel
{
    public IEnumerable<MachineCategory> MachineCategories { get; set; }
    public MachineCategoryFilter MachineCategoryFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
