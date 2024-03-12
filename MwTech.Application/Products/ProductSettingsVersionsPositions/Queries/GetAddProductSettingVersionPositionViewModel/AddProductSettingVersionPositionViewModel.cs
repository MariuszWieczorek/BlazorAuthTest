using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPosition;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetAddProductSettingVersionPositionViewModel;

public class AddProductSettingVersionPositionViewModel
{
    public AddProductSettingVersionPositionCommand AddProductSettingVersionPositionCommand { get; set; }
    public SettingsViewModel SettingsViewModel { get; set; }
}
