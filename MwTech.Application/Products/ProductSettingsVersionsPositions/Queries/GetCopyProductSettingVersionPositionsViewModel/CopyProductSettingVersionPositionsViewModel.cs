using MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;
using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.CopyProductSettingVersionPositions;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetCopyProductSettingVersionPositionsViewModel;

public class CopyProductSettingVersionPositionsViewModel
{
    public CopyProductSettingVersionPositionsCommand CopyProductSettingVersionPositionsCommand { get; set; }
    public ProductsSettingVersionsViewModel ProductsSettingVersionsViewModel { get; set; }
}
