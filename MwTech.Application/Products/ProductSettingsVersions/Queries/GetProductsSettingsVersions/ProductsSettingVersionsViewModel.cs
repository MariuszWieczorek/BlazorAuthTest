using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;

public class ProductsSettingVersionsViewModel
{
    public List<ProductSettingVersion> ProductsSettingsVersions { get; set; }
    public ProductSettingVersionFilter ProductSettingVersionFilter { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }
}
