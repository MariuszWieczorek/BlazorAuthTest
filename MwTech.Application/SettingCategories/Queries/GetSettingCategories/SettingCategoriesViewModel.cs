using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.SettingCategories.Queries.GetSettingCategories;

public class SettingCategoriesViewModel
{
    public IEnumerable<SettingCategory> SettingCategories { get; set; }
    public SettingCategoryFilter SettingCategoryFilter { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public IEnumerable<MachineCategory> MachineCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
